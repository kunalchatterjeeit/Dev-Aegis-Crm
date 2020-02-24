using Business.Common;
using log4net;
using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.HR
{
    public partial class Holiday : System.Web.UI.Page
    {
        ILog logger = log4net.LogManager.GetLogger("ErrorLog");
        public int HolidayId
        {
            get { return Convert.ToInt32(ViewState["HolidayId"]); }
            set { ViewState["HolidayId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ClearControls();
                    LoadHolidayProfileList();
                    LoadHolidayList();
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
        private void LoadHolidayProfileList()
        {
            Business.HR.HolidayProfile objHolidayProfile = new Business.HR.HolidayProfile();
            DataTable dt = objHolidayProfile.HolidayProfile_GetAll(new Entity.HR.HolidayProfile());
            if (dt != null)
            {
                ddlHolidayProfile.DataSource = dt;
                ddlHolidayProfile.DataTextField = "HolidayProfileName";
                ddlHolidayProfile.DataValueField = "HolidayProfileId";
                ddlHolidayProfile.DataBind();
                ddlHolidayProfile.InsertSelect();
            }
        }
        private void ClearControls()
        {
            HolidayId = 0;
            Message.Show = false;
            btnSave.Text = "Save";
            txtHolidayName.Text = "";
            txtDescription.Text = string.Empty;
            txtHolidayDate.Text = string.Empty;
            chkShowNow.Checked = true;
        }
        private void LoadHolidayList()
        {
            Business.HR.Holiday objHoliday = new Business.HR.Holiday();
            DataTable dt = objHoliday.Holiday_GetAll(new Entity.HR.Holiday());
            if (dt != null)
            {
                gvHoliday.DataSource = dt;
                gvHoliday.DataBind();
            }
        }
        private void Holiday_GetById()
        {
            Business.HR.Holiday objHoliday = new Business.HR.Holiday();
            DataTable dt = objHoliday.Holiday_GetById(HolidayId);
            if (dt != null && dt.AsEnumerable().Any())
            {
                ddlHolidayProfile.SelectedValue = dt.Rows[0]["HolidayProfileId"].ToString();
                txtHolidayName.Text = dt.Rows[0]["HolidayName"].ToString();
                txtDescription.Text = dt.Rows[0]["HolidayDescription"].ToString();
                txtHolidayDate.Text = Convert.ToDateTime(dt.Rows[0]["HolidayDate"].ToString()).ToString("dd MMM yyyy");
                chkShowNow.Checked = Convert.ToBoolean(dt.Rows[0]["Show"].ToString());
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControls();
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Business.HR.Holiday objHoliday = new Business.HR.Holiday();
                Entity.HR.Holiday holiday = new Entity.HR.Holiday();
                holiday.HolidayId = HolidayId;
                holiday.HolidayProfileId = int.Parse(ddlHolidayProfile.SelectedValue);
                holiday.HolidayName = txtHolidayName.Text.Trim();
                holiday.HolidayDescription = txtDescription.Text.Trim();
                holiday.HolidayDate = Convert.ToDateTime(txtHolidayDate.Text.Trim());
                holiday.Show = chkShowNow.Checked;
                int RowsAffected = objHoliday.Holiday_Save(holiday);

                if (RowsAffected > 0)
                {
                    ClearControls();
                    LoadHolidayList();
                    Message.IsSuccess = true;
                    Message.Text = "Saved Successfully";
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Holiday Profile Name Exists";
                }
                Message.Show = true;
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
                LoadHolidayList();
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
        protected void gvHoliday_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Ed")
                {
                    HolidayId = Convert.ToInt32(e.CommandArgument.ToString());
                    Holiday_GetById();
                    Message.Show = false;
                    btnSave.Text = "Update";
                }
                else if (e.CommandName == "Del")
                {
                    Business.HR.Holiday objHoliday = new Business.HR.Holiday();
                    int rowsAffected = objHoliday.Holiday_Delete(Convert.ToInt32(e.CommandArgument.ToString()));

                    if (rowsAffected > 0)
                    {
                        ClearControls();
                        LoadHolidayList();
                        Message.IsSuccess = true;
                        Message.Text = "Deleted Successfully";
                    }
                    else
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Data Dependency Exists";
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
    }
}