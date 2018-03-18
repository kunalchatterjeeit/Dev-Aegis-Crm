using Entity.Service;
using System;
using System.Data;
using System.Web;

namespace Business.Common
{
    public static class Context
    {
        public static Int64 ProductId
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

        public static Int64 CallId
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
    }
}
