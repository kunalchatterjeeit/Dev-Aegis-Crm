using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Sales
{
    public class Quote
    {
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
    public class PaymentTerm
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class QuoteStage
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
    public class GetQuote
    {
        public int Id { get; set; }
        public string QuoteNumber { get; set; }
        public string PurchaseOrderNo { get; set; }
        public DateTime? OriginalPODate { get; set; }
        public DateTime? ActualCloseDate { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
    }
    public class GetQuoteParam
    {
        public string QuoteNumber { get; set; }
    }
    public class QuoteLineItem
    {
        public int ItemId { get; set; }
        public string PartNumber { get; set; }
        public string ItemName { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Discount { get; set; }
        public decimal? Quantity { get; set; }
        public bool IsActive { get; set; }
    }
}
