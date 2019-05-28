using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Business.Common
{
    public static class ApplicationConfiguration
    {
        public static string NoReplyEmailSender
        {
            get
            {
                return (WebConfigurationManager.AppSettings.AllKeys.Contains("NoReplyEmailSender")) ? 
                    WebConfigurationManager.AppSettings["NoReplyEmailSender"] : string.Empty;
            }
        }
        public static string NoReplyEmailPassword
        {
            get
            {
                return (WebConfigurationManager.AppSettings.AllKeys.Contains("NoReplyEmailPassword")) ? 
                    WebConfigurationManager.AppSettings["NoReplyEmailPassword"] : string.Empty;
            }
        }
    }
}
