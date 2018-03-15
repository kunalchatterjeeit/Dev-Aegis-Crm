using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

namespace WebAppAegisCRM
{
    public partial class Dashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                Response.Redirect("~/MainLogout.aspx");

            if (!IsPostBack)
            {
                LoadDocket();
                LoadTonnerRequest();
                LoadContractStatusList();
            }

            LoadPieChart();
        }

        protected void LoadDocket()
        {
            Business.Service.Docket objDocket = new Business.Service.Docket();
            Entity.Service.Docket docket = new Entity.Service.Docket();

            docket.DocketNo = "";
            docket.CustomerId = 0;
            docket.ProductId = 0;
            docket.DocketFromDateTime = DateTime.MinValue;
            docket.DocketToDateTime = DateTime.MinValue;
            docket.CallStatusId = 1;
            if (HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
                docket.AssignEngineer = 0;
            else
                docket.AssignEngineer = int.Parse(HttpContext.Current.User.Identity.Name);

            DataTable dt = objDocket.Service_Docket_GetAll(docket);
            using (DataView dv = new DataView(dt))
            {
                if (dt != null)
                {
                    gvDocket.DataSource = dv.ToTable();
                    gvDocket.DataBind();
                }
            }
        }

        protected void LoadTonnerRequest()
        {
            Business.Service.TonerRequest objTonnerRequest = new Business.Service.TonerRequest();
            Entity.Service.TonerRequest tonnerRequest = new Entity.Service.TonerRequest();

            tonnerRequest.RequestNo = "";
            tonnerRequest.CustomerId = 0;
            tonnerRequest.ProductId = 0;
            tonnerRequest.RequestFromDateTime = DateTime.MinValue;
            tonnerRequest.RequestToDateTime = DateTime.MinValue;
            tonnerRequest.CallStatusId = 0;
            if (HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
                tonnerRequest.AssignEngineer = 0;
            else
                tonnerRequest.AssignEngineer = int.Parse(HttpContext.Current.User.Identity.Name);

            DataTable dt = objTonnerRequest.Service_TonerRequest_GetAll(tonnerRequest).Tables[0];
            using (DataView dv = new DataView(dt))
            {
                dv.RowFilter = "CallStatusId IN (7,8)"; //REQUEST IN QUEUE && OPEN FOR APPROVAL

                if (dt != null)
                {
                    gvTonnerRequest.DataSource = dv.ToTable();
                    gvTonnerRequest.DataBind();
                }
            }
        }

        protected void LoadContractStatusList()
        {
            Entity.Service.Contract contract = new Entity.Service.Contract();
            Business.Service.Contract objContract = new Business.Service.Contract();
            contract.MachineId = "";
            contract.FromDate = DateTime.MinValue;
            contract.ToDate = DateTime.MinValue;
            if (HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
                contract.AssignEngineer = 0;
            else
                contract.AssignEngineer = int.Parse(HttpContext.Current.User.Identity.Name);
            DataSet ds = objContract.Service_ContractStatusList(contract);

            gvExpiringSoon.DataSource = ds.Tables[0];
            gvExpiringSoon.DataBind();

            gvExpiredList.DataSource = ds.Tables[1];
            gvExpiredList.DataBind();
        }

        protected void LoadPieChart()
        {
            Business.Service.Contract objContract = new Business.Service.Contract();
            int assignEngineer = 0;
            if (HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
                assignEngineer = 0;
            else
                assignEngineer = int.Parse(HttpContext.Current.User.Identity.Name);
            DataTable dt = objContract.Services_ContractStatus(assignEngineer);
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "script", "PieData(" + dt.Rows[0]["ExperingSoon"].ToString() + "," + dt.Rows[0]["Expired"].ToString() + "," + dt.Rows[0]["InContract"].ToString() + "," + dt.Rows[0]["NeverContracted"].ToString() + ")", true);
        }

        protected void gvDocket_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDocket.PageIndex = e.NewPageIndex;
            LoadDocket();
        }

        protected void gvTonnerRequest_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTonnerRequest.PageIndex = e.NewPageIndex;
            LoadTonnerRequest();
        }

        protected void gvExpiringSoon_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvExpiringSoon.PageIndex = e.NewPageIndex;
            Entity.Service.Contract contract = new Entity.Service.Contract();
            Business.Service.Contract objContract = new Business.Service.Contract();
            contract.MachineId = "";
            contract.FromDate = DateTime.MinValue;
            contract.ToDate = DateTime.MinValue;
            if (HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
                contract.AssignEngineer = 0;
            else
                contract.AssignEngineer = int.Parse(HttpContext.Current.User.Identity.Name);
            DataSet ds = objContract.Service_ContractStatusList(contract);
            gvExpiringSoon.DataSource = ds.Tables[0];
            gvExpiringSoon.DataBind();
        }

        protected void gvExpiredList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvExpiredList.PageIndex = e.NewPageIndex;
            Entity.Service.Contract contract = new Entity.Service.Contract();
            Business.Service.Contract objContract = new Business.Service.Contract();
            contract.MachineId = "";
            contract.FromDate = DateTime.MinValue;
            contract.ToDate = DateTime.MinValue;
            if (HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
                contract.AssignEngineer = 0;
            else
                contract.AssignEngineer = int.Parse(HttpContext.Current.User.Identity.Name);
            DataSet ds = objContract.Service_ContractStatusList(contract);
            gvExpiredList.DataSource = ds.Tables[1];
            gvExpiredList.DataBind();
        }

        protected void gvDocket_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlContainerControl anchorDocket = e.Row.FindControl("anchorDocket") as HtmlContainerControl;
                anchorDocket.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.DOCKET_QUICK_LINK_PERMISSION);
            }
        }

        protected void gvTonnerRequest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlContainerControl anchorToner = e.Row.FindControl("anchorToner") as HtmlContainerControl;
                anchorToner.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.TONNER_QUICK_LINK_PERMISSION);

                if (((DataTable)(gvTonnerRequest.DataSource)).Rows[e.Row.RowIndex]["CallStatusId"].ToString() == "8")
                {
                    e.Row.Attributes["style"] = "background-color: #fff39e";
                }
            }
        }
    }
}