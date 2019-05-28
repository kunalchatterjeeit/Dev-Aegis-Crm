using Business.Common;
using DataAccessEntity.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Sales
{
    public class Assignment
    {
        public Assignment() { }
        public List<Entity.Sales.GetAssignment> GetAllAssignments(Entity.Sales.GetAssignmentParam Param)
        {
            List<Entity.Sales.GetAssignment> AllAssignmentList = new List<Entity.Sales.GetAssignment>();
            GetAssignmentParamDbModel p = new GetAssignmentParamDbModel();
            Param.CopyPropertiesTo(p);
            AssignmentDataAccess.GetAllAssignments(p).CopyListTo(AllAssignmentList);
            return AllAssignmentList;
        }
        public int AssignmentAllocation(Entity.Sales.AssignmentAllocation Model)
        {
            AssignmentAllocationDbModel DbModel = new AssignmentAllocationDbModel();
            Model.CopyPropertiesTo(DbModel);
            return AssignmentDataAccess.AssignmentAllocation(DbModel);
        }
    }
}
