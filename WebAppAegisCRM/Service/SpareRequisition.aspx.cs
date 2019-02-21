using System;
using System.Data;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Service
{
    public partial class SpareRequisition : System.Web.UI.Page
    {
        private void Inventory_ProductSpareMapping_GetById()
        {
            Business.Inventory.ProductMaster objServiceBook = new Business.Inventory.ProductMaster();
            DataTable dt = objServiceBook.ProductSpareMapping_GetById(Business.Common.Context.ProductId, Entity.Inventory.ItemType.Spare);
            gvSpareList.DataSource = dt;
            gvSpareList.DataBind();

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Inventory_ProductSpareMapping_GetById();
            }
        }

        protected void btnProceed_Click(object sender, EventArgs e)
        {
            using (DataTable dtRequisition = new DataTable())
            {
                dtRequisition.Columns.Add("SpareId");
                dtRequisition.Columns.Add("RequisiteQty");
                foreach (GridViewRow gvr in gvSpareList.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkSpare")).Checked)
                    {
                        DataRow drSpareId = dtRequisition.NewRow();
                        drSpareId["SpareId"] = gvSpareList.DataKeys[gvr.RowIndex].Values[0].ToString();
                        drSpareId["RequisiteQty"] = ((TextBox)gvr.FindControl("txtRequisiteQty")).Text.Trim();
                        dtRequisition.Rows.Add(drSpareId);
                        dtRequisition.AcceptChanges();
                    }
                }
                Business.Common.Context.SpareRequisition = dtRequisition;
            }
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "script", "window.close();", true);
        }

        protected void chkSpare_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            GridViewRow gridViewRow = (GridViewRow)checkBox.NamingContainer;
            int spareId = int.Parse(gvSpareList.DataKeys[gridViewRow.RowIndex].Values[0].ToString());

            Label lblA4BWLastReading = (Label)gridViewRow.FindControl("lblA4BWLastReading");

            DataTable dt = new Business.Service.ServiceBook().Service_GetLastMeterReadingOfSpare(Business.Common.Context.CallId, Business.Common.Context.CallType, spareId);
            if (dt != null && dt.Rows.Count > 0)
            {
                lblA4BWLastReading.Text = dt.Rows[0]["A4BWLast"].ToString();
            }
        }
    }
}