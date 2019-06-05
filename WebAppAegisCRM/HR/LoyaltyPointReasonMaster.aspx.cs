using Business.Common;
using System;
using System.Data;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.HR
{
    public partial class LoyaltyPointReasonMaster : System.Web.UI.Page
    {
        private int LoyaltyPointReasonId
        {
            get { return Convert.ToInt32(ViewState["LoyaltyPointReasonId"]); }
            set { ViewState["LoyaltyPointReasonId"] = value; }
        }

        protected void ClearControls()
        {
            LoyaltyPointReasonId = 0;
            ddlDesignation.SelectedIndex = 0;
            txtDescription.Text = "";
            txtReason.Text = "";
            Message.Show = false;
        }

        protected void DesignationMaster_GetAll()
        {
            Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
            Entity.HR.EmployeeMaster employeeMaster = new Entity.HR.EmployeeMaster();
            employeeMaster.CompanyId_FK = 1;
            DataTable dt = objEmployeeMaster.DesignationMaster_GetAll(employeeMaster);
            if (dt.Rows.Count > 0)
            {
                ddlDesignation.DataSource = dt;
                ddlDesignation.DataTextField = "DesignationName";
                ddlDesignation.DataValueField = "DesignationMasterId";
                ddlDesignation.DataBind();
            }
            ddlDesignation.InsertSelect();
        }

        protected void LoadLoyaltyPointReasonMaster()
        {
            DataTable dt = Business.HR.LoyaltyPointReasonMaster.GetAll(new Entity.HR.LoyaltyPointReasonMaster());

            gvLoyaltyReason.DataSource = dt;
            gvLoyaltyReason.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DesignationMaster_GetAll();
                LoadLoyaltyPointReasonMaster();
                ClearControls();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Business.HR.LoyaltyPointReasonMaster objLoyaltyPointReasonMaster = new Business.HR.LoyaltyPointReasonMaster();
            Entity.HR.LoyaltyPointReasonMaster loyaltyPointReasonMaster = new Entity.HR.LoyaltyPointReasonMaster();

            loyaltyPointReasonMaster.LoyaltyPointReasonId = LoyaltyPointReasonId;
            loyaltyPointReasonMaster.Reason = txtReason.Text.Trim();
            loyaltyPointReasonMaster.Description = txtDescription.Text.Trim();
            loyaltyPointReasonMaster.DesignationId = int.Parse(ddlDesignation.SelectedValue);

            int response = objLoyaltyPointReasonMaster.Save(loyaltyPointReasonMaster);

            if (response > 0)
            {
                ClearControls();
                LoadLoyaltyPointReasonMaster();
                Message.IsSuccess = true;
                Message.Text = "Reason saved successfully...";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Sorry!! data not saved.";
            }
            Message.Show = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void gvLoyaltyReason_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLoyaltyReason.PageIndex = e.NewPageIndex;
            LoadLoyaltyPointReasonMaster();
        }

        protected void gvLoyaltyReason_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Business.HR.LoyaltyPointReasonMaster objLoyaltyPointReasonMaster = new Business.HR.LoyaltyPointReasonMaster();
            Entity.HR.LoyaltyPointReasonMaster loyaltyPointReasonMaster = new Entity.HR.LoyaltyPointReasonMaster();

            if (e.CommandName == "Ed")
            {
                int reasonId = int.Parse(e.CommandArgument.ToString());
                loyaltyPointReasonMaster = objLoyaltyPointReasonMaster.GetById(reasonId);
                LoyaltyPointReasonId = loyaltyPointReasonMaster.LoyaltyPointReasonId;
                txtDescription.Text = loyaltyPointReasonMaster.Description;
                txtReason.Text = loyaltyPointReasonMaster.Reason;
                ddlDesignation.SelectedValue = Convert.ToString(loyaltyPointReasonMaster.DesignationId);
            }
            else if (e.CommandName == "Del")
            {
                int cityId = int.Parse(e.CommandArgument.ToString());
                int i = objLoyaltyPointReasonMaster.Delete(cityId);

                if (i > 0)
                {
                    ClearControls();
                    LoadLoyaltyPointReasonMaster();
                    Message.IsSuccess = true;
                    Message.Text = "Reason deleted successfully...";
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Sorry!! data not delete.";
                }
                Message.Show = true;
            }
        }
    }
}