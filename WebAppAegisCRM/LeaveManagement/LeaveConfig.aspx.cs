using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Business.Common;

namespace WebAppAegisCRM.LeaveManagement
{
    public partial class LeaveConfig : System.Web.UI.Page
    {
        public int LeaveConfigId
        {
            get { return Convert.ToInt32(ViewState["LeaveConfigId"]); }
            set { ViewState["LeaveConfigId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadLeaveType();
                LeaveConfig_GetAll();
                Message.Show = false;
            }
        }

        private void LoadLeaveType()
        {
            Business.LeaveManagement.LeaveType objLeaveType = new Business.LeaveManagement.LeaveType();
            DataTable dtLeaveMaster = objLeaveType.LeaveTypeGetAll(new Entity.LeaveManagement.LeaveType());
            if (dtLeaveMaster != null)
            {
                ddlLeaveType.DataSource = dtLeaveMaster;
                ddlLeaveType.DataTextField = "LeaveTypeName";
                ddlLeaveType.DataValueField = "LeaveTypeId";
                ddlLeaveType.DataBind();
            }
            ddlLeaveType.InsertSelect();
        }

        private void LeaveConfig_GetAll()
        {
            Business.LeaveManagement.LeaveConfiguration ObjbelLeaveConfig = new Business.LeaveManagement.LeaveConfiguration();
            Entity.LeaveManagement.LeaveConfiguration lmLeaveConfig = new Entity.LeaveManagement.LeaveConfiguration();

            DataTable dt = ObjbelLeaveConfig.LeaveConfigurations_GetAll(lmLeaveConfig);

            dgvLeaveConfiguration.DataSource = dt;
            dgvLeaveConfiguration.DataBind();
        }

        private void Clear()
        {
            ddlLeaveType.SelectedIndex = 0;
            txtLeaveFrequency.Text = "";
            txtLeaveAccureDate.Text = "";
            txtCarryForwardCount.Text = "";
            Message.Show = false;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Business.LeaveManagement.LeaveConfiguration objLeaveConfig = new Business.LeaveManagement.LeaveConfiguration();
            Entity.LeaveManagement.LeaveConfiguration leaveMaster = new Entity.LeaveManagement.LeaveConfiguration();
            leaveMaster.LeaveConfigId = LeaveConfigId;
            leaveMaster.LeaveTypeId = Convert.ToInt16(ddlLeaveType.SelectedValue);
            leaveMaster.LeaveFrequency = txtLeaveFrequency.Text.Trim();
            leaveMaster.leaveAccureDate = Convert.ToDateTime(txtLeaveAccureDate.Text.Trim());
            leaveMaster.CarryForwardCount = Convert.ToInt16(txtCarryForwardCount.Text.Trim());
            int response = objLeaveConfig.LeaveConfigurations_Save(leaveMaster);
            if (response > 0)
            {
                Clear();
                LeaveConfig_GetAll();
                Message.IsSuccess = true;
                Message.Text = "Saved Successfully";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Role Name Exists";
            }
            Message.Show = true;
        }
    }
}