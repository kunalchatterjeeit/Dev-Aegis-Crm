using Entity.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;

namespace Business.Common
{
    public static class Context
    {
        public static long ProductId
        {
            get
            {
                return (HttpContext.Current.Session["ProductId"] != null) ? Convert.ToInt64(HttpContext.Current.Session["ProductId"].ToString()) : 0;
            }
            set
            {
                HttpContext.Current.Session["ProductId"] = value;
            }
        }

        public static DataTable SelectedAssets
        {
            get
            {
                return (HttpContext.Current.Session["SelectedAssets"] != null) ? (DataTable)HttpContext.Current.Session["SelectedAssets"] : new DataTable();
            }
            set
            {
                HttpContext.Current.Session["SelectedAssets"] = value;
            }
        }

        public static string Signature
        {
            get
            {
                return (HttpContext.Current.Session["Signature"] != null) ? HttpContext.Current.Session["Signature"].ToString() : string.Empty;
            }
            set
            {
                HttpContext.Current.Session["Signature"] = value;
            }
        }

        public static long CallId
        {
            get
            {
                return (HttpContext.Current.Session["CallId"] != null) ? Convert.ToInt64(HttpContext.Current.Session["CallId"].ToString()) : 0;
            }
            set
            {
                HttpContext.Current.Session["CallId"] = value;
            }
        }

        public static CallType CallType
        {
            get
            {
                CallType calltype = CallType.None;

                if ((HttpContext.Current.Session["CallType"] != null) && Enum.TryParse(HttpContext.Current.Session["CallType"].ToString(), out calltype))
                {
                    return calltype;
                }
                else
                {
                    return calltype;
                }
            }
            set
            {
                HttpContext.Current.Session["CallType"] = value;
            }
        }

        public static string CallStatus
        {
            get
            {
                return (HttpContext.Current.Session["CallStatus"] != null) ? HttpContext.Current.Session["CallStatus"].ToString() : string.Empty;
            }
            set
            {
                HttpContext.Current.Session["CallStatus"] = value;
            }
        }

        public static long ServiceBookId
        {
            get
            {
                return (HttpContext.Current.Session["ServiceBookId"] != null) ? Convert.ToInt64(HttpContext.Current.Session["ServiceBookId"].ToString()) : 0;
            }
            set
            {
                HttpContext.Current.Session["ServiceBookId"] = value;
            }
        }

        public static DataTable SpareRequisition
        {
            get
            {
                return (HttpContext.Current.Session["SpareRequisition"] != null) ? (DataTable)HttpContext.Current.Session["SpareRequisition"] : new DataTable();
            }
            set
            {
                HttpContext.Current.Session["SpareRequisition"] = value;
            }
        }

        public static DataSet DocketList
        {
            get
            {
                return (HttpContext.Current.Session["DocketList"] != null) ? (DataSet)HttpContext.Current.Session["DocketList"] : new DataSet();
            }
            set
            {
                HttpContext.Current.Session["DocketList"] = value;
            }
        }

        public static DataSet TonerList
        {
            get
            {
                return (HttpContext.Current.Session["TonerList"] != null) ? (DataSet)HttpContext.Current.Session["TonerList"] : new DataSet();
            }
            set
            {
                HttpContext.Current.Session["TonerList"] = value;
            }
        }

        public static DataSet ContractStatusList
        {
            get
            {
                return (HttpContext.Current.Session["ContractStatusList"] != null) ? (DataSet)HttpContext.Current.Session["ContractStatusList"] : new DataSet();
            }
            set
            {
                HttpContext.Current.Session["ContractStatusList"] = value;
            }
        }

        public static DataSet ContractExpiredList
        {
            get
            {
                return (HttpContext.Current.Session["ContractExpiredList"] != null) ? (DataSet)HttpContext.Current.Session["ContractExpiredList"] : new DataSet();
            }
            set
            {
                HttpContext.Current.Session["ContractExpiredList"] = value;
            }
        }

        public static DataSet ContractExpiringList
        {
            get
            {
                return (HttpContext.Current.Session["ContractExpiringList"] != null) ? (DataSet)HttpContext.Current.Session["ContractExpiringList"] : new DataSet();
            }
            set
            {
                HttpContext.Current.Session["ContractExpiringList"] = value;
            }
        }

        public static List<DateTime> SelectedDates
        {
            get
            {
                return (HttpContext.Current.Session["SelectedDates"] != null) ? (List<DateTime>)HttpContext.Current.Session["SelectedDates"] : new List<DateTime>();
            }
            set
            {
                HttpContext.Current.Session["SelectedDates"] = value;
            }
        }

        public static string DocketNo
        {
            get
            {
                return (HttpContext.Current.Session["DocketNo"] != null) ? HttpContext.Current.Session["DocketNo"].ToString() : string.Empty;
            }
            set
            {
                HttpContext.Current.Session["DocketNo"] = value;
            }
        }

        public static int LeaveApplicationId
        {
            get
            {
                return (HttpContext.Current.Session["LeaveApplicationId"] != null) ? Convert.ToInt32(HttpContext.Current.Session["LeaveApplicationId"].ToString()) : 0;
            }
            set
            {
                HttpContext.Current.Session["LeaveApplicationId"] = value;
            }
        }
    }
}
