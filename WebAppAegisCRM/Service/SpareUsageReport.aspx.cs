using Business.Common;
using Entity.Inventory;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Service
{
    public partial class SpareUsageReport : System.Web.UI.Page
    {
        protected void EmployeeMaster_GetAll()
        {
            Business.HR.EmployeeMaster objEmployeeMaster = new Business.HR.EmployeeMaster();
            Entity.HR.EmployeeMaster employeeMaster = new Entity.HR.EmployeeMaster();
            employeeMaster.CompanyId_FK = 1;
            DataTable dt = objEmployeeMaster.Employee_GetAll_Active(employeeMaster);
            if (dt != null)
            {
                ddlEmployee.DataSource = dt;
                ddlEmployee.DataValueField = "EmployeeMasterId";
                ddlEmployee.DataTextField = "EmployeeName";
                ddlEmployee.DataBind();
            }
            ddlEmployee.InsertSelect();
        }

        private void LoadAllItem()
        {
            using (DataTable dtItem = new DataTable())
            {
                dtItem.Columns.Add("ItemIdType");
                dtItem.Columns.Add("ItemName");

                Business.Inventory.SpareMaster objSpareMaster = new Business.Inventory.SpareMaster();

                foreach (DataRow drItem in objSpareMaster.GetAll(new Entity.Inventory.SpareMaster() { }).Rows)
                {
                    DataRow drNewItem = dtItem.NewRow();
                    drNewItem["ItemIdType"] = drItem["SpareId"].ToString();
                    drNewItem["ItemName"] = drItem["SpareName"].ToString() + ((drItem["IsTonner"].ToString() == "0") ? " (S)" : " (T)");
                    dtItem.Rows.Add(drNewItem);
                    dtItem.AcceptChanges();
                }

                ddlItem.DataSource = dtItem;
                ddlItem.DataValueField = "ItemIdType";
                ddlItem.DataTextField = "ItemName";
                ddlItem.DataBind();
                ddlItem.InsertSelect();
            }
        }

        private void Service_SpareUsage(int pageIndex, int pageSize)
        {
            Business.Service.ServiceBook objServiceBook = new Business.Service.ServiceBook();
            Entity.Service.ServiceBook serviceBook = new Entity.Service.ServiceBook()
            {
                CustomerName = txtCustomerName.Text.Trim(),
                RequestNo = txtCallNo.Text.Trim(),
                ItemId = int.Parse(ddlItem.SelectedValue),
                EmployeeId_FK = int.Parse(ddlEmployee.SelectedValue),
                FromDate = (string.IsNullOrEmpty(txtFromLogRequestDate.Text.Trim())) ? DateTime.MinValue : Convert.ToDateTime(txtFromLogRequestDate.Text.Trim()),
                ToDate = (string.IsNullOrEmpty(txtToLogRequestDate.Text.Trim())) ? DateTime.MinValue : Convert.ToDateTime(txtToLogRequestDate.Text.Trim()),
                PageIndex = pageIndex,
                PageSize = pageSize
            };

            DataSet ds = objServiceBook.Service_SpareUsage(serviceBook);

            gvSpareUsage.DataSource = ds.Tables[0];
            gvSpareUsage.VirtualItemCount = (ds.Tables[1].Rows.Count > 0) ? Convert.ToInt32(ds.Tables[1].Rows[0]["TotalCount"].ToString()) : 10;
            gvSpareUsage.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EmployeeMaster_GetAll();
                LoadAllItem();
                Service_SpareUsage(gvSpareUsage.PageIndex, gvSpareUsage.PageSize);
            }

        }

        protected void gvSpareUsage_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvSpareUsage.PageIndex = e.NewPageIndex;
            Service_SpareUsage(gvSpareUsage.PageIndex, gvSpareUsage.PageSize);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Service_SpareUsage(0, gvSpareUsage.PageSize);
        }
    }
}