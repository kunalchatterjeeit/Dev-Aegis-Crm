using System;

namespace Entity.Inventory
{
    public class StockLocation
    {
        public long StockLocationId { get; set; }
        public long InventoryId { get; set; }
        public int StoreId { get; set; }
        public bool InStore { get; set; }
        public DateTime LastUpdateDate { get; set; }
    }
}
