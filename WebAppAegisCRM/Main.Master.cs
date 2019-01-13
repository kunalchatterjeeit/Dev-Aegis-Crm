using System;
using System.Web;

namespace WebAppAegisCRM
{
    public partial class Main : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                Response.Redirect("~/MainLogout.aspx");

            if (!IsPostBack)
            {
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

                //HR
                liHR.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.HR);
                liAddEditRole.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.ADDEDITROLE);
                liManageRoleAccess.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.MANAGEROLEACCESS);
                liLoyaltyPointReasonMaster.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.ADDEDITLOYALTYPOINTREASONMASTER);
                liEmployeeLoyaltyPoint.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.EMPLOYEELOYALTYPOINT);
                liHolidayProfile.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.HOLIDAY_PROFILE);
                liHoliday.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.HOLIDAY);

                //LEAVE MANAGEMENT
                liLeaveApplicationList.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.LEAVE_APPLICATION_LIST);
                liLeaveApply.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.LEAVE_APPLY);
                liLeaveApprove.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.LEAVE_APPROVE);
                liLeaveConfiguration.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.LEAVE_CONFIGURATION);
                liLeaveDesignationConfiguration.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.LEAVE_DESIGNATION_CONFIGURATION);
                liLeaveManagement.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.LEAVE_MANAGEMENT);
                liLeaveGenerate.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.LEAVE_GENERATE);

                //INVENTORY
                liInventory.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.INVENTORY);
                liPurchaseEntry.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.PURCHASE_ENTRY);
                liPurchaseList.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.PURCHASE_LIST);
                liStockLookup.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.STOCK_LOOKUP);
                liVendorMaster.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.ADD_EDIT_VENDOR);
                liVendorList.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.VENDOR_LIST);
                liPurchaseRequisitionEntry.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.PURCHASE_REQUISITION_ENTRY);

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

                //REPORT
                liReport.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.REPORT);
                liCustomerList.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST);
                liContractStatus.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.CONTRACT_STATUS_REPORT);
                liTonnerRequestList.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.TONER_REQUEST_LIST);
                liDocketList.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.DOCKET_LIST);
                liServiceBookReport.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.SERVICE_BOOK_LIST);
            }
        }
    }
}