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
                LeaveGenerateLog_GetAll();
                Message.Show = false;
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtLeaveConfigurations = GlobalCache.ExecuteCache<DataTable>(typeof(Business.LeaveManagement.LeaveConfiguration), "LeaveConfigurations_GetAll", new Entity.LeaveManagement.LeaveConfiguration() { });
                if (dtLeaveConfigurations != null)
                {
                    if (dtLeaveConfigurations.Select("LeaveTypeId = " + ddlLeaveType.SelectedValue).Any())
                    {
                        if (dtLeaveConfigurations.Select("LeaveTypeId = " + ddlLeaveType.SelectedValue).FirstOrDefault()["LeaveFrequency"].ToString() == LeaveFrequencyEnum.MONTHLY.ToString())
                        {
                            GenerateMonthly();
                        }
                        else if (dtLeaveConfigurations.Select("LeaveTypeId = " + ddlLeaveType.SelectedValue).FirstOrDefault()["LeaveFrequency"].ToString() == LeaveFrequencyEnum.QUARTERLY.ToString())
                        {
                            GenerateQuarterly();
                        }
                        else if (dtLeaveConfigurations.Select("LeaveTypeId = " + ddlLeaveType.SelectedValue).FirstOrDefault()["LeaveFrequency"].ToString() == LeaveFrequencyEnum.YEARLY.ToString())
                        {
                            GenerateYearly();
                        }
                    }
                }
                Message.IsSuccess = true;
                Message.Text = "Processing completed.";
            }
            catch (Exception ex)
            {
                ex.WriteException();
                Message.IsSuccess = false;
                Message.Text = ex.Message;
            }
            Message.Show = true;
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

        private void GenerateMonthly()
        {
            Business.LeaveManagement.LeaveGenerateLog objLeaveGenerateLog = new Business.LeaveManagement.LeaveGenerateLog();
            Entity.LeaveManagement.LeaveGenerateLog leaveGenerateLog = new Entity.LeaveManagement.LeaveGenerateLog();

            leaveGenerateLog.LeaveTypeId = Convert.ToInt32(ddlLeaveType.SelectedValue);
            leaveGenerateLog.Month = ddlMonths.SelectedValue;
            leaveGenerateLog.Year = Convert.ToInt32(ddlYears.SelectedValue);

            DataTable dtLeaveGenerate = objLeaveGenerateLog.LeaveGenerateLog_GetAll(leaveGenerateLog);

            if (dtLeaveGenerate != null && dtLeaveGenerate.AsEnumerable().Any())
            {
                //Leave Type ddlLeaveType.SelectedItem is already generated for ddlMonths.SelectedItem, ddlYears.SelectedItem
                return;
            }
            //Generate Leave
            long leaveGenerateLogId = LeaveGenerateLog_Save();
            int totalCount = Generate();
            LeaveGenerateLog_Update(leaveGenerateLogId, totalCount);
        }

        private void GenerateQuarterly()
        {
            Business.LeaveManagement.LeaveGenerateLog objLeaveGenerateLog = new Business.LeaveManagement.LeaveGenerateLog();
            Entity.LeaveManagement.LeaveGenerateLog leaveGenerateLog = new Entity.LeaveManagement.LeaveGenerateLog();

            leaveGenerateLog.LeaveTypeId = Convert.ToInt32(ddlLeaveType.SelectedValue);
            leaveGenerateLog.Quarter = ddlQuarters.SelectedValue;
            leaveGenerateLog.Year = Convert.ToInt32(ddlYears.SelectedValue);

            DataTable dtLeaveGenerate = objLeaveGenerateLog.LeaveGenerateLog_GetAll(leaveGenerateLog);

            if (dtLeaveGenerate != null && dtLeaveGenerate.AsEnumerable().Any())
            {
                //Leave Type ddlLeaveType.SelectedItem is already generated for ddlQuarters.SelectedItem, ddlYears.SelectedItem
                return;
            }
            //Generate Leave
            long leaveGenerateLogId = LeaveGenerateLog_Save();
            int totalCount = Generate();
            LeaveGenerateLog_Update(leaveGenerateLogId, totalCount);
        }

        private void GenerateYearly()
        {
            Business.LeaveManagement.LeaveGenerateLog objLeaveGenerateLog = new Business.LeaveManagement.LeaveGenerateLog();
            Entity.LeaveManagement.LeaveGenerateLog leaveGenerateLog = new Entity.LeaveManagement.LeaveGenerateLog();

            leaveGenerateLog.LeaveTypeId = Convert.ToInt32(ddlLeaveType.SelectedValue);
            leaveGenerateLog.Year = Convert.ToInt32(ddlYears.SelectedValue);

            DataTable dtLeaveGenerate = objLeaveGenerateLog.LeaveGenerateLog_GetAll(leaveGenerateLog);

            if (dtLeaveGenerate != null && dtLeaveGenerate.AsEnumerable().Any())
            {
                //Leave Type ddlLeaveType.SelectedItem is already generated for ddlYears.SelectedItem
                return;
            }
            //Generate Leave
            long leaveGenerateLogId = LeaveGenerateLog_Save();
            int totalCount = Generate();
            LeaveGenerateLog_Update(leaveGenerateLogId, totalCount);
        }

        private long LeaveGenerateLog_Save()
        {
            Business.LeaveManagement.LeaveGenerateLog objLeaveGenerateLog = new Business.LeaveManagement.LeaveGenerateLog();
            Entity.LeaveManagement.LeaveGenerateLog leaveGenerateLog = new Entity.LeaveManagement.LeaveGenerateLog();

            leaveGenerateLog.LeaveGenerateLogId = 0;
            leaveGenerateLog.LeaveTypeId = Convert.ToInt32(ddlLeaveType.SelectedValue);
            leaveGenerateLog.Month = ddlMonths.SelectedValue;
            leaveGenerateLog.Quarter = ddlQuarters.SelectedValue;
            leaveGenerateLog.Year = Convert.ToInt32(ddlYears.SelectedValue);
            leaveGenerateLog.ScheduleDateTime = DateTime.Now;

            long leaveGenerateLogId = objLeaveGenerateLog.LeaveGenerateLog_Save(leaveGenerateLog);
            return leaveGenerateLogId;
        }

        private long LeaveGenerateLog_Update(long leaveGenerateLogId, int totalCount)
        {
            Business.LeaveManagement.LeaveGenerateLog objLeaveGenerateLog = new Business.LeaveManagement.LeaveGenerateLog();
            Entity.LeaveManagement.LeaveGenerateLog leaveGenerateLog = new Entity.LeaveManagement.LeaveGenerateLog();

            leaveGenerateLog.LeaveGenerateLogId = leaveGenerateLogId;
            leaveGenerateLog.TotalDistribution = totalCount;

            return objLeaveGenerateLog.LeaveGenerateLog_Save(leaveGenerateLog);
        }

        private int Generate()
        {
            int totalCount = 0;
            DataTable dtLeaveConfigurations = GlobalCache.ExecuteCache<DataTable>(typeof(Business.LeaveManagement.LeaveConfiguration), "LeaveConfigurations_GetAll", new Entity.LeaveManagement.LeaveConfiguration() { });
            if (dtLeaveConfigurations != null)
            {
                if (dtLeaveConfigurations.Select("LeaveTypeId = " + ddlLeaveType.SelectedValue).Any())
                {
                    DataRow drLeaveConfiguration = dtLeaveConfigurations.Select("LeaveTypeId = " + ddlLeaveType.SelectedValue).FirstOrDefault();
                    if (drLeaveConfiguration != null)
                    {
                        //Getting all Employees
                        DataTable dtEmployees = new Business.HR.EmployeeMaster().EmployeeMaster_GetAll(new Entity.HR.EmployeeMaster() { CompanyId_FK = 1 });
                        //Getting all Designation wise Leave Configurations
                        DataTable dtLeaveDesignationConfigurations =
                            new Business.LeaveManagement.LeaveDesignationWiseConfiguration().LeaveDesignationConfig_GetAll(new Entity.LeaveManagement.LeaveDesignationWiseConfiguration());

                        if (dtLeaveDesignationConfigurations != null && dtLeaveDesignationConfigurations.AsEnumerable().Any())
                        {
                            foreach (DataRow drEmployee in dtEmployees.Rows)
                            {
                                //Getting Designation of each employee
                                int designationId = Convert.ToInt32(drEmployee["DesignationMasterId_FK"].ToString());
                                int leaveTypeId = Convert.ToInt32(ddlLeaveType.SelectedValue);
                                DataRow drLeaveDesignationConfiguration = dtLeaveDesignationConfigurations
                                    .Select("DesignationId = " + designationId + " AND LeaveTypeId = " + leaveTypeId.ToString()).FirstOrDefault();
                                if (drLeaveDesignationConfiguration != null)
                                {
                                    decimal leaveAmount = Convert.ToDecimal(drLeaveDesignationConfiguration["LeaveCount"].ToString());

                                    if (leaveAmount > 0)
                                    {
                                        Business.LeaveManagement.LeaveAccountBalance objLeaveAccountBalance = new Business.LeaveManagement.LeaveAccountBalance();
                                        int response = objLeaveAccountBalance.LeaveAccontBalance_Adjust(new Entity.LeaveManagement.LeaveAccountBalance()
                                        {
                                            LeaveTypeId = leaveTypeId,
                                            Amount = +leaveAmount,
                                            EmployeeId = Convert.ToInt32(drEmployee["EmployeeMasterId"].ToString()),
                                            Reason = ddlLeaveType.SelectedItem + " IS GENERATED FOR " + ddlMonths.SelectedValue + "_" + ddlQuarters.SelectedValue + "_" + ddlYears.SelectedValue
                                        });
                                        if (response > 0)
                                        {
                                            totalCount++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return totalCount;
        }

        private void LeaveGenerateLog_GetAll()
        {
            Business.LeaveManagement.LeaveGenerateLog objLeaveGenerateLog = new Business.LeaveManagement.LeaveGenerateLog();
            Entity.LeaveManagement.LeaveGenerateLog leaveGenerateLog = new Entity.LeaveManagement.LeaveGenerateLog();

            DataTable dtLeaveGenerateLog = objLeaveGenerateLog.LeaveGenerateLog_GetAll(leaveGenerateLog);

            dgvGeneratedLeaveList.DataSource = dtLeaveGenerateLog;
            dgvGeneratedLeaveList.DataBind();
        }
    }
}