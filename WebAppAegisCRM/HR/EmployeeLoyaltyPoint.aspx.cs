using Business.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.HR
{
    public partial class EmployeeLoyaltyPoint : System.Web.UI.Page
    {
        private void EmployeeLoyaltyPoint_GetAll()
        {
            DataTable dtEmployeePoint = new Business.HR.EmployeeLoyaltyPoint().EmployeeLoyaltyPoint_GetAll(ddlMonth.SelectedValue, int.Parse(ddlYear.SelectedValue));
            gvEmployeePoint.DataSource = dtEmployeePoint;
            gvEmployeePoint.DataBind();
        }
        private void LoadYear()
        {
            ddlYear.Items.Clear();
            for (int i = 2015; i <= 2025; i++)
            {
                ListItem li = new ListItem(i.ToString(), i.ToString());
                ddlYear.Items.Insert(i - 2015, li);
            }
            ddlYear.InsertSelect();
            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            ddlMonth.SelectedValue = DateTime.Now.ToString("MMM");
        }
        private bool Validation(int reasonId)
        {
            if (ddlMonth.SelectedIndex == 0)
            {
                Message1.IsSuccess = false;
                Message1.Text = "Please select month.";
                Message1.Show = true;
                return false;
            }
            if (ddlYear.SelectedIndex == 0)
            {
                Message1.IsSuccess = false;
                Message1.Text = "Please select year.";
                Message1.Show = true;
                return false;
            }
            if (reasonId == 0)
            {
                Message1.IsSuccess = false;
                Message1.Text = "Please select loyalty reason.";
                Message1.Show = true;
                return false;
            }

            return true;
        }
        protected void LoadLoyaltyPointReasonMaster(DropDownList ddlReason, int designationId)
        {
            DataTable dt = GlobalCache.ExecuteCache<DataTable>(typeof(Business.HR.LoyaltyPointReasonMaster), "GetAll", new Entity.HR.LoyaltyPointReasonMaster());
            using (DataView dv = new DataView(dt))
            {
                dv.RowFilter = "DesignationMasterId = " + designationId + "";
                dt = dv.ToTable();
            }

            ddlReason.DataSource = dt;
            ddlReason.DataTextField = "Reason";
            ddlReason.DataValueField = "LoyaltyPointReasonId";
            ddlReason.DataBind();
            ddlReason.InsertSelect();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Message1.Show = false;
                LoadYear();
                EmployeeLoyaltyPoint_GetAll();
            }
        }

        protected void gvEmployeePoint_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Business.HR.EmployeeLoyaltyPoint objEmployeeLoyaltyPoint = new Business.HR.EmployeeLoyaltyPoint();

            if (e.CommandName == "E")
            {

                Entity.HR.EmployeeLoyaltyPoint employeeLoyaltyPoint = new Entity.HR.EmployeeLoyaltyPoint();

                GridViewRow gvEmployeePoint = (GridViewRow)(((LinkButton)e.CommandSource).NamingContainer);

                DropDownList ddlLaoyalPointReason = (DropDownList)gvEmployeePoint.FindControl("ddlLaoyalPointReason");
                TextBox txtPoint = (TextBox)gvEmployeePoint.FindControl("txtPoint");
                TextBox txtNote = (TextBox)gvEmployeePoint.FindControl("txtNote");


                employeeLoyaltyPoint.EmployeeId = int.Parse(e.CommandArgument.ToString());
                employeeLoyaltyPoint.Month = ddlMonth.SelectedValue;
                employeeLoyaltyPoint.Year = int.Parse(ddlYear.SelectedValue);
                employeeLoyaltyPoint.LoyaltyPointReasonId = int.Parse(ddlLaoyalPointReason.SelectedValue);
                employeeLoyaltyPoint.Point = (!string.IsNullOrEmpty(txtPoint.Text)) ? decimal.Parse(txtPoint.Text) : 0;
                employeeLoyaltyPoint.Note = txtNote.Text;
                employeeLoyaltyPoint.CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name);

                if (Validation(employeeLoyaltyPoint.LoyaltyPointReasonId))
                {
                    int response = objEmployeeLoyaltyPoint.EmployeeLoyaltyPoint_Save(employeeLoyaltyPoint);
                    if (response > 0)
                    {
                        Message1.IsSuccess = true;
                        Message1.Text = "Employee Loyalty point is updated.";
                    }
                    else
                    {
                        Message1.IsSuccess = false;
                        Message1.Text = "Sorry! Employee Loyalty point is not updated.";
                    }
                    Message1.Show = true;
                }

            }
            else if (e.CommandName == "D")
            {
                GridViewRow gvEmployeePointRow = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);
                long loyaltyId = 0;
                if (long.TryParse(gvEmployeePoint.DataKeys[gvEmployeePointRow.RowIndex].Values[0].ToString(), out loyaltyId))
                {
                    int response = objEmployeeLoyaltyPoint.EmployeeLoyaltyPoint_Delete(loyaltyId);
                    if (response > 0)
                    {
                        EmployeeLoyaltyPoint_GetAll();
                        Message1.IsSuccess = true;
                        Message1.Text = "Employee Loyalty point is deleted.";
                    }
                    else
                    {
                        Message1.IsSuccess = false;
                        Message1.Text = "Sorry! Employee Loyalty point is not deleted.";
                    }
                }
                Message1.Show = true;
            }
        }

        protected void gvEmployeePoint_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DropDownList ddlLoyaltyPointReason = (DropDownList)e.Row.FindControl("ddlLaoyalPointReason");
                LoadLoyaltyPointReasonMaster(ddlLoyaltyPointReason, int.Parse(((DataTable)(gvEmployeePoint.DataSource)).Rows[e.Row.RowIndex]["DesignationMasterId"].ToString()));
                TextBox txtPoint = (TextBox)e.Row.FindControl("txtPoint");
                TextBox txtNote = (TextBox)e.Row.FindControl("txtNote");

                ddlLoyaltyPointReason.SelectedValue = ((DataTable)(gvEmployeePoint.DataSource)).Rows[e.Row.RowIndex]["LoyaltyPointReasonId"].ToString();
                txtPoint.Text = ((DataTable)(gvEmployeePoint.DataSource)).Rows[e.Row.RowIndex]["Point"].ToString();
                txtNote.Text = ((DataTable)(gvEmployeePoint.DataSource)).Rows[e.Row.RowIndex]["Note"].ToString();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            EmployeeLoyaltyPoint_GetAll();
        }

        protected void gvEmployeePoint_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvEmployeePoint.PageIndex = e.NewPageIndex;
            EmployeeLoyaltyPoint_GetAll();
        }
    }
}