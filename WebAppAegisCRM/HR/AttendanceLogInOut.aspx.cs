using Business.Common;
using Entity.Common;
using System;
using System.Data;
using System.Linq;
using System.Web;

namespace WebAppAegisCRM.HR
{
    public partial class AttendanceLogInOut : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["Log"] != null)
                {
                    if (Request.QueryString["Log"].ToString() == ((int)AttendanceLog.Login).ToString())
                    {
                        Business.HR.Attendance objAttendance = new Business.HR.Attendance();
                        Entity.HR.Attendance attendance = new Entity.HR.Attendance()
                        {
                            AttendanceDate = DateTime.Now.Date.AddMinutes(333),
                            InDateTime = DateTime.Now.AddMinutes(333),
                            OutDateTime = DateTime.Now.AddMinutes(333),
                            EmployeeId = Convert.ToInt32(HttpContext.Current.User.Identity.Name),
                            CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name),
                            TotalHours = 0,
                            Latitude = (Request.QueryString["latitude"] != null) ? Request.QueryString["latitude"].ToString() : string.Empty,
                            Longitude = (Request.QueryString["longitude"] != null) ? Request.QueryString["longitude"].ToString() : string.Empty
                        };
                        objAttendance.Attendance_Save(attendance);
                    }
                    else if (Request.QueryString["Log"].ToString() == ((int)AttendanceLog.Logout).ToString())
                    {
                        Business.HR.Attendance objAttendance = new Business.HR.Attendance();
                        DataTable dt = objAttendance.Attendance_GetByEmployeeId(Convert.ToInt32(HttpContext.Current.User.Identity.Name), DateTime.Now.Date);
                        if (dt != null && dt.AsEnumerable().Any())
                        {
                            Entity.HR.Attendance attendance = new Entity.HR.Attendance()
                            {
                                AttendanceId = Convert.ToInt64(dt.Rows[0]["AttendanceId"].ToString()),
                                AttendanceDate = Convert.ToDateTime(dt.Rows[0]["AttendanceDate"].ToString()),
                                InDateTime = Convert.ToDateTime(dt.Rows[0]["InDateTime"].ToString()),
                                OutDateTime = DateTime.Now.AddMinutes(333),
                                CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name),
                                TotalHours = (DateTime.Now.AddMinutes(333) - Convert.ToDateTime(dt.Rows[0]["InDateTimeRaw"].ToString())).TotalMinutes,
                                Latitude = (Request.QueryString["latitude"] != null) ? Request.QueryString["latitude"].ToString() : string.Empty,
                                Longitude = (Request.QueryString["longitude"] != null) ? Request.QueryString["longitude"].ToString() : string.Empty
                            };
                            objAttendance.Attendance_Save(attendance);
                        }

                    }
                    Response.Redirect(HttpContext.Current.Request.UrlReferrer.AbsoluteUri);
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
            }
        }
    }
}