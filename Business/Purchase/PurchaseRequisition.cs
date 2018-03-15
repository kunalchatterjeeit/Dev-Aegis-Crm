using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Purchase
{
    public class PurchaseRequisition
    {
        public PurchaseRequisition()
        { }

        public Int64 PurchaseRequisition_Save(Entity.Purchase.PurchaseRequisition purchase)
        {
            return DataAccess.Purchase.PurchaseRequisition.PurchaseRequisition_Save(purchase);
        }

        public int Purchase_RequisitionDetails_Save(Entity.Purchase.PurchaseRequisition purchase)
        {
            return DataAccess.Purchase.PurchaseRequisition.Purchase_RequisitionDetails_Save(purchase);
        }
    }
}
