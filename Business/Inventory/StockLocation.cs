using System.Data;

namespace Business.Inventory
{
    public class StockLocation
    {
        public StockLocation()
        { }

        public int Save(Entity.Inventory.StockLocation stockLocation)
        {
            return DataAccess.Inventory.StockLocation.Save(stockLocation);
        }

        public DataTable GetAll()
        {
            return DataAccess.Inventory.StockLocation.GetAll();
        }

        public int Delete(long stockLocationId)
        {
            return DataAccess.Inventory.StockLocation.Delete(stockLocationId);
        }
    }
}
