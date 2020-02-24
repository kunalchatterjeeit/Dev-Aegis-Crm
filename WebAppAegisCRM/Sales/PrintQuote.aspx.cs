using Business.Common;
using Entity.Common;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Sales
{
    public partial class PrintQuote : System.Web.UI.Page
    {
        ILog logger = log4net.LogManager.GetLogger("ErrorLog");
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["StageId"] != null && Request.QueryString["StageId"].ToString().Length > 0)
                {
                    BindQuote(Convert.ToInt32(Request.QueryString["StageId"].ToString()));
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
            }
        }
        private void BindQuote(int Id)
        {
            Business.Sales.Quote Obj = new Business.Sales.Quote();
            string QuoteJson = Obj.GetQuoteJsonByStageId(Id);
            if (QuoteJson != null && QuoteJson != string.Empty)
            {
                Entity.Sales.Quote Quote = JsonConvert.DeserializeObject<Entity.Sales.Quote>(QuoteJson);
                lblQuoteNo.Text = Quote.QuoteNumber;
                lblDate.Text = System.DateTime.Today.ToString("dd MMM yyyy");
                List<Entity.Sales.QuoteLineItem> lineItem = JsonConvert.DeserializeObject<List<Entity.Sales.QuoteLineItem>>(Quote.QuoteLineItem);
                decimal total = 0;
                for (int i = 0; i < lineItem.Count; i++)
                {
                    if (lineItem[i].Quantity > 0 && lineItem[i].UnitPrice > 0)
                    {
                        lineItem[i].Amount = Math.Round(Convert.ToDecimal(lineItem[i].Quantity), 2) * Math.Round(Convert.ToDecimal(lineItem[i].UnitPrice), 2);
                        if (lineItem[i].Discount > 0)
                        {
                            lineItem[i].Amount = lineItem[i].Amount - (lineItem[i].Amount * lineItem[i].Discount / 100);
                        }
                        total = total + Math.Round(Convert.ToDecimal(lineItem[i].Amount), 2);
                    }
                }
                lblSubtotal.Text = Math.Round(total, 2).ToString();
                if (Quote.TaxRate != null || Quote.TaxRate == 0)
                {
                    lblTaxRate.Text = "0.00";
                    lblGST.Text = "0.00";
                    lblTotal.Text = Math.Round(total, 2).ToString();
                }
                else
                {
                    lblTaxRate.Text = Quote.TaxRate.ToString();
                    lblGST.Text = Math.Round(total * Convert.ToDecimal(Quote.TaxRate / 100), 2).ToString();
                    lblTotal.Text = Math.Round(total + total * Convert.ToDecimal(Quote.TaxRate / 100), 2).ToString();
                }
                Business.Sales.Opportunity opportunity = new Business.Sales.Opportunity();
                lblCustomer.Text = opportunity.GetOpportunityById(Convert.ToInt32(Quote.OpportunityId), Convert.ToInt32(ActityType.Lead), Convert.ToInt32(ActityType.Opportunity)).Name;
                if (lineItem.Count > 0)
                {
                    rptrRepeatLineItem.DataSource = lineItem.ToList();
                    rptrRepeatLineItem.DataBind();
                }
            }
        }
    }
}