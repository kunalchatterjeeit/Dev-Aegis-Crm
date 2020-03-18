using Business.Common;
using Entity.HR;
using log4net;
using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.ClaimManagement
{
    public partial class ClaimApplication : System.Web.UI.Page
    {
        ILog logger = log4net.LogManager.GetLogger("ErrorLog");
        private string _ClaimNo
        {
            get
            {
                return DateTime.Now.Ticks.ToString();
            }
        }
        private string _AttachmentName
        {
            get
            {
                return DateTime.Now.Ticks.ToString();
            }
        }

        private DataTable _ClaimDetails
        {
            get
            {
                return Business.Common.Context.ClaimDetails;
            }

            set { Business.Common.Context.ClaimDetails = value; }
        }

        private void ClearAllControl()
        {
            txtExpenseDate.Text = DateTime.Now.ToString("dd MMM yyyy");
            txtPeriodFrom.Text = DateTime.Now.ToString("dd MMM yyyy");
            txtPeriodTo.Text = DateTime.Now.ToString("dd MMM yyyy");
            ddlCategory.SelectedIndex = 0;
            txtCost.Text = "0";
            Message.Show = false;
            _ClaimDetails.Clear();
            LoadClaimDetails();
        }

        private void LoadClaimCategory()
        {
            ddlCategory.DataSource = new Business.ClaimManagement.ClaimCategory()
                .ClaimCategoryGetAll(new Entity.ClaimManagement.ClaimCategory() { });
            ddlCategory.DataTextField = "CategoryName";
            ddlCategory.DataValueField = "ClaimCategoryId";
            ddlCategory.DataBind();
            ddlCategory.InsertSelect();
        }

        private bool ClaimApplyValidation()
        {
            if (string.IsNullOrEmpty(txtClaimHeader.Text.Trim()))
            {
                Message.IsSuccess = false;
                Message.Text = "Please enter claim header.";
                Message.Show = true;
            }
            if (string.IsNullOrEmpty(txtPeriodFrom.Text.Trim()))
            {
                Message.IsSuccess = false;
                Message.Text = "Please enter claim period from.";
                Message.Show = true;
            }
            if (string.IsNullOrEmpty(txtPeriodTo.Text.Trim()))
            {
                Message.IsSuccess = false;
                Message.Text = "Please enter claim period to.";
                Message.Show = true;
            }
            if (!(_ClaimDetails != null && _ClaimDetails.Rows.Count > 0))
            {
                Message.IsSuccess = false;
                Message.Text = "Please add claim details before submit.";
                Message.Show = true;
            }
            return true;
        }

        private bool ClaimAddValidation()
        {
            int designationId = 0;
            DataTable dtEmployee = new Business.HR.EmployeeMaster().EmployeeMaster_ById(new EmployeeMaster() { EmployeeMasterId = Convert.ToInt32(HttpContext.Current.User.Identity.Name) });
            if (dtEmployee != null && dtEmployee.AsEnumerable().Any())
            {
                designationId = Convert.ToInt32(dtEmployee.Rows[0]["DesignationMasterId_FK"].ToString());
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Employee details not found!  Contact administrator.";
                Message.Show = true;
                return false;
            }
            DataTable dtClaimDesignationWiseConfiguration = GlobalCache.ExecuteCache<DataTable>(typeof(Business.ClaimManagement.ClaimDesignationWiseConfiguration), "ClaimDesignationConfig_GetAllCached", new Entity.ClaimManagement.ClaimDesignationWiseConfiguration());
            if (dtClaimDesignationWiseConfiguration != null && dtClaimDesignationWiseConfiguration.AsEnumerable().Any())
            {
                using (DataView dvClaimDesignationWiseConfiguration = new DataView(dtClaimDesignationWiseConfiguration))
                {
                    dvClaimDesignationWiseConfiguration.RowFilter = "DesignationId = " + designationId + " AND ClaimCategoryId = " + ddlCategory.SelectedValue + "";
                    dtClaimDesignationWiseConfiguration = dvClaimDesignationWiseConfiguration.ToTable();
                }
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Claim configuration for your designation not found! Contact administrator.";
                Message.Show = true;
                return false;
            }
            if (dtClaimDesignationWiseConfiguration != null && dtClaimDesignationWiseConfiguration.AsEnumerable().Any())
            {
                decimal totalCost = Convert.ToDecimal(txtCost.Text);

                if (_ClaimDetails != null && _ClaimDetails.AsEnumerable().Any())
                {
                    using (DataView dvClaimDetails = new DataView(_ClaimDetails))
                    {
                        dvClaimDetails.RowFilter = "CategoryId = " + ddlCategory.SelectedValue;
                        if (dvClaimDetails.ToTable() != null && dvClaimDetails.ToTable().AsEnumerable().Any())
                        {
                            totalCost += Convert.ToDecimal(dvClaimDetails.ToTable().Compute("SUM(Cost)", string.Empty));
                        }
                    }
                }
                if (totalCost > Convert.ToDecimal(dtClaimDesignationWiseConfiguration.Rows[0]["Limit"].ToString()))
                {
                    Message.IsSuccess = false;
                    Message.Text = "Your claim limit exceeded.";
                    Message.Show = true;
                    return false;
                }
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Claim configuration for your designation not found! Contact administrator.";
                Message.Show = true;
                return false;
            }

            return true;
        }

        private void GetClaimAccountBalance()
        {
            txtAdvanceAmount.Text = Convert.ToString(new Business.ClaimManagement.ClaimDisbursement()
                .GetClaimAccountBalance(Convert.ToInt32(HttpContext.Current.User.Identity.Name)));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    LoadClaimCategory();
                    ClearAllControl();
                    GetClaimAccountBalance();
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

        protected void gvClaimDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "D")
                {
                    string autoId = e.CommandArgument.ToString();

                    if (DeleteItem(autoId))
                    {
                        LoadClaimDetails();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "mmsg", "alert('Data can not be deleted!!!....');", true);
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ClaimApplyValidation())
                {
                    Entity.ClaimManagement.ClaimApplicationMaster claimApplicationMaster = ClaimApplicationMaster_Save();

                    foreach (DataRow drClaimDetail in _ClaimDetails.Rows)
                    {
                        ClaimApplicationDetails_Save(claimApplicationMaster.ClaimApplicationId,
                            Convert.ToDateTime(drClaimDetail["ExpenseDate"].ToString()),
                            drClaimDetail["Attachment"].ToString(),
                            Convert.ToInt32(Enum.Parse(typeof(ClaimCategoryEnum), drClaimDetail["CategoryId"].ToString())),
                            Convert.ToDecimal(drClaimDetail["Cost"].ToString()),
                            drClaimDetail["Description"].ToString(),
                            Convert.ToInt32(Enum.Parse(typeof(ClaimStatusEnum), drClaimDetail["Status"].ToString())));
                    }

                    int approvalResponse = ClaimApprovalDetails_Save(claimApplicationMaster.ClaimApplicationId);
                    if (approvalResponse > 0)
                    {
                        ClearAllControl();
                        _ClaimDetails.Clear();
                        Message.IsSuccess = true;
                        Message.Text = string.Format("Claim applied successfully. Your claim no. {0}", claimApplicationMaster.ClaimApplicationNumber);
                        Message.Show = true;
                    }
                    else
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Claim approval send failed! Please contact system administrator.";
                        Message.Show = true;
                    }
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Claim apply failed! Please contact system administrator.";
                    Message.Show = true;
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

        private Entity.ClaimManagement.ClaimApplicationMaster ClaimApplicationMaster_Save()
        {
            Entity.ClaimManagement.ClaimApplicationMaster claimApplicationMaster = new Entity.ClaimManagement.ClaimApplicationMaster();
            Business.ClaimManagement.ClaimApplication objClaimApplicationMaster = new Business.ClaimManagement.ClaimApplication();

            claimApplicationMaster.ClaimDateTime = DateTime.Now;
            claimApplicationMaster.ClaimHeading = txtClaimHeader.Text.Trim();
            claimApplicationMaster.PeriodFrom = Convert.ToDateTime(txtPeriodFrom.Text);
            claimApplicationMaster.PeriodTo = Convert.ToDateTime(txtPeriodTo.Text);
            claimApplicationMaster.Status = (int)ClaimStatusEnum.Pending;
            claimApplicationMaster.EmployeeId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            claimApplicationMaster.ClaimApplicationNumber = string.Empty;
            claimApplicationMaster.TotalAmount = (decimal)_ClaimDetails.Compute("SUM(Cost)", string.Empty);
            claimApplicationMaster.CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            claimApplicationMaster.AdjustRequestAmount = Convert.ToDecimal(txtAdjustAdvance.Text.Trim());
            claimApplicationMaster = objClaimApplicationMaster.ClaimApplicationMaster_Save(claimApplicationMaster);
            return claimApplicationMaster;
        }

        private int ClaimApplicationDetails_Save(int claimApplicationId, DateTime expenseDate, string attachment, int categoryId, decimal cost, string description, int status)
        {
            Entity.ClaimManagement.ClaimApplicationDetails claimApplicationDetails = new Entity.ClaimManagement.ClaimApplicationDetails();
            Business.ClaimManagement.ClaimApplication objClaimApplicationMaster = new Business.ClaimManagement.ClaimApplication();

            claimApplicationDetails.ClaimApplicationDetailId = 0;
            claimApplicationDetails.ClaimApplicationId = claimApplicationId;
            claimApplicationDetails.ExpenseDate = expenseDate;
            claimApplicationDetails.Attachment = attachment;
            claimApplicationDetails.CategoryId = categoryId;
            claimApplicationDetails.Cost = cost;
            claimApplicationDetails.Description = description;
            claimApplicationDetails.Status = status;
            int response = objClaimApplicationMaster.ClaimApplicationDetails_Save(claimApplicationDetails);
            return response;
        }

        private int ClaimApprovalDetails_Save(int claimApplicationId)
        {
            int response = 0;
            Business.ClaimManagement.ClaimApprovalConfiguration objClaimApprovalConfiguration = new Business.ClaimManagement.ClaimApprovalConfiguration();
            DataTable dtClaimEmployeeWiseApprovalConfiguration = objClaimApprovalConfiguration.ClaimEmployeeWiseApprovalConfiguration_GetAll(
                new Entity.ClaimManagement.ClaimApprovalConfiguration()
                {
                    EmployeeId = Convert.ToInt32(HttpContext.Current.User.Identity.Name)
                });


            Business.ClaimManagement.ClaimApprovalDetails objClaimApprovalDetails = new Business.ClaimManagement.ClaimApprovalDetails();
            Entity.ClaimManagement.ClaimApprovalDetails ClaimApprovalDetails = new Entity.ClaimManagement.ClaimApprovalDetails();

            //If ClaimEmployeeWiseApprovalConfiguration is configured
            if (dtClaimEmployeeWiseApprovalConfiguration != null
                && dtClaimEmployeeWiseApprovalConfiguration.AsEnumerable().Any()
                && dtClaimEmployeeWiseApprovalConfiguration.Select("ApprovalLevel = 1").Any())
            {
                ClaimApprovalDetails.ApproverId = Convert.ToInt32(dtClaimEmployeeWiseApprovalConfiguration.Select("ApprovalLevel = 1").FirstOrDefault()["ApproverId"].ToString());
            }
            else //If not confiured then send approval to Reporting employee
            {
                DataTable dtEmployee = new Business.HR.EmployeeMaster().EmployeeMaster_ById(new Entity.HR.EmployeeMaster() { EmployeeMasterId = Convert.ToInt32(HttpContext.Current.User.Identity.Name) });
                if (dtEmployee != null && dtEmployee.AsEnumerable().Any())
                {
                    ClaimApprovalDetails.ApproverId = Convert.ToInt32(dtEmployee.Rows[0]["ReportingEmployeeId"].ToString());
                }
            }
            ClaimApprovalDetails.ClaimApprovalDetailId = 0;
            ClaimApprovalDetails.ClaimApplicationId = claimApplicationId;
            ClaimApprovalDetails.Status = (int)ClaimStatusEnum.Pending;
            ClaimApprovalDetails.Remarks = "APPROVAL PENDING";

            response = objClaimApprovalDetails.ClaimApprovalDetails_Save(ClaimApprovalDetails);


            return response;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAllControl();
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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearDetailsControls();
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                Message.Show = false;
                if (ClaimAddValidation())
                {
                    if (_ClaimDetails.Rows.Count == 0)
                    {
                        using (DataTable dtInstance = new DataTable())
                        {
                            DataColumn column = new DataColumn("AutoId");
                            column.AutoIncrement = true;
                            column.ReadOnly = true;
                            column.Unique = false;
                            dtInstance.Columns.Add(column);

                            dtInstance.Columns.Add("ExpenseDate");
                            dtInstance.Columns.Add("CategoryName");
                            dtInstance.Columns.Add("CategoryId");
                            dtInstance.Columns.Add("Status");
                            dtInstance.Columns.Add("Cost", typeof(decimal));
                            dtInstance.Columns.Add("Attachment");
                            dtInstance.Columns.Add("Description");
                            _ClaimDetails = dtInstance;
                        }
                    }

                    DataRow drItem = _ClaimDetails.NewRow();
                    drItem["ExpenseDate"] = txtExpenseDate.Text;
                    drItem["CategoryName"] = ddlCategory.SelectedItem;
                    drItem["CategoryId"] = ddlCategory.SelectedValue;
                    drItem["Status"] = ClaimStatusEnum.Pending.ToString();
                    drItem["Cost"] = txtCost.Text;
                    drItem["Description"] = txtDescription.Text.Trim();
                    drItem["Attachment"] = SaveAttachment();

                    _ClaimDetails.Rows.Add(drItem);
                    _ClaimDetails.AcceptChanges();

                    LoadClaimDetails();
                    ClearDetailsControls();
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

        private void ClearDetailsControls()
        {
            txtExpenseDate.Text = DateTime.Now.ToString("dd MMM yyyy");
            ddlCategory.SelectedIndex = 0;
            txtCost.Text = "0";
            txtDescription.Text = string.Empty;
        }

        private string SaveAttachment()
        {
            string retValue = string.Empty;
            if (fuAttachment.HasFile)
            {
                retValue = string.Format("{0}{1}", _AttachmentName, System.IO.Path.GetExtension(fuAttachment.FileName));
                fuAttachment.PostedFile.SaveAs(Server.MapPath(" ") + "\\ClaimAttachments\\" + retValue);
            }
            return retValue;
        }

        private void LoadClaimDetails()
        {
            gvClaimDetails.DataSource = _ClaimDetails;
            gvClaimDetails.DataBind();
        }

        private bool DeleteItem(string autoId)
        {
            bool retValue = false;
            int lastCount = 0;
            if (_ClaimDetails.Rows.Count > 0)
            {
                lastCount = _ClaimDetails.Rows.Count;
                _ClaimDetails.Rows[_ClaimDetails.Rows.IndexOf(_ClaimDetails.Select("AutoId='" + autoId + "'").FirstOrDefault())].Delete();
                _ClaimDetails.AcceptChanges();
            }
            if (lastCount > _ClaimDetails.Rows.Count)
            {
                retValue = true;
            }
            return retValue;
        }
    }
}