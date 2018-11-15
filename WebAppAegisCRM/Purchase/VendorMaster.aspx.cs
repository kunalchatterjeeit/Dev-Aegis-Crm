using Business.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Purchase
{
    public partial class VendorMaster : System.Web.UI.Page
    {
        Business.Purchase.Vendor objVendorMaster = new Business.Purchase.Vendor();
        Entity.Purchase.Vendor vendormaster = new Entity.Purchase.Vendor();

        public int VendorId
        {
            get { return Convert.ToInt32(ViewState["VendorId"]); }
            set { ViewState["VendorId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Message.Show = false;
                if (Request.QueryString["vendorid"] != null && Request.QueryString["vendorid"].ToString().Length > 0)
                {
                    VendorId = int.Parse(Request.QueryString["vendorid"].ToString());
                    PopulateVendor();
                }
                City_GetAll();
            }
        }
        protected void City_GetAll()
        {
            Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();            
            DataTable dt = objEmployeeMaster.City_GetAll();
            if (dt.Rows.Count > 0)
            {
                ddlCity.DataSource = dt;
                ddlCity.DataBind();
            }
            ddlCity.InsertSelect();
        }

        private void ClearControl()
        {
            txtGSTNo.Text = string.Empty;
            txtVendorName.Text = string.Empty;
            txtAddress.Text = string.Empty;
            ddlState.SelectedIndex = 0;
            ddlDistrict.SelectedIndex = 0;
            ddlCity.SelectedIndex = 0;
            txtPin.Text = string.Empty;
            txtTAN.Text = string.Empty;
            txtStateCode.Text = string.Empty;
            txtPAN.Text = string.Empty;
            txtCST.Text = string.Empty;
            txtPhoneNo.Text = string.Empty;
            txtMobileNo.Text = string.Empty;
            txtMail.Text = string.Empty;
            txtFax.Text = string.Empty;
        }

        private void PopulateVendor()
        {
            vendormaster = objVendorMaster.GetById(VendorId);

            txtVendorName.Text = vendormaster.VendorName;
            txtAddress.Text = vendormaster.Address;
            ddlState.SelectedValue = Convert.ToString(vendormaster.StateId);
            ddlDistrict.SelectedValue = Convert.ToString(vendormaster.DistrictId);
            ddlCity.SelectedValue = Convert.ToString(vendormaster.CityId);
            txtPin.Text = Convert.ToString(vendormaster.PinId);
            txtTAN.Text = vendormaster.Tan;
            txtStateCode.Text = vendormaster.StateCode;
            txtPAN.Text = vendormaster.Pan;
            txtCST.Text = vendormaster.CST;
            txtPhoneNo.Text = vendormaster.PhoneNo;
            txtMobileNo.Text = vendormaster.MobileNo;
            txtFax.Text = vendormaster.Fax;
            txtMail.Text = vendormaster.MailId;
            txtActiveDate.Text = (vendormaster.ActiveDate == DateTime.MinValue) ? string.Empty : Convert.ToString(vendormaster.ActiveDate.ToString("dd MMM yyyy"));
            ddlStatus.SelectedValue = vendormaster.Status.ToString();
            txtConcernedPerson.Text = vendormaster.ConcernedPerson;
            txtBankName.Text = vendormaster.BankName;
            txtBankBranch.Text = vendormaster.BankBranch;
            txtACNo.Text = vendormaster.ACNo;
            txtIFSC.Text = vendormaster.IFSC;
            txtGSTNo.Text = vendormaster.GSTNo;
        }

        protected void Save()
        {
            vendormaster.VendorId = VendorId;
            vendormaster.VendorName = txtVendorName.Text;
            vendormaster.Address = txtAddress.Text;
            vendormaster.CountryId = 1;
            vendormaster.StateId = (ddlState.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlState.SelectedValue);
            vendormaster.DistrictId = (ddlDistrict.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlDistrict.SelectedValue);
            vendormaster.CityId = (ddlCity.SelectedIndex == 0) ? 0 : Convert.ToInt32(ddlCity.SelectedValue);
            vendormaster.PinId = Convert.ToInt32(txtPin.Text);
            vendormaster.Tan = txtTAN.Text;
            vendormaster.StateCode = txtStateCode.Text;
            vendormaster.Pan = txtPAN.Text;
            vendormaster.CST = txtCST.Text;
            vendormaster.ActiveDate = (txtActiveDate.Text == string.Empty) ? DateTime.MinValue : DateTime.Parse(txtActiveDate.Text);
            vendormaster.ConcernedPerson = txtConcernedPerson.Text;
            vendormaster.BankName = txtBankName.Text;
            vendormaster.BankBranch = txtBankBranch.Text;
            vendormaster.ACNo = txtACNo.Text;
            vendormaster.IFSC = txtIFSC.Text;
            vendormaster.Status = Convert.ToBoolean(ddlStatus.SelectedValue);
            vendormaster.PhoneNo = txtPhoneNo.Text;
            vendormaster.MobileNo = txtMobileNo.Text;
            vendormaster.MailId = txtMail.Text;
            vendormaster.Fax = txtFax.Text;
            vendormaster.UserId = int.Parse(HttpContext.Current.User.Identity.Name);
            vendormaster.CompanyId = 1;
            vendormaster.GSTNo = txtGSTNo.Text;
            int i = objVendorMaster.Save(vendormaster);
            if (i > 0)
            {
                ClearControl();
                Message.IsSuccess = true;
                Message.Text = "Data saved";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Data not saved";
            }
            Message.Show = true;

        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            Response.Redirect("VendorMaster.aspx");
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Save();
            ddlState.Items.Clear();
            ddlDistrict.Items.Clear();
            ddlCity.Items.Clear();
        }
    }
}