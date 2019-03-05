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
        public ItemType _ItemType
        {
            get { return (ItemType)Enum.Parse(typeof(ItemType),ViewState["_ItemType"].ToString()); }
            set { ViewState["_ItemType"] = value; }
        }
        private void LoadItemFromStore()
        {
            Business.Inventory.Inventory objInventory = new Business.Inventory.Inventory();
            DataTable dt = objInventory.Inventory_GetInventoryItem(AssetLocation.Store, _ItemType, _ItemName);
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

        private void LoadSelectedAssets()
        {
            gvSelectedAsset.DataSource = Business.Common.Context.SelectedAssets;
            gvSelectedAsset.DataBind();
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
                    _ItemName = itemNo.Substring(0, itemNo.Length - 4);
                    LoadItemFromStore();
                }   
                if (Business.Common.Context.SelectedAssets != null && Business.Common.Context.SelectedAssets.Rows.Count > 0)
                {
                    LoadSelectedAssets();
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
                else if (Business.Common.Context.SelectedAssets != null && Business.Common.Context.SelectedAssets.Rows.Count > 0
                    && Business.Common.Context.SelectedAssets.AsEnumerable().Where(p => p["ItemId"].ToString() == itemId).Any()
                    && Business.Common.Context.SelectedAssets.AsEnumerable().Count(p => p["ItemId"].ToString() == itemId)
                    >= Convert.ToDecimal(objServiceBook.Service_ServiceBookDetailsApproval_GetById(Business.Common.Context.ServiceBookId, Convert.ToInt64(itemId)).Tables[0].Rows[0]["RequisiteQty"].ToString()))
                {
                    Message.IsSuccess = false;
                    Message.Text = "Max limit of selection is reached.";
                    Message.Show = true;
                }
                else
                {
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

                    LoadItemFromStore();
                    LoadSelectedAssets();
                }
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

                LoadItemFromStore();
                LoadSelectedAssets();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "script", "window.close();", true);
        }
    }
}