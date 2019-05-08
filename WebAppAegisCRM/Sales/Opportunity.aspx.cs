using Business.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Sales
{
    public partial class Opportunity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadOpportunityList();
                LoadOpportunityDropdowns();
                Message.Show = false;
                if (OpportunityId > 0)
                {
                    GetOpportunityById();
                }
            }
        }
        public int OpportunityId
        {
            get { return Convert.ToInt32(ViewState["Id"]); }
            set { ViewState["Id"] = value; }
        }
        private void LoadOpportunityList()
        {
            Business.Sales.Opportunity Obj = new Business.Sales.Opportunity();
            Entity.Sales.GetOpportunityParam Param = new Entity.Sales.GetOpportunityParam
            {
                BestPrice = null, CommitStageId = null, Name = null
            };
            gvOpportunity.DataSource = Obj.GetAllOpportunity(Param);
            gvOpportunity.DataBind();
        }
        private void LoadOpportunityDropdowns()
        {
            Business.Sales.Opportunity Obj = new Business.Sales.Opportunity();
            Entity.Sales.GetCampaignParam Param = new Entity.Sales.GetCampaignParam
            {
                EndDate = DateTime.MinValue,
                StartDate = DateTime.MinValue,
                Name = null
            };
            ddlCampaign.DataSource = Obj.GetAllCampaign(Param);
            ddlCampaign.DataTextField = "Name";
            ddlCampaign.DataValueField = "Id";
            ddlCampaign.DataBind();
            ddlCampaign.InsertSelect();

            ddlCommitStage.DataSource = Obj.GetCommitStage();
            ddlCommitStage.DataTextField = "Name";
            ddlCommitStage.DataValueField = "Id";
            ddlCommitStage.DataBind();
            ddlCommitStage.InsertSelect();

            ddlLeadSource.DataSource = Obj.GetAllLeadSource();
            ddlLeadSource.DataTextField = "Name";
            ddlLeadSource.DataValueField = "Id";
            ddlLeadSource.DataBind();
            ddlLeadSource.InsertSelect();
        }
        private void ClearControls()
        {
            OpportunityId = 0;
            Message.Show = false;
            txtBestPrice.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtExpectedCloseDate.Value = string.Empty;
            txtLikelyPrice.Text = string.Empty;
            txtName.Text = string.Empty;
            txtSourceName.Text = string.Empty;
            txtWorstPrice.Text = string.Empty;
            ddlCampaign.SelectedIndex = 0;
            ddlCommitStage.SelectedIndex = 0;
            ddlLeadSource.SelectedIndex = 0;
            btnSave.Text = "Save";
        }
        private bool OpportunityControlValidation()
        {
            if (txtName.Text.Trim() == string.Empty)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Enter Opportunity Name";
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
        protected void gvOpportunity_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ed")
            {
                OpportunityId = Convert.ToInt32(e.CommandArgument.ToString());
                GetOpportunityById();
                Message.Show = false;
                btnSave.Text = "Update";
            }
            else if (e.CommandName == "Del")
            {
                Business.Sales.Opportunity Obj = new Business.Sales.Opportunity();
                int rows = Obj.DeleteOpportunities(Convert.ToInt32(e.CommandArgument.ToString()));
                if (rows > 0)
                {
                    ClearControls();
                    LoadOpportunityList();
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
        private void GetOpportunityById()
        {
            Business.Sales.Opportunity Obj = new Business.Sales.Opportunity();
            Entity.Sales.Opportunity Opportunity = Obj.GetOpportunityById(OpportunityId);
            if (Opportunity.Id != 0)
            {
                txtName.Text = Opportunity.Name;
                txtDescription.Text = Opportunity.Description;
                txtExpectedCloseDate.Value = Opportunity.ExpectedCloseDate == null ? string.Empty : Opportunity.ExpectedCloseDate.ToString();
                txtLikelyPrice.Text = Opportunity.LikelyPrice == null ? string.Empty : Opportunity.LikelyPrice.ToString();
                txtBestPrice.Text = Opportunity.BestPrice == null ? string.Empty : Opportunity.BestPrice.ToString();
                txtWorstPrice.Text = Opportunity.WorstPrice == null ? string.Empty : Opportunity.WorstPrice.ToString();
                txtSourceName.Text = Opportunity.SourceName;
                ddlCampaign.SelectedValue = Opportunity.CampaignId == null ? "0" : Opportunity.CampaignId.ToString();
                ddlCommitStage.SelectedValue = Opportunity.CommitStageId == null ? "0" : Opportunity.CommitStageId.ToString();
                ddlLeadSource.SelectedValue = Opportunity.LeadSource == null ? "0" : Opportunity.LeadSource.ToString();
            }
        }
        private void Save()
        {
            if (OpportunityControlValidation())
            {
                Business.Sales.Opportunity Obj = new Business.Sales.Opportunity();
                Entity.Sales.Opportunity Model = new Entity.Sales.Opportunity
                {
                    Id = OpportunityId,
                    CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name),
                    Description = txtDescription.Text,
                    Name = txtName.Text,
                    SourceName = txtSourceName.Text,
                    ExpectedCloseDate = txtExpectedCloseDate.Value == "" ? (DateTime?)null : Convert.ToDateTime(txtExpectedCloseDate.Value),
                    BestPrice = txtBestPrice.Text == "" ? (decimal?)null : Convert.ToDecimal(txtBestPrice.Text),
                    LikelyPrice = txtLikelyPrice.Text == "" ? (decimal?)null : Convert.ToDecimal(txtLikelyPrice.Text),
                    WorstPrice = txtWorstPrice.Text == "" ? (decimal?)null : Convert.ToDecimal(txtWorstPrice.Text),
                    CampaignId = ddlCampaign.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCampaign.SelectedValue),
                    CommitStageId= ddlCommitStage.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCommitStage.SelectedValue),
                    LeadSource = ddlLeadSource.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlLeadSource.SelectedValue),
                    IsActive = true
                };
                int rows = Obj.SaveOpportunity(Model);
                if (rows > 0)
                {
                    ClearControls();
                    LoadOpportunityList();
                    OpportunityId = 0;
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