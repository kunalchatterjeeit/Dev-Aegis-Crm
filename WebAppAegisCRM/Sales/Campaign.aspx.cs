using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Sales
{
    public partial class Campaign : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {               
                LoadCampaignList();
                Message.Show = false;
                if (CampaignId > 0)
                {
                    GetCampaignById();
                }
            }
        }
        public int CampaignId
        {
            get { return Convert.ToInt32(ViewState["Id"]); }
            set { ViewState["Id"] = value; }
        }
        private void LoadCampaignList()
        {
            Business.Sales.Campaign Obj = new Business.Sales.Campaign();
            Entity.Sales.GetCampaignParam Param = new Entity.Sales.GetCampaignParam
            {
                EndDate = DateTime.MinValue,
                StartDate = DateTime.MinValue,
                Name=null
            };
            gvCampaign.DataSource = Obj.GetAllCampaign(Param);
            gvCampaign.DataBind();
        }
        private void ClearControls()
        {
            CampaignId = 0;
            Message.Show = false;
            txtStartDate.Value = string.Empty;
            txtDescription.Text = string.Empty;
            txtEndDate.Value = string.Empty;
            txtName.Text = string.Empty;
            txtReason.Text = string.Empty;
            btnSave.Text = "Save";
        }
        private bool CampaignControlValidation()
        {
            if (txtName.Text.Trim() == string.Empty)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Enter Campaign Name";
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
        protected void gvCampaign_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ed")
            {
                CampaignId = Convert.ToInt32(e.CommandArgument.ToString());
                GetCampaignById();
                Message.Show = false;
                btnSave.Text = "Update";
            }
            else if (e.CommandName == "Del")
            {
                Business.Sales.Campaign Obj = new Business.Sales.Campaign();
                int rows = Obj.DeleteCampaign(Convert.ToInt32(e.CommandArgument.ToString()));
                if (rows > 0)
                {
                    ClearControls();
                    LoadCampaignList();
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
        private void GetCampaignById()
        {
            Business.Sales.Campaign Obj = new Business.Sales.Campaign();
            Entity.Sales.Campaign Campaign = Obj.GetCampaignById(CampaignId);
            if (Campaign.Id != 0)
            {
                txtName.Text = Campaign.Name;
                txtReason.Text = Campaign.Reason;
                txtDescription.Text = Campaign.Description;
                txtStartDate.Value = Campaign.EndDate == null ? string.Empty: Campaign.StartDate.ToString();
                txtEndDate.Value = Campaign.EndDate == null ? string.Empty : Campaign.EndDate.ToString();
            }
        }
        private void Save()
        {
            if (CampaignControlValidation())
            {
                Business.Sales.Campaign Obj = new Business.Sales.Campaign();
                Entity.Sales.Campaign Model = new Entity.Sales.Campaign
                {
                    Id = CampaignId,                    
                    CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name),
                    Description = txtDescription.Text,
                    Name = txtName.Text,
                    Reason=txtReason.Text,
                    StartDate = txtStartDate.Value == "" ? (DateTime?)null : Convert.ToDateTime(txtStartDate.Value),
                    EndDate = txtEndDate.Value == "" ? (DateTime?)null : Convert.ToDateTime(txtEndDate.Value),
                    IsActive = true
                };
                int rows = Obj.SaveCampaign(Model);
                if (rows > 0)
                {                    
                    ClearControls();
                    LoadCampaignList();
                    CampaignId = 0;
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