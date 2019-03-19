using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Sales
{
    public class Tasks
    {
        public Int64 Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public int TasksPriorityId { get; set; }
        public int TasksRelatedTo { get; set; }
        public int TasksStatusId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
    }
    public class TaskPriority
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class TaskRelatedTo
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class TaskStatus
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
    public class GetTasks
    {
        public Int64 Id { get; set; }
        public string Subject { get; set; }
        public string TaskStatus { get; set; }
        public string TaskPriority { get; set; }
        public string TaskRelatedTo { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }

    public class GetTasksParam
    {
        public string Subject { get; set; }
        public int? TaskStatusId { get; set; }
        public int? TaskPriorityId { get; set; }
        public int? TaskRelatedToId { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
    }
}
