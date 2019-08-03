using System;

namespace Entity.ClaimManagement
{
    public class ClaimApplicationDetails
    {
        public ClaimApplicationDetails() { }
        public int ClaimApplicationDetailId { get; set; }
        public int ClaimApplicationId { get; set; }
        public DateTime ExpenseDate { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public decimal Cost { get; set; }
        public decimal ApprovedAmount { get; set; }
        public string Attachment { get; set; }
        public int Status { get; set; }
        public string ApproverRemarks { get; set; }
    }
}
