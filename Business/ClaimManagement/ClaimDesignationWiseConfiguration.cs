using System.Data;

namespace Business.ClaimManagement
{
    public class ClaimDesignationWiseConfiguration
    {
        public ClaimDesignationWiseConfiguration()
        {

        }

        public int ClaimDesignationConfig_Save(Entity.ClaimManagement.ClaimDesignationWiseConfiguration objClaimDesignation)
        {
            return DataAccess.ClaimManagement.ClaimDesignationWiseConfiguration.ClaimDesignationConfig_Save(objClaimDesignation);
        }
        public DataTable ClaimDesignationConfig_GetAll(Entity.ClaimManagement.ClaimDesignationWiseConfiguration ClaimDesignationWiseConfiguration)
        {
            return DataAccess.ClaimManagement.ClaimDesignationWiseConfiguration.ClaimDesignationConfig_GetAll(ClaimDesignationWiseConfiguration);
        }
        public int ClaimDesignationConfig_Delete(int ClaimDesignationWiseConfigurationId)
        {
            return DataAccess.ClaimManagement.ClaimDesignationWiseConfiguration.ClaimDesignationConfig_Delete(ClaimDesignationWiseConfigurationId);
        }
        public DataTable ClaimDesignationConfig_GetById(int ClaimDesignationWiseConfigurationId)
        {
            return DataAccess.ClaimManagement.ClaimDesignationWiseConfiguration.ClaimDesignationConfig_GetById(ClaimDesignationWiseConfigurationId);
        }
    }
}
