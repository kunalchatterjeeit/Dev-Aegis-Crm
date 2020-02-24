using Business.Common;
using Entity.Inventory;
using log4net;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Inventory
{
    public partial class StockSnap : System.Web.UI.Page
    {
        ILog logger = log4net.LogManager.GetLogger("ErrorLog");
        private void GetStockSnap()
        {
            Business.Inventory.Stock objStock = new Business.Inventory.Stock();
            string name = (string.IsNullOrEmpty(txtName.Text.Trim())) ? string.Empty : txtName.Text.Trim();
            gvStockSnap.DataSource = objStock.GetStockSnap(name);
            gvStockSnap.DataBind();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    GetStockSnap();
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetStockSnap();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
            }
        }

        protected void gvStockSnap_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvStockSnap.PageIndex = e.NewPageIndex;
                GetStockSnap();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
            }
        }

        protected void gvStockSnap_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Details")
                {
                    string itemId = e.CommandArgument.ToString().Split('|')[0];
                    string itemType = e.CommandArgument.ToString().Split('|')[1];
                    string assetLocationId = e.CommandArgument.ToString().Split('|')[2];
                    Business.Inventory.Inventory objInventory = new Business.Inventory.Inventory();
                    if (Convert.ToInt32(assetLocationId) == (int)AssetLocation.Store)
                    {
                        DataTable dtDetails = objInventory.Inventory_StockLocationWiseQuantity(Convert.ToInt32(itemId), (ItemType)Enum.Parse(typeof(ItemType), itemType));
                        gvStockLocation.DataSource = dtDetails;
                        gvStockLocation.DataBind();
                    }
                    else if ((Convert.ToInt32(assetLocationId) == (int)AssetLocation.FOC)
                        || (Convert.ToInt32(assetLocationId) == (int)AssetLocation.Sale))
                    {
                        int challanTypeId = 0;
                        if ((AssetLocation)Enum.Parse(typeof(AssetLocation), assetLocationId) == AssetLocation.Sale)
                            challanTypeId = 1;
                        else
                            challanTypeId = 2;
                        DataTable dtDetails = objInventory.Inventory_SaleFocWiseQuantity(Convert.ToInt32(itemId), (ItemType)Enum.Parse(typeof(ItemType), itemType), challanTypeId);
                        gvStockLocation.DataSource = dtDetails;
                        gvStockLocation.DataBind();
                    }
                    ModalPopupExtender2.Show();
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
            }
        }

    }
}