using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessEntity.Sales
{
    public class NotesDataAccess
    {
        public static List<ContactsDbModel> GetContacts()
        {
            using (var Context = new CRMContext())
            {
                return Context.Contacts.Where(x => x.IsActive == true && x.IsDeleted == false).ToList();
            }
        }
        public static List<GetNotesDbModel> GetAllNotes(GetNotesParamDbModel Param)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.SqlQuery<GetNotesDbModel>(
                                "exec [dbo].[usp_Sales_Notes_GetAll] @Name,@ContactId",
                                new Object[]
                                {
                                    new SqlParameter("Name", DBNull.Value),
                                    new SqlParameter("ContactId", DBNull.Value)
                                }
                             ).ToList();
            }
        }
        public static NotesDbModel GetNoteById(int Id)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.SqlQuery<NotesDbModel>(
                                "exec [dbo].[usp_Sales_Notes_GetById] @Id",
                                new Object[]
                                {
                                    new SqlParameter("Id", Id)
                                }
                             ).FirstOrDefault();
            }
        }
        public static int SaveNotes(NotesDbModel Model)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.ExecuteSqlCommand(
                                "exec [dbo].[usp_Sales_Notes_Save] @Id,@Name,@Description,@ContactId,@CreatedBy,@IsActive",
                                new Object[]
                                {
                                    new SqlParameter("Id", Model.Id),
                                    new SqlParameter("Name", Model.Name),
                                    new SqlParameter("Description", Model.Description),
                                    new SqlParameter("ContactId", Model.ContactId),
                                    new SqlParameter("CreatedBy", Model.CreatedBy),
                                    new SqlParameter("IsActive", Model.IsActive)
                                }
                             );
            }
        }
        public static int DeleteNotes(int Id)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.ExecuteSqlCommand(
                                "exec [dbo].[usp_Sales_Notes_Delete] @Id",
                                new Object[]
                                {
                                    new SqlParameter("Id",Id)
                                }
                             );
            }
        }
    }
}
