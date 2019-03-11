
using System.Collections.Generic;
using System.Data;

namespace Business.Sales
{
    public class Calls
    {
        public Calls()
        {

        }

        public List<Entity.Sales.CallStatus> GetCallStatus()
        {
            return DataAccessEntity.Sales.CallsDataAccess.GetCallStatus();
        }
        public List<Entity.Sales.CallRelated> GetCallRelated()
        {
            return DataAccessEntity.Sales.CallsDataAccess.GetCallRelated();
        }
        public List<Entity.Sales.CallRepeatType> GetCallRepeatType()
        {
            return DataAccessEntity.Sales.CallsDataAccess.GetCallRepeatType();
        }
        public List<Entity.Sales.CallDirection> GetCallDirection()
        {
            return DataAccessEntity.Sales.CallsDataAccess.GetCallDirection();
        }
    }
}
