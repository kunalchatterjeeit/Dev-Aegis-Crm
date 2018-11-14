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

namespace WebAppAegisCRM.Employee
{
    public partial class Employee : System.Web.UI.Page
    {
        public int EmployeeMasterId
        {
            get { return Convert.ToInt32(ViewState["EmployeeMasterId"]); }
            set { ViewState["EmployeeMasterId"] = value; }
        }
        public string Image
        {
            get { return (ViewState["Image"] != null) ? ViewState["Image"].ToString() : string.Empty; }
            set { ViewState["Image"] = value; }
        }
        public string EmployeePassword
        {
            get { return (ViewState["EmployeePassword"] != null) ? ViewState["EmployeePassword"].ToString() : string.Empty; }
            set { ViewState["EmployeePassword"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadRoleList();
                EmployeeMaster_GetAll();
                EmployeeMaster_GetAll_ReferenceEmployee();
                DesignationMaster_GetAll();
                BindCity();
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
            Entity.HR.EmployeeMaster employeeMaster = new Entity.HR.EmployeeMaster();
            employeeMaster.EmployeeMasterId = EmployeeMasterId;
            employeeMaster.EmployeeName = txtemployeename.Text.Trim();
            employeeMaster.Image = (FileUpload1.HasFile) ? System.IO.Path.GetExtension(FileUpload1.FileName) : string.Empty;
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
                employeeMaster.Password = txtPassword.Text.Trim().EncryptQueryString();
            employeeMaster.PANNo = txtPANnumber.Text.Trim();

            employeeMaster.TAddress = txtpresentaddress.Text.Trim();
            employeeMaster.TCityId_FK = (ddlPresentCity.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlPresentCity.SelectedValue);
            employeeMaster.TPIN = txtpresentpin.Text.Trim();
            employeeMaster.RoleId = int.Parse(ddlRole.SelectedValue);
            int i = 0;
            i = objEmployeeMaster.EmployeeSave(employeeMaster);
            if (i > 0)
            {
                if (FileUpload1.HasFile)
                    FileUpload1.PostedFile.SaveAs(Server.MapPath(" ") + "\\EmployeeImage\\" + i.ToString() + employeeMaster.Image);

                CleartextBoxes(this);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "mmsg", "alert('Data Save Succesfully....');", true);
                EmployeeMaster_GetAll();
                EmployeeMaster_GetAll_ReferenceEmployee();
                DesignationMaster_GetAll();
                BindCity();
                EmployeeMasterId = 0;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "mmsg", "alert('Data Can not Save!!!....');", true);

            }
        }

        protected void EmployeeMaster_GetAll()
        {
            Business.HR.EmployeeMaster ObjBelEmployeeMaster = new Business.HR.EmployeeMaster();
            Entity.HR.EmployeeMaster ObjElEmployeeMaster = new Entity.HR.EmployeeMaster();
            ObjElEmployeeMaster.CompanyId_FK = 1;
            DataTable dt = ObjBelEmployeeMaster.EmployeeMaster_GetAll(ObjElEmployeeMaster);
            if (dt.Rows.Count > 0)
                gvEmployeerMaster.DataSource = dt;
            else
                gvEmployeerMaster.DataSource = null;
            gvEmployeerMaster.DataBind();
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

            ListItem li = new ListItem("--SELECT--", "0");
            ddlRole.Items.Insert(0, li);
        }

        public void CleartextBoxes(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if ((c.GetType() == typeof(TextBox)))
                {
                    ((TextBox)(c)).Text = string.Empty;
                }
                if (c.HasControls())
                {
                    CleartextBoxes(c);
                }
            }
        }

        protected void EmployeeMaster_ById(int Id)
        {
            try
            {
                Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
                Entity.HR.EmployeeMaster employeeMaster = new Entity.HR.EmployeeMaster();
                employeeMaster.EmployeeMasterId = Id;
                DataTable dr = objEmployeeMaster.EmployeeMaster_ById(employeeMaster);

                txtemployeename.Text = dr.Rows[0]["EmployeeName"].ToString();
                Image = dr.Rows[0]["Image"].ToString();
                ddlgenderid.SelectedValue = dr.Rows[0]["GenderId"].ToString();
                txtdateofbirth.Text = (dr.Rows[0]["DOB"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr.Rows[0]["DOB"].ToString()).ToString("dd MMM yyyy");
                ddlMaritalStatus.SelectedValue = dr.Rows[0]["MaritalStatus"].ToString();
                txtdom.Text = (dr.Rows[0]["DOM"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr.Rows[0]["DOM"].ToString()).ToString("dd MMM yyyy");
                ddlReligion.SelectedValue = dr.Rows[0]["ReligionId_FK"].ToString();
                txtbloodgroup.Text = dr.Rows[0]["BloodGroup"].ToString();
                txtMobileNo.Text = dr.Rows[0]["PersonalMobileNo"].ToString();
                txtofficialPhoneNo.Text = dr.Rows[0]["OfficeMobileNo"].ToString();
                txtpersonalEmailId.Text = dr.Rows[0]["PersonalEmailId"].ToString();
                txtofficialEmailId.Text = dr.Rows[0]["OfficeEmailId"].ToString();
                ddlRefferencrEmployee.SelectedValue = (dr.Rows[0]["ReferenceEmployeeId"] == DBNull.Value) ? "0" : dr.Rows[0]["ReferenceEmployeeId"].ToString();
                txtpAddress.Text = dr.Rows[0]["pAddress"].ToString();
                ddlCity.SelectedValue = dr.Rows[0]["pCityId_FK"].ToString();
                txtPin.Text = dr.Rows[0]["pPIN"].ToString();
                ddldesignation.SelectedValue = (dr.Rows[0]["DesignationMasterId_FK"] == DBNull.Value) ? "0" : dr.Rows[0]["DesignationMasterId_FK"].ToString();
                txtDOJ.Text = (dr.Rows[0]["DOJ"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dr.Rows[0]["DOJ"].ToString()).ToString("dd MMM yyyy");
                EmployeePassword = dr.Rows[0]["Password"].ToString();
                txtpresentaddress.Text = dr.Rows[0]["tAddress"].ToString();
                ddlPresentCity.SelectedValue = (dr.Rows[0]["tCityId_FK"] == DBNull.Value) ? "0" : dr.Rows[0]["tCityId_FK"].ToString();
                txtpresentpin.Text = dr.Rows[0]["tPINMasterId"].ToString();
                ddlRole.SelectedValue = dr.Rows[0]["UserRole_RoleId"].ToString();
                Image1.ImageUrl = "EmployeeImage\\" + dr.Rows[0]["Image"].ToString();

            }
            catch
            {

            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            CleartextBoxes(this);
        }

        protected void gvEmployeerMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "E")
            {
                EmployeeMasterId = Convert.ToInt16(e.CommandArgument.ToString());
                EmployeeMaster_ById(EmployeeMasterId);

            }
            else if (e.CommandName == "D")
            {
                EmployeeMasterId = Convert.ToInt16(e.CommandArgument.ToString());

                Business.HR.EmployeeMaster ObjBelEmployeeMaster = new Business.HR.EmployeeMaster();
                Entity.HR.EmployeeMaster ObjElEmployeeMaster = new Entity.HR.EmployeeMaster();
                ObjElEmployeeMaster.EmployeeMasterId = EmployeeMasterId;
                int i = 0;
                i = ObjBelEmployeeMaster.DeleteEmployee(ObjElEmployeeMaster);
                if (i > 0)
                {
                    CleartextBoxes(this);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "mmsg", "alert('Data delete succesfully....');", true);
                    EmployeeMaster_GetAll();
                    EmployeeMaster_GetAll_ReferenceEmployee();
                    DesignationMaster_GetAll();
                    BindCity();
                    EmployeeMasterId = 0;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "mmsg", "alert('Data can not delete!!!....');", true);
                }
            }
        }

        protected void EmployeeMaster_GetAll_ReferenceEmployee()
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

            ListItem li = new ListItem("--SELECT--", "0");
            ddlRefferencrEmployee.Items.Insert(0, li);
        }

        protected void DesignationMaster_GetAll()
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

            ListItem li = new ListItem("--SELECT--", "0");
            ddldesignation.Items.Insert(0, li);
        }

        protected void BindCity()
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

            ListItem li = new ListItem("--SELECT--", "0");
            ddlCity.Items.Insert(0, li);
            ddlPresentCity.Items.Insert(0, li);
        }

        protected void gvEmployeerMaster_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}