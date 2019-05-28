using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Sales
{
    public class GetAssignmentParam
    {
        public int ActivityId { get; set; }
        public int ActivityTypeId { get; set; }
    }
    public class GetAssignment
    {
        public string EmployeeName { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsLead { get; set; }
        public DateTime? AssignedDate { get; set; }
        public int EmployeeMasterId { get; set; }
        public DateTime? RevokeDate { get; set; }
    }
    public class AssignmentAllocation
    {
        public int ActivityTypeId { get; set; }
        public int ActivityId { get; set; }
        public int EmployeeId { get; set; }
        public int? AssignedBy { get; set; }       
        public bool IsActive { get; set; }        
        public int? RevokedBy { get; set; }
        public bool? IsLead { get; set; }
    }
}
