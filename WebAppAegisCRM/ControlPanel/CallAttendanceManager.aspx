<%@ Page Title="CALL ATTENDANCE MANAGER" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CallAttendanceManager.aspx.cs" Inherits="WebAppAegisCRM.ControlPanel.CallAttendanceManager" %>

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
                            Service Call Attendance List
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <uc3:Message ID="Message" runat="server" />
                                <div class="clearfix"></div>
                                <asp:GridView ID="gvCallAttendance" DataKeyNames="ServiceCallAttendanceId" runat="server"
                                    AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333" AllowPaging="True" PageSize="20"
                                    class="table table-striped" GridLines="None" Style="text-align: left" OnRowCommand="gvCallAttendance_RowCommand" OnRowDataBound="gvCallAttendance_RowDataBound" OnPageIndexChanging="gvCallAttendance_PageIndexChanging">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                SN.
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="SB No" DataField="ServiceBookId" />
                                        <asp:BoundField HeaderText="Docket No" DataField="DocketNo" />
                                        <asp:BoundField HeaderText="Engineer" DataField="EmployeeName" />
                                        <asp:BoundField HeaderText="Machine" DataField="MachineId" />
                                        <asp:BoundField HeaderText="Contact Person" DataField="ContactPerson" />
                                        <asp:TemplateField HeaderText="Call Status" ItemStyle-Width="110px">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlDocketCallStatus" CssClass="form-control" runat="server">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <HeaderStyle Width="25px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="In-Time" ItemStyle-Width="200px">
                                            <ItemTemplate>
                                                <div style="float: left;">
                                                    <asp:TextBox ID="txtInDate" runat="server" Style="border: none; background: #fff; width: 60px"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtInDate"
                                                        Format="dd MMM yyyy" Enabled="True">
                                                    </asp:CalendarExtender>
                                                    <asp:DropDownList ID="ddlInTimeHH" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlInTimeMM" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlInTimeTT" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Width="25px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Out-Time" ItemStyle-Width="200px">
                                            <ItemTemplate>
                                                <div style="float: left;">
                                                    <asp:TextBox ID="txtOutDate" runat="server" Style="border: none; background: #fff; width: 60px"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtOutDate"
                                                        Format="dd MMM yyyy" Enabled="True">
                                                    </asp:CalendarExtender>
                                                    <asp:DropDownList ID="ddlOutTimeHH" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlOutTimeMM" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="ddlOutTimeTT" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderStyle Width="25px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEdit" runat="server" CommandName="E" ImageUrl="~/images/edit_button.png"
                                                    CommandArgument='<%#Eval("ServiceCallAttendanceId") %>' Width="17px" Height="17px"
                                                    OnClientClick="return confirm('Are You Sure?');" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="25px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelete" runat="server" CommandName="D" ImageUrl="~/Images/delete_button.png"
                                                    CommandArgument='<%#Eval("ServiceCallAttendanceId") %>' Width="17px" Height="17px"
                                                    OnClientClick="return confirm('Are You Sure?');" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="25px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#5bb0de" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#379ed6" Font-Bold="True" ForeColor="White" />
                                    <RowStyle CssClass="RowStyle" BackColor="#F7F6F3" ForeColor="#333333" />
                                    <EditRowStyle BackColor="#999999" />
                                    <EmptyDataRowStyle CssClass="EditRowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" BackColor="#F7F6F3" ForeColor="#284775" />
                                    <PagerStyle CssClass="PagerStyle" BackColor="#379ed6" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <EmptyDataTemplate>
                                        No Attendance Found...
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
