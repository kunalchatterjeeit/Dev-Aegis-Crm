using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using System.Text;
using Entity.Inventory;
using Entity.Service;
using Business.Common;
using System.Linq;

namespace WebAppAegisCRM.Service
{
    public partial class ServiceBook : System.Web.UI.Page
    {
        #region PROPERTIES
        public long TonerRequestId
        {
            get { return Convert.ToInt64(ViewState["TonerRequestId"]); }
            set { ViewState["TonerRequestId"] = value; }
        }
        public long DocketId
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
        public int SelectedDocketCallStatusId
        {
            get { return Convert.ToInt32(ViewState["SelectedDocketCallStatusId"]); }
            set { ViewState["SelectedDocketCallStatusId"] = value; }
        }
        #endregion

        #region USER DEFINED FUNCTIONS
        private bool ValidateMeterReadingDocket()
        {
            if (string.IsNullOrEmpty(txtA3BWCurrentMeterReading.Text.Trim()))
            {
                MessageDocket.IsSuccess = false;
                MessageDocket.Text = "Please enter A3 B/W meter reading.";
                MessageDocket.Show = true;
                return false;
            }
            if (string.IsNullOrEmpty(txtA4BWCurrentMeterReading.Text.Trim()) || Int64.Parse(txtA4BWCurrentMeterReading.Text.Trim()) <= 0)
            {
                MessageDocket.IsSuccess = false;
                MessageDocket.Text = "Please enter A4 B/W meter reading.";
                MessageDocket.Show = true;
                return false;
            }
            if (string.IsNullOrEmpty(txtA3CLCurrentMeterReading.Text.Trim()))
            {
                MessageDocket.IsSuccess = false;
                MessageDocket.Text = "Please enter A3 CL meter reading.";
                MessageDocket.Show = true;
                return false;
            }
            if (string.IsNullOrEmpty(txtA4CLCurrentMeterReading.Text.Trim()))
            {
                MessageDocket.IsSuccess = false;
                MessageDocket.Text = "Please enter A4 CL meter reading.";
                MessageDocket.Show = true;
                return false;
            }

            Business.Customer.Customer objCustomer = new Business.Customer.Customer();
            Entity.Customer.Customer customer = objCustomer.CustomerPurchase_GetByCustomerPurchaseId(CustomerPurchaseId);
            if (customer != null)
            {
                if (customer.A3BWMeterReading > Convert.ToInt64(txtA3BWCurrentMeterReading.Text.Trim()))
                {
                    MessageDocket.IsSuccess = false;
                    MessageDocket.Text = "Wrong A3 B/W meter reading.";
                    MessageDocket.Show = true;
                    return false;
                }
                if (customer.A4BWMeterReading > Convert.ToInt64(txtA4BWCurrentMeterReading.Text.Trim()))
                {
                    MessageDocket.IsSuccess = false;
                    MessageDocket.Text = "Wrong A4 B/W meter reading.";
                    MessageDocket.Show = true;
                    return false;
                }
                if (customer.A3CLMeterReading > Convert.ToInt64(txtA3CLCurrentMeterReading.Text.Trim()))
                {
                    MessageDocket.IsSuccess = false;
                    MessageDocket.Text = "Wrong A3 CL meter reading.";
                    MessageDocket.Show = true;
                    return false;
                }
                if (customer.A4CLMeterReading > Convert.ToInt64(txtA4CLCurrentMeterReading.Text.Trim()))
                {
                    MessageDocket.IsSuccess = false;
                    MessageDocket.Text = "Wrong A4 CL meter reading.";
                    MessageDocket.Show = true;
                    return false;
                }
            }

            return true;
        }
        private bool ValidateDocket()
        {
            MessageDocket.Show = false;
            bool retValue = false;

            if (!(Business.Common.Context.CallStatus.Equals(((int)CallStatusType.DocketOpenForApproval).ToString())
                || (Business.Common.Context.CallStatus.Equals(((int)CallStatusType.DocketResponseGiven).ToString())))
                && ddlCurrentCallStatusDocket.SelectedIndex == 0)
            {
                MessageDocket.IsSuccess = false;
                MessageDocket.Text = "Please select current call status.";
                MessageDocket.Show = true;
                return retValue = false;
            }

            if (ddlCurrentCallStatusDocket.SelectedValue == ((int)CallStatusType.DocketOpenForSpares).ToString()
                || ddlCurrentCallStatusDocket.SelectedValue == ((int)CallStatusType.DocketOpenForConsumables).ToString())
            {
                if (!ValidateMeterReadingDocket())
                {
                    return retValue = false;
                }
                else if (Business.Common.Context.SpareRequisition.Rows.Count == 0)
                {
                    MessageDocket.IsSuccess = false;
                    MessageDocket.Text = "Please submit requisition for spare.";
                    MessageDocket.Show = true;
                    return retValue = false;
                }
                else
                {
                    return retValue = true;
                }
            }

            if (Convert.ToDateTime(txtInDate.Text.Trim()).Date > System.DateTime.Now.Date)
            {
                MessageDocket.IsSuccess = false;
                MessageDocket.Text = "Invalid In date.";
                MessageDocket.Show = true;
                return retValue = false;
            }

            if (Convert.ToDateTime(txtOutDate.Text.Trim()).Date > DateTime.Now.Date)
            {
                MessageDocket.IsSuccess = false;
                MessageDocket.Text = "Invalid Out date.";
                MessageDocket.Show = true;
                return retValue = false;
            }

            if (!ValidateAssociatedEngineer())
            {
                return retValue = false;
            }

            foreach (GridViewRow gvr in gvDocket.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkDocket")).Checked == true)
                {
                    retValue = true;
                    break;
                }
            }
            if (!retValue)
            {
                MessageDocket.IsSuccess = false;
                MessageDocket.Text = "Please select docket.";
                MessageDocket.Show = true;
                return retValue = false;
            }

            //if (!(Request.QueryString["action"] != null && Request.QueryString["action"].Equals("callout")))
            //{
            //    if (ddlInTimeHH.SelectedIndex == 0)
            //    {
            //        MessageDocket.IsSuccess = false;
            //        MessageDocket.Text = "Please select In Time HH";
            //        MessageDocket.Show = true;
            //        return retValue = false;
            //    }
            //    else if (ddlInTimeMM.SelectedIndex == 0)
            //    {
            //        MessageDocket.IsSuccess = false;
            //        MessageDocket.Text = "Please select In Time MM";
            //        MessageDocket.Show = true;
            //        return retValue = false;
            //    }
            //}

            if ((Request.QueryString["action"] != null && Request.QueryString["action"].Equals("callin")))
            {
                if (ddlInTimeHH.SelectedIndex == 0)
                {
                    MessageDocket.IsSuccess = false;
                    MessageDocket.Text = "Please select In Time HH";
                    MessageDocket.Show = true;
                    return retValue = false;
                }
                else if (ddlInTimeMM.SelectedIndex == 0)
                {
                    MessageDocket.IsSuccess = false;
                    MessageDocket.Text = "Please select In Time MM";
                    MessageDocket.Show = true;
                    return retValue = false;
                }
            }

            if (!(Request.QueryString["action"] != null && Request.QueryString["action"].Equals("callin")))
            {
                if (ddlCurrentCallStatusDocket.SelectedValue == Convert.ToString((int)CallStatusType.DocketRequestInQueue))
                {
                    MessageDocket.IsSuccess = false;
                    MessageDocket.Text = "Cannot submit with Request in queue status";
                    MessageDocket.Show = true;
                    return retValue = false;
                }
            }

            if (Request.QueryString["action"] != null && (Request.QueryString["action"].Equals("callin") || Request.QueryString["action"].Equals("callout")))
            {
                retValue = true;
            }
            else
            {
                if (!ValidateMeterReadingDocket())
                {
                    return retValue = false;
                }

                if (ddlDocketActionTaken.SelectedIndex == 0)
                {
                    MessageDocket.IsSuccess = false;
                    MessageDocket.Text = "Please select action taken";
                    MessageDocket.Show = true;
                    return retValue = false;
                }

                if (ddlDocketDiagnosis.SelectedIndex == 0)
                {
                    MessageDocket.IsSuccess = false;
                    MessageDocket.Text = "Please select docket diagnosis.";
                    MessageDocket.Show = true;
                    return retValue = false;
                }

                if (ddlProblemObserved.SelectedIndex == 0)
                {
                    MessageDocket.IsSuccess = false;
                    MessageDocket.Text = "Please select problem observed";
                    MessageDocket.Show = true;
                    return retValue = false;
                }

                //Checking Call in is there otherwise call out is not possible
                if (!new Business.Service.ServiceBook().IsCallInPresent(Business.Common.Context.ServiceBookId))
                {
                    MessageDocket.IsSuccess = false;
                    MessageDocket.Text = "Please do call in first.";
                    MessageDocket.Show = true;
                    return retValue = false;
                }
            }

            if (string.IsNullOrEmpty(Business.Common.Context.Signature)
                && ddlCurrentCallStatusDocket.SelectedValue == Convert.ToString((int)CallStatusType.DocketClose))
            {
                MessageDocket.IsSuccess = false;
                MessageDocket.Text = "Please provide signature before submit.";
                MessageDocket.Show = true;
                return retValue = false;
            }

            if (SelectedDocketCallStatusId == (int)CallStatusType.DocketOpenForApproval)
            {
                MessageDocket.IsSuccess = false;
                MessageDocket.Text = "Cannot submit while status is in OPEN FOR APPROVAL/RESPONSE GIVEN. Please contact admin.";
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
            if (ddlCurrentTonnerRequestCallStatus.SelectedValue == ((int)CallStatusType.TonerDelivered).ToString()
                && Business.Common.Context.SelectedAssets.Rows != null && Business.Common.Context.SelectedAssets.Rows.Count > 0)
            {
                isTonerSelected = true;
            }
            else if (ddlCurrentTonnerRequestCallStatus.SelectedValue == ((int)CallStatusType.TonerRejected).ToString())
            {
                isTonerSelected = true;
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
        private void EmployeeCallLogin(object sender, EventArgs e)
        {
            ddlInTimeHH.SelectedValue = DateTime.Now.ToString("hh");
            ddlInTimeMM.SelectedValue = DateTime.Now.ToString("mm");
            ddlInTimeTT.SelectedValue = DateTime.Now.ToString("tt");
            ddlCurrentCallStatusDocket.SelectedValue = (ddlCurrentCallStatusDocket.SelectedIndex == 0) ? ((int)CallStatusType.DocketRequestInQueue).ToString() : ddlCurrentCallStatusDocket.SelectedValue;
            btnDocketClose_Click(sender, e);
        }
        private void EmployeeCallLogout(object sender, EventArgs e)
        {
            ddlOutTimeHH.SelectedValue = DateTime.Now.ToString("hh");
            ddlOutTimeMM.SelectedValue = DateTime.Now.ToString("mm");
            ddlOutTimeTT.SelectedValue = DateTime.Now.ToString("tt");
            //ddlCurrentCallStatusDocket.SelectedValue = ((int)CallStatusType.DocketRequestInQueue).ToString();
            btnDocketClose_Click(sender, e);
        }
        private bool ValidateAssociatedEngineer()
        {
            bool retValue = true;
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
            return retValue;
        }
        protected void LoadTime(DropDownList ddlInHH, DropDownList ddlInMM, DropDownList ddlOutHH, DropDownList ddlOutMM, DropDownList ddlInTT, DropDownList ddlOutTT)
        {
            ddlInHH.Items.Clear();
            ddlInMM.Items.Clear();
            ddlInTT.Items.Clear();
            ddlOutTT.Items.Clear();
            ddlOutHH.Items.Clear();
            ddlOutMM.Items.Clear();

            ddlInHH.Items.Insert(0, "HH");
            ddlOutHH.Items.Insert(0, "HH");
            ddlInMM.Items.Insert(0, "MM");
            ddlOutMM.Items.Insert(0, "MM");
            ddlInTT.Items.Insert(0, "AM");
            ddlInTT.Items.Insert(1, "PM");
            ddlOutTT.Items.Insert(0, "AM");
            ddlOutTT.Items.Insert(1, "PM");

            for (int i = 0; i <= 12; i++)
            {
                ListItem li = new ListItem(i.ToString("00"), i.ToString("00"));
                ddlInHH.Items.Insert(i + 1, li);
            }
            for (int i = 0; i <= 12; i++)
            {
                ListItem li = new ListItem(i.ToString("00"), i.ToString("00"));
                ddlOutHH.Items.Insert(i + 1, li);
            }

            for (int i = 0; i <= 59; i++)
            {
                ListItem li = new ListItem(i.ToString("00"), i.ToString("00"));
                ddlInMM.Items.Insert(i + 1, li);
            }
            for (int i = 0; i <= 59; i++)
            {
                ListItem li = new ListItem(i.ToString("00"), i.ToString("00"));
                ddlOutMM.Items.Insert(i + 1, li);
            }
            ddlInTimeTT.SelectedIndex = 0;
            ddlOutTimeTT.SelectedIndex = 0;
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
            ddlDocketCallStatus.InsertSelect();
            //ddlDocketCallStatus.SelectedIndex = 1;
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
            DataTable dt = objCallStatus.GetAll((int)CallType.Docket);
            dt.Rows.Remove(dt.AsEnumerable().Where(x => x["CallStatusId"].ToString() == ((int)CallStatusType.DocketResponseGiven).ToString()).FirstOrDefault());
            dt.Rows.Remove(dt.AsEnumerable().Where(x => x["CallStatusId"].ToString() == ((int)CallStatusType.DocketOpenForApproval).ToString()).FirstOrDefault());
            dt.AcceptChanges();
            if (dt != null)
            {
                ddlCurrentCallStatusDocket.DataSource = dt;
                ddlCurrentCallStatusDocket.DataTextField = "CallStatus";
                ddlCurrentCallStatusDocket.DataValueField = "CallStatusId";
                ddlCurrentCallStatusDocket.DataBind();
            }
            ListItem li = new ListItem("--SELECT--", "0");
            ddlCurrentCallStatusDocket.InsertSelect();
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
            DataTable dt = objCallStatus.GetAll((int)CallType.Toner);
            dt.Rows.Remove(dt.AsEnumerable().Where(x => x["CallStatusId"].ToString() == ((int)CallStatusType.TonerOpenForApproval).ToString()).FirstOrDefault());
            dt.Rows.Remove(dt.AsEnumerable().Where(x => x["CallStatusId"].ToString() == ((int)CallStatusType.TonerRequestInQueue).ToString()).FirstOrDefault());
            dt.Rows.Remove(dt.AsEnumerable().Where(x => x["CallStatusId"].ToString() == ((int)CallStatusType.TonerResponseGiven).ToString()).FirstOrDefault());
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
            DataSet dsService = objServiceBook.Service_Tonner_GetByTonnerRequestId(TonerRequestId);

            //if (dsService != null)
            //{
            //    gvTonnerList.DataSource = dsService.Tables[0];
            //    gvTonnerList.DataBind();
            //}
            lblA3BWCurrentMeterReading.Text = (dsService.Tables[0].Rows[0]["A3BWCurrentMeterReading"] == DBNull.Value) ? "0" : dsService.Tables[0].Rows[0]["A3BWCurrentMeterReading"].ToString();
            lblA4BWCurrentMeterReading.Text = (dsService.Tables[0].Rows[0]["A4BWCurrentMeterReading"] == DBNull.Value) ? "0" : dsService.Tables[0].Rows[0]["A4BWCurrentMeterReading"].ToString();
            lblA3CLCurrentMeterReading.Text = (dsService.Tables[0].Rows[0]["A3CLCurrentMeterReading"] == DBNull.Value) ? "0" : dsService.Tables[0].Rows[0]["A3CLCurrentMeterReading"].ToString();
            lblA4CLCurrentMeterReading.Text = (dsService.Tables[0].Rows[0]["A4CLCurrentMeterReading"] == DBNull.Value) ? "0" : dsService.Tables[0].Rows[0]["A4CLCurrentMeterReading"].ToString();
            lblA3BWLastMeterReading.Text = (dsService.Tables[1].Rows.Count == 0 || dsService.Tables[1].Rows[0]["A3BWLastMeterReading"] == DBNull.Value) ? "0" : dsService.Tables[1].Rows[0]["A3BWLastMeterReading"].ToString();
            lblA4BWLastMeterReading.Text = (dsService.Tables[1].Rows.Count == 0 || dsService.Tables[1].Rows[0]["A4BWLastMeterReading"] == DBNull.Value) ? "0" : dsService.Tables[1].Rows[0]["A4BWLastMeterReading"].ToString();
            lblA3CLLastMeterReading.Text = (dsService.Tables[1].Rows.Count == 0 || dsService.Tables[1].Rows[0]["A3CLLastMeterReading"] == DBNull.Value) ? "0" : dsService.Tables[1].Rows[0]["A3CLLastMeterReading"].ToString();
            lblA4CLLastMeterReading.Text = (dsService.Tables[1].Rows.Count == 0 || dsService.Tables[1].Rows[0]["A4CLLastMeterReading"] == DBNull.Value) ? "0" : dsService.Tables[1].Rows[0]["A4CLLastMeterReading"].ToString();
            Business.Common.Context.ServiceBookId = (dsService.Tables[0].Rows[0]["ServiceBookId"] != null) ? Convert.ToInt64(dsService.Tables[0].Rows[0]["ServiceBookId"].ToString()) : 0;
        }
        protected void ClearDocketControls()
        {
            lblProblem.Text = "";
            txtInDate.Text = System.DateTime.Now.ToString("dd MMM yyyy");
            txtOutDate.Text = System.DateTime.Now.ToString("dd MMM yyyy");
            LoadTime(ddlInTimeHH, ddlInTimeMM, ddlOutTimeHH, ddlOutTimeMM, ddlInTimeTT, ddlOutTimeTT);
            ddlCurrentCallStatusDocket.SelectedIndex = 0;
            ddlProblemObserved.SelectedIndex = 0;
            ddlDocketDiagnosis.SelectedIndex = 0;
            ddlDocketActionTaken.SelectedIndex = 0;
            txtCustomerFeedback.Text = "";
            txtRemarks.Text = "";
            ddlServiceEngineer.SelectedIndex = 0;
            //gvAssociatedEngineers.DataBind();
            LoadAssociateEngineersList();
            MessageDocket.Show = false;
            Business.Common.Context.SelectedAssets.Clear();
            Business.Common.Context.Signature = string.Empty;
            Business.Common.Context.CallId = 0;
            Business.Common.Context.CallType = CallType.None;
            Business.Common.Context.ServiceBookId = 0;
            Business.Common.Context.SpareRequisition.Clear();
            ddlInTimeHH.SelectedValue = DateTime.Now.ToString("hh");
            ddlInTimeMM.SelectedValue = DateTime.Now.ToString("mm");
            ddlInTimeTT.SelectedValue = DateTime.Now.ToString("tt");
            ddlOutTimeHH.SelectedValue = DateTime.Now.ToString("hh");
            ddlOutTimeMM.SelectedValue = DateTime.Now.ToString("mm");
            ddlOutTimeTT.SelectedValue = DateTime.Now.ToString("tt");
            txtA3BWCurrentMeterReading.Text = "0";
            txtA4BWCurrentMeterReading.Text = "0";
            txtA3CLCurrentMeterReading.Text = "0";
            txtA4CLCurrentMeterReading.Text = "0";
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
            //gvTonnerList.DataBind();
            MessageTonner.Show = false;
            Business.Common.Context.CallId = 0;
            Business.Common.Context.CallType = CallType.None;
            Business.Common.Context.SelectedAssets.Clear();
            Business.Common.Context.Signature = string.Empty;
            Business.Common.Context.ServiceBookId = 0;
            Business.Common.Context.SpareRequisition.Clear();
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
        }
        protected void LoadAssociateEngineersList()
        {
            Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
            Entity.HR.EmployeeMaster employeeMaster = new Entity.HR.EmployeeMaster();
            employeeMaster.CompanyId_FK = 1;
            DataTable dt = objEmployeeMaster.EmployeeMaster_GetAll(employeeMaster);
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
        private Entity.Service.ServiceBook AssignningValuesToModel(Entity.Service.ServiceBook serviceBook, DataTable dtSpare, DataTable dtAssociatedEngineers)
        {
            serviceBook.ServiceBookId = Business.Common.Context.ServiceBookId;
            serviceBook.CallId = DocketId;
            serviceBook.CallType = (int)CallType.Docket;
            serviceBook.EmployeeId_FK = int.Parse(ddlServiceEngineer.SelectedValue);
            serviceBook.Remarks = txtRemarks.Text.Trim();
            //Has problem check
            //serviceBook.InTime = Convert.ToDateTime(txtInDate.Text + " " + ddlInTimeHH.SelectedValue + ":" + ddlInTimeMM.SelectedValue + ":00" + " " + ddlInTimeTT.SelectedValue);
            //serviceBook.OutTime = (Request.QueryString["action"] != null && Request.QueryString["action"].Equals("callin")) ? DateTime.MinValue : Convert.ToDateTime(txtOutDate.Text + " " + ddlOutTimeHH.SelectedValue + ":" + ddlOutTimeMM.SelectedValue + ":00" + " " + ddlOutTimeTT.SelectedValue);
            serviceBook.Diagnosis = ddlDocketDiagnosis.SelectedValue.Trim();
            serviceBook.ActionTaken = ddlDocketActionTaken.SelectedValue.Trim();
            if ((Request.QueryString["action"] != null && (Request.QueryString["action"].Equals("callin")))
                && (Business.Common.Context.CallStatus.Equals(((int)CallStatusType.DocketOpenForApproval).ToString())
                || (Business.Common.Context.CallStatus.Equals(((int)CallStatusType.DocketResponseGiven).ToString()))))//dropdown does not have this two status in list
            {
                serviceBook.CallStatusId = int.Parse(Business.Common.Context.CallStatus);
            }
            else
            {
                serviceBook.CallStatusId = int.Parse(ddlCurrentCallStatusDocket.SelectedValue);
            }
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
            return serviceBook;
        }
        private static void PreparingSpares(DataTable dtSpare)
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
        }
        private bool CheckingSpareYield(Entity.Service.ServiceBook serviceBook, Business.Service.ServiceBook objServiceBook)
        {
            bool retValue = false;
            using (DataTable dtSpare = new DataTable())
            {
                dtSpare.Columns.Add("CallId");
                dtSpare.Columns.Add("CallType");
                dtSpare.Columns.Add("ItemId");
                dtSpare.Columns.Add("RequisiteQty");
                dtSpare.Columns.Add("A3BW");
                dtSpare.Columns.Add("A3CL");
                dtSpare.Columns.Add("A4BW");
                dtSpare.Columns.Add("A4CL");

                foreach (DataRow drReplacedItem in Business.Common.Context.SpareRequisition.Rows)
                {
                    dtSpare.Rows.Add();
                    dtSpare.Rows[dtSpare.Rows.Count - 1]["CallId"] = serviceBook.CallId;
                    dtSpare.Rows[dtSpare.Rows.Count - 1]["CallType"] = (int)CallType.Docket;
                    dtSpare.Rows[dtSpare.Rows.Count - 1]["ItemId"] = drReplacedItem["SpareId"];
                    dtSpare.Rows[dtSpare.Rows.Count - 1]["RequisiteQty"] = drReplacedItem["RequisiteQty"];
                    if (txtA3BWCurrentMeterReading.Text.Trim() == string.Empty)
                        dtSpare.Rows[dtSpare.Rows.Count - 1]["A3BW"] = 0;
                    else
                        dtSpare.Rows[dtSpare.Rows.Count - 1]["A3BW"] = int.Parse(txtA3BWCurrentMeterReading.Text.Trim());
                    if (txtA4BWCurrentMeterReading.Text.Trim() == string.Empty)
                        dtSpare.Rows[dtSpare.Rows.Count - 1]["A4BW"] = 0;
                    else
                        dtSpare.Rows[dtSpare.Rows.Count - 1]["A4BW"] = int.Parse(txtA4BWCurrentMeterReading.Text.Trim());
                    if (txtA3CLCurrentMeterReading.Text.Trim() == string.Empty)
                        dtSpare.Rows[dtSpare.Rows.Count - 1]["A3CL"] = 0;
                    else
                        dtSpare.Rows[dtSpare.Rows.Count - 1]["A3CL"] = int.Parse(txtA3CLCurrentMeterReading.Text.Trim());
                    if (txtA4CLCurrentMeterReading.Text.Trim() == string.Empty)
                        dtSpare.Rows[dtSpare.Rows.Count - 1]["A4CL"] = 0;
                    else
                        dtSpare.Rows[dtSpare.Rows.Count - 1]["A4CL"] = int.Parse(txtA4CLCurrentMeterReading.Text.Trim());
                    dtSpare.AcceptChanges();
                }
                serviceBook.CallId = DocketId;
                serviceBook.ApprovalItems = dtSpare;
            }

            if (serviceBook.ApprovalItems != null && serviceBook.ApprovalItems.Rows.Count > 0)
            {
                retValue = objServiceBook.Service_ServiceSpareApprovalCheck(serviceBook);
            }
            else
                retValue = false;
            return retValue;
        }
        private void PreparingAssociatedEngineers(DataTable dtAssociatedEngineers)
        {
            dtAssociatedEngineers.Columns.Add("EngineerId");
            dtAssociatedEngineers.Columns.Add("InTime");
            dtAssociatedEngineers.Columns.Add("OutTime");
            dtAssociatedEngineers.Columns.Add("Remarks");
            dtAssociatedEngineers.Columns.Add("CallStatus");

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
                    dtAssociatedEngineers.Rows[dtAssociatedEngineers.Rows.Count - 1]["CallStatus"] = ddlCurrentCallStatusDocket.SelectedValue.Trim();
                    dtAssociatedEngineers.AcceptChanges();
                }
            }
        }
        private void SentMail()
        {
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
        }
        private void Service_ServiceBookMaster_GetByCallId(long callId, CallType callType)
        {
            Business.Service.ServiceBook objBusiness = new Business.Service.ServiceBook();
            DataSet dsService = objBusiness.Service_ServiceBookMaster_GetByCallId(callId, callType);

            if (dsService != null && dsService.Tables != null)
            {
                if (dsService.Tables.Count > 0 && dsService.Tables[0] != null && dsService.Tables[0].Rows != null && dsService.Tables[0].Rows.Count > 0)
                {
                    if (dsService.Tables[0].Rows[0]["InTime"] != null && !string.IsNullOrEmpty(dsService.Tables[0].Rows[0]["InTime"].ToString()))
                    {
                        txtInDate.Text = Convert.ToDateTime(dsService.Tables[0].Rows[0]["InTime"].ToString()).ToString("dd MMM yyyy");

                        if (Convert.ToInt32(Convert.ToDateTime(dsService.Tables[0].Rows[0]["InTime"].ToString()).ToString("HH")) > 12)
                            ddlInTimeHH.SelectedValue = (Convert.ToInt32(Convert.ToDateTime(dsService.Tables[0].Rows[0]["InTime"].ToString()).ToString("HH")) - 12).ToString("00");
                        else
                            ddlInTimeHH.SelectedValue = Convert.ToDateTime(dsService.Tables[0].Rows[0]["InTime"].ToString()).ToString("HH");
                        ddlInTimeMM.SelectedValue = Convert.ToDateTime(dsService.Tables[0].Rows[0]["InTime"].ToString()).ToString("mm");
                        ddlInTimeTT.SelectedValue = Convert.ToDateTime(dsService.Tables[0].Rows[0]["InTime"].ToString()).ToString("tt");
                    }
                    if (dsService.Tables[0].Rows[0]["OutTime"] != null && !string.IsNullOrEmpty(dsService.Tables[0].Rows[0]["OutTime"].ToString()))
                    {
                        txtOutDate.Text = Convert.ToDateTime(dsService.Tables[0].Rows[0]["OutTime"].ToString()).ToString("dd MMM yyyy");

                        if (Convert.ToInt32(Convert.ToDateTime(dsService.Tables[0].Rows[0]["OutTime"].ToString()).ToString("HH")) > 12)
                            ddlOutTimeHH.SelectedValue = (Convert.ToInt32(Convert.ToDateTime(dsService.Tables[0].Rows[0]["OutTime"].ToString()).ToString("HH")) - 12).ToString("00");
                        else
                            ddlOutTimeHH.SelectedValue = Convert.ToDateTime(dsService.Tables[0].Rows[0]["OutTime"].ToString()).ToString("HH");
                        ddlOutTimeMM.SelectedValue = Convert.ToDateTime(dsService.Tables[0].Rows[0]["OutTime"].ToString()).ToString("mm");
                        ddlOutTimeTT.SelectedValue = Convert.ToDateTime(dsService.Tables[0].Rows[0]["OutTime"].ToString()).ToString("tt");
                    }
                    txtA3BWCurrentMeterReading.Text = (dsService.Tables[0].Rows[0]["A3BWMeterReading"] != null
                        && !string.IsNullOrEmpty(dsService.Tables[0].Rows[0]["A3BWMeterReading"].ToString())) ? dsService.Tables[0].Rows[0]["A3BWMeterReading"].ToString() : "0";
                    txtA4BWCurrentMeterReading.Text = (dsService.Tables[0].Rows[0]["A4BWMeterReading"] != null
                        && !string.IsNullOrEmpty(dsService.Tables[0].Rows[0]["A4BWMeterReading"].ToString())) ? dsService.Tables[0].Rows[0]["A4BWMeterReading"].ToString() : "0";
                    txtA3CLCurrentMeterReading.Text = (dsService.Tables[0].Rows[0]["A3CLMeterReading"] != null
                        && !string.IsNullOrEmpty(dsService.Tables[0].Rows[0]["A3CLMeterReading"].ToString())) ? dsService.Tables[0].Rows[0]["A3CLMeterReading"].ToString() : "0";
                    txtA4CLCurrentMeterReading.Text = (dsService.Tables[0].Rows[0]["A4CLMeterReading"] != null
                        && !string.IsNullOrEmpty(dsService.Tables[0].Rows[0]["A4CLMeterReading"].ToString())) ? dsService.Tables[0].Rows[0]["A4CLMeterReading"].ToString() : "0";
                    ddlServiceEngineer.SelectedValue = (dsService.Tables[0].Rows[0]["EmployeeId_FK"] != null) ? dsService.Tables[0].Rows[0]["EmployeeId_FK"].ToString() : "0";
                    ddlProblemObserved.SelectedValue = (dsService.Tables[0].Rows[0]["ProblemObserved"] != null) ? dsService.Tables[0].Rows[0]["ProblemObserved"].ToString() : "0";
                    ddlDocketDiagnosis.SelectedValue = (dsService.Tables[0].Rows[0]["Diagonosis"] != null) ? dsService.Tables[0].Rows[0]["Diagonosis"].ToString() : "0";
                    ddlDocketActionTaken.SelectedValue = (dsService.Tables[0].Rows[0]["ActionTaken"] != null) ? dsService.Tables[0].Rows[0]["ActionTaken"].ToString() : "0";
                    txtRemarks.Text = (dsService.Tables[0].Rows[0]["Remarks"] != null) ? dsService.Tables[0].Rows[0]["Remarks"].ToString() : string.Empty;
                    txtCustomerFeedback.Text = (dsService.Tables[0].Rows[0]["CustomerFeedback"] != null) ? dsService.Tables[0].Rows[0]["CustomerFeedback"].ToString() : string.Empty;
                    Business.Common.Context.CallStatus = (dsService.Tables[0].Rows[0]["ProblemStatus"] != null) ? dsService.Tables[0].Rows[0]["ProblemStatus"].ToString() : "0";
                    ddlCurrentCallStatusDocket.SelectedValue = (dsService.Tables[0].Rows[0]["ProblemStatus"] != null) ? dsService.Tables[0].Rows[0]["ProblemStatus"].ToString() : "0";
                    Business.Common.Context.ServiceBookId = (dsService.Tables[0].Rows[0]["ServiceBookId"] != null) ? Convert.ToInt64(dsService.Tables[0].Rows[0]["ServiceBookId"].ToString()) : 0;
                }
            }
        }
        private void Service_AssociatedEngineers_GetByCallId(long callId, CallType callType)
        {
            Business.Service.ServiceBook objBusiness = new Business.Service.ServiceBook();
            DataSet dsAssociates = objBusiness.Service_AssociatedEngineers_GetByCallId(callId, callType);

            if (dsAssociates != null && dsAssociates.Tables != null)
            {
                if (dsAssociates.Tables.Count > 0 && dsAssociates.Tables[0] != null)
                {
                    foreach (GridViewRow gridViewRow in gvAssociatedEngineers.Rows)
                    {
                        if (dsAssociates.Tables[0].AsEnumerable().Where(x => x["EngineerId"].ToString() == gvAssociatedEngineers.DataKeys[gridViewRow.RowIndex].Values[0].ToString()).Any())
                        {
                            DataRow drAssociate = dsAssociates.Tables[0].NewRow();
                            drAssociate = dsAssociates.Tables[0].AsEnumerable().Where(x => x["EngineerId"].ToString() == gvAssociatedEngineers.DataKeys[gridViewRow.RowIndex].Values[0].ToString()).FirstOrDefault();
                            ((CheckBox)gridViewRow.FindControl("chkEngineer")).Checked = true;
                            if (drAssociate["InTime"] != null)
                            {
                                ((TextBox)gridViewRow.FindControl("txtAssociatedInDate")).Text = (drAssociate["InTime"] != null) ? Convert.ToDateTime(drAssociate["InTime"].ToString()).ToString("dd MMM yyyy") : DateTime.Now.ToString("dd MMM yyyy");

                                if (Convert.ToInt32(Convert.ToDateTime(drAssociate["InTime"].ToString()).ToString("HH")) > 12)
                                    ((DropDownList)gridViewRow.FindControl("ddlAssociatedInTimeHH")).SelectedValue = (Convert.ToInt32(Convert.ToDateTime(drAssociate["InTime"].ToString()).ToString("HH")) - 12).ToString("00");
                                else
                                    ((DropDownList)gridViewRow.FindControl("ddlAssociatedInTimeHH")).SelectedValue = Convert.ToDateTime(drAssociate["InTime"].ToString()).ToString("HH");
                                ((DropDownList)gridViewRow.FindControl("ddlAssociatedInTimeMM")).SelectedValue = (drAssociate["InTime"] != null) ? Convert.ToDateTime(drAssociate["InTime"].ToString()).ToString("MM") : "MM";
                                ((DropDownList)gridViewRow.FindControl("ddlAssociatedInTimeTT")).SelectedValue = (drAssociate["InTime"] != null) ? Convert.ToDateTime(drAssociate["InTime"].ToString()).ToString("tt") : "AM";
                            }
                            if (drAssociate["OutTime"] != null)
                            {
                                ((TextBox)gridViewRow.FindControl("txtAssociatedOutDate")).Text = (Convert.ToDateTime(drAssociate["OutTime"].ToString()).ToString("dd MMM yyyy") != null) ? Convert.ToDateTime(drAssociate["OutTime"].ToString()).ToString("dd MMM yyyy") : DateTime.Now.ToString("dd MMM yyyy");

                                if (Convert.ToInt32(Convert.ToDateTime(drAssociate["OutTime"].ToString()).ToString("HH")) > 12)
                                    ((DropDownList)gridViewRow.FindControl("ddlAssociatedOutTimeHH")).SelectedValue = (Convert.ToInt32(Convert.ToDateTime(drAssociate["OutTime"].ToString()).ToString("HH")) - 12).ToString("00");
                                else
                                    ((DropDownList)gridViewRow.FindControl("ddlAssociatedOutTimeHH")).SelectedValue = Convert.ToDateTime(drAssociate["OutTime"].ToString()).ToString("HH");
                                ((DropDownList)gridViewRow.FindControl("ddlAssociatedOutTimeMM")).SelectedValue = (drAssociate["OutTime"] != null) ? Convert.ToDateTime(drAssociate["OutTime"].ToString()).ToString("mm") : "MM";
                                ((DropDownList)gridViewRow.FindControl("ddlAssociatedOutTimeTT")).SelectedValue = (drAssociate["OutTime"] != null) ? Convert.ToDateTime(drAssociate["OutTime"].ToString()).ToString("tt") : "AM";
                            }
                            ((TextBox)gridViewRow.FindControl("txtAssociatedRemarks")).Text = (drAssociate["Remarks"] != null) ? drAssociate["Remarks"].ToString() : string.Empty;
                        }
                    }
                }
            }
        }
        private void LoadFunctions()
        {
            btnCallTransfer.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.CALL_TRANSFER);
            LoadTime(ddlInTimeHH, ddlInTimeMM, ddlOutTimeHH, ddlOutTimeMM, ddlInTimeTT, ddlOutTimeTT);
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
        }
        #endregion

        #region SYSTEM DEFINED FUNCTIONS
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    Response.Redirect("~/MainLogout.aspx");
                }

                LoadFunctions();
                //Checking Auto Fetch functionality conditions
                if (Request.QueryString["callid"] != null && Request.QueryString["callid"].ToString().Length > 0)
                {
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
                            //TonerRequestId = 0;
                            divCallType.Visible = false;
                            //divTonnerRequest.Visible = false;
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
                            //DocketId = 0;
                            divCallType.Visible = false;
                            //divDocket.Visible = false;
                        }

                        if (Request.QueryString["action"] != null && Request.QueryString["action"].Equals("callin"))
                        {
                            EmployeeCallLogin(sender, e);
                        }
                        else if (Request.QueryString["action"] != null && Request.QueryString["action"].Equals("callout"))
                        {
                            EmployeeCallLogout(sender, e);
                        }
                    }
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlCallType.SelectedValue == Convert.ToString((int)CallType.Toner))
            {
                divTonnerRequest.Visible = true;
                divDocket.Visible = false;
                divDocketClosing.Visible = false;
                divTonnerRequestApproval.Visible = false;
                LoadTonnerRequest(1, gvTonnerRequest.PageSize);
            }
            else if (ddlCallType.SelectedValue == Convert.ToString((int)CallType.Docket))
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
            LoadTonnerRequest(1, gvTonnerRequest.PageSize);
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
                    DocketId = long.Parse(gvDocket.DataKeys[gridViewRow.RowIndex].Values[0].ToString());
                    Business.Common.Context.CallId = DocketId;
                    Business.Common.Context.CallType = CallType.Docket;
                    lblProblem.Text = gridViewRow.Cells[8].Text;
                    divDocketClosing.Visible = true;
                    divDocketClosingHistory.Visible = true;
                    LoadServiceBookMasterHistory();
                    Service_ServiceBookMaster_GetByCallId(DocketId, CallType.Docket);
                    Service_AssociatedEngineers_GetByCallId(DocketId, CallType.Docket);
                }
                else
                {
                    divDocketClosing.Visible = false;
                    divDocketClosingHistory.Visible = false;
                }

                if (gvDocket.DataKeys[gridViewRow.RowIndex].Values != null && gvDocket.DataKeys[gridViewRow.RowIndex].Values.Count > 1)
                    Business.Common.Context.ProductId = long.Parse(gvDocket.DataKeys[gridViewRow.RowIndex].Values[1].ToString());

                //Set Assign Engineer
                if (gvDocket.DataKeys[gridViewRow.RowIndex].Values != null && gvDocket.DataKeys[gridViewRow.RowIndex].Values.Count > 2)
                    ddlServiceEngineer.SelectedValue = Convert.ToString(gvDocket.DataKeys[gridViewRow.RowIndex].Values[2].ToString());

                if (gvDocket.DataKeys[gridViewRow.RowIndex].Values != null && gvDocket.DataKeys[gridViewRow.RowIndex].Values.Count > 3)
                    CustomerPurchaseId = int.Parse(gvDocket.DataKeys[gridViewRow.RowIndex].Values[3].ToString());

                if (gvDocket.DataKeys[gridViewRow.RowIndex].Values != null && gvDocket.DataKeys[gridViewRow.RowIndex].Values.Count > 4)
                    SelectedDocketCallStatusId = Convert.ToInt32(gvDocket.DataKeys[gridViewRow.RowIndex].Values[4].ToString());
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
                        TonerRequestId = long.Parse(gvTonnerRequest.DataKeys[gridViewRow.RowIndex].Values[0].ToString());
                        Business.Common.Context.CallId = TonerRequestId;
                        Business.Common.Context.CallType = CallType.Toner;
                    }

                    if (gvTonnerRequest.DataKeys[gridViewRow.RowIndex].Values != null && gvTonnerRequest.DataKeys[gridViewRow.RowIndex].Values.Count > 1)
                        CustomerPurchaseId = int.Parse(gvTonnerRequest.DataKeys[gridViewRow.RowIndex].Values[1].ToString());

                    if (gvTonnerRequest.DataKeys[gridViewRow.RowIndex].Values != null && gvTonnerRequest.DataKeys[gridViewRow.RowIndex].Values.Count > 2)
                        Business.Common.Context.ProductId = long.Parse(gvTonnerRequest.DataKeys[gridViewRow.RowIndex].Values[2].ToString());

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
                    serviceBook.ServiceBookDetails.Columns.Add("AssetId");
                    serviceBook.ServiceBookDetails.Columns.Add("AssetLocationId");

                    foreach (DataRow drAsset in Business.Common.Context.SelectedAssets.Rows)
                    {
                        DataRow dr = serviceBook.ServiceBookDetails.NewRow();
                        dr["TonerId"] = long.Parse(drAsset["ItemId"].ToString());
                        dr["AssetId"] = drAsset["AssetId"].ToString();
                        dr["AssetLocationId"] = (int)AssetLocation.Customer;
                        serviceBook.ServiceBookDetails.Rows.Add(dr);
                        serviceBook.ServiceBookDetails.AcceptChanges();
                    }
                }

                int approveResponse = objServiceBook.Service_TonerRequest_Approve(serviceBook);

                if (approveResponse > 0)
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

                    int readingResponse = objServiceBook.Service_MeterReading_Update(serviceBook);

                    if (readingResponse > 0)
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
                ServiceCallAttendance serviceCallAttendance = new ServiceCallAttendance();

                using (DataTable dtSpare = new DataTable())
                {
                    PreparingSpares(dtSpare);

                    using (DataTable dtAssociatedEngineers = new DataTable())
                    {
                        PreparingAssociatedEngineers(dtAssociatedEngineers);
                        serviceBook = AssignningValuesToModel(serviceBook, dtSpare, dtAssociatedEngineers);
                    }
                }

                if (Business.Common.Context.SpareRequisition != null && Business.Common.Context.SpareRequisition.Rows != null && Business.Common.Context.SpareRequisition.Rows.Count > 0 &&
                    (ddlCurrentCallStatusDocket.SelectedValue == ((int)CallStatusType.DocketOpenForSpares).ToString()
                    || ddlCurrentCallStatusDocket.SelectedValue == ((int)CallStatusType.DocketFunctional).ToString()))
                {
                    serviceBook.SpareRequisition = Business.Common.Context.SpareRequisition;
                    serviceBook.ServiceBookDetails = new DataTable();
                    serviceBook.ServiceBookDetails.AcceptChanges();
                }

                long serviceBookId = objServiceBook.Service_ServiceBook_Save(serviceBook);

                CallAttendanceSave(objServiceBook, serviceBook, serviceCallAttendance, serviceBookId);

                //Manual call out is removed
                if (Request.QueryString["action"] != null && (Request.QueryString["action"].Equals("callin")))
                //if (Request.QueryString["action"] != null && (Request.QueryString["action"].Equals("callin") || Request.QueryString["action"].Equals("callout")))
                {
                    Response.Redirect(HttpContext.Current.Request.UrlReferrer.AbsoluteUri);
                }
                else
                {
                    int serviceBookDetails = 0;
                    serviceBook.ServiceBookId = serviceBookId;

                    //If low yield then removing spare entry in service book details
                    bool isLowYieldSpareChange = CheckingSpareYield(serviceBook, objServiceBook);
                    if (isLowYieldSpareChange)
                    {
                        serviceBook.ServiceBookDetails = new DataTable();
                        serviceBook.ServiceBookDetails.AcceptChanges();
                        //serviceBook.CallStatusId = (int)CallStatusType.DocketOpenForApproval;
                    }
                    else
                    {
                        if (serviceBookId > 0)
                        {
                            serviceBookDetails = objServiceBook.Service_ServiceBookDetails_Save(serviceBook);
                        }
                    }
                    if (serviceBookId > 0)
                    {
                        //updating last meter reading in Customer Purchase
                        int meterResponse = 0;
                        meterResponse = objServiceBook.Service_MeterReading_Update(serviceBook);
                        if (meterResponse > 0)
                        {
                            serviceBook.ServiceBookId = serviceBookId;
                            if (isLowYieldSpareChange)
                            {
                                MessageDocket.IsSuccess = false;
                                MessageDocket.Text = "Low yield spare change found. Your docket is under verification now.";
                            }
                            else
                            {
                                if (serviceBook.ServiceBookDetails != null && serviceBook.ServiceBookDetails.Rows.Count > 0 && serviceBookDetails == 0)
                                {
                                    MessageDocket.IsSuccess = false;
                                    MessageDocket.Text = "Cannot save spares information in service book details.";
                                }
                                else
                                {
                                    SentMail();
                                    LoadDocket();
                                    ClearDocketControls();
                                    LoadServiceBookMasterHistory();
                                    MessageDocket.IsSuccess = true;
                                    MessageDocket.Text = "Docket response successfully given.";
                                }
                            }
                        }
                        else
                        {
                            MessageDocket.IsSuccess = false;
                            MessageDocket.Text = "Current meter reading unable to update! Please contact system administrator immediately.";
                        }
                    }
                    else
                    {
                        MessageDocket.IsSuccess = false;
                        MessageDocket.Text = "Sorry! docket response not given. Please try again.";
                    }
                    MessageDocket.Show = true;
                }
            }
        }

        private void CallAttendanceSave(Business.Service.ServiceBook objServiceBook, Entity.Service.ServiceBook serviceBook, ServiceCallAttendance serviceCallAttendance, long serviceBookId)
        {
            if (serviceBookId > 0)
            {
                serviceCallAttendance.EmployeeId = serviceBook.EmployeeId_FK;
                serviceCallAttendance.ServiceBookId = serviceBookId;
                serviceCallAttendance.InTime = (Request.QueryString["action"] != null && Request.QueryString["action"].Equals("callin")) ? DateTime.Now : DateTime.MinValue;
                serviceCallAttendance.OutTime = (Request.QueryString["action"] != null && Request.QueryString["action"].Equals("callin")) ? DateTime.MinValue : DateTime.Now;
                serviceCallAttendance.CallStatusId = serviceBook.CallStatusId;
                serviceCallAttendance.Status = 1;
                objServiceBook.Service_CallAttendance_Save(serviceCallAttendance);
            }
            else
            {
                MessageDocket.IsSuccess = false;
                MessageDocket.Text = "Sorry! unable log In-Time. Please try again.";
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
                DropDownList ddlAssociatedInTimeTT = ((DropDownList)e.Row.FindControl("ddlAssociatedInTimeTT"));
                DropDownList ddlAssociatedOutTimeHH = ((DropDownList)e.Row.FindControl("ddlAssociatedOutTimeHH"));
                DropDownList ddlAssociatedOutTimeMM = ((DropDownList)e.Row.FindControl("ddlAssociatedOutTimeMM"));
                DropDownList ddlAssociatedOutTimeTT = ((DropDownList)e.Row.FindControl("ddlAssociatedOutTimeTT"));

                txtAssociatedOutDate.Text = txtAssociatedInDate.Text = System.DateTime.Now.ToString("dd MMM yyyy");
                LoadTime(ddlAssociatedInTimeHH, ddlAssociatedInTimeMM, ddlAssociatedOutTimeHH, ddlAssociatedOutTimeMM, ddlAssociatedInTimeTT, ddlAssociatedOutTimeTT);
            }
        }

        //protected void gvTonnerList_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {
        //        if (((DataTable)(gvTonnerList.DataSource)).Rows[e.Row.RowIndex]["ApprovalStatus"].ToString() == Convert.ToString((int)ApprovalStatus.Rejected))
        //        {
        //            ((CheckBox)e.Row.FindControl("chkToner")).Enabled = false;
        //            e.Row.Attributes["style"] = "background-color: #ff3f3f";
        //        }
        //    }
        //}

        protected void gvTonnerRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTonnerRequest.PageIndex = e.NewPageIndex;
            LoadTonnerRequest(e.NewPageIndex, gvTonnerRequest.PageSize);
        }
        #endregion
    }
}