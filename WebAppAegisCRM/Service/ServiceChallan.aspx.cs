using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business;
using Entity.Inventory;
using Business.Common;

namespace WebAppAegisCRM.Service
{
    public partial class ServiceChallan : System.Web.UI.Page
    {
        private void GetSpareInventory_ByProductId(Int64 productId, int assetLocationId)
        {
            try
            {
                Business.Service.ServiceBook objServiceBook = new Business.Service.ServiceBook();
                DataTable dt = objServiceBook.GetSpareInventory_ByProductId(productId, assetLocationId);
                dt.Columns.Add("IsSelected");
                if (Business.Common.Context.SelectedAssets.Rows.Count > 0)
                {
                    foreach (DataRow drSelected in Business.Common.Context.SelectedAssets.Rows)
                    {
                        if (dt.AsEnumerable().Select(item => item["AssetId"] == drSelected["AssetId"]).Any())
                        {
                            dt.AsEnumerable().Where(item => Guid.Parse(item["AssetId"].ToString()) == Guid.Parse(drSelected["AssetId"].ToString())).FirstOrDefault()["IsSelected"] = "1";
                            dt.AcceptChanges();
                        }
                    }
                }
                if (dt != null)
                {
                    RepeaterInventory.DataSource = dt;
                    RepeaterInventory.DataBind();
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
            }
        }

        private void LoadSelectedAssets()
        {
            gvSelectedAsset.DataSource = Business.Common.Context.SelectedAssets;
            gvSelectedAsset.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetSpareInventory_ByProductId(Business.Common.Context.ProductId, (int)AssetLocation.Store);
                if (Business.Common.Context.SelectedAssets != null && Business.Common.Context.SelectedAssets.Rows.Count > 0)
                {
                    LoadSelectedAssets();
                }
            }
        }

        protected void RepeaterInventory_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Add")
            {
                string assetId = e.CommandArgument.ToString().Split('|')[0];
                string itemId = e.CommandArgument.ToString().Split('|')[1];
                if (Business.Common.Context.SelectedAssets.Rows.Count == 0)
                {
                    Business.Common.Context.SelectedAssets = new DataTable();
                    Business.Common.Context.SelectedAssets.Columns.Add("AssetId");
                    Business.Common.Context.SelectedAssets.Columns.Add("ItemId");
                }
                DataRow dr = Business.Common.Context.SelectedAssets.NewRow();
                dr["AssetId"] = assetId;
                dr["ItemId"] = itemId;
                Business.Common.Context.SelectedAssets.Rows.Add(dr);
                GetSpareInventory_ByProductId(Business.Common.Context.ProductId, (int)AssetLocation.Store);
                LoadSelectedAssets();
            }
        }

        protected void gvSelectedAsset_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "D")
            {
                string assetId = e.CommandArgument.ToString();
                Business.Common.Context.SelectedAssets
                    .Rows[Business.Common.Context.SelectedAssets.Rows
                    .IndexOf(Business.Common.Context.SelectedAssets
                    .Select("AssetId='" + assetId + "'").FirstOrDefault())].Delete();
                Business.Common.Context.SelectedAssets.AcceptChanges();

                GetSpareInventory_ByProductId(Business.Common.Context.ProductId, (int)AssetLocation.Store);
                LoadSelectedAssets();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            Business.Common.Context.Signature = signature.Value;
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "script", "window.close();", true);
        }
    }
}