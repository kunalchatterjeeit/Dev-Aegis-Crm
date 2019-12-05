using System.Data;

namespace Business.Inventory
{
    public class StoreMaster
    {
        public StoreMaster()
        { }

        public int Save(Entity.Inventory.StoreMaster storeMaster)
        {
            return DataAccess.Inventory.StoreMaster.Save(storeMaster);
        }

        public DataTable GetAll()
        {
            return DataAccess.Inventory.StoreMaster.GetAll();
        }

        public Entity.Inventory.StoreMaster GetById(int storeId)
        {
            return DataAccess.Inventory.StoreMaster.GetById(storeId);
        }

        public int Delete(int storeId)
        {
            return DataAccess.Inventory.StoreMaster.Delete(storeId);
        }
    }
}
