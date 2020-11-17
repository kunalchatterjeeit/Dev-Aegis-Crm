using Business.Common;

using System;
using System.Web;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Sales
{
    public partial class Campaign : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    LoadCampaignList();
                    Message.Show = false;
                    if (CampaignId > 0)
                    {
                        GetCampaignById();
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
        public int CampaignId
        {
            get { return Convert.ToInt32(ViewState["Id"]); }
            set { ViewState["Id"] = value; }
        }
        private void LoadCampaignList()
        {
            Business.Sales.Campaign Obj = new Business.Sales.Campaign();
            Entity.Sales.GetCampaignParam Param = new Entity.Sales.GetCampaignParam
            {
                EndDate = DateTime.MinValue,
                StartDate = DateTime.MinValue,
                Name = null
            };

            if (HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
                Param.AssignEngineer = 0;
            else
                Param.AssignEngineer = int.Parse(HttpContext.Current.User.Identity.Name);

            gvCampaign.DataSource = Obj.GetAllCampaign(Param);
            gvCampaign.DataBind();
        }
        private void ClearControls()
        {
            CampaignId = 0;
            Message.Show = false;
            txtStartDate.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtEndDate.Text = string.Empty;
            txtName.Text = string.Empty;
            txtReason.Text = string.Empty;
            btnSave.Text = "Save";
        }
        private bool CampaignControlValidation()
        {
            if (txtName.Text.Trim() == string.Empty)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Enter Campaign Name";
                Message.Show = true;
                return false;
            }
            return true;
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
        protected void gvCampaign_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Ed")
                {
                    CampaignId = Convert.ToInt32(e.CommandArgument.ToString());
                    GetCampaignById();
                    Message.Show = false;
                    btnSave.Text = "Update";
                }
                else if (e.CommandName == "Del")
                {
                    Business.Sales.Campaign Obj = new Business.Sales.Campaign();
                    int rows = Obj.DeleteCampaign(Convert.ToInt32(e.CommandArgument.ToString()));
                    if (rows > 0)
                    {
                        ClearControls();
                        LoadCampaignList();
                        Message.IsSuccess = true;
                        Message.Text = "Deleted Successfully";
                    }
                    else
                    {
                        Message.IsSuccess = false;
                        Message.Text = "Data Dependency Exists";
                    }
                    Message.Show = true;
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
        private void GetCampaignById()
        {
            Business.Sales.Campaign Obj = new Business.Sales.Campaign();
            Entity.Sales.Campaign Campaign = Obj.GetCampaignById(CampaignId);
            if (Campaign.Id != 0)
            {
                txtName.Text = Campaign.Name;
                txtReason.Text = Campaign.Reason;
                txtDescription.Text = Campaign.Description;
                txtStartDate.Text = Campaign.EndDate == null ? string.Empty : Campaign.StartDate.GetValueOrDefault().ToString("dd MMM yyyy");
                txtEndDate.Text = Campaign.EndDate == null ? string.Empty : Campaign.EndDate.GetValueOrDefault().ToString("dd MMM yyyy");
            }
        }
        private void Save()
        {
            if (CampaignControlValidation())
            {
                Business.Sales.Campaign Obj = new Business.Sales.Campaign();
                Entity.Sales.Campaign Model = new Entity.Sales.Campaign
                {
                    Id = CampaignId,
                    CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name),
                    Description = txtDescription.Text,
                    Name = txtName.Text,
                    Reason = txtReason.Text,
                    StartDate = txtStartDate.Text == "" ? (DateTime?)null : Convert.ToDateTime(txtStartDate.Text),
                    EndDate = txtEndDate.Text == "" ? (DateTime?)null : Convert.ToDateTime(txtEndDate.Text),
                    IsActive = true
                };
                int rows = Obj.SaveCampaign(Model);
                if (rows > 0)
                {
                    ClearControls();
                    LoadCampaignList();
                    CampaignId = 0;
                    Message.IsSuccess = true;
                    Message.Text = "Saved Successfully";
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Unable to save data.";
                }
                Message.Show = true;
            }
        }
    }
}