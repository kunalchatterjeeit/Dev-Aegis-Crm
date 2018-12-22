using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.HR
{
   public class EmployeeMaster
    {
        private int employeeMasterId = 0;
        public int EmployeeMasterId
        {
            get { return employeeMasterId; }
            set { employeeMasterId = value; }
        }

        private string roles;
        public string Roles
        {
            get { return roles; }
            set { roles = value; }
        }

        private string employeeName = string.Empty;
        public string EmployeeName
        {
            get { return employeeName; }
            set { employeeName = value; }
        }

        private string image = string.Empty;
        public string Image
        {
            get { return image; }
            set { image = value; }
        }

        private int genderId = 0;
        public int GenderId
        {
            get { return genderId; }
            set { genderId = value; }
        }

        private DateTime dOB = DateTime.MinValue;
        public DateTime DOB
        {
            get { return dOB; }
            set { dOB = value; }
        }

        private string maritalStatus = string.Empty;
        public string MaritalStatus
        {
            get { return maritalStatus; }
            set { maritalStatus = value; }
        }

        private DateTime dOM = DateTime.MinValue;
        public DateTime DOM
        {
            get { return dOM; }
            set { dOM = value; }
        }

        private int nationalityId_FK = 0;
        public int NationalityId_FK
        {
            get { return nationalityId_FK; }
            set { nationalityId_FK = value; }
        }

        private int religionId_FK = 0;
        public int ReligionId_FK
        {
            get { return religionId_FK; }
            set { religionId_FK = value; }
        }

        private string bloodGroup = string.Empty;
        public string BloodGroup
        {
            get { return bloodGroup; }
            set { bloodGroup = value; }
        }

        private string personalMobileNo = string.Empty;
        public string PersonalMobileNo
        {
            get { return personalMobileNo; }
            set { personalMobileNo = value; }
        }

        private string officeMobileNo = string.Empty;
        public string OfficeMobileNo
        {
            get { return officeMobileNo; }
            set { officeMobileNo = value; }
        }

        private string phoneNo = string.Empty;
        public string PhoneNo
        {
            get { return phoneNo; }
            set { phoneNo = value; }
        }

        private string personalEmailId = string.Empty;
        public string PersonalEmailId
        {
            get { return personalEmailId; }
            set { personalEmailId = value; }
        }

        private string officeEmailId = string.Empty;
        public string OfficeEmailId
        {
            get { return officeEmailId; }
            set { officeEmailId = value; }
        }

        private int referenceEmployeeId = 0;
        public int ReferenceEmployeeId
        {
            get { return referenceEmployeeId; }
            set { referenceEmployeeId = value; }
        }

        private string pAddress = string.Empty;
        public string PAddress
        {
            get { return pAddress; }
            set { pAddress = value; }
        }

        private int pCityId_FK = 0;
        public int PCityId_FK
        {
            get { return pCityId_FK; }
            set { pCityId_FK = value; }
        }

        private string pPIN = string.Empty;
        public string PPIN
        {
            get { return pPIN; }
            set { pPIN = value; }
        }

        private int userId = 0;
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        private int companyId_FK = 0;
        public int CompanyId_FK
        {
            get { return companyId_FK; }
            set { companyId_FK = value; }
        }

        private int employeeJobId = 0;
        public int EmployeeJobId
        {
            get { return employeeJobId; }
            set { employeeJobId = value; }
        }

        private int designationMasterId_FK = 0;
        public int DesignationMasterId_FK
        {
            get { return designationMasterId_FK; }
            set { designationMasterId_FK = value; }
        }

        private DateTime dOJ = DateTime.MinValue;
        public DateTime DOJ
        {
            get { return dOJ; }
            set { dOJ = value; }
        }

        private int jobTypeId = 0;

        private string employeeCode = string.Empty;
        public string EmployeeCode
        {
            get { return employeeCode; }
            set { employeeCode = value; }
        }

        private string password = string.Empty;
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string pANNo = string.Empty;
        public string PANNo
        {
            get { return pANNo; }
            set { pANNo = value; }
        }

        private string tAddress = string.Empty;
        public string TAddress
        {
            get { return tAddress; }
            set { tAddress = value; }
        }

        private int tCityId_FK = 0;
        public int TCityId_FK
        {
            get { return tCityId_FK; }
            set { tCityId_FK = value; }
        }

        private string tPIN;
        public string TPIN
        {
            get { return tPIN; }
            set { tPIN = value; }
        }

        public int RoleId { get; set; }
        public int ReportingEmployeeId { get; set; }
    }
}
