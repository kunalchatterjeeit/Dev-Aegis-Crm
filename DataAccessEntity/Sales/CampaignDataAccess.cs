using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessEntity.Sales
{
    public class CampaignDataAccess
    {
        public static List<GetCampaignDbModel> GetAllCampaigns(GetCampaignParamDbModel param)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.SqlQuery<GetCampaignDbModel>(
                                "exec dbo.[usp_Sales_Campaign_GetAll] @Name,@StartDate,@EndDate",
                                new Object[]
                                {
                                    new SqlParameter("Name", DBNull.Value),
                                    new SqlParameter("StartDate", DBNull.Value),
                                    new SqlParameter("EndDate", DBNull.Value)
                                }
                             ).ToList();
            }
        }
        public static int SaveCampaign(CampaignDbModel Model)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.ExecuteSqlCommand(
                                "exec dbo.[usp_Sales_Campaign_Save] @Id,@Name,@Description,@StartDate,@EndDate,@Reason," +
                                "@CreatedBy,@IsActive",
                                new Object[]
                                {
                                    new SqlParameter("Id", Model.Id),
                                    new SqlParameter("Name", Model.Name),
                                    new SqlParameter("Description", Model.Description),
                                    new SqlParameter("StartDate",Model.StartDate==null?(object)DBNull.Value:Model.StartDate),
                                    new SqlParameter("EndDate",Model.EndDate==null?(object)DBNull.Value:Model.EndDate),
                                    new SqlParameter("Reason", Model.Reason),
                                    new SqlParameter("CreatedBy", Model.CreatedBy),
                                    new SqlParameter("IsActive", Model.IsActive)
                                }
                             );
            }
        }
        public static CampaignDbModel GetCampaignById(int Id)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.SqlQuery<CampaignDbModel>(
                                "exec dbo.[usp_Sales_Campaign_GetById] @Id",
                                new Object[]
                                {
                                    new SqlParameter("Id", Id)
                                }
                             ).FirstOrDefault();
            }
        }
        public static int DeleteCampaign(int Id)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.ExecuteSqlCommand(
                                "exec dbo.[usp_Sales_Campaign_Delete] @Id",
                                new Object[]
                                {
                                    new SqlParameter("Id",Id)
                                }
                             );
            }
        }
    }
}
