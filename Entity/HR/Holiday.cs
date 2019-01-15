using System;

namespace Entity.HR
{
    public class Holiday : HolidayProfile
    {
        public Holiday()
        {

        }
        public int HolidayId { get; set; }
        public string HolidayName { get; set; }
        public DateTime HolidayDate { get; set; }
        public string HolidayDescription { get; set; }
        public bool Active { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int HolidayYear { get; set; }
        public bool Show { get; set; }
    }
}
