using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Common;
using DataAccessEntity.Sales;

namespace Business.Sales
{
    public class Meetings
    {
        public Meetings() { }
        public List<Entity.Sales.MeetingStatus> GetMeetingStatus()
        {
            List<Entity.Sales.MeetingStatus> MeetingStatusList = new List<Entity.Sales.MeetingStatus>();
            MeetingsDataAccess.GetMeetingStatus().CopyListTo(MeetingStatusList);
            return MeetingStatusList;
        }
        public List<Entity.Sales.MeetingType> GetMeetingType()
        {
            List<Entity.Sales.MeetingType> MeetingTypeList = new List<Entity.Sales.MeetingType>();
            MeetingsDataAccess.GetMeetingType().CopyListTo(MeetingTypeList);
            return MeetingTypeList;
        }
        public List<Entity.Sales.GetMeetings> GetAllMeetings(Entity.Sales.GetMeetingsParam Param)
        {
            List<Entity.Sales.GetMeetings> AllMeetingList = new List<Entity.Sales.GetMeetings>();
            GetMeetingsParamDbModel p = new GetMeetingsParamDbModel
            {
                Name = Param.Name,
                MeetingStatusId = Param.MeetingStatusId,
                MeetingTypeId = Param.MeetingTypeId,
                StartDateTime = Param.StartDateTime,
                EndDateTime = Param.EndDateTime
            };
            MeetingsDataAccess.GetAllMeetings(p).CopyListTo(AllMeetingList);
            return AllMeetingList;
        }
        public Entity.Sales.Meetings GetMeetingById(int Id)
        {
            Entity.Sales.Meetings Meeting = new Entity.Sales.Meetings();
            MeetingsDataAccess.GetMeetingById(Id).CopyPropertiesTo(Meeting);
            return Meeting;
        }
        public int SaveMeetings(Entity.Sales.Meetings Model)
        {
            MeetingsDbModel DbModel = new MeetingsDbModel
            {
                Id = Model.Id,
                Name = Model.Name,
                Description = Model.Description,
                Location = Model.Location,
                MeetingStatusId = Model.MeetingStatusId,
                MeetingTypeId = Model.MeetingTypeId,
                CreatedBy = Model.CreatedBy,
                EmailReminder = Model.EmailReminder,
                PopupReminder = Model.PopupReminder,
                StartDateTime = Model.StartDateTime,
                EndDateTime = Model.EndDateTime,
                IsActive = Model.IsActive
            };
            return MeetingsDataAccess.SaveMeetings(DbModel);
        }
        public int DeleteMeetings(int Id)
        {
            return MeetingsDataAccess.DeleteMeetings(Id);
        }
    }
}
