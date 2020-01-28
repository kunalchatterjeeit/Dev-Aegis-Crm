using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAppAegisCRM.Models
{
    public class AttendanceModel : BaseModel
    {
        public AttendanceModel() { }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string IsMockedLocation { get; set; }
        public string AttendanceMode { get; set; }
        public string CurrentState { get; set; }
        public bool IsAttendanceBlocked { get; set; }
    }
}