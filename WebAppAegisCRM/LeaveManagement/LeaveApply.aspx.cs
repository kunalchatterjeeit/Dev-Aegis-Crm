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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadLeaveType();
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
            Calendar1.SelectedDates.Clear();
        }



        public void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date < DateTime.Now.Date)
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.Red;
                e.Cell.Font.Strikeout = true;
            }
            if (e.Day.IsSelected == true)
            {
                list.Add(e.Day.Date);
            }
            Session["SelectedDates"] = list;


        }

       
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            // lbFromDate.Text = Calendar1.SelectedDate.ToString("dd-MMM-yyyy");

            // lbToDate.Text = Calendar1.SelectedDate.ToString("dd-MMM-yyyy");

            //   DateTime a  = Calendar1.SelectedDate.ToString("dd-MMM-yyyy");

            // calendar1.AllowMultipleSelect = true;

            // lbFromDate.Text = Calendar1.SelectedDates[0].ToShortDateString();

            // lbTotalCount.Text = Calendar1.SelectedDates[Calendar1.SelectedDates.Count - 1].ToShortDateString();

            ////////////////////////////
            // for(int i=0;i<=Calendar1.SelectedDates.Count;i++)
            //  {
            //      lbTotalCount.Text += Calendar1.SelectedDates[Calendar1.SelectedDates.Count - 1].ToShortDateString();
            // }
            /////////////////////////////////
          //  if (Session["SelectedDates"] != null)
          /*  {
                List<DateTime> newList = (List<DateTime>)Session["SelectedDates"];
                foreach (DateTime dt in newList)
                {
                    Calendar1.SelectedDates.Add(dt);
                }
                list.Clear();

            }*/

            if (Session["SelectedDates"] != null)
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
                    }
                }
            }




            //lbFromDate.Text = Calendar1.SelectedDate.ToString("dd-MMM-yyyy");
            // DateTime rangeStart = Convert.ToDateTime(lbFromDate.Text.Trim());



            // DateTime rangeStart = new DateTime(2018, 11, 7);
            //
            // lbFromDate.Text = rangeStart.ToString("dd-MMM-yyyy");
            //lbToDate.Text = rangeEnd.ToString("dd-MMM-yyyy");
            //TimeSpan TotalCount = rangeEnd - rangeStart;
            //lbTotalCount.Text = TotalCount.ToString("dd");

            /* if (e.Day.Date < rangeStart || e.Day.Date > rangeEnd)
             {
                 e.Day.IsSelectable = false;
                 e.Cell.ForeColor = System.Drawing.Color.Gray;

             }*/


            /*  DateTime df = new DateTime(2018, 11, 7);
              DateTime temp = df;

              while (temp < df)
              {
                  temp = temp.AddDays(1);
                  // do something with inbetween date here...
              }*/




        }
      
    }
}