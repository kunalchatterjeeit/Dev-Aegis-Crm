using Business.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM
{
    public partial class ForgotPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnValidate_Click(object sender, EventArgs e)
        {
            try
            {
                string validationLink = string.Empty;

                Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
                DataTable dtValidate = objEmployeeMaster.ValidateForgotPassword(txtUserName.Text.Trim(), txtEmailId.Text.Trim());

                if (dtValidate != null && dtValidate.Rows.Count > 0)
                {
                    validationLink = string.Concat("EmployeeId=", dtValidate.Rows[0]["EmployeeMasterId"].ToString(), "&Email=", txtEmailId.Text.Trim(), "&UserName=", txtUserName.Text.Trim(), "&Source=ForgotPassword");

                    validationLink = "http://" + HttpContext.Current.Request.Url.Authority + "/ValidateLinks.aspx?enc=" + validationLink.EncryptQueryString();

                    SendEmailValidationLink(txtEmailId.Text.Trim(), dtValidate.Rows[0]["EmployeeName"].ToString(), validationLink);
                    lblUserMessage.InnerText = "Validation link is sent to your office email id.";
                }
                else
                {
                    lblUserMessage.InnerText = "Username and Email Id not found!";
                }
            }
            catch (Exception ex)
            {
                lblUserMessage.InnerText = ex.Message;
                ex.WriteException();
            }
        }

        private void SendEmailValidationLink(string userEmail, string name, string link)
        {
            string mockupPath = "http://" + HttpContext.Current.Request.Url.Authority + "/EmailMockups/ForgotPassword.html";

            string mockupPage = PageContent.Read(mockupPath);
            mockupPage = mockupPage.Replace("{{CURRENT_DATE}}", DateTime.Now.ToString("dd MMM yyyy"));
            mockupPage = mockupPage.Replace("{{USER_NAME}}", name);
            mockupPage = mockupPage.Replace("{{PERSONAL_EMAIL_VERIFICATION_LINK}}", link);

            MailFunctionality.SendMail_Hostgator(userEmail, "Forgot password validation", mockupPage);
        }
    }
}