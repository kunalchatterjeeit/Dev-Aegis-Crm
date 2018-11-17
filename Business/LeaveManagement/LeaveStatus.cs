using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Business.LeaveManagement
{
    public class LeaveStatus
    {
        public int LeaveType_Save(Entity.LeaveManagement.LeaveStatus objLeaveStatus)
        {
            return DataAccess.LeaveManagement.LeaveStatus.LeaveStatus_Save(objLeaveStatus);
        }
        public DataTable LeaveType_GetAll(Entity.LeaveManagement.LeaveStatus objLeaveStatus)
        {
            return DataAccess.LeaveManagement.LeaveStatus.LeaveStatus_GetAll();
        }
        public int LeaveType_Delete(Entity.LeaveManagement.LeaveStatus objLeaveStatus)
        {
            return DataAccess.LeaveManagement.LeaveStatus.LeaveStatus_Delete(objLeaveStatus);
        }
    }
}
