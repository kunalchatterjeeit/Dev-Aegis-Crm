using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Business.HR
{
    public class RoleAccessLevel
    {
        public RoleAccessLevel()
        {
        }

        public void Save(int RoleId, int PermissionId, bool isChecked)
        {
            DataAccess.HR.RoleAccessLevel.Save(RoleId, PermissionId, isChecked);
        }

        public DataTable GetByRoleId(int RoleId)
        {
            return DataAccess.HR.RoleAccessLevel.GetByRoleId(RoleId);
        }
    }
}
