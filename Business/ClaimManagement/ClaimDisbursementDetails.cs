using System.Data;

namespace Business.ClaimManagement
{
    public class ClaimDisbursementDetails
    {
        public int ClaimDisbursementDetails_Save(Entity.ClaimManagement.ClaimDisbursementDetails claimDisbursementDetails)
        {
            return DataAccess.ClaimManagement.ClaimDisbursementDetails.ClaimDisbursementDetails_Save(claimDisbursementDetails);
        }

        public DataTable ClaimDisbursementDetails_GetById(int claimDisbursementDetailsId)
        {
            return DataAccess.ClaimManagement.ClaimDisbursementDetails.ClaimDisbursementDetails_GetById(claimDisbursementDetailsId);
        }

        public DataTable ClaimDisbursementDetails_GetAll(Entity.ClaimManagement.ClaimDisbursementDetails claimDisbursementDetails)
        {
            return DataAccess.ClaimManagement.ClaimDisbursementDetails.ClaimDisbursementDetails_GetAll(claimDisbursementDetails);
        }

    }
}
