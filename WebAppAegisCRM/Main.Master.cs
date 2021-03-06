﻿using Business.Common;
using Entity.Common;
using System;
using System.Data;
using System.Linq;
using System.Web;

namespace WebAppAegisCRM
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated || string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
                Response.Redirect("~/MainLogout.aspx");

            lblUsername.Text = (string.IsNullOrEmpty(lblUsername.Text.Trim())) ? Business.Common.Context.Username : lblUsername.Text.Trim();
            imgUserImage.Src = "HR\\EmployeeImage\\" + ((string.IsNullOrEmpty(imgUserImage.Src)) ? Business.Common.Context.Image : imgUserImage.Src);
            if (string.IsNullOrEmpty(imgUserImage.Src))
                imgUserImage.Attributes.Add("sex", Business.Common.Context.UserGender.ToString());

            Attendance_GetByEmployeeId();

            if (!IsPostBack)
            {
                IndividualLoyalityPoint_ByEmployeeId();
                CheckAttendanceBlocked();

                //CONTROL PANEL
                liControlPanel.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.CONTROLPANEL);
                liServiceCallAttendanceManager.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.SERVICECALLATTENDANCEMANAGER);

                //SETTING
                liSettings.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.SETTINGS);
                liAddEditCity.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.ADDEDITCITY);
                liAddEditContractType.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.ADDEDITCONTRACTTYPE);
                liAddEditBrand.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.ADDEDITBRAND);
                liAddEditModelCategory.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.ADDEDITMODELCATEGORY);
                liAddEditModel.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.ADDEDITMODEL);
                liAddEditSpare.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.ADDEDITSPARE);
                liModelSpareMapping.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.MODELSPAREMAPPING);
                liStoreMaster.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.ADDEDITSTOREMASTER);

                //HR
                liHR.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.HR);
                liAddEditRole.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.ADDEDITROLE);
                liManageRoleAccess.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.MANAGEROLEACCESS);
                if (ApplicationModules.HrModule.ModulePermission())
                {
                    liLoyaltyPointReasonMaster.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.ADDEDITLOYALTYPOINTREASONMASTER);
                    liEmployeeLoyaltyPoint.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.EMPLOYEELOYALTYPOINT);
                    liHolidayProfile.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.HOLIDAY_PROFILE);
                    liHoliday.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.HOLIDAY);
                    liEmployeeHolidayProfileMapping.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.EMPLOYEE_HOLIDAY_PROFILE_MAPPING);
                    liHolidayList.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.HOLIDAY_LIST);
                    liManageAttendance.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.MANAGE_ATTENDANCE);

                    //LEAVE MANAGEMENT
                    liLeaveApplicationList.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.LEAVE_APPLICATION_LIST);
                    liLeaveApply.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.LEAVE_APPLY);
                    liLeaveApprove.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.LEAVE_APPROVE);
                    liLeaveConfiguration.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.LEAVE_CONFIGURATION);
                    liLeaveDesignationConfiguration.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.LEAVE_DESIGNATION_CONFIGURATION);
                    liLeaveManagement.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.LEAVE_MANAGEMENT);
                    liLeaveGenerate.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.LEAVE_GENERATE);
                    liLeaveAdjustment.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.LEAVE_ADJUSTMENT);
                    liLeaveReport.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.LEAVE_REPORT);
                    liEmployeeWorkSummaryReport.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.EMPLOYEE_WORK_SUMMARY_REPORT);
                    
                    //CLAIM MANAGEMENT
                    liClaimList.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.CLAIM_APPLICATION_LIST);
                    liApplyClaim.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.CLAIM_APPLY);
                    liClaimApprove.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.CLAIM_APPROVE);
                    liClaimConfiguration.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.CLAIM_CONFIGURATION);
                    liClaimDesignationConfiguration.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.CLAIM_DESIGNATION_CONFIGURATION);
                    liClaimManagement.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.CLAIM_MANAGEMENT);
                    liClaimReport.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.CLAIM_REPORT);
                    liClaimDisbursement.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.CLAIM_DISBURSEMENT);
                    liVoucherReport.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.VOUCHER_REPORT);

                    liAttendanceReport.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.ATTENDANCE_LIST);
                    lblAttendanceLate.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.ATTENDANCE_LIST);
                    liAttendance.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.ATTENDANCE_LIST);
                }
                //liApplyClaim.Visible = true;
                //liClaimApprove.Visible = true;
                //liClaimConfiguration.Visible = true;
                //liClaimDesignationConfiguration.Visible = true;
                //liClaimList.Visible = true;
                //liClaimManagement.Visible = true;

                //INVENTORY
                liInventory.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.INVENTORY);
                liPurchaseEntry.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.PURCHASE_ENTRY);
                liPurchaseList.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.PURCHASE_LIST);
                liStockLookup.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.STOCK_LOOKUP);
                liVendorMaster.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.ADD_EDIT_VENDOR);
                liVendorList.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.VENDOR_LIST);
                liPurchaseRequisitionEntry.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.PURCHASE_REQUISITION_ENTRY);
                liSaleEntry.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.SALE_CHALLAN_ENTRY);
                liSaleChallanList.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.SALE_CHALLAN_LIST);

                if (ApplicationModules.SalesModule.ModulePermission())
                {
                    //SALES
                    liDepartment.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.SALES_DEPARTMENT);
                    liLeadSource.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.SALES_LEAD_SOURCE);
                    liMeetingType.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.SALES_MEETING_TYPE);
                    liTaskStatus.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.SALES_TASK_STATUS);
                    liSales.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.SALES);
                    liCalls.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.SALES_CALLS);
                    liMeetings.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.SALES_MEETINGS);
                    liNotes.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.SALES_NOTES);
                    liTasks.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.SALES_TASKS);
                    liAccounts.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.SALES_ACCOUNTS);
                    liLeads.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.SALES_LEADS);
                    liCampaign.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.SALES_CAMPAIGN);
                    liOpportunity.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.SALES_OPPORTUNITY);
                    liContacts.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.SALES_CONTACTS);
                    liQuote.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.SALES_QUOTE);
                }

                //SERVICE
                liService.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.SERVICE);
                liAddEditCallStatus.Visible = false;
                liDocketEntry.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.DOCKETENTRY);
                liTonnerRequestEntry.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.TONNERREQUESTENTRY);
                liTonerRequestApproval.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.TONNER_REQUEST_APPROVAL);
                liServiceBook.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.SERVICEBOOK);
                liAddEditCustomer.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.ADDEDITCUSTOMER);
                liTagCustomerModel.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.TAGCUSTOMERMODEL);
                liServiceBookApproval.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.SERVICE_BOOK_SPARE_APPROVAL);
                liCustomerPurchaseAssignEngineer.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.ASSIGN_ENGINEER_BULK);

                //REPORT
                liReport.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.REPORT);
                liCustomerList.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST);
                liContractStatus.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.CONTRACT_STATUS_REPORT);
                liTonnerRequestList.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.TONER_REQUEST_LIST);
                liDocketList.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.DOCKET_LIST);
                liServiceBookReport.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.SERVICE_BOOK_LIST);
                liSpareTonerUsage.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.SPARE_TONER_USAGE_LIST);

            }
        }

        private void Attendance_GetByEmployeeId()
        {
            try
            {
                Business.HR.Attendance objAttendance = new Business.HR.Attendance();
                DataTable dt = objAttendance.Attendance_GetByEmployeeId(Convert.ToInt32(HttpContext.Current.User.Identity.Name), DateTime.UtcNow.AddHours(5).AddMinutes(33));
                if (dt != null && dt.AsEnumerable().Any())
                {
                    ShowLateNotification(dt);

                    if (dt.Rows[0]["OutDateTime"] != null && !string.IsNullOrEmpty(dt.Rows[0]["OutDateTime"].ToString()))
                    {
                        lnkAttendaceLogin.Visible = true;
                        lnkAttendaceLogout.Visible = false;
                    }
                    else
                    {
                        lnkAttendaceLogin.Visible = false;
                        lnkAttendaceLogout.Visible = true;
                    }
                }
                else
                {
                    lnkAttendaceLogin.Visible = true;
                    lnkAttendaceLogout.Visible = false;
                }
            }
            catch (Exception ex)
            { }
        }

        private void IndividualLoyalityPoint_ByEmployeeId()
        {
            DataTable dtEmployeePoint = new Business.HR.EmployeeLoyaltyPoint().IndividualLoyalityPoint_ByEmployeeId(int.Parse(HttpContext.Current.User.Identity.Name));
            int totalPoint = 0;

            if (DateTime.Now.Month == 1 || DateTime.Now.Month == 2 || DateTime.Now.Month == 3)
                totalPoint = new Business.HR.EmployeeLoyaltyPoint().CalculateLoyalityPointFromJanuary(dtEmployeePoint);
            else
                totalPoint = new Business.HR.EmployeeLoyaltyPoint().CalculateLoyalityPointBeforeJanuary(dtEmployeePoint);
            lblLoyalityPoint.InnerText = string.Concat("(LP:", totalPoint, ")");
        }

        private void CheckAttendanceBlocked()
        {
            DataTable dtLeaveApplicationDetails = new Business.LeaveManagement.LeaveApplication().LeaveApplicationDetails_GetByDate(new Entity.LeaveManagement.LeaveApplicationMaster()
            {
                RequestorId = Convert.ToInt32(HttpContext.Current.User.Identity.Name),
                FromLeaveDate = DateTime.Now.Date,
                ToLeaveDate = DateTime.Now.Date,
                LeaveStatuses = Convert.ToString((int)LeaveStatusEnum.Approved)
            });
            if (dtLeaveApplicationDetails != null && dtLeaveApplicationDetails.AsEnumerable().Any())
            {
                liAttendance.Visible = false;
            }
            else
            {
                liAttendance.Visible = true;
            }
        }

        private void ShowLateNotification(DataTable dtAttendance)
        {
            if (dtAttendance != null && dtAttendance.AsEnumerable().Any())
            {
                if (Convert.ToBoolean(dtAttendance.Rows[0]["IsLate"].ToString()) && Convert.ToBoolean(dtAttendance.Rows[0]["IsLateReduced"].ToString()))
                {
                    lblAttendanceLate.Text = "LATE (LEAVE DEDCUTED)";
                }
                else if (Convert.ToBoolean(dtAttendance.Rows[0]["IsLate"].ToString()))
                {
                    lblAttendanceLate.Text = "LATE";
                }
                else if (Convert.ToBoolean(dtAttendance.Rows[0]["IsHalfday"].ToString()))
                {
                    lblAttendanceLate.Text = "HALF-DAY";
                }
                else
                {
                    lblAttendanceLate.Text = string.Empty;
                }
            }
            else
            {
                liAttendance.Visible = true;
            }
        }

        protected void lnkBtnApp_Click(object sender, EventArgs e)
        {
            Response.ContentType = "application/apk";
            Response.AppendHeader("Content-Disposition", "attachment; filename=aegis.aegis.apk");
            Response.TransmitFile(Server.MapPath("~/dist/aegis.aegis.apk"));
            Response.End();
        }
    }
}