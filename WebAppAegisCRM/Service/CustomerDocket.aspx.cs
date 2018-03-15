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
    public partial class CustomerDocket : System.Web.UI.Page
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

        public int ContractTypeId
        {
            get { return Convert.ToInt32(ViewState["ContractTypeId"]); }
            set { ViewState["ContractTypeId"] = value; }
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
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    Response.Redirect("../CustomerLogout.aspx");
                }

                Message.Show = false;
                CustomerMasterId = int.Parse(HttpContext.Current.User.Identity.Name.Split('|')[(int)Constants.Customer.ID]);
                LoadCustomerPurchaseList();
                LoadDocket();
            }
        }

        #region User Defined Funtions
        protected void LoadCustomerPurchaseList()
        {
            Business.Customer.Customer objCustomerMaster = new Business.Customer.Customer();
            Entity.Customer.Customer customerMaster = new Entity.Customer.Customer();
            gvPurchase.DataSource = objCustomerMaster.CustomerPurchase_GetByCustomerId(CustomerMasterId);
            gvPurchase.DataBind();
        }
        protected bool ComplainValidation()
        {
            bool flag = false;

            foreach (GridViewRow gvr in gvPurchase.Rows)
            {
                if (((CheckBox)gvr.FindControl("chk")).Checked)
                {
                    flag = true;
                }
            }

            foreach (GridViewRow gvr in gvPurchase.Rows)
            {
                if (((CheckBox)gvr.FindControl("chk")).Checked)
                {
                    CustomerPurchaseId = int.Parse(gvPurchase.DataKeys[gvr.RowIndex].Values[0].ToString());
                    Business.Service.ServiceBook objServiceBook = new Business.Service.ServiceBook();

                    /* Checking whether any one docket exists or not*/
                    if (bool.Parse(((objServiceBook.Service_CheckIfAnyOpenDocket(CustomerPurchaseId).Rows[0]["Flag"].ToString()) == "1") ? "True" : "False"))
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Already a Docket is in queue. You can not docket more than one.";
                        Message.Show = true;
                        return false;
                    }


                    /* Checking whether machine is in contract or not*/
                    Business.Service.Contract objContract = new Business.Service.Contract();
                    if (objContract.Service_MachineIsInContractCheck(CustomerPurchaseId))
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Out of Contract! Please call Customer Help Desk.";
                        Message.Show = true;
                        return false;
                    }
                }
            }

            if (txtProblem.Text.Trim() != string.Empty)// && txtCurrentMeterReading.Text.Trim() != string.Empty)
                flag = true;
            else
                flag = false;

            if (flag == false)
            {
                Message.IsSuccess = false;
                Message.Text = "Please enter/select all mendatory fields...";
                Message.Show = true;
            }
            return flag;
        }
        protected void LoadDocket()
        {
            Business.Service.Docket objDocket = new Business.Service.Docket();
            Entity.Service.Docket docket = new Entity.Service.Docket();

            docket.CustomerId = int.Parse(HttpContext.Current.User.Identity.Name.Split('|')[(int)Constants.Customer.ID]);
            DataTable dt = objDocket.Service_Docket_GetAll(docket);

            if (dt != null && dt.Rows.Count > 0)
                gvDocket.DataSource = dt.AsEnumerable().Take(5).CopyToDataTable();
            else
                gvDocket.DataSource = null;

            gvDocket.DataBind();
        }
        #endregion

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ComplainValidation())
            {
                Business.Service.Docket objDocket = new Business.Service.Docket();
                Entity.Service.Docket docket = new Entity.Service.Docket();

                foreach (GridViewRow gvr in gvPurchase.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chk")).Checked)
                    {
                        docket.CustomerPurchaseId = int.Parse(gvPurchase.DataKeys[gvr.RowIndex].Values[0].ToString());
                    }
                }

                docket.CustomerId = int.Parse(HttpContext.Current.User.Identity.Name.Split('|')[(int)Constants.Customer.ID]);
                docket.DocketNo = "";
                docket.DocketDateTime = System.DateTime.Now;
                docket.isCustomerEntry = true;
                docket.CallStatusId = 1;
                docket.DocketType = "CM";
                docket.Problem = txtProblem.Text.Trim();
                docket.CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name.Split('|')[(int)Constants.Customer.ID]);

                if (docket.CustomerPurchaseId == 0)
                {
                    Message.IsSuccess = false;
                    Message.Text = "Sorry! we can not receive your docket. Please refresh this page and try again..";
                    return;
                }

                DataTable dt = objDocket.Service_Docket_Save(docket);

                if (dt != null && dt.Rows.Count > 0)
                {
                    LoadDocket();
                    LoadCustomerPurchaseList();
                    txtProblem.Text = "";
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

        protected void chk_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow gv = (GridViewRow)chk.NamingContainer;
            foreach (GridViewRow gvr in gvPurchase.Rows)
            {
                if (gvr != gv)
                {
                    ((CheckBox)gvr.FindControl("chk")).Checked = false;
                }
            }

            //if (chk.Checked)
            //    ContractTypeId = int.Parse(gvPurchase.DataKeys[gv.RowIndex].Values[1].ToString());
            //else
            ContractTypeId = 0;
        }

        protected void gvPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (((DataTable)(gvPurchase.DataSource)).Rows[e.Row.RowIndex]["IsDocketOpen"].ToString() == "1")
                {
                    ((CheckBox)e.Row.FindControl("chk")).Enabled = false;
                    e.Row.Attributes["style"] = "background-color: #ff3f3f";
                }
            }
        }
    }
}