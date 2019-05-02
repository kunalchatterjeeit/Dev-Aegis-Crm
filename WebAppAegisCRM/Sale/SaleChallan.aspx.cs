﻿using Entity.Inventory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Common;
using Entity.Inventory;

namespace WebAppAegisCRM.Sale
{
    public partial class SaleChallan : System.Web.UI.Page
    {
        private DataTable _ItemsList
        {
            get
            {
                return (Session["ItemsList"] != null)
                    ? (DataTable)Session["ItemsList"]
                    : new DataTable();
            }

            set { Session["ItemsList"] = value; }
        }

        private void LoadItemList()
        {
            gvItem.DataSource = _ItemsList;
            gvItem.DataBind();
        }

        private void LoadAllItem()
        {
            using (DataTable dtItem = new DataTable())
            {
                dtItem.Columns.Add("ItemIdType");
                dtItem.Columns.Add("ItemName");

                Business.Inventory.ProductMaster objProductMaster = new Business.Inventory.ProductMaster();

                foreach (DataRow drItem in objProductMaster.GetAll(new Entity.Inventory.ProductMaster() { CompanyMasterId = 1 }).Rows)
                {
                    DataRow drNewItem = dtItem.NewRow();
                    drNewItem["ItemIdType"] = drItem["ProductMasterId"].ToString() + "|" + (int)ItemType.Product;
                    drNewItem["ItemName"] = drItem["ProductName"].ToString() + " (P)";
                    dtItem.Rows.Add(drNewItem);
                    dtItem.AcceptChanges();
                }

                Business.Inventory.SpareMaster objSpareMaster = new Business.Inventory.SpareMaster();

                foreach (DataRow drItem in objSpareMaster.GetAll(new Entity.Inventory.SpareMaster() { }).Rows)
                {
                    DataRow drNewItem = dtItem.NewRow();
                    drNewItem["ItemIdType"] = drItem["SpareId"].ToString() + "|" + (int)ItemType.Spare;
                    drNewItem["ItemName"] = drItem["SpareName"].ToString() + " (S)";
                    dtItem.Rows.Add(drNewItem);
                    dtItem.AcceptChanges();
                }

                ddlItem.DataSource = dtItem;
                ddlItem.DataValueField = "ItemIdType";
                ddlItem.DataTextField = "ItemName";
                ddlItem.DataBind();
                ddlItem.InsertSelect();
            }
        }

        private bool DeleteItem(string itemIdType)
        {
            bool retValue = false;
            int lastCount = 0;
            if (_ItemsList.Rows.Count > 0)
            {
                lastCount = _ItemsList.Rows.Count;
                _ItemsList.Rows[_ItemsList.Rows.IndexOf(_ItemsList.Select("ItemIdType='" + itemIdType + "'").FirstOrDefault())].Delete();
                _ItemsList.AcceptChanges();
            }
            if (lastCount > _ItemsList.Rows.Count)
            {
                retValue = true;
            }
            return retValue;
        }

        private bool Validation()
        {
            bool retValue = true;

            if (retValue && string.IsNullOrEmpty(txtOrderDate.Text.Trim()))
            {
                retValue = false;
                Message.Text = "Please enter order date.";
                Message.IsSuccess = false;
                Message.Show = true;
            }

            if (retValue && ddlChallanType.SelectedIndex == 0)
            {
                retValue = false;
                Message.Text = "Please select challan type.";
                Message.IsSuccess = false;
                Message.Show = true;
            }

            if (retValue && !(_ItemsList != null && _ItemsList.Rows.Count > 0))
            {
                retValue = false;
                Message.Text = "Please add Items.";
                Message.IsSuccess = false;
                Message.Show = true;
            }

            if (Business.Common.Context.SelectedSaleAssets == null)
            {
                retValue = false;
                ClearMasterControls();
                ClearItemControls();
                Business.Common.Context.SelectedSaleAssets.Clear();
                LoadItemList();
                Message.Text = "Please select asset particulars.";
                Message.IsSuccess = false;
                Message.Show = true;
            }

            if (!Business.Common.Context.SelectedSaleAssets.AsEnumerable().Any())
            {
                retValue = false;
                ClearMasterControls();
                ClearItemControls();
                Business.Common.Context.SelectedSaleAssets.Clear();
                LoadItemList();
                Message.Text = "Please select asset particulars.";
                Message.IsSuccess = false;
                Message.Show = true;
            }

            if (Business.Common.Context.SelectedSaleAssets.AsEnumerable().Where(p => p["Finalized"].ToString() == "False").Any())
            {
                retValue = false;
                ClearMasterControls();
                ClearItemControls();
                Business.Common.Context.SelectedSaleAssets.Clear();
                LoadItemList();
                Message.Text = "Please click on Proceed button each time in Asset Selection page.";
                Message.IsSuccess = false;
                Message.Show = true;
            }

            return retValue;
        }

        private void ClearMasterControls()
        {
            Message.Show = false;
            txtOrderDate.Text = DateTime.Now.ToString("dd MMM yyyy");
            txtChallanNo.Text = string.Empty;
            txtOrderNo.Text = string.Empty;
            ddlChallanType.SelectedIndex = 0;
            _ItemsList = null;
        }

        private void ClearItemControls()
        {
            ddlItem.SelectedIndex = 0;
            txtQuantity.Text = string.Empty;
            txtRate.Text = string.Empty;
            txtGst.Text = string.Empty;
            txtHsnCode.Text = string.Empty;
            //_ItemsList = null;
            ddlItem.Focus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadAllItem();
                LoadItemList();
                ClearMasterControls();
                ClearItemControls();
                Business.Common.Context.SelectedSaleAssets.Clear();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (_ItemsList.Rows.Count == 0)
            {
                using (DataTable dtInstance = new DataTable())
                {
                    dtInstance.Columns.Add("ItemIdType");
                    dtInstance.Columns.Add("ItemName");
                    dtInstance.Columns.Add("ItemType");
                    dtInstance.Columns.Add("Quantity");
                    dtInstance.Columns.Add("Rate");
                    dtInstance.Columns.Add("GST");
                    dtInstance.Columns.Add("HSNCode");
                    _ItemsList = dtInstance;
                }
            }

            DataRow drItem = _ItemsList.NewRow();
            drItem["ItemIdType"] = ddlItem.SelectedValue;
            drItem["ItemName"] = ddlItem.SelectedItem.Text;
            drItem["ItemType"] = (ddlItem.SelectedValue.Split('|')[1] == ((int)ItemType.Product).ToString()) ? ItemType.Product.ToString() : ItemType.Spare.ToString();
            drItem["Quantity"] = txtQuantity.Text.Trim();
            drItem["Rate"] = txtRate.Text.Trim();
            drItem["GST"] = txtGst.Text.Trim();
            drItem["HSNCode"] = txtHsnCode.Text.Trim();

            _ItemsList.Rows.Add(drItem);
            _ItemsList.AcceptChanges();

            LoadItemList();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "mmsg", "OpenWindow('AssetSelection.aspx?ItemNo=" + ddlItem.SelectedItem.Text + "&Quantity=" + txtQuantity.Text + "');", true);

            ClearItemControls();
        }

        protected void gvItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "D")
            {
                string itemIdType = e.CommandArgument.ToString();

                if (DeleteItem(itemIdType))
                {
                    if (DeleteSelectedSaleAssets(itemIdType))
                    {
                        LoadItemList();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "mmsg", "alert('Data can not be deleted!!!....');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "mmsg", "alert('Data can not be deleted!!!....');", true);
                }

            }
        }

        private bool DeleteSelectedSaleAssets(string itemIdType)
        {
            if (Business.Common.Context.SelectedSaleAssets.AsEnumerable().Where(p => p["ItemId"].ToString() == itemIdType.Split('|')[0]).Any())
            {
                DataRow[] selectedDataRow = Business.Common.Context.SelectedSaleAssets.Select("ItemId=" + itemIdType.Split('|')[0]);
                foreach (DataRow dataRow in selectedDataRow)
                {
                    dataRow.Delete();
                }
                Business.Common.Context.SelectedSaleAssets.AcceptChanges();
            }
            return true;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation())
                {
                    Entity.Sale.SaleChallan saleChallan = new Entity.Sale.SaleChallan();
                    Business.Sale.SaleChallan objSaleChallan = new Business.Sale.SaleChallan();
                    Entity.Sale.SaleChallanDetails saleChallanDetails = new Entity.Sale.SaleChallanDetails();
                    Entity.Inventory.Inventory inventory = new Entity.Inventory.Inventory();
                    Business.Inventory.Inventory objInventory = new Business.Inventory.Inventory();

                    saleChallan.ChallanNo = (!string.IsNullOrEmpty(txtChallanNo.Text.Trim())) ? txtChallanNo.Text.Trim() : string.Empty;
                    saleChallan.OrderNo = (!string.IsNullOrEmpty(txtOrderNo.Text.Trim())) ? txtOrderNo.Text.Trim() : string.Empty;
                    saleChallan.OrderDate = (!string.IsNullOrEmpty(txtOrderDate.Text.Trim())) ? Convert.ToDateTime(txtOrderDate.Text.Trim()) : DateTime.MinValue;
                    saleChallan.CallanTypeId = Convert.ToInt32(ddlChallanType.SelectedValue);
                    saleChallan.CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    int saleChallanId = objSaleChallan.SaleChallan_Save(saleChallan);
                    int purchaseDetailsResponse = SaveSaleChallanDetails(saleChallan, objSaleChallan, saleChallanId);

                    if (purchaseDetailsResponse > 0)
                    {
                        using (DataTable dtInventory = new DataTable())
                        {
                            int inventoryResponse = SaveInventoryDetails(inventory, objInventory, saleChallanId, dtInventory);

                            if (inventoryResponse > 0)
                            {
                                ClearMasterControls();
                                ClearItemControls();
                                Business.Common.Context.SelectedSaleAssets.Clear();
                                LoadItemList();
                                Message.IsSuccess = true;
                                Message.Text = "Sale Order saved";
                            }
                            else
                            {
                                Message.IsSuccess = false;
                                Message.Text = "Inventory not saved";
                            }
                        }
                    }
                    else
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Sale Order not saved";
                    }
                    Message.Show = true;
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }

        private int SaveSaleChallanDetails(Entity.Sale.SaleChallan saleChallan, Business.Sale.SaleChallan objSaleChallan, int saleChallanId)
        {
            foreach (DataRow drItem in _ItemsList.Rows)
            {
                saleChallan.SaleChallanDetailsCollection.Add(
                new Entity.Sale.SaleChallanDetails()
                {
                    SaleChallanId = saleChallanId,
                    ItemId = Convert.ToInt32(drItem["ItemIdType"].ToString().Split('|')[0]),
                    ItemType = Convert.ToInt32(drItem["ItemIdType"].ToString().Split('|')[1]),
                    ItemQty = (!string.IsNullOrEmpty(drItem["Quantity"].ToString())) ? Convert.ToDecimal(drItem["Quantity"].ToString()) : 0,
                    ItemRate = (!string.IsNullOrEmpty(drItem["Rate"].ToString())) ? Convert.ToDecimal(drItem["Rate"].ToString()) : 0,
                    GST = (!string.IsNullOrEmpty(drItem["GST"].ToString())) ? Convert.ToDecimal(drItem["GST"].ToString()) : 0,
                    HsnCode = drItem["HsnCode"].ToString(),
                    Uom = 1
                });
            }
            int purchaseDetailsResponse = objSaleChallan.Sale_ChallanDetails_Save(saleChallan);
            return purchaseDetailsResponse;
        }

        private static int SaveInventoryDetails(Entity.Inventory.Inventory inventory, Business.Inventory.Inventory objInventory, int saleChallanId, DataTable dtInventory)
        {
            dtInventory.Columns.Add("AssetId");
            dtInventory.Columns.Add("ItemId");
            dtInventory.Columns.Add("ItemType");
            dtInventory.Columns.Add("AssetLocationId");
            dtInventory.Columns.Add("CustomerId");
            dtInventory.Columns.Add("SaleChallanId");
            dtInventory.Columns.Add("EmployeeId");

            foreach (DataRow drItem in Business.Common.Context.SelectedSaleAssets.Rows)
            {
                DataRow drInventoryItem = dtInventory.NewRow();
                drInventoryItem["AssetId"] = drItem["AssetId"].ToString().ToUpper();
                drInventoryItem["ItemId"] = drItem["ItemId"].ToString();
                drInventoryItem["ItemType"] = drItem["ItemType"].ToString();
                drInventoryItem["AssetLocationId"] = (int)AssetLocation.Sale; //Stock In
                drInventoryItem["CustomerId"] = "";
                drInventoryItem["SaleChallanId"] = saleChallanId;
                drInventoryItem["EmployeeId"] = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                dtInventory.Rows.Add(drInventoryItem);
                dtInventory.AcceptChanges();
            }

            inventory.InventoryDetails = dtInventory;
            int inventoryResponse = objInventory.Inventory_Movement(inventory);
            return inventoryResponse;
        }
    }
}