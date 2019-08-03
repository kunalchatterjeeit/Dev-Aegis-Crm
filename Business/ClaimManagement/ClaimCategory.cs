using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ClaimManagement
{
    public class ClaimCategory
    {
        public ClaimCategory()
        {

        }

        public int ClaimCategorySave(Entity.ClaimManagement.ClaimCategory objClaimCategory)
        {
            return DataAccess.ClaimManagement.ClaimCategory.ClaimCategory_Save(objClaimCategory);
        }
        public DataTable ClaimCategoryGetAll(Entity.ClaimManagement.ClaimCategory objClaimCategory)
        {
            return DataAccess.ClaimManagement.ClaimCategory.ClaimCategory_GetAll();
        }
        public int ClaimCategoryDelete(Entity.ClaimManagement.ClaimCategory objClaimCategory)
        {
            return DataAccess.ClaimManagement.ClaimCategory.ClaimCategory_Delete(objClaimCategory);
        }
    }
}
