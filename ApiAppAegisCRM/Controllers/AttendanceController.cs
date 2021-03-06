﻿using ApiAppAegisCRM.Models;
using Business.Common;
using Entity.Common;
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
                        model = Attendance_GetByEmployeeId(model);
                        retValue = Request.CreateResponse(HttpStatusCode.OK, model);
                    }
                }
                catch (Exception ex)
                {
                    new Logger().LogException(ex, "GetAttendanceState");
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
                        model = SaveAttendance(model);
                        retValue = Request.CreateResponse(HttpStatusCode.OK, model);
                    }
                }
                catch (Exception ex)
                {
                    new Logger().LogException(ex, "MakeAttendance");
                    retValue = Request.CreateResponse(HttpStatusCode.OK, ex.Message);
                }
                return retValue;
            }
        }

        private AttendanceModel Attendance_GetByEmployeeId(AttendanceModel model)
        {
            model.ResponseCode = 99;
            try
            {
                Business.HR.Attendance objAttendance = new Business.HR.Attendance();
                DataTable dt = objAttendance.Attendance_GetByEmployeeId(Convert.ToInt32(model.UserId), DateTime.UtcNow.AddHours(5).AddMinutes(33));
                if (dt != null && dt.AsEnumerable().Any())
                {
                    if (dt.Rows[0]["OutDateTime"] != null && !string.IsNullOrEmpty(dt.Rows[0]["OutDateTime"].ToString()))
                    {
                        model.CurrentState = "OUT";
                    }
                    else
                    {
                        model.CurrentState = "IN";
                    }
                }
                else
                {
                    model.CurrentState = "OUT";
                }
                model.ResponseCode = 200;
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex, "Attendance_GetByEmployeeId");
                model.Message = ex.Message;
            }
            model.Message = "OUT";

            return model;
        }

        private bool CheckAttendanceBlocked(AttendanceModel model)
        {
            DataTable dtLeaveApplicationDetails = new Business.LeaveManagement.LeaveApplication().LeaveApplicationDetails_GetByDate(new Entity.LeaveManagement.LeaveApplicationMaster()
            {
                RequestorId = Convert.ToInt32(model.UserId),
                FromLeaveDate = DateTime.Now.Date,
                ToLeaveDate = DateTime.Now.Date,
                LeaveStatuses = Convert.ToString((int)LeaveStatusEnum.Approved)
            });
            if (dtLeaveApplicationDetails == null || !dtLeaveApplicationDetails.AsEnumerable().Any())
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private AttendanceModel SaveAttendance(AttendanceModel model)
        {
            model.ResponseCode = 99;
            try
            {
                if (!CheckAttendanceBlocked(model))
                {
                    if (model.AttendanceMode.ToLower().Equals("out"))
                    {
                        Business.HR.Attendance objAttendance = new Business.HR.Attendance();
                        Entity.HR.Attendance attendance = new Entity.HR.Attendance()
                        {
                            AttendanceDate = DateTime.UtcNow.AddHours(5).AddMinutes(33),
                            InDateTime = DateTime.UtcNow.AddHours(5).AddMinutes(33),
                            OutDateTime = DateTime.UtcNow.AddHours(5).AddMinutes(33),
                            EmployeeId = Convert.ToInt32(model.UserId),
                            CreatedBy = Convert.ToInt32(model.UserId),
                            TotalHours = 0,
                            Latitude = model.Latitude,
                            Longitude = model.Longitude,
                            Source = "Android"
                        };
                        objAttendance.Attendance_Save(attendance);
                        model.Message = "You are successfully IN.";
                        model.ResponseCode = 200;
                    }
                    else if (model.AttendanceMode.ToLower().Equals("in"))
                    {
                        Business.HR.Attendance objAttendance = new Business.HR.Attendance();
                        DataTable dt = objAttendance.Attendance_GetByEmployeeId(Convert.ToInt32(model.UserId), DateTime.UtcNow.AddHours(5).AddMinutes(33));
                        if (dt != null && dt.AsEnumerable().Any())
                        {
                            Entity.HR.Attendance attendance = new Entity.HR.Attendance()
                            {
                                AttendanceId = Convert.ToInt64(dt.Rows[0]["AttendanceId"].ToString()),
                                AttendanceDate = Convert.ToDateTime(dt.Rows[0]["AttendanceDate"].ToString()),
                                InDateTime = Convert.ToDateTime(dt.Rows[0]["InDateTime"].ToString()),
                                OutDateTime = DateTime.UtcNow.AddHours(5).AddMinutes(33),
                                CreatedBy = Convert.ToInt32(model.UserId),
                                TotalHours = (DateTime.UtcNow.AddHours(5).AddMinutes(33) - Convert.ToDateTime(dt.Rows[0]["InDateTimeRaw"].ToString())).TotalMinutes,
                                Latitude = model.Latitude,
                                Longitude = model.Longitude,
                                Source = "Android"
                            };
                            objAttendance.Attendance_Save(attendance);
                            model.Message = "You are successfully OUT.";
                            model.ResponseCode = 200;
                        }
                        else
                        {
                            model.Message = "Invalid employee id.";
                            model.ResponseCode = 200;
                        }
                    }
                }
                else
                {
                    model.Message = "Your attendance is blocked for today. Please contact HR.";
                    model.ResponseCode = 200;
                }
            }
            catch (Exception ex)
            {
                new Logger().LogException(ex, "SaveAttendance");
                model.ResponseCode = 99;
                model.Message = ex.Message;
            }
            return model;
        }

        [HttpPost]
        public HttpResponseMessage Attendance_CurrentMonth([FromBody]BaseModel model)
        {
            HttpResponseMessage retValue = null;
            using (retValue = new HttpResponseMessage(HttpStatusCode.InternalServerError))
            {
                try
                {
                    if (model != null)
                    {
                        List<Models.AttendanceModel> leaveModel = Attendance_CurrentMonth_GetByEmployeeId(model.UserId);
                        retValue = Request.CreateResponse(HttpStatusCode.OK, leaveModel);
                    }
                }
                catch (Exception ex)
                {
                    new Logger().LogException(ex, "Attendance_CurrentMonth");
                    retValue = Request.CreateResponse(HttpStatusCode.OK, ex.Message);
                }
                return retValue;
            }
        }

        private List<Models.AttendanceModel> Attendance_CurrentMonth_GetByEmployeeId(int employeeId)
        {
            List<Models.AttendanceModel> model = new List<Models.AttendanceModel>();
            DataTable dtAttendance = new Business.HR.Attendance().Attendance_CurrentMonth_GetByEmployeeId(employeeId, DateTime.Now.Month, DateTime.Now.Year);
            if (dtAttendance != null
                && dtAttendance.AsEnumerable().Any())
            {
                foreach (DataRow dr in dtAttendance.Rows)
                {
                    model.Add(new Models.AttendanceModel
                    {
                        AttendanceInDate = string.Format("Attendance In-Date & Time: {0}", dr["InDateTime"].ToString()),
                        AttendanceOutDate = string.Format("Attendance Out-Date & Time: {0}", dr["OutDateTime"].ToString()),
                        TotalWorkingHours = string.Format("Working Hours: {0}", dr["TotalHours"].ToString()),
                        IsLate = string.Format("Late: {0}", dr["IsLate"].ToString()),
                        IsLateReduced = string.Format("Leave reduced: {0}", dr["IsLateReduced"].ToString()),
                        IsHalfDay = string.Format("Half day: {0}", dr["IsHalfday"].ToString())
                    });
                }
            }

            return model;
        }
    }
}
