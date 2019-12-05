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
        private void GetSpareInventory()
        {
            try
            {
                Business.Inventory.Inventory objInventory = new Business.Inventory.Inventory();
                DataTable dt = objInventory.Inventory_GetApprovedInventorySpareByServiceBookId(Business.Common.Context.ServiceBookId, AssetLocation.Store, ItemType.Spare, Convert.ToInt32(ddlStore.SelectedValue));
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

        private void GetTonerInventory()
        {
            try
            {
                Business.Inventory.Inventory objInventory = new Business.Inventory.Inventory();
                DataTable dt = objInventory.Inventory_GetApprovedInventorySpareByServiceBookId(Business.Common.Context.ServiceBookId, AssetLocation.Store, ItemType.Toner, Convert.ToInt32(ddlStore.SelectedValue));
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
                LoadStore();
                Message.Show = false;
                
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
                string stockLocationId = e.CommandArgument.ToString().Split('|')[2];
                if (objServiceBook.Service_GetServiceBookDetailsApprovalStatus(Business.Common.Context.ServiceBookId, Convert.ToInt64(itemId)) == Entity.Service.ApprovalStatus.Rejected)
                {
                    Message.IsSuccess = false;
                    Message.Text = "Invalid request. Not able to add since it is already rejected.";
                    Message.Show = true;
                }
                else if (Business.Common.Context.SelectedAssets != null && Business.Common.Context.SelectedAssets.Rows.Count > 0
                    && Business.Common.Context.SelectedAssets.AsEnumerable().Where(p=>p["ItemId"].ToString() == itemId).Any()
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
                        Business.Common.Context.SelectedAssets.Columns.Add("StockLocationId");
                    }
                    DataRow dr = Business.Common.Context.SelectedAssets.NewRow();
                    dr["AssetId"] = assetId;
                    dr["ItemId"] = itemId;
                    dr["StockLocationId"] = stockLocationId;
                    Business.Common.Context.SelectedAssets.Rows.Add(dr);

                    if (Business.Common.Context.CallType == Entity.Service.CallType.Docket)
                    {
                        GetSpareInventory();
                    }
                    else if (Business.Common.Context.CallType == Entity.Service.CallType.Toner)
                    {
                        GetTonerInventory();
                    }

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

                if (Business.Common.Context.CallType == Entity.Service.CallType.Docket)
                {
                    GetSpareInventory();
                }
                else if (Business.Common.Context.CallType == Entity.Service.CallType.Toner)
                {
                    GetTonerInventory();
                }

                LoadSelectedAssets();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            Business.Common.Context.Signature = signature.Value;
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "script", "window.close();", true);
        }

        private void LoadStore()
        {
            Business.Inventory.StoreMaster objStoreMaster = new Business.Inventory.StoreMaster();
            ddlStore.DataSource = objStoreMaster.GetAll();
            ddlStore.DataTextField = "StoreName";
            ddlStore.DataValueField = "StoreId";
            ddlStore.DataBind();
            ddlStore.InsertSelect();
        }

        protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Business.Common.Context.CallType == Entity.Service.CallType.Docket)
            {
                btnSign.Visible = true;
                btnDone.Visible = false;
                GetSpareInventory();
            }
            else if (Business.Common.Context.CallType == Entity.Service.CallType.Toner)
            {
                btnSign.Visible = false;
                btnDone.Visible = true;
                GetTonerInventory();
            }
        }
    }
}