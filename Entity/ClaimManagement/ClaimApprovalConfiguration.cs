using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ClaimManagement
{
    public class ClaimApprovalConfiguration
    {
        public ClaimApprovalConfiguration()
        {

        }

        public int ClaimApprovalConfigurationId { get; set; }
        public int ClaimDesignationConfigurationId { get; set; }
        public int ApproverDesignationId { get; set; }
        public int ApprovalLevel { get; set; }
        public int EmployeeId { get; set; }
        public int CreatedBy { get; set; }
        public long ClaimEmployeeWiseApprovalConfigurationId { get; set; }
        public int ApproverId { get; set; }
    }
}
