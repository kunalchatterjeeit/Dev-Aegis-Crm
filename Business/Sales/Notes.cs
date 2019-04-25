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
        public List<Entity.Sales.GetContacts> GetAllContacts(Entity.Sales.GetContactsParam Param)
        {
            List<Entity.Sales.GetContacts> AllContactList = new List<Entity.Sales.GetContacts>();
            GetContactsParamDbModel p = new GetContactsParamDbModel();
            Param.CopyPropertiesTo(p);
            ContactsDataAccess.GetAllContacts(p).CopyListTo(AllContactList);
            return AllContactList;
        }
        public List<Entity.Sales.GetNotes> GetAllNotes(Entity.Sales.GetNotesParam Param)
        {
            List<Entity.Sales.GetNotes> AllNoteList = new List<Entity.Sales.GetNotes>();
            GetNotesParamDbModel p = new GetNotesParamDbModel();
            Param.CopyPropertiesTo(p);
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
            NotesDbModel DbModel = new NotesDbModel();
            Model.CopyPropertiesTo(DbModel);
            return NotesDataAccess.SaveNotes(DbModel);
        }
        public int DeleteNotes(int Id)
        {
            return NotesDataAccess.DeleteNotes(Id);
        }
        public int SaveNoteLinks(Entity.Sales.Notes Model)
        {
            NotesDbModel DbModel = new NotesDbModel();
            Model.CopyPropertiesTo(DbModel);
            return NotesDataAccess.SaveNoteLinks(DbModel);
        }
    }
}
