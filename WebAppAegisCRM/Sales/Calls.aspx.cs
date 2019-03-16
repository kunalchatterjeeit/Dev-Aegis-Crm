using Business.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Sales
{
    public partial class Calls : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCallsDropdowns();
                LoadCallList();
                Message.Show = false;
            }
        }
        public int CallId
        {
            get { return Convert.ToInt32(ViewState["Id"]); }
            set { ViewState["Id"] = value; }
        }
        private void LoadCallsDropdowns()
        {
            Business.Sales.Calls Obj = new Business.Sales.Calls();

            ddlCallDirection.DataSource = Obj.GetCallDirection();
            ddlCallDirection.DataTextField = "Name";
            ddlCallDirection.DataValueField = "Id";
            ddlCallDirection.DataBind();
            ddlCallDirection.InsertSelect();

            ddlCallRelatedTo.DataSource = Obj.GetCallRelated();
            ddlCallRelatedTo.DataTextField = "Name";
            ddlCallRelatedTo.DataValueField = "Id";
            ddlCallRelatedTo.DataBind();
            ddlCallRelatedTo.InsertSelect();

            ddlCallRepeatType.DataSource = Obj.GetCallRepeatType();
            ddlCallRepeatType.DataTextField = "Name";
            ddlCallRepeatType.DataValueField = "Id";
            ddlCallRepeatType.DataBind();
            ddlCallRepeatType.InsertSelect();

            ddlCallStatus.DataSource = Obj.GetCallStatus();
            ddlCallStatus.DataTextField = "Name";
            ddlCallStatus.DataValueField = "Id";
            ddlCallStatus.DataBind();
            ddlCallStatus.InsertSelect();
        }
        private void LoadCallList()
        {
            Business.Sales.Calls Obj = new Business.Sales.Calls();
            Entity.Sales.GetCallsParam Param = new Entity.Sales.GetCallsParam { StartDateTime = DateTime.Today, EndDateTime = DateTime.Today, CallStatusId = 1, Subject = "" };
            //List<Entity.Sales.GetCalls> EntityObj = new List<Entity.Sales.GetCalls>();
            gvCalls.DataSource = Obj.GetAllCalls(Param);
            gvCalls.DataBind();
        }
        private void ClearControls()
        {
            CallId = 0;
            Message.Show = false;
            txtEndDateTime.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtStartDateTime.Text = string.Empty;
            txtSubject.Text = string.Empty;
            chkPopupReminder.Checked = true;
            chkEmailReminder.Checked = true;
            ddlCallStatus.SelectedIndex = 0;
            ddlCallRepeatType.SelectedIndex = 0;
            ddlCallRelatedTo.SelectedIndex = 0;
            ddlCallDirection.SelectedIndex = 0;
            btnSave.Text = "Save";
        }
        private bool CallControlValidation()
        {
            if (txtSubject.Text.Trim() == string.Empty)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Enter Call Subject";
                Message.Show = true;
                return false;
            }
            else if (txtStartDateTime.Text.Trim() == string.Empty)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Enter Call Start Date Time";
                Message.Show = true;
                return false;
            }
            else if (txtEndDateTime.Text.Trim() == string.Empty)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Enter Call End Date Time";
                Message.Show = true;
                return false;
            }
            else if (ddlCallDirection.SelectedIndex == 0)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Select Call Direction";
                Message.Show = true;
                return false;
            }
            else if (ddlCallRelatedTo.SelectedIndex == 0)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Select Call Related To";
                Message.Show = true;
                return false;
            }
            else if (ddlCallRepeatType.SelectedIndex == 0)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Select Call Repeat Type";
                Message.Show = true;
                return false;
            }
            else if (ddlCallStatus.SelectedIndex == 0)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Select Call Status";
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
        protected void gvCalls_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ed")
            {
                CallId = Convert.ToInt32(e.CommandArgument.ToString());
                GetCallById();
                Message.Show = false;
                btnSave.Text = "Update";
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
        private void GetCallById()
        {
            Business.Sales.Calls Obj = new Business.Sales.Calls();
            Entity.Sales.Calls calls = Obj.GetCallById(CallId);
            if (calls.Id != 0)
            {
                ddlCallDirection.SelectedValue = calls.CallDirectionId.ToString();
                ddlCallRelatedTo.SelectedValue = calls.CallRelatedTo.ToString();
                ddlCallRepeatType.SelectedValue = calls.CallRepeatTypeId.ToString();
                ddlCallStatus.SelectedValue = calls.CallStatusId.ToString();
                txtDescription.Text = calls.Description;
                txtStartDateTime.Text = calls.StartDateTime.ToString();
                txtEndDateTime.Text = calls.EndDateTime.ToString();
                txtSubject.Text = calls.Subject;
                chkEmailReminder.Checked = calls.EmailReminder;
                chkPopupReminder.Checked = calls.PopupReminder;
            }
        }
        private void Save()
        {
            if (CallControlValidation())
            {
                Business.Sales.Calls Obj = new Business.Sales.Calls();
                Entity.Sales.Calls Model = new Entity.Sales.Calls
                {
                    Id = CallId,
                    CallDirectionId = Convert.ToInt32(ddlCallDirection.SelectedValue),
                    CallRelatedTo = Convert.ToInt32(ddlCallRelatedTo.SelectedValue),
                    CallRepeatTypeId = Convert.ToInt32(ddlCallRepeatType.SelectedValue),
                    CallStatusId = Convert.ToInt32(ddlCallStatus.SelectedValue),
                    CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name),
                    Description = txtDescription.Text,
                    Subject = txtSubject.Text,
                    StartDateTime = Convert.ToDateTime(txtStartDateTime.Text),
                    EndDateTime = Convert.ToDateTime(txtEndDateTime.Text),
                    EmailReminder = chkEmailReminder.Checked,
                    PopupReminder = chkPopupReminder.Checked,
                    IsActive = true
                };
                int rows = Obj.SaveCalls(Model);
                if (rows > 0)
                {
                    ClearControls();
                    LoadCallList();
                    CallId = 0;
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