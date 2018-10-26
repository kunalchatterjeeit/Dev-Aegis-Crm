using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Customer
{
   public class Customer
    {
        private Int64 customerMasterId = 0;
        public Int64 CustomerMasterId
        {
            get { return customerMasterId; }
            set { customerMasterId = value; }
        }

        private string customerName = string.Empty;
        public string CustomerName
        {
            get { return customerName; }
            set { customerName = value; }
        }

        private int customerType = 0;
        public int CustomerType
        {
            get { return customerType; }
            set { customerType = value; }
        }

        private string referenceName = string.Empty;
        public string ReferenceName
        {
            get { return referenceName; }
            set { referenceName = value; }
        }

        private int genderId = 0;
        public int GenderId
        {
            get { return genderId; }
            set { genderId = value; }
        }

        private string dOB = string.Empty;
        public string DOB
        {
            get { return dOB; }
            set { dOB = value; }
        }

        private int maritalStatusId = 0;

        public int MaritalStatusId
        {
            get { return maritalStatusId; }
            set { maritalStatusId = value; }
        }
        private string nationality = string.Empty;

        public string Nationality
        {
            get { return nationality; }
            set { nationality = value; }
        }
        private string religion = string.Empty;

        public string Religion
        {
            get { return religion; }
            set { religion = value; }
        }

        private string customerCode = string.Empty;

        public string CustomerCode
        {
            get { return customerCode; }
            set { customerCode = value; }
        }
        private string mobileNo = string.Empty;

        public string MobileNo
        {
            get { return mobileNo; }
            set { mobileNo = value; }
        }
        private string phoneNo = string.Empty;

        public string PhoneNo
        {
            get { return phoneNo; }
            set { phoneNo = value; }
        }
        private string emailId = string.Empty;

        public string EmailId
        {
            get { return emailId; }
            set { emailId = value; }
        }
        private string pAddress = string.Empty;

        public string PAddress
        {
            get { return pAddress; }
            set { pAddress = value; }
        }
        private string pStreet = string.Empty;

        public string PStreet
        {
            get { return pStreet; }
            set { pStreet = value; }
        }
        private string pin = string.Empty;

        public string Pin
        {
            get { return pin; }
            set { pin = value; }
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

        public string Stamp { get; set; }

        private int productid = 0;
        private string serialNo = string.Empty;
        private int purchaseBranchid = 0;
        private DateTime purchaseDate = DateTime.MinValue;
        private string machineId = string.Empty;
        private decimal purchaseAmount = 0;
        private int installedBy = 0;
        private DateTime installationDate = DateTime.MinValue;
        private int salesExecutiveid = 0;
        private string salesRemarks = string.Empty;
        private int technician = 0;
        private string customerRemarks = string.Empty;
        private Int64 customerPurchaseId = 0;
        private string challanNo = string.Empty;
        private string orderNo = string.Empty;
        private int serviceType = 0;
        private int purchaseFrom = 0;
        

        public int PurchaseFrom
        {
            get { return purchaseFrom; }
            set { purchaseFrom = value; }
        }

        public int ServiceType
        {
            get { return serviceType; }
            set { serviceType = value; }
        }

        public string OrderNo
        {
            get { return orderNo; }
            set { orderNo = value; }
        }

        public string ChallanNo
        {
            get { return challanNo; }
            set { challanNo = value; }
        }

        public Int64 CustomerPurchaseId
        {
            get { return customerPurchaseId; }
            set { customerPurchaseId = value; }
        }

        public string CustomerRemarks
        {
            get { return customerRemarks; }
            set { customerRemarks = value; }
        }

        public int Technician
        {
            get { return technician; }
            set { technician = value; }
        }

        public string SalesRemarks
        {
            get { return salesRemarks; }
            set { salesRemarks = value; }
        }

        public int SalesExecutiveid
        {
            get { return salesExecutiveid; }
            set { salesExecutiveid = value; }
        }

        public DateTime InstallationDate
        {
            get { return installationDate; }
            set { installationDate = value; }
        }

        public int InstalledBy
        {
            get { return installedBy; }
            set { installedBy = value; }
        }

        public decimal PurchaseAmount
        {
            get { return purchaseAmount; }
            set { purchaseAmount = value; }
        }

        public string MachineId
        {
            get { return machineId; }
            set { machineId = value; }
        }

        public DateTime PurchaseDate
        {
            get { return purchaseDate; }
            set { purchaseDate = value; }
        }

        public int PurchaseBranchid
        {
            get { return purchaseBranchid; }
            set { purchaseBranchid = value; }
        }

        public string SerialNo
        {
            get { return serialNo; }
            set { serialNo = value; }
        }

        public int Productid
        {
            get { return productid; }
            set { productid = value; }
        }

        public string Description { get; set; }
        public string ContactPerson { get; set; }
        public string Address { get; set; }
        public int BrandId { get; set; }
        public int ContractTypeId { get; set; }
        public DateTime ContractStartDate { get; set; }
        public DateTime ContractEndDate { get; set; }
        public int AssignEngineer { get; set; }
        public Int64 A3BWMeterReading { get; set; }
        public Int64 A3CLMeterReading { get; set; }
        public Int64 A4BWMeterReading { get; set; }
        public Int64 A4CLMeterReading { get; set; }
    }
}
