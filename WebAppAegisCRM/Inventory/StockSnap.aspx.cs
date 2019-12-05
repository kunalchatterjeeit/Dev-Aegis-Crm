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
                string name = (string.IsNullOrEmpty(txtName.Text.Trim())) ? string.Empty : txtName.Text.Trim();
                gvStockSnap.DataSource = objStock.GetStockSnap(name);
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

        protected void gvStockSnap_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Details")
            {
                string itemId = e.CommandArgument.ToString().Split('|')[0];
                string itemType = e.CommandArgument.ToString().Split('|')[1];
                Business.Inventory.Inventory objInventory = new Business.Inventory.Inventory();
                DataTable dtDetails = objInventory.Inventory_StockLocationWiseQuantity(Convert.ToInt32(itemId), (ItemType)Enum.Parse(typeof(ItemType), itemType));
                gvStockLocation.DataSource = dtDetails;
                gvStockLocation.DataBind();
                ModalPopupExtender2.Show();
            }
        }

    }
}