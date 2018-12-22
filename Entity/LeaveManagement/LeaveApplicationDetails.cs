using System;

namespace Entity.LeaveManagement
{
    public class LeaveApplicationDetails
    {
        public LeaveApplicationDetails() { }
        public int LeaveApplicationDetailId { get; set; }
        public int LeaveApplicationId { get; set; }
        public DateTime LeaveDate { get; set; }
    }
}
