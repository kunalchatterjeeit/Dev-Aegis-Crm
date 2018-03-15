using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Business.Inventory
{
    public class BrandMaster
    {
        public BrandMaster()
        { }

        public int Save(Entity.Inventory.BrandMaster brandMaster)
        {
            return DataAccess.Inventory.BrandMaster.Save(brandMaster);
        }

        public DataTable GetAll()
        {
            return DataAccess.Inventory.BrandMaster.GetAll();
        }

        public Entity.Inventory.BrandMaster GetById(int brandId)
        {
            return DataAccess.Inventory.BrandMaster.GetById(brandId);
        }
    }
}
