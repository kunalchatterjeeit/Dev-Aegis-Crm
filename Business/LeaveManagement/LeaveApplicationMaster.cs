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
        public Entity.LeaveManagement.LeaveApplicationMaster LeaveApplicationMaster_Save(Entity.LeaveManagement.LeaveApplicationMaster objLeaveApplicationMaster)
        {
            return DataAccess.LeaveManagement.LeaveApplicationMaster.LeaveApplicationMaster_Save(objLeaveApplicationMaster);
        }
        public DataTable LeaveApplicationMaster_GetAll(Entity.LeaveManagement.LeaveApplicationMaster objLeaveApplicationMaster)
        {
            return DataAccess.LeaveManagement.LeaveApplicationMaster.LeaveApplicationMaster_GetAll(objLeaveApplicationMaster);
        }
        public int LeaveApplicationMaster_Delete(Entity.LeaveManagement.LeaveApplicationMaster objLeaveApplicationMaster)
        {
            return DataAccess.LeaveManagement.LeaveApplicationMaster.LeaveApplicationMaster_Delete(objLeaveApplicationMaster);
        }

        public int LeaveApplicationDetails_Save(Entity.LeaveManagement.LeaveApplicationDetails leaveApplicationDetails)
        {
            return DataAccess.LeaveManagement.LeaveApplicationMaster.LeaveApplicationDetails_Save(leaveApplicationDetails);
        }
        public DataTable LeaveApplicationDetails_GetAll(Entity.LeaveManagement.LeaveApplicationDetails leaveApplicationDetails)
        {
            return DataAccess.LeaveManagement.LeaveApplicationMaster.LeaveApplicationDetails_GetAll(leaveApplicationDetails);
        }
        public int LeaveApplicationDetails_Delete(int leaveApplicationDetailId)
        {
            return DataAccess.LeaveManagement.LeaveApplicationMaster.LeaveApplicationDetails_Delete(leaveApplicationDetailId);
        }
        public DataSet GetLeaveApplicationDetails_ByLeaveApplicationId(int leaveApplicationId)
        {
            return DataAccess.LeaveManagement.LeaveApplicationMaster.GetLeaveApplicationDetails_ByLeaveApplicationId(leaveApplicationId);
        }
    }
}
