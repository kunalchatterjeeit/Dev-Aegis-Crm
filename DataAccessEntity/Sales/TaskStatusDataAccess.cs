using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;

namespace DataAccessEntity.Sales
{
    public class TaskStatusDataAccess
    {
        public static List<TaskStatusDbModel> GetAllTaskStatus()
        {
            using (var Context = new CRMContext())
            {
                return Context.TaskStatus.ToList();
            }
        }
        public static TaskStatusDbModel GetTaskStatusById(int Id)
        {
            using (var Context = new CRMContext())
            {
                return Context.TaskStatus.Where(x => x.Id == Id).FirstOrDefault();
            }
        }
        public static int SaveTaskStatus(TaskStatusDbModel Model)
        {
            using (var Context = new CRMContext())
            {
                Context.TaskStatus.AddOrUpdate(Model);
                return Context.SaveChanges();
            }
        }
        public static int DeleteTaskStatus(int Id)
        {
            using (var Context = new CRMContext())
            {
                var Model = Context.TaskStatus.Where(x => x.Id == Id).Single();
                Context.TaskStatus.Remove(Model);
                return Context.SaveChanges();
            }
        }
    }
}
