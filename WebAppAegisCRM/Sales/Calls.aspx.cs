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
            }
        }

        private void LoadCallsDropdowns()
        {
            Business.Sales.Calls Obj = new Business.Sales.Calls();

            ddlCallDirection.DataSource = Obj.GetCallDirection();
            ddlCallDirection.DataTextField = "Name";
            ddlCallDirection.DataValueField = "Id";
            ddlCallDirection.DataBind();

            ddlCallRelatedTo.DataSource = Obj.GetCallRelated();
            ddlCallRelatedTo.DataTextField = "Name";
            ddlCallRelatedTo.DataValueField = "Id";
            ddlCallRelatedTo.DataBind();

            ddlCallRepeatType.DataSource = Obj.GetCallRepeatType();
            ddlCallRepeatType.DataTextField = "Name";
            ddlCallRepeatType.DataValueField = "Id";
            ddlCallRepeatType.DataBind();

            ddlCallStatus.DataSource = Obj.GetCallStatus();
            ddlCallStatus.DataTextField = "Name";
            ddlCallStatus.DataValueField = "Id";
            ddlCallStatus.DataBind();
        }
    }
}