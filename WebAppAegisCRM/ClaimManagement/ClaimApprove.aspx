<%@ Page Title="CLAIM APPROVE/REJECT" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ClaimApprove.aspx.cs" Inherits="WebAppAegisCRM.ClaimManagement.ClaimApprove" %>

<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <br />
    <uc3:Message ID="MessageSuccess" runat="server" />
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Claim Search Criteria
                </div>
                <div class="panel-body">
                    <div class="col-lg-3">
                        <div class="form-group">
                            Claim Application From Date
                            <asp:TextBox ID="txtFromClaimDate" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True"
                                Format="dd MMM yyyy" TargetControlID="txtFromClaimDate">
                            </asp:CalendarExtender>
                        </div>
                    </div>
                    <div class="col-lg-3">
                        <div class="form-group">
                            Claim Application To Date
                            <asp:TextBox ID="txtToClaimDate" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                Format="dd MMM yyyy" TargetControlID="txtToClaimDate">
                            </asp:CalendarExtender>
                        </div>
                    </div>
                    <div class="col-lg-2">
                        <div class="form-group has-error">
                            <div class="checkbox">
                                <label class="btn btn-warning">
                                    <asp:CheckBox ID="ckShowAll" runat="server" Text="Show All" />
                                </label>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-1">
                        <div class="form-group">
                            <br />
                            <asp:Button ID="btnSearch" runat="server" Text="Search" class="btn btn-outline btn-success" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Claim Approval List
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:GridView ID="gvClaimApprovalList" runat="server"
                            AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                            GridLines="None" Style="text-align: left" OnRowCommand="gvClaimApprovalList_RowCommand">
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
                                <asp:BoundField DataField="Requestor" HeaderText="Requestor" />
                                <asp:BoundField DataField="FromDate" HeaderText="Period From" />
                                <asp:BoundField DataField="ToDate" HeaderText="Period To" />
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
                                No Pending Approval...
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
                                        <td colspan="8">
                                            <div style="height: 40vh; overflow: scroll">
                                                <div class="panel-body">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="gvClaimDetails" runat="server"
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
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Approve Amount
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtApprovedAmount" runat="server" Text='<%# (Eval("ApprovedAmount")!=DBNull.Value && Convert.ToDecimal(Eval("ApprovedAmount"))>0)? Eval("ApprovedAmount").ToString() :Eval("Cost").ToString() %>'></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Remarks
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtApprovedRemarks" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Status
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:DropDownList ID="ddlLineItemStatus" runat="server"></asp:DropDownList>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkBtnAttachment" runat="server" style="font-size: 16px;" class="fa fa-paperclip fa-fw" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField ItemStyle-Width="15px">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="btnUpdate" runat="server" class="fa fa-save fa-fw" CommandName="E" CausesValidation="false"
                                                                             CommandArgument='<%# Eval("ClaimDetailsId") %>' Style="font-size: 16px; margin-top: 7px" ToolTip="Save changes"></asp:LinkButton>
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
                                        <td style="font-weight: bold">Total Claim Amount
                                        </td>
                                        <td colspan="7" style="font-weight: bold">
                                            <asp:Label ID="lblTotalClaimCount" runat="server" class="pull-right"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold">Remarks
                                        </td>
                                        <td colspan="7">
                                            <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" Style="width: 100%" Rows="1"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="btn btn-outline btn-success pull-left" OnClick="btnApprove_Click" />
                                        </td>
                                        <td colspan="4">
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
                                        <asp:BoundField HeaderText="Status" DataField="ClaimStatusName" />
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
</asp:Content>
