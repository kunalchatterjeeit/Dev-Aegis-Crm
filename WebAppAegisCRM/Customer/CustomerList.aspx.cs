using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Customer
{
    public partial class CustomerList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                Response.Redirect("~/MainLogout.aspx");

            if (!IsPostBack)
            {
                Customer_Customer_GetByAssignEngineerId();
            }
        }
        protected void Customer_Customer_GetByAssignEngineerId()
        {
            Business.Customer.Customer objCustomer = new Business.Customer.Customer();
            Entity.Customer.Customer customer = new Entity.Customer.Customer();
            if (HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
                customer.AssignEngineer = 0;
            else
                customer.AssignEngineer = int.Parse(HttpContext.Current.User.Identity.Name);
            customer.CustomerName = txtName.Text.Trim();
            customer.PageIndex = gvCustomerMaster.PageIndex;
            customer.PageSize = gvCustomerMaster.PageSize;

            DataSet ds = objCustomer.Customer_CustomerMaster_GetByAssignEngineerIdWithPaging(customer);
            if (ds.Tables.Count > 0)
            {
                gvCustomerMaster.DataSource = ds.Tables[0];
                gvCustomerMaster.VirtualItemCount = (ds.Tables[1].Rows.Count > 0) ? Convert.ToInt32(ds.Tables[1].Rows[0]["TotalCount"].ToString()) : 10;
                gvCustomerMaster.DataBind();
            }
            else
            {
                gvCustomerMaster.DataSource = null;
                gvCustomerMaster.DataBind();
            }
        }

        protected void gvCustomerMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCustomerMaster.PageIndex = e.NewPageIndex;
            Customer_Customer_GetByAssignEngineerId();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Customer_Customer_GetByAssignEngineerId();
        }
    }
}