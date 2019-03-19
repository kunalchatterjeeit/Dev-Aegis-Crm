using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessEntity.Sales
{
    public class TasksDataAccess
    {
        public static List<TaskPriorityDbModel> GetTaskPriority()
        {
            using (var Context = new CRMContext())
            {
                return Context.TaskPriority.ToList();
            }
        }
        public static List<TaskRelatedToDbModel> GetTaskRelatedTo()
        {
            using (var Context = new CRMContext())
            {
                return Context.TaskRelatedTo.ToList();
            }
        }
        public static List<TaskStatusDbModel> GetTaskStatus()
        {
            using (var Context = new CRMContext())
            {
                return Context.TaskStatus.ToList();
            }
        }
        public static List<GetTasksDbModel> GetAllTasks(GetTasksParamDbModel Param)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.SqlQuery<GetTasksDbModel>(
                                "exec dbo.[usp_Sales_Tasks_GetAll] @Subject,@TasksStatusId,@TasksPriorityId,@TasksRelatedId,@StartFromDateTime,@StartToDateTime",
                                new Object[]
                                {
                                    new SqlParameter("Subject", DBNull.Value),
                                    new SqlParameter("TasksStatusId", DBNull.Value),
                                    new SqlParameter("TasksPriorityId", DBNull.Value),
                                    new SqlParameter("TasksRelatedId", DBNull.Value),
                                    new SqlParameter("StartFromDateTime", DBNull.Value),
                                    new SqlParameter("StartToDateTime", DBNull.Value)
                                }
                             ).ToList();
            }
        }
        public static TasksDbModel GetTaskById(int Id)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.SqlQuery<TasksDbModel>(
                                "exec dbo.[usp_Sales_Tasks_GetById] @Id",
                                new Object[]
                                {
                                    new SqlParameter("Id", Id)
                                }
                             ).FirstOrDefault();
            }
        }
        public static int SaveTasks(TasksDbModel Model)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.ExecuteSqlCommand(
                                "exec dbo.[usp_Sales_Tasks_Save] @Id,@Subject,@Description,@StartDateTime,@EndDateTime," +
                                "@TasksPriorityId,@TasksStatusId,@TasksRelatedTo,@CreatedBy,@IsActive",
                                new Object[]
                                {
                                    new SqlParameter("Id", Model.Id),
                                    new SqlParameter("Subject", Model.Subject),
                                    new SqlParameter("Description", Model.Description),                                   
                                    new SqlParameter("StartDateTime", Model.StartDateTime),
                                    new SqlParameter("EndDateTime", Model.EndDateTime),
                                    new SqlParameter("TasksPriorityId", Model.TasksPriorityId),
                                    new SqlParameter("TasksStatusId", Model.TasksStatusId),
                                    new SqlParameter("TasksRelatedTo", Model.TasksRelatedTo),                                    
                                    new SqlParameter("CreatedBy", Model.CreatedBy),
                                    new SqlParameter("IsActive", Model.IsActive)
                                }
                             );
            }
        }
        public static int DeleteTask(int Id)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.ExecuteSqlCommand(
                                "exec dbo.[usp_Sales_Tasks_Delete] @Id",
                                new Object[]
                                {
                                    new SqlParameter("Id",Id)
                                }
                             );
            }
        }
    }
}
