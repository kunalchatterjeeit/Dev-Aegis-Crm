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
    }
}
