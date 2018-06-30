using Business.Common;
using Entity.Service;
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
        public int CustomerPurchaseId
        {
            get { return Convert.ToInt32(ViewState["CustomerPurchaseId"]); }
            set { ViewState["CustomerPurchaseId"] = value; }
        }
        
        public long TonnerRequestId
        {
            get { return Convert.ToInt64(ViewState["TonnerRequestId"]); }
            set { ViewState["TonnerRequestId"] = value; }
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
                LoadCustomerPurchaseList();
                LoadTonnerRequest();
            }
        }

        #region User Defined Funtions
        protected void LoadCustomerPurchaseList()
        {
            Business.Customer.Customer objCustomerMaster = new Business.Customer.Customer();
            Entity.Customer.Customer customerMaster = new Entity.Customer.Customer();
            gvPurchase.DataSource = objCustomerMaster.CustomerPurchase_GetByCustomerId(int.Parse(HttpContext.Current.User.Identity.Name.Split('|')[(int)Constants.Customer.ID]));
            gvPurchase.DataBind();
        }
        protected bool ComplainValidation()
        {
            bool flag = false;

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

                tonnerRequest.CustomerId = int.Parse(HttpContext.Current.User.Identity.Name.Split('|')[(int)Constants.Customer.ID]); ;
                tonnerRequest.RequestNo = "";
                tonnerRequest.RequestDateTime = DateTime.Now;
                tonnerRequest.isCustomerEntry = true;
                tonnerRequest.CallStatusId = (int)CallStatusType.TonerRequestInQueue;
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
                tonnerRequest.CreatedBy = int.Parse(HttpContext.Current.User.Identity.Name.Split('|')[(int)Constants.Customer.ID]);

                foreach (GridViewRow toner in gvTonner.Rows)
                {
                    if (((CheckBox)toner.FindControl("chk1")).Checked)
                    {
                        tonnerRequest.SpareIds.Add(new TonerIdQuantity
                        {
                            TonerId = long.Parse(gvTonner.DataKeys[toner.RowIndex].Values[0].ToString()),
                            Quantity = int.Parse(((TextBox)toner.FindControl("txtRequisiteQty")).Text)
                        });
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
                    int approvalResponse = Approval_Save(tonnerRequest, dtTonnerRequest, isTonerLowYield);

                    if (!objContract.Service_MachineIsInContractCheck(CustomerPurchaseId) || isTonerLowYield) //Out of Contract AND Low Yield
                    {
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
        }

        private static int Approval_Save(Entity.Service.TonerRequest tonnerRequest, DataTable dtTonnerRequest, bool isLowYield)
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
                dtApproval.Columns.Add("IsLowYield");
                dtApproval.Columns.Add("CallStatus");
                dtApproval.Columns.Add("RespondBy");
                dtApproval.Columns.Add("Comment");
                dtApproval.Columns.Add("RequisiteQty");
                foreach (TonerIdQuantity item in tonnerRequest.SpareIds)
                {
                    DataRow drApprovalItem = dtApproval.NewRow();
                    drApprovalItem["ApprovalId"] = 0;
                    drApprovalItem["ServiceBookId"] = dtTonnerRequest.Rows[0]["ServiceBookId"].ToString();
                    drApprovalItem["ItemId"] = item.TonerId;
                    drApprovalItem["ApprovalStatus"] = (isLowYield) ? (int)ApprovalStatus.None : (int)ApprovalStatus.Approved;
                    drApprovalItem["IsLowYield"] = isLowYield;
                    drApprovalItem["CallStatus"] = (isLowYield) ? (int)CallStatusType.TonerOpenForApproval : (int)CallStatusType.TonerRequestInQueue;
                    drApprovalItem["RespondBy"] = string.Empty;
                    drApprovalItem["Comment"] = (isLowYield) ? "NEED TONER APPROVAL" : "AUTO APPROVED";
                    drApprovalItem["RequisiteQty"] = item.Quantity;
                    dtApproval.Rows.Add(drApprovalItem);
                    dtApproval.AcceptChanges();
                }
                serviceBook.ApprovalItems = dtApproval;
                serviceBook.ApprovalItems.AcceptChanges();
                approvalResponse = objServiceBook.Service_ServiceBookDetailsApproval_Save(serviceBook);
                return approvalResponse;
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
                Business.Service.TonerRequest ObjBl = new Business.Service.TonerRequest();
                DataTable dt = ObjBl.Service_Toner_GetAllByCustomerId(long.Parse(gvPurchase.DataKeys[gv.RowIndex].Values[0].ToString()));
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