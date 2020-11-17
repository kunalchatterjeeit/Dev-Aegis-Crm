using Business.Common;
using Entity.Inventory;
using Entity.Service;

using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Purchase
{
    public partial class PurchaseRequisition : System.Web.UI.Page
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

        private bool DeleteItem(string autoId)
        {
            bool retValue = false;
            int lastCount = 0;
            if (_ItemsList.Rows.Count > 0)
            {
                lastCount = _ItemsList.Rows.Count;
                _ItemsList.Rows[_ItemsList.Rows.IndexOf(_ItemsList.Select("AutoId='" + autoId + "'").FirstOrDefault())].Delete();
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

            if (retValue && string.IsNullOrEmpty(txtPurchaseRequisitionDate.Text.Trim()))
            {
                retValue = false;
                Message.Text = "Please enter purchase requisition date.";
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
            txtPurchaseDepartment.Text = string.Empty;
            ddlVendor.SelectedIndex = 0;
            txtPurchaseRequisitionDate.Text = DateTime.Now.ToString("dd MMM yyyy");
            txtPurchaseRequisitionNo.Text = string.Empty;
            txtPurposeOfRequisition.Text = string.Empty;
            txtWhenNeeded.Text = DateTime.Now.AddMonths(3).ToString("dd MMM yyyy");
            _ItemsList = null;
        }

        private void ClearItemControls()
        {
            ddlItem.SelectedIndex = 0;
            txtQuantity.Text = string.Empty;
            txtDescription.Text = string.Empty;
            //_ItemsList = null;
            ddlItem.Focus();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    LoadVendor();
                    LoadAllItem();
                    LoadItemList();
                    ClearMasterControls();
                    ClearItemControls();
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (_ItemsList.Rows.Count == 0)
                {
                    using (DataTable dtInstance = new DataTable())
                    {
                        DataColumn column = new DataColumn("AutoId");
                        column.AutoIncrement = true;
                        column.ReadOnly = true;
                        column.Unique = false;

                        dtInstance.Columns.Add(column);
                        dtInstance.Columns.Add("ItemIdType");
                        dtInstance.Columns.Add("ItemName");
                        dtInstance.Columns.Add("ItemType");
                        dtInstance.Columns.Add("Quantity");
                        dtInstance.Columns.Add("Description");
                        _ItemsList = dtInstance;
                    }
                }

                DataRow drItem = _ItemsList.NewRow();
                drItem["ItemIdType"] = ddlItem.SelectedValue;
                drItem["ItemName"] = ddlItem.SelectedItem.Text;
                drItem["ItemType"] = (ddlItem.SelectedValue.Split('|')[1] == ((int)ItemType.Product).ToString()) ? ItemType.Product.ToString() : ItemType.Spare.ToString();
                drItem["Quantity"] = txtQuantity.Text.Trim();
                drItem["Description"] = txtDescription.Text.Trim();

                _ItemsList.Rows.Add(drItem);
                _ItemsList.AcceptChanges();

                LoadItemList();
                ClearItemControls();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }

        protected void gvItem_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "D")
                {
                    string autoId = e.CommandArgument.ToString();

                    if (DeleteItem(autoId))
                    {
                        LoadItemList();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "mmsg", "alert('Data can not be deleted!!!....');", true);
                    }
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (Validation())
                {
                    Entity.Purchase.PurchaseRequisition purchase = new Entity.Purchase.PurchaseRequisition();
                    Business.Purchase.PurchaseRequisition objPurchase = new Business.Purchase.PurchaseRequisition();
                    Entity.Purchase.PurchaseDetails purchaseDetails = new Entity.Purchase.PurchaseDetails();

                    purchase.PurchaseRequisitionNo = (!string.IsNullOrEmpty(txtPurchaseRequisitionNo.Text.Trim())) ? txtPurchaseRequisitionNo.Text.Trim() : string.Empty;
                    purchase.RequisitionDate = Convert.ToDateTime(txtPurchaseRequisitionDate.Text.Trim());
                    purchase.PurchaseDepartment = txtPurchaseDepartment.Text.Trim();
                    purchase.VendorId = Convert.ToInt32(ddlVendor.SelectedValue);
                    purchase.WhenNeeded = Convert.ToDateTime(txtPurchaseRequisitionDate.Text.Trim());
                    purchase.PurposeOfRequisition = txtPurposeOfRequisition.Text.Trim();
                    purchase.CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    Int64 purchaseRequisitionId = objPurchase.PurchaseRequisition_Save(purchase);

                    foreach (DataRow drItem in _ItemsList.Rows)
                    {
                        purchase.PurchaseRequisitionDetailsCollection.Add(
                        new Entity.Purchase.PurchaseRequisitionDetails()
                        {
                            PurchaseRequisitionId = purchaseRequisitionId,
                            ItemId = Convert.ToInt32(drItem["ItemIdType"].ToString().Split('|')[0]),
                            ItemType = Convert.ToInt32(drItem["ItemIdType"].ToString().Split('|')[1]),
                            Quantity = (drItem["Quantity"] != null && !string.IsNullOrEmpty(drItem["Quantity"].ToString())) ? Convert.ToDecimal(drItem["Quantity"].ToString()) : 0,
                            UOM = 1,
                            Description = (drItem["Description"] != null && !string.IsNullOrEmpty(drItem["Description"].ToString())) ? drItem["Description"].ToString() : string.Empty,
                            ApprovalStatus = (int)ApprovalStatus.None
                        });
                    }
                    int purchaseDetailsResponse = objPurchase.Purchase_RequisitionDetails_Save(purchase);

                    if (purchaseDetailsResponse > 0)
                    {
                        ClearMasterControls();
                        ClearItemControls();
                        LoadItemList();
                        Message.IsSuccess = true;
                        Message.Text = "Purchase requisition saved";
                    }
                    else
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Purchase requisition not saved";
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
    }
}