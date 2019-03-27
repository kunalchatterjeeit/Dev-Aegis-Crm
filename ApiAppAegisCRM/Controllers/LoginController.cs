using ApiAppAegisCRM.Models;
using Business.Common;
using Entity.Common;
using System;
using System.Collections.Generic;
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
                        model.ResponseCode = 200;
                        model.Password = string.Empty;
                        model.IsLoginBlocked = false;
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
                        model.LoginStatus = "SUCCESS";

                        model.Name = employeeMaster.EmployeeName + " (" + employeeMaster.EmployeeCode + ")";
                        model.UserId = Convert.ToInt32(userId);
                        auth.UserId = Convert.ToInt32(userId);
                        auth.IP = GetIP();
                        auth.Status = Entity.Common.LoginStatus.Success;
                        auth.Client = GetClient();
                        objEmployeeMaster.Login_Save(auth);
                    }
                    else
                    {
                        auth.UserId = Convert.ToInt32(userId);
                        auth.IP = GetIP();
                        auth.Status = Entity.Common.LoginStatus.WrongPassword;
                        auth.Client = GetClient();
                        auth.FailedUserName = model.UserName;
                        auth.FailedPassword = model.Password;
                        objEmployeeMaster.Login_Save(auth);
                        model.LoginStatus = "FAILED";
                    }
                }
                else
                {
                    auth.IP = GetIP();
                    auth.Status = Entity.Common.LoginStatus.Failed;
                    auth.Client = GetClient();
                    auth.FailedUserName = model.UserName;
                    auth.FailedPassword = model.Password;
                    objEmployeeMaster.Login_Save(auth);
                    model.LoginStatus = "FAILED";
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                model.LoginStatus = "FAILED";
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
