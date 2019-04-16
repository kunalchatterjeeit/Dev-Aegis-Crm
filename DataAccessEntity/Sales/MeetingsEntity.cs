using Entity.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessEntity.Sales
{
    public class MeetingsDbModel
    {
        [Key]
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
    public class MeetingTypeDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class MeetingStatusDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }
    }
    public class GetMeetingsDbModel
    {
        public Int64 Id { get; set; }
        public string Name { get; set; }
        public string MeetingType { get; set; }
        public string MeetingStatus { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }

    public class GetMeetingsParamDbModel
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
