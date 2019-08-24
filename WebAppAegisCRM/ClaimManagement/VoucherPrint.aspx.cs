using Entity.ClaimManagement;
using Newtonsoft.Json;
using System;
using System.Data;

namespace WebAppAegisCRM.ClaimManagement
{
    public partial class VoucherPrint : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["no"] != null)
                {
                    Business.ClaimManagement.Voucher objVoucher = new Business.ClaimManagement.Voucher();
                    DataTable dtVoucher = objVoucher.Voucher_GetAll(new Voucher()
                    {
                        VoucherNo = Request.QueryString["no"].ToString()
                    });
                    VoucherJson voucher = new VoucherJson();
                    voucher = JsonConvert.DeserializeObject<VoucherJson>(dtVoucher.Rows[0]["VoucherJson"].ToString());
                    voucher.VoucherNo = dtVoucher.Rows[0]["VoucherNo"].ToString();
                    GenerateVoucher(voucher);
                }
                else
                {
                    throw new Exception("Invalid voucher id");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        private void GenerateVoucher(VoucherJson voucher)
        {
            lblPayMethod.Text = voucher.PayMethod;
            lblCheque.Text = voucher.ChequeNo;
            lblVoucherNo.Text = voucher.VoucherNo;
            lblVoucherDate.Text = voucher.CreateDate.ToString("dd MMM yyyy");
            lblName.Text = voucher.EmployeeName;
            lblTotalAmount.Text = voucher.TotalAmount.ToString().Split('.')[0];
            lblTotalPaisa.Text = voucher.TotalAmount.ToString().Split('.')[1];
            rptrDescription.DataSource = voucher.VoucherDescriptionList;
            rptrDescription.DataBind();
        }
    }
}