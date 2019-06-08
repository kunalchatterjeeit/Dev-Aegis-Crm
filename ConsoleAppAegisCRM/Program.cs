using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleAppAegisCRM
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                ILogger logger = new TextFileLogger();
                logger.WriteLog(ex.Message);
            }
        }

        private static string PrepareLogging(string rawContent)
        {
            return rawContent;
        }
    }
}
