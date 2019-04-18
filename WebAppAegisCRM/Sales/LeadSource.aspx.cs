using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Sales
{
    public partial class LeadSource : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadLeadSourceList();
                Message.Show = false;
                if (LeadSourceId > 0)
                {
                    GetLeadSourceById();
                }
            }
        }
        public int LeadSourceId
        {
            get { return Convert.ToInt32(ViewState["Id"]); }
            set { ViewState["Id"] = value; }
        }
        private void LoadLeadSourceList()
        {
            Business.Sales.LeadSource Obj = new Business.Sales.LeadSource();
            gvLeadSource.DataSource = Obj.GetAllLeadSource();
            gvLeadSource.DataBind();
        }
        private void ClearControls()
        {
            LeadSourceId = 0;
            Message.Show = false;
            txtDescription.Text = string.Empty;
            txtName.Text = string.Empty;
            btnSave.Text = "Save";
        }
        private bool LeadSourceStatusControlValidation()
        {
            if (txtName.Text.Trim() == string.Empty)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Enter Lead Source Name";
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
        protected void gvLeadSource_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ed")
            {
                LeadSourceId = Convert.ToInt32(e.CommandArgument.ToString());
                GetLeadSourceById();
                Message.Show = false;
                btnSave.Text = "Update";
            }
            else if (e.CommandName == "Del")
            {
                Business.Sales.LeadSource Obj = new Business.Sales.LeadSource();
                int rows = Obj.DeleteLeadSource(Convert.ToInt32(e.CommandArgument.ToString()));
                if (rows > 0)
                {
                    ClearControls();
                    LoadLeadSourceList();
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
        private void GetLeadSourceById()
        {
            Business.Sales.LeadSource Obj = new Business.Sales.LeadSource();
            Entity.Sales.LeadSource LeadSource = Obj.GetLeadSourceById(LeadSourceId);
            if (LeadSource.Id != 0)
            {
                txtDescription.Text = LeadSource.Description;
                txtName.Text = LeadSource.Name;
            }
        }
        private void Save()
        {
            if (LeadSourceStatusControlValidation())
            {
                Business.Sales.LeadSource Obj = new Business.Sales.LeadSource();
                Entity.Sales.LeadSource Model = new Entity.Sales.LeadSource
                {
                    Id = LeadSourceId,
                    Name = txtName.Text,
                    Description = txtDescription.Text,

                };
                int rows = Obj.SaveLeadSource(Model);
                if (rows > 0)
                {
                    ClearControls();
                    LoadLeadSourceList();
                    LeadSourceId = 0;
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