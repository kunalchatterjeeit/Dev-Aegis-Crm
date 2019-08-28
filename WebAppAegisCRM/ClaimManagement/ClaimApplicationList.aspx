<%@ Page Title="MY CLAIM APPLICATION LIST" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ClaimApplicationList.aspx.cs" Inherits="WebAppAegisCRM.ClaimManagement.ClaimApplicationList" %>

<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="content-type" content="text/html; charset=UTF-8" />
    <script src="http://maps.google.com/maps/api/js?v=3.21"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <br />
    <%--<asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1">
        <ProgressTemplate>
            <div class="divWaiting">
                <div class="loading">
                    <div class="loading-bar"></div>
                    <div class="loading-bar"></div>
                    <div class="loading-bar"></div>
                    <div class="loading-bar"></div>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <uc3:Message ID="MessageSuccess" runat="server" />
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Claim Application List
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:GridView ID="gvClaimApplicationList" runat="server" AllowPaging="True" PageSize="20"
                            AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                            GridLines="None" Style="text-align: left" OnPageIndexChanging="gvClaimApplicationList_PageIndexChanging"
                            OnRowCommand="gvClaimApplicationList_RowCommand">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SN.
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="ClaimNo" HeaderText="Application Number" />
                                <asp:BoundField DataField="PeriodFrom" HeaderText="Period From" />
                                <asp:BoundField DataField="PeriodTo" HeaderText="Period To" />
                                <asp:BoundField DataField="ClaimHeading" HeaderText="Claim Heading" />
                                <asp:BoundField DataField="TotalAmount" HeaderText="Total Amount" />
                                <asp:BoundField DataField="StatusName" HeaderText="Status" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnView" runat="server" Text="View" CommandName="View" CommandArgument='<%# Eval("ClaimId") %>' CssClass="btn btn-outline btn-info" Style="margin: 2px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#5bb0de" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5bc0de" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle CssClass="RowStyle" BackColor="#F7F6F3" ForeColor="#333333" />
                            <EditRowStyle BackColor="#999999" />
                            <EmptyDataRowStyle CssClass="EditRowStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle" BackColor="White" ForeColor="#284775" />
                            <PagerStyle CssClass="PagerStyle" BackColor="#379ed6" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <EmptyDataTemplate>
                                No Claim Application found...
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <a id="lnkClaim" runat="server"></a>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" BackgroundCssClass="myModalPopupbackGrnd"
        runat="server" TargetControlID="lnkClaim" PopupControlID="Panel1" CancelControlID="imgbtn">
        <Animations>
                 <OnShown><Fadein Duration="0.50" /></OnShown>
        </Animations>
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" CssClass="myModalPopup-20" Style="display: none; z-index: 10000; position: absolute">
        <asp:Panel ID="dragHandler" runat="server" class="popup-working-section" ScrollBars="Auto">
            <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" CssClass="MyTabStyle"
                ActiveTabIndex="1">
                <asp:TabPanel ID="Approval" runat="server">
                    <HeaderTemplate>
                        Claim Approval 
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div class="accountInfo" style="width: 100%; float: left">
                            <br />
                            <fieldset class="login">
                                <uc3:Message ID="Message" runat="server" />
                                <table class="popup-table">
                                    <tr>
                                        <td style="font-weight: bold">Name
                                        </td>
                                        <td>
                                            <asp:Label ID="lblName" runat="server"></asp:Label>
                                        </td>
                                        <td style="font-weight: bold">Claim No
                                        </td>
                                        <td>
                                            <asp:Label ID="lblClaimApplicationNumber" runat="server"></asp:Label>
                                        </td>
                                        <td style="font-weight: bold">Period From
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFromDate" runat="server"></asp:Label>
                                        </td>
                                        <td style="font-weight: bold">Period To
                                        </td>
                                        <td>
                                            <asp:Label ID="lblToDate" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold">
                                            Advance Adjust of <span class="pull-right">&#8377;</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblAdvanceAdjust" runat="server"></asp:Label>
                                        </td>
                                        <td style="font-weight: bold">
                                            Claim Heading
                                        </td>
                                        <td colspan="5">
                                            <asp:Label ID="lblClaimHeader" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8">
                                            <div style="height: 35vh; overflow: scroll">
                                                <div class="panel-body">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="gvClaimDetails" runat="server" OnRowCommand="gvClaimDetails_RowCommand"
                                                            AutoGenerateColumns="False" Width="100%" OnRowDataBound="gvClaimDetails_RowDataBound"
                                                            CellPadding="4" ForeColor="#333333" GridLines="None" Style="text-align: left">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        SN.
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <%# Container.DataItemIndex+1 %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Date
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <%# Convert.ToDateTime(Eval("ExpenseDate").ToString()).ToString("dd MMM yyyy") %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Category" DataField="CategoryName" />
                                                                <asp:BoundField HeaderText="Description" DataField="Description" />
                                                                <asp:BoundField HeaderText="Amount" DataField="Cost" />
                                                                <asp:BoundField HeaderText="Approved Amount" DataField="ApprovedAmount" />
                                                                <asp:BoundField HeaderText="Approver Remarks" DataField="ApproverRemarks" />
                                                                <asp:BoundField HeaderText="Status" DataField="StatusName" />
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkBtnAttachment" runat="server" Style="font-size: 16px;" class="fa fa-paperclip fa-fw" CommandName="A" CommandArgument='<%# Eval("Attachment") %>' />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle BackColor="#5bb0de" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#379ed6" Font-Bold="True" ForeColor="White" />
                                                            <RowStyle CssClass="RowStyle" BackColor="#F7F6F3" ForeColor="#333333" />
                                                            <EditRowStyle BackColor="#999999" />
                                                            <EmptyDataRowStyle CssClass="EditRowStyle" />
                                                            <AlternatingRowStyle CssClass="AltRowStyle" BackColor="White" ForeColor="#284775" />
                                                            <PagerStyle CssClass="PagerStyle" BackColor="#379ed6" ForeColor="White" HorizontalAlign="Center" />
                                                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                                            <EmptyDataTemplate>
                                                                No Record Found...
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold">Total Claim Amount <span class="pull-right">&#8377;</span>
                                        </td>
                                        <td colspan="7" style="font-weight: bold">
                                            <asp:Label ID="lblTotalClaimCount" runat="server" class="pull-right"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold">Total Approved Amount <span class="pull-right">&#8377;</span>
                                        </td>
                                        <td colspan="7" style="font-weight: bold">
                                            <asp:Label ID="lblTotalApprovedAmount" runat="server" class="pull-right" Text="0.00"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="ApprovalHistory" runat="server">
                    <HeaderTemplate>
                        Approval Details
                    </HeaderTemplate>
                    <ContentTemplate>
                        <br />
                        <div class="panel-body">
                            <div class="table-responsive">
                                <asp:GridView ID="gvApprovalHistory" runat="server"
                                    AutoGenerateColumns="False" Width="100%"
                                    CellPadding="4" ForeColor="#333333" GridLines="None" Style="text-align: left">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                SN.
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Approver" DataField="ApproverName" />
                                        <asp:BoundField HeaderText="Date" DataField="ActionDate" />
                                        <asp:BoundField HeaderText="Status" DataField="StatusName" />
                                    </Columns>
                                    <FooterStyle BackColor="#5bb0de" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#379ed6" Font-Bold="True" ForeColor="White" />
                                    <RowStyle CssClass="RowStyle" BackColor="#F7F6F3" ForeColor="#333333" />
                                    <EditRowStyle BackColor="#999999" />
                                    <EmptyDataRowStyle CssClass="EditRowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" BackColor="White" ForeColor="#284775" />
                                    <PagerStyle CssClass="PagerStyle" BackColor="#379ed6" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <EmptyDataTemplate>
                                        No Record Found...
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
            </asp:TabContainer>
        </asp:Panel>
        <img id="imgbtn" runat="server" src="../images/close-button.png" alt="Close" class="popup-close" />
    </asp:Panel>
    <%--</ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
