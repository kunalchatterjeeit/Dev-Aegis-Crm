using System.Data;

namespace Business.HR
{
    public class LoyaltyPointReasonMaster
    {
        public LoyaltyPointReasonMaster()
        {
        }

        public int Save(Entity.HR.LoyaltyPointReasonMaster city)
        {
            return DataAccess.HR.LoyaltyPointReasonMaster.Save(city);
        }
        public static DataTable GetAll(Entity.HR.LoyaltyPointReasonMaster city)
        {
            return DataAccess.HR.LoyaltyPointReasonMaster.GetAll(city);
        }
        public Entity.HR.LoyaltyPointReasonMaster GetById(int cityid)
        {
            return DataAccess.HR.LoyaltyPointReasonMaster.GetById(cityid);
        }
        public int Delete(int cityid)
        {
            return DataAccess.HR.LoyaltyPointReasonMaster.Delete(cityid);
        }
    }
}
