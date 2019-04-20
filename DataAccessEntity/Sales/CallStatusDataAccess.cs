using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace DataAccessEntity.Sales
{
    public class CallStatusDataAccess
    {
        public static List<CallStatusDbModel> GetAllCallStatus()
        {
            using (var Context = new CRMContext())
            {
                return Context.CallStatus.ToList();
            }
        }
        public static CallStatusDbModel GetCallStatusById(int Id)
        {
            using (var Context = new CRMContext())
            {
                return Context.CallStatus.Where(x=>x.Id==Id).FirstOrDefault();
            }
        }
        public static int SaveCallStatus(CallStatusDbModel Model)
        {
            using (var Context = new CRMContext())
            {
                Context.CallStatus.AddOrUpdate(Model);
                return Context.SaveChanges();
            }
        }      
        public static int DeleteCallStatus(int Id)
        {
            using (var Context = new CRMContext())
            {
                var Model = Context.CallStatus.Where(x => x.Id == Id).Single();
                Context.CallStatus.Remove(Model);               
                return Context.SaveChanges();
            }
        }
    }
}
