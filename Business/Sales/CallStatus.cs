
using Business.Common;
using DataAccessEntity.Sales;
using System.Collections.Generic;
using System.Data;

namespace Business.Sales
{
    public class CallStatus
    {
        public CallStatus() { }
        public List<Entity.Sales.CallStatus> GetAllCallStatus()
        {
            List<Entity.Sales.CallStatus> callStatusList = new List<Entity.Sales.CallStatus>();
            CallStatusDataAccess.GetAllCallStatus().CopyListTo(callStatusList);
            return callStatusList;
        }
        public Entity.Sales.CallStatus GetCallStatusById(int Id)
        {
            Entity.Sales.CallStatus callStatus = new Entity.Sales.CallStatus();
            CallStatusDataAccess.GetCallStatusById(Id).CopyPropertiesTo(callStatus);
            return callStatus;
        }
        public int SaveCallStatus(Entity.Sales.CallStatus Model)
        {
            CallStatusDbModel DbModel = new CallStatusDbModel();
            Model.CopyPropertiesTo(DbModel);
            return CallStatusDataAccess.SaveCallStatus(DbModel);
        }       
        public int DeleteCallStatus(int Id)
        {
            return CallStatusDataAccess.DeleteCallStatus(Id);
        }
    }
}
