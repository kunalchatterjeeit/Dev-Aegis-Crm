
using Business.Common;
using DataAccessEntity.Sales;
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
            List<Entity.Sales.CallStatus> callStatusList = new List<Entity.Sales.CallStatus>();
            CallsDataAccess.GetCallStatus().CopyListTo(callStatusList);
            return callStatusList;
        }
        public List<Entity.Sales.CallRelated> GetCallRelated()
        {
            List<Entity.Sales.CallRelated> callRelatedList = new List<Entity.Sales.CallRelated>();
            CallsDataAccess.GetCallRelated().CopyListTo(callRelatedList);
            return callRelatedList;
        }
        public List<Entity.Sales.CallRepeatType> GetCallRepeatType()
        {
            List<Entity.Sales.CallRepeatType> callRepeatTypeList = new List<Entity.Sales.CallRepeatType>();
            CallsDataAccess.GetCallRepeatType().CopyListTo(callRepeatTypeList);
            return callRepeatTypeList;
        }
        public List<Entity.Sales.CallDirection> GetCallDirection()
        {
            List<Entity.Sales.CallDirection> callDirectionList = new List<Entity.Sales.CallDirection>();
            CallsDataAccess.GetCallDirection().CopyListTo(callDirectionList);
            return callDirectionList;
        }
    }
}
