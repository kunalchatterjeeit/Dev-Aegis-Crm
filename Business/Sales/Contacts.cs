using Business.Common;
using DataAccessEntity.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Sales
{
    public class Contacts
    {
        public Contacts() { }
        public List<Entity.Sales.DesignationMaster> GetDesignations()
        {
            List<Entity.Sales.DesignationMaster> DesignationList = new List<Entity.Sales.DesignationMaster>();
            ContactsDataAccess.GetDesignation().CopyListTo(DesignationList);
            return DesignationList;
        }
        public List<Entity.Sales.GetAccounts> GetAllAccounts(Entity.Sales.GetAccountsParam Param)
        {
            List<Entity.Sales.GetAccounts> AllAccountList = new List<Entity.Sales.GetAccounts>();
            GetAccountsParamDbModel p = new GetAccountsParamDbModel();
            Param.CopyPropertiesTo(p);
            AccountDataAccess.GetAllAccounts(p).CopyListTo(AllAccountList);
            return AllAccountList;
        }
        public List<Entity.Sales.GetContacts> GetAllContacts(Entity.Sales.GetContactsParam Param)
        {
            List<Entity.Sales.GetContacts> AllContactList = new List<Entity.Sales.GetContacts>();
            GetContactsParamDbModel p = new GetContactsParamDbModel();
            Param.CopyPropertiesTo(p);
            ContactsDataAccess.GetAllContacts(p).CopyListTo(AllContactList);
            return AllContactList;
        }
        public int SaveContacts(Entity.Sales.Contacts Model)
        {
            ContactsDbModel DbModel = new ContactsDbModel();
            Model.CopyPropertiesTo(DbModel);
            return ContactsDataAccess.SaveContacts(DbModel);
        }
        public Entity.Sales.Contacts GetContactById(int Id)
        {
            Entity.Sales.Contacts Contact = new Entity.Sales.Contacts();
            ContactsDataAccess.GetContactById(Id).CopyPropertiesTo(Contact);
            return Contact;
        }
        public int DeleteContacts(int Id)
        {
            return ContactsDataAccess.DeleteContacts(Id);
        }
    }
}
