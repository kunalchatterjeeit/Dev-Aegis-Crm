using Business.Common;
using log4net;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.ControlPanel
{
    public partial class CallAttendanceManager : System.Web.UI.Page
    {
        ILog logger = log4net.LogManager.GetLogger("ErrorLog");
        private void Service_ServiceCallAttendance_GetAll()
        {
            try
            {
                Business.Service.ServiceBook objService = new Business.Service.ServiceBook();
                Entity.Service.ServiceCallAttendance serviceCallAttendance = new Entity.Service.ServiceCallAttendance();
                serviceCallAttendance.RequestNo = (string.IsNullOrEmpty(txtDocketNo.Text.Trim()) ? string.Empty : txtDocketNo.Text.Trim());
                serviceCallAttendance.MachineId = (string.IsNullOrEmpty(txtMachineId.Text.Trim()) ? string.Empty : txtMachineId.Text.Trim());
                serviceCallAttendance.EmployeeId = Convert.ToInt32(ddlEngineer.SelectedValue);
                //serviceCallAttendance.FromDate = (string.IsNullOrEmpty(txtFromInDate.Text.Trim()) ? DateTime.MinValue : Convert.ToDateTime(txtFromInDate.Text.Trim()));
                //serviceCallAttendance.ToDate = (string.IsNullOrEmpty(txtToInDate.Text.Trim()) ? DateTime.MinValue : Convert.ToDateTime(txtToInDate.Text.Trim()));
                DataSet dsCallAttendance = objService.Service_ServiceCallAttendance_GetAll(serviceCallAttendance);
                gvCallAttendance.DataSource = dsCallAttendance.Tables[0];
                gvCallAttendance.DataBind();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
        private void LoadDocketCallStatus(DropDownList ddlDocketCallStatus)
        {
            Business.Service.CallStatus objCallStatus = new Business.Service.CallStatus();
            DataTable dt = objCallStatus.GetAll(2);
            if (dt != null)
            {
                ddlDocketCallStatus.DataSource = dt;
                ddlDocketCallStatus.DataTextField = "CallStatus";
                ddlDocketCallStatus.DataValueField = "CallStatusId";
                ddlDocketCallStatus.DataBind();
            }

            ddlDocketCallStatus.InsertSelect();
        }
        private void LoadTime(DropDownList ddlInHH, DropDownList ddlInMM, DropDownList ddlOutHH, DropDownList ddlOutMM, DropDownList ddlInTT, DropDownList ddlOutTT)
        {
            ddlInHH.Items.Clear();
            ddlInMM.Items.Clear();
            ddlInTT.Items.Clear();
            ddlOutTT.Items.Clear();
            ddlOutHH.Items.Clear();
            ddlOutMM.Items.Clear();

            ddlInHH.Items.Insert(0, "HH");
            ddlOutHH.Items.Insert(0, "HH");
            ddlInMM.Items.Insert(0, "MM");
            ddlOutMM.Items.Insert(0, "MM");
            ddlInTT.Items.Insert(0, "AM");
            ddlInTT.Items.Insert(1, "PM");
            ddlOutTT.Items.Insert(0, "AM");
            ddlOutTT.Items.Insert(1, "PM");

            for (int i = 0; i <= 12; i++)
            {
                ListItem li = new ListItem(i.ToString("00"), i.ToString("00"));
                ddlInHH.Items.Insert(i + 1, li);
            }
            for (int i = 0; i <= 12; i++)
            {
                ListItem li = new ListItem(i.ToString("00"), i.ToString("00"));
                ddlOutHH.Items.Insert(i + 1, li);
            }

            for (int i = 0; i <= 59; i++)
            {
                ListItem li = new ListItem(i.ToString("00"), i.ToString("00"));
                ddlInMM.Items.Insert(i + 1, li);
            }
            for (int i = 0; i <= 59; i++)
            {
                ListItem li = new ListItem(i.ToString("00"), i.ToString("00"));
                ddlOutMM.Items.Insert(i + 1, li);
            }
            ddlInTT.SelectedIndex = 0;
            ddlOutTT.SelectedIndex = 0;
        }
        private void EmployeeMaster_GetAll()
        {
            Business.HR.EmployeeMaster ObjBelEmployeeMaster = new Business.HR.EmployeeMaster();
            Entity.HR.EmployeeMaster ObjElEmployeeMaster = new Entity.HR.EmployeeMaster();
            ObjElEmployeeMaster.CompanyId_FK = 1;
            DataTable dt = ObjBelEmployeeMaster.Employee_GetAll_Active(ObjElEmployeeMaster);
            dt = dt.Select("DesignationMasterId IN (1,3)").CopyToDataTable();
            ddlEngineer.DataSource = dt;
            ddlEngineer.DataTextField = "EmployeeName";
            ddlEngineer.DataValueField = "EmployeeMasterId";
            ddlEngineer.DataBind();
            ddlEngineer.InsertSelect();
        }
        private void ClearControls()
        {
            Message.Show = false;
            txtDocketNo.Text = string.Empty;
            //txtFromInDate.Text = string.Empty;
            txtMachineId.Text = string.Empty;
            //txtToInDate.Text = string.Empty;
            ddlEngineer.SelectedIndex = 0;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Message.Show = false;
                    EmployeeMaster_GetAll();
                    Service_ServiceCallAttendance_GetAll();
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }

        protected void gvCallAttendance_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Business.Service.ServiceBook objServiceBook = new Business.Service.ServiceBook();

                if (e.CommandName == "E")
                {
                    Entity.Service.ServiceCallAttendance serviceCallAttendance = new Entity.Service.ServiceCallAttendance();

                    GridViewRow gvCallAttendanceRow = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                    TextBox txtInDate = (TextBox)gvCallAttendanceRow.FindControl("txtInDate");
                    DropDownList ddlInTimeHH = (DropDownList)gvCallAttendanceRow.FindControl("ddlInTimeHH");
                    DropDownList ddlInTimeMM = (DropDownList)gvCallAttendanceRow.FindControl("ddlInTimeMM");
                    DropDownList ddlInTimeTT = (DropDownList)gvCallAttendanceRow.FindControl("ddlInTimeTT");
                    TextBox txtOutDate = (TextBox)gvCallAttendanceRow.FindControl("txtOutDate");
                    DropDownList ddlOutTimeHH = (DropDownList)gvCallAttendanceRow.FindControl("ddlOutTimeHH");
                    DropDownList ddlOutTimeMM = (DropDownList)gvCallAttendanceRow.FindControl("ddlOutTimeMM");
                    DropDownList ddlOutTimeTT = (DropDownList)gvCallAttendanceRow.FindControl("ddlOutTimeTT");
                    DropDownList ddlDocketCallStatus = (DropDownList)gvCallAttendanceRow.FindControl("ddlDocketCallStatus");

                    serviceCallAttendance.ServiceCallAttendanceId = int.Parse(e.CommandArgument.ToString());
                    serviceCallAttendance.CallStatusId = int.Parse(ddlDocketCallStatus.SelectedValue);
                    serviceCallAttendance.InTime = Convert.ToDateTime(txtInDate.Text + " " + ddlInTimeHH.SelectedValue + ":" + ddlInTimeMM.SelectedValue + ":00" + " " + ddlInTimeTT.SelectedValue);
                    if (!string.IsNullOrEmpty(txtOutDate.Text.Trim()) && ddlOutTimeHH.SelectedIndex != 0 && ddlOutTimeMM.SelectedIndex != 0)
                        serviceCallAttendance.OutTime = (Request.QueryString["action"] != null && Request.QueryString["action"].Equals("callin")) ? DateTime.MinValue : Convert.ToDateTime(txtOutDate.Text + " " + ddlOutTimeHH.SelectedValue + ":" + ddlOutTimeMM.SelectedValue + ":00" + " " + ddlOutTimeTT.SelectedValue);

                    int response = objServiceBook.Service_CallAttendance_Edit(serviceCallAttendance);
                    if (response > 0)
                    {
                        ClearControls();
                        Message.IsSuccess = true;
                        Message.Text = "Call attendance record is updated.";
                    }
                    else
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Sorry! Call attendance record is not updated.";
                    }
                    Message.Show = true;

                }
                else if (e.CommandName == "D")
                {
                    int response = objServiceBook.Service_CallAttendance_Delete(int.Parse(e.CommandArgument.ToString()));
                    if (response > 0)
                    {
                        Service_ServiceCallAttendance_GetAll();
                        Message.IsSuccess = true;
                        Message.Text = "Call attendance record is deleted.";
                    }
                    else
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Sorry! Call attendance record is not deleted.";
                    }
                    Message.Show = true;
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }

        protected void gvCallAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    TextBox txtInDate = (TextBox)e.Row.FindControl("txtInDate");
                    TextBox txtOutDate = (TextBox)e.Row.FindControl("txtOutDate");
                    DropDownList ddlInTimeHH = (DropDownList)e.Row.FindControl("ddlInTimeHH");
                    DropDownList ddlInTimeMM = (DropDownList)e.Row.FindControl("ddlInTimeMM");
                    DropDownList ddlInTimeTT = (DropDownList)e.Row.FindControl("ddlInTimeTT");
                    DropDownList ddlOutTimeHH = (DropDownList)e.Row.FindControl("ddlOutTimeHH");
                    DropDownList ddlOutTimeMM = (DropDownList)e.Row.FindControl("ddlOutTimeMM");
                    DropDownList ddlOutTimeTT = (DropDownList)e.Row.FindControl("ddlOutTimeTT");
                    DropDownList ddlDocketCallStatus = (DropDownList)e.Row.FindControl("ddlDocketCallStatus");

                    LoadDocketCallStatus(ddlDocketCallStatus);
                    ddlDocketCallStatus.SelectedValue = (((DataTable)(gvCallAttendance.DataSource)).Rows[e.Row.RowIndex]["CallStatus"] != null) ? ((DataTable)(gvCallAttendance.DataSource)).Rows[e.Row.RowIndex]["CallStatus"].ToString() : "0";

                    LoadTime(ddlInTimeHH, ddlInTimeMM, ddlOutTimeHH, ddlOutTimeMM, ddlInTimeTT, ddlOutTimeTT);

                    if (((DataTable)(gvCallAttendance.DataSource)).Rows[e.Row.RowIndex]["InTime"] != null && !string.IsNullOrEmpty(((DataTable)(gvCallAttendance.DataSource)).Rows[e.Row.RowIndex]["InTime"].ToString()))
                    {
                        txtInDate.Text = Convert.ToDateTime(((DataTable)(gvCallAttendance.DataSource)).Rows[e.Row.RowIndex]["InTime"].ToString()).ToString("dd MMM yyyy");

                        if (Convert.ToInt32(Convert.ToDateTime(((DataTable)(gvCallAttendance.DataSource)).Rows[e.Row.RowIndex]["InTime"].ToString()).ToString("HH")) > 12)
                            ddlInTimeHH.SelectedValue = (Convert.ToInt32(Convert.ToDateTime(((DataTable)(gvCallAttendance.DataSource)).Rows[e.Row.RowIndex]["InTime"].ToString()).ToString("HH")) - 12).ToString("00");
                        else
                            ddlInTimeHH.SelectedValue = Convert.ToDateTime(((DataTable)(gvCallAttendance.DataSource)).Rows[e.Row.RowIndex]["InTime"].ToString()).ToString("HH");
                        ddlInTimeMM.SelectedValue = Convert.ToDateTime(((DataTable)(gvCallAttendance.DataSource)).Rows[e.Row.RowIndex]["InTime"].ToString()).ToString("mm");
                        ddlInTimeTT.SelectedValue = Convert.ToDateTime(((DataTable)(gvCallAttendance.DataSource)).Rows[e.Row.RowIndex]["InTime"].ToString()).ToString("tt");
                    }
                    if (((DataTable)(gvCallAttendance.DataSource)).Rows[e.Row.RowIndex]["OutTime"] != null && !string.IsNullOrEmpty(((DataTable)(gvCallAttendance.DataSource)).Rows[e.Row.RowIndex]["OutTime"].ToString()))
                    {
                        txtOutDate.Text = Convert.ToDateTime(((DataTable)(gvCallAttendance.DataSource)).Rows[e.Row.RowIndex]["OutTime"].ToString()).ToString("dd MMM yyyy");

                        if (Convert.ToInt32(Convert.ToDateTime(((DataTable)(gvCallAttendance.DataSource)).Rows[e.Row.RowIndex]["OutTime"].ToString()).ToString("HH")) > 12)
                            ddlOutTimeHH.SelectedValue = (Convert.ToInt32(Convert.ToDateTime(((DataTable)(gvCallAttendance.DataSource)).Rows[e.Row.RowIndex]["OutTime"].ToString()).ToString("HH")) - 12).ToString("00");
                        else
                            ddlOutTimeHH.SelectedValue = Convert.ToDateTime(((DataTable)(gvCallAttendance.DataSource)).Rows[e.Row.RowIndex]["OutTime"].ToString()).ToString("HH");
                        ddlOutTimeMM.SelectedValue = Convert.ToDateTime(((DataTable)(gvCallAttendance.DataSource)).Rows[e.Row.RowIndex]["OutTime"].ToString()).ToString("mm");
                        ddlOutTimeTT.SelectedValue = Convert.ToDateTime(((DataTable)(gvCallAttendance.DataSource)).Rows[e.Row.RowIndex]["OutTime"].ToString()).ToString("tt");
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }

        protected void gvCallAttendance_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCallAttendance.PageIndex = e.NewPageIndex;
                Service_ServiceCallAttendance_GetAll();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Service_ServiceCallAttendance_GetAll();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
    }
}