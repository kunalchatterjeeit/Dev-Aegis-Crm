using System.Data;

namespace Business.HR
{
    public class Holiday
    {
        public Holiday()
        {

        }
        public DataTable Holiday_GetAll(Entity.HR.Holiday holiday)
        {
            return DataAccess.HR.Holiday.Holiday_GetAll(holiday);
        }
        public DataTable Holiday_GetById(int holiday)
        {
            return DataAccess.HR.Holiday.Holiday_GetById(holiday);
        }
        public int Holiday_Save(Entity.HR.Holiday holiday)
        {
            return DataAccess.HR.Holiday.Holiday_Save(holiday);
        }
        public int Holiday_Delete(int holiday)
        {
            return DataAccess.HR.Holiday.Holiday_Delete(holiday);
        }
    }
}
