using System.Data;

namespace Business.HR
{
    public class HolidayProfile
    {
        public HolidayProfile()
        {

        }
        public DataTable HolidayProfile_GetAll(Entity.HR.HolidayProfile holidayProfile)
        {
            return DataAccess.HR.HolidayProfile.HolidayProfile_GetAll(holidayProfile);
        }
        public DataTable HolidayProfile_GetById(int holidayProfileId)
        {
            return DataAccess.HR.HolidayProfile.HolidayProfile_GetById(holidayProfileId);
        }
        public int HolidayProfile_Save(Entity.HR.HolidayProfile holidayProfile)
        {
            return DataAccess.HR.HolidayProfile.HolidayProfile_Save(holidayProfile);
        }
        public int HolidayProfile_Delete(int holidayProfileId)
        {
            return DataAccess.HR.HolidayProfile.HolidayProfile_Delete(holidayProfileId);
        }
    }
}
