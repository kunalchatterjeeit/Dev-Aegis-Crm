using Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Sales
{
    public class Meetings
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public int MeetingTypeId { get; set; }
        public int MeetingStatusId { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public bool PopupReminder { get; set; }
        public bool EmailReminder { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public int MeetingLinkId { get; set; }
        public int LinkId { get; set; }
        public SalesLinkType LinkType { get; set; }
    }
    public class MeetingType
    {       
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class MeetingStatus
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
    public class GetMeetings
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string MeetingType { get; set; }
        public string MeetingStatus { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }

    public class GetMeetingsParam
    {
        public string Name { get; set; }
        public int? MeetingTypeId { get; set; }
        public int? MeetingStatusId { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public int LinkId { get; set; }
        public SalesLinkType LinkType { get; set; }
    }
}
