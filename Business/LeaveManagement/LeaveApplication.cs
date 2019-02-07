using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Business.LeaveManagement
{
    public class LeaveApplication
    {
        public Entity.LeaveManagement.LeaveApplicationMaster LeaveApplicationMaster_Save(Entity.LeaveManagement.LeaveApplicationMaster objLeaveApplicationMaster)
        {
            return DataAccess.LeaveManagement.LeaveApplication.LeaveApplicationMaster_Save(objLeaveApplicationMaster);
        }
        public DataTable LeaveApplicationMaster_GetAll(Entity.LeaveManagement.LeaveApplicationMaster objLeaveApplicationMaster)
        {
            return DataAccess.LeaveManagement.LeaveApplication.LeaveApplicationMaster_GetAll(objLeaveApplicationMaster);
        }
        public int LeaveApplicationMaster_Delete(Entity.LeaveManagement.LeaveApplicationMaster objLeaveApplicationMaster)
        {
            return DataAccess.LeaveManagement.LeaveApplication.LeaveApplicationMaster_Delete(objLeaveApplicationMaster);
        }
        public int LeaveApplicationDetails_Save(Entity.LeaveManagement.LeaveApplicationDetails leaveApplicationDetails)
        {
            return DataAccess.LeaveManagement.LeaveApplication.LeaveApplicationDetails_Save(leaveApplicationDetails);
        }
        public DataTable LeaveApplicationDetails_GetAll(Entity.LeaveManagement.LeaveApplicationDetails leaveApplicationDetails)
        {
            return DataAccess.LeaveManagement.LeaveApplication.LeaveApplicationDetails_GetAll(leaveApplicationDetails);
        }
        public int LeaveApplicationDetails_Delete(int leaveApplicationDetailId)
        {
            return DataAccess.LeaveManagement.LeaveApplication.LeaveApplicationDetails_Delete(leaveApplicationDetailId);
        }
        public DataSet GetLeaveApplicationDetails_ByLeaveApplicationId(int leaveApplicationId)
        {
            return DataAccess.LeaveManagement.LeaveApplication.GetLeaveApplicationDetails_ByLeaveApplicationId(leaveApplicationId);
        }
        public DataTable LeaveApplicationDetails_GetByDate(Entity.LeaveManagement.LeaveApplicationMaster leaveApplicationMaster)
        {
            return DataAccess.LeaveManagement.LeaveApplication.LeaveApplicationDetails_GetByDate(leaveApplicationMaster);
        }
        public DataSet LeaveApplication_GetAll(Entity.LeaveManagement.LeaveApplicationMaster leaveApplicationMaster)
        {
            return DataAccess.LeaveManagement.LeaveApplication.LeaveApplication_GetAll(leaveApplicationMaster);
        }
    }
}
