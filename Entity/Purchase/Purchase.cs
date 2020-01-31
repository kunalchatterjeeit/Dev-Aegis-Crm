using Entity.Inventory;
using System;
using System.Collections.ObjectModel;

namespace Entity.Purchase
{
    public class Purchase
    {
        public Purchase()
        {
            PurchaseDetailsCollection = new Collection<PurchaseDetails>();
        }

        public int PurchaseId { get; set; }
        public string PurchaseOrderNo { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int VendorId { get; set; }
        public string InvoiceNo { get; set; }
        public DateTime InvoiceDate { get; set; }
        public decimal BillAmount { get; set; }
        public decimal PaymentAmount { get; set; }
        public int CreatedBy { get; set; }
        public int StoreId { get; set; }
        public DateTime InvoiceFromDate { get; set; }
        public DateTime InvoiceToDate { get; set; }
        public DateTime PurchaseFromDate { get; set; }
        public DateTime PurchaseToDate { get; set; }
        public int ItemId { get; set; }
        public ItemType itemType { get; set; }

        public Collection<PurchaseDetails> PurchaseDetailsCollection { get; set; }
    }
}
