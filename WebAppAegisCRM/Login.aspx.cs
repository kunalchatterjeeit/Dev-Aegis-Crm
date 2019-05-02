using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Security;
using Business.Common;
using Entity.Common;
using System.Net;
using System.Net.Sockets;

namespace WebAppAegisCRM
{
    public partial class Login : System.Web.UI.Page
    {
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
            return Request.Headers["User-Agent"].ToString();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        private void UserLogin()
        {
            try
            {
                Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
                Entity.HR.EmployeeMaster employeeMaster = new Entity.HR.EmployeeMaster();
                Entity.Common.Auth auth = new Auth();
                employeeMaster = objEmployeeMaster.AuthenticateUser(txtUserName.Text);

                if (employeeMaster != null)
                {
                    string passowrd = employeeMaster.Password;
                    string userId = employeeMaster.UserId.ToString();
//#if (DEBUG)
//                    if (true)
//#else
                    if (employeeMaster.IsActive && passowrd.Equals(txtPassword.Text.Trim().EncodePasswordToBase64()))
//#endif
                    {
                        if (employeeMaster.IsLoginActive)
                        {
                            string roles = employeeMaster.Roles;
                            string userSettings = new Business.Settings.UserSettings().GetByUserId(Convert.ToInt32(userId)).Tables[0].Rows[0]["UserSettings"].ToString();
                            roles = string.Concat(roles, userSettings);

                            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                                                                           1,
                                                                           userId,
                                                                           DateTime.Now,
                                                                           DateTime.Now.AddHours(2),
                                                                           false,
                                                                           roles, //define roles here
                                                                           "/");
                            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                            Response.Cookies.Add(cookie);

                            auth.UserId = Convert.ToInt32(userId);
                            auth.IP = GetIP();
                            auth.Status = Entity.Common.LoginStatus.Success;
                            auth.Client = GetClient();
                            objEmployeeMaster.Login_Save(auth);
                            if (employeeMaster.IsPasswordChangeRequired)
                            {
                                Response.Redirect(@"ResetPassword.aspx");
                            }
                            else
                            {
                                Response.Redirect(@"Dashboard.aspx");
                            }
                        }
                        else
                        {
                            lblUserMessage.InnerHtml = "Login blocked by admin.";
                            lblUserMessage.Visible = true;
                        }
                    }
                    else
                    {
                        auth.UserId = Convert.ToInt32(userId);
                        auth.IP = GetIP();
                        auth.Status = Entity.Common.LoginStatus.WrongPassword;
                        auth.Client = GetClient();
                        auth.FailedUserName = txtUserName.Text;
                        auth.FailedPassword = txtPassword.Text;
                        objEmployeeMaster.Login_Save(auth);
                        lblUserMessage.InnerHtml = "Invalid Username/Password";
                        lblUserMessage.Visible = true;
                    }
                }
                else
                {
                    auth.IP = GetIP();
                    auth.Status = Entity.Common.LoginStatus.Failed;
                    auth.Client = GetClient();
                    auth.FailedUserName = txtUserName.Text;
                    auth.FailedPassword = txtPassword.Text;
                    objEmployeeMaster.Login_Save(auth);
                    lblUserMessage.InnerHtml = "Invalid Username/Password";
                    lblUserMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                lblUserMessage.InnerHtml = "Invalid Username/Password";
                lblUserMessage.Visible = true;
            }
        }

        private void CustomerLogin()
        {
            Business.Customer.Customer ObjUser = new Business.Customer.Customer();
            DataTable dt = ObjUser.CustomerAuthentication(txtCustomerEmail.Text, txtMobileNumber.Text);
            if (dt != null && dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["STATUS"].ToString() == "1")
                {
                    string UserId = dt.Rows[0]["CustomerId"].ToString() + "|" + dt.Rows[0]["CustomerName"].ToString();
                    string roles = "";
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                                                               1,
                                                               UserId,
                                                               DateTime.Now,
                                                               DateTime.Now.AddHours(2),
                                                               false,
                                                               roles,
                                                               "/");
                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(authTicket));
                    Response.Cookies.Add(cookie);
                    Response.Redirect("CustomerDashboard.aspx");
                }
            }
            else
            {
                lblCustomerMessage.InnerHtml = "Invalid Email/Mobile Number";
            }

        }

        protected void btnCustomerLogin_Click(object sender, EventArgs e)
        {
            CustomerLogin();
        }

        protected void btnUserLogin_Click(object sender, EventArgs e)
        {
            UserLogin();
        }
    }
}