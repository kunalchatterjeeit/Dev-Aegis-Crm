using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Purchase
{
    public class Vendor
    {
        public Vendor()
        { }

        private int vendorId = 0;
        private string vendorName = string.Empty;
        private string address = string.Empty;
        private int countryId = 0;
        private int stateId = 0;
        private int districtId = 0;
        private int cityId = 0;
        private int pinId = 0;
        private string tan = string.Empty;
        private string stateCode = string.Empty;
        private string pan = string.Empty;
        private string cST = string.Empty;
        private string concernedPerson = string.Empty;
        private string bankName = string.Empty;
        private string bankBranch = string.Empty;
        private string aCNo = string.Empty;
        private string iFSC = string.Empty;
        private DateTime activeDate = DateTime.MinValue;
        private bool status = true;
        private string gSTNo = string.Empty;

        public string GSTNo
        {
            get { return gSTNo; }
            set { gSTNo = value; }
        }

        public bool Status
        {
            get { return status; }
            set { status = value; }
        }

        public DateTime ActiveDate
        {
            get { return activeDate; }
            set { activeDate = value; }
        }

        public string IFSC
        {
            get { return iFSC; }
            set { iFSC = value; }
        }

        public string ACNo
        {
            get { return aCNo; }
            set { aCNo = value; }
        }

        public string BankName
        {
            get { return bankName; }
            set { bankName = value; }
        }


        public string BankBranch
        {
            get { return bankBranch; }
            set { bankBranch = value; }
        }

        public string ConcernedPerson
        {
            get { return concernedPerson; }
            set { concernedPerson = value; }
        }
        private string mobileNo = string.Empty;
        private string fax = string.Empty;
        private string phoneNo = string.Empty;
        private string mailId = string.Empty;
        private int userId = 0;
        private int companyId = 0;

        public int VendorId
        {
            get { return vendorId; }
            set { vendorId = value; }
        }
        public string VendorName
        {
            get { return vendorName; }
            set { vendorName = value; }
        }
        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public int CountryId
        {
            get { return countryId; }
            set { countryId = value; }
        }
        public int StateId
        {
            get { return stateId; }
            set { stateId = value; }
        }
        public int DistrictId
        {
            get { return districtId; }
            set { districtId = value; }
        }
        public int CityId
        {
            get { return cityId; }
            set { cityId = value; }
        }
        public int PinId
        {
            get { return pinId; }
            set { pinId = value; }
        }
        public string Tan
        {
            get { return tan; }
            set { tan = value; }
        }
        public string StateCode
        {
            get { return stateCode; }
            set { stateCode = value; }
        }
        public string Pan
        {
            get { return pan; }
            set { pan = value; }
        }
        public string CST
        {
            get { return cST; }
            set { cST = value; }
        }
        public string MobileNo
        {
            get { return mobileNo; }
            set { mobileNo = value; }
        }
        public string Fax
        {
            get { return fax; }
            set { fax = value; }
        }
        public string PhoneNo
        {
            get { return phoneNo; }
            set { phoneNo = value; }
        }
        public string MailId
        {
            get { return mailId; }
            set { mailId = value; }
        }
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }
        public int CompanyId
        {
            get { return companyId; }
            set { companyId = value; }
        }
    }
}
