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

namespace WebAppAegisCRM.HR
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
           /* Business.Purchase.Vendor objVendorMaster = new Business.Purchase.Vendor();
            DataTable dtVendor = objVendorMaster.GetAll(new Entity.Purchase.Vendor() { CompanyId = 1 });
            ddlVendor.DataSource = dtVendor;
            ddlVendor.DataTextField = "VendorMasterName";
            ddlVendor.DataValueField = "VendorMasterId";
            ddlVendor.DataBind();
            ddlVendor.InsertSelect();*/
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
            Business.HR.LeaveManagement ObjLeaveConfig = new Business.HR.LeaveManagement();
            Entity.HR.LeaveManagement LeaveMaster = new Entity.HR.LeaveManagement();
            LeaveMaster.LeaveConfigId = LeaveConfigId;
            LeaveMaster.LeaveTypeId = Convert.ToInt16(ddlLeaveTypeId.SelectedValue);
            LeaveMaster.LeaveFrequency = txtLeaveFrequency.Text.Trim();
            LeaveMaster.leaveAccureDate = Convert.ToDateTime(txtLeaveAccureDate.Text.Trim());
            LeaveMaster.CarryForwardCount = Convert.ToInt16(txtCarryForwardCount.Text.Trim());
            Clear();


        }

        protected void LeaveConfig_GetAll()
        {
            Business.HR.LeaveManagement ObjbelLeaveConfig = new Business.HR.LeaveManagement();
            Entity.HR.LeaveManagement lmLeaveMaster = new Entity.HR.LeaveManagement();
            
            DataTable dt = ObjbelLeaveConfig.LeaveConfigurations_GetAll(ObjbelLeaveConfig);
            if (dt.Rows.Count > 0)
                dgvLeaveConfiguration.DataSource = dt;
            else
                dgvLeaveConfiguration.DataSource = null;
                dgvLeaveConfiguration.DataBind();
        }

      
    }
}