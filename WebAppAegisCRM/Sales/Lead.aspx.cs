using Business.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Sales
{
    public partial class Lead : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Business.Common.Context.ReferralUrl = HttpContext.Current.Request.UrlReferrer.AbsoluteUri;
                LoadLeadsDropdowns();
                LoadLeadList();
                Message.Show = false;
            }
        }
        public int LeadId
        {
            get { return Convert.ToInt32(ViewState["Id"]); }
            set { ViewState["Id"] = value; }
        }
        private void LoadLeadsDropdowns()
        {
            Business.Sales.Leads Obj = new Business.Sales.Leads();
            Business.Sales.Campaign CampaignObj = new Business.Sales.Campaign();

            ddlCampaign.DataSource = CampaignObj.GetCampaign();
            ddlCampaign.DataTextField = "Name";
            ddlCampaign.DataValueField = "Id";
            ddlCampaign.DataBind();
            ddlCampaign.InsertSelect();

            ddlDepartment.DataSource = Obj.GetDepartment();
            ddlDepartment.DataTextField = "Name";
            ddlDepartment.DataValueField = "Id";
            ddlDepartment.DataBind();
            ddlDepartment.InsertSelect();
        }
        private void LoadLeadList()
        {
            Business.Sales.Leads Obj = new Business.Sales.Leads();
            Entity.Sales.GetLeadsParam Param = new Entity.Sales.GetLeadsParam { CampaignId = null, DepartmentId = null, Name = null, Email = null };
            //List<Entity.Sales.GetCalls> EntityObj = new List<Entity.Sales.GetCalls>();
            gvLeads.DataSource = Obj.GetAllLeads(Param);
            gvLeads.DataBind();
        }
        private void ClearControls()
        {
            LeadId = 0;
            Message.Show = false;
            txtAlternateAddress.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtEmailId.Text = string.Empty;
            txtFax.Text = string.Empty;
            txtLeadScore.Text = string.Empty;
            txtOfficePhone.Text = string.Empty;
            txtPrimaryAddress.Text = string.Empty;
            txtWebsite.Text = string.Empty;
            txtName.Text = string.Empty;
            ddlCampaign.SelectedIndex = 0;
            ddlDepartment.SelectedIndex = 0;
            btnSave.Text = "Save";
        }
        private bool LeadControlValidation()
        {
            if (txtName.Text.Trim() == string.Empty)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Enter Lead Name";
                Message.Show = true;
                return false;
            }
            else if (txtOfficePhone.Text.Trim() == string.Empty)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Enter Office Phone No";
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
        protected void gvLeads_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ed")
            {
                LeadId = Convert.ToInt32(e.CommandArgument.ToString());
                GetLeadById();
                Message.Show = false;
                btnSave.Text = "Update";
            }
            else if (e.CommandName == "Del")
            {
                Business.Sales.Leads Obj = new Business.Sales.Leads();
                int rows = Obj.DeleteLeads(Convert.ToInt32(e.CommandArgument.ToString()));
                if (rows > 0)
                {
                    ClearControls();
                    LoadLeadList();
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
        private void GetLeadById()
        {
            Business.Sales.Leads Obj = new Business.Sales.Leads();
            Entity.Sales.Leads Leads = Obj.GetLeadById(LeadId);
            if (Leads.Id != 0)
            {
                ddlCampaign.SelectedValue = Leads.CampaignId == null ? "0" : Leads.CampaignId.ToString();
                ddlDepartment.SelectedValue = Leads.DepartmentId == null ? "0" : Leads.DepartmentId.ToString();
                txtDescription.Text = Leads.Description;
                txtAlternateAddress.Text = Leads.AlternateAddress;
                txtEmailId.Text = Leads.Email;
                txtFax.Text = Leads.Fax;
                txtLeadScore.Text = Leads.LeadScore.ToString();
                txtName.Text = Leads.Name;
                txtOfficePhone.Text = Leads.OfficePhone;
                txtPrimaryAddress.Text = Leads.PrimaryAddress;
                txtWebsite.Text = Leads.Website;
            }
        }
        private void Save()
        {
            if (LeadControlValidation())
            {
                Business.Sales.Leads Obj = new Business.Sales.Leads();
                Entity.Sales.Leads Model = new Entity.Sales.Leads
                {
                    Id = LeadId,
                    DepartmentId = ddlDepartment.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlDepartment.SelectedValue),
                    CampaignId = ddlCampaign.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCampaign.SelectedValue),
                    CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name),
                    Description = txtDescription.Text,
                    Name = txtName.Text,
                    AlternateAddress = txtAlternateAddress.Text,
                    Email = txtEmailId.Text,
                    Fax = txtFax.Text,
                    LeadScore = txtLeadScore.Text == "" ? (decimal?)null : Convert.ToDecimal(txtLeadScore.Text),
                    OfficePhone = txtOfficePhone.Text,
                    PrimaryAddress = txtPrimaryAddress.Text,
                    Website = txtWebsite.Text,
                    IsActive = true
                };
                int rows = Obj.SaveLeads(Model);
                if (rows > 0)
                {
                    ClearControls();
                    LoadLeadList();
                    LeadId = 0;
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