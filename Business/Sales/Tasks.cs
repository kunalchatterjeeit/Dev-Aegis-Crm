using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Common;
using DataAccessEntity.Sales;

namespace Business.Sales
{
    public class Tasks
    {
        public Tasks() { }
        public List<Entity.Sales.TaskStatus> GetTaskStatus()
        {
            List<Entity.Sales.TaskStatus> TaskStatusList = new List<Entity.Sales.TaskStatus>();
            TasksDataAccess.GetTaskStatus().CopyListTo(TaskStatusList);
            return TaskStatusList;
        }
        public List<Entity.Sales.TaskRelatedTo> GetTaskRelatedTo()
        {
            List<Entity.Sales.TaskRelatedTo> TaskRelatedList = new List<Entity.Sales.TaskRelatedTo>();
            TasksDataAccess.GetTaskRelatedTo().CopyListTo(TaskRelatedList);
            return TaskRelatedList;
        }
        public List<Entity.Sales.TaskPriority> GetTaskPriority()
        {
            List<Entity.Sales.TaskPriority> TaskPriorityList = new List<Entity.Sales.TaskPriority>();
            TasksDataAccess.GetTaskPriority().CopyListTo(TaskPriorityList);
            return TaskPriorityList;
        }
        public List<Entity.Sales.GetTasks> GetAllTasks(Entity.Sales.GetTasksParam Param)
        {
            List<Entity.Sales.GetTasks> AllTaskList = new List<Entity.Sales.GetTasks>();
            GetTasksParamDbModel p = new GetTasksParamDbModel();
            Param.CopyPropertiesTo(p);
            TasksDataAccess.GetAllTasks(p).CopyListTo(AllTaskList);
            return AllTaskList;
        }
        public Entity.Sales.Tasks GetTaskById(int Id)
        {
            Entity.Sales.Tasks Task = new Entity.Sales.Tasks();
            TasksDataAccess.GetTaskById(Id).CopyPropertiesTo(Task);
            return Task;
        }
        public int SaveTasks(Entity.Sales.Tasks Model)
        {
            TasksDbModel DbModel = new TasksDbModel();
            Model.CopyPropertiesTo(DbModel);
            return TasksDataAccess.SaveTasks(DbModel);
        }
        public int DeleteTasks(int Id)
        {
            return TasksDataAccess.DeleteTask(Id);
        }
    }
}
