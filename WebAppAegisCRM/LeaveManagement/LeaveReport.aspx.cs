using Business.Common;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.LeaveManagement
{
    public partial class LeaveReport : System.Web.UI.Page
    {
        ILog logger = log4net.LogManager.GetLogger("ErrorLog");
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

        private void LeaveApplication_GetAll()
        {
            Entity.LeaveManagement.LeaveApplicationMaster leaveApplicationMaster = new Entity.LeaveManagement.LeaveApplicationMaster();
            leaveApplicationMaster.LeaveTypeId = Convert.ToInt32(ddlLeaveType.SelectedValue);
            leaveApplicationMaster.RequestorId = Convert.ToInt32(ddlEmployee.SelectedValue);
            leaveApplicationMaster.FromDate = (string.IsNullOrEmpty(txtFromDate.Text.Trim())) ? DateTime.MinValue : Convert.ToDateTime(txtFromDate.Text.Trim());
            leaveApplicationMaster.ToDate = (string.IsNullOrEmpty(txtToDate.Text.Trim())) ? DateTime.MinValue : Convert.ToDateTime(txtToDate.Text.Trim());
            Business.LeaveManagement.LeaveApplication objLeaveApplication = new Business.LeaveManagement.LeaveApplication();
            DataSet dsLeaveApplication = objLeaveApplication.LeaveApplication_GetAll(leaveApplicationMaster);
            if (dsLeaveApplication != null && dsLeaveApplication.Tables.Count > 1)
            {
                if (gvLeaveReport.PageIndex == 0)
                {
                    gvLeaveTotalReport.DataSource = dsLeaveApplication.Tables[0];
                    gvLeaveTotalReport.DataBind();
                }

                gvLeaveReport.DataSource = dsLeaveApplication.Tables[1];
                gvLeaveReport.DataBind();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    EmployeeMaster_GetAll();
                    LoadLeaveType();
                    LeaveApplication_GetAll();
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                LeaveApplication_GetAll();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
            }
        }

        protected void gvLeaveReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvLeaveReport.PageIndex = e.NewPageIndex;
                LeaveApplication_GetAll();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
            }
        }
    }
}