using Business.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Sales
{
    public partial class Contacts : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadContactList();
                LoadContactsDropdowns();
                Message.Show = false;
                if (ContactId > 0)
                {
                    GetContactById();
                }
            }
        }
        public int ContactId
        {
            get { return Convert.ToInt32(ViewState["Id"]); }
            set { ViewState["Id"] = value; }
        }
        private void LoadContactsDropdowns()
        {
            Business.Sales.Contacts Obj = new Business.Sales.Contacts();
            Business.Sales.Account AccountObj = new Business.Sales.Account();
            Entity.Sales.GetAccountsParam Param = new Entity.Sales.GetAccountsParam
            {
                Name = null,
                OfficePhone = null
            };
            ddlAccount.DataSource = AccountObj.GetAllAccounts(Param);
            ddlAccount.DataTextField = "Name";
            ddlAccount.DataValueField = "Id";
            ddlAccount.DataBind();
            ddlAccount.InsertSelect();

            ddlDesignation.DataSource = Obj.GetDesignations();
            ddlDesignation.DataTextField = "DesignationName";
            ddlDesignation.DataValueField = "DesignationMasterId";
            ddlDesignation.DataBind();
            ddlDesignation.InsertSelect();
        }
        private void LoadContactList()
        {
            Business.Sales.Contacts Obj = new Business.Sales.Contacts();
            Entity.Sales.GetContactsParam Param = new Entity.Sales.GetContactsParam { Name = null, AccountId = null, Mobile = null };
            //List<Entity.Sales.GetCalls> EntityObj = new List<Entity.Sales.GetCalls>();
            gvContact.DataSource = Obj.GetAllContacts(Param);
            gvContact.DataBind();
        }
        private void ClearControls()
        {
            ContactId = 0;
            Message.Show = false;
            txtMobile.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtEmailId.Text = string.Empty;            
            txtOfficePhone.Text = string.Empty;
            txtgstNo.Text = string.Empty;
            txtName.Text = string.Empty;
            ddlAccount.SelectedIndex = 0;
            ddlDesignation.SelectedIndex = 0;
            btnSave.Text = "Save";
        }
        private bool ContactControlValidation()
        {
            if (txtName.Text.Trim() == string.Empty)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Enter Contact Name";
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
        protected void gvContact_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ed")
            {
                ContactId = Convert.ToInt32(e.CommandArgument.ToString());
                GetContactById();
                Message.Show = false;
                btnSave.Text = "Update";
            }
            else if (e.CommandName == "Del")
            {
                Business.Sales.Contacts Obj = new Business.Sales.Contacts();
                int rows = Obj.DeleteContacts(Convert.ToInt32(e.CommandArgument.ToString()));
                if (rows > 0)
                {
                    ClearControls();
                    LoadContactList();
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
        private void GetContactById()
        {
            Business.Sales.Contacts Obj = new Business.Sales.Contacts();
            Entity.Sales.Contacts Contacts = Obj.GetContactById(ContactId);
            if (Contacts.Id != 0)
            {
                ddlDesignation.SelectedValue = Contacts.DesignationId == null ? "0" : Contacts.DesignationId.ToString();
                ddlAccount.SelectedValue = Contacts.AccountId == null ? "0" : Contacts.AccountId.ToString();
                txtDescription.Text = Contacts.Description;                
                txtEmailId.Text = Contacts.Email;
                txtMobile.Text = Contacts.Mobile;
                txtgstNo.Text = Contacts.GSTNo.ToString();
                txtName.Text = Contacts.Name;
                txtOfficePhone.Text = Contacts.OfficePhone;
            }
        }
        private void Save()
        {
            if (ContactControlValidation())
            {
                Business.Sales.Contacts Obj = new Business.Sales.Contacts();
                Entity.Sales.Contacts Model = new Entity.Sales.Contacts
                {
                    Id = ContactId,
                    DesignationId = ddlDesignation.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDesignation.SelectedValue),
                    AccountId = ddlAccount.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlAccount.SelectedValue),
                    CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name),
                    Description = txtDescription.Text,
                    Name = txtName.Text,
                    Email = txtEmailId.Text,
                    Mobile = txtMobile.Text,
                    GSTNo = txtgstNo.Text,
                    OfficePhone = txtOfficePhone.Text,
                    IsActive = true
                };
                int rows = Obj.SaveContacts(Model);
                if (rows > 0)
                {
                    ClearControls();
                    LoadContactList();
                    ContactId = 0;
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
    }
}