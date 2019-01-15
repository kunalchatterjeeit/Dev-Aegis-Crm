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
        private void LeaveApplicationMaster_GetAll()
        {
            DataTable dtLeaveApplicationMaster =
                new Business.LeaveManagement.LeaveApprovalDetails()
                .GetLeaveApplications_ByApproverId(Convert.ToInt32(HttpContext.Current.User.Identity.Name), (int)LeaveStatusEnum.Pending);
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
                lblTotalLeaveCount.Text = ((Convert.ToDateTime(dsLeaveApplicationDetails.Tables[0].Rows[0]["ToDate"].ToString()) - Convert.ToDateTime(dsLeaveApplicationDetails.Tables[0].Rows[0]["FromDate"].ToString())).TotalDays + 1).ToString();
                hdnAttachmentName.Value = (dsLeaveApplicationDetails.Tables[0].Rows[0]["LeaveAccumulationTypeName"] != null) ? dsLeaveApplicationDetails.Tables[0].Rows[0]["Attachment"].ToString() : string.Empty;

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

            DataTable dtDates = new DataTable();
            dtDates.Columns.Add("Date");
            dtDates.Columns.Add("Day");

            DateTime dateTime = Convert.ToDateTime(dsLeaveApplicationDetails.Tables[0].Rows[0]["FromDate"].ToString());

            while (true)
            {
                if (Convert.ToDateTime(dsLeaveApplicationDetails.Tables[0].Rows[0]["ToDate"].ToString()).AddDays(1).Date <= dateTime.Date)
                    break;
                else
                    dtDates.Rows.Add();

                dtDates.Rows[dtDates.Rows.Count - 1]["Date"] = dateTime.ToString("dd MMM yyyy");
                dtDates.Rows[dtDates.Rows.Count - 1]["Day"] = dateTime.ToString("dddd");
                dtDates.AcceptChanges();

                dateTime = dateTime.AddDays(1);
            }

            gvDates.DataSource = dtDates;
            gvDates.DataBind();
        }

        private int LeaveAccontBalance_Adjust(int leaveApplicationId)
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
                leaveAccountBalance.Amount = -(Convert.ToDecimal((Convert.ToDateTime(dtLeaveApplicationMaster.Rows[0]["ToDate"].ToString()) - Convert.ToDateTime(dtLeaveApplicationMaster.Rows[0]["FromDate"].ToString())).TotalDays + 1));
                leaveAccountBalance.Reason = "LEAVE APPROVED";
            }

            int response = objLeaveAccountBalance.LeaveAccontBalance_Adjust(leaveAccountBalance);
            return response;
        }

        private bool LeaveApprovalValidation()
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
                LeaveApplicationMaster_GetAll();
                Message.Show = false;
                MessageSuccess.Show = false;
            }
        }

        protected void gvLeaveApprovalList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                Business.Common.Context.LeaveApplicationId = Convert.ToInt32(e.CommandArgument.ToString());
                GetLeaveApplicationDetails_ByLeaveApplicationId(Business.Common.Context.LeaveApplicationId);
                TabContainer1.ActiveTab = Approval;
                ModalPopupExtender1.Show();
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
                                LeaveApplicationMaster_GetAll();
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
                            int adjustResponse = LeaveAccontBalance_Adjust(Business.Common.Context.LeaveApplicationId);
                            if (adjustResponse > 0)
                            {
                                LeaveApplicationMaster_GetAll();
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
                        LeaveApplicationMaster_GetAll();
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
                // do nothing
            }
        }
    }
}