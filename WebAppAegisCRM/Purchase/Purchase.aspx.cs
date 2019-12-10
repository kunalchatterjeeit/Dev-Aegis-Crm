using Entity.Inventory;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Common;

namespace WebAppAegisCRM.Purchase
{
    public partial class Purchase : System.Web.UI.Page
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

        private void LoadStore()
        {
            Business.Inventory.StoreMaster objStoreMaster = new Business.Inventory.StoreMaster();
            ddlStore.DataSource = objStoreMaster.GetAll();
            ddlStore.DataTextField = "StoreName";
            ddlStore.DataValueField = "StoreId";
            ddlStore.DataBind();
            ddlStore.InsertSelect();
        }

        private void LoadVendor()
        {
            Business.Purchase.Vendor objVendorMaster = new Business.Purchase.Vendor();
            DataTable dtVendor = objVendorMaster.GetAll(new Entity.Purchase.Vendor() { CompanyId = 1 });
            ddlVendor.DataSource = dtVendor;
            ddlVendor.DataTextField = "VendorMasterName";
            ddlVendor.DataValueField = "VendorMasterId";
            ddlVendor.DataBind();
            ddlVendor.InsertSelect();
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

            if (retValue && string.IsNullOrEmpty(txtPurchaseDate.Text.Trim()))
            {
                retValue = false;
                Message.Text = "Please enter purchase date.";
                Message.IsSuccess = false;
                Message.Show = true;
            }

            if (retValue && ddlVendor.SelectedIndex == 0)
            {
                retValue = false;
                Message.Text = "Please select vendor.";
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

            return retValue;
        }

        private void ClearMasterControls()
        {
            Message.Show = false;
            txtPurchaseOrderNo.Text = string.Empty;
            ddlVendor.SelectedIndex = 0;
            txtInvoiceNo.Text = string.Empty;
            txtInvoiceDate.Text = string.Empty;
            txtBillAmount.Text = string.Empty;
            txtPaymentAmount.Text = string.Empty;
            txtPurchaseDate.Text = DateTime.Now.ToString("dd MMM yyyy");
            ddlStore.SelectedIndex = 0;
            _ItemsList = null;
        }

        private void ClearItemControls()
        {
            ddlItem.SelectedIndex = 0;
            txtQuantity.Text = string.Empty;
            txtRate.Text = string.Empty;
            txtDiscount.Text = string.Empty;
            txtGst.Text = string.Empty;
            txtHsnCode.Text = string.Empty;
            //_ItemsList = null;
            ddlItem.Focus();
        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadStore();
                LoadVendor();
                LoadAllItem();
                LoadItemList();
                ClearMasterControls();
                ClearItemControls();
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
                    dtInstance.Columns.Add("Discount");
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
            drItem["Discount"] = txtDiscount.Text.Trim();
            drItem["GST"] = txtGst.Text.Trim();
            drItem["HSNCode"] = txtHsnCode.Text.Trim();

            _ItemsList.Rows.Add(drItem);
            _ItemsList.AcceptChanges();

            LoadItemList();
            ClearItemControls();
        }

        protected void gvItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "D")
            {
                string itemIdType = e.CommandArgument.ToString();

                if (DeleteItem(itemIdType))
                {
                    LoadItemList();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "mmsg", "alert('Data can not be deleted!!!....');", true);
                }

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (Validation())
            {
                Entity.Purchase.Purchase purchase = new Entity.Purchase.Purchase();
                Business.Purchase.Purchase objPurchase = new Business.Purchase.Purchase();
                Entity.Purchase.PurchaseDetails purchaseDetails = new Entity.Purchase.PurchaseDetails();
                Entity.Inventory.Inventory inventory = new Entity.Inventory.Inventory();
                Business.Inventory.Inventory objInventory = new Business.Inventory.Inventory();

                purchase.PurchaseOrderNo = (!string.IsNullOrEmpty(txtPurchaseOrderNo.Text.Trim())) ? txtPurchaseOrderNo.Text.Trim() : string.Empty;
                purchase.PurchaseDate = Convert.ToDateTime(txtPurchaseDate.Text.Trim());
                purchase.VendorId = Convert.ToInt32(ddlVendor.SelectedValue);
                purchase.InvoiceNo = (!string.IsNullOrEmpty(txtInvoiceNo.Text.Trim())) ? txtInvoiceNo.Text.Trim() : string.Empty;
                purchase.InvoiceDate = (!string.IsNullOrEmpty(txtInvoiceDate.Text.Trim())) ? Convert.ToDateTime(txtInvoiceDate.Text.Trim()) : DateTime.MinValue;
                purchase.BillAmount = (!string.IsNullOrEmpty(txtBillAmount.Text.Trim())) ? Convert.ToDecimal(txtBillAmount.Text.Trim()) : 0;
                purchase.PaymentAmount = (!string.IsNullOrEmpty(txtPaymentAmount.Text.Trim())) ? Convert.ToDecimal(txtPaymentAmount.Text.Trim()) : 0;
                int purchaseId = objPurchase.Purchase_Save(purchase);

                foreach (DataRow drItem in _ItemsList.Rows)
                {
                    purchase.PurchaseDetailsCollection.Add(
                    new Entity.Purchase.PurchaseDetails()
                    {
                        PurchaseId = purchaseId,
                        ItemId = Convert.ToInt32(drItem["ItemIdType"].ToString().Split('|')[0]),
                        ItemType = Convert.ToInt32(drItem["ItemIdType"].ToString().Split('|')[1]),
                        ItemQty = (!string.IsNullOrEmpty(drItem["Quantity"].ToString())) ? Convert.ToDecimal(drItem["Quantity"].ToString()) : 0,
                        ItemRate = (!string.IsNullOrEmpty(drItem["Rate"].ToString())) ? Convert.ToDecimal(drItem["Rate"].ToString()) : 0,
                        Discount = (!string.IsNullOrEmpty(drItem["Discount"].ToString())) ? Convert.ToDecimal(drItem["Discount"].ToString()) : 0,
                        GST = (!string.IsNullOrEmpty(drItem["GST"].ToString())) ? Convert.ToDecimal(drItem["GST"].ToString()) : 0,
                        HsnCode = drItem["HsnCode"].ToString()
                    });
                }
                int purchaseDetailsResponse = objPurchase.PurchaseDetails_Save(purchase);

                if (purchaseDetailsResponse > 0)
                {
                    using (DataTable dtInventory = new DataTable())
                    {
                        dtInventory.Columns.Add("AssetId");
                        dtInventory.Columns.Add("ItemId");
                        dtInventory.Columns.Add("ItemType");
                        dtInventory.Columns.Add("AssetLocationId");
                        dtInventory.Columns.Add("CustomerId");
                        dtInventory.Columns.Add("SaleChallanId");
                        dtInventory.Columns.Add("EmployeeId");
                        dtInventory.Columns.Add("StoreId");

                        foreach (DataRow drItem in _ItemsList.Rows)
                        {
                            for (int qty = 1; qty <= Convert.ToInt32(drItem["Quantity"]); qty++)
                            {
                                DataRow drInventoryItem = dtInventory.NewRow();
                                drInventoryItem["AssetId"] = Guid.NewGuid().ToString().ToUpper();
                                drInventoryItem["ItemId"] = drItem["ItemIdType"].ToString().Split('|')[0];
                                drInventoryItem["ItemType"] = drItem["ItemIdType"].ToString().Split('|')[1];
                                drInventoryItem["AssetLocationId"] = (int)AssetLocation.Store; //Stock In
                                drInventoryItem["CustomerId"] = "";
                                drInventoryItem["SaleChallanId"] = "";
                                drInventoryItem["EmployeeId"] = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                                drInventoryItem["StoreId"] = Convert.ToInt32(ddlStore.SelectedValue);
                                dtInventory.Rows.Add(drInventoryItem);
                                dtInventory.AcceptChanges();
                            }
                        }

                        inventory.InventoryDetails = dtInventory;
                        int inventoryResponse = objInventory.Inventory_Save(inventory);

                        if (inventoryResponse > 0)
                        {
                            GlobalCache.RemoveAll();
                            ClearMasterControls();
                            ClearItemControls();
                            LoadItemList();
                            Message.IsSuccess = true;
                            Message.Text = "Purchase Order saved";
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
                    Message.Text = "Purchase Order not saved";
                }
                Message.Show = true;
            }
        }
    }
}