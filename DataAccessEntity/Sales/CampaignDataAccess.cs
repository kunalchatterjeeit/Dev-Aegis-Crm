using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessEntity.Sales
{
    public class CampaignDataAccess
    {
        public static List<CampaignDbModel> GetCampaigns()
        {
            using (var Context = new CRMContext())
            {
                return Context.Campaign.Where(x => x.IsActive == true && x.IsDeleted == false).ToList();
            }
        }
    }
}
