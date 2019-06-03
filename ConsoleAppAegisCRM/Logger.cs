using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppAegisCRM
{
    internal class Logger : ILogger
    {
        public int WriteLog(string content)
        {
            try
            {
                string fileName = DateTime.Now.ToString("ddMMyyyy") + ".txt";
                using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream))
                    {
                        Console.SetOut(streamWriter);
                        Console.WriteLine(content);
                        using (TextWriter textWriter = Console.Out)
                        {
                            Console.SetOut(textWriter);
                        }
                        streamWriter.Close();
                    }
                    fileStream.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Cannot open Redirect.txt for writing");
                Console.WriteLine(e.Message);
                return 0;
            }
            return 1;
        }
    }
}
