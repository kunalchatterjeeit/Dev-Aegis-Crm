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
        // private Entity.Purchase.Purchase purchase;
      

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
                LoadMachineList();
                
            }
          
               LoadPieChart();
        }

        protected void LoadMachineList()
        {
             Business.Service.ServiceBook objServiceBook = new Business.Service.ServiceBook();
             Entity.Service.ServiceBook servicebook = new Entity.Service.ServiceBook();

             
             servicebook.MachineId = "";


             DataTable dt = objServiceBook.Service_ServiceBookDetailsApproval_GetAll(servicebook);

             using (DataView dv = new DataView(dt))
             {

                 gvMachineList.DataSource = dv.ToTable();
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