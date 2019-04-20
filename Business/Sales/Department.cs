using Business.Common;
using DataAccessEntity.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Sales
{
    public class Department
    {
        public Department() { }
        public List<Entity.Sales.Department> GetAllDepartment()
        {
            List<Entity.Sales.Department> callDepartmentList = new List<Entity.Sales.Department>();
            DepartmentDataAccess.GetAllDepartment().CopyListTo(callDepartmentList);
            return callDepartmentList;
        }
        public Entity.Sales.Department GetDepartmentById(int Id)
        {
            Entity.Sales.Department Department = new Entity.Sales.Department();
            DepartmentDataAccess.GetDepartmentById(Id).CopyPropertiesTo(Department);
            return Department;
        }
        public int SaveDepartment(Entity.Sales.Department Model)
        {
            DepartmentDbModel DbModel = new DepartmentDbModel();
            Model.CopyPropertiesTo(DbModel);
            return DepartmentDataAccess.SaveDepartment(DbModel);
        }
        public int DeleteDepartment(int Id)
        {
            return DepartmentDataAccess.DeleteDepartment(Id);
        }
    }
}
