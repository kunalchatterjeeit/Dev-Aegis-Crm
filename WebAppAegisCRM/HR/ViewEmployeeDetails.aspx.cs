using Business.Common;

using System;
using System.Data.SqlClient;

namespace WebAppAegisCRM.Employee
{
    public partial class ViewEmoployeeDetails : System.Web.UI.Page
    {
        
        public int EmployeeId
        {
            get { return Convert.ToInt32(ViewState["EmployeeMasterId"]); }
            set { ViewState["EmployeeMasterId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                EmployeeId = Convert.ToInt16(Request.QueryString["ID"].ToString());
                Show(EmployeeId);
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
            }
        }
        protected void Show(int Id)
        {
            Business.HR.EmployeeMaster ObjBelEmployeeMaster = new Business.HR.EmployeeMaster();
            Entity.HR.EmployeeMaster ObjElEmployeeMaster = new Entity.HR.EmployeeMaster();
            ObjElEmployeeMaster.EmployeeMasterId = Id;
            SqlDataReader dr = ObjBelEmployeeMaster.ViewEmployeeById(ObjElEmployeeMaster);
            while (dr.Read())
            {

                lblEmployeeName.Text = dr["EmployeeName"].ToString();
                Image1.ImageUrl = dr["Image"].ToString();
                lblgender.Text = dr["GenderId"].ToString();
                lbldob.Text = dr["DOB"].ToString();
                lblmaratorialStatus.Text = dr["MaritalStatus"].ToString();
                lblreligion.Text = dr["ReligionId_FK"].ToString();
                lblBloodGroup.Text = dr["BloodGroup"].ToString();
                lblPersonalMobileNumber.Text = dr["PersonalMobileNo"].ToString();
                lblofficialNumber.Text = dr["OfficeMobileNo"].ToString();
                lblpersonalEmailId.Text = dr["PersonalEmailId"].ToString();
                lblOfficeEmailId.Text = dr["OfficeEmailId"].ToString();
                lblReferenceEmployee.Text = dr["ReferenceEmployeeId"].ToString();
                lblpAddress.Text = dr["pAddress"].ToString();
                lblCityName.Text = dr["CityName"].ToString();
                lblPpin.Text = dr["pPIN"].ToString();
                lblDesignationName.Text = dr["DesignationName"].ToString();
                lblDOJ.Text = dr["DOJ"].ToString();
                lblPANNo.Text = dr["PANNo"].ToString();
                lbltAddress.Text = dr["tAddress"].ToString();
                lbltCityId_FK.Text = dr["tCityId_FK"].ToString();
                lbltPINMasterId_FK.Text = dr["tPINMasterId_FK"].ToString();
            }

        }
    }
}