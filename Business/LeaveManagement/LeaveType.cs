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
        public int LeaveTypeSave(Entity.LeaveManagement.LeaveType objLeaveType)
        {
            return DataAccess.LeaveManagement.LeaveType.LeaveType_Save(objLeaveType);
        }
        public DataTable LeaveTypeGetAll(Entity.LeaveManagement.LeaveType objLeaveType)
        {
            return DataAccess.LeaveManagement.LeaveType.LeaveType_GetAll();
        }
        public int LeaveTypeDelete(Entity.LeaveManagement.LeaveType objLeaveType)
        {
            return DataAccess.LeaveManagement.LeaveType.LeaveType_Delete(objLeaveType);
        }
    }
}
