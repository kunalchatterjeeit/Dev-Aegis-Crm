using Business.Common;

using System;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Inventory
{
    public partial class Brand : System.Web.UI.Page
    {
        
        public int BrandId
        {
            get { return Convert.ToInt32(ViewState["BrandId"]); }
            set { ViewState["BrandId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ClearControls();
                    LoadBrand();
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
            BrandId = 0;
            txtBrandName.Text = "";
            Message.Show = false;
        }

        protected void LoadBrand()
        {
            Business.Inventory.BrandMaster objBrandMaster = new Business.Inventory.BrandMaster();
            gvBrand.DataSource = objBrandMaster.GetAll();
            gvBrand.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Business.Inventory.BrandMaster objBrandMaster = new Business.Inventory.BrandMaster();
                Entity.Inventory.BrandMaster brandMaster = new Entity.Inventory.BrandMaster();

                brandMaster.BrandId = BrandId;
                brandMaster.BrandName = txtBrandName.Text;

                int i = objBrandMaster.Save(brandMaster);

                if (i > 0)
                {
                    ClearControls();
                    LoadBrand();
                    Message.IsSuccess = true;
                    Message.Text = "Brand saved successfully...";
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

        protected void gvBrand_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvBrand.PageIndex = e.NewPageIndex;
                LoadBrand();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }

        protected void gvBrand_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Business.Inventory.BrandMaster objBrandMaster = new Business.Inventory.BrandMaster();
                Entity.Inventory.BrandMaster brandMaster = new Entity.Inventory.BrandMaster();

                if (e.CommandName == "Ed")
                {
                    int brandId = int.Parse(e.CommandArgument.ToString());
                    brandMaster = objBrandMaster.GetById(brandId);
                    BrandId = brandMaster.BrandId;
                    txtBrandName.Text = brandMaster.BrandName;
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