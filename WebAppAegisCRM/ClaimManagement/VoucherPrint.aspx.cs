using Business.Common;
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
                    DataSet dsVoucher = objVoucher.Voucher_GetAll(new Voucher()
                    {
                        VoucherNo = Request.QueryString["no"].ToString(),
                        PageIndex = 0,
                        PageSize = 10
                    });
                    VoucherJson voucher = new VoucherJson();
                    voucher = JsonConvert.DeserializeObject<VoucherJson>(dsVoucher.Tables[0].Rows[0]["VoucherJson"].ToString());
                    voucher.VoucherNo = dsVoucher.Tables[0].Rows[0]["VoucherNo"].ToString();
                    GenerateVoucher(voucher);
                }
                else
                {
                    throw new Exception("Invalid voucher id");
                }
            }
            catch (Exception ex)
            {
                ex.WriteException();
                
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