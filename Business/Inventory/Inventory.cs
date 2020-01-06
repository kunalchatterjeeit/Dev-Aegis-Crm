using Entity.Inventory;
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

        public DataTable Inventory_GetApprovedInventorySpareByServiceBookId(long serviceBookId, AssetLocation assetLocation, ItemType itemType, int storeId)
        {
            return DataAccess.Inventory.Inventory.Inventory_GetApprovedInventorySpareByServiceBookId(serviceBookId, assetLocation, itemType, storeId);
        }

        public DataTable Inventory_GetInventoryItem(AssetLocation assetLocation, ItemType itemType, string itemName, int storeId)
        {
            return DataAccess.Inventory.Inventory.Inventory_GetInventoryItem(assetLocation, itemType, itemName, storeId);
        }

        public int Inventory_Movement(Entity.Inventory.Inventory inventory)
        {
            return DataAccess.Inventory.Inventory.Inventory_Movement(inventory);
        }

        public DataTable Inventory_StockLocationWiseQuantity(int itemId, ItemType itemType)
        {
            return DataAccess.Inventory.Inventory.Inventory_StockLocationWiseQuantity(itemId, itemType);
        }

        public DataTable Inventory_SaleFocWiseQuantity(int itemId, ItemType itemType, AssetLocation assetLocation)
        {
            return DataAccess.Inventory.Inventory.Inventory_SaleFocWiseQuantity(itemId, itemType, assetLocation);
        }
    }
}
