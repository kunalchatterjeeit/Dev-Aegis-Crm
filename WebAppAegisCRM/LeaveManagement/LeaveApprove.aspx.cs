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

namespace WebAppAegisCRM.LeaveManagement
{
    public partial class LeaveApprove : System.Web.UI.Page
    {
        
        private void GetLeaveApplications_ByApproverId(int statusId)
        {
            LeaveTypeEnum leaveType = (LeaveTypeEnum)Enum.Parse(typeof(LeaveTypeEnum), ddlLeaveType.SelectedValue);
            DateTime fromApplicationDate = string.IsNullOrEmpty(txtFromLeaveDate.Text.Trim()) ? DateTime.MinValue : Convert.ToDateTime(txtFromLeaveDate.Text.Trim());
            DateTime toApplicationDate = string.IsNullOrEmpty(txtToLeaveDate.Text.Trim()) ? DateTime.MinValue : Convert.ToDateTime(txtToLeaveDate.Text.Trim());
            DataTable dtLeaveApplicationMaster =
                new Business.LeaveManagement.LeaveApprovalDetails()
                .GetLeaveApplications_ByApproverId(Convert.ToInt32(HttpContext.Current.User.Identity.Name), statusId, leaveType, fromApplicationDate, toApplicationDate);
            gvLeaveApprovalList.DataSource = dtLeaveApplicationMaster;
            gvLeaveApprovalList.DataBind();
        }

        private void GetLeaveApplicationDetails_ByLeaveApplicationId(int leaveApplicationId)
        {
            DataSet dsLeaveApplicationDetails = new Business.LeaveManagement.LeaveApplication().GetLeaveApplicationDetails_ByLeaveApplicationId(leaveApplicationId);
            if (dsLeaveApplicationDetails != null)
            {
                lblLeaveApplicationNumber.Text = dsLeaveApplicationDetails.Tables[0].Rows[0]["LeaveApplicationNumber"].ToString();
                lblName.Text = dsLeaveApplicationDetails.Tables[0].Rows[0]["Requestor"].ToString();
                lblLeaveType.Text = dsLeaveApplicationDetails.Tables[0].Rows[0]["LeaveTypeName"].ToString();
                lblFromDate.Text = dsLeaveApplicationDetails.Tables[0].Rows[0]["FromDate"].ToString();
                lblToDate.Text = dsLeaveApplicationDetails.Tables[0].Rows[0]["ToDate"].ToString();
                lblLeaveAccumulationType.Text = dsLeaveApplicationDetails.Tables[0].Rows[0]["LeaveAccumulationTypeName"].ToString();
                lblTotalLeaveCount.Text = dsLeaveApplicationDetails.Tables[0].Rows[0]["TotalLeaveDays"].ToString();
                hdnAttachmentName.Value = (dsLeaveApplicationDetails.Tables[0].Rows[0]["LeaveAccumulationTypeName"] != null) ? dsLeaveApplicationDetails.Tables[0].Rows[0]["Attachment"].ToString() : string.Empty;
                lblReason.Text = dsLeaveApplicationDetails.Tables[0].Rows[0]["Reason"].ToString();

                if (string.IsNullOrEmpty(hdnAttachmentName.Value))
                {
                    lnkBtnAttachment.Enabled = false;
                    lnkBtnAttachment.Text = "No attachment";
                }
                else
                {
                    lnkBtnAttachment.Enabled = true;
                    lnkBtnAttachment.Text = "Click to download";
                }

                if (dsLeaveApplicationDetails.Tables.Count > 1)
                {
                    gvApprovalHistory.DataSource = dsLeaveApplicationDetails.Tables[1];
                    gvApprovalHistory.DataBind();
                }
            }

            gvDates.DataSource = dsLeaveApplicationDetails.Tables[2];
            gvDates.DataBind();

            if ((Convert.ToInt32(dsLeaveApplicationDetails.Tables[0].Rows[0]["LeaveStatusId"].ToString()) == (int)LeaveStatusEnum.Approved) &&
                Convert.ToDateTime(dsLeaveApplicationDetails.Tables[0].Rows[0]["FromDate"].ToString()).Date >= DateTime.Now.Date)
            {
                btnCancel.Visible = true;
                btnApprove.Visible = false;
                btnReject.Visible = false;
            }
            else if (Convert.ToInt32(dsLeaveApplicationDetails.Tables[0].Rows[0]["LeaveStatusId"].ToString()) == (int)LeaveStatusEnum.Pending)
            {
                btnCancel.Visible = false;
                btnApprove.Visible = true;
                btnReject.Visible = true;
            }
            else
            {
                btnCancel.Visible = false;
                btnApprove.Visible = false;
                btnReject.Visible = false;
            }
        }

        private int LeaveAccontBalance_Deduct(int leaveApplicationId)
        {
            Entity.LeaveManagement.LeaveAccountBalance leaveAccountBalance = new Entity.LeaveManagement.LeaveAccountBalance();
            Business.LeaveManagement.LeaveAccountBalance objLeaveAccountBalance = new Business.LeaveManagement.LeaveAccountBalance();

            DataTable dtLeaveApplicationMaster = new Business.LeaveManagement.LeaveApplication()
                .LeaveApplicationMaster_GetAll(
                new Entity.LeaveManagement.LeaveApplicationMaster()
                {
                    LeaveApplicationId = leaveApplicationId
                });

            if (dtLeaveApplicationMaster != null && dtLeaveApplicationMaster.AsEnumerable().Any())
            {
                leaveAccountBalance.EmployeeId = Convert.ToInt32(dtLeaveApplicationMaster.Rows[0]["RequestorId"].ToString());
                leaveAccountBalance.LeaveTypeId = Convert.ToInt32(dtLeaveApplicationMaster.Rows[0]["LeaveTypeId"].ToString());
                leaveAccountBalance.Amount = -(Convert.ToDecimal(dtLeaveApplicationMaster.Rows[0]["TotalLeaveDays"].ToString()));
                leaveAccountBalance.Reason = "LEAVE APPROVED";
            }

            int response = objLeaveAccountBalance.LeaveAccontBalance_Adjust(leaveAccountBalance);
            return response;
        }

        private int LeaveAccontBalance_Revert(int leaveApplicationId)
        {
            Entity.LeaveManagement.LeaveAccountBalance leaveAccountBalance = new Entity.LeaveManagement.LeaveAccountBalance();
            Business.LeaveManagement.LeaveAccountBalance objLeaveAccountBalance = new Business.LeaveManagement.LeaveAccountBalance();

            DataTable dtLeaveApplicationMaster = new Business.LeaveManagement.LeaveApplication()
                .LeaveApplicationMaster_GetAll(
                new Entity.LeaveManagement.LeaveApplicationMaster()
                {
                    LeaveApplicationId = leaveApplicationId
                });

            if (dtLeaveApplicationMaster != null && dtLeaveApplicationMaster.AsEnumerable().Any())
            {
                leaveAccountBalance.EmployeeId = Convert.ToInt32(dtLeaveApplicationMaster.Rows[0]["RequestorId"].ToString());
                leaveAccountBalance.LeaveTypeId = Convert.ToInt32(dtLeaveApplicationMaster.Rows[0]["LeaveTypeId"].ToString());
                leaveAccountBalance.Amount = (Convert.ToDecimal(dtLeaveApplicationMaster.Rows[0]["TotalLeaveDays"].ToString()));
                leaveAccountBalance.Reason = "LEAVE BALANCE REVERTED AS LEAVE IS CANCELLED";
            }

            int response = objLeaveAccountBalance.LeaveAccontBalance_Adjust(leaveAccountBalance);
            return response;
        }

        private bool LeaveApprovalValidation()
        {
            try
            {
                if (string.IsNullOrEmpty(txtRemarks.Text.Trim()))
                {
                    Message.IsSuccess = false;
                    Message.Text = "Please enter remarks.";
                    Message.Show = true;
                    return false;
                }

                Business.LeaveManagement.LeaveApplication objLeaveApplication = new Business.LeaveManagement.LeaveApplication();
                Entity.LeaveManagement.LeaveApplicationMaster leaveApplicationMaster = new Entity.LeaveManagement.LeaveApplicationMaster();
                leaveApplicationMaster.LeaveApplicationId = Business.Common.Context.LeaveApplicationId;
                DataTable dtLeaveApplication = objLeaveApplication.LeaveApplicationMaster_GetAll(leaveApplicationMaster);
                DataTable dtLeaveAccountBalance = new Business.LeaveManagement.LeaveAccountBalance().LeaveAccountBalance_ByEmployeeId(Convert.ToInt32(dtLeaveApplication.Rows[0]["RequestorId"].ToString()), Convert.ToInt32(dtLeaveApplication.Rows[0]["LeaveTypeId"].ToString())).Tables[0];
                DataSet dsLeaveDetails = objLeaveApplication.GetLeaveApplicationDetails_ByLeaveApplicationId(Business.Common.Context.LeaveApplicationId);
                decimal totalLeaveCount = 0;
                totalLeaveCount = Convert.ToDecimal(dsLeaveDetails.Tables[2].Compute("Sum(AppliedForDay)", string.Empty));
                if (dtLeaveAccountBalance != null && dtLeaveAccountBalance.AsEnumerable().Any())
                {
                    if (totalLeaveCount > Convert.ToDecimal(dtLeaveAccountBalance.Rows[0]["Amount"].ToString()))
                    {
                        Message.Text = "Applicant's Leave Balance is low.";
                        Message.IsSuccess = false;
                        Message.Show = true;
                        return false;
                    }
                    if (Convert.ToBoolean(dtLeaveAccountBalance.Rows[0]["LeaveBlocked"].ToString()))
                    {
                        Message.Text = "Applicant's leaves are blocked. Please contact to HR.";
                        Message.IsSuccess = false;
                        Message.Show = true;
                        return false;
                    }
                }
                else
                {
                    Message.Text = "Applicant do not have any Leave Balance.";
                    Message.IsSuccess = false;
                    Message.Show = true;
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                ex.WriteException();
                Message.Text = ex.Message;
                Message.IsSuccess = false;
                Message.Show = true;
                return false;
            }
        }

        private void LoadLeaveType()
        {
            Business.LeaveManagement.LeaveType objLeaveType = new Business.LeaveManagement.LeaveType();
            DataTable dtLeaveMaster = objLeaveType.LeaveTypeGetAll(new Entity.LeaveManagement.LeaveType());
            if (dtLeaveMaster != null)
            {
                ddlLeaveType.DataSource = dtLeaveMaster;
                ddlLeaveType.DataTextField = "LeaveTypeName";
                ddlLeaveType.DataValueField = "LeaveTypeId";
                ddlLeaveType.DataBind();
            }
            ddlLeaveType.InsertSelect();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            { 
            if (!IsPostBack)
            {
                LoadLeaveType();

                GetLeaveApplications_ByApproverId((int)LeaveStatusEnum.Pending);
                Message.Show = false;
                MessageSuccess.Show = false;
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

        protected void gvLeaveApprovalList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            { 
            if (e.CommandName == "View")
            {
                Business.Common.Context.LeaveApplicationId = Convert.ToInt32(e.CommandArgument.ToString());
                GetLeaveApplicationDetails_ByLeaveApplicationId(Business.Common.Context.LeaveApplicationId);
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

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            try
            {
                if (LeaveApprovalValidation())
                {
                    Entity.LeaveManagement.LeaveApprovalDetails leaveApprovalDetails = new Entity.LeaveManagement.LeaveApprovalDetails();
                    leaveApprovalDetails.ApproverId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    leaveApprovalDetails.LeaveApplicationId = Business.Common.Context.LeaveApplicationId;
                    leaveApprovalDetails.Status = (int)LeaveStatusEnum.Approved;
                    leaveApprovalDetails.Remarks = txtRemarks.Text.Trim();
                    int response = new Business.LeaveManagement.LeaveApprovalDetails().LeaveApprove(leaveApprovalDetails);

                    if (response > 0)
                    {
                        //Fetching Requestor Id
                        Entity.LeaveManagement.LeaveApplicationMaster leaveApplicationMaster = new Entity.LeaveManagement.LeaveApplicationMaster();
                        Business.LeaveManagement.LeaveApplication objLeaveApplication = new Business.LeaveManagement.LeaveApplication();
                        leaveApplicationMaster.LeaveApplicationId = Business.Common.Context.LeaveApplicationId;
                        DataTable dtLeaveApplication = objLeaveApplication.LeaveApplicationMaster_GetAll(leaveApplicationMaster);

                        Business.LeaveManagement.LeaveApprovalConfiguration objLeaveApprovalConfiguration = new Business.LeaveManagement.LeaveApprovalConfiguration();
                        DataTable dtLeaveEmployeeWiseApprovalConfiguration = objLeaveApprovalConfiguration.LeaveEmployeeWiseApprovalConfiguration_GetAll(
                            new Entity.LeaveManagement.LeaveApprovalConfiguration()
                            {
                                EmployeeId = (dtLeaveApplication != null && dtLeaveApplication.AsEnumerable().Any()) ? Convert.ToInt32(dtLeaveApplication.Rows[0]["RequestorId"].ToString()) : 0
                            });

                        int currentLeaveApproverLevel = 0;
                        if (dtLeaveEmployeeWiseApprovalConfiguration != null
                           && dtLeaveEmployeeWiseApprovalConfiguration.AsEnumerable().Any()
                           && dtLeaveEmployeeWiseApprovalConfiguration.Select("ApproverId = " + HttpContext.Current.User.Identity.Name).Any())
                        {
                            //Fetching the current approver approval level
                            currentLeaveApproverLevel = Convert.ToInt32(dtLeaveEmployeeWiseApprovalConfiguration
                                .Select("ApproverId = " + HttpContext.Current.User.Identity.Name).FirstOrDefault()["ApprovalLevel"].ToString());
                            currentLeaveApproverLevel = currentLeaveApproverLevel + 1;
                        }
                        if (dtLeaveEmployeeWiseApprovalConfiguration != null
                       && dtLeaveEmployeeWiseApprovalConfiguration.AsEnumerable().Any()
                       && dtLeaveEmployeeWiseApprovalConfiguration.Select("ApprovalLevel = " + currentLeaveApproverLevel).Any())
                        {
                            Business.LeaveManagement.LeaveApprovalDetails objLeaveApprovalDetails = new Business.LeaveManagement.LeaveApprovalDetails();

                            leaveApprovalDetails.LeaveApprovalDetailId = 0;
                            leaveApprovalDetails.LeaveApplicationId = Business.Common.Context.LeaveApplicationId;
                            leaveApprovalDetails.Status = (int)LeaveStatusEnum.Pending;
                            leaveApprovalDetails.ApproverId = Convert.ToInt32(dtLeaveEmployeeWiseApprovalConfiguration.Select("ApprovalLevel = " + currentLeaveApproverLevel).FirstOrDefault()["ApproverId"].ToString());
                            //Assigning Leave Request to next approver
                            int approvalResponse = objLeaveApprovalDetails.LeaveApprovalDetails_Save(leaveApprovalDetails);
                            if (approvalResponse > 0)
                            {
                                GetLeaveApplications_ByApproverId(ckShowAll.Checked ? (int)LeaveStatusEnum.None : (int)LeaveStatusEnum.Pending);
                                MessageSuccess.IsSuccess = true;
                                MessageSuccess.Text = "Approval moved to next level.";
                                MessageSuccess.Show = true;
                                ModalPopupExtender1.Hide();
                            }
                        }
                        else
                        {
                            //If final Appoval approved then update status in Master table
                            new Business.LeaveManagement.LeaveApplication().LeaveApplicationMaster_Save(
                                                                                new Entity.LeaveManagement.LeaveApplicationMaster()
                                                                                {
                                                                                    LeaveApplicationId = Business.Common.Context.LeaveApplicationId,
                                                                                    LeaveStatusId = (int)LeaveStatusEnum.Approved
                                                                                });
                            int adjustResponse = LeaveAccontBalance_Deduct(Business.Common.Context.LeaveApplicationId);
                            if (adjustResponse > 0)
                            {
                                GetLeaveApplications_ByApproverId(ckShowAll.Checked ? (int)LeaveStatusEnum.None : (int)LeaveStatusEnum.Pending);
                                MessageSuccess.IsSuccess = true;
                                MessageSuccess.Text = "Leave approved.";
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
                if (LeaveApprovalValidation())
                {
                    Entity.LeaveManagement.LeaveApprovalDetails leaveApprovalDetails = new Entity.LeaveManagement.LeaveApprovalDetails();
                    leaveApprovalDetails.ApproverId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    leaveApprovalDetails.LeaveApplicationId = Business.Common.Context.LeaveApplicationId;
                    leaveApprovalDetails.Status = (int)LeaveStatusEnum.Rejected;
                    leaveApprovalDetails.Remarks = txtRemarks.Text.Trim();
                    int response = new Business.LeaveManagement.LeaveApprovalDetails().LeaveApprove(leaveApprovalDetails);

                    if (response > 0)
                    {
                        //If final Appoval approved then update status in Master table
                        new Business.LeaveManagement.LeaveApplication().LeaveApplicationMaster_Save(
                                                                            new Entity.LeaveManagement.LeaveApplicationMaster()
                                                                            {
                                                                                LeaveApplicationId = Business.Common.Context.LeaveApplicationId,
                                                                                LeaveStatusId = (int)LeaveStatusEnum.Rejected
                                                                            });
                        GetLeaveApplications_ByApproverId(ckShowAll.Checked ? (int)LeaveStatusEnum.None : (int)LeaveStatusEnum.Pending);
                        MessageSuccess.IsSuccess = true;
                        MessageSuccess.Text = "Leave rejected.";
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
                LinkButton lnkDownload = lnkBtnAttachment;
                ScriptManager.GetCurrent(this).RegisterPostBackControl(lnkDownload);
                string FileName = hdnAttachmentName.Value;
                string OriginalFileName = hdnAttachmentName.Value;
                string FilePath = Server.MapPath(" ") + "\\LeaveAttachment\\" + hdnAttachmentName.Value;
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
            catch (Exception ex)
            {
                ex.WriteException();
                
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            { 
            GetLeaveApplications_ByApproverId(ckShowAll.Checked ? (int)LeaveStatusEnum.None : (int)LeaveStatusEnum.Pending);
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if (LeaveApprovalValidation())
                {
                    Entity.LeaveManagement.LeaveApprovalDetails leaveApprovalDetails = new Entity.LeaveManagement.LeaveApprovalDetails();
                    leaveApprovalDetails.ApproverId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    leaveApprovalDetails.LeaveApplicationId = Business.Common.Context.LeaveApplicationId;
                    leaveApprovalDetails.Status = (int)LeaveStatusEnum.Cancelled;
                    leaveApprovalDetails.Remarks = txtRemarks.Text.Trim();
                    int response = new Business.LeaveManagement.LeaveApprovalDetails().LeaveApprove(leaveApprovalDetails);

                    if (response > 0)
                    {
                        //If final Appoval approved then update status in Master table
                        new Business.LeaveManagement.LeaveApplication().LeaveApplicationMaster_Save(
                                                                            new Entity.LeaveManagement.LeaveApplicationMaster()
                                                                            {
                                                                                LeaveApplicationId = Business.Common.Context.LeaveApplicationId,
                                                                                LeaveStatusId = (int)LeaveStatusEnum.Cancelled
                                                                            });
                        //Leave Balance reverting
                        LeaveAccontBalance_Revert(Business.Common.Context.LeaveApplicationId);

                        GetLeaveApplications_ByApproverId(ckShowAll.Checked ? (int)LeaveStatusEnum.None : (int)LeaveStatusEnum.Pending);
                        MessageSuccess.IsSuccess = true;
                        MessageSuccess.Text = "Leave cancelled.";
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
    }
}