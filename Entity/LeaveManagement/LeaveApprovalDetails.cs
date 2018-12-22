using System;

namespace Entity.LeaveManagement
{
    public class LeaveApprovalDetails
    {
        public LeaveApprovalDetails() { }

        public int LeaveApprovalDetailId { get; set; }
        public int LeaveApplicationId { get; set; }
        public int ApproverId { get; set; }
        public int Status { get; set; }
        public DateTime ActionDate { get; set; }
        public string Remarks { get; set; }
    }
}
