using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessEntity.Sales
{
    public class QuoteDataAccess
    {
        public static List<QuoteStageDbModel> GetQuoteStage()
        {
            using (var Context = new CRMContext())
            {
                return Context.QuoteStages.ToList();
            }
        }
        public static List<PaymentTermDbModel> GetPeymentTerm()
        {
            using (var Context = new CRMContext())
            {
                return Context.PaymentTerm.ToList();
            }
        }
        public static List<GetQuoteDbModel> GetAllQuotes(GetQuoteParamDbModel Param)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.SqlQuery<GetQuoteDbModel>(
                                "exec dbo.[usp_Sales_Quote_GetAll] @QuoteNumber",
                                new Object[]
                                {
                                    new SqlParameter("QuoteNumber", DBNull.Value)
                                }
                             ).ToList();
            }
        }
        public static string GetQuoteByStageId(int Id)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.SqlQuery<string>(
                                "exec dbo.[usp_Sales_GetQuoteJsonByStageId] @Id",
                                new Object[]
                                {
                                    new SqlParameter("Id", Id)
                                }
                             ).FirstOrDefault();
            }
        }
        public static List<GetQuoteById_PrintDbModel> GetQuoteByIdPrint(int QuoteId)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.SqlQuery<GetQuoteById_PrintDbModel>(
                                "exec dbo.[usp_Sales_Quote_GetById_Print] @QuoteId",
                                new Object[]
                                {
                                    new SqlParameter("QuoteId", QuoteId)
                                }
                             ).ToList();
            }
        }
        public static int SaveQuotes(QuoteDbModel Model)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.ExecuteSqlCommand(
                                "exec dbo.[usp_Sales_Quote_Save] @Id,@QuoteNumber,@OpportunityId,@PurchaseOrderNo,@PaymentTermId,@OriginalPODate," +
                                "@ActualCloseDate,@DateShipped,@ShippingProvider,@CreatedBy,@IsActiveQuote,@ValidTillDate,@QuoteLineItem,@CurrencyCode,@CurrencyName,"+
                                "@TaxRate,@IsActiveQuoteSettings,@QuoteStageId,@IsActiveQuoteHistory,@QuoteJSON",
                                new Object[]
                                {
                                    new SqlParameter("Id", Model.Id),
                                    new SqlParameter("QuoteNumber", Model.QuoteNumber),
                                    new SqlParameter("OpportunityId", Model.OpportunityId==null?(object)DBNull.Value:Model.OpportunityId),
                                    new SqlParameter("PurchaseOrderNo", Model.PurchaseOrderNo),
                                    new SqlParameter("PaymentTermId", Model.PaymentTermId==null?(object)DBNull.Value:Model.PaymentTermId),
                                    new SqlParameter("OriginalPODate", Model.OriginalPODate==null?(object)DBNull.Value:Model.OriginalPODate),
                                    new SqlParameter("ActualCloseDate", Model.ActualCloseDate==null?(object)DBNull.Value:Model.ActualCloseDate),
                                    new SqlParameter("DateShipped", Model.DateShipped==null?(object)DBNull.Value:Model.DateShipped),
                                    new SqlParameter("ShippingProvider", Model.ShippingProvider),
                                    new SqlParameter("CreatedBy", Model.CreatedBy),
                                    new SqlParameter("IsActiveQuote", Model.IsActiveQuote),
                                    new SqlParameter("ValidTillDate", Model.ValidTillDate==null?(object)DBNull.Value:Model.ValidTillDate),
                                    new SqlParameter("QuoteLineItem", Model.QuoteLineItem),
                                    new SqlParameter("CurrencyCode", Model.CurrencyCode),
                                    new SqlParameter("CurrencyName", Model.CurrencyName),
                                    new SqlParameter("TaxRate", Model.TaxRate==null?(object)DBNull.Value:Model.TaxRate),
                                    new SqlParameter("IsActiveQuoteSettings", Model.IsActiveQuoteSettings),
                                    new SqlParameter("QuoteStageId", Model.QuoteStageId==null?(object)DBNull.Value:Model.QuoteStageId),
                                    new SqlParameter("IsActiveQuoteHistory", Model.IsActiveQuoteHistory),
                                    new SqlParameter("QuoteJSON", Model.QuoteJSON)
                                }
                             );
            }
        }
        public static QuoteDbModel GetQuoteById(int Id)
        {
            using (var Context = new CRMContext())
            {
                return Context.Database.SqlQuery<QuoteDbModel>(
                                "exec dbo.[usp_Sales_Quote_GetById] @Id",
                                new Object[]
                                {
                                    new SqlParameter("Id", Id)
                                }
                             ).FirstOrDefault();
            }
        }
    }
}
