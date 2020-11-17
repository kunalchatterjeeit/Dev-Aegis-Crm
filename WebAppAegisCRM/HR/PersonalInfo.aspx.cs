using Business.Common;

using System;
using System.Data;
using System.Web;

namespace WebAppAegisCRM.HR
{
    public partial class PersonalInfo : System.Web.UI.Page
    {
        
        private int EmployeeMasterId
        {
            get { return Convert.ToInt32(ViewState["EmployeeMasterId"]); }
            set { ViewState["EmployeeMasterId"] = value; }
        }
        private bool ValidateImageUpload()
        {
            if (FileUpload1.FileBytes.Length > 250000)
            {
                MessageBox.IsSuccess = false;
                MessageBox.Text = string.Format("File size is too big. Max 250kb is allowed.");
                MessageBox.Show = true;

                return false;
            }
            if (!(System.IO.Path.GetExtension(FileUpload1.FileName).Equals(".jpg") || System.IO.Path.GetExtension(FileUpload1.FileName).Equals(".png")))
            {
                MessageBox.IsSuccess = false;
                MessageBox.Text = string.Format("{0} format is not allowed. JPG and PNG formats are allowed only.", System.IO.Path.GetExtension(FileUpload1.FileName));
                MessageBox.Show = true;

                return false;
            }

            return true;
        }
        private void EmployeeMaster_ById(int Id)
        {
            try
            {
                Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
                Entity.HR.EmployeeMaster employeeMaster = new Entity.HR.EmployeeMaster();
                employeeMaster.EmployeeMasterId = Id;
                DataTable dtEmployeeMaster = objEmployeeMaster.EmployeeMaster_ById(employeeMaster);
                EmployeeMasterId = Convert.ToInt32(dtEmployeeMaster.Rows[0]["EmployeeMasterId"].ToString());
                lblName.Text = dtEmployeeMaster.Rows[0]["EmployeeName"].ToString();
                lblDateOfBirth.Text = (dtEmployeeMaster.Rows[0]["DOB"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dtEmployeeMaster.Rows[0]["DOB"].ToString()).ToString("dd MMM yyyy");
                lblMobile.Text = dtEmployeeMaster.Rows[0]["PersonalMobileNo"].ToString();
                lblPersonalEmail.Text = dtEmployeeMaster.Rows[0]["PersonalEmailId"].ToString();
                lblOfficialEmail.Text = dtEmployeeMaster.Rows[0]["OfficeEmailId"].ToString();
                lblAddress.Text = string.Concat(dtEmployeeMaster.Rows[0]["pAddress"].ToString(), ", ", dtEmployeeMaster.Rows[0]["PermanentCity"].ToString(), ", ", dtEmployeeMaster.Rows[0]["pPIN"].ToString());
                lblDesignation.Text = dtEmployeeMaster.Rows[0]["DesignationName"].ToString();
                lblDateOfJoining.Text = (dtEmployeeMaster.Rows[0]["DOJ"] == DBNull.Value) ? string.Empty : Convert.ToDateTime(dtEmployeeMaster.Rows[0]["DOJ"].ToString()).ToString("dd MMM yyyy");
                lblReporting.Text = dtEmployeeMaster.Rows[0]["ReportingPersion"].ToString();
                Image1.ImageUrl = "EmployeeImage\\" + dtEmployeeMaster.Rows[0]["Image"].ToString();

            }
            catch (Exception ex)
            {
                ex.WriteException();
                MessageBox.IsSuccess = false;
                MessageBox.Text = ex.Message;
                MessageBox.Show = true;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    MessageBox.Show = false;
                    EmployeeMaster_ById(Convert.ToInt32(HttpContext.Current.User.Identity.Name));
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
            }
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateImageUpload())
                {
                    Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
                    Entity.HR.EmployeeMaster employeeMaster = new Entity.HR.EmployeeMaster()
                    {
                        EmployeeMasterId = this.EmployeeMasterId,
                        Image = (FileUpload1.HasFile) ? string.Concat(EmployeeMasterId.ToString(), System.IO.Path.GetExtension(FileUpload1.FileName)) : string.Empty
                    };
                    int response = objEmployeeMaster.Employee_Update(employeeMaster);
                    if (response > 0)
                    {
                        if (FileUpload1.HasFile)
                            FileUpload1.PostedFile.SaveAs(Server.MapPath(" ") + "\\EmployeeImage\\" + employeeMaster.Image);

                        MessageBox.IsSuccess = true;
                        MessageBox.Text = "Image update successfully. Please clear browser cache to see.";
                        MessageBox.Show = true;
                        EmployeeMaster_ById(Convert.ToInt32(HttpContext.Current.User.Identity.Name));
                    }
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
            }
        }
    }
}