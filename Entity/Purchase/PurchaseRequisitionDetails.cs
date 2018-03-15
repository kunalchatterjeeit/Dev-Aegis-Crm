using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Purchase
{
    public class PurchaseRequisitionDetails
    {
        public PurchaseRequisitionDetails()
        { }

        public Int64 PurchaseRequisitionDetailsId { get; set; }
        public Int64 PurchaseRequisitionId { get; set; }
        public int ItemId { get; set; }
        public int ItemType { get; set; }
        public decimal Quantity { get; set; }
        public int UOM { get; set; }
        public string Description { get; set; }
        public int ApprovalStatus { get; set; }
    }
}
