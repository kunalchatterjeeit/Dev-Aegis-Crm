using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Purchase
{
    public class PurchaseDetails
    {
        public PurchaseDetails()
        { }

        public Int64 PurchaseDetailsId { get; set; }
        public int PurchaseId { get; set; }
        public int ItemId { get; set; }
        public int ItemType { get; set; }
        public decimal ItemQty { get; set; }
        public decimal ItemRate { get; set; }
        public decimal GST { get; set; }
        public decimal Discount { get; set; }
        public string HsnCode { get; set; }
        public int Uom { get; set; }
    }
}
