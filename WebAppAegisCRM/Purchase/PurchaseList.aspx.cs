using Business.Common;
using Entity.Inventory;

using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Purchase
{
    public partial class PurchaseList : System.Web.UI.Page
    {
        
        private void Purchase_GetAll()
        {
            Business.Purchase.Purchase objPurchase = new Business.Purchase.Purchase();
            Entity.Purchase.Purchase purchase = new Entity.Purchase.Purchase();
            purchase.PurchaseOrderNo = txtPurchaseOrderNo.Text.Trim();
            purchase.VendorId = Convert.ToInt32(ddlVendor.SelectedValue);
            purchase.InvoiceNo = txtInvoiceNo.Text.Trim();
            purchase.InvoiceFromDate = (string.IsNullOrEmpty(txtInvoiceFromDate.Text.Trim())) ? DateTime.MinValue : Convert.ToDateTime(txtInvoiceFromDate.Text.Trim());
            purchase.InvoiceToDate = (string.IsNullOrEmpty(txtInvoiceToDate.Text.Trim())) ? DateTime.MinValue : Convert.ToDateTime(txtInvoiceToDate.Text.Trim());
            purchase.PurchaseFromDate = (string.IsNullOrEmpty(txtPurchaseFromDate.Text.Trim())) ? DateTime.MinValue : Convert.ToDateTime(txtPurchaseFromDate.Text.Trim());
            purchase.PurchaseToDate = (string.IsNullOrEmpty(txtPurchaseToDate.Text.Trim())) ? DateTime.MinValue : Convert.ToDateTime(txtPurchaseToDate.Text.Trim());
            if (HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
                purchase.CreatedBy = 0;
            else
                purchase.CreatedBy = int.Parse(HttpContext.Current.User.Identity.Name);
            if (ddlItem.SelectedIndex > 0)
            {
                purchase.ItemId = Convert.ToInt32(ddlItem.SelectedValue.Split('|')[0]);
                purchase.itemType = (ddlItem.SelectedValue.Split('|')[1] == ((int)ItemType.Product).ToString()) ? ItemType.Product : ItemType.Spare;
            }
            gvPurchase.DataSource = objPurchase.Purchase_GetAll(purchase);
            gvPurchase.DataBind();
        }
        private void LoadVendor()
        {
            Business.Purchase.Vendor objVendorMaster = new Business.Purchase.Vendor();
            ddlVendor.DataSource = objVendorMaster.GetAll(new Entity.Purchase.Vendor() { CompanyId = 1 });
            ddlVendor.DataTextField = "VendorMasterName";
            ddlVendor.DataValueField = "VendorMasterId";
            ddlVendor.DataBind();
            ddlVendor.InsertSelect();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtInvoiceFromDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                    txtInvoiceToDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                    txtPurchaseFromDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                    txtPurchaseToDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                    LoadVendor();
                    LoadAllItem();
                    Purchase_GetAll();
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Purchase_GetAll();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
            }
        }

        protected void gvPurchase_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvPurchase.PageIndex = e.NewPageIndex;
                Purchase_GetAll();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
            }
        }

        protected void gvPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Business.Purchase.Purchase objPurchase = new Business.Purchase.Purchase();
                Entity.Purchase.Purchase purchase = new Entity.Purchase.Purchase();

                if (e.CommandName == "PurchaseDetails")
                {
                    DataTable dt = objPurchase.PurchaseDetails_GetByPurchaseId(int.Parse(e.CommandArgument.ToString()));
                    gvPurchaseDetails.DataSource = dt;
                    gvPurchaseDetails.DataBind();
                    ModalPopupExtender1.Show();
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
            }
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
    }
}