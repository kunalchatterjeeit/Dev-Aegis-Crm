using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Sales
{
    public class CustomerType
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
   
    public class GetAccountsParam
    {
        public string Name { get; set; }
        public string OfficePhone { get; set; }
    }
    public class GetAccounts
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OfficePhone { get; set; }
        public int EmployeeStrength { get; set; }
        public string Industry { get; set; }
        public string LeadSourceName { get; set; }
        public string CustomerTypeName { get; set; }
    }
    public class Accounts
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string Industry { get; set; }
        public int? CustomerTypeId { get; set; }
        public string OfficePhone { get; set; }
        public int EmployeeStrength { get; set; }
        public decimal? AnualRevenue { get; set; }
        public decimal? AccountScore { get; set; }
        public int? LeadSourceId { get; set; }
        public string SourceName { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
