using Business.Common;
using log4net;
using System;
using System.Data;
using System.Linq;
using System.Web;

namespace WebAppAegisCRM.HR
{
    public partial class IndividualLoyalityPoint : System.Web.UI.Page
    {
        ILog logger = log4net.LogManager.GetLogger("ErrorLog");
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                IndividualLoyalityPoint_ByEmployeeId();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
            }
        }
        private void IndividualLoyalityPoint_ByEmployeeId()
        {
            DataTable dtEmployeePoint = new Business.HR.EmployeeLoyaltyPoint().IndividualLoyalityPoint_ByEmployeeId(int.Parse(HttpContext.Current.User.Identity.Name));
            DataTable dtList = dtEmployeePoint.Clone();

            if (DateTime.Now.Month == 1 || DateTime.Now.Month == 2 || DateTime.Now.Month == 3)
            {
                foreach (DataRow drItem in new Business.HR.EmployeeLoyaltyPoint().LoyalityPointFromJanuary(dtEmployeePoint).Rows)
                {
                    dtList.ImportRow(drItem);
                }
            }
            else
            {
                foreach (DataRow drItem in new Business.HR.EmployeeLoyaltyPoint().LoyalityPointBeforeJanuary(dtEmployeePoint).Rows)
                {
                    dtList.ImportRow(drItem);
                }
            }
            if (dtList.AsEnumerable().Any())
            {
                gvLoyalityPoint.DataSource = dtList;
            }
            else
                gvLoyalityPoint.DataSource = null;
            gvLoyalityPoint.DataBind();
        }
    }
}