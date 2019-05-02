using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessEntity.Sales
{
    public class MeetingTypeDataAccess
    {
        public static List<MeetingTypeDbModel> GetAllMeetingType()
        {
            using (var Context = new CRMContext())
            {
                return Context.MeetingType.ToList();
            }
        }
        public static MeetingTypeDbModel GetMeetingTypeById(int Id)
        {
            using (var Context = new CRMContext())
            {
                return Context.MeetingType.Where(x => x.Id == Id).FirstOrDefault();
            }
        }
        public static int SaveMeetingType(MeetingTypeDbModel Model)
        {
            using (var Context = new CRMContext())
            {
                Context.MeetingType.AddOrUpdate(Model);
                return Context.SaveChanges();
            }
        }
        public static int DeleteMeetingType(int Id)
        {
            using (var Context = new CRMContext())
            {
                var Model = Context.MeetingType.Where(x => x.Id == Id).Single();
                Context.MeetingType.Remove(Model);
                return Context.SaveChanges();
            }
        }
    }
}
