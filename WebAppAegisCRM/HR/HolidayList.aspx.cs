using System;
using System.Data;

namespace WebAppAegisCRM.HR
{
    public partial class HolidayList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadHolidayList();
        }

        private void LoadHolidayList()
        {
            Business.HR.Holiday objHoliday = new Business.HR.Holiday();
            DataTable dt = objHoliday.Holiday_GetAll(new Entity.HR.Holiday());
            if (dt != null)
            {
                gvHoliday.DataSource = dt.Select("Show = 1").CopyToDataTable();
                gvHoliday.DataBind();
            }
        }
    }
}