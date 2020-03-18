using Business.Common;
using log4net;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.LeaveManagement
{
    public partial class LeaveApprovalConfiguration : System.Web.UI.Page
    {
        ILog logger = log4net.LogManager.GetLogger("ErrorLog");
        private int LeaveApprovalConfigurationId
        {
            get { return Convert.ToInt32(ViewState["LeaveApprovalConfigurationId"]); }
            set { ViewState["LeaveApprovalConfigurationId"] = value; }
        }

        private void DesignationMaster_GetAll()
        {
            Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
            Entity.HR.EmployeeMaster employeeMaster = new Entity.HR.EmployeeMaster();
            employeeMaster.CompanyId_FK = 1;
            DataTable dt = objEmployeeMaster.DesignationMaster_GetAll(employeeMaster);
            if (dt.Rows.Count > 0)
            {
                ddlApproverDesignation.DataSource = dt;
                ddlApproverDesignation.DataTextField = "DesignationName";
                ddlApproverDesignation.DataValueField = "DesignationMasterId";
                ddlApproverDesignation.DataBind();

                ddlRequestorDesignation.DataSource = dt;
                ddlRequestorDesignation.DataTextField = "DesignationName";
                ddlRequestorDesignation.DataValueField = "DesignationMasterId";
                ddlRequestorDesignation.DataBind();
            }
            ddlApproverDesignation.InsertSelect();
            ddlRequestorDesignation.InsertSelect();
        }

        protected void LeaveApprovalConfig_GetById()
        {
            Business.LeaveManagement.LeaveApprovalConfiguration objLeaveApprovalConfiguration = new Business.LeaveManagement.LeaveApprovalConfiguration();

            DataTable dt = objLeaveApprovalConfiguration.LeaveApprovalConfig_GetAll(new Entity.LeaveManagement.LeaveApprovalConfiguration() { LeaveApprovalConfigurationId = LeaveApprovalConfigurationId });

            if (dt.Rows.Count > 0)
            {
                ddlRequestorDesignation.SelectedValue = dt.Rows[0]["LeaveDesignationConfigId"].ToString();
                ddlApproverDesignation.SelectedValue = dt.Rows[0]["ApproverDesignationId"].ToString();
                ddlApprovalLevel.SelectedValue = dt.Rows[0]["ApproverLevel"].ToString();
            }
        }

        protected void LeaveApprovalConfig_GetAll()
        {
            Business.LeaveManagement.LeaveApprovalConfiguration objLeaveApprovalConfiguration = new Business.LeaveManagement.LeaveApprovalConfiguration();

            DataTable dt = objLeaveApprovalConfiguration.LeaveApprovalConfig_GetAll(new Entity.LeaveManagement.LeaveApprovalConfiguration() { });
            gvLeaveApprovalConfiguration.DataSource = dt;
            gvLeaveApprovalConfiguration.DataBind();
        }

        private void Clear()
        {
            LeaveApprovalConfigurationId = 0;
            ddlApproverDesignation.SelectedIndex = 0;
            ddlRequestorDesignation.SelectedIndex = 0;
            ddlApprovalLevel.SelectedIndex = 0;
            Message.Show = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    DesignationMaster_GetAll();
                    LeaveApprovalConfig_GetAll();
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Business.LeaveManagement.LeaveApprovalConfiguration objLeaveApprovalConfiguration = new Business.LeaveManagement.LeaveApprovalConfiguration();
                Entity.LeaveManagement.LeaveApprovalConfiguration leaveApprovalConfiguration = new Entity.LeaveManagement.LeaveApprovalConfiguration();

                leaveApprovalConfiguration.LeaveApprovalConfigurationId = LeaveApprovalConfigurationId;
                leaveApprovalConfiguration.ApproverDesignationId = Convert.ToInt32(ddlApproverDesignation.SelectedValue);
                leaveApprovalConfiguration.LeaveDesignationConfigurationId = Convert.ToInt32(ddlRequestorDesignation.SelectedValue);
                leaveApprovalConfiguration.ApprovalLevel = Convert.ToInt32(ddlApprovalLevel.SelectedValue);
                int response = objLeaveApprovalConfiguration.LeaveApprovalConfig_Save(leaveApprovalConfiguration);
                if (response > 0)
                {
                    Clear();
                    LeaveApprovalConfig_GetAll();
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
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Clear();
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

        protected void gvLeaveApprovalConfiguration_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "E")
                {
                    LeaveApprovalConfigurationId = Convert.ToInt32(e.CommandArgument.ToString());
                    LeaveApprovalConfig_GetById();
                }
                else
                {
                    if (e.CommandName == "D")
                    {
                        Business.LeaveManagement.LeaveApprovalConfiguration objLeaveApprovalConfiguration = new Business.LeaveManagement.LeaveApprovalConfiguration();
                        LeaveApprovalConfigurationId = Convert.ToInt32(e.CommandArgument.ToString());
                        int RowsAffected = objLeaveApprovalConfiguration.LeaveApprovalConfig_Delete(LeaveApprovalConfigurationId);
                        if (RowsAffected > 0)
                        {
                            Clear();
                            LeaveApprovalConfig_GetAll();
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