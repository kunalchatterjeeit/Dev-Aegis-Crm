using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Common;
using System.Data;

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
            purchase.InvoiceFromDate = (string.IsNullOrEmpty(txtInvoiceFromDate.Text.Trim()))? DateTime.MinValue: Convert.ToDateTime(txtInvoiceFromDate.Text.Trim());
            purchase.InvoiceToDate = (string.IsNullOrEmpty(txtInvoiceToDate.Text.Trim())) ? DateTime.MinValue : Convert.ToDateTime(txtInvoiceToDate.Text.Trim());
            purchase.PurchaseFromDate = (string.IsNullOrEmpty(txtPurchaseFromDate.Text.Trim())) ? DateTime.MinValue : Convert.ToDateTime(txtPurchaseFromDate.Text.Trim());
            purchase.PurchaseToDate = (string.IsNullOrEmpty(txtPurchaseToDate.Text.Trim())) ? DateTime.MinValue : Convert.ToDateTime(txtPurchaseToDate.Text.Trim());
            if (HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
                purchase.CreatedBy = 0;
            else
                purchase.CreatedBy = int.Parse(HttpContext.Current.User.Identity.Name);
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
            if (!IsPostBack)
            {
                txtInvoiceFromDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                txtInvoiceToDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                txtPurchaseFromDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                txtPurchaseToDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                LoadVendor();
                Purchase_GetAll();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Purchase_GetAll();
        }

        protected void gvPurchase_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPurchase.PageIndex = e.NewPageIndex;
            Purchase_GetAll();
        }

        protected void gvPurchase_RowCommand(object sender, GridViewCommandEventArgs e)
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
    }
}