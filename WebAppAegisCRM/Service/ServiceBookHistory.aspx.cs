using Business.Common;
using Entity.Service;
using System;
using System.Data;

namespace WebAppAegisCRM.Service
{
    public partial class ServiceBookHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    LoadServiceBookMasterHistory();
                    string source = string.Concat("CSR.aspx", "?docketno=", Convert.ToString(Business.Common.Context.DocketNo));
                    iframe.Attributes.Add("src", source);
                }
                catch (Exception ex)
                {
                    ex.WriteException();
                }
            }
        }

        private void LoadServiceBookMasterHistory()
        {
            Business.Service.ServiceBook objServiceBook = new Business.Service.ServiceBook();
            DataTable dt = objServiceBook.ServiceBookMasterHistory_GetAllByCallId(Business.Common.Context.CallId, (int)CallType.Docket);
            if (dt != null)
            {
                gvDocketClosingHistory.DataSource = dt;
                gvDocketClosingHistory.DataBind();
            }
        }
    }
}