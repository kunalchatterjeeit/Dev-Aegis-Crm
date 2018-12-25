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
using System.Drawing;
using System.Windows;
using System.Threading;
using Entity.Common;

namespace WebAppAegisCRM.LeaveManagement
{
    public partial class LeaveApply : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadLeaveType();
                Business.Common.Context.SelectedDates.Clear();
                Message.Show = false;
            }
        }

        private bool LeaveApplicationControlValidation()
        {
            if (ddlLeaveType.SelectedIndex == 0)
            {
                Message.IsSuccess = false;
                Message.Text = "Please select leave type";
                Message.Show = true;
                return false;
            }
            if (Business.Common.Context.SelectedDates != null && !Business.Common.Context.SelectedDates.Any())
            {
                Message.IsSuccess = false;
                Message.Text = "Please select dates";
                Message.Show = true;
                return false;
            }
            return true;
        }

        private bool LeaveApplyValidation()
        {
            if (!LeaveApplicationControlValidation())
            {
                return false;
            }
            DataTable dtLeaveConfigurations = GlobalCache.ExecuteCache<DataTable>(typeof(Business.LeaveManagement.LeaveConfiguration), "LeaveConfigurations_GetAll", new Entity.LeaveManagement.LeaveConfiguration() { });

            Entity.LeaveManagement.LeaveApplicationMaster leaveApplicationMaster = new Entity.LeaveManagement.LeaveApplicationMaster();
            leaveApplicationMaster.RequestorId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            DataTable dtLeaveApplicationMaster = new Business.LeaveManagement.LeaveApplicationMaster().LeaveApplicationMaster_GetAll(leaveApplicationMaster);
            if (dtLeaveConfigurations != null && dtLeaveConfigurations.AsEnumerable().Any())
            {
                DataRow drLeaveConfiguration = dtLeaveConfigurations.Select("LeaveTypeId = " + ddlLeaveType.SelectedValue).FirstOrDefault();

                if (!(DateTime.Now.Date >= Convert.ToDateTime(drLeaveConfiguration["LeaveAccrueDate"].ToString())))
                {
                    Message.Text = "Leave Not Yet Applicable";
                    Message.IsSuccess = false;
                    Message.Show = true;
                    return false;
                }
                if (dtLeaveApplicationMaster != null && dtLeaveApplicationMaster.AsEnumerable().Any())
                {
                    DateTime yearFirstDate = Convert.ToDateTime("01 JAN " + DateTime.Now.Year.ToString());
                    DateTime yearLastDate = Convert.ToDateTime("31 DEC " + DateTime.Now.Year.ToString());
                    leaveApplicationMaster.RequestorId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    leaveApplicationMaster.FromLeaveStartDate = yearFirstDate;
                    leaveApplicationMaster.ToLeaveStartDate = yearLastDate;
                    leaveApplicationMaster.LeaveStatusId = (int)LeaveStatusEnum.Approved;
                    DataTable dtLeaveApplicationFrequency = new Business.LeaveManagement.LeaveApplicationMaster().LeaveApplicationMaster_GetAll(leaveApplicationMaster);
                    int leaveFrequency = (dtLeaveApplicationFrequency != null) ? dtLeaveApplicationFrequency.Rows.Count : 0;
                    if (Convert.ToInt32(drLeaveConfiguration["LeaveFrequency"].ToString()) <= leaveFrequency)
                    {
                        Message.Text = "Max Leave Application Limit reached for this type of leave";
                        Message.IsSuccess = false;
                        Message.Show = true;
                        return false;
                    }
                    if (dtLeaveApplicationMaster.Select("LeaveStatusId = " + ((int)LeaveStatusEnum.Pending).ToString()).Any())
                    {
                        Message.Text = "You already have leave approval pending.";
                        Message.IsSuccess = false;
                        Message.Show = true;
                        return false;
                    }
                }
            }
            else
            {
                Message.Text = "Leave Configuration not found";
                Message.IsSuccess = false;
                Message.Show = true;
                return false;
            }

            DataTable dtEmployee = new Business.HR.EmployeeMaster().EmployeeMaster_ById(new Entity.HR.EmployeeMaster() { EmployeeMasterId = Convert.ToInt32(HttpContext.Current.User.Identity.Name) });
            if (dtEmployee != null && dtEmployee.AsEnumerable().Any())
            {
                Entity.LeaveManagement.LeaveDesignationWiseConfiguration leaveDesignationWiseConfiguration = new Entity.LeaveManagement.LeaveDesignationWiseConfiguration();
                leaveDesignationWiseConfiguration.LeaveTypeId = Convert.ToInt32(ddlLeaveType.SelectedValue);
                leaveDesignationWiseConfiguration.DesignationId = Convert.ToInt32(dtEmployee.Rows[0]["DesignationMasterId_FK"].ToString());
                DataTable dtLeaveDesignationConfiguration = new Business.LeaveManagement.LeaveDesignationWiseConfiguration().LeaveDesignationConfig_GetAll(leaveDesignationWiseConfiguration);
                if (dtLeaveDesignationConfiguration != null && dtLeaveDesignationConfiguration.AsEnumerable().Any())
                {
                    decimal totalDays = Convert.ToDecimal(lbTotalCount.Text.Trim());
                    if (totalDays < Convert.ToDecimal(dtLeaveDesignationConfiguration.Rows[0]["MinApplyDays"].ToString()))
                    {
                        Message.Text = "Min leave should be more than " + dtLeaveDesignationConfiguration.Rows[0]["MinApplyDays"].ToString() + " days";
                        Message.IsSuccess = false;
                        Message.Show = true;
                        return false;
                    }
                    else if (totalDays > Convert.ToDecimal(dtLeaveDesignationConfiguration.Rows[0]["MaxApplyDays"].ToString()))
                    {
                        Message.Text = "Max leave should be less than " + dtLeaveDesignationConfiguration.Rows[0]["MaxApplyDays"].ToString() + " days";
                        Message.IsSuccess = false;
                        Message.Show = true;
                        return false;
                    }

                    if (Convert.ToDecimal(lbTotalCount.Text.Trim()) > Convert.ToDecimal(dtLeaveDesignationConfiguration.Rows[0]["LeaveCount"].ToString()))
                    {
                        Message.Text = "Maximum Leave you can apply is " + dtLeaveDesignationConfiguration.Rows[0]["LeaveCount"].ToString() + " days in a year";
                        Message.IsSuccess = false;
                        Message.Show = true;
                        return false;
                    }
                }
                else
                {
                    Message.Text = "Leave designation configuration not found";
                    Message.IsSuccess = false;
                    Message.Show = true;
                    return false;
                }


                DataTable dtLeaveAccountBalance = new Business.LeaveManagement.LeaveAccountBalance().LeaveAccountBalance_ByEmployeeId(Convert.ToInt32(HttpContext.Current.User.Identity.Name), Convert.ToInt32(ddlLeaveType.SelectedValue));
                if (dtLeaveAccountBalance != null && dtLeaveAccountBalance.AsEnumerable().Any())
                {
                    if (Convert.ToInt32(lbTotalCount.Text.Trim()) > Convert.ToDecimal(dtLeaveAccountBalance.Rows[0]["Amount"].ToString()))
                    {
                        Message.Text = "Your Leave Balance is low.";
                        Message.IsSuccess = false;
                        Message.Show = true;
                        return false;
                    }
                    if (Convert.ToBoolean(dtLeaveAccountBalance.Rows[0]["LeaveBlocked"].ToString()))
                    {
                        Message.Text = "Your leaves are blocked. Please contact to HR.";
                        Message.IsSuccess = false;
                        Message.Show = true;
                        return false;
                    }
                }
                else
                {
                    Message.Text = "You do not have any Leave Balance.";
                    Message.IsSuccess = false;
                    Message.Show = true;
                    return false;
                }
            }
            else
            {
                Message.Text = "Employee Details not found.";
                Message.IsSuccess = false;
                Message.Show = true;
                return false;
            }
            return true;
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

        private Entity.LeaveManagement.LeaveApplicationMaster LeaveApplicationMaster_Save()
        {
            Entity.LeaveManagement.LeaveApplicationMaster leaveApplicationMaster = new Entity.LeaveManagement.LeaveApplicationMaster();
            Business.LeaveManagement.LeaveApplicationMaster objLeaveApplicationMaster = new Business.LeaveManagement.LeaveApplicationMaster();

            leaveApplicationMaster.ApplyDate = DateTime.Now;
            leaveApplicationMaster.FromDate = Convert.ToDateTime(lbFromDate.Text);
            leaveApplicationMaster.ToDate = Convert.ToDateTime(lbToDate.Text);
            leaveApplicationMaster.LeaveAccumulationTypeId = (int)LeaveAccumulationEnum.Taken;
            leaveApplicationMaster.LeaveStatusId = (int)LeaveStatusEnum.Pending;
            leaveApplicationMaster.LeaveTypeId = Convert.ToInt32(ddlLeaveType.SelectedValue);
            leaveApplicationMaster.Reason = txtReason.Text.Trim();
            leaveApplicationMaster.RequestorId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            leaveApplicationMaster = objLeaveApplicationMaster.LeaveApplicationMaster_Save(leaveApplicationMaster);
            return leaveApplicationMaster;
        }

        private int LeaveApplicationDetails_Save(int leaveApplicationId, DateTime leaveDate)
        {
            Entity.LeaveManagement.LeaveApplicationDetails leaveApplicationDetails = new Entity.LeaveManagement.LeaveApplicationDetails();
            Business.LeaveManagement.LeaveApplicationMaster objLeaveApplicationMaster = new Business.LeaveManagement.LeaveApplicationMaster();

            leaveApplicationDetails.LeaveApplicationDetailId = 0;
            leaveApplicationDetails.LeaveApplicationId = leaveApplicationId;
            leaveApplicationDetails.LeaveDate = leaveDate;
            int response = objLeaveApplicationMaster.LeaveApplicationDetails_Save(leaveApplicationDetails);
            return response;
        }

        private int LeaveApprovalDetails_Save(int leaveApplicationId)
        {
            int response = 0;
            Business.LeaveManagement.LeaveApprovalConfiguration objLeaveApprovalConfiguration = new Business.LeaveManagement.LeaveApprovalConfiguration();
            DataTable dtLeaveEmployeeWiseApprovalConfiguration = objLeaveApprovalConfiguration.LeaveEmployeeWiseApprovalConfiguration_GetAll(
                new Entity.LeaveManagement.LeaveApprovalConfiguration()
                {
                    EmployeeId = Convert.ToInt32(HttpContext.Current.User.Identity.Name)
                });


            Business.LeaveManagement.LeaveApprovalDetails objLeaveApprovalDetails = new Business.LeaveManagement.LeaveApprovalDetails();
            Entity.LeaveManagement.LeaveApprovalDetails leaveApprovalDetails = new Entity.LeaveManagement.LeaveApprovalDetails();

            //If LeaveEmployeeWiseApprovalConfiguration is configured
            if (dtLeaveEmployeeWiseApprovalConfiguration != null
                && dtLeaveEmployeeWiseApprovalConfiguration.AsEnumerable().Any()
                && dtLeaveEmployeeWiseApprovalConfiguration.Select("ApprovalLevel = 1").Any())
            {
                leaveApprovalDetails.ApproverId = Convert.ToInt32(dtLeaveEmployeeWiseApprovalConfiguration.Select("ApprovalLevel = 1").FirstOrDefault()["ApproverId"].ToString());
            }
            else //If not confiured then send approval to Reporting employee
            {
                DataTable dtEmployee = new Business.HR.EmployeeMaster().EmployeeMaster_ById(new Entity.HR.EmployeeMaster() { EmployeeMasterId = Convert.ToInt32(HttpContext.Current.User.Identity.Name) });
                if (dtEmployee != null && dtEmployee.AsEnumerable().Any())
                {
                    leaveApprovalDetails.ApproverId = Convert.ToInt32(dtEmployee.Rows[0]["ReportingEmployeeId"].ToString());
                }
            }
            leaveApprovalDetails.LeaveApprovalDetailId = 0;
            leaveApprovalDetails.LeaveApplicationId = leaveApplicationId;
            leaveApprovalDetails.Status = (int)LeaveStatusEnum.Pending;
            leaveApprovalDetails.Remarks = "APPROVAL PENDING";

            response = objLeaveApprovalDetails.LeaveApprovalDetails_Save(leaveApprovalDetails);


            return response;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (LeaveApplyValidation())
                {
                    Entity.LeaveManagement.LeaveApplicationMaster leaveApplicationMaster = LeaveApplicationMaster_Save();
                    if (leaveApplicationMaster.LeaveApplicationId > 0)
                    {
                        foreach (DateTime selectedDate in Business.Common.Context.SelectedDates)
                        {
                            LeaveApplicationDetails_Save(leaveApplicationMaster.LeaveApplicationId, selectedDate);
                        }

                        int approvalResponse = LeaveApprovalDetails_Save(leaveApplicationMaster.LeaveApplicationId);
                        if (approvalResponse > 0)
                        {
                            Message.IsSuccess = true;
                            Message.Text = "Leave applied successfully.";
                            Message.Show = true;
                        }
                        else
                        {
                            Message.IsSuccess = false;
                            Message.Text = "Leave approval send failed! Please contact system administrator.";
                            Message.Show = true;
                        }
                    }
                    else
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Leave apply failed! Please contact system administrator.";
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
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Business.Common.Context.SelectedDates.Clear();
            Calendar1.SelectedDates.Clear();
        }

        public void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date < DateTime.Now.Date
                && (ddlLeaveType.SelectedValue == ((int)LeaveTypeEnum.CL).ToString() || ddlLeaveType.SelectedValue == ((int)LeaveTypeEnum.PL).ToString()))
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.Red;
                e.Cell.Font.Strikeout = true;
            }
            if (Business.Common.Context.SelectedDates.Any())
            {
                foreach (DateTime dt in Business.Common.Context.SelectedDates)
                {
                    Calendar1.SelectedDates.Add(dt);
                }
            }
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            Calendar calender = ((Calendar)sender);

            if (calender.ValidateContinueSelection())
            {
                if (!Business.Common.Context.SelectedDates.Contains(calender.SelectedDate))
                {
                    List<DateTime> lists = Business.Common.Context.SelectedDates;
                    lists.Add(calender.SelectedDate);
                    Business.Common.Context.SelectedDates = lists;
                }
            }
            else
            {
                Business.Common.Context.SelectedDates.Clear();
                calender.SelectedDates.Clear();
            }


            if (Business.Common.Context.SelectedDates.Any())
            {
                lbFromDate.Text = Business.Common.Context.SelectedDates.Min().ToString("dd MMM yyyy");
                lbToDate.Text = Business.Common.Context.SelectedDates.Max().ToString("dd MMM yyyy");
                lbTotalCount.Text = ((Business.Common.Context.SelectedDates.Max() - Business.Common.Context.SelectedDates.Min()).TotalDays + 1).ToString();
            }
            else
            {
                lbFromDate.Text = string.Empty;
                lbToDate.Text = string.Empty;
                lbTotalCount.Text = string.Empty;
            }
        }
    }
}