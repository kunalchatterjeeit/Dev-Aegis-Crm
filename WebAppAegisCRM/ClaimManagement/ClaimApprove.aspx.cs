using Business.Common;
using Entity.Common;
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
    public partial class ClaimApprove : System.Web.UI.Page
    {
        private void GetClaimApplications_ByApproverId(int statusId)
        {
            DateTime fromApplicationDate = string.IsNullOrEmpty(txtFromClaimDate.Text.Trim()) ? DateTime.MinValue : Convert.ToDateTime(txtFromClaimDate.Text.Trim());
            DateTime toApplicationDate = string.IsNullOrEmpty(txtToClaimDate.Text.Trim()) ? DateTime.MinValue : Convert.ToDateTime(txtToClaimDate.Text.Trim());
            DataTable dtClaimApplicationMaster =
                new Business.ClaimManagement.ClaimApprovalDetails()
                .GetClaimApplications_ByApproverId(Convert.ToInt32(HttpContext.Current.User.Identity.Name), statusId, fromApplicationDate, toApplicationDate);
            gvClaimApprovalList.DataSource = dtClaimApplicationMaster;
            gvClaimApprovalList.DataBind();
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
                txtClaimHeader.Text = dsClaimApplicationDetails.Tables[0].Rows[0]["ClaimHeading"].ToString();
                txtAdvanceAdjustAmount.Text = dsClaimApplicationDetails.Tables[0].Rows[0]["AdjustRequestAmount"].ToString();

                GetClaimAccountBalance(Convert.ToInt32(dsClaimApplicationDetails.Tables[0].Rows[0]["EmployeeId"].ToString()));
            }

            gvClaimDetails.DataSource = dsClaimApplicationDetails.Tables[2];
            gvClaimDetails.DataBind();

            ComputeTotalApprovedAmount();

            if ((Convert.ToInt32(dsClaimApplicationDetails.Tables[0].Rows[0]["Status"].ToString()) == (int)ClaimStatusEnum.Approved) &&
                Convert.ToDateTime(dsClaimApplicationDetails.Tables[0].Rows[0]["FromDate"].ToString()).Date >= DateTime.Now.Date)
            {
                btnApprove.Visible = false;
                btnReject.Visible = false;
            }
            else if (Convert.ToInt32(dsClaimApplicationDetails.Tables[0].Rows[0]["Status"].ToString()) == (int)ClaimStatusEnum.Pending)
            {
                btnApprove.Visible = true;
                btnReject.Visible = true;
            }
            else
            {
                btnApprove.Visible = false;
                btnReject.Visible = false;
            }

            if (dsClaimApplicationDetails.Tables.Count > 1)
            {
                gvApprovalHistory.DataSource = dsClaimApplicationDetails.Tables[1];
                gvApprovalHistory.DataBind();
            }
        }

        private bool ClaimApprovalValidation()
        {
            if (Convert.ToDecimal(txtAdvanceAdjustAmount.Text.Trim()) > Convert.ToDecimal(lblTotalApprovedAmount.Text.Trim()))
            {
                Message.IsSuccess = false;
                Message.Text = "Adjustment amount cannot be more than approved amount.";
                Message.Show = true;
                return false;
            }
            if (string.IsNullOrEmpty(txtRemarks.Text.Trim()))
            {
                Message.IsSuccess = false;
                Message.Text = "Please enter remarks.";
                Message.Show = true;
                return false;
            }

            foreach (GridViewRow gvr in gvClaimDetails.Rows)
            {
                HiddenField hdnChecked = (HiddenField)gvr.FindControl("hdnChecked");
                if (string.IsNullOrEmpty(hdnChecked.Value))
                {
                    Message.IsSuccess = false;
                    Message.Text = "Please check and save all claim line items.";
                    Message.Show = true;
                    return false;
                }
            }
            return true;
        }

        private void GetClaimAccountBalance(int requestorId)
        {
            lblAdvanceBalance.Text = Convert.ToString(new Business.ClaimManagement.ClaimDisbursement()
                .GetClaimAccountBalance(requestorId));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetClaimApplications_ByApproverId((int)ClaimStatusEnum.Pending);
                Message.Show = false;
                MessageSuccess.Show = false;
            }
        }

        protected void gvClaimApprovalList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                Business.Common.Context.ClaimApplicationId = Convert.ToInt32(e.CommandArgument.ToString());
                GetClaimApplicationDetails_ByClaimApplicationId(Business.Common.Context.ClaimApplicationId);
                TabContainer1.ActiveTab = Approval;
                ModalPopupExtender1.Show();
            }
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (ClaimApprovalValidation())
                {
                    Entity.ClaimManagement.ClaimApprovalDetails ClaimApprovalDetails = new Entity.ClaimManagement.ClaimApprovalDetails();
                    ClaimApprovalDetails.ApproverId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    ClaimApprovalDetails.ClaimApplicationId = Business.Common.Context.ClaimApplicationId;
                    ClaimApprovalDetails.Status = (int)ClaimStatusEnum.Approved;
                    ClaimApprovalDetails.Remarks = txtRemarks.Text.Trim();
                    int response = new Business.ClaimManagement.ClaimApprovalDetails().Claim_Approve(ClaimApprovalDetails);

                    if (response > 0)
                    {
                        //Fetching Requestor Id
                        Entity.ClaimManagement.ClaimApplicationMaster ClaimApplicationMaster = new Entity.ClaimManagement.ClaimApplicationMaster();
                        Business.ClaimManagement.ClaimApplication objClaimApplication = new Business.ClaimManagement.ClaimApplication();
                        ClaimApplicationMaster.ClaimApplicationId = Business.Common.Context.ClaimApplicationId;
                        DataTable dtClaimApplication = objClaimApplication.ClaimApplication_GetAll(ClaimApplicationMaster);

                        Business.ClaimManagement.ClaimApprovalConfiguration objClaimApprovalConfiguration = new Business.ClaimManagement.ClaimApprovalConfiguration();
                        DataTable dtClaimEmployeeWiseApprovalConfiguration = objClaimApprovalConfiguration.ClaimEmployeeWiseApprovalConfiguration_GetAll(
                            new Entity.ClaimManagement.ClaimApprovalConfiguration()
                            {
                                EmployeeId = (dtClaimApplication != null && dtClaimApplication.AsEnumerable().Any()) ? Convert.ToInt32(dtClaimApplication.Rows[0]["EmployeeId"].ToString()) : 0
                            });

                        int currentClaimApproverLevel = 0;
                        if (dtClaimEmployeeWiseApprovalConfiguration != null
                           && dtClaimEmployeeWiseApprovalConfiguration.AsEnumerable().Any()
                           && dtClaimEmployeeWiseApprovalConfiguration.Select("ApproverId = " + HttpContext.Current.User.Identity.Name).Any())
                        {
                            //Fetching the current approver approval level
                            currentClaimApproverLevel = Convert.ToInt32(dtClaimEmployeeWiseApprovalConfiguration
                                .Select("ApproverId = " + HttpContext.Current.User.Identity.Name).FirstOrDefault()["ApprovalLevel"].ToString());
                            currentClaimApproverLevel = currentClaimApproverLevel + 1;
                        }
                        if (dtClaimEmployeeWiseApprovalConfiguration != null
                       && dtClaimEmployeeWiseApprovalConfiguration.AsEnumerable().Any()
                       && dtClaimEmployeeWiseApprovalConfiguration.Select("ApprovalLevel = " + currentClaimApproverLevel).Any())
                        {
                            Business.ClaimManagement.ClaimApprovalDetails objClaimApprovalDetails = new Business.ClaimManagement.ClaimApprovalDetails();

                            ClaimApprovalDetails.ClaimApprovalDetailId = 0;
                            ClaimApprovalDetails.ClaimApplicationId = Business.Common.Context.ClaimApplicationId;
                            ClaimApprovalDetails.Status = (int)ClaimStatusEnum.Pending;
                            ClaimApprovalDetails.ApproverId = Convert.ToInt32(dtClaimEmployeeWiseApprovalConfiguration.Select("ApprovalLevel = " + currentClaimApproverLevel).FirstOrDefault()["ApproverId"].ToString());
                            //Assigning Claim Request to next approver
                            int approvalResponse = objClaimApprovalDetails.ClaimApprovalDetails_Save(ClaimApprovalDetails);
                            if (approvalResponse > 0)
                            {
                                GetClaimApplications_ByApproverId(ckShowAll.Checked ? (int)ClaimStatusEnum.None : (int)ClaimStatusEnum.Pending);
                                MessageSuccess.IsSuccess = true;
                                MessageSuccess.Text = "Approval moved to next level.";
                                MessageSuccess.Show = true;
                                ModalPopupExtender1.Hide();
                            }
                        }
                        else
                        {
                            //If final Appoval approved then update status in Master table
                            new Business.ClaimManagement.ClaimApplication().ClaimApplicationMaster_Save(
                                                                                new Entity.ClaimManagement.ClaimApplicationMaster()
                                                                                {
                                                                                    ClaimApplicationId = Business.Common.Context.ClaimApplicationId,
                                                                                    Status = (int)ClaimStatusEnum.Approved,
                                                                                    ApprovedAmount = Convert.ToDecimal(lblTotalApprovedAmount.Text.Trim())
                                                                                });

                            GetClaimApplications_ByApproverId(ckShowAll.Checked ? (int)ClaimStatusEnum.None : (int)ClaimStatusEnum.Pending);
                            MessageSuccess.IsSuccess = true;
                            MessageSuccess.Text = "Claim approved.";
                            MessageSuccess.Show = true;
                            ModalPopupExtender1.Hide();
                        }
                        Claim_Heading_Update(Business.Common.Context.ClaimApplicationId, txtClaimHeader.Text.Trim());
                        Claim_Adjust_Update();
                    }
                    else
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Response not given.";
                        Message.Show = true;

                        TabContainer1.ActiveTab = Approval;
                        ModalPopupExtender1.Show();
                    }
                }
                else
                {
                    TabContainer1.ActiveTab = Approval;
                    ModalPopupExtender1.Show();
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }

        private int Claim_Adjust_Update()
        {
            return new Business.ClaimManagement.ClaimApplication().Claim_AdjustAmount_Update(new Entity.ClaimManagement.ClaimApplicationMaster()
            {
                ClaimApplicationId = Business.Common.Context.ClaimApplicationId,
                AdjustRequestAmount = Convert.ToDecimal(txtAdvanceAdjustAmount.Text.Trim())
            });
        }

        protected void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                if (ClaimApprovalValidation())
                {
                    Entity.ClaimManagement.ClaimApprovalDetails ClaimApprovalDetails = new Entity.ClaimManagement.ClaimApprovalDetails();
                    ClaimApprovalDetails.ApproverId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    ClaimApprovalDetails.ClaimApplicationId = Business.Common.Context.ClaimApplicationId;
                    ClaimApprovalDetails.Status = (int)ClaimStatusEnum.Rejected;
                    ClaimApprovalDetails.Remarks = txtRemarks.Text.Trim();
                    int response = new Business.ClaimManagement.ClaimApprovalDetails().Claim_Approve(ClaimApprovalDetails);

                    if (response > 0)
                    {
                        //If final Appoval approved then update status in Master table
                        new Business.ClaimManagement.ClaimApplication().ClaimApplicationMaster_Save(
                                                                            new Entity.ClaimManagement.ClaimApplicationMaster()
                                                                            {
                                                                                ClaimApplicationId = Business.Common.Context.ClaimApplicationId,
                                                                                Status = (int)ClaimStatusEnum.Rejected
                                                                            });
                        GetClaimApplications_ByApproverId(ckShowAll.Checked ? (int)ClaimStatusEnum.None : (int)ClaimStatusEnum.Pending);
                        MessageSuccess.IsSuccess = true;
                        MessageSuccess.Text = "Claim rejected.";
                        MessageSuccess.Show = true;
                        ModalPopupExtender1.Hide();
                    }
                    else
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Response not given.";
                        Message.Show = true;

                        TabContainer1.ActiveTab = Approval;
                        ModalPopupExtender1.Show();
                    }
                }
                else
                {
                    TabContainer1.ActiveTab = Approval;
                    ModalPopupExtender1.Show();
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetClaimApplications_ByApproverId(ckShowAll.Checked ? (int)ClaimStatusEnum.None : (int)ClaimStatusEnum.Pending);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (ClaimApprovalValidation())
                {
                    Entity.ClaimManagement.ClaimApprovalDetails claimApprovalDetails = new Entity.ClaimManagement.ClaimApprovalDetails();
                    claimApprovalDetails.ApproverId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    claimApprovalDetails.ClaimApplicationId = Business.Common.Context.ClaimApplicationId;
                    claimApprovalDetails.Status = (int)ClaimStatusEnum.Rejected;
                    claimApprovalDetails.Remarks = txtRemarks.Text.Trim();
                    int response = new Business.ClaimManagement.ClaimApprovalDetails().Claim_Approve(claimApprovalDetails);

                    if (response > 0)
                    {
                        //If final Appoval approved then update status in Master table
                        new Business.ClaimManagement.ClaimApplication().ClaimApplicationMaster_Save(
                                                                            new Entity.ClaimManagement.ClaimApplicationMaster()
                                                                            {
                                                                                ClaimApplicationId = Business.Common.Context.ClaimApplicationId,
                                                                                Status = (int)ClaimStatusEnum.Rejected
                                                                            });
                        //Claim Balance reverting
                        //ClaimAccontBalance_Revert(Business.Common.Context.ClaimApplicationId);

                        GetClaimApplications_ByApproverId(ckShowAll.Checked ? (int)ClaimStatusEnum.None : (int)ClaimStatusEnum.Pending);
                        MessageSuccess.IsSuccess = true;
                        MessageSuccess.Text = "Claim cancelled.";
                        MessageSuccess.Show = true;
                        ModalPopupExtender1.Hide();
                    }
                    else
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Response not given.";
                        Message.Show = true;

                        TabContainer1.ActiveTab = Approval;
                        ModalPopupExtender1.Show();
                    }
                }
                else
                {
                    TabContainer1.ActiveTab = Approval;
                    ModalPopupExtender1.Show();
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
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

                    //DropDownList ddlLineItemStatus = (DropDownList)e.Row.FindControl("ddlLineItemStatus");
                    //LoadClaimStatus(ddlLineItemStatus);
                    //ddlLineItemStatus.SelectedValue = ((DataTable)(gvClaimDetails.DataSource)).Rows[e.Row.RowIndex]["Status"].ToString();

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

        //private void LoadClaimStatus(DropDownList ddlLineItemStatus)
        //{
        //    ddlLineItemStatus.DataSource = new Business.ClaimManagement.ClaimStatus().ClaimStatus_GetAll(
        //        new Entity.ClaimManagement.ClaimStatus() { });
        //    ddlLineItemStatus.DataTextField = "StatusName";
        //    ddlLineItemStatus.DataValueField = "ClaimStatusId";
        //    ddlLineItemStatus.DataBind();
        //}

        protected void gvClaimDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "U")
                {
                    GridViewRow gridViewRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                    HiddenField hdnChecked = (HiddenField)gridViewRow.FindControl("hdnChecked");
                    TextBox txtApprovedAmount = (TextBox)gridViewRow.FindControl("txtApprovedAmount");
                    TextBox txtApprovedRemarks = (TextBox)gridViewRow.FindControl("txtApprovedRemarks");
                    //DropDownList ddlLineItemStatus = (DropDownList)gridViewRow.FindControl("ddlLineItemStatus");
                    hdnChecked.Value = "Checked";
                    gridViewRow.Font.Italic = true;
                    gridViewRow.Attributes.CssStyle.Add("color", "#038a10");

                    Business.ClaimManagement.ClaimApplication objClaimApplication = new Business.ClaimManagement.ClaimApplication();
                    Entity.ClaimManagement.ClaimApplicationDetails claimApplicationDetails = new Entity.ClaimManagement.ClaimApplicationDetails()
                    {
                        ClaimApplicationDetailId = int.Parse(e.CommandArgument.ToString()),
                        ApprovedAmount = (string.IsNullOrEmpty(txtApprovedAmount.Text.Trim())) ? 0 : Convert.ToDecimal(txtApprovedAmount.Text.Trim()),
                        ApproverRemarks = txtApprovedRemarks.Text.Trim(),
                        Status = (int)ClaimStatusEnum.Approved
                    };

                    objClaimApplication.ClaimApplicationDetails_Save(claimApplicationDetails);

                    ComputeTotalApprovedAmount();

                }
                else if (e.CommandName == "A")
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
        private void ComputeTotalApprovedAmount()
        {
            decimal total = 0;
            foreach (GridViewRow gvr in gvClaimDetails.Rows)
            {
                TextBox txtApprovedAmount = (TextBox)gvr.FindControl("txtApprovedAmount");
                if (!string.IsNullOrEmpty(txtApprovedAmount.Text))
                {
                    total += Convert.ToDecimal(txtApprovedAmount.Text.Trim());
                }
            }
            lblTotalApprovedAmount.Text = total.ToString();
        }

        private int Claim_Heading_Update(int claimApplicationId, string claimHeading)
        {
            Business.ClaimManagement.ClaimApplication objClaimApplication = new Business.ClaimManagement.ClaimApplication();
            return objClaimApplication.Claim_HeadingUpdate(new Entity.ClaimManagement.ClaimApplicationMaster()
            {
                ClaimApplicationId = claimApplicationId,
                ClaimHeading = claimHeading
            });
        }
    }
}