using Entity.Common;
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
                                    new SqlParameter("Name", (!string.IsNullOrEmpty(Param.Name))?Param.Name:(object)DBNull.Value),
                                    new SqlParameter("ContactId", (Param.ContactId>0)?Param.ContactId:(object)DBNull.Value),
                                    new SqlParameter("LinkId", (Param.LinkId>0)?Param.LinkId:(object)DBNull.Value),
                                    new SqlParameter("LinkType", (Param.LinkType != SalesLinkType.None)?(int)Param.LinkType:(object)DBNull.Value)
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

        public static int SaveNoteLinks(NotesDbModel Model)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.ExecuteSqlCommand(
                                "exec dbo.[usp_Sales_NoteLinks_Save] @Id,@NoteId,@LinkId,@LinkType",
                                new Object[]
                                {
                                    new SqlParameter("Id", Model.NoteLinkId),
                                    new SqlParameter("NoteId", Model.Id),
                                    new SqlParameter("LinkId", Model.LinkId),
                                    new SqlParameter("LinkType", Model.LinkId)
                                }
                             );
            }
        }
    }
}
