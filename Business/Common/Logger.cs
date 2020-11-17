
using Serilog;
using System;

namespace Business.Common
{
    public class Logger
    {
        public Logger() { }

        public ILogger Serilog_ExceptionLogger()
        {
            return new DataAccess.Common.Logger().Serilog_ExceptionLogger();
        }

        public void LogCustomException(string message, string actionName)
        {
            DataAccess.Common.Logger.LogCustomException(message, actionName);
        }

        public void LogException(Exception ex, string actionName)
        {
            DataAccess.Common.Logger.LogException(ex, actionName);
        }
    }
}
