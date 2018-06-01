<%@ Page Title="TONER APPROVAL LIST" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="TonerApproval.aspx.cs" Inherits="WebAppAegisCRM.Service.TonerApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
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
            <div class="row">
                <div class="col-lg-10">
                    <uc3:Message ID="Message" runat="server"></uc3:Message>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Approval List
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        Machine Id:
                                        <asp:TextBox ID="txtMachineId" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        Log From Date:
                                        <asp:TextBox ID="txtLogFromDate" CssClass="form-control" runat="server" TextMode="DateTime"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtLogFromDate"
                                            Format="dd MMM yyyy" Enabled="True">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        Log To Date:
                                        <asp:TextBox ID="txtLogToDate" CssClass="form-control" runat="server" TextMode="DateTime"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtLogToDate"
                                            Format="dd MMM yyyy" Enabled="True">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        Approval Status:
                                        <asp:DropDownList ID="ddlApprovalStatus" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="Pending" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Approved" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Rejected" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <br />
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-outline btn-success" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <div style="height: 50vh; overflow: scroll">
                                        <asp:GridView ID="gvApproval" DataKeyNames="ApprovalId" runat="server"
                                            AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                                            class="table table-striped" GridLines="None" Style="text-align: left" OnRowCommand="gvApproval_RowCommand">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        SN.
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Machine" DataField="MachineId" />
                                                <asp:BoundField HeaderText="Toner" DataField="RequestNo" />
                                                <asp:BoundField HeaderText="Spare" DataField="SpareName" />
                                                <asp:BoundField HeaderText="Yield" DataField="Yield" />
                                                <asp:BoundField HeaderText="Req Qty" DataField="RequisiteQty" />
                                                <asp:BoundField HeaderText="A3BW" DataField="A3BWMeterReading" />
                                                <asp:BoundField HeaderText="A3CL" DataField="A3CLMeterReading" />
                                                <asp:BoundField HeaderText="A4BW" DataField="A4BWMeterReading" />
                                                <asp:BoundField HeaderText="A4CL" DataField="A4CLMeterReading" />
                                                <asp:BoundField HeaderText="Log Date" DataField="RequestDateTime" />
                                                <asp:TemplateField HeaderText="Approval Status">
                                                    <ItemTemplate>
                                                        <%# (Eval("ApprovalStatus").ToString()=="2")?"Rejected":(Eval("ApprovalStatus").ToString()=="1")?"Approved":"Pending" %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Comment" DataField="Comment" />
                                                <asp:TemplateField HeaderText="New Comment">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtComment" CssClass="form-control" runat="server" TextMode="MultiLine" Columns="8" Rows="1"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnApprove" runat="server" CausesValidation="false" CommandName="Approve"
                                                        CommandArgument='<%# Eval("ApprovalId") %>' ImageUrl="~/images/Thumbs_up-icon.png"
                                                        ImageAlign="AbsMiddle" ToolTip="Approve" Width="20px" Height="20px"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnReject" runat="server" CausesValidation="false" CommandName="Reject"
                                                        CommandArgument='<%# Eval("ApprovalId") %>' ImageUrl="~/images/Thumbs_down_icon.png"
                                                        ImageAlign="AbsMiddle" ToolTip="Reject" Width="20px" Height="20px"/>
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
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
