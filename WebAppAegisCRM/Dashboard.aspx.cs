using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using Entity.Service;
using Entity.Common;
using System.Web.Script.Serialization;

namespace WebAppAegisCRM
{
    public partial class Dashboard : System.Web.UI.Page, ICallbackEventHandler
    {
        private DashBoardElements _Callback;
        private static DashboardEvent _DashboardEvent { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
                Response.Redirect("~/MainLogout.aspx");
            if (!IsPostBack)
            {
                _DashboardEvent = DashboardEvent.None;
            }

            if (!Page.IsCallback)
            {
                ltCallback.Text = ClientScript.GetCallbackEventReference(this, "'bindDocketgrid'", "EndGetDocketData", "'asyncgrid1'", true);
            }

            LoadPieChart();
        }

        private void LoadDocket(int pageIndex, int pageSize)
        {
            Business.Service.Docket objDocket = new Business.Service.Docket();
            Entity.Service.Docket docket = new Entity.Service.Docket();

            int assignEngineer = 0;
            string callStatusIds = string.Empty;

            callStatusIds = string.Concat(((int)CallStatusType.DocketClose).ToString(), ",", ((int)CallStatusType.DocketFunctional).ToString());//DOCKET CLOSE && FUNCTIONAL

            if (HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
                assignEngineer = 0;
            else
                assignEngineer = int.Parse(HttpContext.Current.User.Identity.Name);

            docket.CallStatusIds = callStatusIds;
            docket.AssignEngineer = assignEngineer;
            docket.PageIndex = pageIndex;
            docket.PageSize = pageSize;

            DataSet response = (_DashboardEvent == DashboardEvent.None || _DashboardEvent == DashboardEvent.Docket) ? objDocket.Service_Docket_GetByCallStatusIds(docket) : Business.Common.Context.DocketList;
            Business.Common.Context.DocketList = response;
            gvDocketAsync.DataSource = response.Tables[0];
            gvDocketAsync.VirtualItemCount = (response.Tables[1].Rows.Count > 0) ? Convert.ToInt32(response.Tables[1].Rows[0]["TotalCount"].ToString()) : 10;
            gvDocketAsync.DataBind();
            //lblDocketTotal.Text = string.Concat("Total records: {0}", gvDocketAsync.VirtualItemCount);
        }

        private void LoadTonerRequest(int pageIndex, int pageSize)
        {
            Business.Service.TonerRequest objTonnerRequest = new Business.Service.TonerRequest();
            Entity.Service.TonerRequest tonerRequest = new Entity.Service.TonerRequest();

            tonerRequest.PageIndex = pageIndex;
            tonerRequest.PageSize = pageSize;
            string callStatusIds = string.Empty;
            callStatusIds = string.Concat(((int)CallStatusType.TonerOpenForApproval).ToString(),
                ",",
                ((int)CallStatusType.TonerRequestInQueue).ToString(),
                ",",
                ((int)CallStatusType.TonerResponseGiven).ToString());

            tonerRequest.MultipleCallStatusFilter = callStatusIds;
            if (HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
                tonerRequest.AssignEngineer = 0;
            else
                tonerRequest.AssignEngineer = int.Parse(HttpContext.Current.User.Identity.Name);

            DataSet response = (_DashboardEvent == DashboardEvent.None || _DashboardEvent == DashboardEvent.Toner) ? objTonnerRequest.Service_TonnerRequest_GetAllMinimal(tonerRequest) : Business.Common.Context.TonerList;
            Business.Common.Context.TonerList = response;
            gvTonnerRequestAsync.DataSource = response.Tables[0];
            gvTonnerRequestAsync.VirtualItemCount = (response.Tables[1].Rows.Count > 0) ? Convert.ToInt32(response.Tables[1].Rows[0]["TotalCount"].ToString()) : 10;
            gvTonnerRequestAsync.DataBind();
            //lblTonerTotal.Text = string.Concat("Total records: ", gvTonnerRequestAsync.VirtualItemCount);
        }

        protected void LoadContractStatusList(int pageIndex, int pageSize, ContractStatusType contractType)
        {
            Entity.Service.Contract contract = new Entity.Service.Contract();
            Business.Service.Contract objContract = new Business.Service.Contract();
            contract.PageIndex = pageIndex;
            contract.PageSize = pageSize;
            contract.MachineId = "";
            contract.FromDate = DateTime.MinValue;
            contract.ToDate = DateTime.MinValue;
            if (HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
                contract.AssignEngineer = 0;
            else
                contract.AssignEngineer = int.Parse(HttpContext.Current.User.Identity.Name);

            DataSet ds = (_DashboardEvent == DashboardEvent.None || _DashboardEvent == DashboardEvent.ExpiredList || _DashboardEvent == DashboardEvent.ExpiringList) ? objContract.Service_ContractStatusList(contract) : Business.Common.Context.ContractStatusList;
            Business.Common.Context.ContractStatusList = ds;

            if (ContractStatusType.None == contractType)
            {
                gvExpiringSoonAsync.DataSource = ds.Tables[0];
                gvExpiringSoonAsync.VirtualItemCount = (ds.Tables[4].Rows.Count > 0) ? Convert.ToInt32(ds.Tables[4].Rows[0]["TotalCount"].ToString()) : 17;
                gvExpiringSoonAsync.DataBind();

                gvExpiredListAsync.DataSource = ds.Tables[1];
                gvExpiredListAsync.VirtualItemCount = (ds.Tables[5].Rows.Count > 0) ? Convert.ToInt32(ds.Tables[5].Rows[0]["TotalCount"].ToString()) : 17;
                gvExpiredListAsync.DataBind();
            }
            else if (ContractStatusType.Expiring == contractType)
            {
                gvExpiringSoonAsync.DataSource = ds.Tables[0];
                gvExpiringSoonAsync.VirtualItemCount = (ds.Tables[4].Rows.Count > 0) ? Convert.ToInt32(ds.Tables[4].Rows[0]["TotalCount"].ToString()) : 17;
                gvExpiringSoonAsync.DataBind();
            }
            else if (ContractStatusType.Expired == contractType)
            {
                gvExpiredListAsync.DataSource = ds.Tables[1];
                gvExpiredListAsync.VirtualItemCount = (ds.Tables[5].Rows.Count > 0) ? Convert.ToInt32(ds.Tables[5].Rows[0]["TotalCount"].ToString()) : 17;
                gvExpiredListAsync.DataBind();
            }
            //lblExpiringTotal.Text = string.Concat("Total records: {0}", gvExpiringSoonAsync.VirtualItemCount);
            //lblExpiredTotal.Text = string.Concat("Total records: {0}", gvExpiredListAsync.VirtualItemCount);
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

        protected void gvExpiringSoonAsync_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvExpiringSoonAsync.PageIndex = e.NewPageIndex;
            //LoadContractStatusList(e.NewPageIndex, gvExpiringSoonAsync.PageSize, ContractStatusType.Expiring);
            _DashboardEvent = DashboardEvent.ExpiringList;
        }

        protected void gvDocketAsync_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvDocketAsync.PageIndex = e.NewPageIndex;
            //LoadDocket(e.NewPageIndex, gvDocketAsync.PageSize);
            _DashboardEvent = DashboardEvent.Docket;
        }

        protected void gvTonnerRequestAsync_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvTonnerRequestAsync.PageIndex = e.NewPageIndex;
            _DashboardEvent = DashboardEvent.Toner;
            //LoadTonnerRequest(e.NewPageIndex, gvDocketAsync.PageSize);
        }

        protected void gvExpiredListAsync_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvExpiredListAsync.PageIndex = e.NewPageIndex;
            //LoadContractStatusList(e.NewPageIndex, gvExpiredListAsync.PageSize, ContractStatusType.Expired);
            _DashboardEvent = DashboardEvent.ExpiredList;
        }

        protected void gvDocketAsync_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlContainerControl anchorDocket = e.Row.FindControl("anchorDocket") as HtmlContainerControl;
                anchorDocket.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.DOCKET_QUICK_LINK_PERMISSION);

                if (((DataTable)(gvDocketAsync.DataSource)).Rows[e.Row.RowIndex]["IsCallAttended"].ToString().Equals("1"))
                {
                    HtmlContainerControl anchorCallIn = e.Row.FindControl("anchorCallIn") as HtmlContainerControl;
                    anchorCallIn.Attributes["style"] = "display:none";
                    e.Row.Attributes["style"] = "background-color: #C6F2C6";
                }
                else
                {
                    HtmlContainerControl anchorCallOut = e.Row.FindControl("anchorCallOut") as HtmlContainerControl;
                    anchorCallOut.Attributes["style"] = "display:none";
                }

                //Call Status wise row color
                if (((DataTable)(gvDocketAsync.DataSource)).Rows[e.Row.RowIndex]["CallStatusId"].ToString() == ((int)CallStatusType.DocketOpenForApproval).ToString())
                {
                    e.Row.Attributes["style"] = "background-color: #FF8787";
                }
                else if (((DataTable)(gvDocketAsync.DataSource)).Rows[e.Row.RowIndex]["CallStatusId"].ToString() == ((int)CallStatusType.DocketResponseGiven).ToString())
                {
                    e.Row.Attributes["style"] = "background-color: #A7FC94";
                }
                else if (((DataTable)(gvDocketAsync.DataSource)).Rows[e.Row.RowIndex]["CallStatusId"].ToString() == ((int)CallStatusType.DocketOpenForSpares).ToString())
                {
                    e.Row.Attributes["style"] = "background-color: #8DF1FC";
                }
                else if (((DataTable)(gvDocketAsync.DataSource)).Rows[e.Row.RowIndex]["CallStatusId"].ToString() == ((int)CallStatusType.DocketOpenForConsumables).ToString())
                {
                    e.Row.Attributes["style"] = "background-color: #8DF1FC";
                }
                else if (((DataTable)(gvDocketAsync.DataSource)).Rows[e.Row.RowIndex]["CallStatusId"].ToString() == ((int)CallStatusType.DocketOpenForSeniorEngineer).ToString())
                {
                    e.Row.Attributes["style"] = "background-color: #F7B3FC";
                }
            }
        }

        protected void gvTonnerRequestAsync_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlContainerControl anchorToner = e.Row.FindControl("anchorToner") as HtmlContainerControl;
                anchorToner.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.TONNER_QUICK_LINK_PERMISSION);

                //Call Status wise row color
                if (((DataTable)(gvTonnerRequestAsync.DataSource)).Rows[e.Row.RowIndex]["CallStatusId"].ToString() == ((int)CallStatusType.TonerOpenForApproval).ToString())
                {
                    e.Row.Attributes["style"] = "background-color: #FF8787";
                }
                else if (((DataTable)(gvTonnerRequestAsync.DataSource)).Rows[e.Row.RowIndex]["CallStatusId"].ToString() == ((int)CallStatusType.TonerResponseGiven).ToString())
                {
                    e.Row.Attributes["style"] = "background-color: #A7FC94";
                }
            }
        }

        public void RaiseCallbackEvent(string eventArgument)
        {
            _Callback = new DashBoardElements();

            if (HttpContext.Current.User.IsInRole(Entity.HR.Utility.DASHBOARD_DOCKET_LIST))
            {
                LoadDocket(gvDocketAsync.PageIndex, gvDocketAsync.PageSize);
                using (System.IO.StringWriter sw = new System.IO.StringWriter())
                {
                    gvDocketAsync.RenderControl(new HtmlTextWriter(sw));
                    _Callback.DocketList = sw.ToString();
                }
            }

            LoadTonerRequest(gvTonnerRequestAsync.PageIndex, gvTonnerRequestAsync.PageSize);
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                gvTonnerRequestAsync.RenderControl(new HtmlTextWriter(sw));
                _Callback.TonerList = sw.ToString();
            }

            LoadContractStatusList(0, gvExpiredListAsync.PageSize, ContractStatusType.None);
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                gvExpiringSoonAsync.RenderControl(new HtmlTextWriter(sw));
                _Callback.ExpiringSoonList = sw.ToString();
            }
            using (System.IO.StringWriter sw = new System.IO.StringWriter())
            {
                gvExpiredListAsync.RenderControl(new HtmlTextWriter(sw));
                _Callback.ExpiredList = sw.ToString();
            }
        }

        public string GetCallbackResult()
        {
            string json = new JavaScriptSerializer().Serialize(_Callback);
            return json;
        }
    }
}