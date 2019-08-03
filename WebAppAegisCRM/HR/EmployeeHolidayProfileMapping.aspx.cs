using Business.Common;
using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.HR
{
    public partial class EmployeeHolidayProfileMapping : System.Web.UI.Page
    {
        public int EmployeeHolidayProfileMappingId
        {
            get { return Convert.ToInt32(ViewState["EmployeeHolidayProfileMappingId"]); }
            set { ViewState["EmployeeHolidayProfileMappingId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearControls();
                LoadEmployeeHolidayProfileMappingList();
                EmployeeMaster_GetAll();
                LoadHolidayProfileList();
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

        private void EmployeeMaster_GetAll()
        {
            Business.HR.EmployeeMaster ObjBelEmployeeMaster = new Business.HR.EmployeeMaster();
            Entity.HR.EmployeeMaster ObjElEmployeeMaster = new Entity.HR.EmployeeMaster();
            ObjElEmployeeMaster.CompanyId_FK = 1;
            DataTable dt = ObjBelEmployeeMaster.Employee_GetAll_Active(ObjElEmployeeMaster);

            ddlEmployee.DataSource = dt;
            ddlEmployee.DataTextField = "EmployeeName";
            ddlEmployee.DataValueField = "EmployeeMasterId";
            ddlEmployee.DataBind();
            ddlEmployee.InsertSelect();
        }

        private void ClearControls()
        {
            EmployeeHolidayProfileMappingId = 0;
            Message.Show = false;
            btnSave.Text = "Save";
        }

        private void LoadEmployeeHolidayProfileMappingList()
        {
            Business.HR.HolidayProfile objHolidayProfile = new Business.HR.HolidayProfile();
            DataTable DT = objHolidayProfile.EmployeeHolidayProfileMapping_GetAll(new Entity.HR.EmployeeHolidayProfileMapping());
            if (DT != null)
            {
                gvEmployeeHolidayProfileMapping.DataSource = DT;
                gvEmployeeHolidayProfileMapping.DataBind();
            }
        }

        private void EmployeeHolidayProfileMapping_GetById()
        {
            Business.HR.HolidayProfile objEmployeeHolidayProfileMapping = new Business.HR.HolidayProfile();
            DataTable dt = objEmployeeHolidayProfileMapping.EmployeeHolidayProfileMapping_GetById(EmployeeHolidayProfileMappingId);
            if (dt != null && dt.AsEnumerable().Any())
            {
                ddlEmployee.SelectedValue = dt.Rows[0]["EmployeeId"].ToString();
                ddlHolidayProfile.SelectedValue = dt.Rows[0]["HolidayProfileId"].ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Business.HR.HolidayProfile objHolidayProfile = new Business.HR.HolidayProfile();
            Entity.HR.EmployeeHolidayProfileMapping employeeHolidayProfileMapping = new Entity.HR.EmployeeHolidayProfileMapping();
            employeeHolidayProfileMapping.EmployeeHolidayProfileMappingId = EmployeeHolidayProfileMappingId;
            employeeHolidayProfileMapping.EmployeeId = int.Parse(ddlEmployee.SelectedValue);
            employeeHolidayProfileMapping.HolidayProfileId = int.Parse(ddlHolidayProfile.SelectedValue);
            employeeHolidayProfileMapping.Active = true;
            employeeHolidayProfileMapping.CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            int RowsAffected = objHolidayProfile.EmployeeHolidayProfileMapping_Save(employeeHolidayProfileMapping);

            if (RowsAffected > 0)
            {
                ClearControls();
                LoadEmployeeHolidayProfileMappingList();
                Message.IsSuccess = true;
                Message.Text = "Saved Successfully";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Failed!";
            }
            Message.Show = true;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadEmployeeHolidayProfileMappingList();
        }

        protected void gvEmployeeHolidayProfileMapping_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ed")
            {
                EmployeeHolidayProfileMappingId = Convert.ToInt32(e.CommandArgument.ToString());
                EmployeeHolidayProfileMapping_GetById();
                Message.Show = false;
                btnSave.Text = "Update";
            }
            else if (e.CommandName == "Del")
            {
                Business.HR.HolidayProfile objHolidayProfile = new Business.HR.HolidayProfile();
                int rowsAffected = objHolidayProfile.EmployeeHolidayProfileMapping_Delete(Convert.ToInt32(e.CommandArgument.ToString()));

                if (rowsAffected > 0)
                {
                    ClearControls();
                    LoadEmployeeHolidayProfileMappingList();
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
    }
}