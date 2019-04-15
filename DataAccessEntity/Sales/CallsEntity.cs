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
    public class CallsDbModel
    {
        [Key]
        public Int64 Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public int CallStatusId { get; set; }
        public int CallRepeatTypeId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int CallDirectionId { get; set; }
        public int CallRelatedTo { get; set; }
        public bool PopupReminder { get; set; }
        public bool EmailReminder { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public int CallLinkId { get; set; }
        public int LinkId { get; set; }
        public SalesLinkType LinkType { get; set; }
    }
    public class CallStatusDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class CallRepeatTypeDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class CallRelatedDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class CallDirectionDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class GetCallsDbModel
    {
        public Int64 Id { get; set; }
        public string Subject { get; set; }
        public string Name { get; set; }
        public string CallStatus { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }

    public class GetCallsParamDbModel
    {
        public string Subject { get; set; }
        public int? CallStatusId { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public int LinkId { get; set; }
        public SalesLinkType LinkType { get; set; }
    }
}
