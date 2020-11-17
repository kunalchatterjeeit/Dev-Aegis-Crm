using Business.Common;
using Entity.Common;
using System;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Employee
{
    public partial class Employee : System.Web.UI.Page
    {
        private int EmployeeMasterId
        {
            get { return Convert.ToInt32(ViewState["EmployeeMasterId"]); }
            set { ViewState["EmployeeMasterId"] = value; }
        }
        private string Image
        {
            get { return (ViewState["Image"] != null) ? ViewState["Image"].ToString() : string.Empty; }
            set { ViewState["Image"] = value; }
        }
        private string EmployeePassword
        {
            get { return (ViewState["EmployeePassword"] != null) ? ViewState["EmployeePassword"].ToString() : string.Empty; }
            set { ViewState["EmployeePassword"] = value; }
        }
        private long LeaveEmployeeWiseApprovalConfigId
        {
            get { return Convert.ToInt64(ViewState["LeaveEmployeeWiseApprovalConfigId"]); }
            set { ViewState["LeaveEmployeeWiseApprovalConfigId"] = value; }
        }
        private bool ValidateSave()
        {
            if (string.IsNullOrEmpty(txtemployeename.Text.Trim()))
            {
                MessageBox.IsSuccess = false;
                MessageBox.Text = "Please enter employee name";
                MessageBox.Show = true;
                txtemployeename.Focus();
                return false;
            }
            if (ddlgenderid.SelectedIndex == 0)
            {
                MessageBox.IsSuccess = false;
                MessageBox.Text = "Please select gender";
                MessageBox.Show = true;
                ddlgenderid.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtdateofbirth.Text.Trim()))
            {
                MessageBox.IsSuccess = false;
                MessageBox.Text = "Please enter date of birth";
                MessageBox.Show = true;
                txtdateofbirth.Focus();
                return false;
            }
            if (ddlReligion.SelectedIndex == 0)
            {
                MessageBox.IsSuccess = false;
                MessageBox.Text = "Please select religion";
                MessageBox.Show = true;
                ddlReligion.Focus();
                return false;
            }
            if (ddlMaritalStatus.SelectedIndex == 0)
            {
                MessageBox.IsSuccess = false;
                MessageBox.Text = "Please select marital status";
                MessageBox.Show = true;
                ddlMaritalStatus.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtMobileNo.Text.Trim()))
            {
                MessageBox.IsSuccess = false;
                MessageBox.Text = "Please enter mobile number";
                MessageBox.Show = true;
                txtMobileNo.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtpersonalEmailId.Text.Trim()))
            {
                MessageBox.IsSuccess = false;
                MessageBox.Text = "Please enter personal email id";
                MessageBox.Show = true;
                txtpersonalEmailId.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtpAddress.Text.Trim()))
            {
                MessageBox.IsSuccess = false;
                MessageBox.Text = "Please enter permanent address";
                MessageBox.Show = true;
                txtpAddress.Focus();
                return false;
            }
            if (ddlCity.SelectedIndex == 0)
            {
                MessageBox.IsSuccess = false;
                MessageBox.Text = "Please select city";
                MessageBox.Show = true;
                ddlCity.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtPin.Text.Trim()))
            {
                MessageBox.IsSuccess = false;
                MessageBox.Text = "Please enter PIN";
                MessageBox.Show = true;
                txtPin.Focus();
                return false;
            }
            if (ddldesignation.SelectedIndex == 0)
            {
                MessageBox.IsSuccess = false;
                MessageBox.Text = "Please select designation";
                MessageBox.Show = true;
                ddldesignation.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtDOJ.Text.Trim()))
            {
                MessageBox.IsSuccess = false;
                MessageBox.Text = "Please enter date of joining";
                MessageBox.Show = true;
                txtDOJ.Focus();
                return false;
            }
            if (ddlRole.SelectedIndex == 0)
            {
                MessageBox.IsSuccess = false;
                MessageBox.Text = "Please select role";
                MessageBox.Show = true;
                ddlRole.Focus();
                return false;
            }
            if (ddlReporting.SelectedIndex == 0)
            {
                MessageBox.IsSuccess = false;
                MessageBox.Text = "Please select reporting person";
                MessageBox.Show = true;
                ddlReporting.Focus();
                return false;
            }
            if (EmployeeMasterId == 0 && string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                MessageBox.IsSuccess = false;
                MessageBox.Text = "Please enter password";
                MessageBox.Show = true;
                txtPassword.Focus();
                return false;
            }

            return true;
        }
        private long ClaimEmployeeWiseApprovalConfigId
        {
            get { return Convert.ToInt64(ViewState["ClaimEmployeeWiseApprovalConfigId"]); }
            set { ViewState["ClaimEmployeeWiseApprovalConfigId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    LoadRoleList();
                    EmployeeMaster_GetAll();
                    EmployeeMaster_GetAll_ReferenceEmployee();
                    EmployeeMaster_GetAll_Reporting();
                    DesignationMaster_GetAll();
                    BindCity();
                    MessageBox.Show = false;
                    MessageGeneralLeave.Show = false;
                    MessageLeave.Show = false;
                    MessageClaim.Show = false;
                    MessageGeneralClaim.Show = false;
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                MessageBox.IsSuccess = false;
                MessageBox.Text = ex.Message;
                MessageBox.Show = true;
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateSave())
                {
                    Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
                    Entity.HR.EmployeeMaster employeeMaster = new Entity.HR.EmployeeMaster();
                    employeeMaster.EmployeeMasterId = EmployeeMasterId;
                    employeeMaster.EmployeeName = txtemployeename.Text.Trim();
                    employeeMaster.Image = (FileUpload1.HasFile) ? System.IO.Path.GetExtension(FileUpload1.FileName) : string.Empty;
                    employeeMaster.Signature = (FileUpload2.HasFile) ? System.IO.Path.GetExtension(FileUpload2.FileName) : string.Empty;
                    employeeMaster.GenderId = Convert.ToInt16(ddlgenderid.SelectedValue);
                    employeeMaster.DOB = Convert.ToDateTime(txtdateofbirth.Text.Trim());
                    employeeMaster.MaritalStatus = ddlMaritalStatus.SelectedValue;
                    employeeMaster.DOM = (txtdom.Text == string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtdom.Text);
                    employeeMaster.NationalityId_FK = 0;
                    employeeMaster.ReligionId_FK = Convert.ToInt16(ddlReligion.SelectedValue);
                    employeeMaster.BloodGroup = txtbloodgroup.Text.Trim();
                    employeeMaster.PersonalMobileNo = txtMobileNo.Text.Trim();
                    employeeMaster.OfficeMobileNo = txtofficialPhoneNo.Text.Trim();
                    employeeMaster.PersonalEmailId = txtpersonalEmailId.Text.Trim();
                    employeeMaster.OfficeEmailId = txtofficialEmailId.Text.Trim();
                    employeeMaster.ReferenceEmployeeId = (ddlRefferencrEmployee.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlRefferencrEmployee.SelectedValue);
                    employeeMaster.PAddress = txtpAddress.Text.Trim();
                    employeeMaster.PCityId_FK = Convert.ToInt16(ddlCity.SelectedValue);
                    employeeMaster.PPIN = txtPin.Text.Trim();
                    employeeMaster.UserId = 1;
                    employeeMaster.CompanyId_FK = 1;
                    employeeMaster.DesignationMasterId_FK = Convert.ToInt16(ddldesignation.SelectedValue);
                    employeeMaster.DOJ = (txtDOJ.Text.Trim() == string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtDOJ.Text.Trim());
                    employeeMaster.EmployeeJobId = 1;
                    if (txtPassword.Text.Trim() == string.Empty)
                        employeeMaster.Password = EmployeePassword;
                    else
                        employeeMaster.Password = txtPassword.Text.Trim().EncodePasswordToBase64();
                    employeeMaster.PANNo = txtPANnumber.Text.Trim();

                    employeeMaster.TAddress = txtpresentaddress.Text.Trim();
                    employeeMaster.TCityId_FK = (ddlPresentCity.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlPresentCity.SelectedValue);
                    employeeMaster.TPIN = txtpresentpin.Text.Trim();
                    employeeMaster.RoleId = int.Parse(ddlRole.SelectedValue);
                    employeeMaster.ReportingEmployeeId = Convert.ToInt32(ddlReporting.SelectedValue);

                    int employeeId = 0;
                    employeeId = objEmployeeMaster.Employee_Save(employeeMaster);
                    if (employeeId > 0)
                    {
                        if (FileUpload1.HasFile)
                            FileUpload1.PostedFile.SaveAs(Server.MapPath(" ") + "\\EmployeeImage\\" + employeeId.ToString() + employeeMaster.Image);
                        if (FileUpload2.HasFile)
                            FileUpload2.PostedFile.SaveAs(Server.MapPath(" ") + "\\EmployeeSignature\\" + employeeId.ToString() + employeeMaster.Signature);
                        ClearTextBoxes(this);
                        MessageBox.IsSuccess = true;
                        MessageBox.Text = "Employee data saved.";
                        EmployeeMaster_GetAll();
                        EmployeeMaster_GetAll_ReferenceEmployee();
                        EmployeeMaster_GetAll_Reporting();
                        DesignationMaster_GetAll();
                        BindCity();
                        EmployeeMasterId = 0;
                    }
                    else
                    {
                        MessageBox.IsSuccess = false;
                        MessageBox.Text = "Failed to save data.";
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                MessageBox.IsSuccess = false;
                MessageBox.Text = ex.Message;
            }
            MessageBox.Show = true;
        }
        private void EmployeeMaster_GetAll()
        {
            Business.HR.EmployeeMaster ObjBelEmployeeMaster = new Business.HR.EmployeeMaster();
            Entity.HR.EmployeeMaster ObjElEmployeeMaster = new Entity.HR.EmployeeMaster();
            ObjElEmployeeMaster.CompanyId_FK = 1;
            DataTable dt = ObjBelEmployeeMaster.EmployeeMaster_GetAll(ObjElEmployeeMaster);
            gvEmployeerMaster.DataSource = dt;
            gvEmployeerMaster.DataBind();
        }
        private void EmployeeMaster_GetAll_Reporting()
        {
            Business.HR.EmployeeMaster ObjBelEmployeeMaster = new Business.HR.EmployeeMaster();
            Entity.HR.EmployeeMaster ObjElEmployeeMaster = new Entity.HR.EmployeeMaster();
            ObjElEmployeeMaster.CompanyId_FK = 1;
            DataTable dt = ObjBelEmployeeMaster.EmployeeMaster_GetAll(ObjElEmployeeMaster);

            ddlReporting.DataSource = dt;
            ddlReporting.DataTextField = "EmployeeName";
            ddlReporting.DataValueField = "EmployeeMasterId";
            ddlReporting.DataBind();
            ddlReporting.InsertSelect();
        }
        private void LoadApprover()
        {
            Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
            Entity.HR.EmployeeMaster employeeMaster = new Entity.HR.EmployeeMaster();
            employeeMaster.CompanyId_FK = 1;
            DataTable dtApprover = objEmployeeMaster.Employee_GetAll_Active(employeeMaster);

            ddlApproverEngineer.DataSource = dtApprover;
            ddlApproverEngineer.DataTextField = "EmployeeName";
            ddlApproverEngineer.DataValueField = "EmployeeMasterId";
            ddlApproverEngineer.DataBind();
            ddlApproverEngineer.InsertSelect();
        }
        private void LoadClaimApprover()
        {
            Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
            Entity.HR.EmployeeMaster employeeMaster = new Entity.HR.EmployeeMaster();
            employeeMaster.CompanyId_FK = 1;
            DataTable dtApprover = objEmployeeMaster.Employee_GetAll_Active(employeeMaster);

            ddlClaimApproverEngineer.DataSource = dtApprover;
            ddlClaimApproverEngineer.DataTextField = "EmployeeName";
            ddlClaimApproverEngineer.DataValueField = "EmployeeMasterId";
            ddlClaimApproverEngineer.DataBind();
            ddlClaimApproverEngineer.InsertSelect();
        }
        private void LoadRoleList()
        {
            Business.HR.RoleMaster objRoleMaster = new Business.HR.RoleMaster();
            DataTable DT = objRoleMaster.GetAll();
            if (DT != null)
            {
                ddlRole.DataSource = DT;
                ddlRole.DataBind();
            }
            ddlRole.InsertSelect();
        }
        private void LeaveEmployeeWiseApprovalConfiguration_GetAll()
        {
            Business.LeaveManagement.LeaveApprovalConfiguration objLeaveApprovalConfiguration = new Business.LeaveManagement.LeaveApprovalConfiguration();
            Entity.LeaveManagement.LeaveApprovalConfiguration leaveApprovalConfiguration = new Entity.LeaveManagement.LeaveApprovalConfiguration();

            leaveApprovalConfiguration.EmployeeId = EmployeeMasterId;
            DataTable dt = objLeaveApprovalConfiguration.LeaveEmployeeWiseApprovalConfiguration_GetAll(leaveApprovalConfiguration);
            gvApproverDetails.DataSource = dt;
            gvApproverDetails.DataBind();
        }
        private void ClaimEmployeeWiseApprovalConfiguration_GetAll()
        {
            Business.ClaimManagement.ClaimApprovalConfiguration objClaimApprovalConfiguration = new Business.ClaimManagement.ClaimApprovalConfiguration();
            Entity.ClaimManagement.ClaimApprovalConfiguration claimApprovalConfiguration = new Entity.ClaimManagement.ClaimApprovalConfiguration();

            claimApprovalConfiguration.EmployeeId = EmployeeMasterId;
            DataTable dt = objClaimApprovalConfiguration.ClaimEmployeeWiseApprovalConfiguration_GetAll(claimApprovalConfiguration);
            gvClaimApproverDetails.DataSource = dt;
            gvClaimApproverDetails.DataBind();
        }
        private void ClearTextBoxes(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if ((c.GetType() == typeof(TextBox)))
                {
                    ((TextBox)(c)).Text = string.Empty;
                }
                if (c.HasControls())
                {
                    ClearTextBoxes(c);
                }
            }
        }
        private void EmployeeMaster_ById(int Id)
        {
            try
            {
                Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
                Entity.HR.EmployeeMaster employeeMaster = new Entity.HR.EmployeeMaster();
                employeeMaster.EmployeeMasterId = Id;
                DataTable dtEmployeeMaster = objEmployeeMaster.EmployeeMaster_ById(employeeMaster);

                txtemployeename.Text = dtEmployeeMaster.Rows[0]["EmployeeName"].ToString();
                Image = dtEmployeeMaster.Rows[0]["Image"].ToString();
                ddlgenderid.SelectedValue = dtEmployeeMaster.Rows[0]["GenderId"].ToString();
                txtdateofbirth.Text = (dtEmployeeMaster.Rows[0]["DOB"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dtEmployeeMaster.Rows[0]["DOB"].ToString()).ToString("dd MMM yyyy");
                ddlMaritalStatus.SelectedValue = dtEmployeeMaster.Rows[0]["MaritalStatus"].ToString();
                txtdom.Text = (dtEmployeeMaster.Rows[0]["DOM"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dtEmployeeMaster.Rows[0]["DOM"].ToString()).ToString("dd MMM yyyy");
                ddlReligion.SelectedValue = dtEmployeeMaster.Rows[0]["ReligionId_FK"].ToString();
                txtbloodgroup.Text = dtEmployeeMaster.Rows[0]["BloodGroup"].ToString();
                txtMobileNo.Text = dtEmployeeMaster.Rows[0]["PersonalMobileNo"].ToString();
                txtofficialPhoneNo.Text = dtEmployeeMaster.Rows[0]["OfficeMobileNo"].ToString();
                txtpersonalEmailId.Text = dtEmployeeMaster.Rows[0]["PersonalEmailId"].ToString();
                txtofficialEmailId.Text = dtEmployeeMaster.Rows[0]["OfficeEmailId"].ToString();
                ddlRefferencrEmployee.SelectedValue = (dtEmployeeMaster.Rows[0]["ReferenceEmployeeId"] == DBNull.Value) ? "0" : dtEmployeeMaster.Rows[0]["ReferenceEmployeeId"].ToString();
                txtpAddress.Text = dtEmployeeMaster.Rows[0]["pAddress"].ToString();
                ddlCity.SelectedValue = dtEmployeeMaster.Rows[0]["pCityId_FK"].ToString();
                txtPin.Text = dtEmployeeMaster.Rows[0]["pPIN"].ToString();
                ddldesignation.SelectedValue = (dtEmployeeMaster.Rows[0]["DesignationMasterId_FK"] == DBNull.Value) ? "0" : dtEmployeeMaster.Rows[0]["DesignationMasterId_FK"].ToString();
                txtDOJ.Text = (dtEmployeeMaster.Rows[0]["DOJ"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dtEmployeeMaster.Rows[0]["DOJ"].ToString()).ToString("dd MMM yyyy");
                EmployeePassword = dtEmployeeMaster.Rows[0]["Password"].ToString();
                txtpresentaddress.Text = dtEmployeeMaster.Rows[0]["tAddress"].ToString();
                ddlPresentCity.SelectedValue = (dtEmployeeMaster.Rows[0]["tCityId_FK"] == DBNull.Value) ? "0" : dtEmployeeMaster.Rows[0]["tCityId_FK"].ToString();
                txtpresentpin.Text = dtEmployeeMaster.Rows[0]["tPINMasterId"].ToString();
                ddlRole.SelectedValue = dtEmployeeMaster.Rows[0]["UserRole_RoleId"].ToString();
                Image1.ImageUrl = "EmployeeImage\\" + dtEmployeeMaster.Rows[0]["Image"].ToString();
                Image2.ImageUrl = "EmployeeSignature\\" + dtEmployeeMaster.Rows[0]["Signature"].ToString();
                ddlReporting.SelectedValue = (dtEmployeeMaster.Rows[0]["ReportingEmployeeId"] == DBNull.Value) ? "0" : dtEmployeeMaster.Rows[0]["ReportingEmployeeId"].ToString();

                DataTable dtLeaveAccountBalance = new Business.LeaveManagement.LeaveAccountBalance().LeaveAccountBalance_ByEmployeeId(Id, (int)LeaveTypeEnum.CL).Tables[0];
                if (dtLeaveAccountBalance != null && dtLeaveAccountBalance.AsEnumerable().Any())
                {
                    if (Convert.ToBoolean(dtLeaveAccountBalance.Rows[0]["LeaveBlocked"].ToString()))
                    {
                        rbtnListLeaveStatus.Items[0].Selected = false;
                        rbtnListLeaveStatus.Items[1].Selected = true;
                    }
                    else
                    {
                        rbtnListLeaveStatus.Items[0].Selected = true;
                        rbtnListLeaveStatus.Items[1].Selected = false;
                    }
                }
                else
                {
                    rbtnListLeaveStatus.Items[0].Selected = true;
                    rbtnListLeaveStatus.Items[1].Selected = false;
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                MessageBox.IsSuccess = false;
                MessageBox.Text = ex.Message;
                MessageBox.Show = true;
            }
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            try
            {
                ClearTextBoxes(this);
                MessageBox.Show = false;
                MessageGeneralLeave.Show = false;
                MessageLeave.Show = false;
            }
            catch (Exception ex)
            {
                ex.WriteException();
                MessageBox.IsSuccess = false;
                MessageBox.Text = ex.Message;
                MessageBox.Show = true;
            }
        }
        protected void gvEmployeerMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "E")
                {
                    EmployeeMasterId = Convert.ToInt32(e.CommandArgument.ToString());
                    EmployeeMaster_ById(EmployeeMasterId);

                }
                else if (e.CommandName == "D")
                {
                    EmployeeMasterId = Convert.ToInt32(e.CommandArgument.ToString());

                    Business.HR.EmployeeMaster ObjBelEmployeeMaster = new Business.HR.EmployeeMaster();
                    Entity.HR.EmployeeMaster ObjElEmployeeMaster = new Entity.HR.EmployeeMaster();
                    ObjElEmployeeMaster.EmployeeMasterId = EmployeeMasterId;
                    int i = 0;
                    i = ObjBelEmployeeMaster.DeleteEmployee(ObjElEmployeeMaster);
                    if (i > 0)
                    {
                        ClearTextBoxes(this);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "mmsg", "alert('Data delete succesfully....');", true);
                        EmployeeMaster_GetAll();
                        EmployeeMaster_GetAll_ReferenceEmployee();
                        EmployeeMaster_GetAll_Reporting();
                        DesignationMaster_GetAll();
                        BindCity();
                        EmployeeMasterId = 0;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "mmsg", "alert('Data can not delete!!!....');", true);
                    }
                }
                else if (e.CommandName == "Leave")
                {
                    EmployeeMasterId = Convert.ToInt32(e.CommandArgument.ToString());
                    LoadApprover();
                    EmployeeMaster_ById(EmployeeMasterId);
                    LeaveEmployeeWiseApprovalConfiguration_GetAll();
                    TabContainer1.ActiveTab = AddApproval;
                    ModalPopupExtender1.Show();
                }
                else if (e.CommandName == "RemoveMobile")
                {
                    Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
                    int i = 0;
                    i = objEmployeeMaster.LiknedDevices_Delete(Convert.ToInt32(e.CommandArgument.ToString()));
                    if (i > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "mmsg", "alert('Device unlinked successfully.');", true);
                        EmployeeMaster_GetAll();
                    }
                }
                else if (e.CommandName == "Claim")
                {
                    EmployeeMasterId = Convert.ToInt32(e.CommandArgument.ToString());
                    LoadClaimApprover();
                    EmployeeMaster_ById(EmployeeMasterId);
                    ClaimEmployeeWiseApprovalConfiguration_GetAll();
                    TabContainer2.ActiveTab = TabPanel3;
                    ModalPopupExtender2.Show();
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                MessageBox.IsSuccess = false;
                MessageBox.Text = ex.Message;
                MessageBox.Show = true;
            }
        }
        private void EmployeeMaster_GetAll_ReferenceEmployee()
        {
            Business.HR.EmployeeMaster ObjBelEmployeeMaster = new Business.HR.EmployeeMaster();
            Entity.HR.EmployeeMaster ObjElEmployeeMaster = new Entity.HR.EmployeeMaster();
            ObjElEmployeeMaster.CompanyId_FK = 1;
            DataTable dt = ObjBelEmployeeMaster.EmployeeMaster_GetAll(ObjElEmployeeMaster);

            if (dt.Rows.Count > 0)
            {
                ddlRefferencrEmployee.DataSource = dt;
                ddlRefferencrEmployee.DataBind();
            }
            ddlRefferencrEmployee.InsertSelect();
        }
        private void DesignationMaster_GetAll()
        {
            Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
            Entity.HR.EmployeeMaster employeeMaster = new Entity.HR.EmployeeMaster();
            employeeMaster.CompanyId_FK = 1;
            DataTable dt = objEmployeeMaster.DesignationMaster_GetAll(employeeMaster);
            if (dt.Rows.Count > 0)
            {
                ddldesignation.DataSource = dt;
                ddldesignation.DataBind();
            }
            ddldesignation.InsertSelect();
        }
        private void BindCity()
        {
            Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
            DataTable dt = objEmployeeMaster.City_GetAll();
            if (dt.Rows.Count > 0)
            {
                ddlCity.DataSource = dt;
                ddlCity.DataBind();

                ddlPresentCity.DataSource = dt;
                ddlPresentCity.DataBind();
            }
            ddlCity.InsertSelect();
            ddlPresentCity.InsertSelect();
        }
        protected void gvEmployeerMaster_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        protected void gvApproverDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                LeaveEmployeeWiseApprovalConfigId = Convert.ToInt64(e.CommandArgument.ToString());

                if (e.CommandName == "E")
                {
                    LeaveEmployeeWiseApprovalConfiguration_GetById();
                }
                else if (e.CommandName == "D")
                {
                    Business.LeaveManagement.LeaveApprovalConfiguration objLeaveApprovalConfiguration = new Business.LeaveManagement.LeaveApprovalConfiguration();
                    Entity.LeaveManagement.LeaveApprovalConfiguration leaveApprovalConfiguration = new Entity.LeaveManagement.LeaveApprovalConfiguration();
                    leaveApprovalConfiguration.LeaveEmployeeWiseApprovalConfigurationId = LeaveEmployeeWiseApprovalConfigId;
                    int response = objLeaveApprovalConfiguration.LeaveEmployeeWiseApprovalConfiguration_Delete(LeaveEmployeeWiseApprovalConfigId);
                    if (response > 0)
                    {
                        ClearTextBoxes(this);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "mmsg", "alert('Data delete succesfully....');", true);
                        LeaveEmployeeWiseApprovalConfiguration_GetAll();
                        LeaveEmployeeWiseApprovalConfigId = 0;
                        TabContainer1.ActiveTab = ApprovalDetails;
                        ModalPopupExtender1.Show();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "mmsg", "alert('Data can not delete!!!....');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                MessageBox.IsSuccess = false;
                MessageBox.Text = ex.Message;
                MessageBox.Show = true;
            }
        }
        protected void btnTSave_Click(object sender, EventArgs e)
        {
            try
            {
                Business.LeaveManagement.LeaveApprovalConfiguration objLeaveApprovalConfiguration = new Business.LeaveManagement.LeaveApprovalConfiguration();
                Entity.LeaveManagement.LeaveApprovalConfiguration leaveApprovalConfiguration = new Entity.LeaveManagement.LeaveApprovalConfiguration();

                leaveApprovalConfiguration.LeaveEmployeeWiseApprovalConfigurationId = LeaveEmployeeWiseApprovalConfigId;
                leaveApprovalConfiguration.EmployeeId = EmployeeMasterId;
                leaveApprovalConfiguration.ApproverId = Convert.ToInt32(ddlApproverEngineer.SelectedValue);
                leaveApprovalConfiguration.ApprovalLevel = Convert.ToInt32(ddlApprovalLevel.SelectedValue);
                leaveApprovalConfiguration.CreatedBy = int.Parse(HttpContext.Current.User.Identity.Name);
                int response = 0;
                response = objLeaveApprovalConfiguration.LeaveEmployeeWiseApprovalConfiguration_Save(leaveApprovalConfiguration);
                if (response > 0)
                {
                    ClearTextBoxes(this);
                    ddlApproverEngineer.SelectedIndex = 0;
                    ddlApprovalLevel.SelectedIndex = 0;
                    LeaveEmployeeWiseApprovalConfiguration_GetAll();
                    LeaveEmployeeWiseApprovalConfigId = 0;

                    MessageLeave.IsSuccess = true;
                    MessageLeave.Text = "Leave setting updated.";

                    TabContainer1.ActiveTab = AddApproval;
                    ModalPopupExtender1.Show();
                }
                else
                {
                    MessageLeave.IsSuccess = false;
                    MessageLeave.Text = "Failed to save data.";
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                MessageLeave.IsSuccess = false;
                MessageLeave.Text = ex.Message;
            }
            finally
            {
                MessageLeave.Show = true;
                TabContainer1.ActiveTab = AddApproval;
                ModalPopupExtender1.Show();
            }
        }
        protected void btnClaimSave_Click(object sender, EventArgs e)
        {
            try
            {
                Business.ClaimManagement.ClaimApprovalConfiguration objClaimApprovalConfiguration = new Business.ClaimManagement.ClaimApprovalConfiguration();
                Entity.ClaimManagement.ClaimApprovalConfiguration claimApprovalConfiguration = new Entity.ClaimManagement.ClaimApprovalConfiguration();

                claimApprovalConfiguration.ClaimEmployeeWiseApprovalConfigurationId = ClaimEmployeeWiseApprovalConfigId;
                claimApprovalConfiguration.EmployeeId = EmployeeMasterId;
                claimApprovalConfiguration.ApproverId = Convert.ToInt32(ddlClaimApproverEngineer.SelectedValue);
                claimApprovalConfiguration.ApprovalLevel = Convert.ToInt32(ddlClaimApprovalLevel.SelectedValue);
                claimApprovalConfiguration.CreatedBy = int.Parse(HttpContext.Current.User.Identity.Name);
                int response = 0;
                response = objClaimApprovalConfiguration.ClaimEmployeeWiseApprovalConfiguration_Save(claimApprovalConfiguration);
                if (response > 0)
                {
                    ClearTextBoxes(this);
                    ddlClaimApproverEngineer.SelectedIndex = 0;
                    ddlClaimApprovalLevel.SelectedIndex = 0;
                    ClaimEmployeeWiseApprovalConfiguration_GetAll();
                    ClaimEmployeeWiseApprovalConfigId = 0;

                    MessageClaim.IsSuccess = true;
                    MessageClaim.Text = "Claim setting updated.";

                    TabContainer2.ActiveTab = TabPanel2;
                    ModalPopupExtender2.Show();
                }
                else
                {
                    MessageClaim.IsSuccess = false;
                    MessageClaim.Text = "Failed to save data.";
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                MessageClaim.IsSuccess = false;
                MessageClaim.Text = ex.Message;
            }
            finally
            {
                MessageClaim.Show = true;
                TabContainer2.ActiveTab = TabPanel2;
                ModalPopupExtender2.Show();
            }
        }
        private void LeaveEmployeeWiseApprovalConfiguration_GetById()
        {
            Business.LeaveManagement.LeaveApprovalConfiguration objLeaveApprovalConfiguration = new Business.LeaveManagement.LeaveApprovalConfiguration();
            Entity.LeaveManagement.LeaveApprovalConfiguration leaveApprovalConfiguration = new Entity.LeaveManagement.LeaveApprovalConfiguration();
            leaveApprovalConfiguration.LeaveEmployeeWiseApprovalConfigurationId = LeaveEmployeeWiseApprovalConfigId;
            DataTable dt = objLeaveApprovalConfiguration.LeaveEmployeeWiseApprovalConfiguration_GetAll(leaveApprovalConfiguration);
            if (dt != null && dt.AsEnumerable().Any())
            {
                ddlApproverEngineer.SelectedValue = dt.Rows[0]["ApproverId"].ToString();
                ddlApprovalLevel.SelectedValue = dt.Rows[0]["ApprovalLevel"].ToString();
            }
            TabContainer1.ActiveTab = AddApproval;
            ModalPopupExtender1.Show();
        }
        private void ClaimEmployeeWiseApprovalConfiguration_GetById()
        {
            Business.ClaimManagement.ClaimApprovalConfiguration objClaimApprovalConfiguration = new Business.ClaimManagement.ClaimApprovalConfiguration();
            Entity.ClaimManagement.ClaimApprovalConfiguration ClaimApprovalConfiguration = new Entity.ClaimManagement.ClaimApprovalConfiguration();
            ClaimApprovalConfiguration.ClaimEmployeeWiseApprovalConfigurationId = ClaimEmployeeWiseApprovalConfigId;
            DataTable dt = objClaimApprovalConfiguration.ClaimEmployeeWiseApprovalConfiguration_GetAll(ClaimApprovalConfiguration);
            if (dt != null && dt.AsEnumerable().Any())
            {
                ddlClaimApproverEngineer.SelectedValue = dt.Rows[0]["ApproverId"].ToString();
                ddlClaimApprovalLevel.SelectedValue = dt.Rows[0]["ApprovalLevel"].ToString();
            }
            TabContainer2.ActiveTab = TabPanel2;
            ModalPopupExtender2.Show();
        }
        protected void rbtnListLeaveStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
                Entity.HR.EmployeeMaster employeeMaster = new Entity.HR.EmployeeMaster();
                employeeMaster.EmployeeMasterId = EmployeeMasterId;
                employeeMaster.LeaveBlocked = Convert.ToBoolean(rbtnListLeaveStatus.SelectedValue);
                int response = objEmployeeMaster.EmployeeLeave_Update(employeeMaster);
                if (response > 0)
                {
                    MessageGeneralLeave.IsSuccess = true;
                    MessageGeneralLeave.Text = "Settings updated.";
                }
                else
                {
                    MessageGeneralLeave.IsSuccess = false;
                    MessageGeneralLeave.Text = "Failed to update settings.";
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                MessageGeneralLeave.IsSuccess = false;
                MessageGeneralLeave.Text = ex.Message;
            }
            finally
            {
                MessageGeneralLeave.Show = true;
                TabContainer1.ActiveTab = LeaveGeneral;
                ModalPopupExtender1.Show();
            }
        }
        protected void chkBlockLogin_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox checkBox = (CheckBox)sender;
                GridViewRow gridViewRow = (GridViewRow)checkBox.NamingContainer;
                int employeeId = Convert.ToInt32(gvEmployeerMaster.DataKeys[gridViewRow.RowIndex].Values[0].ToString());
                new Business.HR.EmployeeMaster().Employee_LoginChange(employeeId, checkBox.Checked);
            }
            catch (Exception ex)
            {
                ex.WriteException();
                MessageBox.IsSuccess = false;
                MessageBox.Text = ex.Message;
                MessageBox.Show = true;
            }
        }
        protected void chkActiveEmployee_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                CheckBox checkBox = (CheckBox)sender;
                GridViewRow gridViewRow = (GridViewRow)checkBox.NamingContainer;
                int employeeId = Convert.ToInt32(gvEmployeerMaster.DataKeys[gridViewRow.RowIndex].Values[0].ToString());
                new Business.HR.EmployeeMaster().Employee_ActiveChange(employeeId, checkBox.Checked);
            }
            catch (Exception ex)
            {
                ex.WriteException();
                MessageBox.IsSuccess = false;
                MessageBox.Text = ex.Message;
                MessageBox.Show = true;
            }
        }
        protected void gvClaimApproverDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ClaimEmployeeWiseApprovalConfigId = Convert.ToInt64(e.CommandArgument.ToString());

                if (e.CommandName == "E")
                {
                    ClaimEmployeeWiseApprovalConfiguration_GetById();
                }
                else if (e.CommandName == "D")
                {
                    Business.ClaimManagement.ClaimApprovalConfiguration objClaimApprovalConfiguration = new Business.ClaimManagement.ClaimApprovalConfiguration();
                    Entity.ClaimManagement.ClaimApprovalConfiguration ClaimApprovalConfiguration = new Entity.ClaimManagement.ClaimApprovalConfiguration();
                    ClaimApprovalConfiguration.ClaimEmployeeWiseApprovalConfigurationId = ClaimEmployeeWiseApprovalConfigId;
                    int response = objClaimApprovalConfiguration.ClaimEmployeeWiseApprovalConfiguration_Delete(ClaimEmployeeWiseApprovalConfigId);
                    if (response > 0)
                    {
                        ClearTextBoxes(this);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "mmsg", "alert('Data delete succesfully....');", true);
                        ClaimEmployeeWiseApprovalConfiguration_GetAll();
                        ClaimEmployeeWiseApprovalConfigId = 0;
                        TabContainer2.ActiveTab = TabPanel3;
                        ModalPopupExtender2.Show();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "mmsg", "alert('Data can not delete!!!....');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                MessageBox.IsSuccess = false;
                MessageBox.Text = ex.Message;
                MessageBox.Show = true;
            }
        }
    }
}