using Business.Common;
using DataAccessEntity.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Sales
{
    public class MeetingType
    {
        public MeetingType() { }

        public List<Entity.Sales.MeetingType> GetAllMeetingType()
        {
            List<Entity.Sales.MeetingType> MeetingTypeList = new List<Entity.Sales.MeetingType>();
            MeetingTypeDataAccess.GetAllMeetingType().CopyListTo(MeetingTypeList);
            return MeetingTypeList;
        }
        public Entity.Sales.MeetingType GetMeetingTypeById(int Id)
        {
            Entity.Sales.MeetingType MeetingType = new Entity.Sales.MeetingType();
            MeetingTypeDataAccess.GetMeetingTypeById(Id).CopyPropertiesTo(MeetingType);
            return MeetingType;
        }
        public int SaveMeetingType(Entity.Sales.MeetingType Model)
        {
            MeetingTypeDbModel DbModel = new MeetingTypeDbModel();
            Model.CopyPropertiesTo(DbModel);
            return MeetingTypeDataAccess.SaveMeetingType(DbModel);
        }
        public int DeleteMeetingType(int Id)
        {
            return MeetingTypeDataAccess.DeleteMeetingType(Id);
        }
    }
}
