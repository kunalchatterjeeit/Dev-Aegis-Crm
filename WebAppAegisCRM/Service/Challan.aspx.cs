using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Business.Common;

namespace WebAppAegisCRM.Service
{
    public partial class Challan : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["requestno"] != null && Request.QueryString["requestno"].ToString().Length > 0)
            {
                try
                {
                    Service_Challan_GetByTonerRequestNo(Request.QueryString["requestno"].ToString());
                }
                catch (Exception ex)
                {
                    //Response.Write(ex.Message);
                    //Business.Common.ErrorLog.MasterErrorLog(Server.MapPath("~") + "/ErrorLog/Errors.txt", ex.Message, HttpContext.Current.User.Identity.Name);
                    ex.WriteException();
                }
            }
        }

        private void Service_Challan_GetByTonerRequestNo(string requestNo)
        {
            Business.Service.ServiceBook objServiceBook = new Business.Service.ServiceBook();
            DataSet ds = new DataSet();
            ds = objServiceBook.Service_Challan_GetByTonerRequestNo(requestNo);

            if (ds != null)
            {
                if (ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0)
                {
                    tdConsigneeName.InnerHtml = ds.Tables[0].Rows[0]["CustomerName"].ToString();
                    tdRemarks.InnerHtml = ds.Tables[0].Rows[0]["Remarks"].ToString();
                }
                if (ds.Tables[1] != null && ds.Tables[1].Rows.Count > 0)
                {
                    tdDescription.InnerHtml = ds.Tables[1].Rows[0]["SpareName"].ToString();
                }
            }

            PrintDateTime.InnerHtml = System.DateTime.Now.ToString("dd/MMM/yyyy HH:MM tt");
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
        }
    }
}