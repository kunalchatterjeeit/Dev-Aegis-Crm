<%@ Page Title="CONTRACT STATUS REPORT" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ContractStatus.aspx.cs" Inherits="WebAppAegisCRM.Service.ContractStatus" %>

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
                            Contract Status Report
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Contract Status :
                                        <asp:DropDownList ID="ddlContractStatus" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="--SELECT--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="In Contract" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Expring Soon" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Expired" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Never Contracted" Value="4"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <br />
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-outline btn-success"
                                        OnClientClick="return Validate();" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" id="divDocket">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <asp:Label ID="lblListTitle" runat="server"></asp:Label>
                            List
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        Machine Id :
                                        <asp:TextBox ID="txtMachineId" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        From Date :
                                        <asp:TextBox ID="txtFromContractDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromContractDate"
                                            Format="dd MMM yyyy" Enabled="True">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        To Date :
                                        <asp:TextBox ID="txtToContractDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToContractDate"
                                            Format="dd MMM yyyy" Enabled="True">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <br />
                                    <asp:Button ID="btnMachineSearch" runat="server" Text="Search" CssClass="btn btn-outline btn-success"
                                        OnClientClick="return ValidationForSave();" OnClick="btnMachineSearch_Click" />
                                </div>
                                <div class="clearfix">
                                </div>
                                <div class="col-lg-12">
                                    <span class="red">* Expired contracts with positive value in Days Left indicates page print number limits are exceeded.</span>
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvContractStatusList" runat="server" RowStyle-Font-Size="12px" AutoGenerateColumns="False"
                                            Width="100%" CellPadding="4" ForeColor="#333333" class="table table-striped"
                                            GridLines="None" Style="text-align: left" OnPageIndexChanging="gvContractStatusList_PageIndexChanging"
                                            PageSize="15" AllowPaging="True" AllowCustomPaging="true">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        SN.
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#  (gvContractStatusList.PageIndex * gvContractStatusList.PageSize) + (Container.DataItemIndex + 1) %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Customer" DataField="CustomerName" />
                                                <asp:BoundField HeaderText="Machine Id" DataField="MachineId" />
                                                <asp:BoundField HeaderText="Contract Type" DataField="ContractName" />
                                                <asp:TemplateField HeaderText="Expire on">
                                                    <ItemTemplate>
                                                        <%# (Convert.ToDateTime(Eval("ContractEndDate")).ToString("yyyy").Equals("1900"))?"N/A":Convert.ToDateTime(Eval("ContractEndDate")).ToString("dd/MM/yyyy") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Days Left" DataField="DayLeft" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <span id="anchorContract" runat="server">
                                                            <a target="_blank" href='../Customer/CustomerPurchase.aspx?customerId=<%# Eval("CustomerId") %>&source=contractStatus&contractId=<%# Eval("ContractId") %>'>
                                                                <img src="../images/go_icon.gif" width="15px" alt="" />
                                                            </a>
                                                        </span>
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
