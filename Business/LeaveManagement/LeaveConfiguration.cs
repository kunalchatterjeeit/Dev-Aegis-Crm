using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Business.LeaveManagement
{
    public class LeaveConfiguration
    {
        public int LeaveConfigurations_Save(Entity.LeaveManagement.LeaveConfiguration objLeaveManagement)
        {
            return DataAccess.LeaveManagement.LeaveConfiguration.LeaveConfigurations_Save(objLeaveManagement);
        }
        public DataTable LeaveConfigurations_GetAll(Entity.LeaveManagement.LeaveConfiguration lmLeaveConfig)
        {
            return DataAccess.LeaveManagement.LeaveConfiguration.LeaveConfigurations_GetAll(lmLeaveConfig);
        }
        public int LeaveConfigurations_Delete(Entity.LeaveManagement.LeaveConfiguration objLeaveManagement)
        {
            return DataAccess.LeaveManagement.LeaveConfiguration.LeaveConfigurations_Delete(objLeaveManagement);
        }

       
    }
}
