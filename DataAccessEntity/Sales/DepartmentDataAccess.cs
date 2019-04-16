using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace DataAccessEntity.Sales
{
    public class DepartmentDataAccess
    {
        public static List<DepartmentDbModel> GetAllDepartment()
        {
            using (var Context = new CRMContext())
            {
                return Context.Department.ToList();
            }
        }
        public static DepartmentDbModel GetDepartmentById(int Id)
        {
            using (var Context = new CRMContext())
            {
                return Context.Department.Where(x => x.Id == Id).FirstOrDefault();
            }
        }
        public static int SaveDepartment(DepartmentDbModel Model)
        {
            using (var Context = new CRMContext())
            {
                Context.Department.AddOrUpdate(Model);
                return Context.SaveChanges();
            }
        }
        public static int DeleteDepartment(int Id)
        {
            using (var Context = new CRMContext())
            {
                var Model = Context.Department.Where(x => x.Id == Id).Single();
                Context.Department.Remove(Model);
                return Context.SaveChanges();
            }
        }
    }
}
