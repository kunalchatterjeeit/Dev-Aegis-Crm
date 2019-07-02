using Entity.Common;
using System;
using System.Data;

namespace Business.LeaveManagement
{
    public class LeaveApprovalDetails
    {
        public int LeaveApprovalDetails_Save(Entity.LeaveManagement.LeaveApprovalDetails leaveApprovalDetails)
        {
            return DataAccess.LeaveManagement.LeaveApprovalDetails.LeaveApprovalDetails_Save(leaveApprovalDetails);
        }

        public DataTable GetLeaveApplications_ByApproverId(int approverId, int statusId, LeaveTypeEnum leaveType, DateTime fromApplicationDate, DateTime toApplicationDate)
        {
            return DataAccess.LeaveManagement.LeaveApprovalDetails.GetLeaveApplications_ByApproverId(approverId, statusId, leaveType, fromApplicationDate, toApplicationDate);
        }

        public int LeaveApprove(Entity.LeaveManagement.LeaveApprovalDetails leaveApprovalDetails)
        {
            return DataAccess.LeaveManagement.LeaveApprovalDetails.LeaveApprove(leaveApprovalDetails);
        }

        public DataTable LeaveApprovalDetails_ByRequestorId(int requestorId, int statusId)
        {
            return DataAccess.LeaveManagement.LeaveApprovalDetails.LeaveApprovalDetails_ByRequestorId(requestorId, statusId);
        }
    }
}
