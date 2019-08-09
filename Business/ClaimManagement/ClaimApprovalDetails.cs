using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ClaimManagement
{
    public class ClaimApprovalDetails
    {
        public int ClaimApprovalDetails_Save(Entity.ClaimManagement.ClaimApprovalDetails ClaimApprovalDetails)
        {
            return DataAccess.ClaimManagement.ClaimApprovalDetails.ClaimApprovalDetails_Save(ClaimApprovalDetails);
        }

        public DataTable GetClaimApplications_ByApproverId(int approverId, int statusId, DateTime fromApplicationDate, DateTime toApplicationDate)
        {
            return DataAccess.ClaimManagement.ClaimApprovalDetails.GetClaimApplications_ByApproverId(approverId, statusId, fromApplicationDate, toApplicationDate);
        }

        public int ClaimApprove(Entity.ClaimManagement.ClaimApprovalDetails ClaimApprovalDetails)
        {
            return DataAccess.ClaimManagement.ClaimApprovalDetails.ClaimApprove(ClaimApprovalDetails);
        }

        public DataTable ClaimApprovalDetails_ByRequestorId(int requestorId, int statusId)
        {
            return DataAccess.ClaimManagement.ClaimApprovalDetails.ClaimApprovalDetails_ByRequestorId(requestorId, statusId);
        }
    }
}
