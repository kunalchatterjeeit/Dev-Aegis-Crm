using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
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
            if (!IsPostBack)
            {
                ClearControls();
                LoadBrand();
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

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void gvBrand_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvBrand.PageIndex = e.NewPageIndex;
            LoadBrand();
        }

        protected void gvBrand_RowCommand(object sender, GridViewCommandEventArgs e)
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
    }
}