using System.Data;

namespace Business.LeaveManagement
{
    public class LeaveGenerateLog
    {
        public long LeaveGenerateLog_Save(Entity.LeaveManagement.LeaveGenerateLog leaveGenerateLog)
        {
            return DataAccess.LeaveManagement.LeaveGenerateLog.LeaveGenerateLog_Save(leaveGenerateLog);
        }

        public DataTable LeaveGenerateLog_GetAll(Entity.LeaveManagement.LeaveGenerateLog leaveGenerateLog)
        {
            return DataAccess.LeaveManagement.LeaveGenerateLog.LeaveGenerateLog_GetAll(leaveGenerateLog);
        }
    }
}
