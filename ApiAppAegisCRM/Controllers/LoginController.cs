using ApiAppAegisCRM.Models;
using Business.Common;
using Entity.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Web.Http;

namespace ApiAppAegisCRM.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage DoLogin([FromBody]LoginModel model)
        {
            HttpResponseMessage retValue = null;
            using (retValue = new HttpResponseMessage(HttpStatusCode.InternalServerError))
            {
                try
                {
                    if (model != null)
                    {
                        model = UserLogin(model);
                        //If success then register device
                        if (model.ResponseCode == 200 && !string.IsNullOrEmpty(model.DeviceId))
                        {
                            model.Message = RegisterDevice(model.UserId, model.DeviceId);
                        }
                        model.Password = string.Empty;
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

        private string RegisterDevice(int employeeId, string deviceId)
        {
            string retValue = string.Empty;
            Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
            int response = objEmployeeMaster.LinkedDevices_Save(employeeId, deviceId);
            if (response > 0)
            {
                retValue = "Login succeeded.Your device is now registered with us.";
            }
            return retValue;
        }

        [HttpPost]
        public HttpResponseMessage DoAutoLogin([FromBody]LoginModel model)
        {
            HttpResponseMessage retValue = null;
            using (retValue = new HttpResponseMessage(HttpStatusCode.InternalServerError))
            {
                try
                {
                    if (model != null)
                    {
                        model = UserAutoLogin(model);
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

        private LoginModel UserLogin(LoginModel model)
        {
            try
            {
                model.ResponseCode = 99;
                Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
                Entity.HR.EmployeeMaster employeeMaster = new Entity.HR.EmployeeMaster();
                Entity.Common.Auth auth = new Auth();
                employeeMaster = objEmployeeMaster.AuthenticateUser(model.UserName);

                if (employeeMaster != null)
                {
                    string passowrd = employeeMaster.Password;
                    string userId = employeeMaster.UserId.ToString();

                    if (passowrd.Equals(model.Password.Trim().EncodePasswordToBase64()))
                    {
                        DataTable dtDevices = objEmployeeMaster.LinkedDevices_GetByUserId(employeeMaster.UserId);
                        if (dtDevices != null && dtDevices.Rows.Count > 0)
                        {
                            model.ResponseCode = 99;
                            model.Message = "A device is already linked with you. Please contact admin to change device.";
                        }
                        else if (employeeMaster.IsPasswordChangeRequired)
                        {
                            model.ResponseCode = 99;
                            model.Message = "Reset password needed. Please visit aegiscrm.in to reset password.";
                        }
                        else if (!employeeMaster.IsLoginActive)
                        {
                            model.ResponseCode = 99;
                            model.Message = "Login blocked by admin.";
                        }
                        else
                        {
                            model.Name = employeeMaster.EmployeeName + " (" + employeeMaster.EmployeeCode + ")";
                            model.UserId = Convert.ToInt32(userId);
                            model.ResponseCode = 200;
                            model.Message = "Success";

                            auth.UserId = Convert.ToInt32(userId);
                            auth.IP = GetIP();
                            auth.Status = Entity.Common.LoginStatus.Success;
                            auth.Client = GetClient();
                            objEmployeeMaster.Login_Save(auth);
                        }
                    }
                    else
                    {
                        model.Message = "Invalid username/password.";

                        auth.UserId = Convert.ToInt32(userId);
                        auth.IP = GetIP();
                        auth.Status = Entity.Common.LoginStatus.WrongPassword;
                        auth.Client = GetClient();
                        auth.FailedUserName = model.UserName;
                        auth.FailedPassword = model.Password;
                        objEmployeeMaster.Login_Save(auth);
                    }
                }
                else
                {
                    model.Message = "Invalid username/password.";

                    auth.IP = GetIP();
                    auth.Status = Entity.Common.LoginStatus.Failed;
                    auth.Client = GetClient();
                    auth.FailedUserName = model.UserName;
                    auth.FailedPassword = model.Password;
                    objEmployeeMaster.Login_Save(auth);
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                model.Message = ex.Message;
            }
            return model;
        }

        private LoginModel UserAutoLogin(LoginModel model)
        {
            try
            {
                model.ResponseCode = 99;
                Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
                Entity.HR.EmployeeMaster employeeMaster = new Entity.HR.EmployeeMaster();
                Entity.Common.Auth auth = new Auth();
                employeeMaster = objEmployeeMaster.AutoAuthenticateUserByDevice(model.DeviceId);

                if (employeeMaster != null)
                {
                    string userId = employeeMaster.UserId.ToString();

                    if (employeeMaster.IsPasswordChangeRequired)
                    {
                        model.ResponseCode = 99;
                        model.Message = "Reset password needed. Please visit aegiscrm.in to reset password.";
                    }
                    else if (!employeeMaster.IsLoginActive)
                    {
                        model.ResponseCode = 99;
                        model.Message = "Login blocked by admin.";
                    }
                    else
                    {
                        model.Name = employeeMaster.EmployeeName + " (" + employeeMaster.EmployeeCode + ")";
                        model.UserId = Convert.ToInt32(userId);
                        model.ResponseCode = 200;
                        model.Message = "Success";

                        auth.UserId = Convert.ToInt32(userId);
                        auth.IP = GetIP();
                        auth.Status = Entity.Common.LoginStatus.Success;
                        auth.Client = GetClient();
                        objEmployeeMaster.Login_Save(auth);
                    }
                }
                else
                {
                    model.Message = "Device not registered. Please login with username and password.";

                    auth.IP = GetIP();
                    auth.Status = Entity.Common.LoginStatus.Failed;
                    auth.Client = GetClient();
                    auth.FailedUserName = model.DeviceId;
                    auth.FailedPassword = model.Password;
                    objEmployeeMaster.Login_Save(auth);
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                model.Message = ex.Message;
            }
            return model;
        }

        private string GetIP()
        {
            string retValue = string.Empty;
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    retValue = ip.ToString();
                }
            }

            return retValue;
        }

        private string GetClient()
        {
            return Request.Headers.UserAgent.ToString();
        }
    }
}
