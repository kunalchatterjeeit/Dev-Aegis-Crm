using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ClaimManagement
{
    public class VoucherPayment
    {
        public VoucherPayment()
        {

        }
        public int VoucherPaymentId { get; set; }
        public int VoucherId { get; set; }
        public decimal TotalAmount { get; set; }
        public int CreatedBy { get; set; }
        public string VoucherNo { get; set; }
    }
}
