using ApiAppAegisCRM.Models;
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
    public class AccountController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage GetAttendanceState([FromBody]AccountModel model)
        {
            HttpResponseMessage retValue = null;
            using (retValue = new HttpResponseMessage(HttpStatusCode.InternalServerError))
            {
                try
                {
                    if (model != null)
                    {
                        model = AttendanceState_GetByEmployeeId(model);
                        model = EmployeeMaster_ById(model);
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

        private AccountModel EmployeeMaster_ById(AccountModel model)
        {
            try
            {
                Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
                Entity.HR.EmployeeMaster employeeMaster = new Entity.HR.EmployeeMaster();
                employeeMaster.EmployeeMasterId = model.UserId;
                DataTable dtEmployeeMaster = objEmployeeMaster.EmployeeMaster_ById(employeeMaster);

                model.EmployeeName = dtEmployeeMaster.Rows[0]["EmployeeName"].ToString();
                model.ImageProfile = string.Format("http://crm.aegissolutions.in/HR/EmployeeImage/{0}", dtEmployeeMaster.Rows[0]["Image"].ToString());
                model.Designation = dtEmployeeMaster.Rows[0]["DesignationName"].ToString();
                model.ReportsTo = dtEmployeeMaster.Rows[0]["ReportingPersion"].ToString();
                model.LoyaltyPoint = IndividualLoyalityPoint_ByEmployeeId(model.UserId);
                model.LastLogin = GetLastLogin(model.UserId);
            }
            catch (Exception ex)
            {
                ex.WriteException();
            }
            return model;
        }

        private string GetLastLogin(int userId)
        {
            DataTable dtLastLogin = new Business.Common.Login().GetLastLogin(userId);
            string retValue = string.Empty;
            if (dtLastLogin != null && dtLastLogin.Rows.Count > 0)
                retValue = dtLastLogin.Rows[0]["INDIATIME"].ToString();
            return retValue;
        }

        private decimal IndividualLoyalityPoint_ByEmployeeId(int userId)
        {
            DataTable dtEmployeePoint = new Business.HR.EmployeeLoyaltyPoint().IndividualLoyalityPoint_ByEmployeeId(userId);
            decimal totalPoint = 0;
            if (DateTime.Now.Month == 1 || DateTime.Now.Month == 2 || DateTime.Now.Month == 3)
                totalPoint = new Business.HR.EmployeeLoyaltyPoint().CalculateLoyalityPointFromJanuary(dtEmployeePoint);
            else
                totalPoint = new Business.HR.EmployeeLoyaltyPoint().CalculateLoyalityPointBeforeJanuary(dtEmployeePoint);
            return totalPoint;
        }

        private AccountModel AttendanceState_GetByEmployeeId(AccountModel model)
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
                        model.AttendanceState = "OUT";
                    }
                    else
                    {
                        model.AttendanceState = "IN";
                    }
                }
                else
                {
                    model.AttendanceState = "OUT";
                }
                model.ResponseCode = 200;
            }
            catch (Exception ex)
            {
                model.Message = ex.Message;
            }
            model.Message = "OUT";

            return model;
        }

        [HttpPost]
        public HttpResponseMessage GetHolidayList([FromBody]BaseModel model)
        {
            HttpResponseMessage retValue = null;
            using (retValue = new HttpResponseMessage(HttpStatusCode.InternalServerError))
            {
                try
                {
                    if (model != null)
                    {
                        List<Models.HolidayModel> holidayModel = LoadHolidayList(model.UserId);
                        retValue = Request.CreateResponse(HttpStatusCode.OK, holidayModel);
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

        private List<Models.HolidayModel> LoadHolidayList(int employeeId)
        {
            List<Models.HolidayModel> model = new List<Models.HolidayModel>();
            int holidayProfileId = 0;
            DataTable dtEmployeeHolidayProfileMapping = new Business.HR.HolidayProfile().EmployeeHolidayProfileMapping_GetByEmployeeId(employeeId);
            if (dtEmployeeHolidayProfileMapping != null
                && dtEmployeeHolidayProfileMapping.AsEnumerable().Any())
            {
                holidayProfileId = Convert.ToInt32(dtEmployeeHolidayProfileMapping.Rows[0]["HolidayProfileId"].ToString());
            }

            Business.HR.Holiday objHoliday = new Business.HR.Holiday();

            DataTable dt = objHoliday.Holiday_GetAll(new Entity.HR.Holiday()
            {
                HolidayYear = DateTime.Now.Year,
                HolidayProfileId = holidayProfileId
            });
            if (dt != null)
            {
                dt = dt.Select("Show = 1").CopyToDataTable();
                dt.AcceptChanges();

                foreach (DataRow dr in dt.Rows)
                {
                    model.Add(new Models.HolidayModel
                    {
                        HolidayName = dr["HolidayName"].ToString(),
                        HolidayDate = dr["HolidayDate"].ToString()
                    });
                }
            }
            return model;
        }

        [HttpPost]
        public HttpResponseMessage GetUpcomingLeave([FromBody]BaseModel model)
        {
            HttpResponseMessage retValue = null;
            using (retValue = new HttpResponseMessage(HttpStatusCode.InternalServerError))
            {
                try
                {
                    if (model != null)
                    {
                        List<Models.LeaveModel> leaveModel = GetUpcomingLeave(model.UserId);
                        retValue = Request.CreateResponse(HttpStatusCode.OK, leaveModel);
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

        private List<Models.LeaveModel> GetUpcomingLeave(int employeeId)
        {
            List<Models.LeaveModel> model = new List<Models.LeaveModel>();
            DataTable dtUpcomingLeave = new Business.LeaveManagement.LeaveApplication().GetUpcomingLeave(employeeId, DateTime.UtcNow.AddHours(5).AddMinutes(33));
            if (dtUpcomingLeave != null
                && dtUpcomingLeave.AsEnumerable().Any())
            {
                foreach (DataRow dr in dtUpcomingLeave.Rows)
                {
                    model.Add(new Models.LeaveModel
                    {
                        LeaveType = dr["LeaveTypeName"].ToString(),
                        LeaveDuration = dr["LeaveDuration"].ToString()
                    });
                }
            }

            return model;
        }

        [HttpPost]
        public HttpResponseMessage GetPendingClaim([FromBody]BaseModel model)
        {
            HttpResponseMessage retValue = null;
            using (retValue = new HttpResponseMessage(HttpStatusCode.InternalServerError))
            {
                try
                {
                    if (model != null)
                    {
                        List<Models.ClaimModel> leaveModel = GetPendingClaim(model.UserId);
                        retValue = Request.CreateResponse(HttpStatusCode.OK, leaveModel);
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

        private List<Models.ClaimModel> GetPendingClaim(int employeeId)
        {
            List<Models.ClaimModel> model = new List<Models.ClaimModel>();
            DataTable dtPendingClaim = new Business.ClaimManagement.ClaimApplication().ClaimApplication_GetAll(
                new Entity.ClaimManagement.ClaimApplicationMaster()
                {
                    EmployeeId = employeeId,
                    Status = (int)ClaimStatusEnum.Pending
                }
                );
            if (dtPendingClaim != null
                && dtPendingClaim.AsEnumerable().Any())
            {
                foreach (DataRow dr in dtPendingClaim.Rows)
                {
                    model.Add(new Models.ClaimModel
                    {
                        ClaimNo = dr["ClaimNo"].ToString(),
                        ClaimHeading = dr["ClaimHeading"].ToString(),
                        ClaimDateTime = dr["ClaimDateTime"].ToString(),
                        PeriodFrom = dr["PeriodFrom"].ToString(),
                        PeriodTo = dr["PeriodTo"].ToString(),
                        StatusName = dr["StatusName"].ToString(),
                        TotalAmount = Convert.ToDecimal(dr["TotalAmount"].ToString())
                    });
                }
            }

            return model;
        }

        [HttpPost]
        public HttpResponseMessage GetLoyalityPoint([FromBody]BaseModel model)
        {
            HttpResponseMessage retValue = null;
            using (retValue = new HttpResponseMessage(HttpStatusCode.InternalServerError))
            {
                try
                {
                    if (model != null)
                    {
                        List<Models.LoyalityPointModel> leaveModel = GetLoyalityPoint(model.UserId);
                        retValue = Request.CreateResponse(HttpStatusCode.OK, leaveModel);
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

        private List<Models.LoyalityPointModel> GetLoyalityPoint(int employeeId)
        {
            List<Models.LoyalityPointModel> model = new List<Models.LoyalityPointModel>();
            DataTable dtEmployeePoint = new Business.HR.EmployeeLoyaltyPoint().IndividualLoyalityPoint_ByEmployeeId(employeeId);
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
            if (dtList != null
                && dtList.AsEnumerable().Any())
            {
                foreach (DataRow dr in dtList.Rows)
                {
                    model.Add(new Models.LoyalityPointModel
                    {
                        Month = dr["Month"].ToString(),
                        Year = Convert.ToInt32(dr["Year"].ToString()),
                        Point = Convert.ToDecimal(dr["Point"].ToString()),
                        Reason = dr["Reason"].ToString()
                    });
                }
            }

            return model;
        }
    }
}