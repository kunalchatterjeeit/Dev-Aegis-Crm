using Business.Common;
using log4net;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Inventory
{
    public partial class StoreMaster : System.Web.UI.Page
    {
        ILog logger = log4net.LogManager.GetLogger("ErrorLog");
        public int StoreId
        {
            get { return Convert.ToInt32(ViewState["StoreId"]); }
            set { ViewState["StoreId"] = value; }
        }

        private void EmployeeMaster_GetAll()
        {
            Business.HR.EmployeeMaster ObjBelEmployeeMaster = new Business.HR.EmployeeMaster();
            Entity.HR.EmployeeMaster ObjElEmployeeMaster = new Entity.HR.EmployeeMaster();
            ObjElEmployeeMaster.CompanyId_FK = 1;
            DataTable dt = ObjBelEmployeeMaster.Employee_GetAll_Active(ObjElEmployeeMaster);

            ddlContactPerson.DataSource = dt;
            ddlContactPerson.DataTextField = "EmployeeName";
            ddlContactPerson.DataValueField = "EmployeeMasterId";
            ddlContactPerson.DataBind();
            ddlContactPerson.InsertSelect();
        }

        private void LoadStore()
        {
            Business.Inventory.StoreMaster objStoreMaster = new Business.Inventory.StoreMaster();
            gvStoreList.DataSource = objStoreMaster.GetAll();
            gvStoreList.DataBind();
        }

        protected void ClearControls()
        {
            StoreId = 0;
            txtStoreName.Text = "";
            txtPhone.Text = "";
            txtLocation.Text = "";
            ddlContactPerson.SelectedIndex = 0;
            Message.Show = false;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Message.Show = false;
                    EmployeeMaster_GetAll();
                    LoadStore();
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

        protected void gvStoreList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Business.Inventory.StoreMaster objStoreMaster = new Business.Inventory.StoreMaster();
                Entity.Inventory.StoreMaster storeMaster = new Entity.Inventory.StoreMaster();

                if (e.CommandName == "Ed")
                {
                    int storeId = int.Parse(e.CommandArgument.ToString());
                    storeMaster = objStoreMaster.GetById(storeId);
                    StoreId = storeMaster.StoreId;
                    txtLocation.Text = storeMaster.Location;
                    txtPhone.Text = storeMaster.Phone;
                    txtStoreName.Text = storeMaster.StoreName;
                    ddlContactPerson.SelectedValue = storeMaster.ContactPerson.ToString();
                }
                else if (e.CommandName == "Del")
                {
                    int storeId = int.Parse(e.CommandArgument.ToString());
                    int response = objStoreMaster.Delete(storeId);

                    if (response > 0)
                    {
                        ClearControls();
                        LoadStore();
                        Message.IsSuccess = true;
                        Message.Text = "Store deleted successfully...";
                    }
                    else
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Can not delete!!!";
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

        protected void gvStoreList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvStoreList.PageIndex = e.NewPageIndex;
                LoadStore();
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Business.Inventory.StoreMaster objStoreMaster = new Business.Inventory.StoreMaster();
                Entity.Inventory.StoreMaster storeMaster = new Entity.Inventory.StoreMaster();

                storeMaster.StoreId = StoreId;
                storeMaster.StoreName = txtStoreName.Text;
                storeMaster.Location = txtLocation.Text;
                storeMaster.Phone = txtPhone.Text;
                storeMaster.ContactPerson = Convert.ToInt32(ddlContactPerson.SelectedValue);
                storeMaster.IsActive = true;

                int response = objStoreMaster.Save(storeMaster);

                if (response > 0)
                {
                    ClearControls();
                    LoadStore();
                    Message.IsSuccess = true;
                    Message.Text = "Store saved successfully...";
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
    }
}