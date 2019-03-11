using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityDataAccess.Sales
{
    public class CallsEntity
    {
        [Key]
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public int CallStatusId { get; set; }
        public int CallRepeatTypeId { get; set; }
        public int CallDirectionId { get; set; }
        public int CallRelatedTo { get; set; }
        public bool PopupReminder { get; set; }
        public bool EmailReminder { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
    }
    public class CallStatus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class CallRepeatType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class CallRelated
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class CallDirection
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
