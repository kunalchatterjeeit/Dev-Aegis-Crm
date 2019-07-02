<%@ Page Title="MANAGE ATTENDANCE" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ManageAttendance.aspx.cs" Inherits="WebAppAegisCRM.HR.ManageAttendance" %>
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
                <div class="col-lg-12">
                    
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Attendance List
                        </div>

                        <div class="col-lg-3">
                            <div class="form-group">
                                Employee Name :
                        <asp:TextBox ID="txtEmployeeName" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                From Docket Date :
                        <asp:TextBox ID="txtFromAttendanceDate" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromAttendanceDate"
                                    Format="dd MMM yyyy" Enabled="True">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-lg-3">
                            <div class="form-group">
                                To Docket Date :
                        <asp:TextBox ID="txtToAttendanceDate" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToAttendanceDate"
                                    Format="dd MMM yyyy" Enabled="True">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <br />
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-outline btn-success" OnClick="btnSearch_Click" />
                        </div>
                        <div class="clearfix"></div>
                         <div class="col-lg-12">
                            <uc3:Message ID="MessageBox" runat="server" />
                        </div>
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvAttendanceList" runat="server" AllowPaging="True" PageSize="20"
                                        AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                                        GridLines="None" Style="text-align: left" OnPageIndexChanging="gvAttendanceList_PageIndexChanging"
                                        OnRowCommand="gvAttendanceList_RowCommand" AllowCustomPaging="true" OnRowDataBound="gvAttendanceList_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    SN.
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%#  (gvAttendanceList.PageIndex * gvAttendanceList.PageSize) + (Container.DataItemIndex + 1) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EmployeeName" HeaderText="Name" />
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Attendance Date
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# (Eval("AttendanceDate") == DBNull.Value)? "": Convert.ToDateTime(Eval("AttendanceDate").ToString()).ToString("dd/MM/yy") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    In Date&Time
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# (Eval("InDateTimeRaw") == DBNull.Value)? "": Convert.ToDateTime(Eval("InDateTimeRaw").ToString()).ToString("dd/MM/yy hh:mm tt") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Out Date&Time
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# (Eval("OutDateTimeRaw") == DBNull.Value)? "": Convert.ToDateTime(Eval("OutDateTimeRaw").ToString()).ToString("dd/MM/yy hh:mm tt") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>Working Time</HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# (Eval("TotalHours") != DBNull.Value)? TimeSpan.FromMinutes(Convert.ToDouble(Eval("TotalHours").ToString())).ToString(@"dd\:hh\:mm"):"" %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>In Location</HeaderTemplate>
                                                <ItemTemplate>
                                                    <a target="_blank" title="Click to open map" href='<%# string.Concat("https://www.google.co.in/search?q=",Eval("InLocation").ToString()) %>'><%# (string.IsNullOrEmpty(Eval("InLocation").ToString()))?"":"Show" %></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>Out Location</HeaderTemplate>
                                                <ItemTemplate>
                                                    <a target="_blank" title="Click to open map" href='<%# string.Concat("https://www.google.co.in/search?q=",Eval("OutLocation").ToString()) %>'><%# (string.IsNullOrEmpty(Eval("OutLocation").ToString()))?"":"Show" %></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Source" HeaderText="Source" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnMarkLate" runat="server" Text="Mark Late" ToolTip="Click to mark as late" CommandName="MarkLate" CommandArgument='<%# Eval("AttendanceId") %>' CssClass="btn btn-outline btn-danger" Style="margin: 2px" />
                                                    <asp:HiddenField ID="hdnMarkLate" runat="server" Value='<%# Eval("IsLate").ToString() %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnMarkHalfDay" runat="server" Text="Mark Half-day" ToolTip="Click to mark as half-day" CommandName="MarkHalfDay" CommandArgument='<%# Eval("AttendanceId") %>' CssClass="btn btn-outline btn-danger" Style="margin: 2px" />
                                                    <asp:HiddenField ID="hdnMarkHalfday" runat="server" Value='<%# Eval("IsHalfday").ToString() %>' />
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
