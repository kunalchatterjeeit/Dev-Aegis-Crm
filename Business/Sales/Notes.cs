using DataAccessEntity.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Common;

namespace Business.Sales
{
    public class Notes
    {
        public Notes()
        {

        }
        public List<Entity.Sales.Contacts> GetAllContacts()  // need to point contact data access. changes required.
        {
            List<Entity.Sales.Contacts> AllContactList = new List<Entity.Sales.Contacts>();
            NotesDataAccess.GetContacts().CopyListTo(AllContactList);
            return AllContactList;
        }
        public List<Entity.Sales.GetNotes> GetAllNotes(Entity.Sales.GetNotesParam Param)
        {
            List<Entity.Sales.GetNotes> AllNoteList = new List<Entity.Sales.GetNotes>();
            GetNotesParamDbModel p = new GetNotesParamDbModel
            {
                Name = Param.Name,
                ContactId = Param.ContactId
            };
            NotesDataAccess.GetAllNotes(p).CopyListTo(AllNoteList);
            return AllNoteList;
        }
        public Entity.Sales.Notes GetNoteById(int Id)
        {
            Entity.Sales.Notes note = new Entity.Sales.Notes();
            NotesDataAccess.GetNoteById(Id).CopyPropertiesTo(note);
            return note;
        }
        public int SaveNotes(Entity.Sales.Notes Model)
        {
            NotesDbModel DbModel = new NotesDbModel
            {
                Id = Model.Id,
                Name = Model.Name,
                Description = Model.Description,
                ContactId = Model.ContactId,
                CreatedBy = Model.CreatedBy,
                IsActive = Model.IsActive
            };
            return NotesDataAccess.SaveNotes(DbModel);
        }
        public int DeleteNotes(int Id)
        {
            return NotesDataAccess.DeleteNotes(Id);
        }
    }
}
