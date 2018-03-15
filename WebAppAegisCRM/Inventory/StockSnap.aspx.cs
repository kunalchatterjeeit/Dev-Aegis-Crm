using Entity.Inventory;
using System;
using System.Data;
using Business.Common;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Inventory
{
    public partial class StockSnap : System.Web.UI.Page
    {
        private void GetStockSnap()
        {
            using (DataTable dtItem = new DataTable())
            {
                dtItem.Columns.Add("ItemIdType");
                dtItem.Columns.Add("ItemName");

                Business.Inventory.Stock objStock = new Business.Inventory.Stock();
                gvStockSnap.DataSource = objStock.GetStockSnap();
                gvStockSnap.DataBind();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetStockSnap();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetStockSnap();
        }

        protected void gvStockSnap_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvStockSnap.PageIndex = e.NewPageIndex;
            GetStockSnap();
        }
    }
}