using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Business.Inventory
{
    public class ProductMaster
    {
        public ProductMaster()
        { }

        public int Save(Entity.Inventory.ProductMaster productMaster)
        {
            return DataAccess.Inventory.ProductMaster.Save(productMaster);
        }
        public DataTable GetAll(Entity.Inventory.ProductMaster productMaster)
        {
            return DataAccess.Inventory.ProductMaster.GetAll(productMaster);
        }
        public Entity.Inventory.ProductMaster GetById(int productMasterId)
        {
            return DataAccess.Inventory.ProductMaster.GetById(productMasterId);
        }
        public int Delete(int productMasterId)
        {
            return DataAccess.Inventory.ProductMaster.Delete(productMasterId);
        }
        
        //PRODUCT SPARE MAPPING
        public int ProductSpareMapping_Save(Entity.Inventory.ProductMaster productMaster)
        {
            return DataAccess.Inventory.ProductMaster.ProductSpareMapping_Save(productMaster);
        }
        public DataTable ProductSpareMapping_GetById(int productid)
        {
            return DataAccess.Inventory.ProductMaster.ProductSpareMapping_GetById(productid);
        }
        public int ProductSpareMapping_Delete(int productsparemappingid)
        {
            return DataAccess.Inventory.ProductMaster.ProductSpareMapping_Delete(productsparemappingid);
        }
        
        //PRODUCT CONSUMABLE MAPPING
        public int ProductConnsumableMapping_Save(Entity.Inventory.ProductMaster productMaster)
        {
            return DataAccess.Inventory.ProductMaster.ProductConsumableMapping_Save(productMaster);
        }
        public DataTable ProductConnsumableMapping_GetById(int productid)
        {
            return DataAccess.Inventory.ProductMaster.ProductConsumableMapping_GetById(productid);
        }
        public int ProductConsumableMapping_Delete(int productconsumablemappingid)
        {
            return DataAccess.Inventory.ProductMaster.ProductConsumableMapping_Delete(productconsumablemappingid);
        }
    }
}
