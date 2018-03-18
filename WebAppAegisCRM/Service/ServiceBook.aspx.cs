using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;
using Entity.Inventory;
using Entity.Service;
using Business.Common;

namespace WebAppAegisCRM.Service
{
    public partial class ServiceBook : System.Web.UI.Page
    {
        public Int64 ServiceBookId
        {
            get { return Convert.ToInt64(ViewState["ServiceBookId"]); }
            set { ViewState["ServiceBookId"] = value; }
        }
        public Int64 TonerRequestId
        {
            get { return Convert.ToInt64(ViewState["TonerRequestId"]); }
            set { ViewState["TonerRequestId"] = value; }
        }
        public Int64 DocketId
        {
            get { return Convert.ToInt64(ViewState["DocketId"]); }
            set { ViewState["DocketId"] = value; }
        }
        public int CustomerPurchaseId
        {
            get { return Convert.ToInt32(ViewState["CustomerPurchaseId"]); }
            set { ViewState["CustomerPurchaseId"] = value; }
        }
        public string DocketNo
        {
            get { return Convert.ToString(ViewState["DocketNo"]); }
            set { ViewState["DocketNo"] = value; }
        }
        public string CustomerName
        {
            get { return Convert.ToString(ViewState["CustomerName"]); }
            set { ViewState["CustomerName"] = value; }
        }
        public string ModelName
        {
            get { return Convert.ToString(ViewState["ModelName"]); }
            set { ViewState["ModelName"] = value; }
        }
        public string MachineId
        {
            get { return Convert.ToString(ViewState["MachineId"]); }
            set { ViewState["MachineId"] = value; }
        }
        public bool IsSendMail
        {
            get { return Convert.ToBoolean(ViewState["IsSendMail"]); }
            set { ViewState["IsSendMail"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    Response.Redirect("~/MainLogout.aspx");
                }

                btnCallTransfer.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.CALL_TRANSFER);
                LoadTime(ddlInTimeHH, ddlInTimeMM, ddlOutTimeHH, ddlOutTimeMM);
                LoadDocketCallStatus();
                LoadProblemObserved();
                LoadDiagnosis();
                LoadActionTaken();
                LoadCurrentCallStatusDocket();
                LoadTonnerRequestCallStatus();
                LoadCurrentTonnerRequestCallStatus();
                LoadProduct();
                LoadCustomer();
                EmployeeMaster_GetAll();
                EmployeeMaster_GetAll_ForToner();

                MessageTonner.Show = false;
                MessageDocket.Show = false;
                //Checking Auto Fetch functionality conditions
                if (Request.QueryString["callid"] != null && Request.QueryString["callid"].ToString().Length > 0)
                    if (Request.QueryString["calltype"] != null && Request.QueryString["calltype"].ToString().Length > 0)
                    {
                        ddlCallType.SelectedValue = Request.QueryString["calltype"].ToString();

                        if (Request.QueryString["calltype"].ToString() == Convert.ToString((int)CallType.Toner))
                        {
                            TonerRequestId = Convert.ToInt64(Request.QueryString["callid"].ToString().DecryptQueryString());
                            btnSearch_Click(sender, e);
                            foreach (GridViewRow gvr in gvTonnerRequest.Rows)
                            {
                                if (gvTonnerRequest.DataKeys[gvr.RowIndex].Values[0].ToString() == Request.QueryString["callid"].ToString().DecryptQueryString())
                                {
                                    ((CheckBox)gvr.FindControl("chkTonnerRequest")).Checked = true;
                                    chkTonnerRequest_CheckedChanged(((CheckBox)gvr.FindControl("chkTonnerRequest")), e);
                                }
                            }
                            TonerRequestId = 0;
                        }
                        else if (Request.QueryString["calltype"].ToString() == Convert.ToString((int)CallType.Docket))
                        {
                            DocketId = Convert.ToInt64(Request.QueryString["callid"].ToString().DecryptQueryString());
                            btnSearch_Click(sender, e);
                            foreach (GridViewRow gvr in gvDocket.Rows)
                            {
                                if (gvDocket.DataKeys[gvr.RowIndex].Values[0].ToString() == Request.QueryString["callid"].ToString().DecryptQueryString())
                                {
                                    ((CheckBox)gvr.FindControl("chkDocket")).Checked = true;
                                    chkDocket_CheckedChanged(((CheckBox)gvr.FindControl("chkDocket")), e);
                                }
                            }
                            DocketId = 0;
                        }
                    }
            }
        }

        #region USER DEFINED FUNCTIONS
        private bool ValidateDocket()
        {
            MessageDocket.Show = false;
            bool retValue = false;

            if (Convert.ToDateTime(txtInDate.Text.Trim()).Date > System.DateTime.Now.Date)
            {
                MessageDocket.IsSuccess = false;
                MessageDocket.Text = "Invalid In date.";
                MessageDocket.Show = true;
                return retValue = false;
            }

            if (Convert.ToDateTime(txtOutDate.Text.Trim()).Date > System.DateTime.Now.Date)
            {
                MessageDocket.IsSuccess = false;
                MessageDocket.Text = "Invalid Out date.";
                MessageDocket.Show = true;
                return retValue = false;
            }

            foreach (GridViewRow gvr in gvAssociatedEngineers.Rows)
            {
                CheckBox chkEngineer = (CheckBox)gvr.FindControl("chkEngineer");
                if (chkEngineer.Checked)
                {
                    TextBox txtAssociatedInDate = (TextBox)gvr.FindControl("txtAssociatedInDate");
                    DropDownList ddlAssociatedInTimeHH = (DropDownList)gvr.FindControl("ddlAssociatedInTimeHH");
                    if (ddlAssociatedInTimeHH.SelectedIndex == 0)
                    {
                        MessageDocket.IsSuccess = false;
                        MessageDocket.Text = "Please select Associated In Time HH";
                        MessageDocket.Show = true;
                        return retValue = false;
                    }
                    DropDownList ddlAssociatedInTimeMM = (DropDownList)gvr.FindControl("ddlAssociatedInTimeMM");
                    if (ddlAssociatedInTimeMM.SelectedIndex == 0)
                    {
                        MessageDocket.IsSuccess = false;
                        MessageDocket.Text = "Please select Associated In Time MM";
                        MessageDocket.Show = true;
                        return retValue = false;
                    }
                    DropDownList ddlAssociatedInTimeTT = (DropDownList)gvr.FindControl("ddlAssociatedInTimeTT");
                    TextBox txtAssociatedOutDate = (TextBox)gvr.FindControl("txtAssociatedOutDate");
                    DropDownList ddlAssociatedOutTimeHH = (DropDownList)gvr.FindControl("ddlAssociatedOutTimeHH");
                    if (ddlAssociatedOutTimeHH.SelectedIndex == 0)
                    {
                        MessageDocket.IsSuccess = false;
                        MessageDocket.Text = "Please select Associated Out Time HH";
                        MessageDocket.Show = true;
                        return retValue = false;
                    }
                    DropDownList ddlAssociatedOutTimeMM = (DropDownList)gvr.FindControl("ddlAssociatedOutTimeMM");
                    if (ddlAssociatedOutTimeMM.SelectedIndex == 0)
                    {
                        MessageDocket.IsSuccess = false;
                        MessageDocket.Text = "Please select Associated Out Time MM";
                        MessageDocket.Show = true;
                        return retValue = false;
                    }
                    DropDownList ddlAssociatedOutTimeTT = (DropDownList)gvr.FindControl("ddlAssociatedOutTimeTT");

                    DateTime associatedInDateTime = Convert.ToDateTime(txtAssociatedInDate.Text + " " + ddlAssociatedInTimeHH.SelectedValue + ":" + ddlAssociatedInTimeMM.SelectedValue + ":00" + " " + ddlAssociatedInTimeTT.SelectedValue);
                    DateTime associatedOutDateTime = Convert.ToDateTime(txtAssociatedOutDate.Text + " " + ddlAssociatedOutTimeHH.SelectedValue + ":" + ddlAssociatedOutTimeMM.SelectedValue + ":00" + " " + ddlAssociatedOutTimeTT.SelectedValue);

                    if (associatedInDateTime > System.DateTime.Now)
                    {
                        MessageDocket.IsSuccess = false;
                        MessageDocket.Text = "Invalid In date of selected associate engineer.";
                        MessageDocket.Show = true;
                        return retValue = false;
                    }

                    if (associatedOutDateTime > System.DateTime.Now)
                    {
                        MessageDocket.IsSuccess = false;
                        MessageDocket.Text = "Invalid Out date of selected associate engineer.";
                        MessageDocket.Show = true;
                        return retValue = false;
                    }

                    if (associatedInDateTime > associatedOutDateTime)
                    {
                        MessageDocket.IsSuccess = false;
                        MessageDocket.Text = "In date should be smaller than Out date of selected associate engineer.";
                        MessageDocket.Show = true;
                        return retValue = false;
                    }
                }
            }

            foreach (GridViewRow gvr in gvDocket.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkDocket")).Checked == true)
                {
                    retValue = true;
                    break;
                }
            }

            if (retValue)
            {
                if (ddlInTimeHH.SelectedIndex == 0)
                {
                    MessageDocket.IsSuccess = false;
                    MessageDocket.Text = "Please select In Time HH";
                    MessageDocket.Show = true;
                    retValue = false;
                }
                else
                    if (ddlInTimeMM.SelectedIndex == 0)
                {
                    MessageDocket.IsSuccess = false;
                    MessageDocket.Text = "Please select In Time MM";
                    MessageDocket.Show = true;
                    retValue = false;
                }
                else
                        if (ddlOutTimeHH.SelectedIndex == 0)
                {
                    MessageDocket.IsSuccess = false;
                    MessageDocket.Text = "Please select Out Time HH";
                    MessageDocket.Show = true;
                    retValue = false;
                }
                else
                            if (ddlOutTimeMM.SelectedIndex == 0)
                {
                    MessageDocket.IsSuccess = false;
                    MessageDocket.Text = "Please select Out Time MM";
                    MessageDocket.Show = true;
                    retValue = false;
                }
            }

            if (string.IsNullOrEmpty(Business.Common.Context.Signature))
            {
                MessageDocket.IsSuccess = false;
                MessageDocket.Text = "Please provide signature before submit.";
                MessageDocket.Show = true;
                return retValue = false;
            }
            return retValue;
        }
        private bool ValidateTonner()
        {
            MessageTonner.Show = true;
            bool retValue = false;

            if (ddlTonerServiceEngineer.SelectedIndex == 0)
            {
                MessageTonner.IsSuccess = false;
                MessageTonner.Text = "Please select toner delivered by.";
                MessageTonner.Show = true;
                return retValue = false;
            }

            foreach (GridViewRow gvr in gvTonnerRequest.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkTonnerRequest")).Checked == true)
                {
                    retValue = true;
                    break;
                }
            }
            if (retValue)
            {
                if (ddlCurrentTonnerRequestCallStatus.SelectedIndex == 0)
                {
                    MessageTonner.IsSuccess = false;
                    MessageTonner.Text = "Please select Current Call Status";
                    MessageTonner.Show = true;
                    retValue = false;
                }
            }

            bool isTonerSelected = false;
            if (gvTonnerList.Rows.Count > 0)
            {
                foreach (GridViewRow gvr in gvTonnerList.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkToner")).Checked
                    || gvTonnerList.DataKeys[gvr.RowIndex].Values[1].ToString().Equals(Convert.ToString((int)ApprovalStatus.Rejected)))
                    {
                        isTonerSelected = true;
                        break;
                    }
                }
            }
            if (!isTonerSelected)
            {
                MessageTonner.IsSuccess = false;
                MessageTonner.Text = "Please select toner.";
                MessageTonner.Show = true;
                return false;
            }

            return retValue;
        }
        protected void LoadTime(DropDownList ddlInHH, DropDownList ddlInMM, DropDownList ddlOutHH, DropDownList ddlOutMM)
        {
            ddlInHH.Items.Clear();
            ddlInMM.Items.Clear();
            ddlInTimeTT.SelectedIndex = 0;

            ddlOutHH.Items.Clear();
            ddlOutMM.Items.Clear();
            ddlOutTimeTT.SelectedIndex = 0;

            ddlInHH.Items.Insert(0, "HH");
            ddlOutHH.Items.Insert(0, "HH");
            ddlInMM.Items.Insert(0, "MM");
            ddlOutMM.Items.Insert(0, "MM");

            for (int i = 0; i <= 11; i++)
            {
                ListItem li = new ListItem(i.ToString("00"), i.ToString());
                ddlInHH.Items.Insert(i + 1, li);
                ddlOutHH.Items.Insert(i + 1, li);
            }

            for (int i = 0; i <= 59; i++)
            {
                ListItem li = new ListItem(i.ToString("00"), i.ToString());
                ddlInMM.Items.Insert(i + 1, li);
                ddlOutMM.Items.Insert(i + 1, li);
            }
        }
        protected void LoadDocketCallStatus()
        {
            Business.Service.CallStatus objCallStatus = new Business.Service.CallStatus();
            DataTable dt = objCallStatus.GetAll(2);

            if (dt != null)
            {
                ddlDocketCallStatus.DataSource = dt;
                ddlDocketCallStatus.DataTextField = "CallStatus";
                ddlDocketCallStatus.DataValueField = "CallStatusId";
                ddlDocketCallStatus.DataBind();
            }
            ListItem li = new ListItem("--SELECT--", "0");
            ddlDocketCallStatus.Items.Insert(0, li);
            ddlDocketCallStatus.SelectedIndex = 1;
        }
        protected void LoadProblemObserved()
        {
            Business.Service.ProblemMasters objProblemObserved = new Business.Service.ProblemMasters();
            DataTable dt = objProblemObserved.GetAll(1);

            if (dt != null)
            {
                ddlProblemObserved.DataSource = dt;
                ddlProblemObserved.DataTextField = "Name";
                ddlProblemObserved.DataValueField = "Name";
                ddlProblemObserved.DataBind();
            }
            ListItem li = new ListItem("--SELECT--", "");
            ddlProblemObserved.Items.Insert(0, li);
            ddlProblemObserved.SelectedIndex = 1;
        }
        protected void LoadDiagnosis()
        {
            Business.Service.ProblemMasters objProblemMasters = new Business.Service.ProblemMasters();
            DataTable dt = objProblemMasters.GetAll(2);

            if (dt != null)
            {
                ddlDocketDiagnosis.DataSource = dt;
                ddlDocketDiagnosis.DataTextField = "Name";
                ddlDocketDiagnosis.DataValueField = "Name";
                ddlDocketDiagnosis.DataBind();
            }
            ListItem li = new ListItem("--SELECT--", "");
            ddlDocketDiagnosis.Items.Insert(0, li);
            ddlDocketDiagnosis.SelectedIndex = 1;
        }
        protected void LoadActionTaken()
        {
            Business.Service.ProblemMasters objProblemMasters = new Business.Service.ProblemMasters();
            DataTable dt = objProblemMasters.GetAll(3);

            if (dt != null)
            {
                ddlDocketActionTaken.DataSource = dt;
                ddlDocketActionTaken.DataTextField = "Name";
                ddlDocketActionTaken.DataValueField = "Name";
                ddlDocketActionTaken.DataBind();
            }
            ListItem li = new ListItem("--SELECT--", "");
            ddlDocketActionTaken.Items.Insert(0, li);
            ddlDocketActionTaken.SelectedIndex = 1;
        }
        protected void LoadCurrentCallStatusDocket()
        {
            Business.Service.CallStatus objCallStatus = new Business.Service.CallStatus();
            DataTable dt = objCallStatus.GetAll(2);

            if (dt != null)
            {
                ddlCurrentCallStatusDocket.DataSource = dt;
                ddlCurrentCallStatusDocket.DataTextField = "CallStatus";
                ddlCurrentCallStatusDocket.DataValueField = "CallStatusId";
                ddlCurrentCallStatusDocket.DataBind();
            }
            ListItem li = new ListItem("--SELECT--", "0");
            ddlCurrentCallStatusDocket.Items.Insert(0, li);
        }
        protected void LoadTonnerRequestCallStatus()
        {
            Business.Service.CallStatus objCallStatus = new Business.Service.CallStatus();
            DataTable dt = objCallStatus.GetAll(1);

            if (dt != null)
            {
                ddlTonnerRequestCallStatus.DataSource = dt;
                ddlTonnerRequestCallStatus.DataTextField = "CallStatus";
                ddlTonnerRequestCallStatus.DataValueField = "CallStatusId";
                ddlTonnerRequestCallStatus.DataBind();
            }
            ListItem li = new ListItem("--SELECT--", "0");
            ddlTonnerRequestCallStatus.Items.Insert(0, li);
        }
        protected void LoadCurrentTonnerRequestCallStatus()
        {
            Business.Service.CallStatus objCallStatus = new Business.Service.CallStatus();
            DataTable dt = objCallStatus.GetAll(1);

            if (dt != null)
            {
                ddlCurrentTonnerRequestCallStatus.DataSource = dt;
                ddlCurrentTonnerRequestCallStatus.DataTextField = "CallStatus";
                ddlCurrentTonnerRequestCallStatus.DataValueField = "CallStatusId";
                ddlCurrentTonnerRequestCallStatus.DataBind();
            }
            ListItem li = new ListItem("--SELECT--", "0");
            ddlCurrentTonnerRequestCallStatus.Items.Insert(0, li);
        }
        protected void LoadProduct()
        {
            Business.Inventory.ProductMaster objProductMaster = new Business.Inventory.ProductMaster();
            Entity.Inventory.ProductMaster productmaster = new Entity.Inventory.ProductMaster();

            productmaster.CompanyMasterId = 1;
            DataTable dt = objProductMaster.GetAll(productmaster);

            if (dt != null)
            {
                ddlTonnerRequestProduct.DataSource = dt;
                ddlTonnerRequestProduct.DataTextField = "ProductName";
                ddlTonnerRequestProduct.DataValueField = "ProductMasterId";
                ddlTonnerRequestProduct.DataBind();
            }
            ListItem li = new ListItem("--SELECT--", "0");
            ddlTonnerRequestProduct.Items.Insert(0, li);
        }
        protected void LoadCustomer()
        {
            Business.Customer.Customer objCustomer = new Business.Customer.Customer();
            Entity.Customer.Customer customer = new Entity.Customer.Customer();
            if (HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
                customer.AssignEngineer = 0;
            else
                customer.AssignEngineer = int.Parse(HttpContext.Current.User.Identity.Name);
            DataTable dt = objCustomer.Customer_Customer_GetByAssignEngineerId(customer);
            ddlCustomer.DataSource = dt;
            ddlCustomer.DataTextField = "CustomerName";
            ddlCustomer.DataValueField = "CustomerMasterId";
            ddlCustomer.DataBind();
            ListItem li = new ListItem("--SELECT--", "0");
            ddlCustomer.Items.Insert(0, li);
        }
        protected void LoadDocket()
        {
            Business.Service.Docket objDocket = new Business.Service.Docket();
            Entity.Service.Docket docket = new Entity.Service.Docket();

            docket.DocketId = DocketId;
            docket.DocketNo = txtDocketNo.Text.Trim();
            docket.CustomerId = int.Parse(ddlCustomer.SelectedValue);
            docket.DocketFromDateTime = (txtFromDocketDate.Text == "") ? DateTime.MinValue : Convert.ToDateTime(txtFromDocketDate.Text);
            docket.DocketToDateTime = (txtToDocketDate.Text == "") ? DateTime.MinValue : Convert.ToDateTime(txtToDocketDate.Text);
            docket.CallStatusId = int.Parse(ddlDocketCallStatus.SelectedValue);
            docket.DocketType = ddlDocketType.SelectedValue;
            if (HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
                docket.AssignEngineer = 0;
            else
                docket.AssignEngineer = int.Parse(HttpContext.Current.User.Identity.Name);

            DataTable dt = objDocket.Service_Docket_GetAll(docket);

            if (dt != null)
            {
                gvDocket.DataSource = dt;
                gvDocket.DataBind();
            }
        }
        protected void LoadTonnerRequest(int pageIndex, int pageSize)
        {
            Business.Service.TonerRequest objTonerRequest = new Business.Service.TonerRequest();
            Entity.Service.TonerRequest tonerRequest = new Entity.Service.TonerRequest();

            tonerRequest.PageIndex = pageIndex;
            tonerRequest.PageSize = pageSize;
            tonerRequest.TonerRequestId = TonerRequestId;
            tonerRequest.RequestNo = txtTonnerRequestNo.Text.Trim();
            tonerRequest.CustomerId = int.Parse(ddlCustomer.SelectedValue);
            tonerRequest.ProductId = int.Parse(ddlTonnerRequestProduct.SelectedValue);
            tonerRequest.RequestFromDateTime = (txtFromTonnerRequestDate.Text == "") ? DateTime.MinValue : Convert.ToDateTime(txtFromTonnerRequestDate.Text);
            tonerRequest.RequestToDateTime = (txtToTonnerRequestDate.Text == "") ? DateTime.MinValue : Convert.ToDateTime(txtToTonnerRequestDate.Text);
            tonerRequest.CallStatusId = int.Parse(ddlTonnerRequestCallStatus.SelectedValue);
            if (HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
                tonerRequest.AssignEngineer = 0;
            else
                tonerRequest.AssignEngineer = int.Parse(HttpContext.Current.User.Identity.Name);

            DataSet response = objTonerRequest.Service_TonerRequest_GetAll(tonerRequest);

            if (response != null)
            {
                gvTonnerRequest.DataSource = response.Tables[0];
                gvTonnerRequest.VirtualItemCount = (response.Tables[1].Rows.Count > 0) ? Convert.ToInt32(response.Tables[1].Rows[0]["TotalCount"].ToString()) : 10;
                gvTonnerRequest.DataBind();
            }
        }
        protected void LoadService_Tonner_GetByTonnerRequestId()
        {
            Business.Service.ServiceBook objServiceBook = new Business.Service.ServiceBook();
            DataSet ds = objServiceBook.Service_Tonner_GetByTonnerRequestId(TonerRequestId);

            if (ds != null)
            {
                gvTonnerList.DataSource = ds.Tables[0];
                gvTonnerList.DataBind();
            }
            lblA3BWCurrentMeterReading.Text = (ds.Tables[0].Rows[0]["A3BWCurrentMeterReading"] == DBNull.Value) ? "0" : ds.Tables[0].Rows[0]["A3BWCurrentMeterReading"].ToString();
            lblA4BWCurrentMeterReading.Text = (ds.Tables[0].Rows[0]["A4BWCurrentMeterReading"] == DBNull.Value) ? "0" : ds.Tables[0].Rows[0]["A4BWCurrentMeterReading"].ToString();
            lblA3CLCurrentMeterReading.Text = (ds.Tables[0].Rows[0]["A3CLCurrentMeterReading"] == DBNull.Value) ? "0" : ds.Tables[0].Rows[0]["A3CLCurrentMeterReading"].ToString();
            lblA4CLCurrentMeterReading.Text = (ds.Tables[0].Rows[0]["A4CLCurrentMeterReading"] == DBNull.Value) ? "0" : ds.Tables[0].Rows[0]["A4CLCurrentMeterReading"].ToString();
            lblA3BWLastMeterReading.Text = (ds.Tables[1].Rows.Count == 0 || ds.Tables[1].Rows[0]["A3BWLastMeterReading"] == DBNull.Value) ? "0" : ds.Tables[1].Rows[0]["A3BWLastMeterReading"].ToString();
            lblA4BWLastMeterReading.Text = (ds.Tables[1].Rows.Count == 0 || ds.Tables[1].Rows[0]["A4BWLastMeterReading"] == DBNull.Value) ? "0" : ds.Tables[1].Rows[0]["A4BWLastMeterReading"].ToString();
            lblA3CLLastMeterReading.Text = (ds.Tables[1].Rows.Count == 0 || ds.Tables[1].Rows[0]["A3CLLastMeterReading"] == DBNull.Value) ? "0" : ds.Tables[1].Rows[0]["A3CLLastMeterReading"].ToString();
            lblA4CLLastMeterReading.Text = (ds.Tables[1].Rows.Count == 0 || ds.Tables[1].Rows[0]["A4CLLastMeterReading"] == DBNull.Value) ? "0" : ds.Tables[1].Rows[0]["A4CLLastMeterReading"].ToString();
        }
        protected void ClearDocketControls()
        {
            lblProblem.Text = "";
            txtInDate.Text = System.DateTime.Now.ToString("dd MMM yyyy");
            txtOutDate.Text = System.DateTime.Now.ToString("dd MMM yyyy");
            LoadTime(ddlInTimeHH, ddlInTimeMM, ddlOutTimeHH, ddlOutTimeMM);
            ddlCurrentCallStatusDocket.SelectedIndex = 0;
            ddlProblemObserved.SelectedIndex = 0;
            ddlDocketDiagnosis.SelectedIndex = 0;
            ddlDocketActionTaken.SelectedIndex = 0;
            txtCustomerFeedback.Text = "";
            txtRemarks.Text = "";
            ddlServiceEngineer.SelectedIndex = 0;
            gvAssociatedEngineers.DataBind();
            MessageDocket.Show = false;
            Business.Common.Context.SelectedAssets.Clear();
            Business.Common.Context.Signature = string.Empty;
            Business.Common.Context.CallId = 0;
            Business.Common.Context.CallType = CallType.None;
        }
        protected void ClearTonnerControls()
        {
            TonerRequestId = 0;
            lblA3BWCurrentMeterReading.Text = "0";
            lblA3BWLastMeterReading.Text = "0";
            lblA4BWCurrentMeterReading.Text = "0";
            lblA4BWLastMeterReading.Text = "0";
            lblA3CLCurrentMeterReading.Text = "0";
            lblA3CLLastMeterReading.Text = "0";
            lblA4CLCurrentMeterReading.Text = "0";
            lblA4CLLastMeterReading.Text = "0";
            ddlCurrentTonnerRequestCallStatus.SelectedIndex = 0;
            ddlTonerServiceEngineer.SelectedIndex = 0;
            txtActionTaken.Text = "";
            txtDiagnosis.Text = "";
            gvTonnerList.DataBind();
            MessageTonner.Show = false;
            Business.Common.Context.CallId = 0;
            Business.Common.Context.CallType = CallType.None;
        }
        protected void EmployeeMaster_GetAll()
        {
            Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
            Entity.HR.EmployeeMaster employeeMaster = new Entity.HR.EmployeeMaster();
            employeeMaster.CompanyId_FK = 1;
            DataTable dt = objEmployeeMaster.EmployeeMaster_GetAll(employeeMaster);
            if (dt != null)
            {
                ddlServiceEngineer.DataSource = dt;
                ddlServiceEngineer.DataValueField = "EmployeeMasterId";
                ddlServiceEngineer.DataTextField = "EmployeeName";
                ddlServiceEngineer.DataBind();
            }
            ListItem li = new ListItem("--SELECT--", "0");
            ddlServiceEngineer.Items.Insert(0, li);

            gvAssociatedEngineers.DataSource = dt;
            gvAssociatedEngineers.DataBind();
        }
        private void EmployeeMaster_GetAll_ForToner()
        {
            Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
            Entity.HR.EmployeeMaster employeeMaster = new Entity.HR.EmployeeMaster();
            employeeMaster.CompanyId_FK = 1;
            DataTable dt = objEmployeeMaster.EmployeeMaster_GetAll(employeeMaster);
            if (dt != null)
            {
                ddlTonerServiceEngineer.DataSource = dt;
                ddlTonerServiceEngineer.DataValueField = "EmployeeMasterId";
                ddlTonerServiceEngineer.DataTextField = "EmployeeName";
                ddlTonerServiceEngineer.DataBind();
            }
            ListItem li = new ListItem("--SELECT--", "0");
            ddlTonerServiceEngineer.Items.Insert(0, li);
        }
        protected void LoadServiceBookMasterHistory()
        {
            Business.Service.ServiceBook objServiceBook = new Business.Service.ServiceBook();
            DataTable dt = objServiceBook.ServiceBookMasterHistory_GetAllByCallId(DocketId, 2);
            if (dt != null)
            {
                gvDocketClosingHistory.DataSource = dt;
                gvDocketClosingHistory.DataBind();
            }
        }
        #endregion

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlCallType.SelectedValue == "1")
            {
                divTonnerRequest.Visible = true;
                divDocket.Visible = false;
                divDocketClosing.Visible = false;
                divTonnerRequestApproval.Visible = false;
                LoadTonnerRequest(1, gvTonnerRequest.PageSize);
            }
            else if (ddlCallType.SelectedValue == "2")
            {
                divTonnerRequest.Visible = false;
                divDocket.Visible = true;
                divDocketClosing.Visible = false;
                divTonnerRequestApproval.Visible = false;
                LoadDocket();
            }
            else
            {
                divTonnerRequest.Visible = false;
                divDocket.Visible = false;
                divDocketClosing.Visible = false;
                divTonnerRequestApproval.Visible = false;
                gvDocket.DataBind();
                gvTonnerRequest.DataBind();
            }
        }

        protected void btnDocketSearch_Click(object sender, EventArgs e)
        {
            LoadDocket();
        }

        protected void btnTonnerRequestSearch_Click(object sender, EventArgs e)
        {
            LoadTonnerRequest(0, gvTonnerRequest.PageSize);
        }

        protected void chkDocket_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ClearDocketControls();
                CheckBox checkBox = (CheckBox)sender;
                GridViewRow gridViewRow = (GridViewRow)checkBox.NamingContainer;
                foreach (GridViewRow gridViewRowItem in gvDocket.Rows)
                {
                    if (gridViewRowItem != gridViewRow)
                    {
                        ((CheckBox)gridViewRowItem.FindControl("chkDocket")).Checked = false;
                    }
                }
                DocketNo = gridViewRow.Cells[2].Text;
                CustomerName = gridViewRow.Cells[3].Text;
                MachineId = gridViewRow.Cells[6].Text;
                ModelName = gridViewRow.Cells[7].Text;

                if (checkBox.Checked)
                {
                    DocketId = Int64.Parse(gvDocket.DataKeys[gridViewRow.RowIndex].Values[0].ToString());
                    Business.Common.Context.CallId = DocketId;
                    Business.Common.Context.CallType = CallType.Docket;
                    lblProblem.Text = gridViewRow.Cells[8].Text;
                    divDocketClosing.Visible = true;
                    LoadServiceBookMasterHistory();
                    divDocketClosingHistory.Visible = true;
                }
                else
                {
                    divDocketClosing.Visible = false;
                    divDocketClosingHistory.Visible = false;
                }

                if (gvDocket.DataKeys[gridViewRow.RowIndex].Values != null && gvDocket.DataKeys[gridViewRow.RowIndex].Values.Count > 1)
                    Business.Common.Context.ProductId = Int64.Parse(gvDocket.DataKeys[gridViewRow.RowIndex].Values[1].ToString());

                if (gvDocket.DataKeys[gridViewRow.RowIndex].Values != null && gvDocket.DataKeys[gridViewRow.RowIndex].Values.Count > 3)
                    CustomerPurchaseId = int.Parse(gvDocket.DataKeys[gridViewRow.RowIndex].Values[3].ToString());

                //Set Assign Engineer
                if (gvDocket.DataKeys[gridViewRow.RowIndex].Values != null && gvDocket.DataKeys[gridViewRow.RowIndex].Values.Count > 2)
                    ddlServiceEngineer.SelectedValue = Convert.ToString(gvDocket.DataKeys[gridViewRow.RowIndex].Values[2].ToString());
            }
            catch (Exception ex)
            {
                ex.WriteException();
            }
        }

        protected void chkTonnerRequest_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                ClearTonnerControls();
                CheckBox checkBox = (CheckBox)sender;
                GridViewRow gridViewRow = (GridViewRow)checkBox.NamingContainer;
                foreach (GridViewRow gridViewRowItem in gvTonnerRequest.Rows)
                {
                    if (gridViewRowItem != gridViewRow)
                    {
                        ((CheckBox)gridViewRowItem.FindControl("chkTonnerRequest")).Checked = false;
                    }
                }

                if (checkBox.Checked)
                {
                    if (gvTonnerRequest.DataKeys[gridViewRow.RowIndex].Values != null && gvTonnerRequest.DataKeys[gridViewRow.RowIndex].Values.Count > 0)
                    {
                        TonerRequestId = Int64.Parse(gvTonnerRequest.DataKeys[gridViewRow.RowIndex].Values[0].ToString());
                        Business.Common.Context.CallId = TonerRequestId;
                        Business.Common.Context.CallType = CallType.Toner;
                    }
                    if (gvTonnerRequest.DataKeys[gridViewRow.RowIndex].Values != null && gvTonnerRequest.DataKeys[gridViewRow.RowIndex].Values.Count > 1)
                        CustomerPurchaseId = int.Parse(gvTonnerRequest.DataKeys[gridViewRow.RowIndex].Values[1].ToString());
                    LoadService_Tonner_GetByTonnerRequestId();
                    divTonnerRequestApproval.Visible = true;
                }
                else
                    divTonnerRequestApproval.Visible = false;
            }
            catch (Exception ex)
            {
                ex.WriteException();
            }
        }

        protected void btnSubmitTonner_Click(object sender, EventArgs e)
        {
            if (ValidateTonner())
            {
                Business.Service.ServiceBook objServiceBook = new Business.Service.ServiceBook();
                Entity.Service.ServiceBook serviceBook = new Entity.Service.ServiceBook();

                serviceBook.TonnerRequestId = TonerRequestId;
                serviceBook.CallStatusId = int.Parse(ddlCurrentTonnerRequestCallStatus.SelectedValue);
                serviceBook.Diagnosis = txtDiagnosis.Text;
                serviceBook.ActionTaken = txtActionTaken.Text;
                serviceBook.EmployeeId_FK = Convert.ToInt32(ddlTonerServiceEngineer.SelectedValue);

                using (serviceBook.ServiceBookDetails = new DataTable())
                {
                    serviceBook.ServiceBookDetails.Columns.Add("TonerId");

                    foreach (GridViewRow gvr in gvTonnerList.Rows)
                    {
                        CheckBox chkToner = (CheckBox)gvr.FindControl("chkToner");
                        if (chkToner.Checked)
                        {
                            DataRow dr = serviceBook.ServiceBookDetails.NewRow();
                            dr["TonerId"] = Int64.Parse(gvTonnerList.DataKeys[gvr.RowIndex].Values[0].ToString());
                            serviceBook.ServiceBookDetails.Rows.Add(dr);
                            serviceBook.ServiceBookDetails.AcceptChanges();
                        }
                    }
                }

                int i = objServiceBook.Service_TonerRequest_Approve(serviceBook);

                if (i > 0)
                {
                    //updating meter readings
                    serviceBook.CustomerPurchaseId = CustomerPurchaseId;
                    if (lblA3BWCurrentMeterReading.Text.Trim() == string.Empty)
                        serviceBook.A3BWMeterReading = null;
                    else
                        serviceBook.A3BWMeterReading = int.Parse(lblA3BWCurrentMeterReading.Text.Trim());
                    if (lblA4BWCurrentMeterReading.Text.Trim() == string.Empty)
                        serviceBook.A4BWMeterReading = null;
                    else
                        serviceBook.A4BWMeterReading = int.Parse(lblA4BWCurrentMeterReading.Text.Trim());
                    if (lblA3CLCurrentMeterReading.Text.Trim() == string.Empty)
                        serviceBook.A3CLMeterReading = null;
                    else
                        serviceBook.A3CLMeterReading = int.Parse(lblA3CLCurrentMeterReading.Text.Trim());
                    if (lblA4CLCurrentMeterReading.Text.Trim() == string.Empty)
                        serviceBook.A4CLMeterReading = null;
                    else
                        serviceBook.A4CLMeterReading = int.Parse(lblA4CLCurrentMeterReading.Text.Trim());

                    int j = objServiceBook.Service_MeterReading_Update(serviceBook);

                    if (j > 0)
                    {

                        ClearTonnerControls();
                        LoadTonnerRequest(gvTonnerRequest.PageIndex, gvTonnerRequest.PageSize);
                        MessageTonner.IsSuccess = true;
                        MessageTonner.Text = "Response to this Toner Request has been given.";
                    }
                    else
                    {
                        MessageTonner.IsSuccess = false;
                        MessageTonner.Text = "Response to this Toner Request is failed! Meter Reading cannot update.";
                    }
                }
                else
                {
                    MessageTonner.IsSuccess = false;
                    MessageTonner.Text = "Response to this Toner Request is failed!";
                }
                MessageTonner.Show = true;
            }
        }

        protected void btnDocketClose_Click(object sender, EventArgs e)
        {
            if (ValidateDocket())
            {
                Business.Service.ServiceBook objServiceBook = new Business.Service.ServiceBook();
                Entity.Service.ServiceBook serviceBook = new Entity.Service.ServiceBook();

                using (DataTable dtSpare = new DataTable())
                {
                    dtSpare.Columns.Add("AssetId");
                    dtSpare.Columns.Add("ReplacedItemId_FK");
                    dtSpare.Columns.Add("AssetLocationId");

                    foreach (DataRow drReplacedItem in Business.Common.Context.SelectedAssets.Rows)
                    {
                        dtSpare.Rows.Add();
                        dtSpare.Rows[dtSpare.Rows.Count - 1]["AssetId"] = drReplacedItem["AssetId"];
                        dtSpare.Rows[dtSpare.Rows.Count - 1]["ReplacedItemId_FK"] = drReplacedItem["ItemId"];
                        dtSpare.Rows[dtSpare.Rows.Count - 1]["AssetLocationId"] = (int)AssetLocation.Customer;
                        dtSpare.AcceptChanges();
                    }

                    using (DataTable dtAssociatedEngineers = new DataTable())
                    {
                        dtAssociatedEngineers.Columns.Add("EngineerId");
                        dtAssociatedEngineers.Columns.Add("InTime");
                        dtAssociatedEngineers.Columns.Add("OutTime");
                        dtAssociatedEngineers.Columns.Add("Remarks");

                        foreach (GridViewRow gvr in gvAssociatedEngineers.Rows)
                        {
                            CheckBox chkEngineer = (CheckBox)gvr.FindControl("chkEngineer");

                            if (chkEngineer.Checked)
                            {
                                TextBox txtAssociatedInDate = (TextBox)gvr.FindControl("txtAssociatedInDate");
                                DropDownList ddlAssociatedInTimeHH = (DropDownList)gvr.FindControl("ddlAssociatedInTimeHH");
                                DropDownList ddlAssociatedInTimeMM = (DropDownList)gvr.FindControl("ddlAssociatedInTimeMM");
                                DropDownList ddlAssociatedInTimeTT = (DropDownList)gvr.FindControl("ddlAssociatedInTimeTT");
                                TextBox txtAssociatedOutDate = (TextBox)gvr.FindControl("txtAssociatedOutDate");
                                DropDownList ddlAssociatedOutTimeHH = (DropDownList)gvr.FindControl("ddlAssociatedOutTimeHH");
                                DropDownList ddlAssociatedOutTimeMM = (DropDownList)gvr.FindControl("ddlAssociatedOutTimeMM");
                                DropDownList ddlAssociatedOutTimeTT = (DropDownList)gvr.FindControl("ddlAssociatedOutTimeTT");
                                TextBox txtAssociatedRemarks = (TextBox)gvr.FindControl("txtAssociatedRemarks");

                                dtAssociatedEngineers.Rows.Add();
                                dtAssociatedEngineers.Rows[dtAssociatedEngineers.Rows.Count - 1]["EngineerId"] = gvAssociatedEngineers.DataKeys[gvr.RowIndex].Values[0].ToString();
                                dtAssociatedEngineers.Rows[dtAssociatedEngineers.Rows.Count - 1]["InTime"] = Convert.ToDateTime(txtAssociatedInDate.Text + " " + ddlAssociatedInTimeHH.SelectedValue + ":" + ddlAssociatedInTimeMM.SelectedValue + ":00" + " " + ddlAssociatedInTimeTT.SelectedValue).ToString("yyyy-MM-dd hh:mm:ss tt");
                                dtAssociatedEngineers.Rows[dtAssociatedEngineers.Rows.Count - 1]["OutTime"] = Convert.ToDateTime(txtAssociatedOutDate.Text + " " + ddlAssociatedOutTimeHH.SelectedValue + ":" + ddlAssociatedOutTimeMM.SelectedValue + ":00" + " " + ddlAssociatedOutTimeTT.SelectedValue).ToString("yyyy-MM-dd hh:mm:ss tt");
                                dtAssociatedEngineers.Rows[dtAssociatedEngineers.Rows.Count - 1]["Remarks"] = txtAssociatedRemarks.Text.Trim();
                                dtAssociatedEngineers.AcceptChanges();
                            }
                        }

                        serviceBook.ServiceBookId = ServiceBookId;
                        serviceBook.CallId = DocketId;
                        serviceBook.CallType = 2;
                        serviceBook.EmployeeId_FK = int.Parse(ddlServiceEngineer.SelectedValue);
                        serviceBook.Remarks = txtRemarks.Text.Trim();
                        //Has problem check
                        serviceBook.InTime = Convert.ToDateTime(txtInDate.Text + " " + ddlInTimeHH.SelectedValue + ":" + ddlInTimeMM.SelectedValue + ":00" + " " + ddlInTimeTT.SelectedValue);
                        serviceBook.OutTime = Convert.ToDateTime(txtOutDate.Text + " " + ddlOutTimeHH.SelectedValue + ":" + ddlOutTimeMM.SelectedValue + ":00" + " " + ddlOutTimeTT.SelectedValue);
                        serviceBook.Diagnosis = ddlDocketDiagnosis.SelectedValue.Trim();
                        serviceBook.ActionTaken = ddlDocketActionTaken.SelectedValue.Trim();
                        serviceBook.CallStatusId = int.Parse(ddlCurrentCallStatusDocket.SelectedValue);
                        serviceBook.CustomerFeedback = txtCustomerFeedback.Text.Trim();
                        serviceBook.CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                        serviceBook.ProblemObserved = ddlProblemObserved.SelectedValue.Trim();
                        serviceBook.ServiceBookDetails = dtSpare;
                        serviceBook.CustomerPurchaseId = CustomerPurchaseId;
                        serviceBook.AssociatedEngineers = dtAssociatedEngineers;
                        if (txtA3BWCurrentMeterReading.Text.Trim() == string.Empty)
                            serviceBook.A3BWMeterReading = null;
                        else
                            serviceBook.A3BWMeterReading = int.Parse(txtA3BWCurrentMeterReading.Text.Trim());
                        if (txtA4BWCurrentMeterReading.Text.Trim() == string.Empty)
                            serviceBook.A4BWMeterReading = null;
                        else
                            serviceBook.A4BWMeterReading = int.Parse(txtA4BWCurrentMeterReading.Text.Trim());
                        if (txtA3CLCurrentMeterReading.Text.Trim() == string.Empty)
                            serviceBook.A3CLMeterReading = null;
                        else
                            serviceBook.A3CLMeterReading = int.Parse(txtA3CLCurrentMeterReading.Text.Trim());
                        if (txtA4CLCurrentMeterReading.Text.Trim() == string.Empty)
                            serviceBook.A4CLMeterReading = null;
                        else
                            serviceBook.A4CLMeterReading = int.Parse(txtA4CLCurrentMeterReading.Text.Trim());

                        serviceBook.Signature = Business.Common.Context.Signature;
                    }
                }
                int i = objServiceBook.Service_ServiceBook_Save(serviceBook);

                if (i > 0)
                {
                    //updating last meter reading in Customer Purchase
                    int j = 0;
                    j = objServiceBook.Service_MeterReading_Update(serviceBook);

                    if (j == 0)
                    {
                        MessageDocket.IsSuccess = false;
                        MessageDocket.Text = "Current meter reading unable to update! Please contact system administrator immediately.";
                        return;
                    }

                    //Mail body
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<div style='width: 99%; font-family: Cambria, Georgia, serif; color: #565656; margin:10px'>");
                    sb.Append("<center><img src='http://aegiscrm.in/images/Aegis_CRM_Logo.png' alt='AEGIS CRM' />");
                    sb.Append("<h2>Aegis Customer Relationship Management Notification</h2><br /></center>");
                    sb.Append("Date:- " + DateTime.Now.ToString("dd/MM/yyyy") + "<br /><br />");
                    sb.Append("Dear Aegis CRM Admin,<br />");
                    sb.Append("An employee has made a low spare request.<br /><br />");
                    sb.Append("<b><u>Low Tonner Request Details</u>:<br /><br />");
                    sb.Append("Docket Request No - " + DocketNo + "<br />");
                    sb.Append("Customer Name - " + CustomerName + "<br />");
                    sb.Append("Request Date and Time - " + System.DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + "<br />");
                    sb.Append("Model Name - " + ModelName + "<br />");
                    sb.Append("Machine ID - " + MachineId + "<br />");
                    //foreach (GridViewRow gvr in gvSpareList.Rows)
                    //{
                    //    CheckBox chkSpare = (CheckBox)gvr.FindControl("chkSpare");
                    //    if (chkSpare.Checked)
                    //    {
                    //        TextBox txtMeterReading = (TextBox)gvr.FindControl("txtMeterReading");
                    //        int lastMeterReading = int.Parse((objServiceBook.GetLastMeterReadingByCustomerPurchaseIdAndItemId(CustomerPurchaseId, int.Parse(gvSpareList.DataKeys[gvr.RowIndex].Values[0].ToString()))).Rows[0]["LastMeterReading"].ToString());
                    //        if ((int.Parse(txtMeterReading.Text) - lastMeterReading) < int.Parse(gvr.Cells[3].Text))
                    //        {
                    //            IsSendMail = true;
                    //            sb.Append("Spare Name - " + gvr.Cells[2].Text + "<br />");
                    //            sb.Append("Spare Yield- " + gvr.Cells[3].Text + "</b><br /><br />");
                    //            sb.Append("Current Reading - " + txtMeterReading.Text + "<br />");
                    //            sb.Append("Last Reading - " + txtMeterReading.Text + "<br /><br />");
                    //        }
                    //    }
                    //}
                    sb.Append("Click to login into portal <a href='http://aegiscrm.in'>aegiscrm.in</a><br /><br />");
                    sb.Append("<hr />");
                    sb.Append("<center tyle='color:#C68E17'>*** This is a system generated mail. Please do not reply. ***</center>");
                    sb.Append("</div>");

                    string fromMail = "", toMail = "", password = "", subject = "";
                    fromMail = "support@aegiscrm.in";
                    password = "P@ssw0rd";
                    toMail = "kunalchatterjeeit@gmail.com";
                    subject = "Low Tonner Request Notofication";

                    if (IsSendMail)
                        Business.Common.MailFunctionality.SendMail_HostingRaja(fromMail, toMail, password, subject, sb.ToString());

                    LoadDocket();
                    ClearDocketControls();
                    LoadServiceBookMasterHistory();
                    MessageDocket.IsSuccess = true;
                    MessageDocket.Text = "Docket response successfully given.";
                }
                else
                {
                    MessageDocket.IsSuccess = false;
                    MessageDocket.Text = "Sorry! docket cannot close. Please try again.";
                }
                MessageDocket.Show = true;
            }
        }

        protected void gvAssociatedEngineers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox txtAssociatedInDate = ((TextBox)e.Row.FindControl("txtAssociatedInDate"));
                TextBox txtAssociatedOutDate = ((TextBox)e.Row.FindControl("txtAssociatedOutDate"));
                DropDownList ddlAssociatedInTimeHH = ((DropDownList)e.Row.FindControl("ddlAssociatedInTimeHH"));
                DropDownList ddlAssociatedInTimeMM = ((DropDownList)e.Row.FindControl("ddlAssociatedInTimeMM"));
                DropDownList ddlAssociatedOutTimeHH = ((DropDownList)e.Row.FindControl("ddlAssociatedOutTimeHH"));
                DropDownList ddlAssociatedOutTimeMM = ((DropDownList)e.Row.FindControl("ddlAssociatedOutTimeMM"));

                txtAssociatedOutDate.Text = txtAssociatedInDate.Text = System.DateTime.Now.ToString("dd MMM yyyy");
                LoadTime(ddlAssociatedInTimeHH, ddlAssociatedInTimeMM, ddlAssociatedOutTimeHH, ddlAssociatedOutTimeMM);
            }
        }

        protected void gvTonnerList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (((DataTable)(gvTonnerList.DataSource)).Rows[e.Row.RowIndex]["ApprovalStatus"].ToString() == Convert.ToString((int)ApprovalStatus.Rejected))
                {
                    ((CheckBox)e.Row.FindControl("chkToner")).Enabled = false;
                    e.Row.Attributes["style"] = "background-color: #ff3f3f";
                }
            }
        }

        protected void gvTonnerRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTonnerRequest.PageIndex = e.NewPageIndex;
            LoadTonnerRequest(e.NewPageIndex, gvTonnerRequest.PageSize);
        }
    }
}