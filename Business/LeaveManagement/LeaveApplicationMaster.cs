using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Business.LeaveManagement
{
    public class LeaveApplicationMaster
    {
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
    }
}
