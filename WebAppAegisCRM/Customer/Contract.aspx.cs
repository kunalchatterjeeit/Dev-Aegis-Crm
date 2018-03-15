using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Customer
{
    public partial class Contract : System.Web.UI.Page
    {
        public int ContractId
        {
            get { return Convert.ToInt32(ViewState["ContractId"]); }
            set { ViewState["ContractId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearControls();
                LoadContract();
            }
        }

        protected void ClearControls()
        {
            ContractId = 0;
            txtContractName.Text = "";
            txtDescription.Text = "";
            Message.Show = false;
        }

        protected void LoadContract()
        {
            Business.Customer.Contract objContract = new Business.Customer.Contract();
            gvContract.DataSource = objContract.GetAll();
            gvContract.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Business.Customer.Contract objContract = new Business.Customer.Contract();
            Entity.Customer.Contract contract = new Entity.Customer.Contract();

            contract.ContractId = ContractId;
            contract.ContractName = txtContractName.Text;
            contract.Description = txtDescription.Text;

            int i = objContract.Save(contract);

            if (i > 0)
            {
                ClearControls();
                LoadContract();
                Message.IsSuccess = true;
                Message.Text = "Contract Information saved successfully...";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can not save!!!";
            }
            Message.Show = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void gvContract_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvContract.PageIndex = e.NewPageIndex;
            LoadContract();
        }

        protected void gvContract_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Business.Customer.Contract objContract = new Business.Customer.Contract();
            Entity.Customer.Contract contract = new Entity.Customer.Contract();

            if (e.CommandName == "Ed")
            {
                int brandId = int.Parse(e.CommandArgument.ToString());
                contract = objContract.GetById(brandId);
                ContractId = contract.ContractId;
                txtContractName.Text = contract.ContractName;
                txtDescription.Text = contract.Description;
            }
        }
    }
}