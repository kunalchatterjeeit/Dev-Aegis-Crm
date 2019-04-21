using System;
using System;
using System.Collections.Generic;
using DataAccessEntity.Sales;
using Business.Common;

namespace Business.Sales
{
    public class Campaign
    {
        public Campaign() { }
        public List<Entity.Sales.GetCampaign> GetAllCampaign(Entity.Sales.GetCampaignParam Param)
        {
            List<Entity.Sales.GetCampaign> CampaignList = new List<Entity.Sales.GetCampaign>();
            GetCampaignParamDbModel p = new GetCampaignParamDbModel();
            Param.CopyPropertiesTo(p);
            CampaignDataAccess.GetAllCampaigns(p).CopyListTo(CampaignList);
            return CampaignList;
        }
        public int SaveCampaign(Entity.Sales.Campaign Model)
        {
            CampaignDbModel DbModel = new CampaignDbModel();
            Model.CopyPropertiesTo(DbModel);
            return CampaignDataAccess.SaveCampaign(DbModel);
        }
        public Entity.Sales.Campaign GetCampaignById(int Id)
        {
            Entity.Sales.Campaign Campaign = new Entity.Sales.Campaign();
            CampaignDataAccess.GetCampaignById(Id).CopyPropertiesTo(Campaign);
            return Campaign;
        }
        public int DeleteCampaign(int Id)
        {
            return CampaignDataAccess.DeleteCampaign(Id);
        }
    }
}
