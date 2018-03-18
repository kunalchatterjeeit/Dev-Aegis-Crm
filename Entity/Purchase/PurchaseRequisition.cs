using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Purchase
{
    public class PurchaseRequisition
    {
        public PurchaseRequisition() {
            PurchaseRequisitionDetailsCollection = new Collection<PurchaseRequisitionDetails>();
        }

        public Int64 PurchaseRequisitionId { get; set; }
        public string PurchaseRequisitionNo { get; set; }
        public DateTime RequisitionDate { get; set; }
        public string PurchaseDepartment { get; set; }
        public int VendorId { get; set; }
        public DateTime WhenNeeded { get; set; }
        public string PurposeOfRequisition { get; set; }
        public int CreatedBy { get; set; }

        public Collection<PurchaseRequisitionDetails> PurchaseRequisitionDetailsCollection { get; set; }
    }
}
