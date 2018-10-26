using Business.Common;
using Entity.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Purchase;
using System.Data.SqlClient;


namespace WebAppAegisCRM
{
    public partial class CustomerDashboard : System.Web.UI.Page
    {
        public int CustomerMasterId
        {
            get { return Convert.ToInt32(ViewState["CustomerMasterId"]); }
            set { ViewState["CustomerMasterId"] = value; }
        }
        public int purchase
        {
            get { return Convert.ToInt32(ViewState["CustomerMasterId"]); }
            set { ViewState["CustomerMasterId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("CustomerLogout.aspx");
            }
            CustomerMasterId = int.Parse(HttpContext.Current.User.Identity.Name.Split('|')[(int)Constants.Customer.ID]);
            if (!IsPostBack)
            {
                LoadCustomerPurchaseList();
            }

            LoadPieChart();
        }

        #region User Defined Funtions
        protected void LoadPieChart()
        {
            decimal upTime = 100;
            Business.Customer.Customer objCustomerMaster = new Business.Customer.Customer();
            DataTable dt = objCustomerMaster.CustomerPurchase_GetByCustomerId(CustomerMasterId);
            if (dt.Rows.Count > 0)
            {
                upTime = Convert.ToDecimal(dt.AsEnumerable().Average(row => row.Field<decimal>("UpTime")));
            }

            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "PieData(" + upTime.ToString() + "," + (100 - upTime).ToString() + ")", true);
        }
        protected void LoadCustomerPurchaseList()
        {
            Business.Customer.Customer objCustomerMaster = new Business.Customer.Customer();
            Entity.Customer.Customer customerMaster = new Entity.Customer.Customer();
            gvMachineList.DataSource = objCustomerMaster.CustomerPurchase_GetByCustomerId(CustomerMasterId);
            gvMachineList.DataBind();
        }
        #endregion

        protected void gvMachineList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvMachineList.PageIndex = e.NewPageIndex;
            LoadCustomerPurchaseList();
        }
    }
}