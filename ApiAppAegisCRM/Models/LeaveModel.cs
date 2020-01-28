using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAppAegisCRM.Models
{
    public class LeaveModel
    {
        public LeaveModel() { }
        public string LeaveType { get; set; }
        public string LeaveDuration { get; set; }
    }
}