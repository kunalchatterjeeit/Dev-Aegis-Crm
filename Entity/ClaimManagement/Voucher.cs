using System;

namespace Entity.ClaimManagement
{
    public class Voucher
    {
        public Voucher()
        {

        }
        public int VoucherId { get; set; }
        public string VoucherNo { get; set; }
        public string VoucherJson { get; set; }
        public int CreatedBy { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public VoucherPayment voucherPayment { get; set; }
    }
}
