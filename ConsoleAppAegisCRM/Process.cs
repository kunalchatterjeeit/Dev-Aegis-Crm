using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppAegisCRM
{
    public class Process
    {
        public DataTable GetCustomerPurchaseForPMCall()
        {
            Business.Service.ServiceBook objServiceBook = new Business.Service.ServiceBook();
            return objServiceBook.Service_GetCustomerPurchaseForPMCall(DateTime.Now);
        }

        public int Service_Amcv_Calculate_Save(long customerPurchaseId)
        {
            Business.Service.ServiceBook objServiceBook = new Business.Service.ServiceBook();
            return objServiceBook.Service_Amcv_Calculate_Save(customerPurchaseId, DateTime.Now);
        }

        public void Execute(ILogger logger)
        {
            logger.WriteLog(PrepareLogging("Execution started..."));
            DataTable dtCustomerPurchase = GetCustomerPurchaseForPMCall();
            if (dtCustomerPurchase != null && dtCustomerPurchase.Rows.Count > 0)
            {
                foreach (DataRow dr in dtCustomerPurchase.Rows)
                {
                    logger.WriteLog(PrepareLogging(string.Concat("Exectuing Customer Purchase Id: ", dr["CustomerPurchaseId"].ToString())));
                    Service_Amcv_Calculate_Save(Convert.ToInt64(dr["CustomerPurchaseId"].ToString()));
                }
                logger.WriteLog(PrepareLogging("Execution completed..."));
            }
            else
            {
                logger.WriteLog(PrepareLogging("No machine eligible for execution..."));
            }
            Console.ReadLine();
        }

        public string PrepareLogging(string rawContent)
        {
            return string.Format("DateTime:{0}, {1}", DateTime.Now, rawContent);
        }
    }
}
