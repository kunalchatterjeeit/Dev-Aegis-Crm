using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessEntity.Sales
{
    public class AssignmentDataAccess
    {
        public static List<GetAssignmentDbModel> GetAllAssignments(GetAssignmentParamDbModel Param)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.SqlQuery<GetAssignmentDbModel>(
                                "exec dbo.[usp_Sales_Assignment_GetByActivityId] @ActivityId,@ActivityTypeId",
                                new Object[]
                                {
                                    new SqlParameter("ActivityId", Param.ActivityId),
                                    new SqlParameter("ActivityTypeId", Param.ActivityTypeId)
                                }
                             ).ToList();
            }
        }
        public static int AssignmentAllocation(AssignmentAllocationDbModel Model)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.ExecuteSqlCommand(
                                "exec dbo.[usp_Sales_Assignment_Allocation] @ActivityTypeId,@ActivityId,@EmployeeId,@AssignedBy,@IsActive," +
                                "@RevokedBy,@IsLead",
                                new Object[]
                                {
                                    new SqlParameter("ActivityTypeId", Model.ActivityTypeId),
                                    new SqlParameter("ActivityId", Model.ActivityId),
                                    new SqlParameter("EmployeeId", Model.EmployeeId),
                                    new SqlParameter("AssignedBy", Model.AssignedBy==null?(object)DBNull.Value:Model.AssignedBy),                                   
                                    new SqlParameter("IsActive", Model.IsActive),
                                    new SqlParameter("RevokedBy", Model.RevokedBy==null?(object)DBNull.Value:Model.RevokedBy),
                                    new SqlParameter("IsLead", Model.IsLead==null?(object)DBNull.Value:Model.IsLead)
                                }
                             );
            }
        }
    }
}
