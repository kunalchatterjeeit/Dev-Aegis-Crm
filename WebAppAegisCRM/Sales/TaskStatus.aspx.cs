using Business.Common;
using log4net;
using System;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Sales
{
    public partial class TaskStatus : System.Web.UI.Page
    {
        ILog logger = log4net.LogManager.GetLogger("ErrorLog");
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    LoadTaskStatusList();
                    Message.Show = false;
                    if (TaskStatusId > 0)
                    {
                        GetTaskStatusById();
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
        public int TaskStatusId
        {
            get { return Convert.ToInt32(ViewState["Id"]); }
            set { ViewState["Id"] = value; }
        }
        private void LoadTaskStatusList()
        {
            Business.Sales.TaskStatus Obj = new Business.Sales.TaskStatus();
            gvTaskStatus.DataSource = Obj.GetAllTaskStatus();
            gvTaskStatus.DataBind();
        }
        private void ClearControls()
        {
            TaskStatusId = 0;
            Message.Show = false;
            txtDescription.Text = string.Empty;
            txtName.Text = string.Empty;
            btnSave.Text = "Save";
        }
        private bool CallStatusControlValidation()
        {
            if (txtName.Text.Trim() == string.Empty)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Enter Task Status Name";
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
        protected void gvTaskStatus_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Ed")
                {
                    TaskStatusId = Convert.ToInt32(e.CommandArgument.ToString());
                    GetTaskStatusById();
                    Message.Show = false;
                    btnSave.Text = "Update";
                }
                else if (e.CommandName == "Del")
                {
                    Business.Sales.TaskStatus Obj = new Business.Sales.TaskStatus();
                    int rows = Obj.DeleteTaskStatus(Convert.ToInt32(e.CommandArgument.ToString()));
                    if (rows > 0)
                    {
                        ClearControls();
                        LoadTaskStatusList();
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
        private void GetTaskStatusById()
        {
            Business.Sales.TaskStatus Obj = new Business.Sales.TaskStatus();
            Entity.Sales.TaskStatus Taskstatus = Obj.GetTaskStatusById(TaskStatusId);
            if (Taskstatus.Id != 0)
            {
                txtDescription.Text = Taskstatus.Description;
                txtName.Text = Taskstatus.Name;
            }
        }
        private void Save()
        {
            if (CallStatusControlValidation())
            {
                Business.Sales.TaskStatus Obj = new Business.Sales.TaskStatus();
                Entity.Sales.TaskStatus Model = new Entity.Sales.TaskStatus
                {
                    Id = TaskStatusId,
                    Name = txtName.Text,
                    Description = txtDescription.Text,

                };
                int rows = Obj.SaveTaskStatus(Model);
                if (rows > 0)
                {
                    ClearControls();
                    LoadTaskStatusList();
                    TaskStatusId = 0;
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