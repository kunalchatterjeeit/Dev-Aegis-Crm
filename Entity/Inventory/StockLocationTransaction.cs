using System;

namespace Entity.Inventory
{
    public class StockLocationTransaction
    {
        public long StockLocationTransactionId { get; set; }
        public long StockLocationId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime TransactionDateTime { get; set; }
    }
}
