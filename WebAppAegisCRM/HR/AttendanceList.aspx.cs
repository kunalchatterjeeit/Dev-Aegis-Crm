using Business.Common;

using System;
using System.Data;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.HR
{
    public partial class AttendanceList : System.Web.UI.Page
    {
        
        private void Attendance_GetAll()
        {
            DataSet dsAttendance =
                new Business.HR.Attendance()
                .Attendance_GetAll(new Entity.HR.Attendance()
                {
                    EmployeeName = txtEmployeeName.Text.Trim(),
                    AttendanceFromDate = (string.IsNullOrEmpty(txtFromAttendanceDate.Text.Trim())) ? DateTime.MinValue : Convert.ToDateTime(txtFromAttendanceDate.Text.Trim()),
                    AttendanceToDate = (string.IsNullOrEmpty(txtToAttendanceDate.Text.Trim())) ? DateTime.MinValue : Convert.ToDateTime(txtToAttendanceDate.Text.Trim()),
                    PageIndex = gvAttendanceList.PageIndex,
                    PageSize = gvAttendanceList.PageSize
                });
            if (dsAttendance.Tables.Count > 1)
            {
                gvAttendanceList.DataSource = dsAttendance;
                gvAttendanceList.VirtualItemCount = (dsAttendance.Tables[1].Rows.Count > 0) ? Convert.ToInt32(dsAttendance.Tables[1].Rows[0]["TotalCount"].ToString()) : 10;
                gvAttendanceList.DataBind();
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                    Attendance_GetAll();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
            }
        }
        protected void gvAttendanceList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvAttendanceList.PageIndex = e.NewPageIndex;
                Attendance_GetAll();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Attendance_GetAll();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
            }
        }
    }
}