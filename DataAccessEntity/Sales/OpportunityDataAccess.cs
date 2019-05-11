using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessEntity.Sales
{
    public class OpportunityDataAccess
    {
        public static List<CommitStageDbModel> GetCommitStage()
        {
            using (var Context = new CRMContext())
            {
                return Context.CommitStage.ToList();
            }
        }
        public static List<GetOpportunityDbModel> GetAllOpportunities(GetOpportunityParamDbModel Param)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.SqlQuery<GetOpportunityDbModel>(
                                "exec dbo.[usp_Sales_Opportunity_GetAll] @Name,@CommitStageId,@BestPrice",
                                new Object[]
                                {
                                    new SqlParameter("Name", DBNull.Value),
                                    new SqlParameter("CommitStageId", DBNull.Value),
                                    new SqlParameter("BestPrice", DBNull.Value)
                                }
                             ).ToList();
            }
        }
        public static int SaveOpportunities(OpportunityDbModel Model)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.ExecuteSqlCommand(
                                "exec dbo.[usp_Sales_Opportunity_Save] @Id,@Name,@Description,@ExpectedCloseDate,@LikelyPrice,@BestPrice," +
                                "@WorstPrice,@CommitStageId,@LeadSource,@SourceName,@CampaignId,@CreatedBy,@IsActive",
                                new Object[]
                                {
                                    new SqlParameter("Id", Model.Id),
                                    new SqlParameter("Name", Model.Name),
                                    new SqlParameter("Description", Model.Description),
                                    new SqlParameter("ExpectedCloseDate", Model.ExpectedCloseDate==null?(object)DBNull.Value:Model.ExpectedCloseDate),
                                    new SqlParameter("LikelyPrice", Model.LikelyPrice==null?(object)DBNull.Value:Model.LikelyPrice),
                                    new SqlParameter("BestPrice", Model.BestPrice==null?(object)DBNull.Value:Model.BestPrice),
                                    new SqlParameter("WorstPrice", Model.WorstPrice==null?(object)DBNull.Value:Model.WorstPrice),
                                    new SqlParameter("CommitStageId", Model.CommitStageId==null?(object)DBNull.Value:Model.CommitStageId),
                                    new SqlParameter("LeadSource", Model.LeadSource==null?(object)DBNull.Value:Model.LeadSource),
                                    new SqlParameter("SourceName", Model.SourceName),
                                    new SqlParameter("CampaignId", Model.CampaignId==null?(object)DBNull.Value:Model.CampaignId),
                                    new SqlParameter("CreatedBy", Model.CreatedBy),
                                    new SqlParameter("IsActive", Model.IsActive)
                                }
                             );
            }
        }
        public static OpportunityDbModel GetOpportunityById(int Id)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.SqlQuery<OpportunityDbModel>(
                                "exec dbo.[usp_Sales_Opportunity_GetById] @Id",
                                new Object[]
                                {
                                    new SqlParameter("Id", Id)
                                }
                             ).FirstOrDefault();
            }
        }
        public static int DeleteOpportunities(int Id)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.ExecuteSqlCommand(
                                "exec dbo.[usp_Sales_Opportunity_Delete] @Id",
                                new Object[]
                                {
                                    new SqlParameter("Id",Id)
                                }
                             );
            }
        }
    }
}
