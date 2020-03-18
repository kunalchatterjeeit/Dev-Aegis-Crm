using Business.Common;
using log4net;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace WebAppERPNew.Inventory
{
    public partial class Product : System.Web.UI.Page
    {
        ILog logger = log4net.LogManager.GetLogger("ErrorLog");
        private int ProductMasterId
        {
            get { return Convert.ToInt32(ViewState["ProductMasterId"]); }
            set { ViewState["ProductMasterId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Message.Show = false;

                    LoadProductMaster();
                    LoadBrand();
                    LoadProductCategory();
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }

        #region User Defined Functions
        private void LoadBrand()
        {
            Business.Inventory.BrandMaster objBrandMaster = new Business.Inventory.BrandMaster();
            ddlBrand.DataSource = objBrandMaster.GetAll();
            ddlBrand.DataTextField = "BrandName";
            ddlBrand.DataValueField = "BrandId";
            ddlBrand.DataBind();
            ddlBrand.InsertSelect();

        }
        private void PopulateProduct()
        {
            Business.Inventory.ProductMaster objProductMaster = new Business.Inventory.ProductMaster();
            Entity.Inventory.ProductMaster productMaster = new Entity.Inventory.ProductMaster();

            productMaster = objProductMaster.GetById(ProductMasterId);
            ProductMasterId = productMaster.ProductMasterId;
            ddlBrand.SelectedValue = Convert.ToString(productMaster.BrandId);
            txtProductCode.Text = productMaster.ProductCode;
            txtProductName.Text = productMaster.ProductName;
            txtProductSpecification.Text = productMaster.ProductSpecification;
            txtMachineLife.Text = Convert.ToString(productMaster.MachineLife);
            txtMCBF.Text = (productMaster.MCBF == 0) ? "" : Convert.ToString(productMaster.MCBF);
            txtMTBF.Text = (productMaster.MTBF == 0) ? "" : Convert.ToString(productMaster.MTBF);
            ddlProductCategory.SelectedValue = Convert.ToString(productMaster.ProductCategoryId);
        }
        private void LoadProductMaster()
        {
            Business.Inventory.ProductMaster objProductMaster = new Business.Inventory.ProductMaster();
            Entity.Inventory.ProductMaster productMaster = new Entity.Inventory.ProductMaster();
            productMaster.CompanyMasterId = 1;
            gvProductMaster.DataSource = objProductMaster.GetAll(productMaster);
            gvProductMaster.DataBind();
        }
        private void Save()
        {
            Business.Inventory.ProductMaster objProductMaster = new Business.Inventory.ProductMaster();
            Entity.Inventory.ProductMaster productMaster = new Entity.Inventory.ProductMaster();

            productMaster.BrandId = int.Parse(ddlBrand.SelectedValue);
            productMaster.ProductMasterId = ProductMasterId;
            productMaster.ParentProductMasterId = 0;
            productMaster.ProductCode = txtProductCode.Text;
            productMaster.ProductName = txtProductName.Text;
            productMaster.ProductSpecification = txtProductSpecification.Text;
            productMaster.MachineLife = int.Parse(txtMachineLife.Text);
            productMaster.MCBF = (txtMCBF.Text == string.Empty) ? 0 : int.Parse(txtMCBF.Text);
            productMaster.MTBF = (txtMTBF.Text == string.Empty) ? 0 : int.Parse(txtMTBF.Text);
            productMaster.ProductCategoryId = int.Parse(ddlProductCategory.SelectedValue);

            productMaster.UserId = 1;
            productMaster.CompanyMasterId = 1;

            int i = objProductMaster.Save(productMaster);
            if (i > 0)
            {
                ClearControl();
                LoadProductMaster();
                Message.IsSuccess = true;
                Message.Text = "Product Saved Successfully...";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Sorry, can not save product!";
            }
            Message.Show = true;

        }
        private void ClearControl()
        {
            txtProductCode.Text = string.Empty;
            txtProductName.Text = string.Empty;
            txtProductSpecification.Text = string.Empty;
            txtMachineLife.Text = "";
            txtMCBF.Text = "";
            txtMTBF.Text = "";
            ddlProductCategory.SelectedIndex = 0;
        }
        private void LoadProductCategory()
        {
            Business.Inventory.ProductCategory objProductCategory = new Business.Inventory.ProductCategory();

            DataTable dt = objProductCategory.GetAll();
            if (dt.Rows.Count > 0)
            {
                ddlProductCategory.DataSource = dt;
                ddlProductCategory.DataTextField = "ProductCategoryName";
                ddlProductCategory.DataValueField = "ProductCategoryId";
                ddlProductCategory.DataBind();
            }
            ddlProductCategory.InsertSelect();
        }
        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("Product.aspx");
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
        protected void gvProductMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Ed")
                {
                    ProductMasterId = int.Parse(e.CommandArgument.ToString());
                    PopulateProduct();
                }
                else if (e.CommandName == "Del")
                {
                    Business.Inventory.ProductMaster objProductMaster = new Business.Inventory.ProductMaster();

                    int i = objProductMaster.Delete(int.Parse(e.CommandArgument.ToString()));
                    if (i > 0)
                    {
                        ClearControl();
                        LoadProductMaster();
                        Message.IsSuccess = true;
                        Message.Text = "Product Deleted Successfully...";
                    }
                    else
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Sorry, can not delete product!";
                    }
                    Message.Show = true;
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
    }
}