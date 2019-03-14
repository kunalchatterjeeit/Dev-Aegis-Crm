using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Service
{
    public partial class EmployeeDocket : System.Web.UI.Page
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
                Message.Show = false;
                txtDocketDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                LoadCustomerforSearch();
                LoadTime();
            }
        }

        #region User Defined Funtions
        protected void LoadTime()
        {
            ddlTimeHH.Items.Clear();
            ddlTimeMM.Items.Clear();
            ddlTimeTT.SelectedIndex = 0;

            ddlTimeHH.Items.Insert(0, "HH");
            ddlTimeMM.Items.Insert(0, "MM");

            for (int i = 0; i <= 11; i++)
            {
                ListItem li = new ListItem(i.ToString("00"), i.ToString());
                ddlTimeHH.Items.Insert(i + 1, li);
            }

            for (int i = 0; i <= 59; i++)
            {
                ListItem li = new ListItem(i.ToString("00"), i.ToString());
                ddlTimeMM.Items.Insert(i + 1, li);
            }
        }
        protected void LoadCustomerforSearch()
        {
            Business.Customer.Customer objCustomer = new Business.Customer.Customer();
            Entity.Customer.Customer customer = new Entity.Customer.Customer();
            customer.CustomerCode = txtCustomerCode.Text.Trim();
            customer.CustomerName = txtName.Text.Trim();
            customer.MobileNo = txtContactNo.Text.Trim();
            customer.EmailId = txtEmail.Text.Trim();
            if (HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
                customer.AssignEngineer = 0;
            else
                customer.AssignEngineer = int.Parse(HttpContext.Current.User.Identity.Name);
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
            LoadCustomerforSearch();
        }

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

            if (ddlTimeHH.SelectedIndex == 0)
            {
                Message.IsSuccess = false;
                Message.Text = "Please select In Time HH";
                Message.Show = true;
                return false;
            }
            else if (ddlTimeMM.SelectedIndex == 0)
            {
                Message.IsSuccess = false;
                Message.Text = "Please select In Time MM";
                Message.Show = true;
                return false;
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
                    if (!objContract.Service_MachineIsInContractCheck(CustomerPurchaseId))
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
            DataTable dt = objDocket.Service_Docket_GetLast10();

            if (dt != null)
            {
                gvDocket.DataSource = dt;
                gvDocket.DataBind();
            }

        }
        #endregion

        protected void btnCustomerSearch_Click(object sender, EventArgs e)
        {
            LoadCustomerforSearch();
        }

        protected void chkCustomer_checkchanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow gv = (GridViewRow)chk.NamingContainer;
            foreach (GridViewRow gvr in gvCustomerMaster.Rows)
            {
                if (gvr != gv)
                {
                    ((CheckBox)gvr.FindControl("chkCustomer")).Checked = false;
                }
                else
                {
                    CustomerMasterId = int.Parse(gvCustomerMaster.DataKeys[gvr.RowIndex].Values[0].ToString());
                    LoadCustomerPurchaseList();
                    LoadDocket();
                }
            }
        }

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

                docket.CustomerId = CustomerMasterId;
                docket.DocketNo = "";
                docket.DocketDateTime = Convert.ToDateTime(txtDocketDate.Text.Trim() + " " + ddlTimeHH.SelectedValue + ":" + ddlTimeMM.SelectedValue + ":00" + " " + ddlTimeTT.SelectedValue);
                docket.isCustomerEntry = false;
                docket.CallStatusId = 1;
                docket.Problem = txtProblem.Text.Trim();
                docket.DocketType = ddlDocketType.SelectedValue;
                docket.CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name);

                if (docket.CustomerPurchaseId == 0)
                {
                    Message.IsSuccess = false;
                    Message.Text = "Sorry! we can not receive your docket. Please refresh this page and try again..";
                    return;
                }

                DataTable dt = objDocket.Service_Docket_Save(docket);

                if (dt != null && dt.Rows.Count > 0)
                {
                    //updating last meter reading in Customer Purchase
                    //Business.Service.ServiceBook objServiceBook = new Business.Service.ServiceBook();
                    //int i = objServiceBook.Service_MeterReading_Update(CustomerPurchaseId, int.Parse(txtCurrentMeterReading.Text.Trim()));

                    //if (i > 0)
                    //{
                    LoadDocket();
                    LoadCustomerPurchaseList();
                    txtProblem.Text = "";
                    txtDocketDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                    Message.IsSuccess = true;
                    Message.Text = "Docket received. Your Docket No : " + dt.Rows[0]["DocketNo"].ToString();
                    //}
                    //else
                    //{
                    //    Message.IsSuccess = false;
                    //    Message.Text = "Current meter reading unable to update! Please contact system administrator immediately.";
                    //}
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