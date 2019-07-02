using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Sales
{
    public class CommitStage
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
    public class GetOpportunity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? BestPrice { get; set; }
        public string CommitStage { get; set; }
        public DateTime? ExpectedCloseDate { get; set; }
        public string LeadName { get; set; }
    }
    public class GetOpportunityParam
    {
        public string Name { get; set; }
        public int? CommitStageId { get; set; }
        public decimal? BestPrice { get; set; }
        public int SourceActivityTypeId { get; set; }
        public int ChildActivityTypeId { get; set; }
    }
    public class Opportunity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? ExpectedCloseDate { get; set; }
        public decimal? LikelyPrice { get; set; }
        public decimal? BestPrice { get; set; }
        public decimal? WorstPrice { get; set; }
        public int? CommitStageId { get; set; }
        public int? LeadSource { get; set; }
        public string SourceName { get; set; }
        public int? CampaignId { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public int ActivityLinkId { get; set; }
        public int? ChildActivityTypeId { get; set; }
        public int? SourceActivityId { get; set; }
        public int? SourceActivityTypeId { get; set; }
        public string LeadName { get; set; }
    }
}
