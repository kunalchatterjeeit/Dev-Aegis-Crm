using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Business.LeaveManagement
{
    public class LeaveDesignation
    {
        public int LeaveDesignationSave(Entity.LeaveManagement.LeaveDesignation objLeaveDesignation)
        {
            return DataAccess.LeaveManagement.LeaveDesignation.LeaveDesignationConfig_Save(objLeaveDesignation);
        }
        public DataTable LeaveDesignationGetAll(Entity.LeaveManagement.LeaveDesignation objLeaveDesignation)
        {
            return DataAccess.LeaveManagement.LeaveDesignation.LeaveDesignationConfig_GetAll();
        }
        public int LeaveConfigurations_Delete(Entity.LeaveManagement.LeaveDesignation objLeaveDesignation)
        {
            return DataAccess.LeaveManagement.LeaveDesignation.LeaveDesignationConfig_Delete(objLeaveDesignation);
        }

    }
}
