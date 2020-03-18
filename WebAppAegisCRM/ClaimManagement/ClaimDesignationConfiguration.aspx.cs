using Business.Common;
using log4net;
using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.ClaimManagement
{
    public partial class ClaimDesignationConfiguration : System.Web.UI.Page
    {
        ILog logger = log4net.LogManager.GetLogger("ErrorLog");
        private int ClaimDesignationWiseConfigurationId
        {
            get { return Convert.ToInt32(ViewState["ClaimDesignationWiseConfigurationId"]); }
            set { ViewState["ClaimDesignationWiseConfigurationId"] = value; }
        }

        private void LoadClaimCategory()
        {
            Business.ClaimManagement.ClaimCategory objClaimCategory = new Business.ClaimManagement.ClaimCategory();
            DataTable dtClaimMaster = objClaimCategory.ClaimCategoryGetAll(new Entity.ClaimManagement.ClaimCategory());
            if (dtClaimMaster != null)
            {
                ddlClaimCategory.DataSource = dtClaimMaster;
                ddlClaimCategory.DataTextField = "CategoryName";
                ddlClaimCategory.DataValueField = "ClaimCategoryId";
                ddlClaimCategory.DataBind();
            }
            ddlClaimCategory.InsertSelect();
        }

        private void DesignationMaster_GetAll()
        {
            Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
            Entity.HR.EmployeeMaster employeeMaster = new Entity.HR.EmployeeMaster();
            employeeMaster.CompanyId_FK = 1;
            DataTable dt = objEmployeeMaster.DesignationMaster_GetAll(employeeMaster);
            if (dt.Rows.Count > 0)
            {
                ddlDesignation.DataSource = dt;
                ddlDesignation.DataTextField = "DesignationName";
                ddlDesignation.DataValueField = "DesignationMasterId";
                ddlDesignation.DataBind();
            }
            ddlDesignation.InsertSelect();
        }

        private void ClaimDesignationConfig_GetById()
        {
            Business.ClaimManagement.ClaimDesignationWiseConfiguration objClaimDesignationWiseConfiguration = new Business.ClaimManagement.ClaimDesignationWiseConfiguration();

            DataTable dt = objClaimDesignationWiseConfiguration.ClaimDesignationConfig_GetById(ClaimDesignationWiseConfigurationId);

            if (dt.Rows.Count > 0)
            {
                ddlClaimCategory.SelectedValue = dt.Rows[0]["ClaimCategoryId"].ToString();
                ddlDesignation.Text = dt.Rows[0]["DesignationId"].ToString();
                txtClaimLimit.Text = dt.Rows[0]["Limit"].ToString();
                txtFollowupInterval.Text = dt.Rows[0]["FollowupInterval"].ToString();
            }
        }

        private void Clear()
        {
            ClaimDesignationWiseConfigurationId = 0;
            ddlClaimCategory.SelectedIndex = 0;
            ddlDesignation.SelectedIndex = 0;
            txtFollowupInterval.Text = string.Empty;
            txtClaimLimit.Text = string.Empty;
            Message.Show = false;
        }

        private void ClaimDesignationWiseConfiguration_GetAll()
        {
            Business.ClaimManagement.ClaimDesignationWiseConfiguration objClaimDesignationWiseConfiguration = new Business.ClaimManagement.ClaimDesignationWiseConfiguration();
            Entity.ClaimManagement.ClaimDesignationWiseConfiguration ClaimDesignationWiseConfiguration = new Entity.ClaimManagement.ClaimDesignationWiseConfiguration();

            DataTable dt = objClaimDesignationWiseConfiguration.ClaimDesignationConfig_GetAll(ClaimDesignationWiseConfiguration);

            gvClaimDesignationConfiguration.DataSource = dt;
            gvClaimDesignationConfiguration.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    LoadClaimCategory();
                    DesignationMaster_GetAll();
                    ClaimDesignationWiseConfiguration_GetAll();
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
                if (ClaimDesignationConfigValidate())
                {
                    Business.ClaimManagement.ClaimDesignationWiseConfiguration objClaimDesignationWiseConfiguration = new Business.ClaimManagement.ClaimDesignationWiseConfiguration();
                    Entity.ClaimManagement.ClaimDesignationWiseConfiguration ClaimDesignationWiseConfiguration = new Entity.ClaimManagement.ClaimDesignationWiseConfiguration();

                    ClaimDesignationWiseConfiguration.ClaimDesignationConfigId = ClaimDesignationWiseConfigurationId;
                    ClaimDesignationWiseConfiguration.ClaimCategoryId = Convert.ToInt32(ddlClaimCategory.SelectedValue);
                    ClaimDesignationWiseConfiguration.DesignationId = Convert.ToInt32(ddlDesignation.SelectedValue);
                    ClaimDesignationWiseConfiguration.Limit = Convert.ToDecimal(txtClaimLimit.Text.Trim());
                    ClaimDesignationWiseConfiguration.FollowupInterval = Convert.ToInt32(txtFollowupInterval.Text.Trim());
                    int response = objClaimDesignationWiseConfiguration.ClaimDesignationConfig_Save(ClaimDesignationWiseConfiguration);
                    if (response > 0)
                    {
                        Clear();
                        GlobalCache.RemoveAll();
                        ClaimDesignationWiseConfiguration_GetAll();
                        Message.IsSuccess = true;
                        Message.Text = "Saved Successfully";
                    }
                    else
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Exists";
                    }
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

        protected void gvClaimDesignationConfiguration_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "E")
                {
                    ClaimDesignationWiseConfigurationId = Convert.ToInt32(e.CommandArgument.ToString());
                    ClaimDesignationConfig_GetById();
                }
                else
                {
                    if (e.CommandName == "D")
                    {
                        Business.ClaimManagement.ClaimDesignationWiseConfiguration objClaimDesignationWiseConfiguration = new Business.ClaimManagement.ClaimDesignationWiseConfiguration();
                        ClaimDesignationWiseConfigurationId = Convert.ToInt32(e.CommandArgument.ToString());
                        int RowsAffected = objClaimDesignationWiseConfiguration.ClaimDesignationConfig_Delete(ClaimDesignationWiseConfigurationId);
                        if (RowsAffected > 0)
                        {
                            GlobalCache.RemoveAll();
                            LoadClaimCategory();
                            ClaimDesignationWiseConfiguration_GetAll();
                            Message.Show = true;
                            Message.Text = "Deleted Successfully";
                        }
                        else
                        {
                            {
                                Message.Show = false;
                                Message.Text = "Data Dependency Exists";
                            }
                            Message.Show = true;
                        }
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

        private bool ClaimDesignationConfigValidate()
        {
            bool retValue = true;
            if (ddlClaimCategory.SelectedIndex == 0)
            {
                Message.IsSuccess = false;
                Message.Text = "Please select Claim Type.";
                Message.Show = true;
                return false;
            }
            if (ddlDesignation.SelectedIndex == 0)
            {
                Message.IsSuccess = false;
                Message.Text = "Please select Designation.";
                Message.Show = true;
                return false;
            }
            if (string.IsNullOrEmpty(txtClaimLimit.Text.Trim()))
            {
                Message.IsSuccess = false;
                Message.Text = "Please enter Claim Limit.";
                Message.Show = true;
                return false;
            }

            Business.ClaimManagement.ClaimDesignationWiseConfiguration objClaimDesignationWiseConfiguration = new Business.ClaimManagement.ClaimDesignationWiseConfiguration();
            Entity.ClaimManagement.ClaimDesignationWiseConfiguration ClaimDesignationWiseConfiguration = new Entity.ClaimManagement.ClaimDesignationWiseConfiguration();
            ClaimDesignationWiseConfiguration.ClaimCategoryId = Convert.ToInt32(ddlClaimCategory.SelectedValue);
            ClaimDesignationWiseConfiguration.DesignationId = Convert.ToInt32(ddlDesignation.SelectedValue);
            DataTable dt = objClaimDesignationWiseConfiguration.ClaimDesignationConfig_GetAll(ClaimDesignationWiseConfiguration);
            if (ClaimDesignationWiseConfigurationId == 0)
            {
                if (dt != null && dt.AsEnumerable().Any())
                {
                    Message.IsSuccess = false;
                    Message.Text = "Designation configuration already exists.";
                    Message.Show = true;
                    return false;
                }
            }
            else
            {
                if (dt == null || !dt.AsEnumerable().Any())
                {
                    Message.IsSuccess = false;
                    Message.Text = "Designation configuration does not exists.";
                    Message.Show = true;
                    return false;
                }
            }
            return retValue;
        }

    }
}