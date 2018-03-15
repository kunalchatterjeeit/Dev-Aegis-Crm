using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace Business.Customer
{
   public class CustomerAddress
   {
       DataAccess.Customer.CustomerAddress ObjBelCustomerAddress = new DataAccess.Customer.CustomerAddress();
       public int SaveCustomerAddress(Entity.Customer.CustomerAddress ObjElCustomerAddress)
        {
            return ObjBelCustomerAddress.SaveCustomerAddress(ObjElCustomerAddress);
        }

       public System.Data.DataTable GetAllAddress(Entity.Customer.CustomerAddress ObjElCustomerAddress)
       {
           return ObjBelCustomerAddress.GetAllAddress(ObjElCustomerAddress);
       }

       public DataTable FetchCustomerAddressDetailsById(Entity.Customer.CustomerAddress ObjElCustomerAddress)
       {
           return ObjBelCustomerAddress.FetchCustomerAddressDetailsById(ObjElCustomerAddress);
       }

       public int DeleteCustomerAddressById(Entity.Customer.CustomerAddress ObjElCustomerAddress)
       {
           return ObjBelCustomerAddress.DeleteCustomerAddressById(ObjElCustomerAddress);
       }
   }
}
