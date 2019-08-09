using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ClaimManagement
{
    public class ClaimStatus
    {
        public int ClaimStatus_Save(Entity.ClaimManagement.ClaimStatus objClaimStatus)
        {
            return DataAccess.ClaimManagement.ClaimStatus.ClaimStatus_Save(objClaimStatus);
        }
        public DataTable ClaimStatus_GetAll(Entity.ClaimManagement.ClaimStatus objClaimStatus)
        {
            return DataAccess.ClaimManagement.ClaimStatus.ClaimStatus_GetAll();
        }
        public int ClaimStatus_Delete(Entity.ClaimManagement.ClaimStatus objClaimStatus)
        {
            return DataAccess.ClaimManagement.ClaimStatus.ClaimStatus_Delete(objClaimStatus);
        }
    }
}
