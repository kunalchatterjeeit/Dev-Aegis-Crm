using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Inventory
{
    public class Stock
    {
        public Stock() { }

        public DataTable GetStockSnap(string name)
        {
            return DataAccess.Inventory.Stock.GetStockSnap(name);
        }
    }
}
