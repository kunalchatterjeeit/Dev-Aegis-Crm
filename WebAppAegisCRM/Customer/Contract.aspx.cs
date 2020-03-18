using Business.Common;
using log4net;
using System;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Customer
{
    public partial class Contract : System.Web.UI.Page
    {
        ILog logger = log4net.LogManager.GetLogger("ErrorLog");
        public int ContractId
        {
            get { return Convert.ToInt32(ViewState["ContractId"]); }
            set { ViewState["ContractId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ClearControls();
                    LoadContract();
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }

        protected void ClearControls()
        {
            ContractId = 0;
            txtContractName.Text = "";
            txtDescription.Text = "";
            Message.Show = false;
        }

        private void LoadContract()
        {
            Business.Customer.Contract objContract = new Business.Customer.Contract();
            gvContract.DataSource = objContract.GetAll();
            gvContract.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                ClearControls();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }

        protected void gvContract_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvContract.PageIndex = e.NewPageIndex;
                LoadContract();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }

        protected void gvContract_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                ex.WriteException();
                logger.Error(ex.Message);
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
    }
}