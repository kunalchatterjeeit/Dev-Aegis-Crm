using Business.Common;
using Entity.Common;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.LeaveManagement
{
    public partial class GenerateLeave : System.Web.UI.Page
    {
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

        private void LoadMonths()
        {
            var months = Enumerable.Range(1, 12).Select(i => new { I = i, M = DateTimeFormatInfo.CurrentInfo.GetMonthName(i) });
            ddlMonths.DataSource = months;
            ddlMonths.DataTextField = "M";
            ddlMonths.DataValueField = "M";
            ddlMonths.DataBind();
            ddlMonths.InsertSelect();
        }

        private void LoadQuarters()
        {
            ddlQuarters.DataSource = Enum.GetValues(typeof(QuarterlyEnum));
            ddlQuarters.DataBind();
            ddlQuarters.InsertSelect();
        }

        private void LoadYears()
        {
            ddlYears.Items.Clear();
            for (int i = 2015; i <= 2025; i++)
            {
                ListItem li = new ListItem(i.ToString(), i.ToString());
                ddlYears.Items.Insert(i - 2015, li);
            }
            ddlYears.InsertSelect();
            ddlYears.SelectedValue = DateTime.Now.Year.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadLeaveType();
                LoadMonths();
                LoadQuarters();
                LoadYears();
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {

        }

        protected void ddlLeaveType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlMonths.SelectedIndex = 0;
            ddlQuarters.SelectedIndex = 0;
            DataTable dtLeaveConfigurations = GlobalCache.ExecuteCache<DataTable>(typeof(Business.LeaveManagement.LeaveConfiguration), "LeaveConfigurations_GetAll", new Entity.LeaveManagement.LeaveConfiguration() { });
            if (dtLeaveConfigurations != null)
            {
                if (dtLeaveConfigurations.Select("LeaveTypeId = " + ddlLeaveType.SelectedValue).Any())
                {
                    if (dtLeaveConfigurations.Select("LeaveTypeId = " + ddlLeaveType.SelectedValue).FirstOrDefault()["LeaveFrequency"].ToString() == LeaveFrequencyEnum.MONTHLY.ToString())
                    {
                        ddlQuarters.Enabled = false;
                        ddlMonths.Enabled = true;
                    }
                    else if (dtLeaveConfigurations.Select("LeaveTypeId = " + ddlLeaveType.SelectedValue).FirstOrDefault()["LeaveFrequency"].ToString() == LeaveFrequencyEnum.QUARTERLY.ToString())
                    {
                        ddlQuarters.Enabled = true;
                        ddlMonths.Enabled = false;
                    }
                    else
                    {
                        ddlQuarters.Enabled = false;
                        ddlMonths.Enabled = false;
                    }
                }
                else
                {
                    ddlQuarters.Enabled = true;
                    ddlMonths.Enabled = true;
                }
            }
        }
    }
}