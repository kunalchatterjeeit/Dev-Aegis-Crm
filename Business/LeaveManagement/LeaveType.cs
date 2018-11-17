using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace Business.LeaveManagement
{
    public class LeaveType
    {
        public int LeaveType_Save(Entity.LeaveManagement.LeaveType objLeaveType)
        {
            return DataAccess.LeaveManagement.LeaveType.LeaveType_Save(objLeaveType);
        }
        public DataTable LeaveType_GetAll(Entity.LeaveManagement.LeaveType objLeaveType)
        {
            return DataAccess.LeaveManagement.LeaveType.LeaveType_GetAll();
        }
        public int LeaveType_Delete(Entity.LeaveManagement.LeaveType objLeaveType)
        {
            return DataAccess.LeaveManagement.LeaveType.LeaveType_Delete(objLeaveType);
        }
    }
}
