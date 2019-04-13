using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataAccessEntity.Sales
{
    public class AccountDataAccess
    {
        public static List<CustomerTypeDbModel> GetCustomerTypes()
        {
            using (var Context = new CRMContext())
            {
                return Context.CustomerType.ToList();
            }
        }
        public static List<LeadSourceDbModel> GetLeadSources()
        {
            using (var Context = new CRMContext())
            {
                return Context.LeadSource.ToList();
            }
        }
        public static List<GetAccountsDbModel> GetAllAccounts(GetAccountsParamDbModel Param)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.SqlQuery<GetAccountsDbModel>(
                                "exec dbo.[usp_Sales_Accounts_GetAll] @Name,@OfficePhone",
                                new Object[]
                                {
                                    new SqlParameter("Name", DBNull.Value),
                                    new SqlParameter("OfficePhone", DBNull.Value)
                                }
                             ).ToList();
            }
        }
        public static int SaveAccounts(AccountsDbModel Model)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.ExecuteSqlCommand(
                                "exec dbo.[usp_Sales_Accounts_Save] @Id,@Name,@Description,@Website,@Industry,@CustomerTypeId," +
                                "@OfficePhone,@EmployeeStrenth,@AnnualRevenue,@AccountScore,@LeadSourceId,@SourceName,@CreatedBy,@IsActive",
                                new Object[]
                                {
                                    new SqlParameter("Id", Model.Id),
                                    new SqlParameter("Name", Model.Name),
                                    new SqlParameter("Description", Model.Description),
                                    new SqlParameter("Website", Model.Website),
                                    new SqlParameter("Industry", Model.Industry),
                                    new SqlParameter("CustomerTypeId", Model.CustomerTypeId==null?(object)DBNull.Value:Model.CustomerTypeId),
                                    new SqlParameter("OfficePhone", Model.OfficePhone),
                                    new SqlParameter("EmployeeStrenth", Model.EmployeeStrength),
                                    new SqlParameter("AnnualRevenue", Model.AnualRevenue==null?(object)DBNull.Value:Model.AnualRevenue),
                                    new SqlParameter("AccountScore", Model.AccountScore==null?(object)DBNull.Value:Model.AccountScore),
                                    new SqlParameter("LeadSourceId", Model.LeadSourceId==null?(object)DBNull.Value:Model.LeadSourceId),
                                    new SqlParameter("SourceName", Model.SourceName),
                                    new SqlParameter("CreatedBy", Model.CreatedBy),
                                    new SqlParameter("IsActive", Model.IsActive)
                                }
                             );
            }
        }
        public static AccountsDbModel GetAccountById(int Id)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.SqlQuery<AccountsDbModel>(
                                "exec dbo.[usp_Sales_Accounts_GetById] @Id",
                                new Object[]
                                {
                                    new SqlParameter("Id", Id)
                                }
                             ).FirstOrDefault();
            }
        }
        public static int DeleteAccount(int Id)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.ExecuteSqlCommand(
                                "exec dbo.[usp_Sales_Accounts_Delete] @Id",
                                new Object[]
                                {
                                    new SqlParameter("Id",Id)
                                }
                             );
            }
        }
    }
}
