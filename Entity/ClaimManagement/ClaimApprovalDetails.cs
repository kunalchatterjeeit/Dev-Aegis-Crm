using System;

namespace Entity.ClaimManagement
{
    public class ClaimApprovalDetails
    {
        public ClaimApprovalDetails() { }

        public int ClaimApprovalDetailId { get; set; }
        public int ClaimApplicationId { get; set; }
        public int ApproverId { get; set; }
        public int Status { get; set; }
        public DateTime ActionDate { get; set; }
        public string Remarks { get; set; }
    }
}
