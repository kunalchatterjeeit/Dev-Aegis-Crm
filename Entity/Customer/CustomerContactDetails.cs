using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Customer
{
   public class CustomerContactDetails
    {
        private Int64 customerContactDetailsId = 0;

        public Int64 CustomerContactDetailsId
        {
            get { return customerContactDetailsId; }
            set { customerContactDetailsId = value; }
        }
        private Int64 customerMasterId_FK = 0;

        public Int64 CustomerMasterId_FK
        {
            get { return customerMasterId_FK; }
            set { customerMasterId_FK = value; }
        }
      private string contactPerson = string.Empty;

      public string ContactPerson
      {
          get { return contactPerson; }
          set { contactPerson = value; }
      }
      private string cPDesignation = string.Empty;

      public string CPDesignation
      {
          get { return cPDesignation; }
          set { cPDesignation = value; }
      }
      private string cPPhoneNo = string.Empty;

      public string CPPhoneNo
      {
          get { return cPPhoneNo; }
          set { cPPhoneNo = value; }
      }
      private int companyMasterId_FK = 0;

      public int CompanyMasterId_FK
      {
          get { return companyMasterId_FK; }
          set { companyMasterId_FK = value; }
      }
      private int userId = 0;

      public int UserId
      {
          get { return userId; }
          set { userId = value; }
      }

    }
}
