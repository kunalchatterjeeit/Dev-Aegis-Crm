using Entity.Common;
using System;
using System.Data;
using System.Web;
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
                LoadContractStatusList(0, gvContractStatusList.PageSize);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlContractStatus.SelectedValue == "0")
                lblListTitle.Text = "N/A";
            else
                lblListTitle.Text = ddlContractStatus.SelectedItem.Text;

            LoadContractStatusList(0, gvContractStatusList.PageSize);
        }

        protected void LoadContractStatusList(int pageIndex, int pageSize)
        {
            Entity.Service.Contract contract = new Entity.Service.Contract();
            Business.Service.Contract objContract = new Business.Service.Contract();
            contract.PageIndex = pageIndex;
            contract.PageSize = pageSize;
            contract.MachineId = txtMachineId.Text.Trim();
            contract.FromDate = (txtFromContractDate.Text == "") ? DateTime.MinValue : Convert.ToDateTime(txtFromContractDate.Text.Trim());
            contract.ToDate = (txtToContractDate.Text == "") ? DateTime.MinValue : Convert.ToDateTime(txtToContractDate.Text.Trim());
            contract.ProductSerialNo = txtProductSerialNo.Text.Trim();
            if (HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
                contract.AssignEngineer = 0;
            else
                contract.AssignEngineer = int.Parse(HttpContext.Current.User.Identity.Name);
            DataSet ds = objContract.Service_ContractStatusList(contract);

            if (ddlContractStatus.SelectedValue == ((int)ContractStatusType.None).ToString())
            {
                gvContractStatusList.DataSource = null;
            }
            else if (ddlContractStatus.SelectedValue == ((int)ContractStatusType.Expiring).ToString())
            {
                gvContractStatusList.DataSource = ds.Tables[0];
                gvContractStatusList.VirtualItemCount = (ds.Tables[4].Rows.Count > 0) ? Convert.ToInt32(ds.Tables[4].Rows[0]["TotalCount"].ToString()) : 15;
            }
            else if (ddlContractStatus.SelectedValue == ((int)ContractStatusType.Expired).ToString())
            {
                gvContractStatusList.DataSource = ds.Tables[1];
                gvContractStatusList.VirtualItemCount = (ds.Tables[5].Rows.Count > 0) ? Convert.ToInt32(ds.Tables[5].Rows[0]["TotalCount"].ToString()) : 15;
            }
            else if (ddlContractStatus.SelectedValue == ((int)ContractStatusType.InContract).ToString())
            {
                gvContractStatusList.DataSource = ds.Tables[2];
                gvContractStatusList.VirtualItemCount = (ds.Tables[6].Rows.Count > 0) ? Convert.ToInt32(ds.Tables[6].Rows[0]["TotalCount"].ToString()) : 15;
            }
            else if (ddlContractStatus.SelectedValue == ((int)ContractStatusType.NeverContracted).ToString())
            {
                gvContractStatusList.DataSource = ds.Tables[3];
                gvContractStatusList.VirtualItemCount = (ds.Tables[7].Rows.Count > 0) ? Convert.ToInt32(ds.Tables[7].Rows[0]["TotalCount"].ToString()) : 15;
            }
            else
            {
                gvContractStatusList.DataSource = null;
            }
            gvContractStatusList.DataBind();
        }

        protected void btnMachineSearch_Click(object sender, EventArgs e)
        {
            LoadContractStatusList(0, gvContractStatusList.PageSize);
        }

        protected void gvContractStatusList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvContractStatusList.PageIndex = e.NewPageIndex;
            LoadContractStatusList(gvContractStatusList.PageIndex, gvContractStatusList.PageSize);
        }
    }
}