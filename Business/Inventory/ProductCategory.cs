using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Business.Inventory
{
    public class ProductCategory
    {
        public ProductCategory()
        { }

        public int Save(Entity.Inventory.ProductCategory productCategory)
        {
            return DataAccess.Inventory.ProductCategory.Save(productCategory);
        }

        public DataTable GetAll()
        {
            return DataAccess.Inventory.ProductCategory.GetAll();
        }

        public Entity.Inventory.ProductCategory GetById(int productCategoryId)
        {
            return DataAccess.Inventory.ProductCategory.GetById(productCategoryId);
        }
    }
}
