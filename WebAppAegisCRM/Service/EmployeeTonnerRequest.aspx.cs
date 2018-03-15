using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections.ObjectModel;
using Entity.Service;
using Business.Common;

namespace WebAppAegisCRM.Service
{
    public partial class EmployeeTonnerRequest : System.Web.UI.Page
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

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Message.Show = false;
                    txtRequestDate.Text = DateTime.Now.ToString("dd MMM yyyy");
                    LoadCustomerforSearch();
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
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
            DataTable dt = objCustomer.Customer_Customer_GetByAssignEngineerId(customer);
            if (dt.Rows.Count > 0)
                gvCustomerMaster.DataSource = dt;
            else
                gvCustomerMaster.DataSource = null;
            gvCustomerMaster.DataBind();
        }
        protected bool ComplainValidation()
        {
            bool retValue = false;

            bool isTonerSelected = false;
            if (gvTonner.Rows.Count > 0)
            {
                foreach (GridViewRow toner in gvTonner.Rows)
                {
                    CheckBox chkToner = (CheckBox)toner.FindControl("chk1");
                    if (chkToner.Checked)
                    {
                        isTonerSelected = true;
                        break;
                    }
                }
            }
            if (!isTonerSelected)
            {
                Message.Text = "Please select toner";
                Message.Show = true;
                return false;
            }

            foreach (GridViewRow gvr in gvPurchase.Rows)
            {
                if (((CheckBox)gvr.FindControl("chk")).Checked)
                {
                    retValue = true;
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
                        Message.Text = "A toner Request is under approval. You can not request more than one.";
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
                    //        if (bool.Parse((dt.Rows[0]["Flag"].ToString() == "1") ? "True" : "False")) //True for Out of Contract
                    //        {
                    //            Message.IsSuccess = false;
                    //            Message.Text = "Out of contract! Please call customer help desk.";
                    //            Message.Show = true;
                    //            return false;
                    //        }
                    //    }
                    //}
                }
            }

            if (txtA3BWMeterReading.Text.Trim() == string.Empty
                && txtA3CLMeterReading.Text.Trim() == string.Empty
                && txtA4BWMeterReading.Text.Trim() == string.Empty
                && txtA4CLMeterReading.Text.Trim() == string.Empty)
            {
                retValue = false;
                Message.Text = "Please enter atleast one current meter reading.";
            }

            if (!string.IsNullOrEmpty(txtA3BWMeterReading.Text.Trim())
                && Convert.ToInt64(lblA3BWLastMeterReading.Text.Trim()) > Convert.ToInt64(txtA3BWMeterReading.Text.Trim()))
            {
                retValue = false;
                Message.Text = "A3 B/W current meter readings cannot less than last meter readings.";
            }

            if (!string.IsNullOrEmpty(txtA4BWMeterReading.Text.Trim())
                && Convert.ToInt64(lblA4BWLastMeterReading.Text.Trim()) > Convert.ToInt64(txtA4BWMeterReading.Text.Trim()))
            {
                retValue = false;
                Message.Text = "A4 B/W current meter readings cannot less than last meter readings.";
            }

            if (!string.IsNullOrEmpty(txtA3CLMeterReading.Text.Trim())
                && Convert.ToInt64(lblA3ClLastMeterReading.Text.Trim()) > Convert.ToInt64(txtA3CLMeterReading.Text.Trim()))
            {
                retValue = false;
                Message.Text = "A3 CL current meter readings cannot less than last meter readings.";
            }

            if (!string.IsNullOrEmpty(txtA4CLMeterReading.Text.Trim())
                && Convert.ToInt64(lblA4ClLastMeterReading.Text.Trim()) > Convert.ToInt64(txtA4CLMeterReading.Text.Trim()))
            {
                retValue = false;
                Message.Text = "A4 CL current meter readings cannot less than last meter readings.";
            }

            if (retValue == false)
            {
                Message.IsSuccess = false;
                Message.Show = true;
            }
            return retValue;
        }
        protected void LoadTonnerRequest()
        {
            Business.Service.TonerRequest objTonnerRequest = new Business.Service.TonerRequest();
            DataTable dt = objTonnerRequest.Service_TonerRequest_GetLast10();

            if (dt != null)
            {
                gvTonnerRequest.DataSource = dt;
                gvTonnerRequest.DataBind();
            }

        }
        #endregion

        protected void btnCustomerSearch_Click(object sender, EventArgs e)
        {
            LoadCustomerforSearch();
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (ComplainValidation())
            {
                Business.Service.TonerRequest objTonnerRequest = new Business.Service.TonerRequest();
                Entity.Service.TonerRequest tonnerRequest = new Entity.Service.TonerRequest();
                Business.Service.ServiceBook objServiceBook = new Business.Service.ServiceBook();
                Entity.Service.ServiceBook serviceBook = new Entity.Service.ServiceBook();

                foreach (GridViewRow gvr in gvPurchase.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chk")).Checked)
                    {
                        tonnerRequest.CustomerPurchaseId = int.Parse(gvPurchase.DataKeys[gvr.RowIndex].Values[0].ToString());
                        serviceBook.CustomerPurchaseId = int.Parse(gvPurchase.DataKeys[gvr.RowIndex].Values[0].ToString());
                    }
                }

                tonnerRequest.CustomerId = CustomerMasterId;
                tonnerRequest.RequestNo = "";
                tonnerRequest.RequestDateTime = Convert.ToDateTime(txtRequestDate.Text);
                tonnerRequest.isCustomerEntry = false;
                tonnerRequest.CallStatusId = 8;
                if (txtA3BWMeterReading.Text.Trim() == string.Empty)
                    tonnerRequest.A3BWMeterReading = null;
                else
                    tonnerRequest.A3BWMeterReading = int.Parse(txtA3BWMeterReading.Text.Trim());
                if (txtA4BWMeterReading.Text.Trim() == string.Empty)
                    tonnerRequest.A4BWMeterReading = null;
                else
                    tonnerRequest.A4BWMeterReading = int.Parse(txtA4BWMeterReading.Text.Trim());
                if (txtA3CLMeterReading.Text.Trim() == string.Empty)
                    tonnerRequest.A3CLMeterReading = null;
                else
                    tonnerRequest.A3CLMeterReading = int.Parse(txtA3CLMeterReading.Text.Trim());
                if (txtA4CLMeterReading.Text.Trim() == string.Empty)
                    tonnerRequest.A4CLMeterReading = null;
                else
                    tonnerRequest.A4CLMeterReading = int.Parse(txtA4CLMeterReading.Text.Trim());
                tonnerRequest.Remarks = txtRequest.Text.Trim();
                tonnerRequest.CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name);

                foreach (GridViewRow toner in gvTonner.Rows)
                {
                    if (((CheckBox)toner.FindControl("chk1")).Checked)
                    {
                        tonnerRequest.SpareIds.Add(Int64.Parse(gvTonner.DataKeys[toner.RowIndex].Values[0].ToString()));
                    }
                }

                if (txtA3BWMeterReading.Text.Trim() == string.Empty)
                    serviceBook.A3BWMeterReading = null;
                else
                    serviceBook.A3BWMeterReading = int.Parse(txtA3BWMeterReading.Text.Trim());
                if (txtA4BWMeterReading.Text.Trim() == string.Empty)
                    serviceBook.A4BWMeterReading = null;
                else
                    serviceBook.A4BWMeterReading = int.Parse(txtA4BWMeterReading.Text.Trim());
                if (txtA3CLMeterReading.Text.Trim() == string.Empty)
                    serviceBook.A3CLMeterReading = null;
                else
                    serviceBook.A3CLMeterReading = int.Parse(txtA3CLMeterReading.Text.Trim());
                if (txtA4CLMeterReading.Text.Trim() == string.Empty)
                    serviceBook.A4CLMeterReading = null;
                else
                    serviceBook.A4CLMeterReading = int.Parse(txtA4CLMeterReading.Text.Trim());

                DataTable dtTonnerRequest = objTonnerRequest.Service_TonerRequest_Save(tonnerRequest);
                bool isTonerLowYield = objTonnerRequest.Service_TonerLowYieldCheck(tonnerRequest);
                int meterUpdateResponse = objServiceBook.Service_MeterReading_Update(serviceBook);

                if (dtTonnerRequest.Rows.Count > 0 && meterUpdateResponse > 0)
                {
                    string message = "Toner request received. Your request no : " + dtTonnerRequest.Rows[0]["TonnerRequestNo"].ToString() + ". ";

                    /* Checking whether machine is in contract or not*/
                    Business.Service.Contract objContract = new Business.Service.Contract();
                    if (objContract.Service_MachineIsInContractCheck(CustomerPurchaseId) || isTonerLowYield) //Out of Contract AND Low Yield
                    {
                        int approvalResponse = Approval_Save(tonnerRequest, dtTonnerRequest);
                        if (approvalResponse > 0)
                        {
                            //Appending low toner warning
                            message += Server.HtmlDecode("<span style='color:red'>Warning: Your request is under verification</span>");
                            //SentNotification(objServiceBook, dtTonnerRequest);
                        }
                    }
                    ResetControls(dtTonnerRequest);
                    Message.IsSuccess = true;
                    Message.Text = message;
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Sorry! we can not receive your request. Please try again.";
                }
                Message.Show = true;
            }
        }

        private static int Approval_Save(Entity.Service.TonerRequest tonnerRequest, DataTable dtTonnerRequest)
        {
            int approvalResponse = 0;
            Business.Service.ServiceBook objServiceBook = new Business.Service.ServiceBook();
            Entity.Service.ServiceBook serviceBook = new Entity.Service.ServiceBook();

            using (DataTable dtApproval = new DataTable())
            {
                dtApproval.Columns.Add("ApprovalId");
                dtApproval.Columns.Add("ServiceBookId");
                dtApproval.Columns.Add("ItemId");
                dtApproval.Columns.Add("ApprovalStatus");
                dtApproval.Columns.Add("RespondBy");
                dtApproval.Columns.Add("Comment");
                foreach (Int64 tonerId in tonnerRequest.SpareIds)
                {
                    DataRow drApprovalItem = dtApproval.NewRow();
                    drApprovalItem["ApprovalId"] = 0;
                    drApprovalItem["ServiceBookId"] = dtTonnerRequest.Rows[0]["ServiceBookId"].ToString();
                    drApprovalItem["ItemId"] = tonerId;
                    drApprovalItem["ApprovalStatus"] = (int)ApprovalStatus.None;
                    drApprovalItem["RespondBy"] = string.Empty;
                    drApprovalItem["Comment"] = "NEED TONER APPROVAL";
                    dtApproval.Rows.Add(drApprovalItem);
                    dtApproval.AcceptChanges();
                }
                serviceBook.ApprovalItems = dtApproval;
                serviceBook.ApprovalItems.AcceptChanges();
                approvalResponse = objServiceBook.Service_ServiceBookDetailsApproval_Save(serviceBook);
                return approvalResponse;
            }
        }

        private void ResetControls(DataTable dtTonnerRequest)
        {
            LoadTonnerRequest();
            LoadCustomerPurchaseList();

            gvTonner.DataSource = null;
            gvTonner.DataBind();

            TonnerRequestId = Convert.ToInt32(dtTonnerRequest.Rows[0]["TonnerRequestId"].ToString());
            txtA3BWMeterReading.Text = "";
            txtA4BWMeterReading.Text = "";
            txtA3CLMeterReading.Text = "";
            txtA4CLMeterReading.Text = "";
            txtRequest.Text = "";
            txtRequestDate.Text = DateTime.Now.ToString("dd MMM yyyy");
        }

        private void SentNotification(Business.Service.ServiceBook objServiceBook, DataTable dtTonnerRequest)
        {
            //Checking whether low tonner request, if yes then send notification mail
            using (DataSet ds = objServiceBook.Service_Tonner_GetByTonnerRequestId(TonnerRequestId))
            {
                string A3BWCurrentMeterReading = "",
                        A3CLCurrentMeterReading = "",
                        A4CLCurrentMeterReading = "",
                        A4BWCurrentMeterReading = "",
                        A3BWLastMeterReading = "",
                        A3CLLastMeterReading = "",
                        A4CLLastMeterReading = "",
                        A4BWLastMeterReading = "",
                        TonnerName = "",
                        Yield = "",
                        TonnerRequestNo = "";

                if (ds != null)
                {
                    TonnerRequestNo = dtTonnerRequest.Rows[0]["TonnerRequestNo"].ToString();
                    TonnerName = (ds.Tables[0].Rows[0]["SpareName"] == DBNull.Value) ? "" : ds.Tables[0].Rows[0]["SpareName"].ToString();
                    Yield = (ds.Tables[0].Rows[0]["Yield"] == DBNull.Value) ? "" : ds.Tables[0].Rows[0]["Yield"].ToString();
                    A3BWCurrentMeterReading = (ds.Tables[0].Rows[0]["A3BWCurrentMeterReading"] == DBNull.Value) ? "0" : ds.Tables[0].Rows[0]["A3BWCurrentMeterReading"].ToString();
                    A3CLCurrentMeterReading = (ds.Tables[0].Rows[0]["A3CLCurrentMeterReading"] == DBNull.Value) ? "0" : ds.Tables[0].Rows[0]["A3CLCurrentMeterReading"].ToString();
                    A4CLCurrentMeterReading = (ds.Tables[0].Rows[0]["A4CLCurrentMeterReading"] == DBNull.Value) ? "0" : ds.Tables[0].Rows[0]["A4CLCurrentMeterReading"].ToString();
                    A4BWCurrentMeterReading = (ds.Tables[0].Rows[0]["A4BWCurrentMeterReading"] == DBNull.Value) ? "0" : ds.Tables[0].Rows[0]["A4BWCurrentMeterReading"].ToString();
                    A3BWLastMeterReading = (ds.Tables[1].Rows.Count == 0 || ds.Tables[1].Rows[0]["A3BWLastMeterReading"] == DBNull.Value) ? "0" : ds.Tables[1].Rows[0]["A3BWLastMeterReading"].ToString();
                    A4BWLastMeterReading = (ds.Tables[1].Rows.Count == 0 || ds.Tables[1].Rows[0]["A4BWLastMeterReading"] == DBNull.Value) ? "0" : ds.Tables[1].Rows[0]["A4BWLastMeterReading"].ToString();
                    A3CLLastMeterReading = (ds.Tables[1].Rows.Count == 0 || ds.Tables[1].Rows[0]["A3CLLastMeterReading"] == DBNull.Value) ? "0" : ds.Tables[1].Rows[0]["A3CLLastMeterReading"].ToString();
                    A4CLLastMeterReading = (ds.Tables[1].Rows.Count == 0 || ds.Tables[1].Rows[0]["A4CLLastMeterReading"] == DBNull.Value) ? "0" : ds.Tables[1].Rows[0]["A4CLLastMeterReading"].ToString();
                }

                //Mail body
                StringBuilder sb = new StringBuilder();
                sb.Append("<div style='width: 99%; font-family: Cambria, Georgia, serif; color: #565656; margin:10px'>");
                sb.Append("<center><img src='http://aegiscrm.in/images/Aegis_CRM_Logo.png' alt='AEGIS CRM' />");
                sb.Append("<h2>Aegis Customer Relationship Management Notification</h2><br /></center>");
                sb.Append("Date:- " + DateTime.Now.ToString("dd/MM/yyyy") + "<br /><br />");
                sb.Append("Dear Aegis CRM Admin,<br />");
                sb.Append("A customer/employee has made a low tonner request and waiting for approval.<br /><br />");
                sb.Append("<b><u>Low Toner Request Details</u>:<br /><br />");
                sb.Append("Toner Request No - " + TonnerRequestNo + "<br />");
                sb.Append("Customer Name - " + CustomerName + "<br />");
                sb.Append("Request Date and Time - " + System.DateTime.Now.ToString("dd/MM/yyyy hh:mm tt") + "<br />");
                sb.Append("A3 B/W Current Meter Reading - " + A3BWCurrentMeterReading + "<br />");
                sb.Append("A3 CL Current Meter Reading - " + A3CLCurrentMeterReading + "<br />");
                sb.Append("A4 B/W Current Meter Reading - " + A4BWCurrentMeterReading + "<br />");
                sb.Append("A4 CL Current Meter Reading - " + A4CLCurrentMeterReading + "<br />");
                sb.Append("A3 B/W Last Toner Reading - " + A3BWLastMeterReading + "<br />");
                sb.Append("A3 CL Last Toner Reading - " + A3CLLastMeterReading + "<br />");
                sb.Append("A4 B/W Last Toner Reading - " + A4BWLastMeterReading + "<br />");
                sb.Append("A4 CL Last Toner Reading - " + A4CLLastMeterReading + "<br />");
                sb.Append("Model Name - " + ModelName + "<br />");
                sb.Append("Machine ID - " + MachineId + "<br />");
                sb.Append("Toner Name - " + TonnerName + "<br />");
                sb.Append("Toner Yield- " + Yield + "</b><br /><br />");
                sb.Append("Click to login into portal <a href='http://aegiscrm.in'>aegiscrm.in</a><br /><br />");
                sb.Append("<hr />");
                sb.Append("<center tyle='color:#C68E17'>*** This is a system generated mail. Please do not reply. ***</center>");
                sb.Append("</div>");

                //string fromMail = "", toMail = "", password = "", subject = "";
                //fromMail = "support@aegiscrm.in";
                //password = "P@ssw0rd";
                //toMail = "support@aegissolutions.in";
                //subject = "Low Toner Request Notification";

                //Business.Common.MailFunctionality.SendMail_HostingRaja(fromMail, toMail, password, subject, sb.ToString());
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

            if (chk.Checked)
            {
                //ContractTypeId = int.Parse(gvPurchase.DataKeys[gv.RowIndex].Values[1].ToString());

                ModelName = gv.Cells[3].Text;
                MachineId = gv.Cells[4].Text;

                Business.Service.TonerRequest ObjBl = new Business.Service.TonerRequest();
                DataTable dt = ObjBl.Service_Toner_GetAllByCustomerId(Int64.Parse(gvPurchase.DataKeys[gv.RowIndex].Values[0].ToString()));
                if (dt != null)
                {
                    gvTonner.DataSource = dt;
                    gvTonner.DataBind();
                }

                Business.Service.ServiceBook objServiceBook = new Business.Service.ServiceBook();
                DataSet dsLastMeterReading = objServiceBook.Service_GetLastMeterReadingByCustomerPurchaseId(Int64.Parse(gvPurchase.DataKeys[gv.RowIndex].Values[0].ToString()));
                lblA3BWLastMeterReading.Text = dsLastMeterReading.Tables[0].Rows[0]["A3BWLastMeterReading"].ToString();
                lblA3ClLastMeterReading.Text = dsLastMeterReading.Tables[0].Rows[0]["A3CLLastMeterReading"].ToString();
                lblA4BWLastMeterReading.Text = dsLastMeterReading.Tables[0].Rows[0]["A4BWLastMeterReading"].ToString();
                lblA4ClLastMeterReading.Text = dsLastMeterReading.Tables[0].Rows[0]["A4CLLastMeterReading"].ToString();
            }
            else
            {
                ContractTypeId = 0;
                gvTonner.DataBind();
            }
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
                    CustomerName = gv.Cells[3].Text;
                    CustomerMasterId = int.Parse(gvCustomerMaster.DataKeys[gvr.RowIndex].Values[0].ToString());
                    LoadCustomerPurchaseList();
                    LoadTonnerRequest();
                }
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