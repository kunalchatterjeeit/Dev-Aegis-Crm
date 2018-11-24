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
       
        public string Active { get; set; }
        public int CreatedDate { get; set; }
        public int LeaveTypeId { get; set; }
        public int LeaveConfigId { get; set; }
        public string LeaveFrequency { get; set; }
        public int Encashable { get; set; }
        public DateTime LeaveAccrueDate { get; set; }
    }

}
