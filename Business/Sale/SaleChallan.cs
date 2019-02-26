using System;
using System.Data;
using Business.Purchase;

namespace Business.Sale
{
    public class SaleChallan
    {
        public SaleChallan() { }

        public int SaleChallan_Save(Entity.Sale.SaleChallan saleChallan)
        {
            return DataAccess.Sale.SaleChallan.SaleChallan_Save(saleChallan);
        }
        public int Sale_ChallanDetails_Save(Entity.Sale.SaleChallan saleChallan)
        {
            return DataAccess.Sale.SaleChallan.Sale_ChallanDetails_Save(saleChallan);
        }
        public DataTable Sale_Challan_GetAll(Entity.Sale.SaleChallan saleChallan)
        {
            return DataAccess.Sale.SaleChallan.Sale_Challan_GetAll(saleChallan);
        }
        public DataTable Sale_Challan_GetById(int saleChallanid)
        {
            return DataAccess.Sale.SaleChallan.Sale_Challan_GetById(saleChallanid);
        }

        public static implicit operator SaleChallan(Purchase.Purchase v)
        {
            throw new NotImplementedException();
        }
    }
}
