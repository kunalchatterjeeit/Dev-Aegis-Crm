using Business.Common;
using Entity.Common;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Sales
{
    public partial class Account : System.Web.UI.Page
    {
        ILog logger = log4net.LogManager.GetLogger("ErrorLog");
        public int AccountId
        {
            get { return Convert.ToInt32(ViewState["Id"]); }
            set { ViewState["Id"] = value; }
        }
        public int SocialMediaMappingId
        {
            get { return Convert.ToInt32(ViewState["SocialMediaMappingId"]); }
            set { ViewState["SocialMediaMappingId"] = value; }
        }
        public int ActivityLinkId
        {
            get { return Convert.ToInt32(ViewState["ActivityLinkId"]); }
            set { ViewState["ActivityLinkId"] = value; }
        }
        private void LoadCallList()
        {
            Business.Sales.Calls Obj = new Business.Sales.Calls();
            Entity.Sales.GetCallsParam Param = new Entity.Sales.GetCallsParam
            {
                StartDateTime = DateTime.MinValue,
                EndDateTime = DateTime.MinValue,
                LinkId = AccountId,
                LinkType = SalesLinkType.Account
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
                LinkId = AccountId,
                LinkType = SalesLinkType.Account
            };
            gvMeetingss.DataSource = Obj.GetAllMeetings(Param);
            gvMeetingss.DataBind();
        }
        private void LoadNotesList()
        {
            Business.Sales.Notes Obj = new Business.Sales.Notes();
            Entity.Sales.GetNotesParam Param = new Entity.Sales.GetNotesParam
            {
                LinkId = AccountId,
                LinkType = SalesLinkType.Account
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
                LinkId = AccountId,
                LinkType = SalesLinkType.Account
            };
            gvTasks.DataSource = Obj.GetAllTasks(Param);
            gvTasks.DataBind();
        }
        private void PopulateItems()
        {
            if (AccountId == 0)
            {
                btnCreateNewCall.Enabled = false;
                btnCreateNewMeeting.Enabled = false;
                btnCreateNewNote.Enabled = false;
                btnCreateNewTask.Enabled = false;
                btnCreateEmployee.Enabled = false;
            }
            else
            {
                btnCreateNewCall.Enabled = true;
                btnCreateNewMeeting.Enabled = true;
                btnCreateNewNote.Enabled = true;
                btnCreateNewTask.Enabled = true;
                btnCreateEmployee.Enabled = true;

                LoadCallList();
                LoadMeetingList();
                LoadNotesList();
                LoadTaskList();
                LoadAssginments();
                SetCreateLinks();
            }
        }

        private void LoadAssginments()
        {
            Business.Sales.Assignment Obj = new Business.Sales.Assignment();
            Entity.Sales.GetAssignmentParam Param = new Entity.Sales.GetAssignmentParam { ActivityId = AccountId, ActivityTypeId = Convert.ToInt32(ActityType.Account) };
            gvAssignedEmployee.DataSource = Obj.GetAllAssignments(Param);
            gvAssignedEmployee.DataBind();
        }

        private void LoadLinkType()
        {
            ddlLinkType.Items.Insert(0, new ListItem() { Text = "Customer", Value = ActityType.Customer.ToString(), Selected = true, Enabled = true });
        }
        private long GetCustomerIdByName(string name)
        {
            long retValue = 0;
            try
            {
                retValue = Business.Customer.Customer.GetCustomerIdByNameFromCache(name);
            }
            catch (Exception ex)
            {
                ex.WriteException();
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
            return retValue;
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Business.Common.Context.ReferralUrl = HttpContext.Current.Request.UrlReferrer.AbsoluteUri;
                    LoadCustomerType();
                    LoadLeadSource();
                    LoadAccountList();
                    LoadLinkType();
                    Message.Show = false;
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
        private void LoadCustomerType()
        {
            Business.Sales.Account Obj = new Business.Sales.Account();

            ddlCustomerType.DataSource = Obj.GetCustomerType();
            ddlCustomerType.DataTextField = "Name";
            ddlCustomerType.DataValueField = "Id";
            ddlCustomerType.DataBind();
            ddlCustomerType.InsertSelect();
        }
        private void LoadLeadSource()
        {
            Business.Sales.Account Obj = new Business.Sales.Account();

            ddlLeadSource.DataSource = Obj.GetLeadSource();
            ddlLeadSource.DataTextField = "Name";
            ddlLeadSource.DataValueField = "Id";
            ddlLeadSource.DataBind();
            ddlLeadSource.InsertSelect();
        }
        private void LoadAccountList()
        {
            Business.Sales.Account Obj = new Business.Sales.Account();
            Entity.Sales.GetAccountsParam Param = new Entity.Sales.GetAccountsParam { Name = null, OfficePhone = null, SourceActivityTypeId = Convert.ToInt32(ActityType.Customer), ChildActivityTypeId = Convert.ToInt32(ActityType.Account) };
            gvAccounts.DataSource = Obj.GetAllAccounts(Param);
            gvAccounts.DataBind();
        }
        private void LoadSocialMediaList()
        {
            Business.Sales.SocialMedia Obj = new Business.Sales.SocialMedia();
            Entity.Sales.GetSocialMediaParam Param = new Entity.Sales.GetSocialMediaParam { LinkId = AccountId, LinkTypeId = Convert.ToInt32(ActityType.Account) };
            gvSocialMedia.DataSource = Obj.GetAllSocialMedia(Param);
            gvSocialMedia.DataBind();
        }
        private void ClearControls()
        {
            AccountId = 0;
            Message.Show = false;
            txtAccountScore.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtAnnualRevenue.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtEmployeeStrength.Text = string.Empty;
            txtOfficePhone.Text = string.Empty;
            txtIndustry.Text = string.Empty;
            txtName.Text = string.Empty;
            txtWebsite.Text = string.Empty;
            txtSourceName.Text = string.Empty;
            ddlCustomerType.SelectedIndex = 0;
            ddlLeadSource.SelectedIndex = 0;
            txtCustomerName.Text = string.Empty;
            btnSave.Text = "Save";
        }
        private bool AccountControlValidation()
        {
            if (txtName.Text.Trim() == string.Empty)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Enter Account Name";
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
            else if (txtEmployeeStrength.Text.Trim() == string.Empty)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Enter Employee Strength";
                Message.Show = true;
                return false;
            }

            return true;
        }
        private void SetCreateLinks()
        {
            btnCreateNewCall.PostBackUrl = string.Concat("Calls.aspx?id=", AccountId, "&itemtype=", SalesLinkType.Account);
            btnCreateNewMeeting.PostBackUrl = string.Concat("Meeting.aspx?id=", AccountId, "&itemtype=", SalesLinkType.Account);
            btnCreateNewNote.PostBackUrl = string.Concat("Notes.aspx?id=", AccountId, "&itemtype=", SalesLinkType.Account);
            btnCreateNewTask.PostBackUrl = string.Concat("Task.aspx?id=", AccountId, "&itemtype=", SalesLinkType.Account);
            btnCreateEmployee.PostBackUrl = string.Concat("~/HR/Employee.aspx?id=", AccountId, "&itemtype=", SalesLinkType.Account);
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
        protected void btnFind_Click(object sender, EventArgs e)
        {
            try
            {
                hdnOpenForm.Value = "true";
                Business.Customer.Customer objCustomer = new Business.Customer.Customer();
                Entity.Customer.Customer customer = new Entity.Customer.Customer();
                customer.CustomerMasterId = GetCustomerIdByName(txtCustomerName.Text);
                DataTable dt = objCustomer.FetchCustomerDetailsById(customer);
                if (dt.Rows.Count > 0)
                {
                    txtName.Text = dt.Rows[0]["CustomerName"].ToString();
                    //ddlCustomerType.SelectedValue = dt.Rows[0]["CustomerType"].ToString();
                    txtOfficePhone.Text = dt.Rows[0]["PhoneNo"].ToString();
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
        protected void gvAccounts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Ed")
                {
                    AccountId = Convert.ToInt32(e.CommandArgument.ToString());
                    GetAccountById();
                    LoadSocialMediaList();
                    Message.Show = false;
                    btnSave.Text = "Update";
                    PopulateItems();
                    hdnOpenForm.Value = "true";
                }
                else if (e.CommandName == "View")
                {
                    AccountId = Convert.ToInt32(e.CommandArgument.ToString());
                    GetAccountById();
                    PopulateItems();
                    LoadSocialMediaList();
                    hdnOpenForm.Value = "true";
                }
                else if (e.CommandName == "Del")
                {
                    Business.Sales.Account Obj = new Business.Sales.Account();
                    int rows = Obj.DeleteAccounts(Convert.ToInt32(e.CommandArgument.ToString()));
                    if (rows > 0)
                    {
                        ClearControls();
                        LoadAccountList();
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
        protected void gvSocialMedia_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Save")
                {
                    SocialMediaMappingId = Convert.ToInt32(e.CommandArgument.ToString());
                    GridViewRow row = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    TextBox txtDescription = (TextBox)row.FindControl("txtDescription");
                    TextBox txtURL = (TextBox)row.FindControl("txtUrl");
                    Business.Sales.SocialMedia Obj = new Business.Sales.SocialMedia();
                    Entity.Sales.SocialMedia Model = new Entity.Sales.SocialMedia
                    {
                        Id = SocialMediaMappingId,
                        Description = txtDescription.Text,
                        LinkId = AccountId,
                        LinkTypeId = Convert.ToInt32(ActityType.Account),
                        URL = txtURL.Text,
                        SocialMediaId = Convert.ToInt32(gvSocialMedia.DataKeys[row.RowIndex].Values[0].ToString())
                    };
                    int rows = Obj.SaveSocialMedia(Model);
                    if (rows > 0)
                    {
                        SocialMediaMappingId = 0;
                        Message.IsSuccess = true;
                        Message.Text = "Saved Successfully";
                    }
                    else
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Unable to link.";
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
        private void GetAccountById()
        {
            Business.Sales.Account Obj = new Business.Sales.Account();
            Entity.Sales.Accounts Account = Obj.GetAccountById(AccountId, Convert.ToInt32(ActityType.Customer), Convert.ToInt32(ActityType.Account));
            if (Account.Id != 0)
            {
                ddlLeadSource.SelectedValue = Account.LeadSourceId == null ? "0" : Account.LeadSourceId.ToString();
                ddlCustomerType.SelectedValue = Account.CustomerTypeId == null ? "0" : Account.CustomerTypeId.ToString();
                txtDescription.Text = Account.Description;
                txtAccountScore.Text = Account.AccountScore.ToString();
                txtAnnualRevenue.Text = Account.AnualRevenue.ToString();
                txtEmployeeStrength.Text = Account.EmployeeStrength.ToString();
                txtIndustry.Text = Account.Industry;
                txtName.Text = Account.Name;
                txtOfficePhone.Text = Account.OfficePhone;
                txtSourceName.Text = Account.SourceName;
                txtWebsite.Text = Account.Website;
                txtCustomerName.Text = Account.CustomerName;
                ActivityLinkId = Account.ActivityLinkId;
            }
        }
        private void Save()
        {
            if (AccountControlValidation())
            {
                Business.Sales.Account Obj = new Business.Sales.Account();
                Entity.Sales.Accounts Model = new Entity.Sales.Accounts
                {
                    Id = AccountId,
                    LeadSourceId = ddlLeadSource.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlLeadSource.SelectedValue),
                    CustomerTypeId = ddlCustomerType.SelectedValue == "0" ? (int?)null : Convert.ToInt32(ddlCustomerType.SelectedValue),
                    CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name),
                    Description = txtDescription.Text,
                    Name = txtName.Text,
                    Industry = txtIndustry.Text,
                    SourceName = txtSourceName.Text,
                    AccountScore = txtAccountScore.Text == "" ? (decimal?)null : Convert.ToDecimal(txtAccountScore.Text),
                    AnualRevenue = txtAnnualRevenue.Text == "" ? (decimal?)null : Convert.ToDecimal(txtAnnualRevenue.Text),
                    EmployeeStrength = Convert.ToInt32(txtEmployeeStrength.Text),
                    OfficePhone = txtOfficePhone.Text,
                    Website = txtWebsite.Text,
                    IsActive = true,
                    ActivityLinkId = ActivityLinkId,
                    ChildActivityTypeId = Convert.ToInt32(ActityType.Account),
                    SourceActivityTypeId = Convert.ToInt32(ActityType.Customer),
                    SourceActivityId = txtCustomerName.Text == "" ? (int?)null : Convert.ToInt32(GetCustomerIdByName(txtCustomerName.Text))
                };
                int rows = Obj.SaveAccounts(Model);
                if (rows > 0)
                {
                    ClearControls();
                    LoadAccountList();
                    AccountId = 0;
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
            try
            {
                if (e.CommandName == "Ed")
                {
                    Response.Redirect(string.Concat("Calls.aspx?id=", AccountId, "&itemtype=", SalesLinkType.Account, "&callid=", e.CommandArgument.ToString()));
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
                logger.Error(ex.Message);
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }

        protected void gvMeetingss_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Ed")
                {
                    Response.Redirect(string.Concat("Meeting.aspx?id=", AccountId, "&itemtype=", SalesLinkType.Account, "&meetingid=", e.CommandArgument.ToString()));
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

        protected void gvNotes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Ed")
                {
                    Response.Redirect(string.Concat("Notes.aspx?id=", AccountId, "&itemtype=", SalesLinkType.Account, "&noteid=", e.CommandArgument.ToString()));
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
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }

        protected void gvTasks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Ed")
                {
                    Response.Redirect(string.Concat("Task.aspx?id=", AccountId, "&itemtype=", SalesLinkType.Account, "&taskid=", e.CommandArgument.ToString()));
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
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }

        protected void chkAssigned_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Entity.Sales.AssignmentAllocation Model = new Entity.Sales.AssignmentAllocation();
                Business.Sales.Assignment Obj = new Business.Sales.Assignment();
                CheckBox checkBox = (CheckBox)sender;
                GridViewRow gridViewRow = (GridViewRow)checkBox.NamingContainer;
                if (checkBox.Checked)
                {
                    Model.ActivityId = AccountId;
                    Model.ActivityTypeId = Convert.ToInt32(ActityType.Account);
                    Model.IsActive = true;
                    Model.EmployeeId = Convert.ToInt32(gvAssignedEmployee.DataKeys[gridViewRow.RowIndex].Values[0].ToString());
                    Model.IsLead = null;
                    Model.AssignedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    Model.RevokedBy = null;
                }
                else
                {
                    Model.ActivityId = AccountId;
                    Model.ActivityTypeId = Convert.ToInt32(ActityType.Account);
                    Model.IsActive = false;
                    Model.EmployeeId = Convert.ToInt32(gvAssignedEmployee.DataKeys[gridViewRow.RowIndex].Values[0].ToString());
                    Model.IsLead = null;
                    Model.AssignedBy = null;
                    Model.RevokedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                }
                int a = Obj.AssignmentAllocation(Model);
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
        protected void gvAssignedEmployee_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    CheckBox chkAssigned = (CheckBox)e.Row.FindControl("chkAssigned");
                    RadioButton rbtnIsLead = (RadioButton)e.Row.FindControl("rbtnIsLead");
                    chkAssigned.Checked = ((List<Entity.Sales.GetAssignment>)gvAssignedEmployee.DataSource)[e.Row.RowIndex].IsActive.GetValueOrDefault();
                    rbtnIsLead.Checked = ((List<Entity.Sales.GetAssignment>)gvAssignedEmployee.DataSource)[e.Row.RowIndex].IsLead.GetValueOrDefault();
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
        protected void rbtnIsLead_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //Clear the existing selected row 
                foreach (GridViewRow oldrow in gvAssignedEmployee.Rows)
                {
                    ((RadioButton)oldrow.FindControl("rbtnIsLead")).Checked = false;
                }

                //Set the new selected row
                RadioButton rb = (RadioButton)sender;
                GridViewRow row = (GridViewRow)rb.NamingContainer;
                if (((CheckBox)row.FindControl("chkAssigned")).Checked)
                {
                    ((RadioButton)row.FindControl("rbtnIsLead")).Checked = true;
                    Entity.Sales.AssignmentAllocation Model = new Entity.Sales.AssignmentAllocation();
                    Business.Sales.Assignment Obj = new Business.Sales.Assignment();
                    Model.ActivityId = AccountId;
                    Model.ActivityTypeId = Convert.ToInt32(ActityType.Account);
                    Model.IsActive = false;
                    Model.EmployeeId = Convert.ToInt32(gvAssignedEmployee.DataKeys[row.RowIndex].Values[0].ToString());
                    Model.IsLead = true;
                    Model.AssignedBy = null;
                    Model.RevokedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    int a = Obj.AssignmentAllocation(Model);
                }
                else
                {
                    ((RadioButton)row.FindControl("rbtnIsLead")).Checked = false;
                    Message.IsSuccess = false;
                    Message.Text = "Please select an employee to assign";
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
    }
}