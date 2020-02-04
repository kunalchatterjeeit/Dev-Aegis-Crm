using ApiAppAegisCRM.Models;
using Business.Common;
using Entity.Common;
using Entity.Service;
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
                model.LoyaltyPoint = IndividualLoyalityPoint_ByEmployeeId(model.UserId).ToString();
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
                        ClaimNo = string.Format("Claim No: {0}", dr["ClaimNo"].ToString()),
                        ClaimHeading = string.Format("Claim Header: {0}", dr["ClaimHeading"].ToString()),
                        ClaimDateTime = string.Format("Claim Date: {0}", dr["ClaimDateTime"].ToString()),
                        PeriodFrom = string.Format("Period From: {0}", dr["PeriodFrom"].ToString()),
                        PeriodTo = string.Format("Period To: {0}", dr["PeriodTo"].ToString()),
                        StatusName = string.Format("Claim Status: {0}", dr["StatusName"].ToString()),
                        TotalAmount = string.Format("Claim Amount: {0}", dr["TotalAmount"].ToString())
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
                        Month = string.Format("Assesment Month: {0}", dr["Month"].ToString()),
                        Year = string.Format("Assesment Year: {0}", dr["Year"].ToString()),
                        Point = string.Format("Point: {0}", dr["Point"].ToString()),
                        Reason = string.Format("Reason: ", dr["Reason"].ToString())
                    });
                }
            }

            return model;
        }

        [HttpPost]
        public HttpResponseMessage GetDocket([FromBody]BaseModel model)
        {
            HttpResponseMessage retValue = null;
            using (retValue = new HttpResponseMessage(HttpStatusCode.InternalServerError))
            {
                try
                {
                    if (model != null)
                    {
                        List<Models.DocketModel> docketModel = GetDocket(model.UserId);
                        retValue = Request.CreateResponse(HttpStatusCode.OK, docketModel);
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

        private List<Models.DocketModel> GetDocket(int employeeId)
        {
            List<Models.DocketModel> model = new List<Models.DocketModel>();
            Business.Service.Docket objDocket = new Business.Service.Docket();
            Entity.Service.Docket docket = new Entity.Service.Docket();

            Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
            Entity.HR.EmployeeMaster employeeMaster = new Entity.HR.EmployeeMaster();
            DataTable dtEmployee = objEmployeeMaster.EmployeeMaster_ById(new Entity.HR.EmployeeMaster() { EmployeeMasterId = employeeId });
            if (dtEmployee.AsEnumerable().Any())
                employeeMaster = objEmployeeMaster.AuthenticateUser(dtEmployee.Rows[0]["EmployeeCode"].ToString());

            int assignEngineer = 0;
            if (employeeMaster != null)
            {
                string[] roles = employeeMaster.Roles.Split(',');
                if (roles.Contains(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
                    assignEngineer = 0;
                else
                    assignEngineer = employeeId;
            }

            string callStatusIds = string.Empty;
            callStatusIds = string.Concat(((int)CallStatusType.DocketClose).ToString(), ",", ((int)CallStatusType.DocketFunctional).ToString());//DOCKET CLOSE && FUNCTIONAL          
            docket.CallStatusIds = callStatusIds;
            docket.AssignEngineer = assignEngineer;

            DataTable response = objDocket.Service_Docket_GetAllByCallStatusIds(docket);
            if (response != null
                && response.AsEnumerable().Any())
            {
                foreach (DataRow dr in response.Rows)
                {
                    model.Add(new Models.DocketModel
                    {
                        AssignedEngineerName = string.Format("Assigned Engineer: {0}", dr["AssignedEngineerName"].ToString()),
                        CallStatus = string.Format("Call Status: {0}", dr["CallStatus"].ToString()),
                        ContactPerson = string.Format("Contact Person: {0}", dr["ContactPerson"].ToString()),
                        CustomerName = string.Format("Customer Name: {0}", dr["CustomerName"].ToString()),
                        DocketDateTime = string.Format("Docket Date & Time: {0}", dr["DocketDate"].ToString()),
                        DocketNo = string.Format("Docket No: {0}", dr["DocketId"].ToString()),
                        IsCallAttended = string.Format("Call Attended: {0}", (dr["IsCallAttended"].ToString().Equals("1")) ? "True" : "False"),
                        ProductName = string.Format("Product Name: {0}", dr["ProductName"].ToString())
                    });
                }
            }

            return model;
        }

        [HttpPost]
        public HttpResponseMessage GetToner([FromBody]BaseModel model)
        {
            HttpResponseMessage retValue = null;
            using (retValue = new HttpResponseMessage(HttpStatusCode.InternalServerError))
            {
                try
                {
                    if (model != null)
                    {
                        List<Models.TonerModel> tonerModel = GetToner(model.UserId);
                        retValue = Request.CreateResponse(HttpStatusCode.OK, tonerModel);
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

        private List<Models.TonerModel> GetToner(int employeeId)
        {
            List<Models.TonerModel> model = new List<Models.TonerModel>();
            Business.Service.TonerRequest objTonnerRequest = new Business.Service.TonerRequest();

            Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
            Entity.HR.EmployeeMaster employeeMaster = new Entity.HR.EmployeeMaster();
            DataTable dtEmployee = objEmployeeMaster.EmployeeMaster_ById(new Entity.HR.EmployeeMaster() { EmployeeMasterId = employeeId });
            if (dtEmployee.AsEnumerable().Any())
                employeeMaster = objEmployeeMaster.AuthenticateUser(dtEmployee.Rows[0]["EmployeeCode"].ToString());

            int assignEngineer = 0;
            if (employeeMaster != null)
            {
                string[] roles = employeeMaster.Roles.Split(',');
                if (roles.Contains(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
                    assignEngineer = 0;
                else
                    assignEngineer = employeeId;
            }

            string callStatusIds = string.Empty;
            callStatusIds = string.Concat(((int)CallStatusType.TonerOpenForApproval).ToString(),
                ",",
                ((int)CallStatusType.TonerRequestInQueue).ToString(),
                ",",
                ((int)CallStatusType.TonerResponseGiven).ToString());

            DataTable response = objTonnerRequest.Service_Toner_GetByCallStatusIds(callStatusIds,assignEngineer);
            if (response != null
                && response.AsEnumerable().Any())
            {
                foreach (DataRow dr in response.Rows)
                {
                    model.Add(new Models.TonerModel
                    {
                        CallStatus = string.Format("Call Status: {0}", dr["CallStatus"].ToString()),
                        ContactPerson = string.Format("Contact Person: {0}", dr["ContactPerson"].ToString()),
                        CustomerName = string.Format("Customer Name: {0}", dr["CustomerName"].ToString()),
                        TonerDateTime = string.Format("Toner Date & Time: {0}", dr["RequestDate"].ToString()),
                        TonerNo = string.Format("Toner No: {0}", dr["TonnerRequestId"].ToString()),
                        ProductName = string.Format("Product Name: {0}", dr["ProductName"].ToString())
                    });
                }
            }

            return model;
        }

        [HttpPost]
        public HttpResponseMessage IsAuthorized([FromBody]AuthorizationModel model)
        {
            HttpResponseMessage retValue = null;
            using (retValue = new HttpResponseMessage(HttpStatusCode.InternalServerError))
            {
                try
                {
                    if (model != null)
                    {
                        AuthorizationModel authorizationModel = IsAuthorized(model.UserId, model.UtilityCode);
                        retValue = Request.CreateResponse(HttpStatusCode.OK, authorizationModel);
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

        private AuthorizationModel IsAuthorized(int employeeId, string utilityCode)
        {
            AuthorizationModel model = new AuthorizationModel();
            Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
            Entity.HR.EmployeeMaster employeeMaster = new Entity.HR.EmployeeMaster();
            DataTable dtEmployee = objEmployeeMaster.EmployeeMaster_ById(new Entity.HR.EmployeeMaster() { EmployeeMasterId = employeeId });
            if (dtEmployee.AsEnumerable().Any())
                employeeMaster = objEmployeeMaster.AuthenticateUser(dtEmployee.Rows[0]["EmployeeCode"].ToString());

            if (employeeMaster != null)
            {
                string[] roles = employeeMaster.Roles.Split(',');
                model.ReturnValue = roles.Contains(utilityCode);
            }
            else
            {
                model.ReturnValue = false;
            }
            return model;
        }

        [HttpPost]
        public HttpResponseMessage GetStockSnaps([FromBody]StockSnapModel model)
        {
            HttpResponseMessage retValue = null;
            using (retValue = new HttpResponseMessage(HttpStatusCode.InternalServerError))
            {
                try
                {
                    if (model != null)
                    {
                        List<Models.StockSnapModel> snapModel = GetStockSnaps(model.UserId, model.ProductName);
                        retValue = Request.CreateResponse(HttpStatusCode.OK, snapModel);
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

        private List<Models.StockSnapModel> GetStockSnaps(int employeeId, string itemName)
        {
            List<Models.StockSnapModel> model = new List<StockSnapModel>();
            Business.Inventory.Stock objStock = new Business.Inventory.Stock();
            string name = (string.IsNullOrEmpty(itemName.Trim())) ? string.Empty : itemName.Trim();

            Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
            Entity.HR.EmployeeMaster employeeMaster = new Entity.HR.EmployeeMaster();
            DataTable dtEmployee = objEmployeeMaster.EmployeeMaster_ById(new Entity.HR.EmployeeMaster() { EmployeeMasterId = employeeId });
            if (dtEmployee.AsEnumerable().Any())
                employeeMaster = objEmployeeMaster.AuthenticateUser(dtEmployee.Rows[0]["EmployeeCode"].ToString());

            if (employeeMaster != null)
            {
                string[] roles = employeeMaster.Roles.Split(',');
                if (roles.Contains(Entity.HR.Utility.STOCK_LOOKUP))
                {
                    DataTable response = objStock.GetStockSnap(itemName);
                    if (response != null
                        && response.AsEnumerable().Any())
                    {
                        foreach (DataRow dr in response.Rows)
                        {
                            model.Add(new Models.StockSnapModel
                            {
                                AssetLocationId = dr["AssetLocationId"].ToString(),
                                ItemId = dr["ItemId"].ToString(),
                                ItemType = dr["ItemType"].ToString(),
                                Location = string.Format("Location: {0}", dr["Location"].ToString()),
                                Quantity = string.Format("Quantity: {0}", dr["Quantity"].ToString()),
                                SpareName= string.Format("Spare Name: {0}", dr["SpareName"].ToString()),
                                ProductName = string.Format("Product Name: {0}", dr["ProductName"].ToString())
                            });
                        }
                    }
                }
                else
                { }
            }           

            return model;
        }


    }
}