using Business.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Sale
{
    public partial class SaleChallanList : System.Web.UI.Page
    {
        private void Sale_GetAll()
        {
            Business.Sale.SaleChallan objSaleChallan = new Business.Sale.SaleChallan();
            Entity.Sale.SaleChallan saleChallan = new Entity.Sale.SaleChallan();
            saleChallan.CustomerName = txtCustomerName.Text.Trim();
            saleChallan.ChallanNo = txtChallanNo.Text.Trim();
            saleChallan.OrderNo = txtOrderNo.Text.Trim();
            saleChallan.OrderFromDate = (string.IsNullOrEmpty(txtSaleFromDate.Text.Trim())) ? DateTime.MinValue : Convert.ToDateTime(txtSaleFromDate.Text.Trim());
            saleChallan.OrderToDate = (string.IsNullOrEmpty(txtSaleToDate.Text.Trim())) ? DateTime.MinValue : Convert.ToDateTime(txtSaleToDate.Text.Trim());
            saleChallan.CallanTypeId = Convert.ToInt32(ddlChallanType.SelectedValue);
            if (HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
                saleChallan.CreatedBy = 0;
            else
                saleChallan.CreatedBy = int.Parse(HttpContext.Current.User.Identity.Name);
            gvSale.DataSource = objSaleChallan.Sale_Challan_GetAll(saleChallan);
            gvSale.DataBind();
        }

        private void Sale_ChallanType_GetAll()
        {
            ddlChallanType.DataSource = new Business.Sale.ChallanType().Sale_ChallanType_GetAll();
            ddlChallanType.DataTextField = "TypeName";
            ddlChallanType.DataValueField = "ChallanTypeId";
            ddlChallanType.DataBind();
            ddlChallanType.InsertSelect();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Message.Show = false;
                Sale_ChallanType_GetAll();
                txtSaleFromDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                txtSaleToDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                Sale_GetAll();
            }
        }

        protected void gvSale_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSale.PageIndex = e.NewPageIndex;
            Sale_GetAll();
        }

        protected void gvSale_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Business.Sale.SaleChallan objSaleChallan = new Business.Sale.SaleChallan();
                Entity.Sale.SaleChallan saleChallan = new Entity.Sale.SaleChallan();

                if (e.CommandName == "SaleDetails")
                {
                    DataTable dt = objSaleChallan.SaleChallanDetails_GetBySaleChallanId(long.Parse(e.CommandArgument.ToString()));
                    gvSaleDetails.DataSource = dt;
                    gvSaleDetails.DataBind();
                    ModalPopupExtender1.Show();
                }
                else if (e.CommandName == "D")
                {
                    int response = objSaleChallan.Sale_Challan_Delete(int.Parse(e.CommandArgument.ToString()));
                    if (response > 0)
                    {
                        Sale_GetAll();
                        Message.IsSuccess = true;
                        Message.Text = "Deleted Successfully";
                    }
                    else
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Data Dependency Exists, please contact system admin.";
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Sale_GetAll();
        }
    }
}