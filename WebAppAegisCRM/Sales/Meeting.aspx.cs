using Business.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Sales
{
    public partial class Meeting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Business.Common.Context.ReferralUrl = Request.UrlReferrer.AbsoluteUri;
            if (string.IsNullOrEmpty(hdnItemType.Value) || string.IsNullOrEmpty(hdnItemId.Value))
            {
                ModalPopupExtender1.Show();
            }
            if (!IsPostBack)
            {
                LoadMeetingsDropdowns();
                LoadMeetingList();
                Message.Show = false;
            }
        }
        public int MeetingId
        {
            get { return Convert.ToInt32(ViewState["Id"]); }
            set { ViewState["Id"] = value; }
        }
        private void LoadMeetingsDropdowns()
        {
            Business.Sales.Meetings Obj = new Business.Sales.Meetings();

            ddlMeetingType.DataSource = Obj.GetMeetingType();
            ddlMeetingType.DataTextField = "Name";
            ddlMeetingType.DataValueField = "Id";
            ddlMeetingType.DataBind();
            ddlMeetingType.InsertSelect();

            ddlMeetingStatus.DataSource = Obj.GetMeetingStatus();
            ddlMeetingStatus.DataTextField = "Name";
            ddlMeetingStatus.DataValueField = "Id";
            ddlMeetingStatus.DataBind();
            ddlMeetingStatus.InsertSelect();
            
        }
        private void LoadMeetingList()
        {
            Business.Sales.Meetings Obj = new Business.Sales.Meetings();
            Entity.Sales.GetMeetingsParam Param = new Entity.Sales.GetMeetingsParam { StartDateTime = null, EndDateTime = null, MeetingStatusId = null, MeetingTypeId = null, Name = null };
            //List<Entity.Sales.GetCalls> EntityObj = new List<Entity.Sales.GetCalls>();
            gvMeetingss.DataSource = Obj.GetAllMeetings(Param);
            gvMeetingss.DataBind();
        }
        private void ClearControls()
        {
            MeetingId = 0;
            Message.Show = false;
            txtMeetingStartDateTime.Value = string.Empty;
            txtDescription.Text = string.Empty;
            txtMeetingEndDateTime.Value = string.Empty;
            txtName.Text = string.Empty;
            txtLocation.Text = string.Empty;
            chkPopupReminder.Checked = true;
            chkEmailReminder.Checked = true;
            ddlMeetingStatus.SelectedIndex = 0;
            ddlMeetingType.SelectedIndex = 0;            
            btnSave.Text = "Save";
        }
        private bool MeetingControlValidation()
        {
            if (txtName.Text.Trim() == string.Empty)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Enter Meeting Name";
                Message.Show = true;
                return false;
            }
            else if (txtMeetingStartDateTime.Value.Trim() == string.Empty)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Enter Meeting Start Date Time";
                Message.Show = true;
                return false;
            }
            else if (txtMeetingEndDateTime.Value.Trim() == string.Empty)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Enter Meeting End Date Time";
                Message.Show = true;
                return false;
            }
            else if (ddlMeetingStatus.SelectedIndex == 0)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Select Meeting Status";
                Message.Show = true;
                return false;
            }
            else if (ddlMeetingType.SelectedIndex == 0)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Select Meeting Type";
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
        protected void gvMeetings_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ed")
            {
                MeetingId = Convert.ToInt32(e.CommandArgument.ToString());
                GetMeetingById();
                Message.Show = false;
                btnSave.Text = "Update";
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
        private void GetMeetingById()
        {
            Business.Sales.Meetings Obj = new Business.Sales.Meetings();
            Entity.Sales.Meetings meetings = Obj.GetMeetingById(MeetingId);
            if (meetings.Id != 0)
            {
                ddlMeetingType.SelectedValue = meetings.MeetingTypeId.ToString();
                ddlMeetingStatus.SelectedValue = meetings.MeetingStatusId.ToString();               
                txtDescription.Text = meetings.Description;
                txtMeetingStartDateTime.Value = meetings.StartDateTime.ToString("dd MMM yyyy HH:mm tt");
                txtMeetingEndDateTime.Value = meetings.EndDateTime.ToString("dd MMM yyyy HH:mm tt");
                txtName.Text = meetings.Name;
                txtLocation.Text = meetings.Location;
                chkEmailReminder.Checked = meetings.EmailReminder;
                chkPopupReminder.Checked = meetings.PopupReminder;
            }
        }
        private void Save()
        {
            if (MeetingControlValidation())
            {
                Business.Sales.Meetings Obj = new Business.Sales.Meetings();
                Entity.Sales.Meetings Model = new Entity.Sales.Meetings
                {
                    Id = MeetingId,
                    MeetingStatusId = Convert.ToInt32(ddlMeetingStatus.SelectedValue),
                    MeetingTypeId = Convert.ToInt32(ddlMeetingType.SelectedValue),
                    CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name),
                    Description = txtDescription.Text,
                    Name = txtName.Text,
                    Location = txtLocation.Text,
                    StartDateTime = Convert.ToDateTime(txtMeetingStartDateTime.Value),
                    EndDateTime = Convert.ToDateTime(txtMeetingEndDateTime.Value),
                    EmailReminder = chkEmailReminder.Checked,
                    PopupReminder = chkPopupReminder.Checked,
                    IsActive = true
                };
                int rows = Obj.SaveMeetings(Model);
                if (rows > 0)
                {
                    ClearControls();
                    LoadMeetingList();
                    MeetingId = 0;
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