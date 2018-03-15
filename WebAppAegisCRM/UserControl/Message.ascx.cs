using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace WebAppAegisCRM.UserControl
{
    public partial class Message : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public bool IsSuccess { get; set; }
        public string Text
        {
            set
            {
                if (IsSuccess == true)
                {
                    lblSuccess.Text = value;
                }
                else
                {
                    lblError.Text = value;
                }
            }
        }

        public bool Show
        {
            set
            {
                if (value == true)
                {
                    if (IsSuccess == true)
                    {
                        spanSuccess.Visible = true;
                        spanError.Visible = false;
                    }
                    else
                    {
                        spanSuccess.Visible = false;
                        spanError.Visible = true;
                    }
                }
                else
                {
                    spanSuccess.Visible = false;
                    spanError.Visible = false;
                }
            }
        }

    }
}