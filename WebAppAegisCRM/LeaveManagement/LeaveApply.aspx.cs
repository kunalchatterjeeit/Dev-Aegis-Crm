using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Business.Common;
using System.Drawing;
using System.Windows;
using System.Threading;







namespace WebAppAegisCRM.LeaveManagement
{
    public partial class LeaveApply : System.Web.UI.Page
    {
        public int LeaveApplyId
        {
            get { return Convert.ToInt16(ViewState["LeaveApplyId"]); }
            set { ViewState["LeaveApplyId"] = value;
         
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadLeaveType();
                Business.Common.Context.SelectedDates.Clear();
            }


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
        public static List<DateTime> list = new List<DateTime>();

        protected void btnSave_Click(object sender, EventArgs e)
        {

           
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Business.Common.Context.SelectedDates.Clear();
            Calendar1.SelectedDates.Clear();
        }



        public void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date < DateTime.Now.Date)
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.Red;
                e.Cell.Font.Bold = true;
                e.Cell.Font.Strikeout = true;
                
            }
            if (Business.Common.Context.SelectedDates.Any())
            {
                foreach (DateTime dt in Business.Common.Context.SelectedDates)
                {
                    Calendar1.SelectedDates.Add(dt);
                }
            }


        }

       
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {

            /* if (Session["SelectedDates"] != null)
              {

                  List<DateTime> newList = (List<DateTime>)Session["SelectedDates"];
                  foreach (DateTime dt in newList)
                  {
                      if (Calendar1.SelectedDates.Contains(dt) || Calendar1.SelectedDate == dt)
                      {
                          Calendar1.SelectedDates.Remove(dt);
                      }
                      else
                      {
                           Calendar1.SelectedDates.Add(dt);
                          lbFromDate.Text = Calendar1.SelectedDate.ToString("dd-MMM-yyyy");
                          lbToDate.Text = Calendar1.SelectedDate.ToString("dd-MMM-yyyy");

                          DateTime rangeStart = Convert.ToDateTime(lbFromDate.Text.Trim());
                          DateTime rangeEnd = Convert.ToDateTime(lbFromDate.Text.Trim());
                          TimeSpan TotalCount = rangeEnd - rangeStart;

                      }
                  }

              }*/
            Calendar calender = ((Calendar)sender);

            if (calender.ValidateContinueSelection())
            {
                if (!Business.Common.Context.SelectedDates.Contains(calender.SelectedDate))
                {
                    List<DateTime> lists = Business.Common.Context.SelectedDates;
                    lists.Add(calender.SelectedDate);
                    Business.Common.Context.SelectedDates = lists;
                }
            }
            else
            {
                Business.Common.Context.SelectedDates.Clear();
                calender.SelectedDates.Clear();
            }


            if (Business.Common.Context.SelectedDates.Any())
            {
                lbFromDate.Text = Business.Common.Context.SelectedDates.Min().ToString("dd MMM yyyy");
                lbToDate.Text = Business.Common.Context.SelectedDates.Max().ToString("dd MMM yyyy");
                lbTotalCount.Text = ((Business.Common.Context.SelectedDates.Max() - Business.Common.Context.SelectedDates.Min()).TotalDays + 1).ToString();
            }
            else
            {
                lbFromDate.Text = string.Empty;
                lbToDate.Text = string.Empty;
                lbTotalCount.Text = string.Empty;
            }
        }
    }

    }
}