using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessEntity.Sales
{
    public class DepartmentDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Name { get; set; }       
    }
    public class GetLeadsParamDbModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int? DepartmentId { get; set; }
        public int? CampaignId { get; set; }
    }
    public class GetLeadsDbModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string OfficePhone { get; set; }
        public decimal? LeadScore { get; set; }
    }
    public class LeadsDbModel
    {
        [Key]
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
    }
}
