<%@ Page Title="DOCKET LIST" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="DocketList.aspx.cs" Inherits="WebAppAegisCRM.Service.DocketList" %>

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
                            Docket List
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-3">
                                <div class="form-group">
                                    Docket No :
                                        <asp:TextBox ID="txtDocketNo" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="form-group">
                                    Customer :
                                    <asp:DropDownList ID="ddlCustomer" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="form-group">
                                    Model :
                                    <asp:DropDownList ID="ddlProduct" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="form-group">
                                    From Docket Date :
                                    <asp:TextBox ID="txtFromDocketDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDocketDate"
                                        Format="dd MMM yyyy" Enabled="True">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="form-group">
                                    To Docket Date :
                                    <asp:TextBox ID="txtToDocketDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDocketDate"
                                        Format="dd MMM yyyy" Enabled="True">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="form-group">
                                    Call Status :
                                    <asp:DropDownList ID="ddlCallStatus" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="form-group">
                                    Docket Type :
                                    <asp:DropDownList ID="ddlDocketType" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                        <asp:ListItem Text="CM" Value="CM"></asp:ListItem>
                                        <asp:ListItem Text="INSTALLATION" Value="INSTALLATION"></asp:ListItem>
                                        <asp:ListItem Text="OTHERS" Value="OTHERS"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <br />
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-outline btn-success" OnClientClick="return ValidationForSave();" OnClick="btnSubmit_Click" />
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvDocket" DataKeyNames="DocketId" runat="server" AutoGenerateColumns="False" 
                                            Width="100%" CellPadding="4" ForeColor="#333333" class="table table-striped" GridLines="None" 
                                            Style="text-align: left" OnPageIndexChanging="gvDocket_PageIndexChanging" PageSize="20" AllowPaging="true">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        SN.
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Docket No" DataField="DocketNo" />
                                                <asp:BoundField HeaderText="Date" DataField="DocketDate" />
                                                <asp:BoundField HeaderText="Time" DataField="DocketTime" />
                                                <asp:BoundField HeaderText="Model" DataField="ProductName" />
                                                <asp:BoundField HeaderText="Machine Id" DataField="MachineId" />
                                                <asp:BoundField HeaderText="Problem" DataField="Problem" ItemStyle-Width="20%" />
                                                <asp:BoundField HeaderText="Status" DataField="CallStatus" />
                                                <asp:BoundField HeaderText="CE" DataField="IsCustomerEntry" ItemStyle-HorizontalAlign="Center" />
                                            </Columns>
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#0349AA" Font-Bold="True" ForeColor="White" />
                                            <RowStyle CssClass="RowStyle" BackColor="#F7F6F3" ForeColor="#333333" />
                                            <EditRowStyle BackColor="#999999" />
                                            <EmptyDataRowStyle CssClass="EditRowStyle" />
                                            <AlternatingRowStyle CssClass="AltRowStyle" BackColor="White" ForeColor="#284775" />
                                            <PagerStyle CssClass="PagerStyle" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
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
