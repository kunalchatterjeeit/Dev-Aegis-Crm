using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ClaimManagement
{
    public class ClaimDisbursement
    {
        public int ClaimDisbursement_Save(Entity.ClaimManagement.ClaimDisbursement claimDisbursement)
        {
            return DataAccess.ClaimManagement.ClaimDisbursement.ClaimDisbursement_Save(claimDisbursement);
        }

        public DataTable ClaimDisbursement_GetById(int claimDisbursementId)
        {
            return DataAccess.ClaimManagement.ClaimDisbursement.ClaimDisbursement_GetById(claimDisbursementId);
        }

        public DataTable ClaimDisbursement_GetAll(Entity.ClaimManagement.ClaimDisbursement claimDisbursement)
        {
            return DataAccess.ClaimManagement.ClaimDisbursement.ClaimDisbursement_GetAll(claimDisbursement);
        }

        public decimal GetClaimAccountBalance(int employeeId)
        {
            return DataAccess.ClaimManagement.ClaimDisbursement.GetClaimAccountBalance(employeeId);
        }
    }
}
