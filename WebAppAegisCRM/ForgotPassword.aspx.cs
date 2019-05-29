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

        protected void btnReset_Click(object sender, EventArgs e)
        {

        }

        protected void btnValidate_Click(object sender, EventArgs e)
        {
            string mockupPath = "http://" + HttpContext.Current.Request.Url.Authority + "/EmailMockups/ForgotPassword.html";

            try
            {
                string mockupPage = Business.Common.PageContent.Read(mockupPath);
                mockupPage = mockupPage.Replace("{{CURRENT_DATE}}", DateTime.Now.ToString("dd MMM yyyy"));
                mockupPage = mockupPage.Replace("{{USER_NAME}}", "Kunal Chatterjee");
                mockupPage = mockupPage.Replace("{{PERSONAL_EMAIL_VERIFICATION_LINK}}", "emailverificationlink");

                MailFunctionality.SendMail_Hostgator("", "", mockupPage);
            }
            catch (Exception ex)
            {
                lblUserMessage.InnerText = mockupPath;
                ex.WriteException();
            }
        }
    }
}