using Business.Common;

using System;
using System.Data;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.LeaveManagement
{
    public partial class LeaveAdjustment : System.Web.UI.Page
    {
        
        private int EmployeeMasterId
        {
            get { return Convert.ToInt32(ViewState["EmployeeMasterId"]); }
            set { ViewState["EmployeeMasterId"] = value; }
        }
        private void EmployeeMaster_GetAll()
        {
            Business.HR.EmployeeMaster ObjBelEmployeeMaster = new Business.HR.EmployeeMaster();
            Entity.HR.EmployeeMaster ObjElEmployeeMaster = new Entity.HR.EmployeeMaster();
            ObjElEmployeeMaster.CompanyId_FK = 1;
            DataTable dt = ObjBelEmployeeMaster.Employee_GetAll_Active(ObjElEmployeeMaster);
            gvEmployeerMaster.DataSource = dt;
            gvEmployeerMaster.DataBind();
        }
        private void LeaveAccountBalance_ByEmployeeId()
        {
            Business.LeaveManagement.LeaveAccountBalance objLeaveAccountBalance = new Business.LeaveManagement.LeaveAccountBalance();
            gvCurrentLeaveBalance.DataSource = objLeaveAccountBalance.LeaveAccountBalance_ByEmployeeId(EmployeeMasterId, 0).Tables[0];
            gvCurrentLeaveBalance.DataBind();
        }
        private void LeaveAccountBalanceDetails_ByEmployeeId()
        {
            Business.LeaveManagement.LeaveAccountBalance objLeaveAccountBalance = new Business.LeaveManagement.LeaveAccountBalance();
            gvLeaveBalanceDetails.DataSource = objLeaveAccountBalance.LeaveAccountBalance_ByEmployeeId(EmployeeMasterId, 0).Tables[1];
            gvLeaveBalanceDetails.DataBind();
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


        private int LeaveAccontBalance_Adjust(int leaveApplicationId)
        {
            Entity.LeaveManagement.LeaveAccountBalance leaveAccountBalance = new Entity.LeaveManagement.LeaveAccountBalance();
            Business.LeaveManagement.LeaveAccountBalance objLeaveAccountBalance = new Business.LeaveManagement.LeaveAccountBalance();

            leaveAccountBalance.EmployeeId = EmployeeMasterId;
            leaveAccountBalance.LeaveTypeId = Convert.ToInt32(ddlLeaveType.SelectedValue);
            leaveAccountBalance.Amount = (ddlOperation.SelectedValue == "1") ? Convert.ToDecimal(txtLeaveAmount.Text.Trim()) : -Convert.ToDecimal(txtLeaveAmount.Text.Trim());
            leaveAccountBalance.Reason = "MANUAL ADJUSTMENT: " + txtComments.Text.Trim();

            int response = objLeaveAccountBalance.LeaveAccontBalance_Adjust(leaveAccountBalance);
            return response;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    EmployeeMaster_GetAll();
                    Message.Show = false;
                    Message1.Show = false;
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

        protected void btnLeaveAdjust_Click(object sender, EventArgs e)
        {
            try
            {
                int adjustResponse = LeaveAccontBalance_Adjust(Business.Common.Context.LeaveApplicationId);
                if (adjustResponse > 0)
                {
                    ddlLeaveType.SelectedIndex = 0;
                    ddlOperation.SelectedIndex = 0;
                    txtComments.Text = string.Empty;
                    txtLeaveAmount.Text = string.Empty;
                    LeaveAccountBalance_ByEmployeeId();
                    LeaveAccountBalanceDetails_ByEmployeeId();
                    Message.IsSuccess = true;
                    Message.Text = "Leave account updated successfully.";
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Something went wrong! Please contact system administrator";
                    ModalPopupExtender1.Show();
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
                Message.IsSuccess = false;
                Message.Text = ex.Message;
            }
            finally
            {
                Message.Show = true;
            }
        }

        protected void gvEmployeerMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Leave")
                {
                    EmployeeMasterId = Convert.ToInt32(e.CommandArgument.ToString());
                    LeaveAccountBalance_ByEmployeeId();
                    LeaveAccountBalanceDetails_ByEmployeeId();
                    LoadLeaveType();
                    TabContainer1.ActiveTab = LeaveUpdate;
                    ModalPopupExtender1.Show();
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
                Message1.IsSuccess = false;
                Message1.Text = ex.Message;
                Message1.Show = true;
            }
        }
    }
}