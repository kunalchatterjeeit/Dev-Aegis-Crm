using Entity.Common;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Entity.Service
{
    public class TonerRequest : BaseEntity
    {
        public TonerRequest()
        {
            SpareIds = new Collection<long>();
        }

        public Int64 TonerRequestValueId { get; set; }
        public Int64 TonerRequestId { get; set; }
        public Int64 TonerId { get; set; }
        public string RequestNo { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public DateTime RequestDateTime { get; set; }
        public DateTime RequestFromDateTime { get; set; }
        public DateTime RequestToDateTime { get; set; }
        public int? A3BWMeterReading { get; set; }
        public int? A4BWMeterReading { get; set; }
        public int? A3CLMeterReading { get; set; }
        public int? A4CLMeterReading { get; set; }
        public Collection<Int64> SpareIds { get; set; }
        public string Remarks { get; set; }
        public bool isCustomerEntry { get; set; }
        public int CallStatusId { get; set; }
        public int CreatedBy { get; set; }
        public Int64 CustomerPurchaseId { get; set; }
        public string Diagnosis { get; set; }
        public string ActionTaken { get; set; }
        public bool IsApprove { get; set; }
        public int ApprovedBy { get; set; }
        public int AssignEngineer { get; set; }
    }
}
