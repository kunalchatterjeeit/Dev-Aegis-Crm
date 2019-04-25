using Business.Common;
using Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Sales
{
    public partial class Notes : System.Web.UI.Page
    {
        private void SetQueryStringValue()
        {
            if (Request.QueryString["id"] != null && Request.QueryString["itemtype"] != null)
            {
                hdnItemId.Value = Request.QueryString["id"].ToString();
                hdnItemType.Value = Request.QueryString["itemtype"].ToString();
            }
            if (Request.QueryString["noteid"] != null)
            {
                NoteId = Convert.ToInt32(Request.QueryString["noteid"].ToString());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SetQueryStringValue();
            }

            if (string.IsNullOrEmpty(hdnItemType.Value) || string.IsNullOrEmpty(hdnItemId.Value))
            {
                ModalPopupExtender1.Show();
            }
            if (!IsPostBack)
            {
                Business.Common.Context.ReferralUrl = HttpContext.Current.Request.UrlReferrer.AbsoluteUri;
                LoadContacts();
                LoadNotesList();
                Message.Show = false;
                if (NoteId > 0)
                {
                    GetNoteById();
                }
            }
        }
        public int NoteId
        {
            get { return Convert.ToInt32(ViewState["Id"]); }
            set { ViewState["Id"] = value; }
        }
        private void LoadContacts()
        {
            Business.Sales.Notes Obj = new Business.Sales.Notes();
            Entity.Sales.GetContactsParam Param = new Entity.Sales.GetContactsParam
            {
                Name = null, AccountId = null, Mobile = null
            };
            ddlContact.DataSource = Obj.GetAllContacts(Param);
            ddlContact.DataTextField = "Name";
            ddlContact.DataValueField = "Id";
            ddlContact.DataBind();
            ddlContact.InsertSelect();
        }
        private void LoadNotesList()
        {
            Business.Sales.Notes Obj = new Business.Sales.Notes();
            Entity.Sales.GetNotesParam Param = new Entity.Sales.GetNotesParam {
                LinkId = (!string.IsNullOrEmpty(hdnItemType.Value)) ? Convert.ToInt32(hdnItemId.Value) : 0,
                LinkType = (!string.IsNullOrEmpty(hdnItemType.Value)) ? (SalesLinkType)Enum.Parse(typeof(SalesLinkType), hdnItemType.Value) : SalesLinkType.None
            };
            gvNotes.DataSource = Obj.GetAllNotes(Param);
            gvNotes.DataBind();
        }
        private void ClearControls()
        {
            NoteId = 0;
            Message.Show = false;
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;            
            ddlContact.SelectedIndex = 0;           
            btnSave.Text = "Save";
        }
        private bool NoteControlValidation()
        {
            if (txtName.Text.Trim() == string.Empty)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Enter Note Name";
                Message.Show = true;
                return false;
            }            
            else if (ddlContact.SelectedIndex == 0)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Select Contact Name";
                Message.Show = true;
                return false;
            }
            return true;
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        protected void gvNotes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ed")
            {
                NoteId = Convert.ToInt32(e.CommandArgument.ToString());
                GetNoteById();
                Message.Show = false;
                btnSave.Text = "Update";
            }
            else if (e.CommandName == "Del")
            {
                Business.Sales.Notes Obj = new Business.Sales.Notes();
                int rows = Obj.DeleteNotes(Convert.ToInt32(e.CommandArgument.ToString()));
                if (rows > 0)
                {
                    ClearControls();
                    LoadNotesList();
                    Message.IsSuccess = true;
                    Message.Text = "Deleted Successfully";
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Data Dependency Exists";
                }
                Message.Show = true;
            }
        }
        private void GetNoteById()
        {
            Business.Sales.Notes Obj = new Business.Sales.Notes();
            Entity.Sales.Notes notes = Obj.GetNoteById(NoteId);
            if (notes.Id != 0)
            {                
                ddlContact.SelectedValue = notes.ContactId.ToString();
                txtDescription.Text = notes.Description;
                txtName.Text = notes.Name.ToString();                
            }
        }
        private void Save()
        {
            if (NoteControlValidation())
            {
                Business.Sales.Notes Obj = new Business.Sales.Notes();
                Entity.Sales.Notes Model = new Entity.Sales.Notes
                {
                    Id = NoteId,
                    ContactId = Convert.ToInt32(ddlContact.SelectedValue),                   
                    CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name),
                    Description = txtDescription.Text,
                    Name = txtName.Text,
                    IsActive = true
                };
                int rows = Obj.SaveNotes(Model);
                if (rows > 0)
                {
                    SaveNoteLinks();
                    ClearControls();
                    LoadNotesList();
                    NoteId = 0;
                    Message.IsSuccess = true;
                    Message.Text = "Saved Successfully";
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Unable to save data.";
                }
                Message.Show = true;
            }
        }

        private void SaveNoteLinks()
        {
            Business.Sales.Notes Obj = new Business.Sales.Notes();
            Entity.Sales.Notes Model = new Entity.Sales.Notes
            {
                Id = NoteId,
                LinkId = Convert.ToInt32(hdnItemId.Value),
                LinkType = (SalesLinkType)Enum.Parse(typeof(SalesLinkType), hdnItemType.Value)
            };
            Obj.SaveNoteLinks(Model);
        }
    }
}