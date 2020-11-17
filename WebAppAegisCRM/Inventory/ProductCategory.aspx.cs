using Business.Common;

using System;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Inventory
{
    public partial class ProductCategory : System.Web.UI.Page
    {
        
        public int ProductCategoryId
        {
            get { return Convert.ToInt32(ViewState["ProductCategoryId"]); }
            set { ViewState["ProductCategoryId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ClearControls();
                    LoadProductCategory();
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

        protected void ClearControls()
        {
            ProductCategoryId = 0;
            txtProductCategoryName.Text = "";
            Message.Show = false;
        }

        protected void LoadProductCategory()
        {
            Business.Inventory.ProductCategory objProductCategory = new Business.Inventory.ProductCategory();
            gvProductCategory.DataSource = objProductCategory.GetAll();
            gvProductCategory.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Business.Inventory.ProductCategory objProductCategory = new Business.Inventory.ProductCategory();
                Entity.Inventory.ProductCategory productCategory = new Entity.Inventory.ProductCategory();

                productCategory.ProductCategoryId = ProductCategoryId;
                productCategory.ProductCategoryName = txtProductCategoryName.Text;

                int i = objProductCategory.Save(productCategory);

                if (i > 0)
                {
                    ClearControls();
                    LoadProductCategory();
                    Message.IsSuccess = true;
                    Message.Text = "Model Category saved successfully...";
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Can not save!!!";
                }
                Message.Show = true;
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControls();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }

        protected void gvProductCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Business.Inventory.ProductCategory objProductCategory = new Business.Inventory.ProductCategory();
                Entity.Inventory.ProductCategory productCategory = new Entity.Inventory.ProductCategory();

                if (e.CommandName == "Ed")
                {
                    int productCategoryId = int.Parse(e.CommandArgument.ToString());
                    productCategory = objProductCategory.GetById(productCategoryId);
                    ProductCategoryId = productCategory.ProductCategoryId;
                    txtProductCategoryName.Text = productCategory.ProductCategoryName;
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