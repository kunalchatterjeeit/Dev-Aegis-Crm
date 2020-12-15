using Entity.Common;
using System;
using System.Collections.Generic;

namespace Entity.ClaimManagement
{
    public class Voucher : BaseEntity
    {
        public Voucher()
        {
            VoucherPayment = new VoucherPayment();
        }
        public int VoucherId { get; set; }
        public string VoucherNo { get; set; }
        public string VoucherJson { get; set; }
        public int CreatedBy { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public VoucherPayment VoucherPayment { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string EmployeeName { get; set; }

    }
}
