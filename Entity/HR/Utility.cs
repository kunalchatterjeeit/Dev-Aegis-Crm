using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.HR
{
    public static class Utility
    {
        //Control Panel
        public const string CONTROLPANEL = "600";
        public const string SERVICECALLATTENDANCEMANAGER = "601";

        //Settings
        public const string SETTINGS = "100";
        public const string ADDEDITCITY = "101";
        public const string ADDEDITCONTRACTTYPE = "102";
        public const string ADDEDITBRAND = "103";
        public const string ADDEDITMODELCATEGORY = "104";
        public const string ADDEDITMODEL = "105";
        public const string ADDEDITSPARE = "106";
        public const string MODELSPAREMAPPING = "107";

        //HR
        public const string HR = "200";
        public const string ADDEDITROLE = "201";
        public const string MANAGEROLEACCESS = "202";
        public const string ADDEDITEMPLOYEE = "203";
        public const string ADDEDITLOYALTYPOINTREASONMASTER = "204";
        public const string EMPLOYEELOYALTYPOINT = "205";
        public const string HOLIDAY_PROFILE = "206";
        public const string HOLIDAY = "207";
        public const string EMPLOYEE_HOLIDAY_PROFILE_MAPPING = "208";
        public const string HOLIDAY_LIST = "209";
        public const string ATTENDANCE_LIST = "210";



        //Inventory
        public const string INVENTORY = "300";
        public const string PURCHASE_ENTRY = "301";
        public const string ADD_EDIT_VENDOR = "302";
        public const string VENDOR_LIST = "303";
        public const string PURCHASE_LIST = "304";
        public const string STOCK_LOOKUP = "305";
        public const string PURCHASE_REQUISITION_ENTRY = "306";
        public const string SALE_CHALLAN_ENTRY = "307";
        public const string SALE_CHALLAN_LIST = "308";



        //Service
        public const string SERVICE = "400";
        public const string DOCKETENTRY = "403";
        public const string TONNERREQUESTENTRY = "405";
        public const string SERVICEBOOK = "407";
        public const string ADDEDITCUSTOMER = "401";
        public const string TAGCUSTOMERMODEL = "402";
        public const string DOCKET_LIST_SHOW_ALL = "408";
        public const string TONNER_REQUEST_LIST_SHOW_ALL = "409";
        public const string DOCKET_QUICK_LINK_PERMISSION = "410";
        public const string TONNER_QUICK_LINK_PERMISSION = "411";
        public const string CUSTOMER_LIST_SHOW_ALL = "412";
        public const string TONNER_REQUEST_APPROVAL = "413";
        public const string CALL_TRANSFER = "414";
        public const string SERVICE_BOOK_SPARE_APPROVAL = "415";
        public const string ASSIGN_ENGINEER_BULK = "416";


        //Reports
        public const string REPORT = "500";
        public const string CUSTOMER_LIST = "501";
        public const string CONTRACT_STATUS_REPORT = "502";
        public const string EMPLOYEE_LIST = "503";
        public const string TONER_REQUEST_LIST = "504";
        public const string DOCKET_LIST = "505";
        public const string SERVICE_BOOK_LIST = "506";
        public const string SPARE_TONER_USAGE_LIST = "507";

        //LeaveManagement
        public const string LEAVE_MANAGEMENT = "700";
        public const string LEAVE_CONFIGURATION = "701";
        public const string LEAVE_DESIGNATION_CONFIGURATION = "702";
        public const string LEAVE_APPLY = "703";
        public const string LEAVE_APPROVE = "704";
        public const string LEAVE_APPLICATION_LIST = "705";
        public const string LEAVE_GENERATE = "706";
        public const string LEAVE_ADJUSTMENT = "707";
        public const string LEAVE_REPORT = "708";

        //Sales
        public const string SALES = "800";
        public const string SALES_CALLS = "801";
        public const string SALES_MEETINGS = "802";
        public const string SALES_NOTES = "803";
        public const string SALES_TASKS = "804";
        public const string SALES_ACCOUNTS = "805";
        public const string SALES_LEADS = "806";

        //Dashboard Settings
        public const string DASHBOARD_DOCKET_LIST = "10001";
        public const string DASHBOARD_TONER_LIST = "10002";
        public const string DASHBOARD_CONTRACT_STATUS_CHART = "10003";
        public const string DASHBOARD_CONTRACT_EXPIRING_LIST = "10004";
        public const string DASHBOARD_CONTRACT_EXPIRED_LIST = "10005";
    }
}
