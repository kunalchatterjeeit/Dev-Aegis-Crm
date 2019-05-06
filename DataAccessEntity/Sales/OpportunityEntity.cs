using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessEntity.Sales
{
    public class CommitStageDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }
    }
    public class GetOpportunityDbModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? BestPrice { get; set; }
        public string CommitStage { get; set; }
        public DateTime? ExpectedCloseDate { get; set; }
    }
    public class GetOpportunityParamDbModel
    {
        public string Name { get; set; }
        public int? CommitStageId { get; set; }
        public decimal? BestPrice { get; set; }
    }
    public class OpportunityDbModel
    {
        [Key]
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
    }
}
