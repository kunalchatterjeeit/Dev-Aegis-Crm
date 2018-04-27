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
                foreach (GridViewRow gvr in gvSpareList.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkSpare")).Checked)
                    {
                        DataRow drSpareId = dtRequisition.NewRow();
                        drSpareId["SpareId"] = gvSpareList.DataKeys[gvr.RowIndex].Values[0].ToString();
                        dtRequisition.Rows.Add(drSpareId);
                        dtRequisition.AcceptChanges();
                    }
                }
                Business.Common.Context.SpareRequisition = dtRequisition;
            }
            ClientScript.RegisterClientScriptBlock(Page.GetType(), "script", "window.close();", true);
        }
    }
}