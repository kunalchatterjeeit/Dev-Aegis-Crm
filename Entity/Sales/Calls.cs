using Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Sales
{
    public class Calls
    {
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

    public class CallRepeatType
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class CallRelated
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class CallDirection
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class GetCalls
    {
        public Int64 Id { get; set; }
        public string Subject { get; set; }
        public string Name { get; set; }
        public string CallStatus { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }

    public class GetCallsParam
    {
        public string Subject { get; set; }
        public int? CallStatusId { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public int LinkId { get; set; }
        public SalesLinkType LinkType { get; set; }
        public int AssignEngineer { get; set; }
    }
}
