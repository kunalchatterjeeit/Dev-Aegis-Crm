using AjaxControlToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppAegisCRM.UserControl
{
    public partial class SalesLinkTypeControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ddlAccounts.Items.Add(new ListItem() { Text = "Account1", Value = "1" });
            ddlLead.Items.Add(new ListItem() { Text = "Lead1", Value = "2" });
            ddlOpportunity.Items.Add(new ListItem() { Text = "Opportunity1", Value = "3" });
        }

        protected void btnContinue_Click(object sender, EventArgs e)
        {
            HiddenField hdnItemType = (HiddenField)Parent.FindControl("hdnItemType");
            HiddenField hdnItemId = (HiddenField)Parent.FindControl("hdnItemId");
            if (TabContainer1.ActiveTabIndex == 0)
            {
                hdnItemType.Value = "SalesAccounts";
                hdnItemId.Value = ddlAccounts.SelectedValue;
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                hdnItemType.Value = "SalesLeads";
                hdnItemId.Value = ddlLead.SelectedValue;
            }
            else if (TabContainer1.ActiveTabIndex == 2)
            {
                hdnItemType.Value = "SalesOpportunity";
                hdnItemId.Value = ddlOpportunity.SelectedValue;
            }
            ((ModalPopupExtender)Parent.FindControl("ModalPopupExtender1")).Hide();
        }

        protected void btnCreateNew_Click(object sender, EventArgs e)
        {
            if (TabContainer1.ActiveTabIndex == 0)
            {
                Response.Redirect("../Sales/Accounts.aspx");
            }
            else if (TabContainer1.ActiveTabIndex == 1)
            {
                Response.Redirect("../Sales/Leads.aspx");
            }
            else if (TabContainer1.ActiveTabIndex == 2)
            {
                Response.Redirect("../Sales/Opportunities.aspx");
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect(Business.Common.Context.ReferralUrl);
        }
    }
}