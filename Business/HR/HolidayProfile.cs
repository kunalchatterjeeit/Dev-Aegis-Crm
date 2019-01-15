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
        public int EmployeeHolidayProfileMapping_Save(Entity.HR.EmployeeHolidayProfileMapping employeeHolidayProfileMapping)
        {
            return DataAccess.HR.HolidayProfile.EmployeeHolidayProfileMapping_Save(employeeHolidayProfileMapping);
        }
        public DataTable EmployeeHolidayProfileMapping_GetById(int employeeHolidayProfileMappingId)
        {
            return DataAccess.HR.HolidayProfile.EmployeeHolidayProfileMapping_GetById(employeeHolidayProfileMappingId);
        }
        public int EmployeeHolidayProfileMapping_Delete(int employeeHolidayProfileMappingId)
        {
            return DataAccess.HR.HolidayProfile.EmployeeHolidayProfileMapping_Delete(employeeHolidayProfileMappingId);
        }
        public DataTable EmployeeHolidayProfileMapping_GetAll(Entity.HR.EmployeeHolidayProfileMapping employeeHolidayProfileMapping)
        {
            return DataAccess.HR.HolidayProfile.EmployeeHolidayProfileMapping_GetAll(employeeHolidayProfileMapping);
        }
    }
}
