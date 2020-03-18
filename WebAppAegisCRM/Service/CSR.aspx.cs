using Business.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
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

        private void Csr_Save(string docketNo)
        {
            Business.Service.ServiceBook objServiceBook = new Business.Service.ServiceBook();
            DataSet ds = objServiceBook.Service_CSR_GetByDocketNo(docketNo);
            Entity.Service.CsrJson csrJson = new Entity.Service.CsrJson();
            Entity.Service.Csr csr = new Entity.Service.Csr();

            if (ds != null)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    csrJson.ProductName = ds.Tables[0].Rows[0]["ProductName"].ToString();
                    csrJson.MachineId = ds.Tables[0].Rows[0]["MachineId"].ToString();
                    csrJson.DocketNo = ds.Tables[0].Rows[0]["DocketNo"].ToString();
                    csrJson.CustomerCode = ds.Tables[0].Rows[0]["CustomerCode"].ToString();
                    csrJson.CustomerName = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                    csrJson.CustomerAddress = ds.Tables[0].Rows[0]["Address"].ToString();
                    csrJson.ProductSerialNo = ds.Tables[0].Rows[0]["ProductSerialNo"].ToString();
                    csrJson.DocketDate = ds.Tables[0].Rows[0]["DocketDate"].ToString();
                    csrJson.DocketTime = ds.Tables[0].Rows[0]["DocketTime"].ToString();
                    csrJson.DocketType = ds.Tables[0].Rows[0]["DocketType"].ToString();
                    csrJson.Problem = ds.Tables[0].Rows[0]["Problem"].ToString();
                    csrJson.ActionTaken = ds.Tables[0].Rows[0]["ActionTaken"].ToString();
                    csrJson.Diagnosis = ds.Tables[0].Rows[0]["Diagonosis"].ToString();
                    csrJson.Signature = ds.Tables[0].Rows[0]["Signature"].ToString();
                    csrJson.Stamp = ds.Tables[0].Rows[0]["Stamp"].ToString();
                    /* Checking whether machine is in contract or not*/
                    Business.Service.Contract objContract = new Business.Service.Contract();
                    if (objContract.Service_MachineIsInContractCheck(Convert.ToInt32(ds.Tables[0].Rows[0]["CustomerPurchaseId"].ToString())))
                    {
                        csrJson.IsInContract = true;
                    }
                    else
                    {
                        csrJson.IsInContract = false;
                    }
                }
                if (ds.Tables[3] != null && ds.Tables[3].Rows.Count > 0)
                {
                    csrJson.A3BWMeterReading = ds.Tables[3].Rows[0]["A3BWMeterReading"].ToString();
                    csrJson.A3CLMeterReading = ds.Tables[3].Rows[0]["A3CLMeterReading"].ToString();
                    csrJson.A4BWMeterReading = ds.Tables[3].Rows[0]["A4BWMeterReading"].ToString();
                    csrJson.A4CLMeterReading = ds.Tables[3].Rows[0]["A4CLMeterReading"].ToString();
                }
                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    List<Entity.Service.CallAttendance> callAttendances = new List<Entity.Service.CallAttendance>();

                    foreach (DataRow dataRow in ds.Tables[1].Rows)
                    {
                        callAttendances.Add(new Entity.Service.CallAttendance()
                        {
                            EmployeeName = dataRow["EmployeeName"].ToString(),
                            InDate = dataRow["InDate"].ToString(),
                            InTime = dataRow["InTime"].ToString(),
                            OutDate = dataRow["OutDate"].ToString(),
                            OutTime = dataRow["OutTime"].ToString(),
                            ProblemStatus = dataRow["ProblemStatus"].ToString()
                        });
                    }
                    csrJson.callAttendances = new List<Entity.Service.CallAttendance>();
                    csrJson.callAttendances = callAttendances;
                }
                if (ds.Tables[2] != null && ds.Tables[2].Rows.Count > 0)
                {
                    List<Entity.Service.SpareUsed> sparesUsed = new List<Entity.Service.SpareUsed>();

                    foreach (DataRow dataRow in ds.Tables[2].Rows)
                    {
                        sparesUsed.Add(new Entity.Service.SpareUsed()
                        {
                            SpareName = dataRow["SpareName"].ToString()
                        });
                    }
                    csrJson.sparesUsed = new List<Entity.Service.SpareUsed>();
                    csrJson.sparesUsed = sparesUsed;
                }
                csr.CsrContent = JsonConvert.SerializeObject(csrJson);
                csr.ServiceBookId = Convert.ToInt64(ds.Tables[0].Rows[0]["ServiceBookId"].ToString());
                csr.CreatedBy = Convert.ToInt32(HttpContext.Current.User.Identity.Name);
            }
            objServiceBook.Service_CsrSave(csr);
        }

        private void Service_CSR_GetByDocketId(string docketNo)
        {
            Business.Service.ServiceBook objServiceBook = new Business.Service.ServiceBook();
            Entity.Service.Csr csr = new Entity.Service.Csr();
            DataTable dtServiceBook = objServiceBook.Service_GetServiceBookByDocketNo(docketNo);

            if (dtServiceBook != null && dtServiceBook.Rows.Count > 0)
            {
                Entity.Service.CsrJson csrJson = new Entity.Service.CsrJson();
                DataTable dtCsr = objServiceBook.Service_CsrGetByServiceBookId(Convert.ToInt64(dtServiceBook.Rows[0]["ServiceBookId"].ToString()));
                if (dtCsr != null && dtCsr.Rows.Count > 0)
                {
                    csrJson = JsonConvert.DeserializeObject<Entity.Service.CsrJson>(dtCsr.Rows[0]["CsrContent"].ToString());
                    if (csrJson != null)
                    {
                        lblMachineModel.Text = csrJson.ProductName;
                        lblMachineId.Text = csrJson.MachineId;
                        lblDocketNo.Text = csrJson.DocketNo;
                        lblCustomerID.Text = csrJson.CustomerCode;
                        lblCustomerNameAddress.Text = csrJson.CustomerName + "<br/>" + csrJson.CustomerAddress;
                        lblMachineSlNo.Text = csrJson.ProductSerialNo;
                        lblDocketDateTime.Text = csrJson.DocketDate + " " + csrJson.DocketTime;
                        lblDocketType.Text = csrJson.DocketType;
                        lblComplaint.Text = csrJson.Problem;
                        lblDiagonosis.Text = csrJson.Diagnosis;
                        lblAction.Text = csrJson.ActionTaken;
                        imgCustomerSign.Src = (csrJson != null && csrJson.Signature.Length > 0) ? csrJson.Signature : Constants.NoImgaeUrl;
                        imgCustomerStamp.Src = (csrJson != null && csrJson.Stamp.Length > 0) ? "~/Customer/StampImage/" + csrJson.Stamp : Constants.NoImgaeUrl;
                        lblA3BW.Text = csrJson.A3BWMeterReading;
                        lblA4BW.Text = csrJson.A4BWMeterReading;
                        lblA3CL.Text = csrJson.A3CLMeterReading;
                        lblA4CL.Text = csrJson.A4CLMeterReading;
                        lblServiceChargeable.Text = csrJson.IsInContract ? string.Empty : "Service Chargeable";

                        if (csrJson.callAttendances != null)
                        {
                            tdRepeatCallCount.RowSpan = csrJson.callAttendances.Count + 1;
                            rptrRepeatCall.DataSource = csrJson.callAttendances;
                            rptrRepeatCall.DataBind();
                        }
                        if (csrJson.sparesUsed != null)
                        {
                            rptrParts.DataSource = csrJson.sparesUsed;
                            rptrParts.DataBind();
                        }
                    }
                }
                else
                {
                    Csr_Save(Request.QueryString["docketno"].ToString());
                    Service_CSR_GetByDocketId(Request.QueryString["docketno"].ToString());
                }
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }
    }
}