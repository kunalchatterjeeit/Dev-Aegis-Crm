using Business.Common;
using DataAccessEntity.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Sales
{
    public class Opportunity
    {
        public Opportunity() { }
        public List<Entity.Sales.CommitStage> GetCommitStage()
        {
            List<Entity.Sales.CommitStage> CommitStageList = new List<Entity.Sales.CommitStage>();
            OpportunityDataAccess.GetCommitStage().CopyListTo(CommitStageList);
            return CommitStageList;
        }
        public List<Entity.Sales.LeadSource> GetAllLeadSource()
        {
            List<Entity.Sales.LeadSource> LeadSourceList = new List<Entity.Sales.LeadSource>();
            LeadSourceDataAccess.GetAllLeadSource().CopyListTo(LeadSourceList);
            return LeadSourceList;
        }
        public List<Entity.Sales.GetCampaign> GetAllCampaign(Entity.Sales.GetCampaignParam Param)
        {
            List<Entity.Sales.GetCampaign> CampaignList = new List<Entity.Sales.GetCampaign>();
            GetCampaignParamDbModel p = new GetCampaignParamDbModel();
            Param.CopyPropertiesTo(p);
            CampaignDataAccess.GetAllCampaigns(p).CopyListTo(CampaignList);
            return CampaignList;
        }
        public List<Entity.Sales.GetOpportunity> GetAllLeads(Entity.Sales.GetOpportunityParam Param)
        {
            List<Entity.Sales.GetOpportunity> AllOpportunityList = new List<Entity.Sales.GetOpportunity>();
            GetOpportunityParamDbModel p = new GetOpportunityParamDbModel();
            Param.CopyPropertiesTo(p);
            OpportunityDataAccess.GetAllOpportunities(p).CopyListTo(AllOpportunityList);
            return AllOpportunityList;
        }
        public int SaveOpportunity(Entity.Sales.Opportunity Model)
        {
            OpportunityDbModel DbModel = new OpportunityDbModel();
            Model.CopyPropertiesTo(DbModel);
            return OpportunityDataAccess.SaveOpportunities(DbModel);
        }
        public Entity.Sales.Opportunity GetOpportunityById(int Id)
        {
            Entity.Sales.Opportunity Opportunity = new Entity.Sales.Opportunity();
            OpportunityDataAccess.GetOpportunityById(Id).CopyPropertiesTo(Opportunity);
            return Opportunity;
        }
        public int DeleteOpportunities(int Id)
        {
            return OpportunityDataAccess.DeleteOpportunities(Id);
        }
    }
}
