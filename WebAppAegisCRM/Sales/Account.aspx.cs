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
    public partial class Account : System.Web.UI.Page
    {
        public int AccountId
        {
            get { return Convert.ToInt32(ViewState["Id"]); }
            set { ViewState["Id"] = value; }
        }
        private void LoadCallList()
        {
            Business.Sales.Calls Obj = new Business.Sales.Calls();
            Entity.Sales.GetCallsParam Param = new Entity.Sales.GetCallsParam {
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
            Entity.Sales.GetMeetingsParam Param = new Entity.Sales.GetMeetingsParam {
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
            Entity.Sales.GetNotesParam Param = new Entity.Sales.GetNotesParam {
                LinkId = AccountId,
                LinkType = SalesLinkType.Account
            };
            gvNotes.DataSource = Obj.GetAllNotes(Param);
            gvNotes.DataBind();
        }
        private void LoadTaskList()
        {
            Business.Sales.Tasks Obj = new Business.Sales.Tasks();
            Entity.Sales.GetTasksParam Param = new Entity.Sales.GetTasksParam {
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
            throw new NotImplementedException();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Business.Common.Context.ReferralUrl = HttpContext.Current.Request.UrlReferrer.AbsoluteUri;
                LoadAccountsDropdowns();
                LoadAccountList();
                Message.Show = false;
            }
        }
        private void LoadAccountsDropdowns()
        {
            Business.Sales.Account Obj = new Business.Sales.Account();

            ddlCustomerType.DataSource = Obj.GetCustomerType();
            ddlCustomerType.DataTextField = "Name";
            ddlCustomerType.DataValueField = "Id";
            ddlCustomerType.DataBind();
            ddlCustomerType.InsertSelect();

            ddlLeadSource.DataSource = Obj.GetLeadSource();
            ddlLeadSource.DataTextField = "Name";
            ddlLeadSource.DataValueField = "Id";
            ddlLeadSource.DataBind();
            ddlLeadSource.InsertSelect();
        }
        private void LoadAccountList()
        {
            Business.Sales.Account Obj = new Business.Sales.Account();
            Entity.Sales.GetAccountsParam Param = new Entity.Sales.GetAccountsParam { Name = null, OfficePhone = null };
            //List<Entity.Sales.GetCalls> EntityObj = new List<Entity.Sales.GetCalls>();
            gvAccounts.DataSource = Obj.GetAllAccounts(Param);
            gvAccounts.DataBind();
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
            ClearControls();
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        protected void gvAccounts_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ed")
            {
                AccountId = Convert.ToInt32(e.CommandArgument.ToString());
                GetAccountById();
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
        private void GetAccountById()
        {
            Business.Sales.Account Obj = new Business.Sales.Account();
            Entity.Sales.Accounts Account = Obj.GetAccountById(AccountId);
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
                    IsActive = true
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

        protected void gvMeetingss_RowCommand(object sender, GridViewCommandEventArgs e)
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

        protected void gvNotes_RowCommand(object sender, GridViewCommandEventArgs e)
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

        protected void gvTasks_RowCommand(object sender, GridViewCommandEventArgs e)
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

        protected void gvAssignedEmployee_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void chkAssigned_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void rbtnIsLead_CheckedChanged(object sender, EventArgs e)
        {
            //Clear the existing selected row 
            foreach (GridViewRow oldrow in gvAssignedEmployee.Rows)
            {
                ((RadioButton)oldrow.FindControl("rbtnIsLead")).Checked = false;
            }

            //Set the new selected row
            RadioButton rb = (RadioButton)sender;
            GridViewRow row = (GridViewRow)rb.NamingContainer;
            ((RadioButton)row.FindControl("rbtnIsLead")).Checked = true;
        }
    }
}