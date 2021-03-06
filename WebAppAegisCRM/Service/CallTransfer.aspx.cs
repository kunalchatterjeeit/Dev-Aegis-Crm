﻿using Business.Common;
using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Service
{
    public partial class CallTransfer : Page
    {
        public int EmployeeId
        {
            get { return Convert.ToInt32(ViewState["EmployeeId"]); }
            set { ViewState["EmployeeId"] = value; }
        }

        protected void EmployeeMaster_GetAll()
        {
            Business.HR.EmployeeMaster ObjBelEmployeeMaster = new Business.HR.EmployeeMaster();
            Entity.HR.EmployeeMaster ObjElEmployeeMaster = new Entity.HR.EmployeeMaster();
            ObjElEmployeeMaster.CompanyId_FK = 1;
            DataTable dt = ObjBelEmployeeMaster.Employee_GetAll_Active(ObjElEmployeeMaster);
            dt = dt.Select("DesignationMasterId IN (1,3)").CopyToDataTable();
            if (dt.Rows.Count > 0)
                gvEmployeerMaster.DataSource = dt;
            else
                gvEmployeerMaster.DataSource = null;
            gvEmployeerMaster.DataBind();
        }

        private bool ValidateCallTransfer()
        {
            bool retValue = false;

            Business.Service.ServiceBook objServiceBook = new Business.Service.ServiceBook();
            DataSet dsServiceMaster = objServiceBook.Service_ServiceBookMaster_GetByCallId(Business.Common.Context.CallId, Business.Common.Context.CallType);
            if (dsServiceMaster != null && dsServiceMaster.Tables.Count > 0 && dsServiceMaster.Tables[0].AsEnumerable().Any())
            {
                DataTable dtServiceCallAttendance = objServiceBook.Service_ServiceCallAttendanceByServiceBookId(Convert.ToInt64(dsServiceMaster.Tables[0].Rows[0]["ServiceBookId"].ToString()));
                if (dtServiceCallAttendance != null)
                {
                    if (dtServiceCallAttendance.Select("OutTime is NULL").Any())
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Call Transfer is not allowed while Call Checked In.";
                        retValue = false;
                    }
                    else
                    {
                        retValue = true;
                    }
                }
            }
            else
            {
                retValue = true;
            }
            return retValue;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Message.Show = false;
                EmployeeMaster_GetAll();
            }
        }

        protected void rbtnSelect_CheckedChanged(object sender, EventArgs e)
        {
            //Clear the existing selected row 
            foreach (GridViewRow oldrow in gvEmployeerMaster.Rows)
            {
                ((RadioButton)oldrow.FindControl("rbtnSelect")).Checked = false;
            }

            //Set the new selected row
            RadioButton rb = (RadioButton)sender;
            GridViewRow row = (GridViewRow)rb.NamingContainer;
            ((RadioButton)row.FindControl("rbtnSelect")).Checked = true;

            EmployeeId = Convert.ToInt32(gvEmployeerMaster.DataKeys[row.RowIndex].Values[0].ToString());

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateCallTransfer())
                {
                    Business.Service.ServiceBook objServiceBook = new Business.Service.ServiceBook();
                    Entity.Service.ServiceBook serviceBook = new Entity.Service.ServiceBook();
                    serviceBook.EmployeeId_FK = EmployeeId;
                    serviceBook.CallId = Business.Common.Context.CallId;
                    serviceBook.CallType = (int)Business.Common.Context.CallType;
                    serviceBook.Remarks = txtTransferReason.Text.Trim();
                    serviceBook.CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
                    int response = objServiceBook.Service_CallTransfer_Save(serviceBook);
                    if (response > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertUser", "refreshAndClose();", true);
                    }
                    else
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Sorry! cannot tranfer call. Please refresh this page and try again..";
                    }
                }
            }
            catch (Exception ex)
            {
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                ex.WriteException();
            }
            Message.Show = true;
        }
    }
}
