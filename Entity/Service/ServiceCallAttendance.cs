using System;

namespace Entity.Service
{
    public class ServiceCallAttendance : ServiceBook
    {
        public long ServiceCallAttendanceId { get; set; }
        public int EmployeeId { get; set; }
        public int Status { get; set; }
    }
}
