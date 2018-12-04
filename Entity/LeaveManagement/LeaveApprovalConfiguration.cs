using System;
using Entity.HR;

namespace Entity.LeaveManagement
{
    public class LeaveApprovalConfiguration
    {
        public LeaveApprovalConfiguration() { }

        public int LeaveApprovalConfigurationId { get; set; }
        public int LeaveDesignationConfigurationId { get; set; }
        public int ApproverDesignationId { get; set; }
        public int ApprovalLevel { get; set; }
        public int EmployeeId { get; set; }
        public int CreatedBy { get; set; }
        public long LeaveEmployeeWiseApprovalConfigurationId { get; set; }
        public int ApproverId { get; set; }
    }
}
