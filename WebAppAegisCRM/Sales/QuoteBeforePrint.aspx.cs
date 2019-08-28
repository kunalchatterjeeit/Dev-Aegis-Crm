using System;

namespace WebAppAegisCRM.Sales
{
    public partial class QuoteBeforePrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["QuoteId"] != null && Request.QueryString["QuoteId"].ToString().Length > 0)
            {
                LoadQuoteDetails(Convert.ToInt32(Request.QueryString["QuoteId"].ToString()));
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