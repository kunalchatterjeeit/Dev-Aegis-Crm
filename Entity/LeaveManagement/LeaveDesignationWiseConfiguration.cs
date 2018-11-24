using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.LeaveManagement
{
    public class LeaveDesignationWiseConfiguration
    {
        public int LeaveDesignationConfigId { get; set; }
        public decimal LeaveCount { get; set; }
        public string Active { get; set; }
        public int DesignationId { get; set; }
        public int LeaveTypeId { get; set; }
        public decimal CarryForwardCount { get; set; }       
        public DateTime CreatedDate { get; set; }
    }

}
