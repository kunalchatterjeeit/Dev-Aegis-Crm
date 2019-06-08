using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppAegisCRM
{
    public class ConsoleLogger : ILogger
    {
        public int WriteLog(string content)
        {
            try
            {
                Console.WriteLine(content);
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return 0;
        }
    }
}
