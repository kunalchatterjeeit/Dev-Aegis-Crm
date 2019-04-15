using System;
using System.Collections.Generic;
using DataAccessEntity.Sales;
using Business.Common;

namespace Business.Sales
{
    public class Account
    {
        public Account() { }
        public List<Entity.Sales.CustomerType> GetCustomerType()
        {
            List<Entity.Sales.CustomerType> CustomerTypeList = new List<Entity.Sales.CustomerType>();
            AccountDataAccess.GetCustomerTypes().CopyListTo(CustomerTypeList);
            return CustomerTypeList;
        }
        public List<Entity.Sales.LeadSource> GetLeadSource()
        {
            List<Entity.Sales.LeadSource> LeadSourceList = new List<Entity.Sales.LeadSource>();
            AccountDataAccess.GetLeadSources().CopyListTo(LeadSourceList);
            return LeadSourceList;
        }
        public List<Entity.Sales.GetAccounts> GetAllAccounts(Entity.Sales.GetAccountsParam Param)
        {
            List<Entity.Sales.GetAccounts> AllAccountList = new List<Entity.Sales.GetAccounts>();
            GetAccountsParamDbModel p = new GetAccountsParamDbModel();
            Param.CopyPropertiesTo(p);
            AccountDataAccess.GetAllAccounts(p).CopyListTo(AllAccountList);
            return AllAccountList;
        }
        public int SaveAccounts(Entity.Sales.Accounts Model)
        {
            AccountsDbModel DbModel = new AccountsDbModel();
            Model.CopyPropertiesTo(DbModel);
            return AccountDataAccess.SaveAccounts(DbModel);
        }
        public Entity.Sales.Accounts GetAccountById(int Id)
        {
            Entity.Sales.Accounts Account = new Entity.Sales.Accounts();
            AccountDataAccess.GetAccountById(Id).CopyPropertiesTo(Account);
            return Account;
        }
        public int DeleteAccounts(int Id)
        {
            return AccountDataAccess.DeleteAccount(Id);
        }
    }
}
