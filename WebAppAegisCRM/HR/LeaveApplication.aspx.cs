using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Business.Common;


namespace WebAppAegisCRM.HR
{
	public partial class LeaveApplication : System.Web.UI.Page
	{
        public int LeaveApplicationid
        {
            get { return Convert.ToInt32(ViewState["LeaveMasterId"]); }
            set { ViewState["LeaveMasterId"] = value; }
        }
       
        protected void Page_Load(object sender, EventArgs e)
		{

		}

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            Business.HR.LeaveManagement objLeaveMaster = new Business.HR.LeaveManagement();
            Entity.HR.LeaveMaster leaveMaster = new Entity.HR.LeaveMaster();


            leaveMaster.LeaveApplicationId = LeaveApplicationid;
            leaveMaster.LeaveApplicationNumber = txtLeaveApplicationNo.Text.Trim();
            leaveMaster.LeaveTypeId=Convert.ToInt16(ddLeavetypeid.SelectedValue);
            leaveMaster.LeaveAccumulationTypeId = txtLeaveAccumulationTypeId.Text.Trim();
            leaveMaster.FromDate = (txtLSD.Text.Trim() == string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtLSD.Text.Trim());
            leaveMaster.ToDate = (txtLED.Text.Trim() == string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtLED.Text.Trim());
            leaveMaster.ApplyDate = (txtLAD.Text.Trim() == string.Empty) ? DateTime.MinValue : Convert.ToDateTime(txtLAD.Text.Trim());
            leaveMaster.Reason = txtReason.Text.Trim();
            leaveMaster.Attachment = Attachment.Text.Trim();

        }
        public void CleartextBoxes(Control parent)
        {
            foreach (Control c in parent.Controls)
            {
                if ((c.GetType() == typeof(TextBox)))
                {
                    ((TextBox)(c)).Text = string.Empty;
                }
                if (c.HasControls())
                {
                    CleartextBoxes(c);
                }
            }
        }
    }
}