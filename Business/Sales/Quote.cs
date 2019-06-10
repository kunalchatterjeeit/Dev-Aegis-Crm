using Business.Common;
using DataAccessEntity.Sales;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Sales
{
    public class Quote
    {
        public Quote() { }
        public List<Entity.Sales.QuoteStage> GetQuoteStage()
        {
            List<Entity.Sales.QuoteStage> QuoteStageList = new List<Entity.Sales.QuoteStage>();
            QuoteDataAccess.GetQuoteStage().CopyListTo(QuoteStageList);
            return QuoteStageList;
        }
        public List<Entity.Sales.PaymentTerm> GetPaymentTerm()
        {
            List<Entity.Sales.PaymentTerm> PaymentTermsList = new List<Entity.Sales.PaymentTerm>();
            QuoteDataAccess.GetPeymentTerm().CopyListTo(PaymentTermsList);
            return PaymentTermsList;
        }
        public List<Entity.Sales.GetOpportunity> GetAllOpportunity(Entity.Sales.GetOpportunityParam Param)
        {
            List<Entity.Sales.GetOpportunity> AllOpportunityList = new List<Entity.Sales.GetOpportunity>();
            GetOpportunityParamDbModel p = new GetOpportunityParamDbModel();
            Param.CopyPropertiesTo(p);
            OpportunityDataAccess.GetAllOpportunities(p).CopyListTo(AllOpportunityList);
            return AllOpportunityList;
        }
        public List<Entity.Sales.GetQuote> GetAllQuotes(Entity.Sales.GetQuoteParam Param)
        {
            List<Entity.Sales.GetQuote> AllQuoteList = new List<Entity.Sales.GetQuote>();
            GetQuoteParamDbModel p = new GetQuoteParamDbModel();
            Param.CopyPropertiesTo(p);
            QuoteDataAccess.GetAllQuotes(p).CopyListTo(AllQuoteList);
            return AllQuoteList;
        }
        public int SaveQuote(Entity.Sales.Quote Model)
        {
            QuoteDbModel DbModel = new QuoteDbModel();
            Model.CopyPropertiesTo(DbModel);
            return QuoteDataAccess.SaveQuotes(DbModel);
        }
        public Entity.Sales.Quote GetQuoteById(int Id)
        {
            Entity.Sales.Quote Quote = new Entity.Sales.Quote();
            QuoteDataAccess.GetQuoteById(Id).CopyPropertiesTo(Quote);
            return Quote;
        }
    }
}
