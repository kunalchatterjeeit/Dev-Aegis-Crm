
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.HR
{
    public class EmployeeLoyaltyPoint
    {
        public EmployeeLoyaltyPoint()
        { }
        public DataTable EmployeeLoyaltyPoint_GetAll(string month, int year)
        {
            return DataAccess.HR.EmployeeLoyaltyPoint.EmployeeLoyaltyPoint_GetAll(month, year);
        }
        public int EmployeeLoyaltyPoint_Save(Entity.HR.EmployeeLoyaltyPoint employeeLoyaltyPoint)
        {
            return DataAccess.HR.EmployeeLoyaltyPoint.EmployeeLoyaltyPoint_Save(employeeLoyaltyPoint);
        }
        public int EmployeeLoyaltyPoint_Delete(long loyaltyid)
        {
            return DataAccess.HR.EmployeeLoyaltyPoint.EmployeeLoyaltyPoint_Delete(loyaltyid);
        }
    }
}
