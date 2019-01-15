using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.LeaveManagement
{
    public partial class LeaveCanlendar : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            DataTable dtLeaveApplicationMaster =
                new Business.LeaveManagement.LeaveApplication()
                .LeaveApplicationDetails_GetByDate(new Entity.LeaveManagement.LeaveApplicationMaster()
                {
                    FromLeaveDate = new DateTime(DateTime.Now.Year, 1, 1),
                    ToLeaveDate = new DateTime(DateTime.Now.Year, 1, 1).AddYears(1)
                });

            if (e.Day.Date == DateTime.Now.Date)
            {
                e.Cell.ForeColor = System.Drawing.Color.Red;
                e.Cell.Text = e.Day.DayNumberText + "<br>Hello";
            }

            if (Business.Common.Context.SelectedDates.Any())
            {
                foreach (DateTime dt in Business.Common.Context.SelectedDates)
                {
                    Calendar1.SelectedDates.Add(dt);
                }
            }
        }
    }
}