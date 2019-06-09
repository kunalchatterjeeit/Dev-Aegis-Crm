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
                ILogger logger = new ConsoleLogger();
                new Process().Execute(logger);
            }
            catch (Exception ex)
            {
                ILogger logger = new TextFileLogger();
                logger.WriteLog(ex.Message);
            }
        }

        
    }
}
