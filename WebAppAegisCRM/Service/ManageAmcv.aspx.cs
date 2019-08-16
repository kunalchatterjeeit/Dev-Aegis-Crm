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
    public partial class ManageAmcv : System.Web.UI.Page
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
        public long AmcvId
        {
            get { return Convert.ToInt64(ViewState["AmcvId"]); }
            set { ViewState["AmcvId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Message.Show = false;
                LoadAmcv();
            }
        }

        #region User Defined Funtions
        protected void LoadAmcv()
        {
            Business.Service.ServiceBook objServiceBook = new Business.Service.ServiceBook();
            string customerName = txtCustomerName.Text.Trim();
            string machineId = txtMachineId.Text.Trim();
            DataTable dt = objServiceBook.Service_AMCV_NotProcessed_GetAll(customerName, machineId);

            if (dt != null)
            {
                gvAmcv.DataSource = dt;
                gvAmcv.DataBind();
            }
        }
        #endregion

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            LoadAmcv();
        }

        protected void gvAmcv_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAmcv.PageIndex = e.NewPageIndex;
            LoadAmcv();
        }

        protected void gvAmcv_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "P")
                {
                    GridViewRow gvr = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
                    CustomerMasterId = Convert.ToInt32(gvAmcv.DataKeys[gvr.RowIndex].Values[1].ToString());
                    CustomerPurchaseId = Convert.ToInt32(gvAmcv.DataKeys[gvr.RowIndex].Values[2].ToString());
                    AmcvId = Convert.ToInt64(e.CommandArgument.ToString());

                    CreateDocket();
                    Business.Service.ServiceBook objServiceBook = new Business.Service.ServiceBook();
                    objServiceBook.Service_AmcvProcess_Save(AmcvId);
                    LoadAmcv();
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                Message.IsSuccess = false;
                Message.Text = "Sorry! we can not receive your docket. Please refresh this page and try again..";
                Message.Show = true;
            }
        }

        private void CreateDocket()
        {
            Business.Service.Docket objDocket = new Business.Service.Docket();
            Entity.Service.Docket docket = new Entity.Service.Docket()
            {
                CustomerPurchaseId = CustomerPurchaseId,
                CustomerId = CustomerMasterId,
                DocketNo = "",
                DocketDateTime = DateTime.Now,
                isCustomerEntry = false,
                CallStatusId = 1,
                Problem = string.Format("PM CALL GENERATED FOR AMCV {0}", AmcvId),
                DocketType = "PM",
                CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name),
            };

            if (docket.CustomerPurchaseId == 0)
            {
                Message.IsSuccess = false;
                Message.Text = "Sorry! we can not receive your docket. Please refresh this page and try again..";
                Message.Show = true;
                return;
            }

            DataTable dt = objDocket.Service_Docket_Save(docket);

            if (dt != null && dt.Rows.Count > 0)
            {
                Message.IsSuccess = true;
                Message.Text = "Docket received. Your Docket No : " + dt.Rows[0]["DocketNo"].ToString();

            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Sorry! we can not receive your docket. Please refresh this page and try again.";
            }
            Message.Show = true;
        }
    }
}