
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
        public List<Entity.Sales.GetCalls> GetAllCalls(Entity.Sales.GetCallsParam Param)
        {
            List<Entity.Sales.GetCalls> AllCallList = new List<Entity.Sales.GetCalls>();
            GetCallsParamDbModel p = new GetCallsParamDbModel
            {
                CallStatusId = Param.CallStatusId,
                Subject = Param.Subject,
                StartDateTime = Param.StartDateTime,
                EndDateTime = Param.EndDateTime
            };
            CallsDataAccess.GetAllCalls(p).CopyListTo(AllCallList);
            return AllCallList;
        }
        public Entity.Sales.Calls GetCallById(int Id)
        {
            Entity.Sales.Calls call = new Entity.Sales.Calls();
            CallsDataAccess.GetCallById(Id).CopyPropertiesTo(call);
            return call;
        }
        public int SaveCalls(Entity.Sales.Calls Model)
        {
            CallsDbModel DbModel = new CallsDbModel
            {
                Id = Model.Id,
                Subject = Model.Subject,
                Description = Model.Description,
                CallStatusId = Model.CallStatusId,
                CallDirectionId = Model.CallDirectionId,
                CallRelatedTo = Model.CallRelatedTo,
                CallRepeatTypeId = Model.CallRepeatTypeId,
                CreatedBy = Model.CreatedBy,
                EmailReminder = Model.EmailReminder,
                PopupReminder = Model.PopupReminder,
                StartDateTime = Model.StartDateTime,
                EndDateTime = Model.EndDateTime,
                IsActive=Model.IsActive
            };
            return CallsDataAccess.SaveCalls(DbModel);
        }
        public int DeleteCalls(int Id)
        {
            return CallsDataAccess.DeleteCalls(Id);
        }
    }
}
