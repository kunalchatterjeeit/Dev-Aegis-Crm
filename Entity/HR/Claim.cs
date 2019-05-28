using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.HR
{
    public class Claim
    {
        public Claim() { }

        public int ClaimId { get; set; }
        public string ClaimNo { get; set; }
        public int EmployeeId { get; set; }
        public DateTime PeriodFrom { get; set; }
        public DateTime PeriodTo { get; set; }
        public DateTime ClaimDateTime { get; set; }
        public decimal TotalAmount { get; set; }
        public ClaimStatusEnum ClaimStatus { get; set; }
        public int ApproverId { get; set; }
        public DateTime ApprovalDateTime { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDateTime { get; set; }
    }
}
