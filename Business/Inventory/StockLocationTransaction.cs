using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Inventory
{
    public class StockLocationTransaction
    {
        public StockLocationTransaction()
        { }

        public int Save(Entity.Inventory.StockLocationTransaction StockLocationTransaction)
        {
            return DataAccess.Inventory.StockLocationTransaction.Save(StockLocationTransaction);
        }

        public DataTable GetAll()
        {
            return DataAccess.Inventory.StockLocationTransaction.GetAll();
        }

        public int Delete(int storeId)
        {
            return DataAccess.Inventory.StockLocationTransaction.Delete(storeId);
        }
    }
}
