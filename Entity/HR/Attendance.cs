using Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.HR
{
    public class Attendance : BaseEntity
    {
        public Attendance() { }

        public long AttendanceId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime AttendanceDate { get; set; }
        public DateTime InDateTime { get; set; }
        public DateTime OutDateTime { get; set; }
        public double TotalHours { get; set; }
        public int CreatedBy { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public DateTime AttendanceFromDate { get; set; }
        public DateTime AttendanceToDate { get; set; }
        public string EmployeeName { get; set; }
        public string Source { get; set; }

    }
}
