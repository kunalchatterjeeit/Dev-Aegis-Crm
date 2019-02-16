using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Entity.Service
{
    public class ServiceBook
    {
        public ServiceBook()
        { }
        
        public long ServiceBookId { get; set; }
        public long ServiceBookDetailsId { get; set; }
        public long CallId { get; set; }
        public int CallType { get; set; }
        public int CallStatusId { get; set; }
        public long TonnerRequestId { get; set; }
        public int EmployeeId_FK { get; set; }
        public string Remarks { get; set; }
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }
        public string Diagnosis { get; set; }
        public string ActionTaken { get; set; }
        public string CustomerFeedback { get; set; }
        public int CreatedBy { get; set; }
        public DataTable ServiceBookDetails { get; set; }
        public string ProblemObserved { get; set; }
        public int CustomerId { get; set; }
        public int ModelId { get; set; }
        public string MachineId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string DocketType { get; set; }
        public int CustomerPurchaseId { get; set; }
        public int? A3BWMeterReading { get; set; }
        public int? A4BWMeterReading { get; set; }
        public int? A3CLMeterReading { get; set; }
        public int? A4CLMeterReading { get; set; }
        public string Signature { get; set; }
        public DataTable AssociatedEngineers { get; set; }
        public DataTable ApprovalItems { get; set; }
        public string RequestNo { get; set; }
        public DateTime RequestFromDate { get; set; }
        public DateTime RequestToDate { get; set; }
        public int ApprovalStatus { get; set; }
        public int CallTransferId { get; set; }
        public DataTable SpareRequisition { get; set; }
        public int ItemId { get; set; }
    }

    public class MachineResponseTime
    {
        public string MachineId { get; set; }
        public decimal Avg_Response_Time { get; set; }
    }
}
