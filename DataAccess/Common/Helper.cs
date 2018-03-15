using Entity.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Common
{
    public static class Helper
    {
        public static SqlCommand InsertPaging(this SqlCommand sqlCommand, BaseEntity entity)
        {
            int pageIndex = (entity.PageIndex < 0) ? 0 : entity.PageIndex * entity.PageSize;
            if (entity.PageIndex == 0)
                sqlCommand.Parameters.AddWithValue("@PageIndex", DBNull.Value);
            else
                sqlCommand.Parameters.AddWithValue("@PageIndex", pageIndex);
            if (entity.PageSize == 0)
                sqlCommand.Parameters.AddWithValue("@PageSize", DBNull.Value);
            else
                sqlCommand.Parameters.AddWithValue("@PageSize", entity.PageSize);
            return sqlCommand;
        }
    }
}
