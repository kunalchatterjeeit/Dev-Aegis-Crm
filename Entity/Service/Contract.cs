using Entity.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Entity.Service
{
    public class Contract: BaseEntity
    {
        public int ContractId { get; set; }
        public int CustomerId { get; set; }
        public int ContractTypeId { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public int CustomerPurchaseId { get; set; }
        public Int64 A3BWStartMeter { get; set; }
        public Int64 A4BWStartMeter { get; set; }
        public Int64 A3CLStartMeter { get; set; }
        public Int64 A4CLStartMeter { get; set; }
        public Int64 A3BWPages { get; set; }
        public Int64 A4BWPages { get; set; }
        public Int64 A3CLPages { get; set; }
        public Int64 A4CLPages { get; set; }
        public Int64 A3BWRate { get; set; }
        public Int64 A4BWRate { get; set; }
        public Int64 A3CLRate { get; set; }
        public Int64 A4CLRate { get; set; }
        public DataTable ContractDetails { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string MachineId { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public int AssignEngineer { get; set; }
        public string ProductSerialNo { get; set; }
    }
}
