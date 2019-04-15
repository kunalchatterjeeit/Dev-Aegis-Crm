using Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessEntity.Sales
{
    public class TasksDbModel
    {
        [Key]
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
        public int LinkId { get; set; }
        public SalesLinkType LinkType { get; set; }
    }
    public class TaskPriorityDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class TaskRelatedToDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class TaskStatusDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }
    }
    public class GetTasksDbModel
    {
        public Int64 Id { get; set; }
        public string Subject { get; set; }       
        public string TaskStatus { get; set; }
        public string TaskPriority { get; set; }
        public string TaskRelatedTo { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }

    public class GetTasksParamDbModel
    {
        public string Subject { get; set; }
        public int? TaskStatusId { get; set; }
        public int? TaskPriorityId { get; set; }
        public int? TaskRelatedToId { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
    }
}
