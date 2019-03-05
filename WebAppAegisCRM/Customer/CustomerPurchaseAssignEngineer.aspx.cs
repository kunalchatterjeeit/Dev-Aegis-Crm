using Business.Common;
using System;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Customer
{
    public partial class CustomerPurchaseAssignEngineer : System.Web.UI.Page
    {
        protected void LoadCustomer()
        {
            Business.Customer.Customer objCustomer = new Business.Customer.Customer();
            Entity.Customer.Customer customer = new Entity.Customer.Customer();
            if (HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
                customer.AssignEngineer = 0;
            else
                customer.AssignEngineer = int.Parse(HttpContext.Current.User.Identity.Name);
            DataTable dt = objCustomer.Customer_Customer_GetByAssignEngineerId(customer);
            ddlCustomer.DataSource = dt;
            ddlCustomer.DataTextField = "CustomerName";
            ddlCustomer.DataValueField = "CustomerMasterId";
            ddlCustomer.DataBind();
        }

        private void LoadEmployee(DropDownList ddlAssignEngineer)
        {
            Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
            Entity.HR.EmployeeMaster employeeMaster = new Entity.HR.EmployeeMaster();

            employeeMaster.CompanyId_FK = 1;
            DataTable dt = objEmployeeMaster.EmployeeMaster_GetAll(employeeMaster);
            dt = dt.Select("DesignationMasterId IN (1,3)").CopyToDataTable();
            ddlAssignEngineer.DataSource = dt;
            ddlAssignEngineer.DataTextField = "EmployeeName";
            ddlAssignEngineer.DataValueField = "EmployeeMasterId";
            ddlAssignEngineer.DataBind();
            ddlAssignEngineer.InsertSelect();
        }

        private void LoadCustomerPurchase()
        {
            Business.Customer.Customer objCustomer = new Business.Customer.Customer();
            DataTable dtPurchase = objCustomer.CustomerPurchase_GetByCustomerId(int.Parse(ddlCustomer.SelectedValue));
            gvCustomerPurchase.DataSource = dtPurchase;
            gvCustomerPurchase.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Message.Show = false;
                LoadCustomer();
                LoadCustomerPurchase();
            }
        }

        protected void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chkSelect = (CheckBox)sender;
            GridViewRow gv = (GridViewRow)chkSelect.NamingContainer;
            DropDownList ddlAssignEngineer = (DropDownList)gv.FindControl("ddlAssignedEngineer");
            ddlAssignEngineer.Enabled = chkSelect.Checked;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            LoadCustomerPurchase();
        }

        protected void btnSaveAssignment_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateAssignment())
                {
                    Business.Customer.Customer objCustomer = new Business.Customer.Customer();

                    foreach (GridViewRow gvr in gvCustomerPurchase.Rows)
                    {
                        if (((CheckBox)gvr.FindControl("chkSelect")).Checked)
                        {
                            long customerPurchaseId = (long)gvCustomerPurchase.DataKeys[gvr.RowIndex]["CustomerPurchaseId"];
                            DropDownList ddlAssignEngineer = (DropDownList)gvr.FindControl("ddlAssignedEngineer");
                            int assignedEngineerId = Convert.ToInt32(ddlAssignEngineer.SelectedValue);

                            objCustomer.Customer_CustomerPurchaseAssignEngineer_Save(customerPurchaseId, assignedEngineerId);
                        }
                    }

                    Message.IsSuccess = true;
                    Message.Text = "Engineers are assigned successfully.";
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                Message.IsSuccess = false;
                Message.Text = ex.Message;
            }
            Message.Show = true;
        }

        private bool ValidateAssignment()
        {
            foreach (GridViewRow gvr in gvCustomerPurchase.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkSelect")).Checked)
                {
                    DropDownList ddlAssignEngineer = (DropDownList)gvr.FindControl("ddlAssignedEngineer");
                    if (ddlAssignEngineer.SelectedIndex == 0)
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Please select engineer to assign at row number: " + (Convert.ToInt32(gvr.RowIndex) + 1).ToString();
                        Message.Show = true;
                        ddlAssignEngineer.Focus();
                        return false;
                    }
                }
            }

            foreach (GridViewRow gvr in gvCustomerPurchase.Rows)
            {
                if (((CheckBox)gvr.FindControl("chkSelect")).Checked)
                {
                    return true;
                }
            }
            Message.IsSuccess = false;
            Message.Text = "Please select engineer to assign";
            Message.Show = true;
            return false;
        }

        protected void gvCustomerPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddlAssignedEngineer = (DropDownList)e.Row.FindControl("ddlAssignedEngineer");
                    LoadEmployee(ddlAssignedEngineer);
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