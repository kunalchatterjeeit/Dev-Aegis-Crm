using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessEntity.Sales
{
    public class LeadsDataAccess
    {
        public static List<DepartmentDbModel> GetDepartments()
        {
            using (var Context = new CRMContext())
            {
                return Context.Department.ToList();
            }
        }
        public static List<GetLeadsDbModel> GetAllLeads(GetLeadsParamDbModel Param)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.SqlQuery<GetLeadsDbModel>(
                                "exec dbo.[usp_Sales_Leads_GetAll] @Name,@Email,@DepartmentId,@CampaignId",
                                new Object[]
                                {
                                    new SqlParameter("Name", DBNull.Value),
                                    new SqlParameter("Email", DBNull.Value),
                                    new SqlParameter("DepartmentId", DBNull.Value),
                                    new SqlParameter("CampaignId", DBNull.Value)
                                }
                             ).ToList();
            }
        }
        public static int SaveLeads(LeadsDbModel Model)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.ExecuteSqlCommand(
                                "exec dbo.[usp_Sales_Leads_Save] @Id,@Name,@Description,@Website,@Email,@LeadScore," +
                                "@PrimaryAddress,@AlternateAddress,@DepartmentId,@OfficePhone,@Fax,@CampaignId,@CreatedBy,@IsActive",
                                new Object[]
                                {
                                    new SqlParameter("Id", Model.Id),
                                    new SqlParameter("Name", Model.Name),
                                    new SqlParameter("Description", Model.Description),
                                    new SqlParameter("Website", Model.Website),
                                    new SqlParameter("Email", Model.Email),
                                    new SqlParameter("LeadScore", Model.LeadScore==null?(object)DBNull.Value:Model.LeadScore),
                                    new SqlParameter("PrimaryAddress", Model.PrimaryAddress),
                                    new SqlParameter("AlternateAddress", Model.AlternateAddress),
                                    new SqlParameter("DepartmentId", Model.DepartmentId==null?(object)DBNull.Value:Model.DepartmentId),
                                    new SqlParameter("OfficePhone", Model.OfficePhone),
                                    new SqlParameter("Fax", Model.Fax),
                                    new SqlParameter("CampaignId", Model.CampaignId==null?(object)DBNull.Value:Model.CampaignId),
                                    new SqlParameter("CreatedBy", Model.CreatedBy),
                                    new SqlParameter("IsActive", Model.IsActive)
                                }
                             );
            }
        }
        public static LeadsDbModel GetLeadById(int Id)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.SqlQuery<LeadsDbModel>(
                                "exec dbo.[usp_Sales_Leads_GetById] @Id",
                                new Object[]
                                {
                                    new SqlParameter("Id", Id)
                                }
                             ).FirstOrDefault();
            }
        }
        public static int DeleteLeads(int Id)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.ExecuteSqlCommand(
                                "exec dbo.[usp_Sales_Leads_Delete] @Id",
                                new Object[]
                                {
                                    new SqlParameter("Id",Id)
                                }
                             );
            }
        }
    }
}
