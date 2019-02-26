using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Sale
{
    public class SaleChallanDetails
    {
        public SaleChallanDetails() { }

        public long SaleChallanDetailsId { get; set; }
        public int SaleChallanId { get; set; }
        public int ItemId { get; set; }
        public int ItemType { get; set; }
        public decimal ItemQty { get; set; }
        public decimal ItemRate { get; set; }
        public decimal GST { get; set; }
        public string HsnCode { get; set; }
        public int Uom { get; set; }
    }
}
