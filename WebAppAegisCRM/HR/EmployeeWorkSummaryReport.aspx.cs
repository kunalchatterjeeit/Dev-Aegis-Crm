using Business.Common;
using log4net;
using System;

namespace WebAppAegisCRM.HR
{
    public partial class EmployeeWorkSummaryReport : System.Web.UI.Page
    {
        ILog logger = log4net.LogManager.GetLogger("ErrorLog");
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                EmployeeWorkReport();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
            }
        }
        private void EmployeeWorkReport()
        {
            Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
            gvEmployeeWorkReport.DataSource = objEmployeeMaster.EmployeeWorkReport(Convert.ToDateTime(txtFromDate.Text.Trim()), Convert.ToDateTime(txtToDate.Text.Trim()));
            gvEmployeeWorkReport.DataBind();
        }
    }
}