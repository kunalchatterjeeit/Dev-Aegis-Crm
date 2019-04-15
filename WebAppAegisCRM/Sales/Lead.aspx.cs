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
    public partial class Lead : System.Web.UI.Page
    {
        public int LeadId
        {
            get { return Convert.ToInt32(ViewState["Id"]); }
            set { ViewState["Id"] = value; }
        }
        private void LoadCallList()
        {
            Business.Sales.Calls Obj = new Business.Sales.Calls();
            Entity.Sales.GetCallsParam Param = new Entity.Sales.GetCallsParam { StartDateTime = null, EndDateTime = null, CallStatusId = null, Subject = null };
            gvCalls.DataSource = Obj.GetAllCalls(Param);
            gvCalls.DataBind();
        }
        private void LoadMeetingList()
        {
            Business.Sales.Meetings Obj = new Business.Sales.Meetings();
            Entity.Sales.GetMeetingsParam Param = new Entity.Sales.GetMeetingsParam { StartDateTime = null, EndDateTime = null, MeetingStatusId = null, MeetingTypeId = null, Name = null };
            gvMeetingss.DataSource = Obj.GetAllMeetings(Param);
            gvMeetingss.DataBind();
        }
        private void LoadNotesList()
        {
            Business.Sales.Notes Obj = new Business.Sales.Notes();
            Entity.Sales.GetNotesParam Param = new Entity.Sales.GetNotesParam { ContactId = null, Name = null };
            gvNotes.DataSource = Obj.GetAllNotes(Param);
            gvNotes.DataBind();
        }
        private void LoadTaskList()
        {
            Business.Sales.Tasks Obj = new Business.Sales.Tasks();
            Entity.Sales.GetTasksParam Param = new Entity.Sales.GetTasksParam { StartDateTime = null, EndDateTime = null, TaskPriorityId = null, Subject = null, TaskRelatedToId = null, TaskStatusId = null };
            gvTasks.DataSource = Obj.GetAllTasks(Param);
            gvTasks.DataBind();
        }
        private void PopulateItems()
        {
            if (LeadId == 0)
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
            btnCreateNewCall.PostBackUrl = string.Concat("Calls.aspx?id=", LeadId, "&itemtype=", SalesLinkType.Lead);
            btnCreateNewMeeting.PostBackUrl = string.Concat("Meeting.aspx?id=", LeadId, "&itemtype=", SalesLinkType.Lead);
            btnCreateNewNote.PostBackUrl = string.Concat("Notes.aspx?id=", LeadId, "&itemtype=", SalesLinkType.Lead);
            btnCreateNewTask.PostBackUrl = string.Concat("Task.aspx?id=", LeadId, "&itemtype=", SalesLinkType.Lead);
        }
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
                PopulateItems();
                hdnOpenForm.Value = "true";
            }
            else if (e.CommandName == "View")
            {
                LeadId = Convert.ToInt32(e.CommandArgument.ToString());
                GetLeadById();
                PopulateItems();
                hdnOpenForm.Value = "true";
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

        protected void gvCalls_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ed")
            {
                Response.Redirect(string.Concat("Calls.aspx?id=", LeadId, "&itemtype=", SalesLinkType.Lead, "&callid=", e.CommandArgument.ToString()));
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
                Response.Redirect(string.Concat("Meeting.aspx?id=", LeadId, "&itemtype=", SalesLinkType.Lead, "&meetingid=", e.CommandArgument.ToString()));
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
                Response.Redirect(string.Concat("Notes.aspx?id=", LeadId, "&itemtype=", SalesLinkType.Lead, "&noteid=", e.CommandArgument.ToString()));
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
                Response.Redirect(string.Concat("Task.aspx?id=", LeadId, "&itemtype=", SalesLinkType.Lead, "&taskid=", e.CommandArgument.ToString()));
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