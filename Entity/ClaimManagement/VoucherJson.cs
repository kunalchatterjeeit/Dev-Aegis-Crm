using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.ClaimManagement
{
    public class VoucherJson
    {
        public VoucherJson()
        {
            VoucherDescriptionList = new List<VoucherDescription>();
        }
        public string VoucherNo { get; set; }
        public DateTime CreateDate { get; set; }
        public string EmployeeName { get; set; }
        public string PayMethod { get; set; }
        public string ChequeNo { get; set; }
        public List<VoucherDescription> VoucherDescriptionList { get; set; }
        public decimal TotalAmount { get; set; }
        public string AmountInWords { get; set; }
    }

    public class VoucherDescription
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
    }
}
