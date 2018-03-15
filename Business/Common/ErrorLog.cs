using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Business.Common
{
    public class ErrorLog
    {
        public ErrorLog()
        { }

        public static void MasterErrorLog(string logpath, string errormsg, string userid)
        {
            // This text is added only once to the file.
            if (!File.Exists(logpath))
            {
                // Create a file to write to.
                string[] createText = { "Error Message: " + errormsg, Environment.NewLine, "User ID: " + userid, Environment.NewLine, "Error DateTime: " + DateTime.Today.ToString("dd-MMM-yyyy hh:mm:ss tt"), Environment.NewLine };
                File.WriteAllLines(logpath, createText);
                File.AppendAllText(logpath, Environment.NewLine);
                string appendText = "------------------------------------------------------------------------------------------------------";
                File.AppendAllText(logpath, appendText);
            }
            else
            {
                File.AppendAllText(logpath, Environment.NewLine);
                File.AppendAllText(logpath, "Error Message: " + errormsg);
                File.AppendAllText(logpath, Environment.NewLine);
                File.AppendAllText(logpath, "User ID: " + userid);
                File.AppendAllText(logpath, Environment.NewLine);
                File.AppendAllText(logpath, "Error DateTime: " + DateTime.Today.ToString("dd-MMM-yyyy hh:mm:ss tt"));
                File.AppendAllText(logpath, Environment.NewLine);
                File.AppendAllText(logpath, "------------------------------------------------------------------------------------------------------");
                
            }
        }
    }
}
