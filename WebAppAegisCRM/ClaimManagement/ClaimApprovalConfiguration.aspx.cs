using Business.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.ClaimManagement
{
    public partial class ClaimApprovalConfiguration : System.Web.UI.Page
    {
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

        protected void ClaimApprovalConfig_GetById()
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

        protected void ClaimApprovalConfig_GetAll()
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
            if (!IsPostBack)
            {
                DesignationMaster_GetAll();
                ClaimApprovalConfig_GetAll();
                Message.Show = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        protected void gvClaimApprovalConfiguration_RowCommand(object sender, GridViewCommandEventArgs e)
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
    }
}