using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.HR
{
    public partial class EmployeeWorkSummaryReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            EmployeeWorkReport();
        }

        private void EmployeeWorkReport()
        {
            Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
            gvEmployeeWorkReport.DataSource = objEmployeeMaster.EmployeeWorkReport(Convert.ToDateTime(txtFromDate.Text.Trim()), Convert.ToDateTime(txtToDate.Text.Trim()));
            gvEmployeeWorkReport.DataBind();
        }
    }
}