using Business.Common;
using System;
using System.Collections.Generic;
using System.Data;
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
            Business.ClaimManagement.ClaimStatus objClaimStatus= new Business.ClaimManagement.ClaimStatus();
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
    }
}