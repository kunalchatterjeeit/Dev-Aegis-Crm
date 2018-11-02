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
        public int LeaveDesignationConfig_Delete(Entity.HR.LeaveMaster ObjElEmployeeMaster)
        {
            return DataAccess.HR.EmployeeMaster.LeaveDesignationConfig_Delete(ObjElEmployeeMaster);
        }
    }
}
