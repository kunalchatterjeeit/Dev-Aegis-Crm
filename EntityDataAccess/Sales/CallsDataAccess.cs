using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityDataAccess.Sales
{
    public class CallsDataAccess
    {
        public static List<CallStatus> GetCallStatus()
        {
            CRMContext Context = new CRMContext();
            return Context.CallStatus.ToList();
        }
        public static List<CallDirection> GetCallDirection()
        {
            CRMContext Context = new CRMContext();
            return Context.CallDirection.ToList();
        }
        public static List<CallRelated> GetCallRelated()
        {
            EntityDataAccess.CRMContext Context = new CRMContext();
            return Context.CallRelated.ToList();
        }
        public static List<CallRepeatType> GetCallRepeatType()
        {
            CRMContext Context = new CRMContext();
            return Context.CallRepeatType.ToList();
        }
    }
}
