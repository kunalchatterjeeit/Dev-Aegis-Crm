using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessEntity.Sales
{
    public class ContactsDataAccess
    {
        public static List<DesignationMasterDbModel> GetDesignation()
        {
            using (var Context = new CRMContext())
            {
                return Context.Designations.ToList();
            }
        }
        public static List<GetContactsDbModel> GetAllContacts(GetContactsParamDbModel Param)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.SqlQuery<GetContactsDbModel>(
                                "exec dbo.[usp_Sales_Contacts_GetAll] @Name,@AccountId,@Mobile",
                                new Object[]
                                {
                                    new SqlParameter("Name", DBNull.Value),
                                    new SqlParameter("AccountId", DBNull.Value),
                                    new SqlParameter("Mobile", DBNull.Value)
                                }
                             ).ToList();
            }
        }
        public static int SaveContacts(ContactsDbModel Model)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.ExecuteSqlCommand(
                                "exec dbo.[usp_Sales_Contacts_Save] @Id,@Name,@Description,@AccountId,@Email,@Mobile," +
                                "@DesignationId,@GSTNo,@OfficePhone,@CreatedBy,@IsActive",
                                new Object[]
                                {
                                    new SqlParameter("Id", Model.Id),
                                    new SqlParameter("Name", Model.Name),
                                    new SqlParameter("Description", Model.Description),
                                    new SqlParameter("AccountId", Model.AccountId==null?(object)DBNull.Value:Model.AccountId),
                                    new SqlParameter("Email", Model.Email),
                                    new SqlParameter("Mobile", Model.Mobile),
                                    new SqlParameter("DesignationId", Model.DesignationId==null?(object)DBNull.Value:Model.DesignationId),
                                    new SqlParameter("GSTNo", Model.GSTNo),
                                    new SqlParameter("OfficePhone", Model.OfficePhone),
                                    new SqlParameter("CreatedBy", Model.CreatedBy),
                                    new SqlParameter("IsActive", Model.IsActive)
                                }
                             );
            }
        }
        public static ContactsDbModel GetContactById(int Id)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.SqlQuery<ContactsDbModel>(
                                "exec dbo.[usp_Sales_Contacts_GetById] @Id",
                                new Object[]
                                {
                                    new SqlParameter("Id", Id)
                                }
                             ).FirstOrDefault();
            }
        }
        public static int DeleteContacts(int Id)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.ExecuteSqlCommand(
                                "exec dbo.[usp_Sales_Contacts_Delete] @Id",
                                new Object[]
                                {
                                    new SqlParameter("Id",Id)
                                }
                             );
            }
        }
    }
}
