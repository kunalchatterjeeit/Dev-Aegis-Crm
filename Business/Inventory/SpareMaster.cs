using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Business.Inventory
{
    public class SpareMaster
    {
        public SpareMaster()
        { }

        public int Save(Entity.Inventory.SpareMaster spareMaster)
        {
            return DataAccess.Inventory.SpareMaster.Save(spareMaster);
        }

        public DataTable GetAll(Entity.Inventory.SpareMaster spareMaster)
        {
            return DataAccess.Inventory.SpareMaster.GetAll(spareMaster);
        }

        public DataTable Inventory_SpareGetByStoreId(int storeId)
        {
            return DataAccess.Inventory.SpareMaster.Inventory_SpareGetByStoreId(storeId);
        }

        public Entity.Inventory.SpareMaster GetById(int SpareMasterId)
        {
            return DataAccess.Inventory.SpareMaster.GetById(SpareMasterId);
        }

        public int Delete(int SpareMasterId)
        {
            return DataAccess.Inventory.SpareMaster.Delete(SpareMasterId);
        }
    }
}
