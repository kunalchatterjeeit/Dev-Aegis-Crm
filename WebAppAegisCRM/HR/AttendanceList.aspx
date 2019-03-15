<%@ Page Title="ATTENDANCE LIST" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="AttendanceList.aspx.cs" Inherits="WebAppAegisCRM.HR.AttendanceList" %>

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
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvAttendanceList" runat="server" AllowPaging="True" PageSize="20"
                                        AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                                        GridLines="None" Style="text-align: left" OnPageIndexChanging="gvAttendanceList_PageIndexChanging"
                                        OnRowCommand="gvAttendanceList_RowCommand">
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
                                            <asp:BoundField DataField="AttendanceDate" HeaderText="Attendance Date" />
                                            <asp:BoundField DataField="InDateTime" HeaderText="In Date&Time" />
                                            <asp:BoundField DataField="OutDateTime" HeaderText="Out Date&Time" />
                                            <asp:TemplateField>
                                                <HeaderTemplate>Working Time</HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# (Eval("TotalHours") != DBNull.Value)? TimeSpan.FromMinutes(Convert.ToDouble(Eval("TotalHours").ToString())).ToString(@"dd\:hh\:mm"):"" %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnInLocationView" runat="server" Text="In GPS Location" CommandName="InLocationView" CommandArgument='<%# Eval("AttendanceId") %>' CssClass="btn btn-outline btn-info" Style="margin: 2px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnOutLocationView" runat="server" Text="Out GPS Location" CommandName="OutLocationView" CommandArgument='<%# Eval("AttendanceId") %>' CssClass="btn btn-outline btn-info" Style="margin: 2px" />
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
            <a id="lnkLeave" runat="server"></a>
            <asp:ModalPopupExtender ID="ModalPopupExtender1" BackgroundCssClass="myModalPopupbackGrnd"
                runat="server" TargetControlID="lnkLeave" PopupControlID="Panel1" CancelControlID="imgbtn">
            </asp:ModalPopupExtender>
            <asp:Panel ID="Panel1" runat="server" CssClass="myModalPopup-8" Style="display: none; z-index: 10000; position: absolute">
                <asp:Panel ID="dragHandler" runat="server" class="popup-working-section" ScrollBars="Auto">
                    <div class="accountInfo" style="width: 100%; float: left">
                        <br />
                        <fieldset class="login">
                        </fieldset>
                    </div>
                </asp:Panel>
                <img id="imgbtn" runat="server" src="../images/close-button.png" alt="Close" class="popup-close" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
