using System.Data;

namespace Business.LeaveManagement
{
    public class LeaveApprovalDetails
    {
        public int LeaveApprovalDetails_Save(Entity.LeaveManagement.LeaveApprovalDetails leaveApprovalDetails)
        {
            return DataAccess.LeaveManagement.LeaveApprovalDetails.LeaveApprovalDetails_Save(leaveApprovalDetails);
        }

        public DataTable GetLeaveApplications_ByApproverId(int approverId, int statusId)
        {
            return DataAccess.LeaveManagement.LeaveApprovalDetails.GetLeaveApplications_ByApproverId(approverId, statusId);
        }

        public int LeaveApprove(Entity.LeaveManagement.LeaveApprovalDetails leaveApprovalDetails)
        {
            return DataAccess.LeaveManagement.LeaveApprovalDetails.LeaveApprove(leaveApprovalDetails);
        }
    }
}
