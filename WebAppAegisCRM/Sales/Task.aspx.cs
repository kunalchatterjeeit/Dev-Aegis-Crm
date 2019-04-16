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
    public partial class Task : System.Web.UI.Page
    {
        private void SetQueryStringValue()
        {
            if (Request.QueryString["id"] != null && Request.QueryString["itemtype"] != null)
            {
                hdnItemId.Value = Request.QueryString["id"].ToString();
                hdnItemType.Value = Request.QueryString["itemtype"].ToString();
            }
            if (Request.QueryString["taskid"] != null)
            {
                TaskId = Convert.ToInt32(Request.QueryString["taskid"].ToString());
            }
        }

        protected void Page_Load(object sender, EventArgs e)
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
                LoadTasksDropdowns();
                LoadTaskList();
                Message.Show = false;
                if (TaskId > 0)
                {
                    GetTaskById();
                }
            }
        }
        public int TaskId
        {
            get { return Convert.ToInt32(ViewState["Id"]); }
            set { ViewState["Id"] = value; }
        }
        private void LoadTasksDropdowns()
        {
            Business.Sales.Tasks Obj = new Business.Sales.Tasks();

            ddlTaskPriority.DataSource = Obj.GetTaskPriority();
            ddlTaskPriority.DataTextField = "Name";
            ddlTaskPriority.DataValueField = "Id";
            ddlTaskPriority.DataBind();
            ddlTaskPriority.InsertSelect();

            ddlTaskRelatedTo.DataSource = Obj.GetTaskRelatedTo();
            ddlTaskRelatedTo.DataTextField = "Name";
            ddlTaskRelatedTo.DataValueField = "Id";
            ddlTaskRelatedTo.DataBind();
            ddlTaskRelatedTo.InsertSelect();

            ddlTaskStatus.DataSource = Obj.GetTaskStatus();
            ddlTaskStatus.DataTextField = "Name";
            ddlTaskStatus.DataValueField = "Id";
            ddlTaskStatus.DataBind();
            ddlTaskStatus.InsertSelect();
        }
        private void LoadTaskList()
        {
            Business.Sales.Tasks Obj = new Business.Sales.Tasks();
            Entity.Sales.GetTasksParam Param = new Entity.Sales.GetTasksParam {
                StartDateTime = DateTime.MinValue,
                EndDateTime = DateTime.MinValue,
                LinkId = (!string.IsNullOrEmpty(hdnItemType.Value)) ? Convert.ToInt32(hdnItemId.Value) : 0,
                LinkType = (!string.IsNullOrEmpty(hdnItemType.Value)) ? (SalesLinkType)Enum.Parse(typeof(SalesLinkType), hdnItemType.Value) : SalesLinkType.None
            };
            gvTasks.DataSource = Obj.GetAllTasks(Param);
            gvTasks.DataBind();
        }
        private void ClearControls()
        {
            TaskId = 0;
            Message.Show = false;
            txtDescription.Text = string.Empty;
            txtSubject.Text = string.Empty;
            txtTaskEndDateTime.Value = string.Empty;
            txtTaskStartDateTime.Value = string.Empty;
            ddlTaskStatus.SelectedIndex = 0;
            ddlTaskRelatedTo.SelectedIndex = 0;
            ddlTaskPriority.SelectedIndex = 0;
            btnSave.Text = "Save";
        }
        private bool TaskControlValidation()
        {
            if (txtSubject.Text.Trim() == string.Empty)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Enter Task Subject";
                Message.Show = true;
                return false;
            }
            else if (txtTaskStartDateTime.Value.Trim() == string.Empty)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Enter Task Start Date Time";
                Message.Show = true;
                return false;
            }
            else if (txtTaskEndDateTime.Value.Trim() == string.Empty)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Enter Task End Date Time";
                Message.Show = true;
                return false;
            }
            else if (ddlTaskPriority.SelectedIndex == 0)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Select Task Priority";
                Message.Show = true;
                return false;
            }
            else if (ddlTaskRelatedTo.SelectedIndex == 0)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Select Task Related To";
                Message.Show = true;
                return false;
            }
            else if (ddlTaskStatus.SelectedIndex == 0)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Select Task Status";
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
        protected void gvTasks_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ed")
            {
                TaskId = Convert.ToInt32(e.CommandArgument.ToString());
                GetTaskById();
                Message.Show = false;
                btnSave.Text = "Update";
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
        private void GetTaskById()
        {
            Business.Sales.Tasks Obj = new Business.Sales.Tasks();
            Entity.Sales.Tasks Tasks = Obj.GetTaskById(TaskId);
            if (Tasks.Id != 0)
            {
                ddlTaskStatus.SelectedValue = Tasks.TasksStatusId.ToString();
                ddlTaskRelatedTo.SelectedValue = Tasks.TasksRelatedTo.ToString();
                ddlTaskPriority.SelectedValue = Tasks.TasksPriorityId.ToString();
                txtDescription.Text = Tasks.Description;
                txtTaskStartDateTime.Value = Tasks.StartDateTime.ToString("dd MMM yyyy HH:mm tt");
                txtTaskEndDateTime.Value = Tasks.EndDateTime.ToString("dd MMM yyyy HH:mm tt");
                txtSubject.Text = Tasks.Subject;
            }
        }
        private void Save()
        {
            if (TaskControlValidation())
            {
                Business.Sales.Tasks Obj = new Business.Sales.Tasks();
                Entity.Sales.Tasks Model = new Entity.Sales.Tasks
                {
                    Id = TaskId,
                    TasksPriorityId = Convert.ToInt32(ddlTaskPriority.SelectedValue),
                    TasksRelatedTo = Convert.ToInt32(ddlTaskRelatedTo.SelectedValue),
                    TasksStatusId = Convert.ToInt32(ddlTaskStatus.SelectedValue),
                    CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name),
                    Description = txtDescription.Text,
                    Subject = txtSubject.Text,
                    StartDateTime = Convert.ToDateTime(txtTaskStartDateTime.Value),
                    EndDateTime = Convert.ToDateTime(txtTaskEndDateTime.Value),
                    IsActive = true
                };
                int rows = Obj.SaveTasks(Model);
                if (rows > 0)
                {
                    SaveTaskLinks();
                    ClearControls();
                    LoadTaskList();
                    TaskId = 0;
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

        private void SaveTaskLinks()
        {
            Business.Sales.Tasks Obj = new Business.Sales.Tasks();
            Entity.Sales.Tasks Model = new Entity.Sales.Tasks
            {
                Id = TaskId,
                LinkId = Convert.ToInt32(hdnItemId.Value),
                LinkType = (SalesLinkType)Enum.Parse(typeof(SalesLinkType), hdnItemType.Value)
            };
            Obj.SaveTaskLinks(Model);
        }
    }
}