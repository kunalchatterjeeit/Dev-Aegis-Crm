using Business.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                Response.Redirect("~/MainLogout.aspx");

            if (!IsPostBack)
            {
                lblMessage.InnerText = string.Empty;
            }
        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateSave())
                {
                    Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
                    Entity.HR.EmployeeMaster employeeMaster = new Entity.HR.EmployeeMaster();
                    employeeMaster.EmployeeMasterId = (Business.Common.Context.EmployeeId == 0) ?
                        Convert.ToInt32(HttpContext.Current.User.Identity.Name) :
                        Business.Common.Context.EmployeeId;
                    employeeMaster.Password = txtNewPassword.Text.Trim().EncodePasswordToBase64();
                    int employeeId = 0;
                    employeeId = objEmployeeMaster.HR_PasswordReset_Save(employeeMaster);
                    if (employeeId > 0)
                    {
                        lblMessage.InnerText = "Password changed.";
                        Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.Cache.SetNoStore();
                        Thread.Sleep(2000);
                        System.Web.Security.FormsAuthentication.SignOut();
                        System.Web.Security.FormsAuthentication.RedirectToLoginPage();
                    }
                    else
                    {
                        lblMessage.InnerText = "Failed to save data.";
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                lblMessage.InnerText = ex.Message;
            }
        }

        private bool ValidateSave()
        {
            if (string.IsNullOrEmpty(txtNewPassword.Text.Trim()))
            {
                lblMessage.InnerText = "Please enter new password.";
                return false;
            }
            return true;
        }
    }
}