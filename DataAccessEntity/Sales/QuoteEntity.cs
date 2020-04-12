using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessEntity.Sales
{
    public class QuoteDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string QuoteNumber { get; set; }
        public int? OpportunityId { get; set; }
        public string PurchaseOrderNo { get; set; }
        public int? PaymentTermId { get; set; }
        public DateTime? OriginalPODate { get; set; }
        public DateTime? ActualCloseDate { get; set; }
        public DateTime? DateShipped { get; set; }
        public DateTime? ValidTillDate { get; set; }
        public string ShippingProvider { get; set; }
        public int CreatedBy { get; set; }
        public bool IsActiveQuote { get; set; }
        public string QuoteLineItem { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public decimal? TaxRate { get; set; }
        public bool IsActiveQuoteSettings { get; set; }
        public int? QuoteStageId { get; set; }
        public bool IsActiveQuoteHistory { get; set; }
        public string QuoteJSON { get; set; }
    }
    public class PaymentTermDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }
    }
    public class QuoteStageDbModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class GetQuoteDbModel
    {
        public int Id { get; set; }
        public string QuoteNumber { get; set; }
        public string PurchaseOrderNo { get; set; }
        public DateTime? OriginalPODate { get; set; }
        public DateTime? ActualCloseDate { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
    }
    public class GetQuoteById_PrintDbModel
    {
        public int Id { get; set; }
        public int QuoteId { get; set; }
        public string StageName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedBy { get; set; }
    }
    public class GetQuoteParamDbModel
    {
        public string QuoteNumber { get; set; }
        public int AssignEngineer { get; set; }
    }
}
