using Business.Common;
using DataAccessEntity.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Sales
{
    public class LeadSource
    {
        public LeadSource() { }
        public List<Entity.Sales.LeadSource> GetAllLeadSource()
        {
            List<Entity.Sales.LeadSource> LeadSourceList = new List<Entity.Sales.LeadSource>();
            LeadSourceDataAccess.GetAllLeadSource().CopyListTo(LeadSourceList);
            return LeadSourceList;
        }
        public Entity.Sales.LeadSource GetLeadSourceById(int Id)
        {
            Entity.Sales.LeadSource LeadSource = new Entity.Sales.LeadSource();
            LeadSourceDataAccess.GetLeadSourceById(Id).CopyPropertiesTo(LeadSource);
            return LeadSource;
        }
        public int SaveLeadSource(Entity.Sales.LeadSource Model)
        {
            LeadSourceDbModel DbModel = new LeadSourceDbModel();
            Model.CopyPropertiesTo(DbModel);
            return LeadSourceDataAccess.SaveLeadSource(DbModel);
        }
        public int DeleteLeadSource(int Id)
        {
            return LeadSourceDataAccess.DeleteLeadSource(Id);
        }
    }
}
