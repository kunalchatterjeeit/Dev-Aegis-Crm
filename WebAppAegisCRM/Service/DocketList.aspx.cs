using Business.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Service
{
    public partial class DocketList : System.Web.UI.Page
    {
        public int CustomerMasterId
        {
            get { return Convert.ToInt32(ViewState["CustomerMasterId"]); }
            set { ViewState["CustomerMasterId"] = value; }
        }
        public int CustomerPurchaseId
        {
            get { return Convert.ToInt32(ViewState["CustomerPurchaseId"]); }
            set { ViewState["CustomerPurchaseId"] = value; }
        }

        public Int64 DocketId
        {
            get { return Convert.ToInt64(ViewState["DocketId"]); }
            set { ViewState["DocketId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCallStatus();
                LoadCustomer();
                LoadProduct();
                LoadDocket();
            }
        }

        #region User Defined Funtions
        protected void LoadCallStatus()
        {
            Business.Service.CallStatus objCallStatus = new Business.Service.CallStatus();

            DataTable dt = objCallStatus.GetAll(2);
            if (dt != null)
            {
                ddlCallStatus.DataSource = dt;
                ddlCallStatus.DataTextField = "CallStatus";
                ddlCallStatus.DataValueField = "CallStatusId";
                ddlCallStatus.DataBind();
            }
            ddlCallStatus.InsertSelect();
        }
        protected void LoadCustomer()
        {
            Business.Customer.Customer objCustomer = new Business.Customer.Customer();
            Entity.Customer.Customer customer = new Entity.Customer.Customer();
            if (HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
                customer.AssignEngineer = 0;
            else
                customer.AssignEngineer = int.Parse(HttpContext.Current.User.Identity.Name);
            DataTable dt = objCustomer.Customer_Customer_GetByAssignEngineerId(customer);
            ddlCustomer.DataSource = dt;
            ddlCustomer.DataTextField = "CustomerName";
            ddlCustomer.DataValueField = "CustomerMasterId";
            ddlCustomer.DataBind();
            ddlCustomer.InsertSelect();
        }
        private void LoadProduct()
        {
            Business.Inventory.ProductMaster objProductMaster = new Business.Inventory.ProductMaster();
            Entity.Inventory.ProductMaster productmaster = new Entity.Inventory.ProductMaster();

            productmaster.CompanyMasterId = 1;
            ddlProduct.DataSource = objProductMaster.GetAll(productmaster);
            ddlProduct.DataTextField = "ProductName";
            ddlProduct.DataValueField = "ProductMasterId";
            ddlProduct.DataBind();
            ddlProduct.InsertSelect();
        }
        protected void LoadDocket()
        {
            Business.Service.Docket objDocket = new Business.Service.Docket();
            Entity.Service.Docket docket = new Entity.Service.Docket();

            docket.DocketNo = txtDocketNo.Text.Trim();
            docket.CustomerId = int.Parse(ddlCustomer.SelectedValue);
            docket.ProductId = int.Parse(ddlProduct.SelectedValue);
            docket.DocketFromDateTime = (txtFromDocketDate.Text == "") ? DateTime.MinValue : Convert.ToDateTime(txtFromDocketDate.Text);
            docket.DocketToDateTime = (txtToDocketDate.Text == "") ? DateTime.MinValue : Convert.ToDateTime(txtToDocketDate.Text);
            docket.CallStatusId = int.Parse(ddlCallStatus.SelectedValue);
            docket.DocketType = ddlDocketType.SelectedValue;
            if (HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
                docket.AssignEngineer = 0;
            else
                docket.AssignEngineer = int.Parse(HttpContext.Current.User.Identity.Name);

            DataTable dt = objDocket.Service_Docket_GetAll(docket);

            if (dt != null)
            {
                gvDocket.DataSource = dt;
                gvDocket.DataBind();
            }

        }
        #endregion

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            LoadDocket();
        }

        protected void gvDocket_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDocket.PageIndex = e.NewPageIndex;
            LoadDocket();
        }
    }
}