﻿using Business.Common;
using Entity.Common;
using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.LeaveManagement
{
    public partial class LeaveApplicationList : System.Web.UI.Page
    {
        private void LeaveApplicationMaster_GetAll()
        {
            DataTable dtLeaveApplicationMaster =
                new Business.LeaveManagement.LeaveApplication()
                .LeaveApplicationMaster_GetAll(new Entity.LeaveManagement.LeaveApplicationMaster()
                {
                    RequestorId = Convert.ToInt32(HttpContext.Current.User.Identity.Name)
                });
            gvLeaveApplicationList.DataSource = dtLeaveApplicationMaster;
            gvLeaveApplicationList.DataBind();
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

                if (dsLeaveApplicationDetails.Tables.Count > 1)
                {
                    gvApprovalHistory.DataSource = dsLeaveApplicationDetails.Tables[1];
                    gvApprovalHistory.DataBind();
                }
            }

            if (Convert.ToInt32(dsLeaveApplicationDetails.Tables[0].Rows[0]["LeaveStatusId"].ToString()) == (int)LeaveStatusEnum.Pending)
            {
                btnCancel.Visible = true;
                btnFollowup.Visible = true;
            }
            else
            {
                btnCancel.Visible = false;
                btnFollowup.Visible = false;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Message.Show = false;
                MessageSuccess.Show = false;
                LeaveApplicationMaster_GetAll();
            }
        }

        protected void gvLeaveApplicationList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLeaveApplicationList.PageIndex = e.NewPageIndex;
            LeaveApplicationMaster_GetAll();
        }

        protected void gvLeaveApplicationList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                Business.Common.Context.LeaveApplicationId = Convert.ToInt32(e.CommandArgument.ToString());
                GetLeaveApplicationDetails_ByLeaveApplicationId(Business.Common.Context.LeaveApplicationId);
                TabContainer1.ActiveTab = Approval;
                ModalPopupExtender1.Show();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtApprovalDetails = new Business.LeaveManagement.LeaveApprovalDetails().LeaveApprovalDetails_ByRequestorId(
                    Convert.ToInt32(HttpContext.Current.User.Identity.Name),
                    (int)LeaveStatusEnum.Pending);

                if (dtApprovalDetails != null && dtApprovalDetails.AsEnumerable().Any())
                {
                    foreach (DataRow drApproval in dtApprovalDetails.Rows)
                    {
                        Entity.LeaveManagement.LeaveApprovalDetails leaveApprovalDetails = new Entity.LeaveManagement.LeaveApprovalDetails();
                        leaveApprovalDetails.ApproverId = Convert.ToInt32(drApproval["ApproverId"].ToString());
                        leaveApprovalDetails.LeaveApplicationId = Business.Common.Context.LeaveApplicationId;
                        leaveApprovalDetails.Status = (int)LeaveStatusEnum.Cancelled;
                        leaveApprovalDetails.Remarks = "CANCELLED BY USER";
                        int response = new Business.LeaveManagement.LeaveApprovalDetails().LeaveApprove(leaveApprovalDetails);
                    }

                    //If final all Approvals Cancelled then update status in Master table
                    new Business.LeaveManagement.LeaveApplication().LeaveApplicationMaster_Save(
                                                                        new Entity.LeaveManagement.LeaveApplicationMaster()
                                                                        {
                                                                            LeaveApplicationId = Business.Common.Context.LeaveApplicationId,
                                                                            LeaveStatusId = (int)LeaveStatusEnum.Cancelled
                                                                        });


                    MessageSuccess.IsSuccess = true;
                    MessageSuccess.Text = "Leave is cancelled.";
                    MessageSuccess.Show = true;
                    ModalPopupExtender1.Hide();
                    LeaveApplicationMaster_GetAll();
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Leave Cancel is not allowed.";
                    Message.Show = true;
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

        protected void btnFollowup_Click(object sender, EventArgs e)
        { }
    }
}