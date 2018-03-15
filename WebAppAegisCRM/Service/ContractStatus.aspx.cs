using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Service
{
    public partial class ContractStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString().Length > 0)
            {
                ddlContractStatus.SelectedValue = Request.QueryString["id"].ToString();
                LoadContractStatusList();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlContractStatus.SelectedValue == "0")
                lblListTitle.Text = "N/A";
            else
                lblListTitle.Text = ddlContractStatus.SelectedItem.Text;

            LoadContractStatusList();
        }

        protected void LoadContractStatusList()
        {
            Entity.Service.Contract contract = new Entity.Service.Contract();
            Business.Service.Contract objContract = new Business.Service.Contract();
            contract.MachineId = txtMachineId.Text.Trim();
            contract.FromDate = (txtFromContractDate.Text == "") ? DateTime.MinValue : Convert.ToDateTime(txtFromContractDate.Text.Trim());
            contract.ToDate = (txtToContractDate.Text == "") ? DateTime.MinValue : Convert.ToDateTime(txtToContractDate.Text.Trim());
            if (HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
                contract.AssignEngineer = 0;
            else
                contract.AssignEngineer = int.Parse(HttpContext.Current.User.Identity.Name);
            DataSet ds = objContract.Service_ContractStatusList(contract);

            if (ddlContractStatus.SelectedValue == "0")
            {
                gvContractStatusList.DataSource = null;
            }
            else if (ddlContractStatus.SelectedValue == "2")
            {
                gvContractStatusList.DataSource = ds.Tables[0];
            }
            else if (ddlContractStatus.SelectedValue == "3")
            {
                gvContractStatusList.DataSource = ds.Tables[1];
            }
            else if (ddlContractStatus.SelectedValue == "1")
            {
                gvContractStatusList.DataSource = ds.Tables[2];
            }
            else if (ddlContractStatus.SelectedValue == "4")
            {
                gvContractStatusList.DataSource = ds.Tables[3];
            }
            else
            {
                gvContractStatusList.DataSource = null;
            }
            gvContractStatusList.DataBind();
        }

        protected void btnMachineSearch_Click(object sender, EventArgs e)
        {
            LoadContractStatusList();
        }

        protected void gvContractStatusList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvContractStatusList.PageIndex = e.NewPageIndex;
            LoadContractStatusList();
        }
    }
}