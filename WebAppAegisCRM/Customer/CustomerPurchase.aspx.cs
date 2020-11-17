using Business.Common;

using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Customer
{
    public partial class CustomerPurchase : System.Web.UI.Page
    {
        
        Business.Customer.Customer objCustomer = new Business.Customer.Customer();
        Entity.Customer.Customer customer = new Entity.Customer.Customer();
        public int CustomerMasterId
        {
            get { return Convert.ToInt32(ViewState["CustomerMasterId"]); }
            set { ViewState["CustomerMasterId"] = value; }
        }
        public int CustomerPurchaseId
        {
            get { return Convert.ToInt32(ViewState["CustomerPurchaseId"]); }
            set { ViewState["CustomerPurchaseId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                {
                    Response.Redirect("~/MainLogout.aspx");
                }
                if (!IsPostBack)
                {
                    LoadBrand();
                    if (HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
                        GetAllCustomer();
                    else
                        Customer_GetByAssignEngineerId();
                    LoadProduct();
                    LoadEmployee();
                    LoadContractType();
                    Message.Show = false;

                    //Direct link from dashboard lists
                    if (Request.QueryString["customerId"] != null && Request.QueryString["source"] != null && Request.QueryString["contractId"] != null)
                    {
                        if (Request.QueryString["source"].ToString() == "dashboard" || Request.QueryString["source"].ToString() == "contractStatus")
                        {
                            CustomerMasterId = int.Parse(Request.QueryString["customerId"].ToString());
                            popupHeader2.InnerHtml = objCustomer.CustomerPurchase_GetByCustomerId(CustomerMasterId).Rows[0]["CustomerName"].ToString();
                            LoadCustomerPurchaseListForContract();
                            LoadContractList();
                            ClearControlForContract();
                            LoadContractDetail(int.Parse(Request.QueryString["contractId"].ToString()));
                            ModalPopupExtender2.Show();
                        }
                    }
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
        #region User Defined Funtions
        private void Customer_GetByAssignEngineerId()
        {
            Business.Customer.Customer objCustomer = new Business.Customer.Customer();
            Entity.Customer.Customer customer = new Entity.Customer.Customer();
            customer.AssignEngineer = int.Parse(HttpContext.Current.User.Identity.Name);
            customer.CustomerName = txtName.Text.Trim();
            customer.PageIndex = gvCustomerMaster.PageIndex;
            customer.PageSize = gvCustomerMaster.PageSize;

            DataSet ds = objCustomer.Customer_CustomerMaster_GetByAssignEngineerIdWithPaging(customer);
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
        private void GetAllCustomer()
        {
            Business.Customer.Customer objCustomer = new Business.Customer.Customer();
            Entity.Customer.Customer customer = new Entity.Customer.Customer();
            customer.CompanyMasterId_FK = 1;
            customer.CustomerName = txtName.Text.Trim();
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
        private void LoadBrand()
        {
            Business.Inventory.BrandMaster objBrandMaster = new Business.Inventory.BrandMaster();
            ddlBrand.DataSource = objBrandMaster.GetAll();
            ddlBrand.DataTextField = "BrandName";
            ddlBrand.DataValueField = "BrandId";
            ddlBrand.DataBind();
            ddlBrand.InsertSelect();
        }
        private void PopulateCustomerPurchaseDetails()
        {
            customer = objCustomer.CustomerPurchase_GetByCustomerPurchaseId(CustomerPurchaseId);
            if (customer != null)
            {
                try
                {
                    txtMachineId.Text = customer.MachineId;
                    txtProductSlNo.Text = customer.SerialNo;
                    txtCustomerRemarks.Text = customer.CustomerRemarks;
                    txtContactPerson.Text = customer.ContactPerson;
                    txtAddress.Text = customer.Address;
                    txtMobileNo.Text = customer.MobileNo;
                    txtPhone.Text = customer.PhoneNo;
                    ddlBrand.SelectedValue = Convert.ToString(customer.BrandId);
                    ddlAssignEngineer.SelectedValue = Convert.ToString(customer.AssignEngineer);
                    LoadProduct();
                    ddlProduct.SelectedValue = Convert.ToString(customer.Productid);
                    txtInstallationDate.Text = (customer.InstallationDate == DateTime.MinValue) ? string.Empty : customer.InstallationDate.ToString("dd MMM yyyy");
                }
                catch
                { }
            }
        }
        private void LoadProduct()
        {
            Business.Inventory.ProductMaster objProductMaster = new Business.Inventory.ProductMaster();
            Entity.Inventory.ProductMaster productmaster = new Entity.Inventory.ProductMaster();

            productmaster.CompanyMasterId = 1;
            productmaster.Nature = 1;
            productmaster.BrandId = (ddlBrand.SelectedIndex == 0) ? 0 : int.Parse(ddlBrand.SelectedValue);
            DataTable dt = objProductMaster.GetAll(productmaster);
            ddlProduct.DataSource = dt;
            ddlProduct.DataTextField = "ProductName";
            ddlProduct.DataValueField = "ProductMasterId";
            ddlProduct.DataBind();
            ddlProduct.InsertSelect();
        }
        private void LoadEmployee()
        {
            Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
            Entity.HR.EmployeeMaster employeeMaster = new Entity.HR.EmployeeMaster();

            employeeMaster.CompanyId_FK = 1;
            DataTable dt = objEmployeeMaster.Employee_GetAll_Active(employeeMaster);
            dt = dt.Select("DesignationMasterId IN (1,3)").CopyToDataTable();
            ddlAssignEngineer.DataSource = dt;
            ddlAssignEngineer.DataTextField = "EmployeeName";
            ddlAssignEngineer.DataValueField = "EmployeeMasterId";
            ddlAssignEngineer.DataBind();
            ddlAssignEngineer.InsertSelect();
        }
        private void LoadCustomerPurchaseList()
        {
            gvCustomerPurchaseList.DataSource = objCustomer.CustomerPurchase_GetByCustomerId(CustomerMasterId);
            gvCustomerPurchaseList.DataBind();
        }
        private void LoadCustomerPurchaseListForContract()
        {
            gvCustomerPurchaseListForContract.DataSource = objCustomer.CustomerPurchase_GetByCustomerId(CustomerMasterId);
            gvCustomerPurchaseListForContract.DataBind();
        }
        private void LoadContractType()
        {
            Business.Customer.Contract objContract = new Business.Customer.Contract();
            ddlContractType.DataSource = objContract.GetAll();
            ddlContractType.DataTextField = "ContractName";
            ddlContractType.DataValueField = "ContractTypeId";
            ddlContractType.DataBind();
            ddlContractType.InsertSelect();
        }
        private void ClearControl()
        {
            //ddlNature.SelectedIndex = 0;
            txtCustomerRemarks.Text = string.Empty;
            //txtInstallationDate.Text = string.Empty;
            //txtInvoiceNo.Text = string.Empty;
            //txtProductPrice.Text = string.Empty;
            txtProductSlNo.Text = string.Empty;
            //txtPurchaseDate.Text = string.Empty;
            //txtSalesRemarks.Text = string.Empty;
            //ddlInstalledBy.SelectedIndex = 0;
            ddlProduct.SelectedIndex = 0;
            //ddlSalesExecutive.SelectedIndex = 0;
            //ddlTehnician.SelectedIndex = 0;
            ddlAssignEngineer.SelectedIndex = 0;
            ddlBrand.SelectedIndex = 0;
            txtContactPerson.Text = "";
            txtAddress.Text = "";
            txtMobileNo.Text = "";
            txtPhone.Text = "";
            txtInstallationDate.Text = string.Empty;
            CustomerPurchaseId = 0;
        }
        private void ClearControlForContract()
        {
            ddlContractType.SelectedIndex = 0;
            txtContractStartDate.Text = "";
            txtContractEndDate.Text = "";
            LoadCustomerPurchaseListForContract();
            Message1.Show = false;
        }
        private void Save()
        {
            customer.CustomerPurchaseId = CustomerPurchaseId;
            customer.CustomerMasterId = CustomerMasterId;
            customer.Productid = int.Parse(ddlProduct.SelectedValue);
            customer.SerialNo = txtProductSlNo.Text;
            customer.PurchaseDate = DateTime.MinValue;
            customer.MachineId = "";
            customer.InstallationDate = (string.IsNullOrEmpty(txtInstallationDate.Text.Trim())) ? DateTime.MinValue : Convert.ToDateTime(txtInstallationDate.Text.Trim());
            customer.AssignEngineer = int.Parse(ddlAssignEngineer.SelectedValue);
            customer.CustomerRemarks = txtCustomerRemarks.Text;
            customer.UserId = int.Parse(HttpContext.Current.User.Identity.Name);
            customer.CompanyMasterId_FK = 1;
            customer.ContactPerson = txtContactPerson.Text;
            customer.Address = txtAddress.Text;
            customer.MobileNo = txtMobileNo.Text;
            customer.PhoneNo = txtPhone.Text;
            customer.Stamp = (FileUploadStamp.HasFile) ? System.IO.Path.GetExtension(FileUploadStamp.FileName) : "";

            string retValue = objCustomer.CustomerPurchase_Save(customer);
            if (!string.IsNullOrEmpty(retValue))
            {
                if (FileUploadStamp.HasFile)
                    FileUploadStamp.PostedFile.SaveAs(Server.MapPath(" ") + "\\StampImage\\" + retValue + customer.Stamp);
                LoadCustomerPurchaseList();
                ClearControl();
                Message.IsSuccess = true;
                Message.Text = "Customer purchase data saved";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Customer purchase data con not save.";
            }
            Message.Show = true;
            ModalPopupExtender1.Show();
        }
        private void ContractSave()
        {
            Business.Service.Contract objContract = new Business.Service.Contract();
            Entity.Service.Contract contract = new Entity.Service.Contract();

            contract.CreatedBy = int.Parse(HttpContext.Current.User.Identity.Name);
            contract.ContractId = 0;
            contract.CustomerId = CustomerMasterId;
            contract.ContractTypeId = int.Parse(ddlContractType.SelectedValue);
            contract.ContractStartDate = (!String.IsNullOrEmpty(txtContractStartDate.Text.Trim())) ? Convert.ToDateTime(txtContractStartDate.Text.Trim()) : DateTime.MinValue;
            contract.ContractEndDate = (!String.IsNullOrEmpty(txtContractEndDate.Text.Trim())) ? Convert.ToDateTime(txtContractEndDate.Text.Trim()) : DateTime.MinValue;

            using (DataTable dtContractDetails = new DataTable())
            {
                dtContractDetails.Columns.Add("CustomerPurchaseId");
                dtContractDetails.Columns.Add("A3BWStartMeter");
                dtContractDetails.Columns.Add("A4BWStartMeter");
                dtContractDetails.Columns.Add("A3CLStartMeter");
                dtContractDetails.Columns.Add("A4CLStartMeter");
                dtContractDetails.Columns.Add("A3BWPages");
                dtContractDetails.Columns.Add("A4BWPages");
                dtContractDetails.Columns.Add("A3CLPages");
                dtContractDetails.Columns.Add("A4CLPages");
                dtContractDetails.Columns.Add("A3BWRate");
                dtContractDetails.Columns.Add("A4BWRate");
                dtContractDetails.Columns.Add("A3CLRate");
                dtContractDetails.Columns.Add("A4CLRate");

                foreach (GridViewRow gvr in gvCustomerPurchaseListForContract.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkSelect")).Checked)
                    {
                        dtContractDetails.Rows.Add();
                        dtContractDetails.Rows[dtContractDetails.Rows.Count - 1]["CustomerPurchaseId"] = gvCustomerPurchaseListForContract.DataKeys[gvr.RowIndex].Values[0].ToString();
                        dtContractDetails.Rows[dtContractDetails.Rows.Count - 1]["A3BWStartMeter"] = (!String.IsNullOrEmpty(((TextBox)gvr.FindControl("txtA3BWStartMeter")).Text.Trim())) ? ((TextBox)gvr.FindControl("txtA3BWStartMeter")).Text.Trim() : "";
                        dtContractDetails.Rows[dtContractDetails.Rows.Count - 1]["A4BWStartMeter"] = (!String.IsNullOrEmpty(((TextBox)gvr.FindControl("txtA4BWStartMeter")).Text.Trim())) ? ((TextBox)gvr.FindControl("txtA4BWStartMeter")).Text.Trim() : "";
                        dtContractDetails.Rows[dtContractDetails.Rows.Count - 1]["A3CLStartMeter"] = (!String.IsNullOrEmpty(((TextBox)gvr.FindControl("txtA3CLStartMeter")).Text.Trim())) ? ((TextBox)gvr.FindControl("txtA3CLStartMeter")).Text.Trim() : "";
                        dtContractDetails.Rows[dtContractDetails.Rows.Count - 1]["A4CLStartMeter"] = (!String.IsNullOrEmpty(((TextBox)gvr.FindControl("txtA4CLStartMeter")).Text.Trim())) ? ((TextBox)gvr.FindControl("txtA4CLStartMeter")).Text.Trim() : "";
                        dtContractDetails.Rows[dtContractDetails.Rows.Count - 1]["A3BWPages"] = (!String.IsNullOrEmpty(((TextBox)gvr.FindControl("txtA3BWPage")).Text.Trim())) ? ((TextBox)gvr.FindControl("txtA3BWPage")).Text.Trim() : "";
                        dtContractDetails.Rows[dtContractDetails.Rows.Count - 1]["A4BWPages"] = (!String.IsNullOrEmpty(((TextBox)gvr.FindControl("txtA4BWPage")).Text.Trim())) ? ((TextBox)gvr.FindControl("txtA4BWPage")).Text.Trim() : "";
                        dtContractDetails.Rows[dtContractDetails.Rows.Count - 1]["A3CLPages"] = (!String.IsNullOrEmpty(((TextBox)gvr.FindControl("txtA3CLPage")).Text.Trim())) ? ((TextBox)gvr.FindControl("txtA3CLPage")).Text.Trim() : "";
                        dtContractDetails.Rows[dtContractDetails.Rows.Count - 1]["A4CLPages"] = (!String.IsNullOrEmpty(((TextBox)gvr.FindControl("txtA4CLPage")).Text.Trim())) ? ((TextBox)gvr.FindControl("txtA4CLPage")).Text.Trim() : "";
                        dtContractDetails.Rows[dtContractDetails.Rows.Count - 1]["A3BWRate"] = (!String.IsNullOrEmpty(((TextBox)gvr.FindControl("txtA3BWRate")).Text.Trim())) ? ((TextBox)gvr.FindControl("txtA3BWRate")).Text.Trim() : "";
                        dtContractDetails.Rows[dtContractDetails.Rows.Count - 1]["A4BWRate"] = (!String.IsNullOrEmpty(((TextBox)gvr.FindControl("txtA4BWRate")).Text.Trim())) ? ((TextBox)gvr.FindControl("txtA4BWRate")).Text.Trim() : "";
                        dtContractDetails.Rows[dtContractDetails.Rows.Count - 1]["A3CLRate"] = (!String.IsNullOrEmpty(((TextBox)gvr.FindControl("txtA3CLRate")).Text.Trim())) ? ((TextBox)gvr.FindControl("txtA3CLRate")).Text.Trim() : "";
                        dtContractDetails.Rows[dtContractDetails.Rows.Count - 1]["A4CLRate"] = (!String.IsNullOrEmpty(((TextBox)gvr.FindControl("txtA4CLRate")).Text.Trim())) ? ((TextBox)gvr.FindControl("txtA4CLRate")).Text.Trim() : "";

                        dtContractDetails.AcceptChanges();

                        //updating last meter reading in Customer Purchase
                        Business.Service.ServiceBook objServiceBook = new Business.Service.ServiceBook();
                        Entity.Service.ServiceBook serviceBook = new Entity.Service.ServiceBook();
                        serviceBook.CustomerPurchaseId = Convert.ToInt32(dtContractDetails.Rows[dtContractDetails.Rows.Count - 1]["CustomerPurchaseId"]);
                        serviceBook.A3BWMeterReading = string.IsNullOrEmpty(dtContractDetails.Rows[dtContractDetails.Rows.Count - 1]["A3BWStartMeter"].ToString()) ? 0
                                                        : Convert.ToInt32(dtContractDetails.Rows[dtContractDetails.Rows.Count - 1]["A3BWStartMeter"]);
                        serviceBook.A4BWMeterReading = string.IsNullOrEmpty(dtContractDetails.Rows[dtContractDetails.Rows.Count - 1]["A4BWStartMeter"].ToString()) ? 0
                                                        : Convert.ToInt32(dtContractDetails.Rows[dtContractDetails.Rows.Count - 1]["A4BWStartMeter"]);
                        serviceBook.A3CLMeterReading = string.IsNullOrEmpty(dtContractDetails.Rows[dtContractDetails.Rows.Count - 1]["A3CLStartMeter"].ToString()) ? 0
                                                        : Convert.ToInt32(dtContractDetails.Rows[dtContractDetails.Rows.Count - 1]["A3CLStartMeter"]);
                        serviceBook.A4CLMeterReading = string.IsNullOrEmpty(dtContractDetails.Rows[dtContractDetails.Rows.Count - 1]["A4CLStartMeter"].ToString()) ? 0
                                                        : Convert.ToInt32(dtContractDetails.Rows[dtContractDetails.Rows.Count - 1]["A4CLStartMeter"]);

                        int response = 0;
                        response = objServiceBook.Service_MeterReading_Update(serviceBook);

                        if (response == 0)
                        {
                            Message1.IsSuccess = false;
                            Message1.Text = "Current meter reading unable to update! Please add new entry or contact admin.";
                            Message1.Show = true;
                            ModalPopupExtender2.Show();
                            return;
                        }
                    }
                }

                if (dtContractDetails.Rows.Count > 0)
                    contract.ContractDetails = dtContractDetails;
                else
                {
                    Message1.IsSuccess = false;
                    Message1.Text = "Please select atleast 1 machine.";
                    Message1.Show = true;
                    ModalPopupExtender2.Show();
                    return;
                }
            }
            int retVal = objContract.Save(contract);
            if (retVal > 0)
            {
                LoadContractList();
                ClearControlForContract();

                Message1.IsSuccess = true;
                Message1.Text = "Data saved.";
                Message1.Show = true;
                ModalPopupExtender2.Show();
            }
            else
            {
                Message1.IsSuccess = false;
                Message1.Text = "Data not saved";
                Message1.Show = true;
                ModalPopupExtender2.Show();
            }
        }
        private void LoadContractList()
        {
            Business.Service.Contract objContract = new Business.Service.Contract();
            DataTable dt = objContract.GetAll(CustomerMasterId);
            if (dt.Rows.Count > 0)
                gvContractList.DataSource = dt;
            else
                gvContractList.DataSource = null;
            gvContractList.DataBind();
        }
        #endregion

        protected void ddlBrand_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadProduct();
                ModalPopupExtender1.Show();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
        protected void gvCustomerMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Business.Customer.Customer objCustomer = new Business.Customer.Customer();
                Entity.Customer.Customer customer = new Entity.Customer.Customer();

                if (e.CommandName == "PurchaseDetails")
                {
                    CustomerMasterId = int.Parse(e.CommandArgument.ToString());
                    customer.CustomerMasterId = CustomerMasterId;
                    DataTable dr = objCustomer.FetchCustomerDetailsById(customer);
                    popupHeader1.InnerHtml = (objCustomer.FetchCustomerDetailsById(customer) == null) ? "" : dr.Rows[0]["CustomerName"].ToString();
                    LoadCustomerPurchaseList();
                    ClearControl();
                    ModalPopupExtender1.Show();

                }
                else if (e.CommandName == "ContractDetails")
                {
                    CustomerMasterId = int.Parse(e.CommandArgument.ToString());
                    customer.CustomerMasterId = CustomerMasterId;
                    DataTable dr = objCustomer.FetchCustomerDetailsById(customer);
                    popupHeader2.InnerHtml = (objCustomer.FetchCustomerDetailsById(customer) == null) ? "" : dr.Rows[0]["CustomerName"].ToString();
                    LoadCustomerPurchaseListForContract();
                    LoadContractList();
                    ClearControlForContract();
                    ModalPopupExtender2.Show();
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("CustomerPurchase.aspx");
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
        protected void btnContractSave_Click(object sender, EventArgs e)
        {
            try
            {
                ContractSave();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
        protected void btnContractCancel_Click(object sender, EventArgs e)
        {
            try
            {
                Response.Redirect("CustomerPurchase.aspx");
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
        protected void ddlNature_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                ModalPopupExtender1.Show();
                LoadProduct();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
        protected void gvCustomerPurchaseList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Ed")
                {
                    int customerPurchaseId = int.Parse(e.CommandArgument.ToString());
                    CustomerPurchaseId = customerPurchaseId;
                    PopulateCustomerPurchaseDetails();
                    ModalPopupExtender1.Show();
                    TabContainer1.ActiveTabIndex = 0;
                }

                if (e.CommandName == "Del")
                {
                    try
                    {
                        Int64 customerpurchaseid = Int64.Parse(e.CommandArgument.ToString());
                        int i = objCustomer.CustomerPurchase_DeleteByCustomerPurchaseId(customerpurchaseid);
                        if (i > 0)
                        {
                            LoadCustomerPurchaseList();
                            Message.IsSuccess = true;
                            Message.Text = "Data deleted";
                        }
                        else
                        {
                            Message.IsSuccess = false;
                            Message.Text = "Data can not delete.";
                        }
                    }
                    catch (Exception ex)
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Data has dependency. Can not delete.";
                        Business.Common.ErrorLog.MasterErrorLog(Server.MapPath("~") + "/ErrorLog/Errors.txt", ex.Message, HttpContext.Current.User.Identity.Name);
                    }
                    Message.Show = true;
                    ModalPopupExtender1.Show();
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
        protected void gvContractList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    if (Convert.ToDateTime(((DataTable)((gvContractList.DataSource))).Rows[e.Row.RowIndex]["ContractEndDate"].ToString()) <= DateTime.Now)
                    {
                        e.Row.Attributes["style"] = "background-color: #FF8787"; //red
                    }
                    else if (Convert.ToDateTime(((DataTable)((gvContractList.DataSource))).Rows[e.Row.RowIndex]["ContractEndDate"].ToString()) <= DateTime.Now.AddMonths(1))
                    {
                        e.Row.Attributes["style"] = "background-color: #fff387"; //change color yellow
                    }
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
        protected void gvContractList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                ClearControlForContract();

                if (e.CommandName == "View")
                {
                    //CustomerMasterId = int.Parse(e.CommandArgument.ToString());                
                    LoadContractDetail(int.Parse(e.CommandArgument.ToString()));
                }
                else if (e.CommandName == "Del")
                {
                    Business.Service.Contract objContract = new Business.Service.Contract();

                    int i = objContract.Delete(int.Parse(e.CommandArgument.ToString()));
                    if (i > 0)
                    {
                        //LoadCustomerPurchaseList();
                        LoadContractList();
                        ClearControlForContract();
                        Message1.IsSuccess = true;
                        Message1.Text = "Data deleted.";
                    }
                    else
                    {
                        Message1.IsSuccess = false;
                        Message1.Text = "Data not deleted.";
                    }
                    Message1.Show = true;
                }
                ModalPopupExtender2.Show();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
        private void LoadContractDetail(int contractId)
        {
            Business.Service.Contract objContract = new Business.Service.Contract();
            DataSet ds = objContract.GetById(contractId);

            if (ds != null)
            {
                ddlContractType.SelectedValue = ds.Tables[0].Rows[0]["ContractType"].ToString();
                txtContractStartDate.Text = (ds.Tables[0].Rows[0]["ContractStartDate"] != DBNull.Value) ? Convert.ToDateTime(ds.Tables[0].Rows[0]["ContractStartDate"].ToString()).ToString("dd/MMM/yyyy") : "";
                txtContractEndDate.Text = (ds.Tables[0].Rows[0]["ContractEndDate"] != DBNull.Value) ? Convert.ToDateTime(ds.Tables[0].Rows[0]["ContractEndDate"].ToString()).ToString("dd/MMM/yyyy") : "";

                foreach (GridViewRow gvr in gvCustomerPurchaseListForContract.Rows)
                {
                    foreach (DataRow dr in ds.Tables[1].Rows)
                    {
                        if (gvCustomerPurchaseListForContract.DataKeys[gvr.RowIndex].Values[0].ToString() == dr["CustomerPurchaseId"].ToString())
                        {
                            ((CheckBox)gvr.FindControl("chkSelect")).Checked = true;
                            ((TextBox)gvr.FindControl("txtA3BWStartMeter")).Text = (dr["A3BWStartMeter"] != DBNull.Value) ? dr["A3BWStartMeter"].ToString() : "";
                            ((TextBox)gvr.FindControl("txtA4BWStartMeter")).Text = (dr["A4BWStartMeter"] != DBNull.Value) ? dr["A4BWStartMeter"].ToString() : "";
                            ((TextBox)gvr.FindControl("txtA3CLStartMeter")).Text = (dr["A3CLStartMeter"] != DBNull.Value) ? dr["A3CLStartMeter"].ToString() : "";
                            ((TextBox)gvr.FindControl("txtA4CLStartMeter")).Text = (dr["A4CLStartMeter"] != DBNull.Value) ? dr["A4CLStartMeter"].ToString() : "";
                            ((TextBox)gvr.FindControl("txtA3BWPage")).Text = (dr["A3BWPages"] != DBNull.Value) ? dr["A3BWPages"].ToString() : "";
                            ((TextBox)gvr.FindControl("txtA4BWPage")).Text = (dr["A4BWPages"] != DBNull.Value) ? dr["A4BWPages"].ToString() : "";
                            ((TextBox)gvr.FindControl("txtA3CLPage")).Text = (dr["A3CLPages"] != DBNull.Value) ? dr["A3CLPages"].ToString() : "";
                            ((TextBox)gvr.FindControl("txtA4CLPage")).Text = (dr["A4CLPages"] != DBNull.Value) ? dr["A4CLPages"].ToString() : "";
                            ((TextBox)gvr.FindControl("txtA3BWRate")).Text = (dr["A3BWRate"] != DBNull.Value) ? dr["A3BWRate"].ToString() : "";
                            ((TextBox)gvr.FindControl("txtA4BWRate")).Text = (dr["A4BWRate"] != DBNull.Value) ? dr["A4BWRate"].ToString() : "";
                            ((TextBox)gvr.FindControl("txtA3CLRate")).Text = (dr["A3CLRate"] != DBNull.Value) ? dr["A3CLRate"].ToString() : "";
                            ((TextBox)gvr.FindControl("txtA4CLRate")).Text = (dr["A4CLRate"] != DBNull.Value) ? dr["A4CLRate"].ToString() : "";
                        }
                    }
                }
                TabContainer2.ActiveTabIndex = 0;
            }
        }
        protected void gvCustomerMaster_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCustomerMaster.PageIndex = e.NewPageIndex;
                GetAllCustomer();
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
                GetAllCustomer();
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