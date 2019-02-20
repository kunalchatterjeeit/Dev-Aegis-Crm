using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Services.Description;
namespace WebAppAegisCRM.Customer
{
    public partial class CustomerMaster : Page
    {
        public long CustomerId
        {
            get { return Convert.ToInt64(ViewState["CustomerId"]); }
            set { ViewState["CustomerId"] = value; }
        }
        public long CustomerAddressId
        {
            get { return Convert.ToInt64(ViewState["CustomerAddressId"]); }
            set { ViewState["CustomerAddressId"] = value; }
        }
        public long CustomerContactDetailsId
        {
            get { return Convert.ToInt64(ViewState["CustomerContactDetailsId"]); }
            set { ViewState["CustomerContactDetailsId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                Response.Redirect("~/MainLogout.aspx");
            }
            if (!IsPostBack)
            {
                GetAllCustomer();
                CleartextBoxes(this);
            }
        }

        public void CleartextBoxes(Control parent)
        {

            foreach (Control c in parent.Controls)
            {
                if ((c.GetType() == typeof(TextBox)))
                {
                    ((TextBox)(c)).Text = "";
                }
                if (c.HasControls())
                {
                    CleartextBoxes(c);
                }
            }
            ddlCustomerType.SelectedIndex = 0;
            Message.Show = false;
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Business.Customer.Customer objCustomer = new Business.Customer.Customer();
            Entity.Customer.Customer customer = new Entity.Customer.Customer();
            customer.CompanyMasterId_FK = 1;
            customer.CustomerMasterId = CustomerId;
            customer.CustomerName = txtCustomerName.Text;
            customer.CustomerType = Convert.ToInt16(ddlCustomerType.SelectedValue);
            customer.EmailId = txtEmailId.Text;
            customer.MobileNo = txtMobileNo.Text;
            customer.PAddress = txtpAddress.Text;
            customer.PhoneNo = txtPhoneNo.Text;
            customer.Pin = txtPin.Text;
            customer.PStreet = txtpStreet.Text;
            customer.ReferenceName = txtrefferenceName.Text;
            customer.UserId = Convert.ToInt32(HttpContext.Current.User.Identity.Name);

            long i = objCustomer.Save(customer);
            if (i > 0)
            {
                CleartextBoxes(this);
                GetAllCustomer();
                CustomerId = 0;
                Message.IsSuccess = true;
                Message.Text = "Customer information saved successfully...";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can not save!!!";
            }
            Message.Show = true;
        }

        protected void GetAllCustomer()
        {
            Business.Customer.Customer objCustomer = new Business.Customer.Customer();
            Entity.Customer.Customer customer = new Entity.Customer.Customer();
            customer.CompanyMasterId_FK = 1;
            customer.PageIndex = gvCustomerMaster.PageIndex;
            customer.PageSize = gvCustomerMaster.PageSize;

            DataSet ds = objCustomer.GetAllCustomer(customer);
            if (ds.Tables.Count > 0)
            {
                gvCustomerMaster.DataSource = ds.Tables[0];
                gvCustomerMaster.VirtualItemCount = (ds.Tables[1].Rows.Count > 0) ? Convert.ToInt32(ds.Tables[1].Rows[0]["TotalCount"].ToString()) : 10;
                gvCustomerMaster.DataBind();
            }
            else
            {
                gvCustomerMaster.DataSource = null;
                gvCustomerMaster.DataBind();
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            CleartextBoxes(this);
            CustomerId = 0;
        }

        protected void gvCustomerMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "E")
            {
                CustomerId = Convert.ToInt64(e.CommandArgument.ToString());
                FetchCustomerDetailsById(CustomerId);

            }
            else if (e.CommandName == "D")
            {
                CustomerId = Convert.ToInt64(e.CommandArgument.ToString());
                Business.Customer.Customer ObjBelCustomer = new Business.Customer.Customer();
                Entity.Customer.Customer ObjElCustomer = new Entity.Customer.Customer();
                ObjElCustomer.CustomerMasterId = CustomerId;
                int i = 0;
                i = ObjBelCustomer.DeleteCustomer(ObjElCustomer);
                if (i > 0)
                {
                    CleartextBoxes(this);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "mmsg", "alert('Data Delete Succesfully....');", true);
                    CustomerId = 0;
                    GetAllCustomer();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "mmsg", "alert('Data Can not Delete!!!....');", true);
                }

            }
            else if (e.CommandName == "Address")
            {
                CustomerId = Convert.ToInt64(e.CommandArgument.ToString());
                GetAllAddress(Convert.ToInt64(e.CommandArgument.ToString()));
                TabContainer2.ActiveTab = AddressList;
                ModalPopupExtender2.Show();

            }
            else if (e.CommandName == "Contact")
            {
                CustomerId = Convert.ToInt64(e.CommandArgument.ToString());
                GetAllACustomerContactDetails(Convert.ToInt64(e.CommandArgument.ToString()));
                TabContainer1.ActiveTab = ContactDetails;
                ModalPopupExtender1.Show();
            }
        }

        protected void FetchCustomerDetailsById(long Id)
        {
            Business.Customer.Customer objCustomer = new Business.Customer.Customer();
            Entity.Customer.Customer customer = new Entity.Customer.Customer();
            customer.CustomerMasterId = Id;
            DataTable dt = objCustomer.FetchCustomerDetailsById(customer);
            if (dt.Rows.Count > 0)
            {
                txtCustomerName.Text = dt.Rows[0]["CustomerName"].ToString();
                ddlCustomerType.SelectedValue = dt.Rows[0]["CustomerType"].ToString();
                txtEmailId.Text = dt.Rows[0]["EmailId"].ToString();
                txtMobileNo.Text = dt.Rows[0]["MobileNo"].ToString();
                txtpAddress.Text = dt.Rows[0]["pAddress"].ToString();
                txtPhoneNo.Text = dt.Rows[0]["PhoneNo"].ToString();
                txtPin.Text = dt.Rows[0]["Pin"].ToString();
                txtpStreet.Text = dt.Rows[0]["pStreet"].ToString();
                txtrefferenceName.Text = dt.Rows[0]["ReferenceName"].ToString();
            }
        }

        protected void btnTSave_Click(object sender, EventArgs e)
        {
            Business.Customer.CustomerContactDetails objCustomerContactDetails = new Business.Customer.CustomerContactDetails();
            Entity.Customer.CustomerContactDetails customerContactDetails = new Entity.Customer.CustomerContactDetails();

            customerContactDetails.CustomerContactDetailsId = CustomerContactDetailsId;
            customerContactDetails.CustomerMasterId_FK = CustomerId;
            customerContactDetails.ContactPerson = txtcontactPerson.Text;
            customerContactDetails.CPDesignation = txtcontactDesignation.Text;
            customerContactDetails.CPPhoneNo = txtCphoneNumber.Text;
            customerContactDetails.CompanyMasterId_FK = 1;
            customerContactDetails.UserId = 1;
            int i = 0;
            i = objCustomerContactDetails.SaveContactDeatails(customerContactDetails);
            if (i > 0)
            {
                CleartextBoxes(this);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "mmsg", "alert('Data Save Succesfully....');", true);
                GetAllACustomerContactDetails(CustomerId);
                CustomerContactDetailsId = 0;
                TabContainer1.ActiveTab = ContactDetails;
                ModalPopupExtender1.Show();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "mmsg", "alert('Data Can not Save!!!....');", true);
                //Message.IsSuccess = false;
                //Message.Text = "Can not save!!!";
            }
        }

        protected void btnSaveAddressDetails_Click(object sender, EventArgs e)
        {

            Business.Customer.CustomerAddress objCustomerAddress = new Business.Customer.CustomerAddress();
            Entity.Customer.CustomerAddress customerAddress = new Entity.Customer.CustomerAddress();
            customerAddress.CustomerAddressId = CustomerAddressId;
            customerAddress.TAddress = txttAddress.Text;
            customerAddress.TStreet = txtStreet.Text;
            customerAddress.CompanyMasterId_FK = 1;
            customerAddress.UserId = 1;
            customerAddress.CustomerMasterId_FK = CustomerId;
            int i = 0;
            i = objCustomerAddress.SaveCustomerAddress(customerAddress);
            if (i > 0)
            {
                CleartextBoxes(this);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "mmsg", "alert('Data save succesfully....');", true);
                GetAllAddress(CustomerId);
                CustomerAddressId = 0;
                TabContainer2.ActiveTab = AddressList;
                ModalPopupExtender2.Show();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "mmsg", "alert('Data Ccn not save!!!....');", true);
                //Message.IsSuccess = false;
                //Message.Text = "Can not save!!!";
            }

        }

        protected void GetAllACustomerContactDetails(long Id)
        {
            Business.Customer.CustomerContactDetails objustomerContactDetails = new Business.Customer.CustomerContactDetails();
            Entity.Customer.CustomerContactDetails customerContactDetails = new Entity.Customer.CustomerContactDetails();
            customerContactDetails.CompanyMasterId_FK = 1;
            customerContactDetails.CustomerMasterId_FK = Id;
            DataTable dt = objustomerContactDetails.GetAllACustomerContactDetails(customerContactDetails);
            if (dt.Rows.Count > 0)
            {
                gvContactDetails.DataSource = dt;
                gvContactDetails.DataBind();
            }
            else
            {

                gvContactDetails.DataSource = null;
                gvContactDetails.DataBind();
            }

        }

        protected void GetAllAddress(long Id)
        {
            Business.Customer.CustomerAddress objCustomerAddress = new Business.Customer.CustomerAddress();
            Entity.Customer.CustomerAddress customerAddress = new Entity.Customer.CustomerAddress();
            customerAddress.CompanyMasterId_FK = 1;
            customerAddress.CustomerMasterId_FK = Id;
            DataTable dt = objCustomerAddress.GetAllAddress(customerAddress);
            if (dt.Rows.Count > 0)
            {
                gvCustomerAddressList.DataSource = dt;
                gvCustomerAddressList.DataBind();
            }
            else
            {

                gvCustomerAddressList.DataSource = null;
                gvCustomerAddressList.DataBind();
            }
        }

        protected void FetchCustomerContactDetailsById(long Id)
        {
            Business.Customer.CustomerContactDetails objCustomerContactDetails = new Business.Customer.CustomerContactDetails();
            Entity.Customer.CustomerContactDetails customerContactDetails = new Entity.Customer.CustomerContactDetails();
            customerContactDetails.CustomerContactDetailsId = Id;
            DataTable dr = objCustomerContactDetails.FetchCustomerContactDetailsById(customerContactDetails);
            if (dr.Rows.Count > 0)
            {
                txtcontactDesignation.Text = dr.Rows[0]["CPDesignation"].ToString();
                txtcontactPerson.Text = dr.Rows[0]["ContactPerson"].ToString();
                txtCphoneNumber.Text = dr.Rows[0]["CPPhoneNo"].ToString();
                CustomerContactDetailsId = Convert.ToInt64(dr.Rows[0]["CustomerContactDetailsId"].ToString());

            }
        }

        protected void FetchCustomerAddressDetailsById(long Id)
        {
            Business.Customer.CustomerAddress objCustomerAddress = new Business.Customer.CustomerAddress();
            Entity.Customer.CustomerAddress customerAddress = new Entity.Customer.CustomerAddress();
            customerAddress.CustomerAddressId = Id;
            DataTable dr = objCustomerAddress.FetchCustomerAddressDetailsById(customerAddress);
            if (dr.Rows.Count > 0)
            {
                txttAddress.Text = dr.Rows[0]["tAddress"].ToString();
                txtStreet.Text = dr.Rows[0]["tStreet"].ToString();
                CustomerAddressId = Convert.ToInt64(dr.Rows[0]["CustomerAddressId"].ToString());
            }
        }

        protected void gvContactDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "E")
            {
                CustomerContactDetailsId = Convert.ToInt64(e.CommandArgument.ToString());
                FetchCustomerContactDetailsById(Convert.ToInt64(e.CommandArgument.ToString()));
                TabContainer1.ActiveTab = AddContact;
                ModalPopupExtender1.Show();
            }
            else if (e.CommandName == "D")
            {
                CustomerContactDetailsId = Convert.ToInt64(e.CommandArgument.ToString());
                TabContainer1.ActiveTab = AddContact;
                int i = 0;
                Business.Customer.CustomerContactDetails ObjBelCustomerContactDetails = new Business.Customer.CustomerContactDetails();
                Entity.Customer.CustomerContactDetails ObjElCustomerContactDetails = new Entity.Customer.CustomerContactDetails();
                ObjElCustomerContactDetails.CustomerContactDetailsId = CustomerContactDetailsId;

                i = ObjBelCustomerContactDetails.DeleteCustomerContactDetailsById(ObjElCustomerContactDetails);
                if (i > 0)
                {
                    CleartextBoxes(this);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "mmsg", "alert('Data Delete Succesfully....');", true);
                    CustomerContactDetailsId = 0;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "mmsg", "alert('Data Can not Delete!!!....');", true);
                }
                //ModalPopupExtender1.Show();
                CustomerContactDetailsId = 0;
            }
        }

        protected void gvCustomerAddressList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "E")
            {
                CustomerAddressId = Convert.ToInt64(e.CommandArgument.ToString());
                FetchCustomerAddressDetailsById(Convert.ToInt64(e.CommandArgument.ToString()));
                TabContainer2.ActiveTab = AddAddress;
                ModalPopupExtender2.Show();

            }
            else if (e.CommandName == "D")
            {
                CustomerAddressId = Convert.ToInt64(e.CommandArgument.ToString());
                int i = 0;
                Business.Customer.CustomerAddress ObjBelCustomerAddress = new Business.Customer.CustomerAddress();
                Entity.Customer.CustomerAddress ObjElCustomerAddress = new Entity.Customer.CustomerAddress();
                // ObjElCustomerAddress.CustomerAddressId = Id;
                ObjElCustomerAddress.CustomerAddressId = CustomerAddressId;

                i = ObjBelCustomerAddress.DeleteCustomerAddressById(ObjElCustomerAddress);
                if (i > 0)
                {
                    CleartextBoxes(this);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "mmsg", "alert('Data Delete Succesfully....');", true);
                    CustomerContactDetailsId = 0;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "mmsg", "alert('Data Can not Delete!!!....');", true);
                }
                //ModalPopupExtender1.Show();
                CustomerContactDetailsId = 0;
            }
        }

        protected void gvCustomerMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCustomerMaster.PageIndex = e.NewPageIndex;
            GetAllCustomer();
        }
    }
}