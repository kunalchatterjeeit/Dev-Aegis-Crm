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
        public int LeaveApplicationMasterSave(Entity.LeaveManagement.LeaveApplicationMaster objLeaveApplicationMaster)
        {
            return DataAccess.LeaveManagement.LeaveApplicationMaster.LeaveApplicationMaster_Save(objLeaveApplicationMaster);
        }
        public DataTable LeaveApplicationMasterGetAll(Entity.LeaveManagement.LeaveApplicationMaster objLeaveApplicationMaster)
        {
            return DataAccess.LeaveManagement.LeaveApplicationMaster.LeaveApplicationMaster_GetAll();
        }
        public int LeaveApplicationMasterDelete(Entity.LeaveManagement.LeaveApplicationMaster objLeaveApplicationMaster)
        {
            return DataAccess.LeaveManagement.LeaveApplicationMaster.LeaveApplicationMaster_Delete(objLeaveApplicationMaster);
        }

    }
}
