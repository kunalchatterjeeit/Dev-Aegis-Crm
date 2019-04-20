using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessEntity.Sales
{
    public class LeadSourceDataAccess
    {
        public static List<LeadSourceDbModel> GetAllLeadSource()
        {
            using (var Context = new CRMContext())
            {
                return Context.LeadSource.ToList();
            }
        }
        public static LeadSourceDbModel GetLeadSourceById(int Id)
        {
            using (var Context = new CRMContext())
            {
                return Context.LeadSource.Where(x => x.Id == Id).FirstOrDefault();
            }
        }
        public static int SaveLeadSource(LeadSourceDbModel Model)
        {
            using (var Context = new CRMContext())
            {
                Context.LeadSource.AddOrUpdate(Model);
                return Context.SaveChanges();
            }
        }
        public static int DeleteLeadSource(int Id)
        {
            using (var Context = new CRMContext())
            {
                var Model = Context.LeadSource.Where(x => x.Id == Id).Single();
                Context.LeadSource.Remove(Model);
                return Context.SaveChanges();
            }
        }
    }
}
