using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.LeaveManagement
{
   public class LeaveApplicationMaster
    {
        public int LeaveApplicationId { get; set; }
        public string LeaveApplicationNumber { get; set; }
        public int RequestorId { get; set; }
        public int LeaveTypeId { get; set; }       
        public int LeaveAccumulationTypeId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int LeaveStatusId { get; set; }
        public string Reason { get; set; }
        public string Attachment { get; set; }
        public DateTime ApplyDate { get; set; }
        public DateTime FromLeaveStartDate { get; set; }
        public DateTime ToLeaveStartDate { get; set; }
        public DateTime FromApplyDate { get; set; }
        public DateTime ToApplyDate { get; set; }
    }
}
