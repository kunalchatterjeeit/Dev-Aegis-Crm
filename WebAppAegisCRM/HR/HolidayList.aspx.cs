using Business.Common;
using log4net;
using System;
using System.Data;
using System.Linq;
using System.Web;

namespace WebAppAegisCRM.HR
{
    public partial class HolidayList : System.Web.UI.Page
    {
        ILog logger = log4net.LogManager.GetLogger("ErrorLog");
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                LoadHolidayList();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
            }
        }
        private void LoadHolidayList()
        {
            int holidayProfileId = 0;
            Business.HR.HolidayProfile objEmployeeHolidayProfileMapping = new Business.HR.HolidayProfile();
            DataTable dtEmployeeHolidayProfileMapping = objEmployeeHolidayProfileMapping.EmployeeHolidayProfileMapping_GetAll(new Entity.HR.EmployeeHolidayProfileMapping());
            if (dtEmployeeHolidayProfileMapping != null
                && dtEmployeeHolidayProfileMapping.AsEnumerable().Any()
                && dtEmployeeHolidayProfileMapping.Select("EmployeeMasterId = " + HttpContext.Current.User.Identity.Name).Any())
            {
                holidayProfileId = Convert.ToInt32(dtEmployeeHolidayProfileMapping
                    .Select("EmployeeMasterId = " + HttpContext.Current.User.Identity.Name).CopyToDataTable()
                    .Rows[0]["HolidayProfileId"].ToString());
            }

            Business.HR.Holiday objHoliday = new Business.HR.Holiday();

            DataTable dt = objHoliday.Holiday_GetAll(new Entity.HR.Holiday()
            {
                HolidayYear = DateTime.Now.Year,
                HolidayProfileId = holidayProfileId
            });
            if (dt != null)
            {
                gvHoliday.DataSource = dt.Select("Show = 1").CopyToDataTable();
                gvHoliday.DataBind();
            }
        }
    }
}