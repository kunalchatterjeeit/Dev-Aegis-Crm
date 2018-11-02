using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.HR
{
    public class LeaveMaster
    {
        public LeaveMaster()
        { }

        public int LeaveDesignationConfigId { get; set; }
        public int LeaveTypeId { get; set; }
        public int DesignationId { get; set; }
        public decimal LeaveCount { get; set; }
        public DateTime CreateDate { get; set; }
        public string Active { get; set; }
       
        public int LeaveApplicationId { get; set; }
        public int LeaveApplicationNumber { get; set; }
        public int RequestorId { get; set; }
        public string LeaveAccumulationTypeId { get; set; }
        public string FromDate { get; set; }
        public int ToDate { get; set; }
        public int LeaveStatusId { get; set; }
        public int Reason { get; set; }
        public int Attachment { get; set; }

    }

}
