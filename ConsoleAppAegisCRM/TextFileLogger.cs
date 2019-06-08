using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppAegisCRM
{
    internal class TextFileLogger : ILogger
    {
        public int WriteLog(string content)
        {
            try
            {
                string fileName = DateTime.Now.ToString("ddMMyyyy") + ".txt";
                if (!File.Exists(fileName))
                {
                    File.Create(fileName).Dispose();

                    using (TextWriter tw = new StreamWriter(fileName))
                    {
                        tw.WriteLine(content);
                    }

                }
                else if (File.Exists(fileName))
                {
                    using (TextWriter tw = new StreamWriter(fileName, true))
                    {
                        tw.WriteLine(content);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open text file for writing");
                Console.WriteLine(e.Message);
                return 0;
            }
            return 1;
        }
    }
}
