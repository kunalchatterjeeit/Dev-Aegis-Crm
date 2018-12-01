using Business.Common;
using System;
using System.Data;
using System.Web.UI;

namespace WebAppAegisCRM.Service
{
    public partial class CSR : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["docketno"] != null && Request.QueryString["docketno"].ToString().Length > 0)
            {
                try
                {
                    Service_CSR_GetByDocketId(Request.QueryString["docketno"].ToString());
                }
                catch (Exception ex)
                {
                    ex.WriteException();
                }
            }
        }

        private void Service_CSR_GetByDocketId(string docketNo)
        {
            Business.Service.ServiceBook objServiceBook = new Business.Service.ServiceBook();
            DataSet ds = objServiceBook.Service_CSR_GetByDocketNo(docketNo);

            if (ds != null)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    lblMachineModel.Text = ds.Tables[0].Rows[0]["ProductName"].ToString();
                    lblMachineId.Text = ds.Tables[0].Rows[0]["MachineId"].ToString();
                    lblDocketNo.Text = ds.Tables[0].Rows[0]["DocketNo"].ToString();
                    lblCustomerID.Text = ds.Tables[0].Rows[0]["CustomerCode"].ToString();
                    lblCustomerNameAddress.Text = ds.Tables[0].Rows[0]["CustomerName"].ToString() + "<br/>" + ds.Tables[0].Rows[0]["Address"].ToString();
                    lblMachineSlNo.Text = ds.Tables[0].Rows[0]["ProductSerialNo"].ToString();
                    lblDocketDateTime.Text = ds.Tables[0].Rows[0]["DocketDate"].ToString() + " " + ds.Tables[0].Rows[0]["DocketTime"].ToString();
                    lblDocketType.Text = ds.Tables[0].Rows[0]["DocketType"].ToString();
                    lblComplaint.Text = ds.Tables[0].Rows[0]["Problem"].ToString();
                    lblDiagonosis.Text = ds.Tables[0].Rows[0]["Diagonosis"].ToString();
                    lblAction.Text = ds.Tables[0].Rows[0]["ActionTaken"].ToString();
                    imgCustomerSign.Src = (ds.Tables[0] != null && ds.Tables[0].Rows[0]["Signature"].ToString().Length > 0) ? ds.Tables[0].Rows[0]["Signature"].ToString() : Constants.NoImgaeUrl;
                    imgCustomerStamp.Src = (ds.Tables[0] != null && ds.Tables[0].Rows[0]["Stamp"].ToString().Length > 0) ? "~/Customer/StampImage/" + ds.Tables[0].Rows[0]["Stamp"].ToString() : Constants.NoImgaeUrl;
                }
                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    tdRepeatCallCount.RowSpan = ds.Tables[1].Rows.Count + 1;
                    rptrRepeatCall.DataSource = ds.Tables[1];
                    rptrRepeatCall.DataBind();
                }
                if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                {
                    rptrParts.DataSource = ds.Tables[2];
                    rptrParts.DataBind();
                }
                if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                {
                    lblA3BW.Text = ds.Tables[3].Rows[0]["A3BWMeterReading"].ToString();
                    lblA4BW.Text = ds.Tables[3].Rows[0]["A4BWMeterReading"].ToString();
                    lblA3CL.Text = ds.Tables[3].Rows[0]["A3CLMeterReading"].ToString();
                    lblA4CL.Text = ds.Tables[3].Rows[0]["A4CLMeterReading"].ToString();
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }
    }
}