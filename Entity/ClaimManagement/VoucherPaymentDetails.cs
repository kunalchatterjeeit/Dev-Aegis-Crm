using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ClaimManagement
{
    public class VoucherPaymentDetails
    {
        public VoucherPaymentDetails()
        {

        }
        public int VoucherPaymentDetailsId { get; set; }
        public int VoucherPaymentId { get; set; }
        public int PaymentModeId { get; set; }
        public decimal PaymentAmount { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
    }
}
