using System;
using System.Web.UI.WebControls;
using System.Data;
using Business.Common;
using Entity.Common;

namespace WebAppAegisCRM.LeaveManagement
{
    public partial class LeaveConfig : System.Web.UI.Page
    {
        private int LeaveConfigurationId
        {
            get { return Convert.ToInt32(ViewState["LeaveConfigurationId"]); }
            set { ViewState["LeaveConfigurationId"] = value; }
        }

        private void LoadLeaveFrequency()
        {
            ddlLeaveFrequency.DataSource = Enum.GetValues(typeof(LeaveFrequencyEnum));
            ddlLeaveFrequency.DataBind();
            ddlLeaveFrequency.InsertSelect();
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
            Entity.LeaveManagement.LeaveConfiguration leaveConfiguration = new Entity.LeaveManagement.LeaveConfiguration();

            DataTable dt = Business.LeaveManagement.LeaveConfiguration.LeaveConfigurations_GetAll(leaveConfiguration);

            dgvLeaveConfiguration.DataSource = dt;
            dgvLeaveConfiguration.DataBind();
        }

        private void Clear()
        {
            LeaveConfigurationId = 0;
            ddlLeaveType.SelectedIndex = 0;
            ddlLeaveFrequency.SelectedIndex = 0;
            txtLeaveAccrueDate.Text = "";
            Message.Show = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadLeaveType();
                LeaveConfig_GetAll();
                LoadLeaveFrequency();
                Message.Show = false;
            }
        }

        protected void FetchLeaveConfigById(int LeaveConfigId)
        {
            Business.LeaveManagement.LeaveConfiguration objLeaveConfiguration = new Business.LeaveManagement.LeaveConfiguration();
            Entity.LeaveManagement.LeaveConfiguration leaveConfiguration = new Entity.LeaveManagement.LeaveConfiguration();
            leaveConfiguration.LeaveConfigId = LeaveConfigId;
            DataTable dt = objLeaveConfiguration.FetchLeaveConfigById(leaveConfiguration);
            if (dt.Rows.Count > 0)
            {
                ddlLeaveType.SelectedValue = dt.Rows[0]["LeaveTypeId"].ToString();
                ddlLeaveFrequency.SelectedValue = dt.Rows[0]["LeaveFrequency"].ToString();
                txtLeaveAccrueDate.Text = Convert.ToDateTime(dt.Rows[0]["LeaveAccrueDate"]).ToString("dd MMM yyyy");
                ckEncashable.Checked = Convert.ToBoolean(dt.Rows[0]["Encashable"].ToString());
            }
        }

        protected void gvLeaveConfig_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "E")
            {
                LeaveConfigurationId = Convert.ToInt32(e.CommandArgument.ToString());
                FetchLeaveConfigById(LeaveConfigurationId);
            }
            else
            {
                if (e.CommandName == "D")
                {
                    Business.LeaveManagement.LeaveConfiguration objLeaveConfiguration = new Business.LeaveManagement.LeaveConfiguration();
                    LeaveConfigurationId = Convert.ToInt32(e.CommandArgument.ToString());
                    int RowsAffected = objLeaveConfiguration.LeaveConfigurations_Delete(LeaveConfigurationId);
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
            Business.LeaveManagement.LeaveConfiguration objLeaveConfiguration = new Business.LeaveManagement.LeaveConfiguration();
            Entity.LeaveManagement.LeaveConfiguration leaveConfiguration = new Entity.LeaveManagement.LeaveConfiguration();
            leaveConfiguration.LeaveConfigId = LeaveConfigurationId;
            leaveConfiguration.LeaveTypeId = Convert.ToInt32(ddlLeaveType.SelectedValue);
            leaveConfiguration.LeaveFrequency = ddlLeaveFrequency.SelectedValue;
            leaveConfiguration.LeaveAccrueDate = Convert.ToDateTime(txtLeaveAccrueDate.Text.Trim());
            leaveConfiguration.Encashable = ckEncashable.Checked;
            int response = objLeaveConfiguration.LeaveConfigurations_Save(leaveConfiguration);
            if (response > 0)
            {
                Clear();
                LeaveConfig_GetAll();
                GlobalCache.RemoveAll();
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