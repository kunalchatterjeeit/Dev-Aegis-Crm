using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.LeaveManagement
{
    public class LeaveConfiguration
    {
        public LeaveConfiguration()
        { }

        public int LeaveDesignationConfigId { get; set; }
       
        public decimal LeaveCount { get; set; }
        public DateTime CreateDate { get; set; }
        public string Active { get; set; }

        public int LeaveTypeId { get; set; }

        public int LeaveConfigId { get; set; }
        public string LeaveFrequency { get; set; }
        public int CarryForwardCount { get; set; }
        public int Encashable { get; set; }
        public int CreatedDate { get; set; }

        public DateTime LeaveAccureDate = DateTime.MinValue;
        public DateTime leaveAccureDate
        {
            get { return LeaveAccureDate; }
            set { LeaveAccureDate = value; }
        }

    }

}
