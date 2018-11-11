<%@ Page Title="AEGIS CRM" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Dashboard.aspx.cs" Inherits="WebAppAegisCRM.Dashboard" EnableEventValidation="false" %>

<%@ Import Namespace="Business.Common" %>
<%@ Import Namespace="Entity.Common" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
    <style type="text/css">
        .over img {
            margin: 0;
            background: yellow;
            position: absolute;
            top: 50%;
            left: 50%;
            margin-right: -50%;
            transform: translate(-50%, -50%);
        }
    </style>
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
    <script type="text/javascript">
        function EndGetDocketData(arg) {
            debugger;
            document.getElementById("gvDocketDiv").innerHTML = $.parseJSON(arg).DocketList;
            document.getElementById("gvTonerDiv").innerHTML = $.parseJSON(arg).TonerList;
            document.getElementById("gvExpiringSoonDiv").innerHTML = $.parseJSON(arg).ExpiringSoonList;
            document.getElementById("gvExpiredDiv").innerHTML = $.parseJSON(arg).ExpiredList;
        }
        setTimeout("<asp:literal runat='server' id='ltCallback' />", 100);
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
                        <div style="min-height: 30vh;">
                            <div class="table-responsive" id="gvDocketDiv">
                                <div class="over" style="position: absolute; width: 100%; height: 100%">
                                    <img src="images/gridLoader.gif" style="position: absolute; height: 50%" />
                                </div>
                                <asp:GridView ID="gvDocketAsync" DataKeyNames="DocketId" runat="server" RowStyle-Font-Size="9px"
                                    AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                                    class="table table-striped" GridLines="None" Style="text-align: left"
                                    OnRowDataBound="gvDocketAsync_RowDataBound" OnPageIndexChanging="gvDocketAsync_PageIndexChanging"
                                    AllowPaging="true" AllowCustomPaging="true" PageSize="6">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                SN.
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%#  (gvDocketAsync.PageIndex * gvDocketAsync.PageSize) + (Container.DataItemIndex + 1) %>
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
                                                <%--<span id="anchorCallOut" runat="server" title='<%# string.Concat("CALL OUT TIME WILL BE: ", DateTime.Now.ToShortTimeString()) %>'>
                                                    <a href='Service/ServiceBook.aspx?callid=<%# Eval("DocketId").ToString().EncryptQueryString() %>&calltype=<%# (int)Entity.Service.CallType.Docket %>&action=callout'>
                                                        <img src="images/outtime_icon.png" width="13px" alt="GO" />
                                                    </a>
                                                </span>--%>
                                                <span id="anchorCallOut" runat="server" title='CALL OUT IS ON SERVICE BOOK SUBMITION'>
                                                    <img src="images/outtime_icon.png" width="13px" alt="GO" />
                                                </span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <span id="anchorDocket" runat="server" title='<%# Eval("AssignedEngineerName").ToString() + " | "+ Eval("CallStatus").ToString() %>'><a href='Service/ServiceBook.aspx?callid=<%# Eval("DocketId").ToString().EncryptQueryString() %>&calltype=<%# (int)Entity.Service.CallType.Docket %>'>
                                                    <img src="images/go_icon.gif" width="13px" alt="GO" /></a></span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#99CCFF" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#379ed6" Font-Bold="True" ForeColor="White" />
                                    <RowStyle CssClass="RowStyle" BackColor="#F7F6F3" ForeColor="#333333" />
                                    <EditRowStyle BackColor="#999999" />
                                    <EmptyDataRowStyle CssClass="EditRowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" BackColor="White" ForeColor="#284775" />
                                    <PagerStyle CssClass="PagerStyle" BackColor="#379ED6" ForeColor="White" HorizontalAlign="Center" Font-Overline="False" Font-Underline="False" />
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
        <div class="col-lg-6">
            <div class="panel panel-warning">
                <div class="panel-heading">
                    Toner Requests 
                </div>
                <div class="row">
                    <div class="col-lg-12">
                        <div style="min-height: 30vh;">
                            <div class="table-responsive" id="gvTonerDiv">
                                <div class="over" style="position: absolute; width: 100%; height: 100%">
                                    <img src="images/gridLoader.gif" style="position: absolute; height: 50%" />
                                </div>
                                <asp:GridView ID="gvTonnerRequestAsync" DataKeyNames="TonnerRequestId" runat="server"
                                    RowStyle-Font-Size="9px" AutoGenerateColumns="False" Width="100%" CellPadding="4"
                                    ForeColor="#333333" class="table table-striped" GridLines="None" Style="text-align: left"
                                    OnRowDataBound="gvTonnerRequestAsync_RowDataBound" OnPageIndexChanging="gvTonnerRequestAsync_PageIndexChanging"
                                    AllowPaging="true" AllowCustomPaging="true" PageSize="6">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                SN.
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%#  (gvTonnerRequestAsync.PageIndex * gvTonnerRequestAsync.PageSize) + (Container.DataItemIndex + 1) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Date" DataField="ShortRequestDate" />
                                        <asp:BoundField HeaderText="Name" DataField="CustomerName" />
                                        <asp:BoundField HeaderText="Model" DataField="ProductName" />
                                        <asp:BoundField HeaderText="CP" DataField="ContactPerson" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <span id="anchorToner" runat="server" title='<%# Eval("CallStatus").ToString() %>'><a href='Service/ServiceBook.aspx?callid=<%# Eval("TonnerRequestId").ToString().EncryptQueryString() %>&calltype=<%# (int)Entity.Service.CallType.Toner %>'>
                                                    <img src="images/go_icon.gif" width="13px" /></a></span>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#99CCFF" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#379ed6" Font-Bold="True" ForeColor="White" />
                                    <RowStyle CssClass="RowStyle" BackColor="#F7F6F3" ForeColor="#333333" />
                                    <EditRowStyle BackColor="#999999" />
                                    <EmptyDataRowStyle CssClass="EditRowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" BackColor="White" ForeColor="#284775" />
                                    <PagerStyle CssClass="PagerStyle" BackColor="#379ED6" ForeColor="White" HorizontalAlign="Center" />
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
                        <div class="table-responsive" id="gvExpiringSoonDiv" style="min-height: 30vh;">
                            <div class="over" style="position: absolute; width: 100%; height: 100%; min-height: 20vh;">
                                <img src="images/gridLoader.gif" style="position: absolute; height: 50%" />
                            </div>
                            <asp:GridView ID="gvExpiringSoonAsync" runat="server" DataKeyNames="CustomerId,ContractId"
                                RowStyle-Font-Size="9px" AutoGenerateColumns="False" Width="100%" CellPadding="4"
                                ForeColor="#333333" class="table table-striped" GridLines="None" Style="text-align: left"
                                OnPageIndexChanging="gvExpiringSoonAsync_PageIndexChanging" PageSize="18" AllowPaging="true" AllowCustomPaging="true">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            SN.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#  (gvExpiringSoonAsync.PageIndex * gvExpiringSoonAsync.PageSize) + (Container.DataItemIndex + 1) %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Expire on">
                                        <ItemTemplate>
                                            <%# Convert.ToDateTime(Eval("ContractEndDate")).ToString("dd/MM/yy") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
                        <div class="table-responsive" id="gvExpiredDiv" style="min-height: 30vh;">
                            <div class="over" style="position: absolute; width: 100%; height: 100%; min-height: 20vh;">
                                <img src="images/gridLoader.gif" style="position: absolute; height: 50%" />
                            </div>
                            <asp:GridView ID="gvExpiredListAsync" runat="server" DataKeyNames="CustomerId,ContractId"
                                RowStyle-Font-Size="9px" AutoGenerateColumns="False" Width="100%" CellPadding="4"
                                ForeColor="#333333" class="table table-striped" GridLines="None" Style="text-align: left"
                                OnPageIndexChanging="gvExpiredListAsync_PageIndexChanging" PageSize="18" AllowPaging="true" AllowCustomPaging="true">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            SN.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%#  (gvExpiredListAsync.PageIndex * gvExpiredListAsync.PageSize) + (Container.DataItemIndex + 1) %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Expired on">
                                        <ItemTemplate>
                                            <%# Convert.ToDateTime(Eval("ContractEndDate")).ToString("dd/MM/yy") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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
