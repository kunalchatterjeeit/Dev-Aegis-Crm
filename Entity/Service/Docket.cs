using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Service
{
    public class Docket
    {
        public Docket()
        { }

        public Int64 DocketId { get; set; }
        public string DocketNo { get; set; }
        public string DocketType { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public DateTime DocketDateTime { get; set; }
        public DateTime DocketFromDateTime { get; set; }
        public DateTime DocketToDateTime { get; set; }
        public string Problem { get; set; }
        public bool isCustomerEntry { get; set; }
        public int CallStatusId { get; set; }
        public int CreatedBy { get; set; }
        public Int64 CustomerPurchaseId { get; set; }
        public int AssignEngineer { get; set; }
    }
}
