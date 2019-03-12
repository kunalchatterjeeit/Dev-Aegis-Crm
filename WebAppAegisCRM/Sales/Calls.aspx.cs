using Business.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Sales
{
    public partial class Calls : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadCallsDropdowns();
                Message.Show = false;
            }
        }

        private void LoadCallsDropdowns()
        {
            Business.Sales.Calls Obj = new Business.Sales.Calls();

            ddlCallDirection.DataSource = Obj.GetCallDirection();
            ddlCallDirection.DataTextField = "Name";
            ddlCallDirection.DataValueField = "Id";
            ddlCallDirection.DataBind();
            ddlCallDirection.InsertSelect();

            ddlCallRelatedTo.DataSource = Obj.GetCallRelated();
            ddlCallRelatedTo.DataTextField = "Name";
            ddlCallRelatedTo.DataValueField = "Id";
            ddlCallRelatedTo.DataBind();
            ddlCallRelatedTo.InsertSelect();

            ddlCallRepeatType.DataSource = Obj.GetCallRepeatType();
            ddlCallRepeatType.DataTextField = "Name";
            ddlCallRepeatType.DataValueField = "Id";
            ddlCallRepeatType.DataBind();
            ddlCallRepeatType.InsertSelect();

            ddlCallStatus.DataSource = Obj.GetCallStatus();
            ddlCallStatus.DataTextField = "Name";
            ddlCallStatus.DataValueField = "Id";
            ddlCallStatus.DataBind();
            ddlCallStatus.InsertSelect();
        }
    }
}