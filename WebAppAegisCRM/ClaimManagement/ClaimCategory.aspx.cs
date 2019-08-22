using Business.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.ClaimManagement
{
    public partial class ClaimCategory : System.Web.UI.Page
    {
        private int ClaimCategoryId
        {
            get { return Convert.ToInt32(ViewState["ClaimCategoryId"]); }
            set { ViewState["ClaimCategoryId"] = value; }
        }
        private void ClaimCategoryType_GetAll()
        {
            Business.ClaimManagement.ClaimCategoryType objClaimCategoryType = new Business.ClaimManagement.ClaimCategoryType();
            ddlCategoryType.DataSource = objClaimCategoryType.ClaimCategoryType_GetAll();
            ddlCategoryType.DataTextField = "CategoryTypeName";
            ddlCategoryType.DataValueField = "ClaimCategoryTypeId";
            ddlCategoryType.DataBind();
            ddlCategoryType.InsertSelect();
        }
        private void ClaimCategoryGetAll()
        {
            Business.ClaimManagement.ClaimCategory objClaimCategory = new Business.ClaimManagement.ClaimCategory();
            gvClaimCategory.DataSource = objClaimCategory.ClaimCategoryGetAll(new Entity.ClaimManagement.ClaimCategory() { });
            gvClaimCategory.DataBind();
        }
        private void ClearControls()
        {
            ClaimCategoryId = 0;
            Message.Show = false;
            txtDescription.Text = string.Empty;
            txtName.Text = string.Empty;
            ddlCategoryType.SelectedIndex = 0;
            btnSave.Text = "Save";
        }
        private bool ClaimControlValidation()
        {
            if (ddlCategoryType.SelectedIndex == 0)
            {
                Message.IsSuccess = false;
                Message.Text = "Please select claim category type";
                Message.Show = true;
                return false;
            }
            if (txtName.Text.Trim() == string.Empty)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Enter Claim Name";
                Message.Show = true;
                return false;
            }
            return true;
        }

        private void ClaimCategory_GetById()
        {
            Business.ClaimManagement.ClaimCategory objClaimCategory = new Business.ClaimManagement.ClaimCategory();
            DataTable dtClaimCategory = objClaimCategory.ClaimCategory_GetById(ClaimCategoryId);
            if (dtClaimCategory != null)
            {
                txtDescription.Text = dtClaimCategory.Rows[0]["Description"].ToString();
                txtName.Text = dtClaimCategory.Rows[0]["CategoryName"].ToString();
                ddlCategoryType.SelectedValue = dtClaimCategory.Rows[0]["ClaimCategoryTypeId"].ToString();
            }
        }
        private void Save()
        {
            if (ClaimControlValidation())
            {
                Business.ClaimManagement.ClaimCategory objClaimCategory = new Business.ClaimManagement.ClaimCategory();
                Entity.ClaimManagement.ClaimCategory Model = new Entity.ClaimManagement.ClaimCategory
                {
                    ClaimCategoryTypeId = int.Parse(ddlCategoryType.SelectedValue),
                    ClaimCategoryId = ClaimCategoryId,
                    ClaimCategoryName = txtName.Text,
                    ClaimCategoryDescription = txtDescription.Text,

                };
                int rows = objClaimCategory.ClaimCategorySave(Model);
                if (rows > 0)
                {
                    ClearControls();
                    ClaimCategoryGetAll();
                    ClaimCategoryId = 0;
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClaimCategoryType_GetAll();
                ClaimCategoryGetAll();
                Message.Show = false;
                if (ClaimCategoryId > 0)
                {
                    ClaimCategory_GetById();
                }
            }
        }

        protected void gvClaimCategory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Ed")
            {
                ClaimCategoryId = Convert.ToInt32(e.CommandArgument.ToString());
                ClaimCategory_GetById();
                Message.Show = false;
                btnSave.Text = "Update";
            }
            else if (e.CommandName == "Del")
            {
                Business.Sales.Department Obj = new Business.Sales.Department();
                int rows = Obj.DeleteDepartment(Convert.ToInt32(e.CommandArgument.ToString()));
                if (rows > 0)
                {
                    ClearControls();
                    ClaimCategoryGetAll();
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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }
    }
}