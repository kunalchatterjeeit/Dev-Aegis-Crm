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
    public partial class Opportunity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               // Business.Common.Context.ReferralUrl = HttpContext.Current.Request.UrlReferrer.AbsoluteUri;
                LoadOpportunityList();
                LoadOpportunityDropdowns();
                Message.Show = false;
                if (OpportunityId > 0)
                {
                    GetOpportunityById();
                }
            }
        }
        private void LoadCallList()
        {
            Business.Sales.Calls Obj = new Business.Sales.Calls();
            Entity.Sales.GetCallsParam Param = new Entity.Sales.GetCallsParam
            {
                StartDateTime = DateTime.MinValue,
                EndDateTime = DateTime.MinValue,
                LinkId = OpportunityId,
                LinkType = SalesLinkType.Opportunity
            };
            gvCalls.DataSource = Obj.GetAllCalls(Param);
            gvCalls.DataBind();
        }
        private void LoadMeetingList()
        {
            Business.Sales.Meetings Obj = new Business.Sales.Meetings();
            Entity.Sales.GetMeetingsParam Param = new Entity.Sales.GetMeetingsParam
            {
                StartDateTime = DateTime.MinValue,
                EndDateTime = DateTime.MinValue,
                LinkId = OpportunityId,
                LinkType = SalesLinkType.Opportunity
            };
            gvMeetingss.DataSource = Obj.GetAllMeetings(Param);
            gvMeetingss.DataBind();
        }
        private void LoadNotesList()
        {
            Business.Sales.Notes Obj = new Business.Sales.Notes();
            Entity.Sales.GetNotesParam Param = new Entity.Sales.GetNotesParam
            {
                LinkId = OpportunityId,
                LinkType = SalesLinkType.Opportunity
            };
            gvNotes.DataSource = Obj.GetAllNotes(Param);
            gvNotes.DataBind();
        }
        private void LoadTaskList()
        {
            Business.Sales.Tasks Obj = new Business.Sales.Tasks();
            Entity.Sales.GetTasksParam Param = new Entity.Sales.GetTasksParam
            {
                StartDateTime = DateTime.MinValue,
                EndDateTime = DateTime.MinValue,
                LinkId = OpportunityId,
                LinkType = SalesLinkType.Opportunity
            };
            gvTasks.DataSource = Obj.GetAllTasks(Param);
            gvTasks.DataBind();
        }
        private void PopulateItems()
        {
            if (OpportunityId == 0)
            {
                btnCreateNewCall.Enabled = false;
                btnCreateNewMeeting.Enabled = false;
                btnCreateNewNote.Enabled = false;
                btnCreateNewTask.Enabled = false;
            }
            else
            {
                btnCreateNewCall.Enabled = true;
                btnCreateNewMeeting.Enabled = true;
                btnCreateNewNote.Enabled = true;
                btnCreateNewTask.Enabled = true;

                LoadCallList();
                LoadMeetingList();
                LoadNotesList();
                LoadTaskList();
                SetCreateLinks();
            }
        }
        private void SetCreateLinks()
        {
            btnCreateNewCall.PostBackUrl = string.Concat("Calls.aspx?id=", OpportunityId, "&itemtype=", SalesLinkType.Opportunity);
            btnCreateNewMeeting.PostBackUrl = string.Concat("Meeting.aspx?id=", OpportunityId, "&itemtype=", SalesLinkType.Opportunity);
            btnCreateNewNote.PostBackUrl = string.Concat("Notes.aspx?id=", OpportunityId, "&itemtype=", SalesLinkType.Opportunity);
            btnCreateNewTask.PostBackUrl = string.Concat("Task.aspx?id=", OpportunityId, "&itemtype=", SalesLinkType.Opportunity);
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
            txtExpectedCloseDate.Text = string.Empty;
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
            else if (e.CommandName == "View")
            {
                OpportunityId = Convert.ToInt32(e.CommandArgument.ToString());
                GetOpportunityById();
                PopulateItems();
                hdnOpenForm.Value = "true";
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
                txtExpectedCloseDate.Text = Opportunity.ExpectedCloseDate == null ? string.Empty : Opportunity.ExpectedCloseDate.GetValueOrDefault().ToString("dd MMM yyyy");
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
                    ExpectedCloseDate = txtExpectedCloseDate.Text == "" ? (DateTime?)null : Convert.ToDateTime(txtExpectedCloseDate.Text),
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
        protected void gvCalls_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ed")
            {
                Response.Redirect(string.Concat("Calls.aspx?id=", OpportunityId, "&itemtype=", SalesLinkType.Account, "&callid=", e.CommandArgument.ToString()));
            }
            else if (e.CommandName == "Del")
            {
                Business.Sales.Calls Obj = new Business.Sales.Calls();
                int rows = Obj.DeleteCalls(Convert.ToInt32(e.CommandArgument.ToString()));
                if (rows > 0)
                {
                    ClearControls();
                    LoadCallList();
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

        protected void gvMeetingss_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ed")
            {
                Response.Redirect(string.Concat("Meeting.aspx?id=", OpportunityId, "&itemtype=", SalesLinkType.Account, "&meetingid=", e.CommandArgument.ToString()));
            }
            else if (e.CommandName == "Del")
            {
                Business.Sales.Meetings Obj = new Business.Sales.Meetings();
                int rows = Obj.DeleteMeetings(Convert.ToInt32(e.CommandArgument.ToString()));
                if (rows > 0)
                {
                    ClearControls();
                    LoadMeetingList();
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

        protected void gvNotes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ed")
            {
                Response.Redirect(string.Concat("Notes.aspx?id=", OpportunityId, "&itemtype=", SalesLinkType.Account, "&noteid=", e.CommandArgument.ToString()));
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

        protected void gvTasks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ed")
            {
                Response.Redirect(string.Concat("Task.aspx?id=", OpportunityId, "&itemtype=", SalesLinkType.Account, "&taskid=", e.CommandArgument.ToString()));
            }
            else if (e.CommandName == "Del")
            {
                Business.Sales.Tasks Obj = new Business.Sales.Tasks();
                int rows = Obj.DeleteTasks(Convert.ToInt32(e.CommandArgument.ToString()));
                if (rows > 0)
                {
                    ClearControls();
                    LoadTaskList();
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
    }
}