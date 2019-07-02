<%@ Page Title="MY LEAVE APPLICATION LIST" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="LeaveApplicationList.aspx.cs" Inherits="WebAppAegisCRM.LeaveManagement.LeaveApplicationList" %>

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
                    Leave Application List
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:GridView ID="gvLeaveApplicationList" runat="server" AllowPaging="True" PageSize="20"
                            AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                            GridLines="None" Style="text-align: left" OnPageIndexChanging="gvLeaveApplicationList_PageIndexChanging"
                            OnRowCommand="gvLeaveApplicationList_RowCommand">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SN.
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="LeaveApplicationNumber" HeaderText="Application Number" />
                                <asp:BoundField DataField="LeaveTypeName" HeaderText="Leave Type" />
                                <asp:BoundField DataField="FromDate" HeaderText="From" />
                                <asp:BoundField DataField="ToDate" HeaderText="To" />
                                <asp:BoundField DataField="LeaveAccumulationTypeName" HeaderText="Accumulation Type" />
                                <asp:BoundField DataField="LeaveStatusName" HeaderText="Status" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnView" runat="server" Text="View" CommandName="View" CommandArgument='<%# Eval("LeaveApplicationId") %>' CssClass="btn btn-outline btn-info" Style="margin: 2px" />
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
                                No Leave Application found...
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <a id="lnkLeave" runat="server"></a>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" BackgroundCssClass="myModalPopupbackGrnd"
        runat="server" TargetControlID="lnkLeave" PopupControlID="Panel1" CancelControlID="imgbtn">
        <Animations>
                 <OnShown><Fadein Duration="0.50" /></OnShown>
        </Animations>
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" CssClass="myModalPopup-8" Style="display: none; z-index: 10000; position: absolute">
        <asp:Panel ID="dragHandler" runat="server" class="popup-working-section" ScrollBars="Auto">
            <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" CssClass="MyTabStyle"
                ActiveTabIndex="1">
                <asp:TabPanel ID="Approval" runat="server">
                    <HeaderTemplate>
                        Leave Details 
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div class="accountInfo" style="width: 100%; float: left">
                            <br />
                            <fieldset class="login">
                                <uc3:Message ID="Message" runat="server" />
                                <table class="popup-table">
                                    <tr>
                                        <td style="font-weight: bold">Leave Application Number
                                        </td>
                                        <td>
                                            <asp:Label ID="lblLeaveApplicationNumber" runat="server"></asp:Label>
                                        </td>
                                        <td style="font-weight: bold">Name
                                        </td>
                                        <td>
                                            <asp:Label ID="lblName" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold">From Date
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFromDate" runat="server"></asp:Label>
                                        </td>
                                        <td style="font-weight: bold">To Date
                                        </td>
                                        <td>
                                            <asp:Label ID="lblToDate" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold">Leave Type
                                        </td>
                                        <td>
                                            <asp:Label ID="lblLeaveType" runat="server"></asp:Label>
                                        </td>
                                        <td style="font-weight: bold">Leave Accumulation Type
                                        </td>
                                        <td>
                                            <asp:Label ID="lblLeaveAccumulationType" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold">Total Leave Applied
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTotalLeaveCount" runat="server"></asp:Label>
                                        </td>
                                        <td style="font-weight: bold">Attachment
                                        </td>
                                        <td>
                                            <asp:HiddenField ID="hdnAttachmentName" runat="server" />
                                            <asp:LinkButton ID="lnkBtnAttachment" runat="server" OnClick="lnkBtnAttachment_Click">click to download</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold">Reason
                                        </td>
                                        <td colspan="2">
                                            <asp:Label ID="lblReason" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold">Leave Dates
                                        </td>
                                        <td colspan="3">
                                            <div style="height: 20vh; overflow: scroll">
                                                <div class="panel-body">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="gvDates" runat="server"
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
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Date
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <%# Convert.ToDateTime(Eval("LeaveDate").ToString()).ToString("dd MMM yyyy") %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Day
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <%# Convert.ToDateTime(Eval("LeaveDate").ToString()).ToString("dddd") %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Applied Day" DataField="AppliedForDay" />
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
                                        <td colspan="2">
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel Leave" CssClass="btn btn-outline btn-danger pull-left" OnClick="btnCancel_Click" />
                                        </td>
                                        <td colspan="2">
                                            <asp:Button ID="btnFollowup" runat="server" Text="Follow up" CssClass="btn btn-outline btn-warning pull-right" OnClick="btnFollowup_Click" />
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
                                        <asp:BoundField HeaderText="Status" DataField="LeaveStatusName" />
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
