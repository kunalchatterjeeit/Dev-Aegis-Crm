using Business.Common;
using Entity.Inventory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Sale
{
    public partial class AssetSelection : System.Web.UI.Page
    {
        public string _ItemName
        {
            get { return ViewState["_ItemName"].ToString(); }
            set { ViewState["_ItemName"] = value; }
        }
        public decimal _Quantity
        {
            get { return Convert.ToDecimal(ViewState["_Quantity"].ToString()); }
            set { ViewState["_Quantity"] = value; }
        }
        public ItemType _ItemType
        {
            get { return (ItemType)Enum.Parse(typeof(ItemType), ViewState["_ItemType"].ToString()); }
            set { ViewState["_ItemType"] = value; }
        }
        private void LoadItemFromStore()
        {
            Business.Inventory.Inventory objInventory = new Business.Inventory.Inventory();
            DataTable dt = objInventory.Inventory_GetInventoryItem(AssetLocation.Store, _ItemType, _ItemName);
            dt.Columns.Add("IsSelected");
            if (Business.Common.Context.SelectedSaleAssets.Rows.Count > 0)
            {
                foreach (DataRow drSelected in Business.Common.Context.SelectedSaleAssets.Rows)
                {
                    if (dt.AsEnumerable().Where(item => item["AssetId"].ToString() == drSelected["AssetId"].ToString()).Any())
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

        private void LoadSelectedSaleAssets()
        {
            if (Business.Common.Context.SelectedSaleAssets.AsEnumerable().Where(p => p["ItemName"].ToString() == _ItemName).Any())
            {
                gvSelectedAsset.DataSource = Business.Common.Context.SelectedSaleAssets.AsEnumerable().Where(p => p["ItemName"].ToString() == _ItemName).CopyToDataTable();
                gvSelectedAsset.DataBind();
            }
            else
            {
                gvSelectedAsset.DataSource = null;
                gvSelectedAsset.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Message.Show = false;

                if (Request.QueryString["ItemNo"] != null)
                {
                    string itemNo = Request.QueryString["ItemNo"].ToString();
                    if (itemNo.Contains("(P)"))
                    {
                        _ItemType = ItemType.Product;
                    }
                    else
                    {
                        _ItemType = ItemType.Spare;
                    }
                    if (Request.QueryString["Quantity"] != null)
                    {
                        _Quantity = Convert.ToDecimal(Request.QueryString["Quantity"]);
                    }
                    _ItemName = itemNo.Substring(0, itemNo.Length - 4);
                    LoadItemFromStore();
                }
                if (Business.Common.Context.SelectedSaleAssets != null && Business.Common.Context.SelectedSaleAssets.Rows.Count > 0)
                {
                    LoadSelectedSaleAssets();
                }
            }
        }

        protected void RepeaterInventory_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Business.Service.ServiceBook objServiceBook = new Business.Service.ServiceBook();
            if (e.CommandName == "Add")
            {
                Message.Show = false;
                string assetId = e.CommandArgument.ToString().Split('|')[0];
                string itemId = e.CommandArgument.ToString().Split('|')[1];
                if (objServiceBook.Service_GetServiceBookDetailsApprovalStatus(Business.Common.Context.ServiceBookId, Convert.ToInt64(itemId)) == Entity.Service.ApprovalStatus.Rejected)
                {
                    Message.IsSuccess = false;
                    Message.Text = "Invalid request. Not able to add since it is already rejected.";
                    Message.Show = true;
                }
                else if (Business.Common.Context.SelectedSaleAssets != null && Business.Common.Context.SelectedSaleAssets.Rows.Count > 0
                    && Business.Common.Context.SelectedSaleAssets.AsEnumerable().Where(p => p["ItemId"].ToString() == itemId).Any()
                    && Business.Common.Context.SelectedSaleAssets.AsEnumerable().Count(p => p["ItemId"].ToString() == itemId)
                    >= _Quantity)
                {
                    Message.IsSuccess = false;
                    Message.Text = "Max limit of selection is reached.";
                    Message.Show = true;
                }
                else
                {
                    if (Business.Common.Context.SelectedSaleAssets.Rows.Count == 0)
                    {
                        Business.Common.Context.SelectedSaleAssets = new DataTable();
                        Business.Common.Context.SelectedSaleAssets.Columns.Add("AssetId");
                        Business.Common.Context.SelectedSaleAssets.Columns.Add("ItemId");
                        Business.Common.Context.SelectedSaleAssets.Columns.Add("ItemName");
                        Business.Common.Context.SelectedSaleAssets.Columns.Add("ItemType");
                        Business.Common.Context.SelectedSaleAssets.Columns.Add("Finalized");
                    }
                    DataRow dr = Business.Common.Context.SelectedSaleAssets.NewRow();
                    dr["AssetId"] = assetId;
                    dr["ItemId"] = itemId;
                    dr["ItemName"] = _ItemName;
                    dr["ItemType"] = (int)_ItemType;
                    dr["Finalized"] = "False";
                    Business.Common.Context.SelectedSaleAssets.Rows.Add(dr);

                    LoadItemFromStore();
                    LoadSelectedSaleAssets();
                }
            }
        }

        protected void gvSelectedAsset_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "D")
            {
                string assetId = e.CommandArgument.ToString();
                Business.Common.Context.SelectedSaleAssets
                    .Rows[Business.Common.Context.SelectedSaleAssets.Rows
                    .IndexOf(Business.Common.Context.SelectedSaleAssets
                    .Select("AssetId='" + assetId + "'").FirstOrDefault())].Delete();
                Business.Common.Context.SelectedSaleAssets.AcceptChanges();

                LoadItemFromStore();
                LoadSelectedSaleAssets();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Business.Common.Context.SelectedSaleAssets != null && Business.Common.Context.SelectedSaleAssets.Rows.Count > 0
                    && Business.Common.Context.SelectedSaleAssets.AsEnumerable().Where(p => p["ItemName"].ToString() == _ItemName).Any()
                    && Business.Common.Context.SelectedSaleAssets.AsEnumerable().Count(p => p["ItemName"].ToString() == _ItemName)
                    != _Quantity)
            {
                Message.IsSuccess = false;
                Message.Text = "Please select exact quantity. You suppose to choose " + _Quantity.ToString() + " item(s).";
                Message.Show = true;
            }
            else
            {
                Business.Common.Context.SelectedSaleAssets.AsEnumerable().ToList<DataRow>().ForEach(r => { r["Finalized"] = "True"; });
                ScriptManager.RegisterStartupScript(this, this.GetType(), "mmsg", "window.close();", true);
            }
        }
    }
}