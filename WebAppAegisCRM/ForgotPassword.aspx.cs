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
            try
            {
                string mockupPath = "http://" + HttpContext.Current.Request.Url.Authority + "/EmailMockups/ForgotPassword.html";
                string mockupPage = Business.Common.PageContent.Read(mockupPath);
                mockupPage = mockupPage.Replace("{{CURRENT_DATE}}", DateTime.Now.ToString("dd MMM yyyy"));
                mockupPage = mockupPage.Replace("{{USER_NAME}}", "Kunal Chatterjee");
                mockupPage = mockupPage.Replace("{{PERSONAL_EMAIL_VERIFICATION_LINK}}", "emailverificationlink");

                SmtpClient smtpClient = new SmtpClient("mail.aegiscrm.in", 25);

                smtpClient.Credentials = new System.Net.NetworkCredential(Business.Common.ApplicationConfiguration.NoReplyEmailSender, Business.Common.ApplicationConfiguration.NoReplyEmailPassword);
                //smtpClient.UseDefaultCredentials = true;
                smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = false;
                MailMessage mail = new MailMessage();

                //Setting From , To and CC
                mail.From = new MailAddress(Business.Common.ApplicationConfiguration.NoReplyEmailSender, "Aegis CRM");
                mail.To.Add(new MailAddress("kunalchatterjeeit@gmail.com"));
                mail.Subject = "Forgot email varification process";
                mail.Body = mockupPage;
                mail.IsBodyHtml = true;

                smtpClient.Send(mail);
            }
            catch (Exception ex)
            {
                lblUserMessage.InnerText = ex.Message;
            }
        }
    }
}