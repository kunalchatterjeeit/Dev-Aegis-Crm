<%@ Page Title="AEGIS CRM" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Dashboard.aspx.cs" Inherits="WebAppAegisCRM.Dashboard" %>

<%@ Import Namespace="Business.Common" %>
<%@ Import Namespace="Entity.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Morris Charts CSS -->
    <link href="bower_components/morrisjs/morris.css" rel="stylesheet" />
    <!-- Timeline CSS -->
    <link href="dist/css/timeline.css" rel="stylesheet" />
    <!-- Flot Charts JavaScript -->
    <script type="text/javascript" src="bower_components/flot/excanvas.min.js"></script>
    <script type="text/javascript" src="bower_components/flot/jquery.flot.js"></script>
    <script type="text/javascript" src="bower_components/flot/jquery.flot.pie.js"></script>
    <script type="text/javascript" src="bower_components/flot/jquery.flot.resize.js"></script>
    <script type="text/javascript" src="bower_components/flot/jquery.flot.time.js"></script>
    <script type="text/javascript" src="bower_components/flot.tooltip/js/jquery.flot.tooltip.min.js"></script>
    <script type="text/javascript">
        //Flot Pie Chart
        function PieData(a, b, c, d) {
            $(document).ready(function () {

                var data = [{
                    label: "<a href='Service/ContractStatus.aspx?id=2'>Expiring Soon</a>",
                    data: a
                }, {
                    label: "<a href='Service/ContractStatus.aspx?id=4'>Never Contracted</a>",
                    data: d
                }, {
                    label: "<a href='Service/ContractStatus.aspx?id=3'>Expired</a>",
                    data: b
                }, {
                    label: "<a href='Service/ContractStatus.aspx?id=1'>Contract</a>",
                    data: c
                }];

                var plotObj = $.plot($("#flot-pie-chart"), data, {
                    series: {
                        pie: {
                            show: true
                        }
                    },
                    grid: {
                        hoverable: true
                    },
                    tooltip: true,
                    tooltipOpts: {
                        content: "%p.0%, %s", // show percentages, rounding to 2 decimal places
                        shifts: {
                            x: 20,
                            y: 0
                        },
                        defaultTheme: false
                    }
                });

            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="row">
        <div class="col-lg-6">
            <div class="panel panel-danger">
                <div class="panel-heading">
                    Dockets
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="table-responsive">
                            <asp:GridView ID="gvDocket" DataKeyNames="DocketId" runat="server" RowStyle-Font-Size="9px"
                                AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                                class="table table-striped" GridLines="None" Style="text-align: left"
                                OnPageIndexChanging="gvDocket_PageIndexChanging" PageSize="5" AllowPaging="true"
                                OnRowDataBound="gvDocket_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            SN.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Date" DataField="ShortDocketDate" />
                                    <asp:BoundField HeaderText="Name" DataField="CustomerName" />
                                    <asp:BoundField HeaderText="Model" DataField="ProductName" />
                                    <asp:BoundField HeaderText="CP" DataField="ContactPerson" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <span id="anchorCallIn" runat="server" title='<%# string.Concat("CALL ATTEND TIME WILL BE: ", DateTime.Now.ToShortTimeString()) %>'>
                                                <a href='Service/ServiceBook.aspx?callid=<%# Eval("DocketId").ToString().EncryptQueryString() %>&calltype=<%# (int)Entity.Service.CallType.Docket %>&action=callin'>
                                                    <img src="images/intime_icon.png" width="13px" alt="GO" />
                                                </a>
                                            </span>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <span id="anchorDocket" runat="server" title='<%# Eval("AssignedEngineerName").ToString() %>'><a href='Service/ServiceBook.aspx?callid=<%# Eval("DocketId").ToString().EncryptQueryString() %>&calltype=<%# (int)Entity.Service.CallType.Docket %>'>
                                                <img src="images/go_icon.gif" width="13px" alt="GO" /></a></span>
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
        <div class="col-lg-6">
            <div class="panel panel-warning">
                <div class="panel-heading">
                    Toner Requests
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="table-responsive">
                            <asp:GridView ID="gvTonnerRequest" DataKeyNames="TonnerRequestId" runat="server"
                                RowStyle-Font-Size="9px" AutoGenerateColumns="False" Width="100%" CellPadding="4"
                                ForeColor="#333333" class="table table-striped" GridLines="None" Style="text-align: left"
                                OnPageIndexChanging="gvTonnerRequest_PageIndexChanging" PageSize="5" AllowPaging="true"
                                OnRowDataBound="gvTonnerRequest_RowDataBound">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            SN.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Date" DataField="ShortRequestDate" />
                                    <asp:BoundField HeaderText="Name" DataField="CustomerName" />
                                    <asp:BoundField HeaderText="Model" DataField="ProductName" />
                                    <asp:BoundField HeaderText="CP" DataField="ContactPerson" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <span id="anchorToner" runat="server"><a href='Service/ServiceBook.aspx?callid=<%# Eval("TonnerRequestId").ToString().EncryptQueryString() %>&calltype=<%# (int)Entity.Service.CallType.Toner %>'>
                                                <img src="images/go_icon.gif" width="13px" /></a></span>
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
    <div class="row">
        <div class="col-lg-6">
            <div class="panel panel-green">
                <div class="panel-heading">
                    Contract Status                      
                </div>
                <!-- /.panel-heading -->
                <div class="panel-body">
                    <div class="flot-chart">
                        <div class="flot-chart-content" id="flot-pie-chart"></div>
                    </div>
                </div>
                <!-- /.panel-body -->
            </div>
            <!-- /.panel -->
        </div>

        <div class="col-lg-3">
            <div class="panel panel-yellow">
                <div class="panel-heading">
                    Contracts Expiring Soon
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="table-responsive">
                            <asp:GridView ID="gvExpiringSoon" runat="server" DataKeyNames="CustomerId,ContractId"
                                RowStyle-Font-Size="9px" AutoGenerateColumns="False" Width="100%" CellPadding="4"
                                ForeColor="#333333" class="table table-striped" GridLines="None" Style="text-align: left"
                                OnPageIndexChanging="gvExpiringSoon_PageIndexChanging" PageSize="18" AllowPaging="true" AllowCustomPaging="true">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            SN.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#  (gvExpiringSoon.PageIndex * gvExpiringSoon.PageSize) + (Container.DataItemIndex + 1) %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Expire on" DataField="ContractEndDate" />
                                    <asp:BoundField HeaderText="Machine Id" DataField="MachineId" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <span id="anchorExpyring" runat="server" title='<%# Eval("CustomerName") %>'>
                                                <a target="_blank" href='Customer/CustomerPurchase.aspx?customerId=<%# Eval("CustomerId").ToString().EncryptQueryString() %>&source=dashboard&contractId=<%# Eval("ContractId").ToString().EncryptQueryString() %>'>
                                                    <img src="images/go_icon.gif" width="13px" alt="" />
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
        <div class="col-lg-3">
            <div class="panel panel-red">
                <div class="panel-heading">
                    Contracts Expired
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div class="table-responsive">
                            <asp:GridView ID="gvExpiredList" runat="server" DataKeyNames="CustomerId,ContractId"
                                RowStyle-Font-Size="9px" AutoGenerateColumns="False" Width="100%" CellPadding="4"
                                ForeColor="#333333" class="table table-striped" GridLines="None" Style="text-align: left"
                                OnPageIndexChanging="gvExpiredList_PageIndexChanging" PageSize="18" AllowPaging="true" AllowCustomPaging="true">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            SN.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#  (gvExpiredList.PageIndex * gvExpiredList.PageSize) + (Container.DataItemIndex + 1) %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Expired on" DataField="ContractEndDate" />
                                    <asp:BoundField HeaderText="Machine Id" DataField="MachineId" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <span id="anchorExpired" runat="server" title='<%# Eval("CustomerName") %>'>
                                                <a target="_blank" href='Customer/CustomerPurchase.aspx?customerId=<%# Eval("CustomerId").ToString().EncryptQueryString() %>&source=dashboard&contractId=<%# Eval("ContractId").ToString().EncryptQueryString() %>'>
                                                    <img src="images/go_icon.gif" width="13px" alt="" />
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
</asp:Content>
