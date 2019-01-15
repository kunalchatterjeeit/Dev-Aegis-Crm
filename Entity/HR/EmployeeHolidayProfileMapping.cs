using System;

namespace Entity.HR
{
    public class EmployeeHolidayProfileMapping:HolidayProfile
    {
        public EmployeeHolidayProfileMapping()
        {

        }
        public int EmployeeHolidayProfileMappingId { get; set; }
        public int EmployeeId { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Active { get; set; }
    }
}
