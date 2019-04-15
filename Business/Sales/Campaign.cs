using System;
using System;
using System.Collections.Generic;
using DataAccessEntity.Sales;
using Business.Common;

namespace Business.Sales
{
    public class Campaign
    {
        public List<Entity.Sales.Campaigns> GetCampaign()
        {
            List<Entity.Sales.Campaigns> CampaignList = new List<Entity.Sales.Campaigns>();
            CampaignDataAccess.GetCampaigns().CopyListTo(CampaignList);
            return CampaignList;
        }
    }
}
