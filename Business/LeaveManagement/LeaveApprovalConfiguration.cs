using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.LeaveManagement
{
    public class LeaveApprovalConfiguration
    {
        public LeaveApprovalConfiguration() { }

        public int LeaveApprovalConfig_Save(Entity.LeaveManagement.LeaveApprovalConfiguration leaveApprovalConfiguration)
        {
            return DataAccess.LeaveManagement.LeaveApprovalConfiguration.LeaveApprovalConfig_Save(leaveApprovalConfiguration);
        }

        public DataTable LeaveApprovalConfig_GetAll(Entity.LeaveManagement.LeaveApprovalConfiguration leaveApprovalConfiguration)
        {
            return DataAccess.LeaveManagement.LeaveApprovalConfiguration.LeaveApprovalConfig_GetAll(leaveApprovalConfiguration);
        }

        public int LeaveApprovalConfig_Delete(int leaveApprovalConfigurationId)
        {
            return DataAccess.LeaveManagement.LeaveApprovalConfiguration.LeaveApprovalConfig_Delete(leaveApprovalConfigurationId);
        }

        public int LeaveEmployeeWiseApprovalConfiguration_Save(Entity.LeaveManagement.LeaveApprovalConfiguration leaveApprovalConfiguration)
        {
            return DataAccess.LeaveManagement.LeaveApprovalConfiguration.LeaveEmployeeWiseApprovalConfiguration_Save(leaveApprovalConfiguration);
        }

        public int LeaveEmployeeWiseApprovalConfiguration_Delete(long leaveEmployeeWiseApprovalConfigurationId)
        {
            return DataAccess.LeaveManagement.LeaveApprovalConfiguration.LeaveEmployeeWiseApprovalConfiguration_Delete(leaveEmployeeWiseApprovalConfigurationId);
        }

        public DataTable LeaveEmployeeWiseApprovalConfiguration_GetAll(Entity.LeaveManagement.LeaveApprovalConfiguration leaveApprovalConfiguration)
        {
            return DataAccess.LeaveManagement.LeaveApprovalConfiguration.LeaveEmployeeWiseApprovalConfiguration_GetAll(leaveApprovalConfiguration);
        }
    }
}
