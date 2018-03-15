using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Customer
{
   public class CustomerAddress
    {


        private Int64 customerAddressId = 0;

        public Int64 CustomerAddressId
        {
            get { return customerAddressId; }
            set { customerAddressId = value; }
        }
        private string tAddress = string.Empty;

        public string TAddress
        {
            get { return tAddress; }
            set { tAddress = value; }
        }
        private string tStreet = string.Empty;

        public string TStreet
        {
            get { return tStreet; }
            set { tStreet = value; }
        }


        private Int64 customerMasterId_FK = 0;

        public Int64 CustomerMasterId_FK
        {
            get { return customerMasterId_FK; }
            set { customerMasterId_FK = value; }
        }

        private int userId = 0;

        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        private int companyMasterId_FK = 0;

        public int CompanyMasterId_FK
        {
            get { return companyMasterId_FK; }
            set { companyMasterId_FK = value; }
        }
       
    }
}
