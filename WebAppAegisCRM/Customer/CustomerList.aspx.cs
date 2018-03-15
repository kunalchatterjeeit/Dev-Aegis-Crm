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

            DataTable dt = objCustomer.Customer_Customer_GetByAssignEngineerId(customer);
            if (dt.Rows.Count > 0)
                gvCustomerMaster.DataSource = dt;
            else
                gvCustomerMaster.DataSource = null;
            gvCustomerMaster.DataBind();
        }
    }
}