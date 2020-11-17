using Business.Common;

using System;
using System.Data;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.HR
{
    public partial class RoleMaster : System.Web.UI.Page
    {
        
        public int RoleId
        {
            get { return Convert.ToInt32(ViewState["RoleId"]); }
            set { ViewState["RoleId"] = value; }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    ClearControls();
                    LoadRoleList();
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
        private void ClearControls()
        {
            RoleId = 0;
            Message.Show = false;
            btnSave.Text = "Save";
            txtRole.Text = "";
        }
        private void LoadRoleList()
        {
            Business.HR.RoleMaster objRoleMaster = new Business.HR.RoleMaster();
            DataTable DT = objRoleMaster.GetAll();
            if (DT != null)
            {
                dgvRoleMaster.DataSource = DT;
                dgvRoleMaster.DataBind();
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
                
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Business.HR.RoleMaster objRoleMaster = new Business.HR.RoleMaster();
                Entity.HR.RoleMaster Role = new Entity.HR.RoleMaster();
                Role.RoleId = RoleId;
                Role.RoleName = txtRole.Text;
                int RowsAffected = objRoleMaster.Save(Role);

                if (RowsAffected > 0)
                {
                    ClearControls();
                    LoadRoleList();
                    Message.IsSuccess = true;
                    Message.Text = "Saved Successfully";
                }
                else
                {
                    Message.IsSuccess = false;
                    Message.Text = "Role Name Exists";
                }
                Message.Show = true;
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                LoadRoleList();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
                Message.IsSuccess = false;
                Message.Text = ex.Message;
                Message.Show = true;
            }
        }
        protected void dgvRoleMaster_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Ed")
                {
                    RoleId = Convert.ToInt32(e.CommandArgument.ToString());
                    GridViewRow row = (GridViewRow)(((ImageButton)e.CommandSource).NamingContainer);

                    txtRole.Text = row.Cells[1].Text;
                    Message.Show = false;
                    btnSave.Text = "Update";
                }
                else if (e.CommandName == "Del")
                {
                    Business.HR.RoleMaster objRoleMaster = new Business.HR.RoleMaster();
                    int RowsAffected = objRoleMaster.Delete(Convert.ToInt32(e.CommandArgument.ToString()));

                    if (RowsAffected > 0)
                    {
                        ClearControls();
                        LoadRoleList();
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
    }
}