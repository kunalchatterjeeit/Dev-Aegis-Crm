using System;
using System.Web.UI.WebControls;
using System.Data;
using Business.Common;

namespace WebAppAegisCRM.HR
{
    public partial class RoleAccessLevel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    LoadRoles();
                    LoadRoleAccessLevel();
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
            }
        }
        private void LoadRoles()
        {
            Business.HR.RoleMaster ObjRole = new Business.HR.RoleMaster();
            DataTable DT = ObjRole.GetAll();
            if (DT != null)
            {
                ddlRole.DataSource = DT;
                ddlRole.DataBind();
            }
        }
        private void LoadRoleAccessLevel()
        {
            UncheckAll();
            int RoleId = Convert.ToInt32(ddlRole.SelectedValue);
            Business.HR.RoleAccessLevel ObjRoleAccessLevel = new Business.HR.RoleAccessLevel();
            DataTable DT = ObjRoleAccessLevel.GetByRoleId(RoleId);

            foreach (DataRow dr in DT.Rows)
            {
                if (ChkLstSettings.Items.FindByValue(dr["PermissionId"].ToString()) != null)
                    ChkLstSettings.Items.FindByValue(dr["PermissionId"].ToString()).Selected = true;
                else if (ChkListHR.Items.FindByValue(dr["PermissionId"].ToString()) != null)
                    ChkListHR.Items.FindByValue(dr["PermissionId"].ToString()).Selected = true;
                else if (ChkListInventory.Items.FindByValue(dr["PermissionId"].ToString()) != null)
                    ChkListInventory.Items.FindByValue(dr["PermissionId"].ToString()).Selected = true;
                else if (ChkListService.Items.FindByValue(dr["PermissionId"].ToString()) != null)
                    ChkListService.Items.FindByValue(dr["PermissionId"].ToString()).Selected = true;
                else if (ChkListReport.Items.FindByValue(dr["PermissionId"].ToString()) != null)
                    ChkListReport.Items.FindByValue(dr["PermissionId"].ToString()).Selected = true;
                else if (ChkControlPanel.Items.FindByValue(dr["PermissionId"].ToString()) != null)
                    ChkControlPanel.Items.FindByValue(dr["PermissionId"].ToString()).Selected = true;
                else if (chkListLeaveManagement.Items.FindByValue(dr["PermissionId"].ToString()) != null)
                    chkListLeaveManagement.Items.FindByValue(dr["PermissionId"].ToString()).Selected = true;
                else if (chkListSales.Items.FindByValue(dr["PermissionId"].ToString()) != null)
                    chkListSales.Items.FindByValue(dr["PermissionId"].ToString()).Selected = true;
                else if (chkListClaim.Items.FindByValue(dr["PermissionId"].ToString()) != null)
                    chkListClaim.Items.FindByValue(dr["PermissionId"].ToString()).Selected = true;
            }
        }
        private void UncheckAll()
        {
            foreach (ListItem lstItem in ChkLstSettings.Items)
                lstItem.Selected = false;

            foreach (ListItem lstItem in ChkListHR.Items)
                lstItem.Selected = false;

            foreach (ListItem lstItem in ChkListInventory.Items)
                lstItem.Selected = false;

            foreach (ListItem lstItem in ChkListService.Items)
                lstItem.Selected = false;

            foreach (ListItem lstItem in chkListLeaveManagement.Items)
                lstItem.Selected = false;

            foreach (ListItem lstItem in chkListSales.Items)
                lstItem.Selected = false;

            foreach (ListItem lstItem in chkListClaim.Items)
                lstItem.Selected = false;
        }
        protected void ddlRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            { 
            LoadRoleAccessLevel();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
            }
        }
        protected void CheckListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            { 
            int index = Convert.ToInt32(Request.Form["__EVENTTARGET"].Substring(Request.Form["__EVENTTARGET"].LastIndexOf('$') + 1));
            CheckBoxList cbl = (CheckBoxList)sender;
            int PermissionId = int.Parse(cbl.Items[index].Value);
            SaveRoleAccessLevel(PermissionId, cbl.Items[index].Selected);
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
            }
        }
        private void SaveRoleAccessLevel(int PermissionId, bool IsChecked)
        {
            Business.HR.RoleAccessLevel ObjRoleAccessLevel = new Business.HR.RoleAccessLevel();
            int RoleId = Convert.ToInt32(ddlRole.SelectedValue);
            ObjRoleAccessLevel.Save(RoleId, PermissionId, IsChecked);
        }
    }
}