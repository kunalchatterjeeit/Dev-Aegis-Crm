<%@ Page Title="LEAVE APPROVE" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="LeaveApprove.aspx.cs" Inherits="WebAppAegisCRM.LeaveManagement.LeaveApprove" %>

<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <br />
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1">
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
        <ContentTemplate>
            <uc3:Message ID="MessageSuccess" runat="server" />
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Leave Approval List
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <asp:GridView ID="gvLeaveApprovalList" runat="server"
                                    AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                                    GridLines="None" Style="text-align: left" OnRowCommand="gvLeaveApprovalList_RowCommand">
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
                                        <asp:BoundField DataField="Requestor" HeaderText="Requestor" />
                                        <asp:BoundField DataField="LeaveTypeName" HeaderText="Leave Type" />
                                        <asp:BoundField DataField="FromDate" HeaderText="From" />
                                        <asp:BoundField DataField="ToDate" HeaderText="To" />
                                        <asp:BoundField DataField="LeaveAccumulationTypeName" HeaderText="Accumulation Type" />
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
                                        No Pending Approval...
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
            </asp:ModalPopupExtender>
            <asp:Panel ID="Panel1" runat="server" CssClass="myModalPopup-8" Style="display: none; z-index: 10000; position: absolute">
                <asp:Panel ID="dragHandler" runat="server" class="popup-working-section" ScrollBars="Auto">
                    <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" CssClass="MyTabStyle"
                        ActiveTabIndex="1">
                        <asp:TabPanel ID="Approval" runat="server">
                            <HeaderTemplate>
                                Leave Approval 
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
                                                <td></td>  
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
                                                                        <asp:BoundField HeaderText="Date" DataField="Date" />
                                                                        <asp:BoundField HeaderText="Day" DataField="Day" />
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
                                                <td style="font-weight: bold">Remarks
                                                </td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Style="width: 100%" Rows="5"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn btn-outline btn-success pull-left" OnClick="btnApprove_Click" />
                                                </td>
                                                <td colspan="2">
                                                    <asp:Button ID="btnReject" runat="server" Text="Reject" CssClass="btn btn-outline btn-warning pull-right" OnClick="btnReject_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel ID="ApprovalHistory" runat="server">
                            <HeaderTemplate>
                                Approval History
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
