using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppAegisCRM
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger logger = new Logger();
            logger.WriteLog(PrepareLogging());
        }

        private static string PrepareLogging()
        {
            return "Working2";
        }
    }
}
