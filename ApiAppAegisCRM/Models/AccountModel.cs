using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiAppAegisCRM.Models
{
    public class AccountModel : BaseModel
    {
        public string ImageProfile { get; set; }
        public string EmployeeName { get; set; }
        public string Designation { get; set; }
        public string ReportsTo { get; set; }
        public string LastLogin { get; set; }
        public decimal LoyaltyPoint { get; set; }
        public string AttendanceState { get; set; }
    }
}