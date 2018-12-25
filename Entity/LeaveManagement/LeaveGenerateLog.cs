using System;

namespace Entity.LeaveManagement
{
    public class LeaveGenerateLog
    {
        public long LeaveGenerateLogId { get; set; }
        public int LeaveTypeId { get; set; }
        public string Month { get; set; }
        public string Quarter { get; set; }
        public int Year { get; set; }
        public int TotalDistribution { get; set; }
        public DateTime ScheduleDateTime { get; set; }
    }
}
