<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SalesLinkTypeControl.ascx.cs" Inherits="WebAppAegisCRM.UserControl.SalesLinkTypeControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:TabContainer ID="TabContainer1" runat="server" Width="100%" CssClass="MyTabStyle"
    ActiveTabIndex="0   ">
    <asp:TabPanel ID="AccountsTab" runat="server">
        <HeaderTemplate>
            Accounts
        </HeaderTemplate>
        <ContentTemplate>
            <div>
                <fieldset class="login margin-top">
                    <legend>Please select account to continue</legend>
                    <asp:DropDownList ID="ddlAccounts" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </fieldset>
            </div>
        </ContentTemplate>
    </asp:TabPanel>
    <asp:TabPanel ID="LeadsTab" runat="server">
        <HeaderTemplate>
            Lead
        </HeaderTemplate>
        <ContentTemplate>
            <fieldset class="login margin-top">
                <legend>Please select lead to continue</legend>
                <asp:DropDownList ID="ddlLead" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </fieldset>
        </ContentTemplate>
    </asp:TabPanel>
    <asp:TabPanel ID="OpportunityTab" runat="server">
        <HeaderTemplate>
            Opportunity
        </HeaderTemplate>
        <ContentTemplate>
            <fieldset class="login margin-top">
                <legend>Please select opportunity to continue</legend>
                <asp:DropDownList ID="ddlOpportunity" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </fieldset>
        </ContentTemplate>
    </asp:TabPanel>
</asp:TabContainer>
<br />
<div class="form-group">
    <asp:Button ID="btnContinue" runat="server" Text="Continue" class="btn btn-outline btn-success" Style="width: 100%" OnClick="btnContinue_Click" />
    <asp:Button ID="btnCreateNew" runat="server" Text="Create New" class="btn btn-outline btn-info margin-top" Style="width: 100%" OnClick="btnCreateNew_Click" />
    <asp:Button ID="btnReturn" runat="server" Text="Return" class="btn btn-outline btn-danger margin-top" Style="width: 100%" OnClick="btnReturn_Click" />
</div>

