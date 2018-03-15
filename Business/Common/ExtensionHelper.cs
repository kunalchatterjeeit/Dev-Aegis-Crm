using Kunal.Exception;
using System;
using System.Linq;

namespace Business.Common
{
    public static class ExtensionHelper
    {
        internal static Type[] GetTypes(params object[] args)
        {
            return ((args != null || args.Length > 0) ? args.Select(a => a.GetType()).ToArray() : new Type[0]);
        }
        public static void WriteException(this Exception ex)
        {
            ExceptionLogger.WriteErrorLog("AEGIS", ex, Application.Crm);
        }
    }
}
