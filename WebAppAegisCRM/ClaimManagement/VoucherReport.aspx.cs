using Business.Common;

using System;
using System.Data;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.ClaimManagement
{
    public partial class VoucherReport : System.Web.UI.Page
    {
        
        private void Voucher_GetAll()
        {
            Business.ClaimManagement.Voucher objVoucher = new Business.ClaimManagement.Voucher();
            DataSet dsVoucher = objVoucher.Voucher_GetAll(new Entity.ClaimManagement.Voucher()
            {
                VoucherNo = txtVoucherNo.Text.Trim(),
                FromDate = (string.IsNullOrEmpty(txtFromDate.Text.Trim()) ? DateTime.MinValue : Convert.ToDateTime(txtFromDate.Text.Trim())),
                ToDate = (string.IsNullOrEmpty(txtToDate.Text.Trim()) ? DateTime.MinValue : Convert.ToDateTime(txtToDate.Text.Trim())),
                PageIndex = gvVoucherList.PageIndex,
                PageSize = gvVoucherList.PageSize
            });
            gvVoucherList.DataSource = dsVoucher.Tables[0];
            gvVoucherList.VirtualItemCount = (dsVoucher.Tables[1].Rows.Count > 0) ? Convert.ToInt32(dsVoucher.Tables[1].Rows[0]["TotalCount"].ToString()) : 20;
            gvVoucherList.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    Voucher_GetAll();
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
            }
        }

        protected void gvVoucherList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvVoucherList.PageIndex = e.NewPageIndex;
                Voucher_GetAll();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Voucher_GetAll();
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
            }
        }
    }
}