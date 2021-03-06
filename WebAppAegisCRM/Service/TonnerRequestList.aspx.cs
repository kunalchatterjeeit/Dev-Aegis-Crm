﻿using Business.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Service
{
    public partial class TonnerRequestList : System.Web.UI.Page
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

        public Int64 TonnerRequestId
        {
            get { return Convert.ToInt64(ViewState["TonnerRequestId"]); }
            set { ViewState["TonnerRequestId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCallStatus();
                LoadProduct();
                LoadTonnerRequest();
            }
        }

        #region User Defined Funtions
        protected void LoadCallStatus()
        {
            Business.Service.CallStatus objCallStatus = new Business.Service.CallStatus();

            DataTable dt = objCallStatus.GetAll(1);
            if (dt != null)
            {
                ddlCallStatus.DataSource = dt;
                ddlCallStatus.DataTextField = "CallStatus";
                ddlCallStatus.DataValueField = "CallStatusId";
                ddlCallStatus.DataBind();
            }
            ddlCallStatus.InsertSelect();
        }
        //protected void LoadCustomer()
        //{
        //    Business.Customer.Customer objCustomer = new Business.Customer.Customer();
        //    Entity.Customer.Customer customer = new Entity.Customer.Customer();
        //    if (HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
        //        customer.AssignEngineer = 0;
        //    else
        //        customer.AssignEngineer = int.Parse(HttpContext.Current.User.Identity.Name);
        //    DataTable dt = objCustomer.Customer_Customer_GetByAssignEngineerId(customer);
        //    ddlCustomer.DataSource = dt;
        //    ddlCustomer.DataTextField = "CustomerName";
        //    ddlCustomer.DataValueField = "CustomerMasterId";
        //    ddlCustomer.DataBind();
        //    ddlCustomer.InsertSelect();
        //}
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
        protected void LoadTonnerRequest()
        {
            Business.Service.TonerRequest objTonerRequest = new Business.Service.TonerRequest();
            Entity.Service.TonerRequest tonerRequest = new Entity.Service.TonerRequest();
           
            tonerRequest.PageIndex = gvTonnerRequest.PageIndex;
            tonerRequest.PageSize = gvTonnerRequest.PageSize;
            tonerRequest.RequestNo = txtTonnerRequestNo.Text.Trim();
            tonerRequest.CustomerName = txtCustomerName.Text.Trim();
            tonerRequest.ProductId = int.Parse(ddlProduct.SelectedValue);
            tonerRequest.RequestFromDateTime = (txtFromTonnerRequestDate.Text == "") ? DateTime.MinValue : Convert.ToDateTime(txtFromTonnerRequestDate.Text);
            tonerRequest.RequestToDateTime = (txtToTonnerRequestDate.Text == "") ? DateTime.MinValue : Convert.ToDateTime(txtToTonnerRequestDate.Text);
            tonerRequest.CallStatusId = int.Parse(ddlCallStatus.SelectedValue);
            if (HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
                tonerRequest.AssignEngineer = 0;
            else
                tonerRequest.AssignEngineer = int.Parse(HttpContext.Current.User.Identity.Name);

            DataSet response = objTonerRequest.Service_TonerRequest_GetAll(tonerRequest);

            if (response != null)
            {
                gvTonnerRequest.DataSource = response.Tables[0];
                gvTonnerRequest.VirtualItemCount = (response.Tables[1].Rows.Count > 0) ? Convert.ToInt32(response.Tables[1].Rows[0]["TotalCount"].ToString()) : 20;
                gvTonnerRequest.DataBind();
            }

        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadTonnerRequest();
        }

        protected void gvTonnerRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTonnerRequest.PageIndex = e.NewPageIndex;
            LoadTonnerRequest();
        }
    }
}