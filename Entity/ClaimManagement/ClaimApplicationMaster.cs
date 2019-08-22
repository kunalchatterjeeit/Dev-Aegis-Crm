using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ClaimManagement
{
    public class ClaimApplicationMaster
    {
        public int ClaimApplicationId { get; set; }
        public string ClaimApplicationNumber { get; set; }
        public string ClaimHeading { get; set; }
        public int EmployeeId { get; set; }
        public DateTime PeriodFrom { get; set; }
        public DateTime PeriodTo { get; set; }
        public DateTime ClaimDateTime { get; set; }
        public int CreatedBy { get; set; }
        public decimal TotalAmount { get; set; }
        public int Status { get; set; }
        public decimal ApprovedAmount { get; set; }
        public decimal AdjustRequestAmount { get; set; }
    }
}
