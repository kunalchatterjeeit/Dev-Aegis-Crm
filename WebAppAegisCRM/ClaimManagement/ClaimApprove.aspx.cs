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
                //lblClaimCategor.Text = dsClaimApplicationDetails.Tables[0].Rows[0]["ClaimTypeName"].ToString();
                lblFromDate.Text = dsClaimApplicationDetails.Tables[0].Rows[0]["FromDate"].ToString();
                lblToDate.Text = dsClaimApplicationDetails.Tables[0].Rows[0]["ToDate"].ToString();
                //lblClaimAccumulationType.Text = dsClaimApplicationDetails.Tables[0].Rows[0]["ClaimAccumulationTypeName"].ToString();
                lblTotalClaimCount.Text = dsClaimApplicationDetails.Tables[0].Rows[0]["TotalAmount"].ToString();
                //hdnAttachmentName.Value = dsClaimApplicationDetails.Tables[0].Rows[0]["ApprovedAmount"].ToString();
                //lblReason.Text = dsClaimApplicationDetails.Tables[0].Rows[0]["Reason"].ToString();

            }

            gvClaimDetails.DataSource = dsClaimApplicationDetails.Tables[2];
            gvClaimDetails.DataBind();

            lblTotalApprovedAmount.Text = dsClaimApplicationDetails.Tables[2].Compute("SUM(Cost)", string.Empty).ToString();

            if ((Convert.ToInt32(dsClaimApplicationDetails.Tables[0].Rows[0]["Status"].ToString()) == (int)ClaimStatusEnum.Approved) &&
                Convert.ToDateTime(dsClaimApplicationDetails.Tables[0].Rows[0]["FromDate"].ToString()).Date >= DateTime.Now.Date)
            {
                //btnCancel.Visible = true;
                btnApprove.Visible = false;
                btnReject.Visible = false;
            }
            else if (Convert.ToInt32(dsClaimApplicationDetails.Tables[0].Rows[0]["Status"].ToString()) == (int)ClaimStatusEnum.Pending)
            {
                //btnCancel.Visible = false;
                btnApprove.Visible = true;
                btnReject.Visible = true;
            }
            else
            {
                //btnCancel.Visible = false;
                btnApprove.Visible = false;
                btnReject.Visible = false;
            }
        }

        //private int ClaimAccontBalance_Deduct(int ClaimApplicationId)
        //{
        //    Entity.ClaimManagement.ClaimAccountBalance ClaimAccountBalance = new Entity.ClaimManagement.ClaimAccountBalance();
        //    Business.ClaimManagement.ClaimAccountBalance objClaimAccountBalance = new Business.ClaimManagement.ClaimAccountBalance();

        //    DataTable dtClaimApplicationMaster = new Business.ClaimManagement.ClaimApplication()
        //        .ClaimApplicationMaster_GetAll(
        //        new Entity.ClaimManagement.ClaimApplicationMaster()
        //        {
        //            ClaimApplicationId = ClaimApplicationId
        //        });

        //    if (dtClaimApplicationMaster != null && dtClaimApplicationMaster.AsEnumerable().Any())
        //    {
        //        ClaimAccountBalance.EmployeeId = Convert.ToInt32(dtClaimApplicationMaster.Rows[0]["RequestorId"].ToString());
        //        ClaimAccountBalance.ClaimTypeId = Convert.ToInt32(dtClaimApplicationMaster.Rows[0]["ClaimTypeId"].ToString());
        //        ClaimAccountBalance.Amount = -(Convert.ToDecimal(dtClaimApplicationMaster.Rows[0]["TotalClaimDays"].ToString()));
        //        ClaimAccountBalance.Reason = "Claim APPROVED";
        //    }

        //    int response = objClaimAccountBalance.ClaimAccontBalance_Adjust(ClaimAccountBalance);
        //    return response;
        //}

        //private int ClaimAccontBalance_Revert(int ClaimApplicationId)
        //{
        //    Entity.ClaimManagement.ClaimAccountBalance ClaimAccountBalance = new Entity.ClaimManagement.ClaimAccountBalance();
        //    Business.ClaimManagement.ClaimAccountBalance objClaimAccountBalance = new Business.ClaimManagement.ClaimAccountBalance();

        //    DataTable dtClaimApplicationMaster = new Business.ClaimManagement.ClaimApplication()
        //        .ClaimApplicationMaster_GetAll(
        //        new Entity.ClaimManagement.ClaimApplicationMaster()
        //        {
        //            ClaimApplicationId = ClaimApplicationId
        //        });

        //    if (dtClaimApplicationMaster != null && dtClaimApplicationMaster.AsEnumerable().Any())
        //    {
        //        ClaimAccountBalance.EmployeeId = Convert.ToInt32(dtClaimApplicationMaster.Rows[0]["RequestorId"].ToString());
        //        ClaimAccountBalance.ClaimTypeId = Convert.ToInt32(dtClaimApplicationMaster.Rows[0]["ClaimTypeId"].ToString());
        //        ClaimAccountBalance.Amount = (Convert.ToDecimal(dtClaimApplicationMaster.Rows[0]["TotalClaimDays"].ToString()));
        //        ClaimAccountBalance.Reason = "Claim BALANCE REVERTED AS Claim IS CANCELLED";
        //    }

        //    int response = objClaimAccountBalance.ClaimAccontBalance_Adjust(ClaimAccountBalance);
        //    return response;
        //}

        private bool ClaimApprovalValidation()
        {
            if (string.IsNullOrEmpty(txtRemarks.Text.Trim()))
            {
                Message.IsSuccess = false;
                Message.Text = "Please enter remarks.";
                Message.Show = true;
                return false;
            }
            return true;
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
                    int response = new Business.ClaimManagement.ClaimApprovalDetails().ClaimApprove(ClaimApprovalDetails);

                    if (response > 0)
                    {
                        //Fetching Requestor Id
                        Entity.ClaimManagement.ClaimApplicationMaster ClaimApplicationMaster = new Entity.ClaimManagement.ClaimApplicationMaster();
                        Business.ClaimManagement.ClaimApplication objClaimApplication = new Business.ClaimManagement.ClaimApplication();
                        ClaimApplicationMaster.ClaimApplicationId = Business.Common.Context.ClaimApplicationId;
                        DataTable dtClaimApplication = objClaimApplication.ClaimApplicationMaster_GetAll(ClaimApplicationMaster);

                        Business.ClaimManagement.ClaimApprovalConfiguration objClaimApprovalConfiguration = new Business.ClaimManagement.ClaimApprovalConfiguration();
                        DataTable dtClaimEmployeeWiseApprovalConfiguration = objClaimApprovalConfiguration.ClaimEmployeeWiseApprovalConfiguration_GetAll(
                            new Entity.ClaimManagement.ClaimApprovalConfiguration()
                            {
                                EmployeeId = (dtClaimApplication != null && dtClaimApplication.AsEnumerable().Any()) ? Convert.ToInt32(dtClaimApplication.Rows[0]["RequestorId"].ToString()) : 0
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
                                                                                    Status = (int)ClaimStatusEnum.Approved
                                                                                });
                            int adjustResponse = 0;// ClaimAccontBalance_Deduct(Business.Common.Context.ClaimApplicationId);
                            if (adjustResponse > 0)
                            {
                                GetClaimApplications_ByApproverId(ckShowAll.Checked ? (int)ClaimStatusEnum.None : (int)ClaimStatusEnum.Pending);
                                MessageSuccess.IsSuccess = true;
                                MessageSuccess.Text = "Claim approved.";
                                MessageSuccess.Show = true;
                                ModalPopupExtender1.Hide();
                            }
                            else
                            {
                                Message.IsSuccess = false;
                                Message.Text = "Something went wrong! Please contact system administrator";
                                Message.Show = true;
                                TabContainer1.ActiveTab = Approval;
                                ModalPopupExtender1.Show();
                            }
                        }
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
                    //ClaimApprovalDetails.Remarks = txtRemarks.Text.Trim();
                    int response = new Business.ClaimManagement.ClaimApprovalDetails().ClaimApprove(ClaimApprovalDetails);

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

        protected void lnkBtnAttachment_Click(object sender, EventArgs e)
        {
            try
            {
                //LinkButton lnkDownload = lnkBtnAttachment;
                //ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkDownload);
                //string FileName = hdnAttachmentName.Value;
                //string OriginalFileName = hdnAttachmentName.Value;
                //string FilePath = Server.MapPath(" ") + "\\ClaimAttachment\\" + hdnAttachmentName.Value;
                //FileInfo file = new FileInfo(FilePath);
                //if (file.Exists)
                //{
                //    Response.ContentType = ContentType;
                //    Response.AppendHeader("Content-Disposition", "attachment; filename=" + OriginalFileName);
                //    Response.Headers.Set("Cache-Control", "private, max-age=0");
                //    Response.WriteFile(FilePath);
                //    Response.End();
                //}
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
                    int response = new Business.ClaimManagement.ClaimApprovalDetails().ClaimApprove(claimApprovalDetails);

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
                    if (string.IsNullOrEmpty(((DataTable)(gvClaimDetails.DataSource)).Rows[e.Row.RowIndex]["ClaimDetailsId"].ToString()))
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
                    ddlLineItemStatus.SelectedValue = ((DataTable)(gvClaimDetails.DataSource)).Rows[e.Row.RowIndex]["ClaimDetailsId"].ToString();
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
            if (e.CommandName == "U")
            {
                GridViewRow gridViewRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);
                HiddenField hdnChecked = (HiddenField)gridViewRow.FindControl("hdnChecked");
                hdnChecked.Value = "Checked";
                gridViewRow.Font.Italic = true;
                gridViewRow.Attributes.CssStyle.Add("color", "#038a10");
                ModalPopupExtender1.Show();

                ComputeTotalApprovedAmount();
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
    }
}