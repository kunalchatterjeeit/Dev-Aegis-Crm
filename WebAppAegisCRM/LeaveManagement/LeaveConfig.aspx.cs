using System;
using System.Web.UI.WebControls;
using System.Data;
using Business.Common;

namespace WebAppAegisCRM.LeaveManagement
{
    public partial class LeaveConfig : System.Web.UI.Page
    {
        private int LeaveConfigId
        {
            get { return Convert.ToInt16(ViewState["LeaveConfigId"]); }
            set { ViewState["LeaveConfigId"] = value; }
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
            LeaveConfigId = 0;
            ddlLeaveType.SelectedIndex = 0;
            txtLeaveFrequency.Text = "";
            txtLeaveAccureDate.Text = "";
            txtCarryForwardCount.Text = "";
            Message.Show = false;
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

        protected void FetchLeaveConfigById(int LeaveConfigId)
        {
            Business.LeaveManagement.LeaveConfiguration ObjbelLeaveConfig = new Business.LeaveManagement.LeaveConfiguration();
            Entity.LeaveManagement.LeaveConfiguration lmLeaveConfig = new Entity.LeaveManagement.LeaveConfiguration();
            lmLeaveConfig.LeaveConfigId = LeaveConfigId;
            DataTable dt = ObjbelLeaveConfig.FetchLeaveConfigById(lmLeaveConfig);
            if (dt.Rows.Count > 0)
            {
                ddlLeaveType.SelectedValue = dt.Rows[0]["LeaveTypeId"].ToString();
                txtLeaveFrequency.Text = dt.Rows[0]["LeaveFrequency"].ToString();
                txtLeaveAccureDate.Text = dt.Rows[0]["LeaveAccureDate"].ToString();
                txtCarryForwardCount.Text = dt.Rows[0]["CarryForwardCount"].ToString();
            }
        }

        protected void gvLeaveConfig_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "E")
            {
                LeaveConfigId = Convert.ToInt16(e.CommandArgument.ToString());
                FetchLeaveConfigById(LeaveConfigId);
            }
            else
            {
                if (e.CommandName == "D")
                {
                    Business.LeaveManagement.LeaveConfiguration objLeaveConfig = new Business.LeaveManagement.LeaveConfiguration();
                    LeaveConfigId = Convert.ToInt16(e.CommandArgument.ToString());
                    int RowsAffected = objLeaveConfig.LeaveConfigurations_Delete(LeaveConfigId);
                    if (RowsAffected > 0)
                    {
                        LoadLeaveType();
                        LeaveConfig_GetAll();
                        Message.Show = true;
                        Message.Text = "Deleted Successfully";
                    }
                    else
                    {
                        Message.Show = false;
                        Message.Text = "Data Dependency Exists";
                    }
                    Message.Show = true;
                }
            }
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
            leaveMaster.CarryForwardCount = Convert.ToDecimal(txtCarryForwardCount.Text);
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
                Message.Text = "Exists";
            }
            Message.Show = true;
        }
    }
}