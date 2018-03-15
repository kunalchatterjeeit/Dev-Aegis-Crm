using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Business.HR
{
    public class RoleMaster
    {
        public RoleMaster()
        {
        }

        public int Save(Entity.HR.RoleMaster Role)
        {
            return DataAccess.HR.RoleMaster.Save(Role);
        }

        public DataTable GetAll()
        {
            return DataAccess.HR.RoleMaster.GetAll();
        }

        public int Delete(int RoleId)
        {
            return DataAccess.HR.RoleMaster.Delete(RoleId);
        }
    }
}
