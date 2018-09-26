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

namespace WebAppAegisCRM
{
    public partial class CustomerDashboard : System.Web.UI.Page
    {
        private Entity.Purchase.Purchase purchase;

        public int CustomerMasterId
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
                MachineList();
            }
                LoadPieChart();
        }

        protected void MachineList()
        {
            Business.Purchase.Purchase objPurchase = new Business.Purchase.Purchase();
            Entity.Purchase.Purchase Purchase = new Entity.Purchase.Purchase();

            DataTable ds = objPurchase.Purchase_GetAll(purchase);
            {
                gvMachineList.DataSource = ds;
                gvMachineList.DataBind();
            }

            


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
        #endregion
    }
}