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
            if (!IsPostBack)
            {
                ddlLoginType_SelectedIndexChanged(sender, e);
            }
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (ddlLoginType.SelectedValue == "1")
                UserLogin();
            else
                CustomerLogin();
        }

        private void UserLogin()
        {
            try
            {
                Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
                Entity.HR.EmployeeMaster employeeMaster = new Entity.HR.EmployeeMaster();
                Entity.Common.Auth auth = new Auth();
                employeeMaster = objEmployeeMaster.AuthenticateUser(txtUsername.Text);

                if (employeeMaster != null)
                {
                    string passowrd = employeeMaster.Password;
                    string userId = employeeMaster.UserId.ToString();
                    if (passowrd.Equals(txtPassword.Text.Trim().EncryptQueryString()))
                    {
                        string roles = employeeMaster.Roles;
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
                        Response.Redirect(@"Dashboard.aspx");
                    }
                    else
                    {
                        auth.UserId = Convert.ToInt32(userId);
                        auth.IP = GetIP();
                        auth.Status = Entity.Common.LoginStatus.WrongPassword;
                        auth.Client = GetClient();
                        auth.FailedUserName = txtUsername.Text;
                        auth.FailedPassword = txtPassword.Text;
                        objEmployeeMaster.Login_Save(auth);
                        lblMessage.InnerHtml = "Invalid Username/Password";
                        lblMessage.Visible = true;
                    }
                }
                else
                {
                    auth.IP = GetIP();
                    auth.Status = Entity.Common.LoginStatus.Failed;
                    auth.Client = GetClient();
                    auth.FailedUserName = txtUsername.Text;
                    auth.FailedPassword = txtPassword.Text;
                    objEmployeeMaster.Login_Save(auth);
                    lblMessage.InnerHtml = "Invalid Username/Password";
                    lblMessage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                lblMessage.InnerHtml = "Invalid Username/Password";
                lblMessage.Visible = true;
            }
        }

        //private void UserLogin()
        //{
        //    Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
        //    Entity.HR.EmployeeMaster employeeMaster = new Entity.HR.EmployeeMaster();
        //    employeeMaster = objEmployeeMaster.AuthenticateUser(txtUsername.Text);

        //    if (employeeMaster != null)
        //    {
        //        if (employeeMaster.Password == txtPassword.Text)
        //        {
        //            string UserId = employeeMaster.UserId.ToString();
        //            string roles = employeeMaster.Roles;
        //            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
        //                                                       1,
        //                                                       UserId,
        //                                                       DateTime.Now,
        //                                                       DateTime.Now.AddHours(2),
        //                                                       false,
        //                                                       roles,
        //                                                       "/");
        //            HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName,FormsAuthentication.Encrypt(authTicket));
        //            Response.Cookies.Add(cookie);
        //            Response.Redirect(@"Dashboard.aspx");
        //        }
        //        else
        //        {
        //            lblMessage.InnerHtml = "Invalid Username/Password";
        //        }
        //    }
        //    else
        //    {
        //        lblMessage.InnerHtml = "Invalid Username/Password";
        //    }
        //}

        private void CustomerLogin()
        {
            Business.Customer.Customer ObjUser = new Business.Customer.Customer();
            DataTable dt = ObjUser.CustomerAuthentication(txtUsername.Text, txtPassword.Text);
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
                lblMessage.InnerHtml = "Invalid Username/Password";
            }

        }

        protected void ddlLoginType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLoginType.SelectedValue == "1")
            {
                lblUserName.Text = "Username :";
                lblPassword.Text = "Password :";
            }
            else
            {
                lblUserName.Text = "Email Id :";
                lblPassword.Text = "Mobile No :";
            }
        }
    }
}