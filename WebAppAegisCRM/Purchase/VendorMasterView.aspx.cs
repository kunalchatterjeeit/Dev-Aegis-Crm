using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Common;

namespace WebAppAegisCRM.Purchase
{
    public partial class VendorMasterView : System.Web.UI.Page
    {
        Business.Purchase.Vendor objVendorMaster = new Business.Purchase.Vendor();
        Entity.Purchase.Vendor vendormaster = new Entity.Purchase.Vendor();
        public int VendorId
        {
            get { return Convert.ToInt32(ViewState["VendorId"]); }
            set { ViewState["VendorId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadVendor();
               // Message.Show = false;
            }
        }
        protected void LoadVendor()
        {
            vendormaster.CompanyId = 1;
            gvVendorMaster.DataSource = objVendorMaster.GetAll(vendormaster);
            gvVendorMaster.DataBind();
        }
        protected void gvVendorMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvVendorMaster.PageIndex = e.NewPageIndex;
            LoadVendor();
        }

        protected void gvVendorMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {

        }

        protected void gvVendorMaster_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                int vendorid = int.Parse(gvVendorMaster.DataKeys[e.RowIndex].Values[0].ToString());
                int i = objVendorMaster.Delete(vendorid);
                if (i > 0)
                {
                    LoadVendor();
                   // Message.IsSuccess = true;
                   // Message.Text = "Data deleted";
                }
                else
                {
                    //Message.IsSuccess = false;
                    //Message.Text = "Data can not delete.";
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
            }

            //Message.Show = true;
        }

        protected void gvVendorMaster_RowEditing(object sender, GridViewEditEventArgs e)
        {
            int vendorMasterId = int.Parse(gvVendorMaster.DataKeys[e.NewEditIndex].Values[0].ToString());
            Response.Redirect("VendorMaster.aspx?vendorid=" + vendorMasterId);
        }
    }
}