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
                .LeaveApplicationMaster_GetAll(new Entity.LeaveManagement.LeaveApplicationMaster() {
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
            Business.LeaveManagement.LeaveApprovalConfiguration objLeaveApprovalConfiguration = new Business.LeaveManagement.LeaveApprovalConfiguration();
            DataTable dtLeaveEmployeeWiseApprovalConfiguration = objLeaveApprovalConfiguration.LeaveEmployeeWiseApprovalConfiguration_GetAll(
                new Entity.LeaveManagement.LeaveApprovalConfiguration()
                {
                    EmployeeId = Convert.ToInt32(HttpContext.Current.User.Identity.Name)
                });

            int currentLeaveApproverLevel = 0;
            if (dtLeaveEmployeeWiseApprovalConfiguration != null
               && dtLeaveEmployeeWiseApprovalConfiguration.AsEnumerable().Any()
               && dtLeaveEmployeeWiseApprovalConfiguration.Select("ApproverId = " + HttpContext.Current.User.Identity.Name).Any())
            {
                //Fetching the current approver approval level
                currentLeaveApproverLevel = Convert.ToInt32(dtLeaveEmployeeWiseApprovalConfiguration
                    .Select("ApproverId = " + HttpContext.Current.User.Identity.Name).FirstOrDefault()["ApprovalLevel"].ToString());
            }
                Entity.LeaveManagement.LeaveApprovalDetails leaveApprovalDetails = new Entity.LeaveManagement.LeaveApprovalDetails();
            leaveApprovalDetails.ApproverId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            leaveApprovalDetails.LeaveApplicationId = Business.Common.Context.LeaveApplicationId;
            leaveApprovalDetails.Status = (int)LeaveStatusEnum.Cancelled;
            leaveApprovalDetails.Remarks = "CANCELLED BY USER";
            int response = new Business.LeaveManagement.LeaveApprovalDetails().LeaveApprove(leaveApprovalDetails);
        }

        protected void btnFollowup_Click(object sender, EventArgs e)
        { }
    }
}