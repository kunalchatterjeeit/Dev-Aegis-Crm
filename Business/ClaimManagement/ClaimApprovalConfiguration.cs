using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ClaimManagement
{
    public class ClaimApprovalConfiguration
    {
        public ClaimApprovalConfiguration()
        {

        }

        public int ClaimApprovalConfig_Save(Entity.ClaimManagement.ClaimApprovalConfiguration ClaimApprovalConfiguration)
        {
            return DataAccess.ClaimManagement.ClaimApprovalConfiguration.ClaimApprovalConfig_Save(ClaimApprovalConfiguration);
        }

        public DataTable ClaimApprovalConfig_GetAll(Entity.ClaimManagement.ClaimApprovalConfiguration ClaimApprovalConfiguration)
        {
            return DataAccess.ClaimManagement.ClaimApprovalConfiguration.ClaimApprovalConfig_GetAll(ClaimApprovalConfiguration);
        }

        public int ClaimApprovalConfig_Delete(int ClaimApprovalConfigurationId)
        {
            return DataAccess.ClaimManagement.ClaimApprovalConfiguration.ClaimApprovalConfig_Delete(ClaimApprovalConfigurationId);
        }

        public int ClaimEmployeeWiseApprovalConfiguration_Save(Entity.ClaimManagement.ClaimApprovalConfiguration ClaimApprovalConfiguration)
        {
            return DataAccess.ClaimManagement.ClaimApprovalConfiguration.ClaimEmployeeWiseApprovalConfiguration_Save(ClaimApprovalConfiguration);
        }

        public int ClaimEmployeeWiseApprovalConfiguration_Delete(long ClaimEmployeeWiseApprovalConfigurationId)
        {
            return DataAccess.ClaimManagement.ClaimApprovalConfiguration.ClaimEmployeeWiseApprovalConfiguration_Delete(ClaimEmployeeWiseApprovalConfigurationId);
        }

        public DataTable ClaimEmployeeWiseApprovalConfiguration_GetAll(Entity.ClaimManagement.ClaimApprovalConfiguration ClaimApprovalConfiguration)
        {
            return DataAccess.ClaimManagement.ClaimApprovalConfiguration.ClaimEmployeeWiseApprovalConfiguration_GetAll(ClaimApprovalConfiguration);
        }
    }
}
