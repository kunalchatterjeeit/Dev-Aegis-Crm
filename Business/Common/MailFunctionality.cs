using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace Business.Common
{
    public class MailFunctionality
    {
        public static void SendMail_HostingRaja(string fromMail, string toMail, string password, string subject, string body)
        {
            MailMessage email = new MailMessage();
            email.To.Add(toMail);
            //email.CC.Add(txtcemail.Text);
            email.From = new MailAddress(fromMail, "Notification from Aegis CRM");
            email.IsBodyHtml = true;
            email.Subject = subject;
            email.Body = body;
            SmtpClient smtp = new SmtpClient();
            smtp.EnableSsl = false;
            smtp.Port = 25;
            smtp.Host = "mail.aegiscrm.com"; //Or Your SMTP Server Address
            smtp.Credentials = new
            System.Net.NetworkCredential(fromMail, password);
            smtp.Send(email);
        }

        public static void SendMail_Hostgator(string toMail, string subject, string body)
        {
            SmtpClient smtpClient = new SmtpClient("mail.aegiscrm.in", 25);

            smtpClient.Credentials = new System.Net.NetworkCredential(ApplicationConfiguration.NoReplyEmailSender, ApplicationConfiguration.NoReplyEmailPassword);
            //smtpClient.UseDefaultCredentials = true;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = false;
            MailMessage mail = new MailMessage();

            //Setting From , To and CC
            mail.From = new MailAddress(ApplicationConfiguration.NoReplyEmailSender, "Aegis CRM");
            mail.To.Add(new MailAddress(toMail));
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            smtpClient.Send(mail);
        }
    }
}
