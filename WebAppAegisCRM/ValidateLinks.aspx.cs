using Business.Common;
using System;
using System.Linq;

namespace WebAppAegisCRM
{
    public partial class ValidateLinks : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Response.Write("Please wait...");
                string enc = Request.QueryString["enc"].ToString();
                string dcr = enc.DecryptQueryString();

                string[] info = dcr.Split('&');
                string source = info.Where(p => p.Contains("Source")).FirstOrDefault();

                switch (source.Split('=')[1])
                {
                    case "ForgotPassword":
                        ForgotPasswordConfigurations(info);
                        break;
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                Response.Write(ex.Message);
            }

        }

        private void ForgotPasswordConfigurations(string[] info)
        {
            string employeeId = info.Where(p => p.Contains("EmployeeId")).FirstOrDefault();
            employeeId = employeeId.Split('=')[1].ToString();
            Business.Common.Context.EmployeeId = Convert.ToInt32(employeeId);
            Response.Redirect("ResetPassword.aspx");
        }
    }
}