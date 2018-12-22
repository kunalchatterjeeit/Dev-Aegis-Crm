using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.LeaveManagement
{
    public class LeaveAccountBalance
    {
        public long LeaveAccountBalanceId { get; set; }
        public int EmployeeId { get; set; }
        public int LeaveTypeId { get; set; }
        public decimal Amount { get; set; }
        public long LeaveAccountBalanceDetailsId { get; set; }
        public string Reason { get; set; }
    }
}
