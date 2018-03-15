using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Business.Purchase
{
    public class Vendor
    {
        public Vendor()
        { }

        public int Save(Entity.Purchase.Vendor vendorMaster)
        {
            return DataAccess.Purchase.Vendor.Save(vendorMaster);
        }
        public DataTable GetAll(Entity.Purchase.Vendor vendorMaster)
        {
            return DataAccess.Purchase.Vendor.GetAll(vendorMaster);
        }
        public Entity.Purchase.Vendor GetById(int vendorId)
        {
            return DataAccess.Purchase.Vendor.GetById(vendorId);
        }
        public int Delete(int vendorId)
        {
            return DataAccess.Purchase.Vendor.Delete(vendorId);
        }
    }
}
