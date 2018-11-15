using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Business.LeaveManagement
{
   public class LeaveManagement
    {
        public int LeaveDesignationConfig_Save(Entity.LeaveManagement.LeaveManagement objLeaveManagement)
        {
            return DataAccess.LeaveManagement.LeaveManagement.LeaveDesignationConfig_Save(objLeaveManagement);
        }
        public int LeaveDesignationConfig_GetAll(Entity.LeaveManagement.LeaveManagement objLeaveManagement)
        {
            return DataAccess.LeaveManagement.LeaveManagement.LeaveDesignationConfig_GetAll(objLeaveManagement);
        }
        public int LeaveDesignationConfig_Delete(Entity.LeaveManagement.LeaveManagement objLeaveManagement)
        {
            return DataAccess.LeaveManagement.LeaveManagement.LeaveDesignationConfig_Delete(objLeaveManagement);
        }
        public int LeaveApplicationMaster_Save(Entity.LeaveManagement.LeaveManagement objleaveapplicationmaster)
        {
            return DataAccess.LeaveManagement.LeaveManagement.LeaveApplicationMaster_Save(objleaveapplicationmaster);
        }
        public int LeaveApplicationMaster_GetAll(Entity.LeaveManagement.LeaveManagement objleaveapplicationmaster)
        {
            return DataAccess.LeaveManagement.LeaveManagement.LeaveApplicationMaster_GetAll(objleaveapplicationmaster);
        }
        public int LeaveApplicationMaster_Delete(Entity.LeaveManagement.LeaveManagement objleaveapplicationmaster)
        {
            return DataAccess.LeaveManagement.LeaveManagement.LeaveApplicationMaster_Delete(objleaveapplicationmaster);
        }
        public int LeaveConfigurations_Save(Entity.LeaveManagement.LeaveManagement objLeaveManagement)
        {
            return DataAccess.LeaveManagement.LeaveManagement.LeaveApplicationMaster_Save(objLeaveManagement);
        }
        public int LeaveConfigurations_GetAll(Entity.LeaveManagement.LeaveManagement objLeaveManagement)
        {
            return DataAccess.LeaveManagement.LeaveManagement.LeaveApplicationMaster_GetAll(objLeaveManagement);
        }
        public int LeaveConfigurations_Delete(Entity.LeaveManagement.LeaveManagement objLeaveManagement)
        {
            return DataAccess.LeaveManagement.LeaveManagement.LeaveApplicationMaster_GetAll(objLeaveManagement);
        }

        public DataTable LeaveConfigurations_GetAll(LeaveManagement objbelLeaveConfig)
        {
            throw new NotImplementedException();
        }
    }
}
