using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.ClaimManagement
{
    public partial class VoucherReport : System.Web.UI.Page
    {
        private void Voucher_GetAll()
        {
            Business.ClaimManagement.Voucher objVoucher = new Business.ClaimManagement.Voucher();
            gvVoucherList.DataSource = objVoucher.Voucher_GetAll(new Entity.ClaimManagement.Voucher()
            {
                VoucherNo = txtVoucherNo.Text.Trim(),
                FromDate = (string.IsNullOrEmpty(txtFromDate.Text.Trim()) ? DateTime.MinValue : Convert.ToDateTime(txtFromDate.Text.Trim())),
                ToDate = (string.IsNullOrEmpty(txtToDate.Text.Trim()) ? DateTime.MinValue : Convert.ToDateTime(txtToDate.Text.Trim())),
                //PageIndex = gvVoucherList.PageIndex,
                //PageSize = gvVoucherList.PageSize
            });
            gvVoucherList.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Voucher_GetAll();
            }
        }

        protected void gvVoucherList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvVoucherList.PageIndex = e.NewPageIndex;
            Voucher_GetAll();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Voucher_GetAll();
        }
    }
}