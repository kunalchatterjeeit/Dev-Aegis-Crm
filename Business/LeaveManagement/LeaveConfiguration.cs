using System.Data;

namespace Business.LeaveManagement
{
    public class LeaveConfiguration
    {
        public int LeaveConfigurations_Save(Entity.LeaveManagement.LeaveConfiguration objLeaveManagement)
        {
            return DataAccess.LeaveManagement.LeaveConfiguration.LeaveConfigurations_Save(objLeaveManagement);
        }
        public static DataTable LeaveConfigurations_GetAll(Entity.LeaveManagement.LeaveConfiguration lmLeaveConfig)
        {
            return DataAccess.LeaveManagement.LeaveConfiguration.LeaveConfigurations_GetAll(lmLeaveConfig);
        }
        /*public int LeaveConfigurations_Delete(Entity.LeaveManagement.LeaveConfiguration objLeaveManagement)
        {
            return DataAccess.LeaveManagement.LeaveConfiguration.LeaveConfigurations_Delete(objLeaveManagement);
        }*/
        public DataTable FetchLeaveConfigById(Entity.LeaveManagement.LeaveConfiguration objLeaveManagement)
        {
            return DataAccess.LeaveManagement.LeaveConfiguration.FetchLeaveConfigById(objLeaveManagement);
        }

        public int LeaveConfigurations_Delete(int leaveConfigId)
        {
            return DataAccess.LeaveManagement.LeaveConfiguration.LeaveConfigurations_Delete(leaveConfigId);
        }
    }
}
