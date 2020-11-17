using Business.Common;
using Entity.Common;
using System;
using System.Web;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Sales
{
    public partial class Calls : System.Web.UI.Page
    {
        private void SetQueryStringValue()
        {
            if (Request.QueryString["id"] != null && Request.QueryString["itemtype"] != null)
            {
                hdnItemId.Value = Request.QueryString["id"].ToString();
                hdnItemType.Value = Request.QueryString["itemtype"].ToString();
            }
            if (Request.QueryString["callid"] != null)
            {
                CallId = Convert.ToInt32(Request.QueryString["callid"].ToString());
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
                    LoadCallsDropdowns();
                    LoadCallList();
                    Message.Show = false;
                    if (CallId > 0)
                    {
                        GetCallById();
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
        public int CallId
        {
            get { return Convert.ToInt32(ViewState["Id"]); }
            set { ViewState["Id"] = value; }
        }
        private void LoadCallsDropdowns()
        {
            LoadCallDirection();
            LoadCallRelatedTo();
            LoadCallRepeatType();
            LoadCallStatus();
        }

        private void LoadCallDirection()
        {
            Business.Sales.Calls Obj = new Business.Sales.Calls();
            ddlCallDirection.DataSource = Obj.GetCallDirection();
            ddlCallDirection.DataTextField = "Name";
            ddlCallDirection.DataValueField = "Id";
            ddlCallDirection.DataBind();
            ddlCallDirection.InsertSelect();
        }

        private void LoadCallStatus()
        {
            Business.Sales.Calls Obj = new Business.Sales.Calls();
            ddlCallStatus.DataSource = Obj.GetCallStatus();
            ddlCallStatus.DataTextField = "Name";
            ddlCallStatus.DataValueField = "Id";
            ddlCallStatus.DataBind();
            ddlCallStatus.InsertSelect();
        }

        private void LoadCallRelatedTo()
        {
            Business.Sales.Calls Obj = new Business.Sales.Calls();
            ddlCallRelatedTo.DataSource = Obj.GetCallRelated();
            ddlCallRelatedTo.DataTextField = "Name";
            ddlCallRelatedTo.DataValueField = "Id";
            ddlCallRelatedTo.DataBind();
            ddlCallRelatedTo.InsertSelect();
        }

        private void LoadCallRepeatType()
        {
            Business.Sales.Calls Obj = new Business.Sales.Calls();
            ddlCallRepeatType.DataSource = Obj.GetCallRepeatType();
            ddlCallRepeatType.DataTextField = "Name";
            ddlCallRepeatType.DataValueField = "Id";
            ddlCallRepeatType.DataBind();
            ddlCallRepeatType.InsertSelect();
        }

        private void LoadCallList()
        {
            Business.Sales.Calls Obj = new Business.Sales.Calls();
            Entity.Sales.GetCallsParam Param = new Entity.Sales.GetCallsParam
            {
                StartDateTime = DateTime.MinValue,
                EndDateTime = DateTime.MinValue,
                LinkId = (!string.IsNullOrEmpty(hdnItemType.Value)) ? Convert.ToInt32(hdnItemId.Value) : 0,
                LinkType = (!string.IsNullOrEmpty(hdnItemType.Value)) ? (SalesLinkType)Enum.Parse(typeof(SalesLinkType), hdnItemType.Value) : SalesLinkType.None
            };

            if (HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
                Param.AssignEngineer = 0;
            else
                Param.AssignEngineer = int.Parse(HttpContext.Current.User.Identity.Name);

            gvCalls.DataSource = Obj.GetAllCalls(Param);
            gvCalls.DataBind();
        }
        private void ClearControls()
        {
            CallId = 0;
            Message.Show = false;
            txtCallStartDateTime.Value = string.Empty;
            txtDescription.Text = string.Empty;
            txtCallEndDateTime.Value = string.Empty;
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
            else if (txtCallStartDateTime.Value.Trim() == string.Empty)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Enter Call Start Date Time";
                Message.Show = true;
                return false;
            }
            else if (txtCallEndDateTime.Value.Trim() == string.Empty)
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
            try
            {
                ClearControls();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
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
                
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
        protected void gvCalls_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                ex.WriteException();
                
                Message.IsSuccess = false;
                Message.Text = ex.Message;
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
                txtCallStartDateTime.Value = calls.StartDateTime.ToString("dd MMM yyyy HH:mm tt");
                txtCallEndDateTime.Value = calls.EndDateTime.ToString("dd MMM yyyy HH:mm tt");
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
                    StartDateTime = Convert.ToDateTime(txtCallStartDateTime.Value),
                    EndDateTime = Convert.ToDateTime(txtCallEndDateTime.Value),
                    EmailReminder = chkEmailReminder.Checked,
                    PopupReminder = chkPopupReminder.Checked,
                    IsActive = true
                };
                CallId = Obj.SaveCalls(Model);
                if (CallId > 0)
                {
                    SaveCallLink();
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
        private void SaveCallLink()
        {
            Business.Sales.Calls Obj = new Business.Sales.Calls();
            Entity.Sales.Calls Model = new Entity.Sales.Calls
            {
                Id = CallId,
                LinkId = Convert.ToInt32(hdnItemId.Value),
                LinkType = (SalesLinkType)Enum.Parse(typeof(SalesLinkType), hdnItemType.Value)
            };
            Obj.SaveCallLinks(Model);
        }
    }
}