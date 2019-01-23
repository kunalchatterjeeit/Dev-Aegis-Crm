using System.Data;

namespace Business.LeaveManagement
{
    public class LeaveAccountBalance
    {
        public LeaveAccountBalance()
        { }

        public DataSet LeaveAccountBalance_ByEmployeeId(int employeeId, int leaveTypeId)
        {
            return DataAccess.LeaveManagement.LeaveAccountBalance.LeaveAccountBalance_ByEmployeeId(employeeId, leaveTypeId);
        }

        public int LeaveAccontBalance_Adjust(Entity.LeaveManagement.LeaveAccountBalance leaveAccountBalance)
        {
            return DataAccess.LeaveManagement.LeaveAccountBalance.LeaveAccontBalance_Adjust(leaveAccountBalance);
        }
    }
}
