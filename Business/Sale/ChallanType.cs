using System.Data;

namespace Business.Sale
{
    public class ChallanType
    {
        public DataTable Sale_ChallanType_GetAll()
        {
            return DataAccess.Sale.ChallanType.Sale_ChallanType_GetAll();
        }
    }
}
