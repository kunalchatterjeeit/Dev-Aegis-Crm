using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Business.LeaveManagement
{
    public class LeaveDesignationWiseConfiguration
    {
        public int LeaveDesignationConfig_Save(Entity.LeaveManagement.LeaveDesignationWiseConfiguration objLeaveDesignation)
        {
            return DataAccess.LeaveManagement.LeaveDesignationWiseConfiguration.LeaveDesignationConfig_Save(objLeaveDesignation);
        }
        public DataTable LeaveDesignationConfig_GetAll(Entity.LeaveManagement.LeaveDesignationWiseConfiguration leaveDesignationWiseConfiguration)
        {
            return DataAccess.LeaveManagement.LeaveDesignationWiseConfiguration.LeaveDesignationConfig_GetAll(leaveDesignationWiseConfiguration);
        }
        public int LeaveDesignationConfig_Delete(int leaveDesignationWiseConfigurationId)
        {
            return DataAccess.LeaveManagement.LeaveDesignationWiseConfiguration.LeaveDesignationConfig_Delete(leaveDesignationWiseConfigurationId);
        }
        public DataTable LeaveDesignationConfig_GetById(int leaveDesignationWiseConfigurationId)
        {
            return DataAccess.LeaveManagement.LeaveDesignationWiseConfiguration.LeaveDesignationConfig_GetById(leaveDesignationWiseConfigurationId);
        }
    }
}
