using Business.Common;
using log4net;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.ClaimManagement
{
    public partial class ClaimApprovalConfiguration : System.Web.UI.Page
    {
        ILog logger = log4net.LogManager.GetLogger("ErrorLog");
        private int ClaimApprovalConfigurationId
        {
            get { return Convert.ToInt32(ViewState["ClaimApprovalConfigurationId"]); }
            set { ViewState["ClaimApprovalConfigurationId"] = value; }
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

        private void ClaimApprovalConfig_GetById()
        {
            Business.ClaimManagement.ClaimApprovalConfiguration objClaimApprovalConfiguration = new Business.ClaimManagement.ClaimApprovalConfiguration();

            DataTable dt = objClaimApprovalConfiguration.ClaimApprovalConfig_GetAll(new Entity.ClaimManagement.ClaimApprovalConfiguration() { ClaimApprovalConfigurationId = ClaimApprovalConfigurationId });

            if (dt.Rows.Count > 0)
            {
                ddlRequestorDesignation.SelectedValue = dt.Rows[0]["ClaimDesignationConfigId"].ToString();
                ddlApproverDesignation.SelectedValue = dt.Rows[0]["ApproverDesignationId"].ToString();
                ddlApprovalLevel.SelectedValue = dt.Rows[0]["ApproverLevel"].ToString();
            }
        }

        private void ClaimApprovalConfig_GetAll()
        {
            Business.ClaimManagement.ClaimApprovalConfiguration objClaimApprovalConfiguration = new Business.ClaimManagement.ClaimApprovalConfiguration();

            DataTable dt = objClaimApprovalConfiguration.ClaimApprovalConfig_GetAll(new Entity.ClaimManagement.ClaimApprovalConfiguration() { });
            gvClaimApprovalConfiguration.DataSource = dt;
            gvClaimApprovalConfiguration.DataBind();
        }

        private void Clear()
        {
            ClaimApprovalConfigurationId = 0;
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
                    ClaimApprovalConfig_GetAll();
                    Message.Show = false;
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
                logger.Error(ex.Message);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Business.ClaimManagement.ClaimApprovalConfiguration objClaimApprovalConfiguration = new Business.ClaimManagement.ClaimApprovalConfiguration();
                Entity.ClaimManagement.ClaimApprovalConfiguration ClaimApprovalConfiguration = new Entity.ClaimManagement.ClaimApprovalConfiguration();

                ClaimApprovalConfiguration.ClaimApprovalConfigurationId = ClaimApprovalConfigurationId;
                ClaimApprovalConfiguration.ApproverDesignationId = Convert.ToInt32(ddlApproverDesignation.SelectedValue);
                ClaimApprovalConfiguration.ClaimDesignationConfigurationId = Convert.ToInt32(ddlRequestorDesignation.SelectedValue);
                ClaimApprovalConfiguration.ApprovalLevel = Convert.ToInt32(ddlApprovalLevel.SelectedValue);
                int response = objClaimApprovalConfiguration.ClaimApprovalConfig_Save(ClaimApprovalConfiguration);
                if (response > 0)
                {
                    Clear();
                    ClaimApprovalConfig_GetAll();
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
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
                logger.Error(ex.Message);
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
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
                logger.Error(ex.Message);
            }
        }

        protected void gvClaimApprovalConfiguration_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "E")
                {
                    ClaimApprovalConfigurationId = Convert.ToInt32(e.CommandArgument.ToString());
                    ClaimApprovalConfig_GetById();
                }
                else
                {
                    if (e.CommandName == "D")
                    {
                        Business.ClaimManagement.ClaimApprovalConfiguration objClaimApprovalConfiguration = new Business.ClaimManagement.ClaimApprovalConfiguration();
                        ClaimApprovalConfigurationId = Convert.ToInt32(e.CommandArgument.ToString());
                        int RowsAffected = objClaimApprovalConfiguration.ClaimApprovalConfig_Delete(ClaimApprovalConfigurationId);
                        if (RowsAffected > 0)
                        {
                            Clear();
                            ClaimApprovalConfig_GetAll();
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
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
                logger.Error(ex.Message);
            }
        }
    }
}