using System;
using System.Collections.Generic;
using DataAccessEntity.Sales;
using Business.Common;

namespace Business.Sales
{
    public class Leads
    {
        public Leads() { }
        public List<Entity.Sales.Department> GetDepartment()
        {
            List<Entity.Sales.Department> DepartmentList = new List<Entity.Sales.Department>();
            LeadsDataAccess.GetDepartments().CopyListTo(DepartmentList);
            return DepartmentList;
        }
        public List<Entity.Sales.GetLeads> GetAllLeads(Entity.Sales.GetLeadsParam Param)
        {
            List<Entity.Sales.GetLeads> AllLeadList = new List<Entity.Sales.GetLeads>();
            GetLeadsParamDbModel p = new GetLeadsParamDbModel();
            Param.CopyPropertiesTo(p);
            LeadsDataAccess.GetAllLeads(p).CopyListTo(AllLeadList);
            return AllLeadList;
        }
        public int SaveLeads(Entity.Sales.Leads Model)
        {
            LeadsDbModel DbModel = new LeadsDbModel();
            Model.CopyPropertiesTo(DbModel);
            return LeadsDataAccess.SaveLeads(DbModel);
        }
        public Entity.Sales.Leads GetLeadById(int Id)
        {
            Entity.Sales.Leads Lead = new Entity.Sales.Leads();
            LeadsDataAccess.GetLeadById(Id).CopyPropertiesTo(Lead);
            return Lead;
        }
        public int DeleteLeads(int Id)
        {
            return LeadsDataAccess.DeleteLeads(Id);
        }
    }
}
