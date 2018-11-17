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
        public DataTable LeaveDesignationConfig_GetAll(Entity.LeaveManagement.LeaveManagement objLeaveManagement)
        {
            return DataAccess.LeaveManagement.LeaveManagement.LeaveDesignationConfig_GetAll();
        }
        public int LeaveDesignationConfig_Delete(Entity.LeaveManagement.LeaveManagement objLeaveManagement)
        {
            return DataAccess.LeaveManagement.LeaveManagement.LeaveDesignationConfig_Delete(objLeaveManagement);
        }   
    }
}
