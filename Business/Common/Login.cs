using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Common
{
    public class Login
    {
        public Login() { }

        public DataTable GetLoginDetails()
        {
            return DataAccess.Common.Login.GetAll();
        }
        public DataTable GetLastLogin(int employeeId)
        {
            return DataAccess.Common.Login.GetLastLogin(employeeId);
        }
    }
}
