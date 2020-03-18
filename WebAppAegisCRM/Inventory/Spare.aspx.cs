using Business.Common;
using log4net;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Inventory
{
    public partial class Spare : System.Web.UI.Page
    {
        ILog logger = log4net.LogManager.GetLogger("ErrorLog");
        public int SpareId
        {
            get { return Convert.ToInt32(ViewState["SpareId"]); }
            set { ViewState["SpareId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ClearControls();
                    LoadSpare();
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

        private void ClearControls()
        {
            SpareId = 0;
            txtSpareName.Text = "";
            txtYield.Text = "";
            txtDescription.Text = "";
            Message.Show = false;
            chkIsTonner.Checked = false;
        }

        private void LoadSpare()
        {
            Business.Inventory.SpareMaster objSpareMaster = new Business.Inventory.SpareMaster();
            Entity.Inventory.SpareMaster spareMaster = new Entity.Inventory.SpareMaster();
            DataTable dt = objSpareMaster.GetAll(spareMaster);
            if (dt != null)
            {
                gvSpare.DataSource = dt;
                gvSpare.DataBind();
            }
        }

        private void PopulateSpare()
        {
            Business.Inventory.SpareMaster objSpareMaster = new Business.Inventory.SpareMaster();
            Entity.Inventory.SpareMaster spareMaster = new Entity.Inventory.SpareMaster();

            spareMaster = objSpareMaster.GetById(SpareId);
            txtSpareName.Text = spareMaster.SpareName;
            txtYield.Text = Convert.ToString(spareMaster.Yield);
            txtDescription.Text = spareMaster.Description;
            chkIsTonner.Checked = spareMaster.isTonner;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Business.Inventory.SpareMaster objSpareMaster = new Business.Inventory.SpareMaster();
                Entity.Inventory.SpareMaster spareMaster = new Entity.Inventory.SpareMaster();

                spareMaster.SpareId = SpareId;
                spareMaster.SpareName = txtSpareName.Text;
                spareMaster.Yield = int.Parse(txtYield.Text);
                spareMaster.Description = txtDescription.Text;
                spareMaster.isTonner = chkIsTonner.Checked;

                int i = objSpareMaster.Save(spareMaster);
                if (i > 0)
                {
                    ClearControls();
                    LoadSpare();

                    Message.IsSuccess = true;
                    Message.Text = "Spare saved successfully...";
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Spare can not save!!!";
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
        protected void gvSpare_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Ed")
                {
                    SpareId = int.Parse(e.CommandArgument.ToString());
                    PopulateSpare();
                }
                else if (e.CommandName == "Del")
                {
                    Business.Inventory.SpareMaster objSpareMaster = new Business.Inventory.SpareMaster();
                    int i = objSpareMaster.Delete(int.Parse(e.CommandArgument.ToString()));

                    if (i > 0)
                    {
                        ClearControls();
                        LoadSpare();

                        Message.IsSuccess = true;
                        Message.Text = "Spare deleted successfully...";
                    }
                    else
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Spare can not delete!!!";
                    }
                    Message.Show = true;
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