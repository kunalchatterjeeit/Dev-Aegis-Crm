using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Business.HR
{
    class LeaveManagement
    {
        public int LeaveDesignationConfig_Save(Entity.HR.LeaveMaster objLeaveManagement)
        {
            return DataAccess.HR.LeaveManagement.LeaveDesignationConfig_Save(objLeaveManagement);
        }
        public int LeaveDesignationConfig_GetAll(Entity.HR.LeaveMaster objLeaveManagement)
        {
            return DataAccess.HR.LeaveManagement.LeaveDesignationConfig_GetAll(objLeaveManagement);
        }
        public int LeaveDesignationConfig_Delete(Entity.HR.LeaveMaster objLeaveManagement)
        {
            return DataAccess.HR.LeaveManagement.LeaveDesignationConfig_Delete(objLeaveManagement);
        }
        public int LeaveApplicationMaster_Save(Entity.HR.LeaveMaster objleaveapplicationmaster)
        {
            return DataAccess.HR.LeaveManagement.LeaveApplicationMaster_Save(objleaveapplicationmaster);
        }
        public int LeaveApplicationMaster_GetAll(Entity.HR.LeaveMaster objleaveapplicationmaster)
        {
            return DataAccess.HR.LeaveManagement.LeaveApplicationMaster_GetAll(objleaveapplicationmaster);
        }
        public int LeaveApplicationMaster_Delete(Entity.HR.LeaveMaster objleaveapplicationmaster)
        {
            return DataAccess.HR.LeaveManagement.LeaveApplicationMaster_Delete(objleaveapplicationmaster);
        }
    }
}
