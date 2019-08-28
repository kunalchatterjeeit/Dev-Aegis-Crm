using Business.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.ClaimManagement
{
    public partial class ClaimReport : System.Web.UI.Page
    {
        private void Status_GetAll()
        {
            Business.ClaimManagement.ClaimStatus objClaimStatus = new Business.ClaimManagement.ClaimStatus();
            DataTable dt = objClaimStatus.ClaimStatus_GetAll(new Entity.ClaimManagement.ClaimStatus() { });

            ddlStatus.DataSource = dt;
            ddlStatus.DataTextField = "StatusName";
            ddlStatus.DataValueField = "ClaimStatusId";
            ddlStatus.DataBind();
            ddlStatus.InsertSelect();
        }

        private void EmployeeMaster_GetAll()
        {
            Business.HR.EmployeeMaster ObjBelEmployeeMaster = new Business.HR.EmployeeMaster();
            Entity.HR.EmployeeMaster ObjElEmployeeMaster = new Entity.HR.EmployeeMaster();
            ObjElEmployeeMaster.CompanyId_FK = 1;
            DataTable dt = ObjBelEmployeeMaster.Employee_GetAll_Active(ObjElEmployeeMaster);

            ddlEmployee.DataSource = dt;
            ddlEmployee.DataTextField = "EmployeeName";
            ddlEmployee.DataValueField = "EmployeeMasterId";
            ddlEmployee.DataBind();
            ddlEmployee.InsertSelect();
        }

        private void ClaimApplication_GetAll()
        {
            Entity.ClaimManagement.ClaimApplicationMaster ClaimApplicationMaster = new Entity.ClaimManagement.ClaimApplicationMaster();
            ClaimApplicationMaster.EmployeeId = Convert.ToInt32(ddlEmployee.SelectedValue);
            ClaimApplicationMaster.PeriodFrom = (string.IsNullOrEmpty(txtFromDate.Text.Trim())) ? DateTime.MinValue : Convert.ToDateTime(txtFromDate.Text.Trim());
            ClaimApplicationMaster.PeriodTo = (string.IsNullOrEmpty(txtToDate.Text.Trim())) ? DateTime.MinValue : Convert.ToDateTime(txtToDate.Text.Trim());
            ClaimApplicationMaster.Status = Convert.ToInt32(ddlStatus.SelectedValue);
            Business.ClaimManagement.ClaimApplication objClaimApplication = new Business.ClaimManagement.ClaimApplication();
            DataTable dtClaimApplication = objClaimApplication.ClaimApplication_GetAll(ClaimApplicationMaster);
            if (dtClaimApplication != null)
            {
                gvClaimReport.DataSource = dtClaimApplication;
                gvClaimReport.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Status_GetAll();
                EmployeeMaster_GetAll();
                ClaimApplication_GetAll();
                Message.Show = false;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ClaimApplication_GetAll();
        }

        protected void gvClaimReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvClaimReport.PageIndex = e.NewPageIndex;
            ClaimApplication_GetAll();
        }

        protected void gvClaimReport_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                Business.Common.Context.ClaimApplicationId = Convert.ToInt32(e.CommandArgument.ToString());
                GetClaimApplicationDetails_ByClaimApplicationId(Business.Common.Context.ClaimApplicationId);
                ModalPopupExtender1.Show();
            }

        }

        private void GetClaimApplicationDetails_ByClaimApplicationId(int ClaimApplicationId)
        {
            DataSet dsClaimApplicationDetails = new Business.ClaimManagement.ClaimApplication().GetClaimApplicationDetails_ByClaimApplicationId(ClaimApplicationId);
            if (dsClaimApplicationDetails != null)
            {
                lblClaimApplicationNumber.Text = dsClaimApplicationDetails.Tables[0].Rows[0]["ClaimNo"].ToString();
                lblName.Text = dsClaimApplicationDetails.Tables[0].Rows[0]["Requestor"].ToString();
                lblFromDate.Text = dsClaimApplicationDetails.Tables[0].Rows[0]["FromDate"].ToString();
                lblToDate.Text = dsClaimApplicationDetails.Tables[0].Rows[0]["ToDate"].ToString();
                lblTotalClaimCount.Text = dsClaimApplicationDetails.Tables[0].Rows[0]["TotalAmount"].ToString();
                lblClaimHeader.Text = dsClaimApplicationDetails.Tables[0].Rows[0]["ClaimHeading"].ToString();
            }

            gvClaimDetails.DataSource = dsClaimApplicationDetails.Tables[2];
            gvClaimDetails.DataBind();

            ComputeTotalApprovedAmount();

            if (dsClaimApplicationDetails.Tables.Count > 1)
            {
                gvApprovalHistory.DataSource = dsClaimApplicationDetails.Tables[1];
                gvApprovalHistory.DataBind();
            }
        }

        private void ComputeTotalApprovedAmount()
        {
            decimal total = 0;
            foreach (GridViewRow gvr in gvClaimDetails.Rows)
            {
                if (!string.IsNullOrEmpty(gvr.Cells[5].Text.ToString()) && !gvr.Cells[5].Text.Equals("&nbsp;"))
                {
                    total += Convert.ToDecimal(gvr.Cells[5].Text.ToString());
                }
            }
            lblTotalApprovedAmount.Text = total.ToString();
        }

        protected void gvClaimDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    LinkButton lnkBtnAttachment = (LinkButton)e.Row.FindControl("lnkBtnAttachment");
                    if (!string.IsNullOrEmpty(((DataTable)gvClaimDetails.DataSource).Rows[e.Row.RowIndex]["Attachment"].ToString()))
                    {
                        lnkBtnAttachment.CssClass = "fa fa-paperclip fa-fw";
                        lnkBtnAttachment.Enabled = true;
                        lnkBtnAttachment.ToolTip = "Click to download";
                    }
                    else
                    {
                        lnkBtnAttachment.CssClass = "fa fa-exclamation-triangle fa-fw";
                        lnkBtnAttachment.Enabled = false;
                        lnkBtnAttachment.ToolTip = "No attachment";
                    }

                    DropDownList ddlLineItemStatus = (DropDownList)e.Row.FindControl("ddlLineItemStatus");
                    LoadClaimStatus(ddlLineItemStatus);
                    ddlLineItemStatus.SelectedValue = ((DataTable)(gvClaimDetails.DataSource)).Rows[e.Row.RowIndex]["Status"].ToString();

                    HiddenField hdnChecked = (HiddenField)e.Row.FindControl("hdnChecked");
                    if (!string.IsNullOrEmpty(((DataTable)gvClaimDetails.DataSource).Rows[e.Row.RowIndex]["ApprovedAmount"].ToString()))
                    {
                        hdnChecked.Value = "Checked";
                        e.Row.Attributes.CssStyle.Add("color", "#038a10");
                        e.Row.Font.Italic = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
            }
        }

        private void LoadClaimStatus(DropDownList ddlLineItemStatus)
        {
            ddlLineItemStatus.DataSource = new Business.ClaimManagement.ClaimStatus().ClaimStatus_GetAll(
                new Entity.ClaimManagement.ClaimStatus() { });
            ddlLineItemStatus.DataTextField = "StatusName";
            ddlLineItemStatus.DataValueField = "ClaimStatusId";
            ddlLineItemStatus.DataBind();
        }

        protected void gvClaimDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "A")
                {
                    string claimAttachmentName = e.CommandArgument.ToString();
                    DownloadAttachment(claimAttachmentName);
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
            finally
            {
                ModalPopupExtender1.Show();
            }
        }

        private void DownloadAttachment(string claimAttachmentName)
        {
            try
            {
                string FileName = claimAttachmentName;
                string OriginalFileName = claimAttachmentName;
                string FilePath = Server.MapPath(" ") + "\\ClaimAttachments\\" + FileName;
                FileInfo file = new FileInfo(FilePath);
                if (file.Exists)
                {
                    Response.ContentType = ContentType;
                    Response.AppendHeader("Content-Disposition", "attachment; filename=" + OriginalFileName);
                    Response.Headers.Set("Cache-Control", "private, max-age=0");
                    Response.WriteFile(FilePath);
                    Response.End();
                }
            }
            catch
            {
                // do nothing
            }
        }
    }
}