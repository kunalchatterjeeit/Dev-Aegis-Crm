<%@ Page Title="EMPLOYEE WORK SUMMARY REPORT" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="EmployeeWorkSummaryReport.aspx.cs" Inherits="WebAppAegisCRM.HR.EmployeeWorkSummaryReport" %>

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
            <div class="row">
                <div class="col-lg-12">

                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Employee Work Summary List
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                From Date :
                        <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate"
                                    Format="dd MMM yyyy" Enabled="True">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                To Date :
                        <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate"
                                    Format="dd MMM yyyy" Enabled="True">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <br />
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-outline btn-success" OnClick="btnSearch_Click" />
                        </div>
                        <div class="clearfix"></div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvEmployeeWorkReport" runat="server" AllowPaging="false"
                                        AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                                        GridLines="None" Style="text-align: left">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    SN.
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Name" />
                                            <asp:BoundField DataField="CL" HeaderText="CL" />
                                            <asp:BoundField DataField="PL" HeaderText="PL" />
                                            <asp:BoundField DataField="LWP" HeaderText="LWP" />
                                            <asp:BoundField DataField="Late" HeaderText="Late" />
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
                                            No Attendance record found...
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
