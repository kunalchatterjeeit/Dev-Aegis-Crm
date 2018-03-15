using Business.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM
{
    public partial class CustomerMain : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                Response.Redirect("../CustomerLogout.aspx");

            if (HttpContext.Current.User.Identity.IsAuthenticated)
                lblCustomerName.Text = "Welcome " + HttpContext.Current.User.Identity.Name.Split('|')[(int)Constants.Customer.Name];
        }
    }
}