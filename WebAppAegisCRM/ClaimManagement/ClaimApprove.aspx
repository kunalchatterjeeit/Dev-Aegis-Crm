<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ClaimApprove.aspx.cs" Inherits="WebAppAegisCRM.ClaimManagement.ClaimApprove" %>

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
                                <asp:BoundField DataField="ClaimApplicationNumber" HeaderText="Application Number" />
                                <asp:BoundField DataField="Requestor" HeaderText="Requestor" />
                                <asp:BoundField DataField="ClaimTypeName" HeaderText="Claim Type" />
                                <asp:BoundField DataField="FromDate" HeaderText="From" />
                                <asp:BoundField DataField="ToDate" HeaderText="To" />
                                <asp:BoundField DataField="ClaimStatusName" HeaderText="Status" />
                                <asp:BoundField DataField="ClaimAccumulationTypeName" HeaderText="Accumulation Type" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnView" runat="server" Text="View" CommandName="View" CommandArgument='<%# Eval("ClaimApplicationId") %>' CssClass="btn btn-outline btn-info" Style="margin: 2px" />
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
</asp:Content>
