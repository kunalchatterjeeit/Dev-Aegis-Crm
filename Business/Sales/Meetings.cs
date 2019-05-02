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
            MeetingTypeDataAccess.GetAllMeetingType().CopyListTo(MeetingTypeList);
            return MeetingTypeList;
        }
        public List<Entity.Sales.GetMeetings> GetAllMeetings(Entity.Sales.GetMeetingsParam Param)
        {
            List<Entity.Sales.GetMeetings> AllMeetingList = new List<Entity.Sales.GetMeetings>();
            GetMeetingsParamDbModel p = new GetMeetingsParamDbModel();
            Param.CopyPropertiesTo(p);
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
            MeetingsDbModel DbModel = new MeetingsDbModel();
            Model.CopyPropertiesTo(DbModel);
            return MeetingsDataAccess.SaveMeetings(DbModel);
        }
        public int DeleteMeetings(int Id)
        {
            return MeetingsDataAccess.DeleteMeetings(Id);
        }
        public int SaveMeetingLinks(Entity.Sales.Meetings Model)
        {
            MeetingsDbModel DbModel = new MeetingsDbModel();
            Model.CopyPropertiesTo(DbModel);
            return MeetingsDataAccess.SaveMeetingLinks(DbModel);
        }
    }
}
