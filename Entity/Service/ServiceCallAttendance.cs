using System;

namespace Entity.Service
{
    public class ServiceCallAttendance
    {
        public long ServiceCallAttendanceId { get; set; }
        public long ServiceBookId { get; set; }
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }
        public int EmployeeId { get; set; }
        public int CallStatusId { get; set; }
        public int Status { get; set; }
    }
}
