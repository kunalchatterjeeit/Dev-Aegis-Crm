using Business.Common;

using System;
using System.Data;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.HR
{
    public partial class ManageAttendance : System.Web.UI.Page
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
                {
                    MessageBox.Show = false;
                    Attendance_GetAll();
                }
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
        protected void gvAttendanceList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "MarkLate")
                {
                    HiddenField hdnMarkLate = (HiddenField)((GridViewRow)(((Button)e.CommandSource).NamingContainer)).FindControl("hdnMarkLate");
                    int response = new Business.HR.Attendance().Attendance_MarkLate(Convert.ToInt32(e.CommandArgument.ToString()), (hdnMarkLate.Value == "True") ? false : true);
                    if (response > 0)
                    {
                        MessageBox.IsSuccess = true;
                        MessageBox.Text = "Marked sucessfully.";
                        Attendance_GetAll();
                    }
                    else
                    {
                        MessageBox.IsSuccess = false;
                        MessageBox.Text = "Failed to mark.";
                    }

                }
                else if (e.CommandName == "MarkHalfDay")
                {
                    HiddenField hdnMarkHalfday = (HiddenField)((GridViewRow)((Button)e.CommandSource).NamingContainer).FindControl("hdnMarkHalfday");
                    int response = new Business.HR.Attendance().Attendance_MarkHalfday(Convert.ToInt32(e.CommandArgument.ToString()), (hdnMarkHalfday.Value == "True") ? false : true);
                    if (response > 0)
                    {
                        MessageBox.IsSuccess = true;
                        MessageBox.Text = "Marked sucessfully.";
                        Attendance_GetAll();
                    }
                    else
                    {
                        MessageBox.IsSuccess = false;
                        MessageBox.Text = "Failed to mark.";
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
                MessageBox.IsSuccess = false;
                MessageBox.Text = ex.Message;
            }
            finally
            {
                MessageBox.Show = true;
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
        protected void gvAttendanceList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    Button btnMarkLate = e.Row.FindControl("btnMarkLate") as Button;
                    HiddenField hdnMarkLate = e.Row.FindControl("hdnMarkLate") as HiddenField;
                    btnMarkLate.Text = (hdnMarkLate.Value == "True") ? "Mark Not Late" : "Mark Late";
                    btnMarkLate.CssClass = (hdnMarkLate.Value == "True") ? "btn btn-outline btn-success" : "btn btn-outline btn-danger";
                    btnMarkLate.ToolTip = (hdnMarkLate.Value == "True") ? "Click to mark as not late" : "Click to mark as late";

                    Button btnMarkHalfDay = e.Row.FindControl("btnMarkHalfDay") as Button;
                    HiddenField hdnMarkHalfday = e.Row.FindControl("hdnMarkHalfday") as HiddenField;
                    btnMarkHalfDay.Text = (hdnMarkHalfday.Value == "True") ? "Mark Full-Day" : "Mark Half-Day";
                    btnMarkHalfDay.CssClass = (hdnMarkHalfday.Value == "True") ? "btn btn-outline btn-success" : "btn btn-outline btn-danger";
                    btnMarkHalfDay.ToolTip = (hdnMarkHalfday.Value == "True") ? "Click to mark as full-day" : "Click to mark as half-day";
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
            }
        }
    }
}