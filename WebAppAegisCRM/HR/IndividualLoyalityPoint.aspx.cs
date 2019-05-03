using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.HR
{
    public partial class IndividualLoyalityPoint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IndividualLoyalityPoint_ByEmployeeId();
        }

        private void IndividualLoyalityPoint_ByEmployeeId()
        {
            DataTable dtEmployeePoint = new Business.HR.EmployeeLoyaltyPoint().IndividualLoyalityPoint_ByEmployeeId(int.Parse(HttpContext.Current.User.Identity.Name));
            
            var filteredPoint = dtEmployeePoint.AsEnumerable().Where(row
                       => row["Year"].ToString() == DateTime.Now.Year.ToString());
            if (filteredPoint.Any())
            {
                gvLoyalityPoint.DataSource = filteredPoint.CopyToDataTable().Rows;
            }
            else
                gvLoyalityPoint.DataSource = null;
            gvLoyalityPoint.DataBind();
        }
    }
}