using System.Collections.Generic;

namespace Entity.Service
{
    public class CsrJson
    {
        public CsrJson() { }
        public string ProductName { get; set; }
        public string MachineId { get; set; }
        public string DocketNo { get; set; }
        public string CustomerCode { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public string ProductSerialNo { get; set; }
        public string DocketDate { get; set; }
        public string DocketTime { get; set; }
        public string DocketType { get; set; }
        public string Problem { get; set; }
        public string Diagnosis { get; set; }
        public string ActionTaken { get; set; }
        public string Signature { get; set; }
        public string Stamp { get; set; }
        public string A3BWMeterReading { get; set; }
        public string A4BWMeterReading { get; set; }
        public string A3CLMeterReading { get; set; }
        public string A4CLMeterReading { get; set; }
        public List<CallAttendance> callAttendances { get; set; }
        public List<SpareUsed> sparesUsed { get; set; }
        public bool IsInContract { get; set; }
    }

    public class CallAttendance {
        public CallAttendance() { }
        public string InDate { get; set; }
        public string InTime { get; set; }
        public string OutDate { get; set; }
        public string OutTime { get; set; }
        public string ProblemStatus { get; set; }
        public string EmployeeName { get; set; }
    }

    public class SpareUsed {
        public SpareUsed() { }
        public string SpareName { get; set; }
    }
}
