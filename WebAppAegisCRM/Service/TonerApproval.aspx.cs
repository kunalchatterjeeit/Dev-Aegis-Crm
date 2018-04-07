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
    public partial class TonerApproval : System.Web.UI.Page
    {
        private void Service_ServiceBookDetailsApproval_GetAll()
        {
            Business.Service.ServiceBook objServiceBook = new Business.Service.ServiceBook();
            Entity.Service.ServiceBook serviceBook = new Entity.Service.ServiceBook()
            {
                MachineId = txtMachineId.Text.Trim(),
                FromDate = (!string.IsNullOrEmpty(txtLogFromDate.Text.Trim()) ? Convert.ToDateTime(txtLogFromDate.Text.Trim()) : DateTime.MinValue),
                ToDate = (!string.IsNullOrEmpty(txtLogToDate.Text.Trim()) ? Convert.ToDateTime(txtLogToDate.Text.Trim()) : DateTime.MinValue),
                ApprovalStatus = Convert.ToInt32(ddlApprovalStatus.SelectedValue),
                CallType = 1
            };

            DataTable dt = objServiceBook.Service_ServiceBookDetailsApproval_GetAll(serviceBook);
            gvApproval.DataSource = dt;
            gvApproval.DataBind();
        }
        protected void gvApproval_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Business.Service.ServiceBook objServiceBook = new Business.Service.ServiceBook();
            Entity.Service.ServiceBook serviceBook = new Entity.Service.ServiceBook();

            int approvalId = int.Parse(e.CommandArgument.ToString());
            int approvalResponse = 0;
            GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
            string comment = ((TextBox)row.FindControl("txtComment")).Text.Trim();
            using (DataTable dtApproval = new DataTable())
            {
                dtApproval.Columns.Add("ApprovalId");
                dtApproval.Columns.Add("ServiceBookId");
                dtApproval.Columns.Add("ItemId");
                dtApproval.Columns.Add("ApprovalStatus");
                dtApproval.Columns.Add("RespondBy");
                dtApproval.Columns.Add("Comment");

                DataRow drApprovalItem = dtApproval.NewRow();
                drApprovalItem["ApprovalId"] = approvalId;
                drApprovalItem["ServiceBookId"] = 0;
                drApprovalItem["ItemId"] = 0;
                drApprovalItem["ApprovalStatus"] = (e.CommandName == "Approve") ? (int)ApprovalStatus.Approved : (int)ApprovalStatus.Rejected;
                drApprovalItem["RespondBy"] = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                drApprovalItem["Comment"] = comment;
                dtApproval.Rows.Add(drApprovalItem);
                dtApproval.AcceptChanges();

                serviceBook.ApprovalItems = dtApproval;
                serviceBook.ApprovalItems.AcceptChanges();
                approvalResponse = objServiceBook.Service_ServiceBookDetailsApproval_Save(serviceBook);

                if (approvalResponse > 0)
                {
                    Service_ServiceBookDetailsApproval_GetAll();
                    Message.IsSuccess = true;
                    Message.Text = "Toner response has been given.";
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Toner response failed.";
                }
                Message.Show = true;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Message.Show = false;
                Service_ServiceBookDetailsApproval_GetAll();
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Service_ServiceBookDetailsApproval_GetAll();
        }
    }
}