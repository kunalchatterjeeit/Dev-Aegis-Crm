﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppAegisCRM
{
    public interface ILogger
    {
        int WriteLog(string content);
    }
}
