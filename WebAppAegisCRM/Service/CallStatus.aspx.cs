using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.Service
{
    public partial class CallStatus : System.Web.UI.Page
    {
        public int CallStatusId
        {
            get { return Convert.ToInt32(ViewState["CallStatusId"]); }
            set { ViewState["CallStatusId"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ClearControls();
                LoadCallStatus();
            }
        }

        protected void ClearControls()
        {
            CallStatusId = 0;
            txtCallStatusName.Text = "";
            Message.Show = false;
        }

        protected void LoadCallStatus()
        {
            //Business.Service.CallStatus objCallStatus = new Business.Service.CallStatus();

            //DataTable dt = new DataTable();
            //dt = objCallStatus.GetAll();
            //gvCallStatus.DataSource = dt;
            //gvCallStatus.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Business.Service.CallStatus objCallStatus = new Business.Service.CallStatus();
            Entity.Service.CallStatus CallStatus = new Entity.Service.CallStatus();

            CallStatus.CallStatusId = CallStatusId;
            CallStatus.CallStatusName = txtCallStatusName.Text;

            int i = objCallStatus.Save(CallStatus);

            if (i > 0)
            {
                ClearControls();
                LoadCallStatus();
                Message.IsSuccess = true;
                Message.Text = "Call Status saved successfully...";
            }
            else
            {
                Message.IsSuccess = false;
                Message.Text = "Can not save!!!";
            }
            Message.Show = true;
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ClearControls();
        }

        protected void gvCallStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCallStatus.PageIndex = e.NewPageIndex;
            LoadCallStatus();
        }

        protected void gvCallStatus_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            Business.Service.CallStatus objCallStatus = new Business.Service.CallStatus();
            Entity.Service.CallStatus CallStatus = new Entity.Service.CallStatus();

            if (e.CommandName == "Ed")
            {
                int callStatusId = int.Parse(e.CommandArgument.ToString());
                CallStatus = objCallStatus.GetById(callStatusId);
                CallStatusId = CallStatus.CallStatusId;
                txtCallStatusName.Text = CallStatus.CallStatusName;
            }
        }
    }
}