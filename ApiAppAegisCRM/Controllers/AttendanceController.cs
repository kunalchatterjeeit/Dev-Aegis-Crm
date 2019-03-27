using ApiAppAegisCRM.Models;
using Business.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiAppAegisCRM.Controllers
{
    public class AttendanceController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GetAttendanceState([FromBody]AttendanceModel model)
        {
            HttpResponseMessage retValue = null;
            using (retValue = new HttpResponseMessage(HttpStatusCode.InternalServerError))
            {
                try
                {
                    if (model != null)
                    {
                        model.CurrentState = Attendance_GetByEmployeeId(Convert.ToInt32(model.UserId));
                        model.ResponseCode = 200;
                        retValue = Request.CreateResponse(HttpStatusCode.OK, model);
                    }
                }
                catch (Exception ex)
                {
                    ex.WriteException();
                    retValue = Request.CreateResponse(HttpStatusCode.OK, ex.Message);
                }
                return retValue;
            }
        }

        [HttpPost]
        public HttpResponseMessage MakeAttendance([FromBody]AttendanceModel model)
        {
            HttpResponseMessage retValue = null;
            using (retValue = new HttpResponseMessage(HttpStatusCode.InternalServerError))
            {
                try
                {
                    if (model != null)
                    {
                        SaveAttendance(model);
                        model.IsAttendanceBlocked = false;
                        model.ResponseCode = 200;
                        retValue = Request.CreateResponse(HttpStatusCode.OK, model);
                    }
                }
                catch (Exception ex)
                {
                    ex.WriteException();
                    retValue = Request.CreateResponse(HttpStatusCode.OK, ex.Message);
                }
                return retValue;
            }
        }

        private string Attendance_GetByEmployeeId(int userId)
        {
            try
            {
                Business.HR.Attendance objAttendance = new Business.HR.Attendance();
                DataTable dt = objAttendance.Attendance_GetByEmployeeId(userId, DateTime.Now.Date.AddMinutes(333));
                if (dt != null && dt.AsEnumerable().Any())
                {
                    if (dt.Rows[0]["OutDateTime"] != null && !string.IsNullOrEmpty(dt.Rows[0]["OutDateTime"].ToString()))
                    {
                        return "OUT";
                    }
                    else
                    {
                        return "IN";
                    }
                }
                else
                {
                    return "OUT";
                }
            }
            catch { }
            return "OUT";
        }

        private void SaveAttendance(AttendanceModel model)
        {
            if (model.AttendanceMode.ToLower().Equals("in"))
            {
                Business.HR.Attendance objAttendance = new Business.HR.Attendance();
                Entity.HR.Attendance attendance = new Entity.HR.Attendance()
                {
                    AttendanceDate = DateTime.Now.Date.AddMinutes(333),
                    InDateTime = DateTime.Now.AddMinutes(333),
                    OutDateTime = DateTime.Now.AddMinutes(333),
                    EmployeeId = Convert.ToInt32(model.UserId),
                    CreatedBy = Convert.ToInt32(model.UserId),
                    TotalHours = 0,
                    Latitude = model.Latitude,
                    Longitude = model.Longitude
                };
                objAttendance.Attendance_Save(attendance);
            }
            else if (model.AttendanceMode.ToLower().Equals("out"))
            {
                Business.HR.Attendance objAttendance = new Business.HR.Attendance();
                DataTable dt = objAttendance.Attendance_GetByEmployeeId(Convert.ToInt32(model.UserId), DateTime.Now.Date.AddMinutes(333));
                if (dt != null && dt.AsEnumerable().Any())
                {
                    Entity.HR.Attendance attendance = new Entity.HR.Attendance()
                    {
                        AttendanceId = Convert.ToInt64(dt.Rows[0]["AttendanceId"].ToString()),
                        AttendanceDate = Convert.ToDateTime(dt.Rows[0]["AttendanceDate"].ToString()),
                        InDateTime = Convert.ToDateTime(dt.Rows[0]["InDateTime"].ToString()),
                        OutDateTime = DateTime.Now.AddMinutes(333),
                        CreatedBy = Convert.ToInt32(model.UserId),
                        TotalHours = (DateTime.Now.AddMinutes(333) - Convert.ToDateTime(dt.Rows[0]["InDateTimeRaw"].ToString())).TotalMinutes,
                        Latitude = model.Latitude,
                        Longitude = model.Longitude
                    };
                    objAttendance.Attendance_Save(attendance);
                }

            }
        }
    }
}
