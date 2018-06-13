﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using Entity.Service;
using Entity.Common;

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
                LoadContractStatusList(0, gvExpiredList.PageSize, ContractStatusType.None);
            }

            LoadPieChart();
        }

        protected void LoadDocket()
        {
            Business.Service.Docket objDocket = new Business.Service.Docket();
            Entity.Service.Docket docket = new Entity.Service.Docket();

            //docket.PageIndex = 0;
            //docket.PageSize = 50;
            docket.DocketNo = "";
            docket.CustomerId = 0;
            docket.ProductId = 0;
            docket.DocketFromDateTime = DateTime.MinValue;
            docket.DocketToDateTime = DateTime.MinValue;
            if (HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
                docket.AssignEngineer = 0;
            else
                docket.AssignEngineer = int.Parse(HttpContext.Current.User.Identity.Name);

            DataTable dt = objDocket.Service_Docket_GetAll(docket);
            using (DataView dv = new DataView(dt))
            {
                dv.RowFilter = "CallStatusId IN (1,12)"; //REQUEST IN QUEUE && RESPONSE GIVEN
                gvDocket.DataSource = dv.ToTable();
                gvDocket.DataBind();
            }
        }

        protected void LoadTonnerRequest()
        {
            Business.Service.TonerRequest objTonnerRequest = new Business.Service.TonerRequest();
            Entity.Service.TonerRequest tonerRequest = new Entity.Service.TonerRequest();

            tonerRequest.PageIndex = 0;
            tonerRequest.PageSize = 50;
            tonerRequest.RequestNo = "";
            tonerRequest.CustomerId = 0;
            tonerRequest.ProductId = 0;
            tonerRequest.RequestFromDateTime = DateTime.MinValue;
            tonerRequest.RequestToDateTime = DateTime.MinValue;
            tonerRequest.CallStatusId = 0;
            if (HttpContext.Current.User.IsInRole(Entity.HR.Utility.CUSTOMER_LIST_SHOW_ALL))
                tonerRequest.AssignEngineer = 0;
            else
                tonerRequest.AssignEngineer = int.Parse(HttpContext.Current.User.Identity.Name);

            DataTable dt = objTonnerRequest.Service_TonerRequest_GetAll(tonerRequest).Tables[0];
            using (DataView dv = new DataView(dt))
            {
                dv.RowFilter = "CallStatusId IN (7,13)"; //REQUEST IN QUEUE && RESPONSE GIVEN
                gvTonnerRequest.DataSource = dv.ToTable();

                gvTonnerRequest.DataBind();
            }
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
            DataSet ds = objContract.Service_ContractStatusList(contract);

            if (ContractStatusType.None == contractType)
            {
                gvExpiringSoon.DataSource = ds.Tables[0];
                gvExpiringSoon.VirtualItemCount = (ds.Tables[4].Rows.Count > 0) ? Convert.ToInt32(ds.Tables[4].Rows[0]["TotalCount"].ToString()) : 17;
                gvExpiringSoon.DataBind();

                gvExpiredList.DataSource = ds.Tables[1];
                gvExpiredList.VirtualItemCount = (ds.Tables[5].Rows.Count > 0) ? Convert.ToInt32(ds.Tables[5].Rows[0]["TotalCount"].ToString()) : 17;
                gvExpiredList.DataBind();
            }
            else if (ContractStatusType.Expiring == contractType)
            {
                gvExpiringSoon.DataSource = ds.Tables[0];
                gvExpiringSoon.VirtualItemCount = (ds.Tables[4].Rows.Count > 0) ? Convert.ToInt32(ds.Tables[4].Rows[0]["TotalCount"].ToString()) : 17;
                gvExpiringSoon.DataBind();
            }
            else if (ContractStatusType.Expired == contractType)
            {
                gvExpiredList.DataSource = ds.Tables[1];
                gvExpiredList.VirtualItemCount = (ds.Tables[5].Rows.Count > 0) ? Convert.ToInt32(ds.Tables[5].Rows[0]["TotalCount"].ToString()) : 17;
                gvExpiredList.DataBind();
            }
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
            LoadContractStatusList(e.NewPageIndex, gvExpiringSoon.PageSize, ContractStatusType.Expiring);
        }

        protected void gvExpiredList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvExpiredList.PageIndex = e.NewPageIndex;
            LoadContractStatusList(e.NewPageIndex, gvExpiredList.PageSize, ContractStatusType.Expired);
        }

        protected void gvDocket_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlContainerControl anchorDocket = e.Row.FindControl("anchorDocket") as HtmlContainerControl;
                anchorDocket.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.DOCKET_QUICK_LINK_PERMISSION);

                if (((DataTable)(gvDocket.DataSource)).Rows[e.Row.RowIndex + gvDocket.PageIndex * gvDocket.PageSize]["IsCallAttended"].ToString().Equals("1"))
                {
                    HtmlContainerControl anchorCallIn = e.Row.FindControl("anchorCallIn") as HtmlContainerControl;
                    anchorCallIn.Attributes["style"] = "display:none";
                    e.Row.Attributes["style"] = "background-color: #C6F2C6";
                }
                if (((DataTable)(gvDocket.DataSource)).Rows[e.Row.RowIndex + gvDocket.PageIndex * gvDocket.PageSize]["CallStatusId"].ToString() == ((int)CallStatusType.DocketResponseGiven).ToString())
                {
                    e.Row.Attributes["style"] = "background-color: #FFF39E";
                }
            }
        }

        protected void gvTonnerRequest_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HtmlContainerControl anchorToner = e.Row.FindControl("anchorToner") as HtmlContainerControl;
                anchorToner.Visible = HttpContext.Current.User.IsInRole(Entity.HR.Utility.TONNER_QUICK_LINK_PERMISSION);

                if (((DataTable)(gvTonnerRequest.DataSource)).Rows[e.Row.RowIndex + gvTonnerRequest.PageIndex * gvTonnerRequest.PageSize]["CallStatusId"].ToString() == ((int)CallStatusType.TonerResponseGiven).ToString())
                {
                    e.Row.Attributes["style"] = "background-color: #fff39e";
                }
            }
        }
    }
}