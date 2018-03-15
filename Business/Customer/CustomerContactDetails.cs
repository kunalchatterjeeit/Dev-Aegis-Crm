using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
namespace Business.Customer
{
   public class CustomerContactDetails
    {
       DataAccess.Customer.CustomerContactDetails ObjDelelCustomerContactDetails = new DataAccess.Customer.CustomerContactDetails();
        public int SaveContactDeatails(Entity.Customer.CustomerContactDetails ObjElCustomerContactDetails)
        {
            return ObjDelelCustomerContactDetails.SaveContactDeatails(ObjElCustomerContactDetails);
        }

        public System.Data.DataTable GetAllACustomerContactDetails(Entity.Customer.CustomerContactDetails ObjElCustomerContactDetails)
        {
           return  ObjDelelCustomerContactDetails.GetAllACustomerContactDetails(ObjElCustomerContactDetails);
         
        }

        public DataTable FetchCustomerContactDetailsById(Entity.Customer.CustomerContactDetails ObjElCustomerContactDetails)
        {
            return ObjDelelCustomerContactDetails.FetchCustomerContactDetailsById(ObjElCustomerContactDetails);
        }

        public int DeleteCustomerContactDetailsById(Entity.Customer.CustomerContactDetails ObjElCustomerContactDetails)
        {
            return ObjDelelCustomerContactDetails.DeleteCustomerContactDetailsById(ObjElCustomerContactDetails);
        }
    }
}
