using Business.Common;
using Entity.Common;
using log4net;
using System;
using System.Web;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Sales
{
    public partial class Meeting : System.Web.UI.Page
    {
        ILog logger = log4net.LogManager.GetLogger("ErrorLog");
        private void SetQueryStringValue()
        {
            try
            {
                if (Request.QueryString["id"] != null && Request.QueryString["itemtype"] != null)
                {
                    hdnItemId.Value = Request.QueryString["id"].ToString();
                    hdnItemType.Value = Request.QueryString["itemtype"].ToString();
                }
                if (Request.QueryString["meetingid"] != null)
                {
                    MeetingId = Convert.ToInt32(Request.QueryString["meetingid"].ToString());
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
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
                    LoadMeetingsDropdowns();
                    LoadMeetingList();
                    Message.Show = false;
                    if (MeetingId > 0)
                    {
                        GetMeetingById();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
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
            Entity.Sales.GetMeetingsParam Param = new Entity.Sales.GetMeetingsParam
            {
                StartDateTime = DateTime.MinValue,
                EndDateTime = DateTime.MinValue,
                LinkId = (!string.IsNullOrEmpty(hdnItemType.Value)) ? Convert.ToInt32(hdnItemId.Value) : 0,
                LinkType = (!string.IsNullOrEmpty(hdnItemType.Value)) ? (SalesLinkType)Enum.Parse(typeof(SalesLinkType), hdnItemType.Value) : SalesLinkType.None
            };
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
            try
            {
                ClearControls();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
        protected void gvMeetings_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
                Message.IsSuccess = false;
                Message.Text = ex.Message;
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
                MeetingId = Obj.SaveMeetings(Model);
                if (MeetingId > 0)
                {
                    SaveMeetingLink();
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

        private void SaveMeetingLink()
        {
            Business.Sales.Meetings Obj = new Business.Sales.Meetings();
            Entity.Sales.Meetings Model = new Entity.Sales.Meetings
            {
                Id = MeetingId,
                LinkId = Convert.ToInt32(hdnItemId.Value),
                LinkType = (SalesLinkType)Enum.Parse(typeof(SalesLinkType), hdnItemType.Value)
            };
            Obj.SaveMeetingLinks(Model);
        }
    }
}