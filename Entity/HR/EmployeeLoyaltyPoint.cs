using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.HR
{
    public class EmployeeLoyaltyPoint
    {
        public long LoyaltyId { get; set; }
        public int EmployeeId { get; set; }
        public int LoyaltyPointReasonId { get; set; }
        public decimal Point { get; set; }
        public string Month { get; set; }
        public int Year { get; set; }
        public string Note { get; set; }
        public int CreatedBy { get; set; }
    }
}
