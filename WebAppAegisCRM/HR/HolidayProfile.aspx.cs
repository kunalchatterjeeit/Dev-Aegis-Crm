using Business.Common;

using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.HR
{
    public partial class HolidayProfile : System.Web.UI.Page
    {
        
        public int HolidayProfileId
        {
            get { return Convert.ToInt32(ViewState["HolidayProfileId"]); }
            set { ViewState["HolidayProfileId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ClearControls();
                    LoadHolidayProfileList();
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
        private void ClearControls()
        {
            HolidayProfileId = 0;
            Message.Show = false;
            btnSave.Text = "Save";
            txtHolidayProfile.Text = "";
            txtDescription.Text = string.Empty;
        }
        private void LoadHolidayProfileList()
        {
            Business.HR.HolidayProfile objHolidayProfile = new Business.HR.HolidayProfile();
            DataTable DT = objHolidayProfile.HolidayProfile_GetAll(new Entity.HR.HolidayProfile());
            if (DT != null)
            {
                gvHolidayProfile.DataSource = DT;
                gvHolidayProfile.DataBind();
            }
        }
        private void HolidayProfile_GetById()
        {
            Business.HR.HolidayProfile objHolidayProfile = new Business.HR.HolidayProfile();
            DataTable dt = objHolidayProfile.HolidayProfile_GetById(HolidayProfileId);
            if (dt != null && dt.AsEnumerable().Any())
            {
                txtHolidayProfile.Text = dt.Rows[0]["HolidayProfileName"].ToString();
                txtDescription.Text = dt.Rows[0]["HolidayProfileDescription"].ToString();
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
                
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Business.HR.HolidayProfile objHolidayProfile = new Business.HR.HolidayProfile();
                Entity.HR.HolidayProfile holidayProfile = new Entity.HR.HolidayProfile();
                holidayProfile.HolidayProfileId = HolidayProfileId;
                holidayProfile.HolidayProfileName = txtHolidayProfile.Text.Trim();
                holidayProfile.HolidayProfileDescription = txtDescription.Text.Trim();
                int RowsAffected = objHolidayProfile.HolidayProfile_Save(holidayProfile);

                if (RowsAffected > 0)
                {
                    ClearControls();
                    LoadHolidayProfileList();
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
                
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                LoadHolidayProfileList();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
        protected void gvHolidayProfile_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Ed")
                {
                    HolidayProfileId = Convert.ToInt32(e.CommandArgument.ToString());
                    HolidayProfile_GetById();
                    Message.Show = false;
                    btnSave.Text = "Update";
                }
                else if (e.CommandName == "Del")
                {
                    Business.HR.HolidayProfile objHolidayProfile = new Business.HR.HolidayProfile();
                    int rowsAffected = objHolidayProfile.HolidayProfile_Delete(Convert.ToInt32(e.CommandArgument.ToString()));

                    if (rowsAffected > 0)
                    {
                        ClearControls();
                        LoadHolidayProfileList();
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
                
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
    }
}