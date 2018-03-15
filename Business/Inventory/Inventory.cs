using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Business.Inventory
{
    public class Inventory
    {
        public Inventory()
        { }

        public int Inventory_Save(Entity.Inventory.Inventory inventory)
        {
            return DataAccess.Inventory.Inventory.Inventory_Save(inventory);
        }

        public static DataTable Inventory_GetAll(DateTime fromDate, DateTime toDate)
        {
            return DataAccess.Inventory.Inventory.Inventory_GetAll(fromDate, toDate);
        }

        public static DataTable Inventory_Transaction_GetByInventoryId(Int64 inventoryId)
        {
            return DataAccess.Inventory.Inventory.Inventory_Transaction_GetByInventoryId(inventoryId);
        }
    }
}
