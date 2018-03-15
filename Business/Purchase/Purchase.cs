using System;
using System.Data;

namespace Business.Purchase
{
    public class Purchase
    {
        public Purchase()
        { }

        public int Purchase_Save(Entity.Purchase.Purchase purchase)
        {
            return DataAccess.Purchase.Purchase.Purchase_Save(purchase);
        }

        public int PurchaseDetails_Save(Entity.Purchase.Purchase purchase)
        {
            return DataAccess.Purchase.Purchase.PurchaseDetails_Save(purchase);
        }

        public DataTable Purchase_GetAll(Entity.Purchase.Purchase purchase)
        {
            return DataAccess.Purchase.Purchase.Purchase_GetAll(purchase);
        }

        public DataTable PurchaseDetails_GetByPurchaseId(Int64 purchaseId)
        {
            return DataAccess.Purchase.Purchase.PurchaseDetails_GetByPurchaseId(purchaseId);
        }

        public int Purchase_Delete(int purchaseId)
        {
            return DataAccess.Purchase.Purchase.Purchase_Delete(purchaseId);
        }
    }
}
