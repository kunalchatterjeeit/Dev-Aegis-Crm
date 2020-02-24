using Business.Common;
using log4net;
using System;

namespace WebAppAegisCRM.Sales
{
    public partial class QuoteBeforePrint : System.Web.UI.Page
    {
        ILog logger = log4net.LogManager.GetLogger("ErrorLog");
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            { 
            if (Request.QueryString["QuoteId"] != null && Request.QueryString["QuoteId"].ToString().Length > 0)
            {
                LoadQuoteDetails(Convert.ToInt32(Request.QueryString["QuoteId"].ToString()));
            }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
            }
        }
        private void LoadQuoteDetails(int QuoteId)
        {
            Business.Sales.Quote Obj = new Business.Sales.Quote();

            gvQuotePrint.DataSource = Obj.GetQuoteByIdPrint(QuoteId);
            gvQuotePrint.DataBind();
        }
       
    }
}