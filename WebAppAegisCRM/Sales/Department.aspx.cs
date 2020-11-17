using Business.Common;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Sales
{
    public partial class Department : System.Web.UI.Page
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    LoadDepartmentList();
                    Message.Show = false;
                    if (DeptId > 0)
                    {
                        GetDepartmentById();
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
        public int DeptId
        {
            get { return Convert.ToInt32(ViewState["Id"]); }
            set { ViewState["Id"] = value; }
        }
        private void LoadDepartmentList()
        {
            Business.Sales.Department Obj = new Business.Sales.Department();
            gvDepartment.DataSource = Obj.GetAllDepartment();
            gvDepartment.DataBind();
        }
        private void ClearControls()
        {
            DeptId = 0;
            Message.Show = false;
            txtDescription.Text = string.Empty;
            txtName.Text = string.Empty;
            btnSave.Text = "Save";
        }
        private bool DepartmentControlValidation()
        {
            if (txtName.Text.Trim() == string.Empty)
            {
                Message.IsSuccess = false;
                Message.Text = "Please Enter Department Name";
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
        protected void gvDepartment_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                if (e.CommandName == "Ed")
                {
                    DeptId = Convert.ToInt32(e.CommandArgument.ToString());
                    GetDepartmentById();
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
                        LoadDepartmentList();
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
        private void GetDepartmentById()
        {
            Business.Sales.Department Obj = new Business.Sales.Department();
            Entity.Sales.Department Department = Obj.GetDepartmentById(DeptId);
            if (Department.Id != 0)
            {
                txtDescription.Text = Department.Description;
                txtName.Text = Department.Name;
            }
        }
        private void Save()
        {
            if (DepartmentControlValidation())
            {
                Business.Sales.Department Obj = new Business.Sales.Department();
                Entity.Sales.Department Model = new Entity.Sales.Department
                {
                    Id = DeptId,
                    Name = txtName.Text,
                    Description = txtDescription.Text,

                };
                int rows = Obj.SaveDepartment(Model);
                if (rows > 0)
                {
                    ClearControls();
                    LoadDepartmentList();
                    DeptId = 0;
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