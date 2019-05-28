using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Communication
{
    public class Placeholder
    {
        public Placeholder()
        {

        }
        public DataTable Placeholder_GetByTypeId(int placeholderTypeId)
        {
            return DataAccess.Communication.Placeholder.Placeholder_GetByTypeId(placeholderTypeId);
        }
    }
}
