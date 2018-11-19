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
                Clear();
                
            }
        }
    
        private void LoadLeaveTypeId()
        {
           
            Business.LeaveManagement.LeaveConfiguration objLeaveMaster = new Business.LeaveManagement.LeaveConfiguration();
            DataTable dtLeaveMaster = objLeaveMaster.LeaveConfigurations_GetAll(new Entity.LeaveManagement.LeaveConfiguration());
            ddlLeaveTypeId.DataSource = dtLeaveMaster;
            ddlLeaveTypeId.DataTextField = "LeaveTypeId";
            ddlLeaveTypeId.DataValueField = "LeaveTypeName";
            ddlLeaveTypeId.DataBind();
            ddlLeaveTypeId.InsertSelect();


        }
        private void Clear()
        {
            ddlLeaveTypeId.Text = "";
            txtLeaveFrequency.Text = "";
            txtLeaveAccureDate.Text = "";
            txtCarryForwardCount.Text = "";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Business.LeaveManagement.LeaveConfiguration ObjLeaveConfig = new Business.LeaveManagement.LeaveConfiguration();
            Entity.LeaveManagement.LeaveConfiguration LeaveMaster = new Entity.LeaveManagement.LeaveConfiguration();
            LeaveMaster.LeaveConfigId = LeaveConfigId;
            LeaveMaster.LeaveTypeId = Convert.ToInt16(ddlLeaveTypeId.SelectedValue);
            LeaveMaster.LeaveFrequency = txtLeaveFrequency.Text.Trim();
            LeaveMaster.leaveAccureDate = Convert.ToDateTime(txtLeaveAccureDate.Text.Trim());
            LeaveMaster.CarryForwardCount = Convert.ToInt16(txtCarryForwardCount.Text.Trim());
            Clear();
            LeaveConfig_GetAll();


        }

        protected void LeaveConfig_GetAll()
        {
            Business.LeaveManagement.LeaveConfiguration ObjbelLeaveConfig = new Business.LeaveManagement.LeaveConfiguration();
            Entity.LeaveManagement.LeaveConfiguration lmLeaveConfig = new Entity.LeaveManagement.LeaveConfiguration();

            DataTable dt = ObjbelLeaveConfig.LeaveConfigurations_GetAll(lmLeaveConfig);
            if (dt.Rows.Count > 0)
                dgvLeaveConfiguration.DataSource = dt;
            else
                dgvLeaveConfiguration.DataSource = null;
                dgvLeaveConfiguration.DataBind();
        }

        private bool Validation()
        {
            bool retValue = true;

            if (retValue && string.IsNullOrEmpty(txtLeaveAccureDate.Text.Trim()))
            {
                retValue = false;
                Message.Text = "Please enter LeaveAccure Date.";
                Message.IsSuccess = false;
                Message.Show = true;
            }

            if (retValue && ddlLeaveTypeId.SelectedIndex == 0)
            {
                retValue = false;
                Message.Text = "Please select Leave Type.";
                Message.IsSuccess = false;
                Message.Show = true;
            }
            if(retValue && string.IsNullOrEmpty(txtCarryForwardCount.Text.Trim()))
            {
                retValue = false;
                Message.Text = "Please Add Carry Forward Count ";
                retValue = true;
            }

           

            return retValue;
        }


    }
}