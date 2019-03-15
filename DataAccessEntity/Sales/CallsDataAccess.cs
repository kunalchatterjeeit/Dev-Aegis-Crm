using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessEntity.Sales
{
    public class CallsDataAccess
    {
        public static List<CallStatusDbModel> GetCallStatus()
        {
            CRMContext Context = new CRMContext();
            return Context.CallStatus.ToList();
        }
        public static List<CallDirectionDbModel> GetCallDirection()
        {
            CRMContext Context = new CRMContext();
            return Context.CallDirection.ToList();
        }
        public static List<CallRelatedDbModel> GetCallRelated()
        {
            CRMContext Context = new CRMContext();
            return Context.CallRelated.ToList();
        }
        public static List<CallRepeatTypeDbModel> GetCallRepeatType()
        {
            CRMContext Context = new CRMContext();
            return Context.CallRepeatType.ToList();
        }
    }
}
