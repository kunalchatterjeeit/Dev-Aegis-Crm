using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.LeaveManagement
{
    public partial class LeaveApplicationList : System.Web.UI.Page
    {
        private void LeaveApplicationMaster_GetAll()
        {
            DataTable dtLeaveApplicationMaster =
                new Business.LeaveManagement.LeaveApplication()
                .LeaveApplicationMaster_GetAll(new Entity.LeaveManagement.LeaveApplicationMaster() {
                    RequestorId = Convert.ToInt32(HttpContext.Current.User.Identity.Name)
                });
            gvLeaveApplicationList.DataSource = dtLeaveApplicationMaster;
            gvLeaveApplicationList.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LeaveApplicationMaster_GetAll();
            }
        }

        protected void gvLeaveApplicationList_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvLeaveApplicationList.PageIndex = e.NewPageIndex;
            LeaveApplicationMaster_GetAll();
        }
    }
}