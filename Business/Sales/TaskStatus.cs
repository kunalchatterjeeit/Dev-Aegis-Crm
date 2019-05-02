using Business.Common;
using DataAccessEntity.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Sales
{
    public class TaskStatus
    {
        public TaskStatus() { }
        public List<Entity.Sales.TaskStatus> GetAllTaskStatus()
        {
            List<Entity.Sales.TaskStatus> TaskStatusList = new List<Entity.Sales.TaskStatus>();
            TaskStatusDataAccess.GetAllTaskStatus().CopyListTo(TaskStatusList);
            return TaskStatusList;
        }
        public Entity.Sales.TaskStatus GetTaskStatusById(int Id)
        {
            Entity.Sales.TaskStatus TaskStatus = new Entity.Sales.TaskStatus();
            TaskStatusDataAccess.GetTaskStatusById(Id).CopyPropertiesTo(TaskStatus);
            return TaskStatus;
        }
        public int SaveTaskStatus(Entity.Sales.TaskStatus Model)
        {
            TaskStatusDbModel DbModel = new TaskStatusDbModel();
            Model.CopyPropertiesTo(DbModel);
            return TaskStatusDataAccess.SaveTaskStatus(DbModel);
        }
        public int DeleteTaskStatus(int Id)
        {
            return TaskStatusDataAccess.DeleteTaskStatus(Id);
        }
    }
}
