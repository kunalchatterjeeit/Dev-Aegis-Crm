using System.Data;

namespace Business.ClaimManagement
{
    public class ClaimApplication
    {
        public ClaimApplication()
        {

        }
        public Entity.ClaimManagement.ClaimApplicationMaster ClaimApplicationMaster_Save(Entity.ClaimManagement.ClaimApplicationMaster objClaimApplicationMaster)
        {
            return DataAccess.ClaimManagement.ClaimApplication.ClaimApplicationMaster_Save(objClaimApplicationMaster);
        }
        //public DataTable ClaimApplicationMaster_GetAll(Entity.ClaimManagement.ClaimApplicationMaster objClaimApplicationMaster)
        //{
        //    return DataAccess.ClaimManagement.ClaimApplication.ClaimApplicationMaster_GetAll(objClaimApplicationMaster);
        //}
        public int ClaimApplicationMaster_Delete(Entity.ClaimManagement.ClaimApplicationMaster objClaimApplicationMaster)
        {
            return DataAccess.ClaimManagement.ClaimApplication.ClaimApplicationMaster_Delete(objClaimApplicationMaster);
        }
        public int ClaimApplicationDetails_Save(Entity.ClaimManagement.ClaimApplicationDetails ClaimApplicationDetails)
        {
            return DataAccess.ClaimManagement.ClaimApplication.ClaimApplicationDetails_Save(ClaimApplicationDetails);
        }
        public DataTable ClaimApplicationDetails_GetAll(Entity.ClaimManagement.ClaimApplicationDetails ClaimApplicationDetails)
        {
            return DataAccess.ClaimManagement.ClaimApplication.ClaimApplicationDetails_GetAll(ClaimApplicationDetails);
        }
        public int ClaimApplicationDetails_Delete(int ClaimApplicationDetailId)
        {
            return DataAccess.ClaimManagement.ClaimApplication.ClaimApplicationDetails_Delete(ClaimApplicationDetailId);
        }
        public DataSet GetClaimApplicationDetails_ByClaimApplicationId(int ClaimApplicationId)
        {
            return DataAccess.ClaimManagement.ClaimApplication.GetClaimApplicationDetails_ByClaimApplicationId(ClaimApplicationId);
        }
        public DataTable ClaimApplicationDetails_GetByDate(Entity.ClaimManagement.ClaimApplicationMaster ClaimApplicationMaster)
        {
            return DataAccess.ClaimManagement.ClaimApplication.ClaimApplicationDetails_GetByDate(ClaimApplicationMaster);
        }
        public DataTable ClaimApplication_GetAll(Entity.ClaimManagement.ClaimApplicationMaster ClaimApplicationMaster)
        {
            return DataAccess.ClaimManagement.ClaimApplication.ClaimApplication_GetAll(ClaimApplicationMaster);
        }
        public int Claim_HeadingUpdate(Entity.ClaimManagement.ClaimApplicationMaster claimApplication)
        {
            return DataAccess.ClaimManagement.ClaimApplication.Claim_HeadingUpdate(claimApplication);
        }
        public int Claim_StatusUpdate(Entity.ClaimManagement.ClaimApplicationMaster claimApplication)
        {
            return DataAccess.ClaimManagement.ClaimApplication.Claim_StatusUpdate(claimApplication);
        }
    }
}
