using Business.Common;
using Entity.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;

namespace WebAppAegisCRM.WebServices
{
    /// <summary>
    /// Summary description for WebServiceChart
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class WebServiceChart : System.Web.Services.WebService
    {

        [WebMethod(EnableSession = true)]
        public List<MachineResponseTime> ColumnChartData()
        {
            Business.Customer.Customer objCustomerMaster = new Business.Customer.Customer();
            int customerMasterId = int.Parse(HttpContext.Current.User.Identity.Name.Split('|')[(int)Constants.Customer.ID]);
            DataTable dt = objCustomerMaster.CustomerPurchase_GetByCustomerId(customerMasterId);
            List<MachineResponseTime> chartList = new List<MachineResponseTime>();
            foreach (DataRow dr in dt.Rows)
            {
                chartList.Add(new MachineResponseTime { MachineId = dr["MachineId"].ToString(), Avg_Response_Time = Convert.ToDecimal(dr["AVG_Response_Time"].ToString()) });
            }
            return chartList;
        }
    }
}
