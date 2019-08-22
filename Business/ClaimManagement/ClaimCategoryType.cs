using System.Data;

namespace Business.ClaimManagement
{
    public class ClaimCategoryType
    {
        public ClaimCategoryType()
        {

        }

        public DataTable ClaimCategoryType_GetAll()
        {
            return DataAccess.ClaimManagement.ClaimCategoryType.ClaimCategoryType_GetAll();
        }
    }
}
