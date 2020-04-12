using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Sales
{
   
    public class GetLeadsParam
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int? DepartmentId { get; set; }
        public int? CampaignId { get; set; }
        public int SourceActivityTypeId { get; set; }
        public int ChildActivityTypeId { get; set; }
        public int AssignEngineer { get; set; }
    }
    public class GetLeads
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string OfficePhone { get; set; }
        public decimal? LeadScore { get; set; }
        public string AccountName { get; set; }
    }
    public class Leads
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public decimal? LeadScore { get; set; }
        public string PrimaryAddress { get; set; }
        public string AlternateAddress { get; set; }
        public int? DepartmentId { get; set; }
        public string OfficePhone { get; set; }
        public string Fax { get; set; }
        public int? CampaignId { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public int ActivityLinkId { get; set; }
        public int? ChildActivityTypeId { get; set; }
        public int? SourceActivityId { get; set; }
        public int? SourceActivityTypeId { get; set; }
        public string AccountName { get; set; }
    }
}
