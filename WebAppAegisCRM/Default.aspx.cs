using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    DateTime dateTime = new DateTime(Convert.ToInt64(Request.QueryString["id"].ToString()));
                    if (dateTime < DateTime.UtcNow)
                    {
                        Response.Redirect("https://aegissolutions.in/");
                    }
                }
                else
                {
                    Response.Redirect("https://aegissolutions.in/");
                }
                Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.Cache.SetNoStore();
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Response.Redirect(string.Concat("Login.aspx?id=", Request.QueryString["id"].ToString()));
        }
    }
}