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
    public partial class CustomerTonnerRequest : System.Web.UI.Page
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

        public Int64 TonnerRequestId
        {
            get { return Convert.ToInt64(ViewState["TonnerRequestId"]); }
            set { ViewState["TonnerRequestId"] = value; }
        }

        public Int64 SpareId
        {
            get { return Convert.ToInt64(ViewState["SpareId"]); }
            set { ViewState["SpareId"] = value; }
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
                LoadTonnerRequest();
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

            if (SpareId == 0)
            {
                Message.IsSuccess = false;
                Message.Text = "Please select Toner";
                Message.Show = true;
                return flag;
            }

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

                    /* Checking whether any one open tonner request exists or not*/
                    if (bool.Parse(((objServiceBook.Service_CheckIfAnyOpenTonnerRequest(CustomerPurchaseId).Rows[0]["Flag"].ToString()) == "1") ? "True" : "False"))
                    {
                        Message.IsSuccess = false;
                        Message.Text = "A Toner Request is under approval. You can not request more than one.";
                        Message.Show = true;
                        return false;
                    }

                    /* Checking whether machine is in contract or not*/
                    //Business.Service.Contract objContract = new Business.Service.Contract();
                    //DataTable dt = new DataTable();
                    //dt = objContract.Service_MachineIsInContractCheck(CustomerPurchaseId);
                    //if (dt != null)
                    //{
                    //    if (dt.Rows.Count > 0)
                    //    {
                    //        if (!bool.Parse((dt.Rows[0]["Flag"].ToString() == "1") ? "True" : "False"))
                    //        {
                    //            Message.IsSuccess = false;
                    //            Message.Text = "Out of Contract! Please call Customer Help Desk.";
                    //            Message.Show = true;
                    //            return false;
                    //        }
                    //    }
                    //}
                }
            }

            if (txtA3BWMeterReading.Text.Trim() == string.Empty)
                flag = false;
            if (txtA3CLMeterReading.Text.Trim() == string.Empty)
                flag = false;
            if (txtA4BWMeterReading.Text.Trim() == string.Empty)
                flag = false;
            if (txtA4CLMeterReading.Text.Trim() == string.Empty)
                flag = false;

            if (flag == false)
            {
                Message.IsSuccess = false;
                Message.Text = "Please enter/select all mendatory fields...";
                Message.Show = true;
            }
            return flag;
        }
        protected void LoadTonnerRequest()
        {
            Business.Service.TonerRequest objTonnerRequest = new Business.Service.TonerRequest();
            Entity.Service.TonerRequest tonnerRequest = new Entity.Service.TonerRequest();

            tonnerRequest.CustomerId = int.Parse(HttpContext.Current.User.Identity.Name.Split('|')[(int)Constants.Customer.ID]);
            DataTable dt = objTonnerRequest.Service_TonerRequest_GetAll(tonnerRequest).Tables[0];

            if (dt != null && dt.Rows.Count > 0)
                gvTonnerRequest.DataSource = dt.AsEnumerable().Take(5).CopyToDataTable();
            else
                gvTonnerRequest.DataSource = null;

            gvTonnerRequest.DataBind();

        }
        #endregion

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ComplainValidation())
            {
                Business.Service.TonerRequest objTonnerRequest = new Business.Service.TonerRequest();
                Entity.Service.TonerRequest tonnerRequest = new Entity.Service.TonerRequest();

                foreach (GridViewRow gvr in gvPurchase.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chk")).Checked)
                    {
                        tonnerRequest.CustomerPurchaseId = int.Parse(gvPurchase.DataKeys[gvr.RowIndex].Values[0].ToString());
                    }
                }

                tonnerRequest.CustomerId = int.Parse(HttpContext.Current.User.Identity.Name);
                tonnerRequest.RequestNo = "";
                tonnerRequest.RequestDateTime = System.DateTime.Now;
                tonnerRequest.isCustomerEntry = true;
                tonnerRequest.CallStatusId = 8;
                tonnerRequest.A3BWMeterReading = int.Parse(txtA3BWMeterReading.Text.Trim());
                tonnerRequest.A4BWMeterReading = int.Parse(txtA4BWMeterReading.Text.Trim());
                tonnerRequest.A3CLMeterReading = int.Parse(txtA3CLMeterReading.Text.Trim());
                tonnerRequest.A4CLMeterReading = int.Parse(txtA4CLMeterReading.Text.Trim());

                tonnerRequest.Remarks = txtRequest.Text.Trim();
                tonnerRequest.CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                tonnerRequest.SpareIds.Add(SpareId);

                DataTable dt = objTonnerRequest.Service_TonerRequest_Save(tonnerRequest);

                if (dt.Rows.Count > 0)
                {
                    LoadTonnerRequest();
                    LoadCustomerPurchaseList();
                    txtA3BWMeterReading.Text = "";
                    txtA3CLMeterReading.Text = "";
                    txtA4BWMeterReading.Text = "";
                    txtA4CLMeterReading.Text = "";
                    txtRequest.Text = "";
                    Message.IsSuccess = true;
                    Message.Text = "Toner request received. Your Request No : " + dt.Rows[0]["TonnerRequestNo"].ToString() + ". " + dt.Rows[0]["Msg"].ToString();
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Sorry! we can not receive your request. Please try again.";
                }
                Message.Show = true;
            }
        }

        protected void chk1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk1 = (CheckBox)sender;
            GridViewRow gv = (GridViewRow)chk1.NamingContainer;
            foreach (GridViewRow gvr in gvTonner.Rows)
            {
                if (gvr != gv)
                {
                    ((CheckBox)gvr.FindControl("chk1")).Checked = false;
                }
            }
            if (chk1.Checked)
            {
                SpareId = Int64.Parse(gvTonner.DataKeys[gv.RowIndex].Values[0].ToString());
            }
            else
                SpareId = 0;
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

            if (chk.Checked)
            {
                //ContractTypeId = int.Parse(gvPurchase.DataKeys[gv.RowIndex].Values[1].ToString());
                ContractTypeId = 0;

                Business.Service.TonerRequest ObjBl = new Business.Service.TonerRequest();
                DataTable dt = ObjBl.Service_Toner_GetAllByCustomerId(Int64.Parse(gvPurchase.DataKeys[gv.RowIndex].Values[0].ToString()));
                if (dt != null)
                {
                    gvTonner.DataSource = dt;
                    gvTonner.DataBind();
                }
            }
            else
            {
                ContractTypeId = 0;
                gvTonner.DataBind();
            }
        }

        protected void gvPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (((DataTable)(gvPurchase.DataSource)).Rows[e.Row.RowIndex]["IsTonerOpen"].ToString() == "1")
                {
                    ((CheckBox)e.Row.FindControl("chk")).Enabled = false;
                    e.Row.Attributes["style"] = "background-color: #ff3f3f";
                }
            }
        }
    }
}