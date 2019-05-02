using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Sales
{
    public partial class CallStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCallStatusList();
                Message.Show = false;
                if (CallStatusId > 0)
                {
                    GetCallStatusById();
                }
            }
        }
        public int CallStatusId
        {
            get { return Convert.ToInt32(ViewState["Id"]); }
            set { ViewState["Id"] = value; }
        }
        private void LoadCallStatusList()
        {
            Business.Sales.CallStatus Obj = new Business.Sales.CallStatus();
            gvCallStatus.DataSource = Obj.GetAllCallStatus();
            gvCallStatus.DataBind();
        }
        private void ClearControls()
        {
            CallStatusId = 0;
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
                Message.Text = "Please Enter Call Status Name";
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
        protected void gvCallStatus_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ed")
            {
                CallStatusId = Convert.ToInt32(e.CommandArgument.ToString());
                GetCallStatusById();
                Message.Show = false;
                btnSave.Text = "Update";
            }
            else if (e.CommandName == "Del")
            {
                Business.Sales.CallStatus Obj = new Business.Sales.CallStatus();
                int rows = Obj.DeleteCallStatus(Convert.ToInt32(e.CommandArgument.ToString()));
                if (rows > 0)
                {
                    ClearControls();
                    LoadCallStatusList();
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
        private void GetCallStatusById()
        {
            Business.Sales.CallStatus Obj = new Business.Sales.CallStatus();
            Entity.Sales.CallStatus callstatus = Obj.GetCallStatusById(CallStatusId);
            if (callstatus.Id != 0)
            {                
                txtDescription.Text = callstatus.Description;
                txtName.Text = callstatus.Name;
            }
        }
        private void Save()
        {
            if (CallStatusControlValidation())
            {
                Business.Sales.CallStatus Obj = new Business.Sales.CallStatus();
                Entity.Sales.CallStatus Model = new Entity.Sales.CallStatus
                {
                    Id = CallStatusId,
                    Name = txtName.Text,
                    Description = txtDescription.Text,
                    
                };
                int rows = Obj.SaveCallStatus(Model);
                if (rows > 0)
                {                    
                    ClearControls();
                    LoadCallStatusList();
                    CallStatusId = 0;
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