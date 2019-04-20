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
            if (document.getElementById("gvDocketDiv") != null)
                document.getElementById("gvDocketDiv").innerHTML = $.parseJSON(arg).DocketList;
            if (document.getElementById("gvTonerDiv") != null)
                document.getElementById("gvTonerDiv").innerHTML = $.parseJSON(arg).TonerList;
            if (document.getElementById("gvExpiringSoonDiv") != null)
                document.getElementById("gvExpiringSoonDiv").innerHTML = $.parseJSON(arg).ExpiringSoonList;
            if (document.getElementById("gvExpiredDiv") != null)
                document.getElementById("gvExpiredDiv").innerHTML = $.parseJSON(arg).ExpiredList;
        }
        setTimeout("<asp:literal runat='server' id='ltCallback' />", 100);
    </script>
    <script type="text/javascript">
        function showSuccessDivClose() {
            alert('Settings change will take effect from next login. To change user settings goto User Settings page.')
        }
    </script>
    <style type="text/css">
        .container {
            margin-top: 30px;
        }

        h1, h2, h3, h4, h5, h6 {
            font-family: 'Source Sans Pro';
            font-weight: 700;
        }

        .fancyTab {
            text-align: center;
            padding: 15px 0;
            background-color: #eee;
            box-shadow: 0 0 0 1px #ddd;
            top: 15px;
            transition: top .2s;
        }

            .fancyTab.active {
                top: 0;
                transition: top .2s;
            }

        .whiteBlock {
            display: none;
        }

        .fancyTab.active .whiteBlock {
            display: block;
            height: 2px;
            bottom: -2px;
            background-color: #fff;
            width: 99%;
            position: absolute;
            z-index: 1;
        }

        .fancyTab a {
            font-family: 'Source Sans Pro';
            font-size: 1.65em;
            font-weight: 300;
            transition: .2s;
            color: #333;
        }

        /*.fancyTab .hidden-xs {
  white-space:nowrap;
}*/

        .fancyTabs {
            border-bottom: 2px solid #ddd;
            margin: 15px 0 0;
        }

        li.fancyTab a {
            padding-top: 15px;
            top: -15px;
            padding-bottom: 0;
        }

        li.fancyTab.active a {
            padding-top: inherit;
        }

        .fancyTab .fa {
            font-size: 40px;
            width: 100%;
            padding: 15px 0 5px;
            color: #666;
        }

        .fancyTab.active .fa {
            color: #cfb87c;
        }

        .fancyTab a:focus {
            outline: none;
        }

        .fancyTabContent {
            border-color: transparent;
            box-shadow: 0 -2px 0 -1px #fff, 0 0 0 1px #ddd;
            padding: 30px 15px 15px;
            position: relative;
            background-color: #fff;
        }

        .nav-tabs > li.fancyTab.active > a,
        .nav-tabs > li.fancyTab.active > a:focus,
        .nav-tabs > li.fancyTab.active > a:hover {
            border-width: 0;
        }

        .nav-tabs > li.fancyTab:hover {
            background-color: #f9f9f9;
            box-shadow: 0 0 0 1px #ddd;
        }

        .nav-tabs > li.fancyTab.active:hover {
            background-color: #fff;
            box-shadow: 1px 1px 0 1px #fff, 0 0px 0 1px #ddd, -1px 1px 0 0px #ddd inset;
        }

        .nav-tabs > li.fancyTab:hover a {
            border-color: transparent;
        }

        .nav.nav-tabs .fancyTab a[data-toggle="tab"] {
            background-color: transparent;
            border-bottom: 0;
        }

        .nav-tabs > li.fancyTab:hover a {
            border-right: 1px solid transparent;
        }

        .nav-tabs > li.fancyTab > a {
            margin-right: 0;
            border-top: 0;
            padding-bottom: 30px;
            margin-bottom: -30px;
        }

        .nav-tabs > li.fancyTab {
            margin-right: 0;
            margin-bottom: 0;
        }

            .nav-tabs > li.fancyTab:last-child a {
                border-right: 1px solid transparent;
            }

            .nav-tabs > li.fancyTab.active:last-child {
                border-right: 0px solid #ddd;
                box-shadow: 0px 2px 0 0px #fff, 0px 0px 0 1px #ddd;
            }

        .fancyTab:last-child {
            box-shadow: 0 0 0 1px #ddd;
        }

        .tabs .nav-tabs li.fancyTab.active a {
            box-shadow: none;
            top: 0;
        }


        .fancyTab.active {
            background: #fff;
            box-shadow: 1px 1px 0 1px #fff, 0 0px 0 1px #ddd, -1px 1px 0 0px #ddd inset;
            padding-bottom: 30px;
        }

        .arrow-down {
            display: none;
            width: 0;
            height: 0;
            border-left: 20px solid transparent;
            border-right: 20px solid transparent;
            border-top: 22px solid #ddd;
            position: absolute;
            top: -1px;
            left: calc(50% - 20px);
        }

        .arrow-down-inner {
            width: 0;
            height: 0;
            border-left: 18px solid transparent;
            border-right: 18px solid transparent;
            border-top: 12px solid #fff;
            position: absolute;
            top: -22px;
            left: -18px;
        }

        .fancyTab.active .arrow-down {
            display: block;
        }

        @media (max-width: 1200px) {

            .fancyTab .fa {
                font-size: 36px;
            }

            .fancyTab .hidden-xs {
                font-size: 22px;
            }
        }


        @media (max-width: 992px) {

            .fancyTab .fa {
                font-size: 33px;
            }

            .fancyTab .hidden-xs {
                font-size: 18px;
                font-weight: normal;
            }
        }


        @media (max-width: 768px) {

            .fancyTab > a {
                font-size: 18px;
            }

            .nav > li.fancyTab > a {
                padding: 15px 0;
                margin-bottom: inherit;
            }

            .fancyTab .fa {
                font-size: 30px;
            }

            .nav-tabs > li.fancyTab > a {
                border-right: 1px solid transparent;
                padding-bottom: 0;
            }

            .fancyTab.active .fa {
                color: #333;
            }
        }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {
            var numItems = $('li.fancyTab').length;
            if (numItems == 12) {
                $("li.fancyTab").width('8.3%');
            }
            if (numItems == 11) {
                $("li.fancyTab").width('9%');
            }
            if (numItems == 10) {
                $("li.fancyTab").width('10%');
            }
            if (numItems == 9) {
                $("li.fancyTab").width('11.1%');
            }
            if (numItems == 8) {
                $("li.fancyTab").width('12.5%');
            }
            if (numItems == 7) {
                $("li.fancyTab").width('14.2%');
            }
            if (numItems == 6) {
                $("li.fancyTab").width('16.666666666666667%');
            }
            if (numItems == 5) {
                $("li.fancyTab").width('20%');
            }
            if (numItems == 4) {
                $("li.fancyTab").width('25%');
            }
            if (numItems == 3) {
                $("li.fancyTab").width('33.3%');
            }
            if (numItems == 2) {
                $("li.fancyTab").width('50%');
            }




        });

        $(window).load(function () {
            $('.fancyTabs').each(function () {
                var highestBox = 0;
                $('.fancyTab a', this).each(function () {

                    if ($(this).height() > highestBox)
                        highestBox = $(this).height();
                });
                $('.fancyTab a', this).height(highestBox);
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <section id="fancyTabWidget" class="tabs t-tabs">
            <ul class="nav nav-tabs fancyTabs" role="tablist">
                <li class="tab fancyTab active">
                <div class="arrow-down"><div class="arrow-down-inner"></div></div>	
                    <a id="tab0" href="#tabBody0" role="tab" aria-controls="tabBody0" aria-selected="true" data-toggle="tab" tabindex="0"><span class="fa fa-cogs"></span><span class="hidden-xs">Services</span></a>
                    <div class="whiteBlock"></div>
                </li>
                
                <li class="tab fancyTab">
                <div class="arrow-down"><div class="arrow-down-inner"></div></div>
                    <a id="tab3" href="#tabBody1" role="tab" aria-controls="tabBody3" aria-selected="true" data-toggle="tab" tabindex="0"><span class="fa fa-mortar-board"></span><span class="hidden-xs">Sales</span></a>
                    <div class="whiteBlock"></div>
                </li>
            </ul>
            <div id="myTabContent" class="tab-content fancyTabContent" aria-live="polite">
                <div class="tab-pane  fade active in" id="tabBody0" role="tabpanel" aria-labelledby="tab0" aria-hidden="false" tabindex="0">
                    <div class="row">
                        <div class="col-lg-6" id="DocketListDiv" runat="server">
                            <div class="panel panel-danger">
                                <div class="panel-heading">
                                    <asp:Button ID="btnDocketListClose" runat="server" Text="X" ToolTip="Close" OnClick="btnDivClose_Click" OnClientClick="showSuccessDivClose()" />
                                    &nbsp;&nbsp;&nbsp;Dockets
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div style="min-height: 25vh;">
                                            <div class="table-responsive" id="gvDocketDiv">
                                                <div class="over" style="position: absolute; width: 100%; height: 100%">
                                                    <img src="images/gridLoader.gif" style="position: absolute; height: 50%" />
                                                </div>
                                                <asp:GridView ID="gvDocketAsync" DataKeyNames="DocketId" runat="server" RowStyle-Font-Size="9px"
                                                    AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                                                    class="table table-striped" GridLines="None" Style="text-align: left"
                                                    OnRowDataBound="gvDocketAsync_RowDataBound" OnPageIndexChanging="gvDocketAsync_PageIndexChanging"
                                                    AllowPaging="true" AllowCustomPaging="true" PageSize="10">
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
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Name
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <span title='<%# Eval("CustomerName") %>'>
                                                                    <%# (Eval("CustomerName").ToString().Length>30)?Eval("CustomerName").ToString().Substring(0,30)+"...":Eval("CustomerName").ToString() %>
                                                                </span>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Model" DataField="ProductName" />
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                CP
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <span title='<%# Eval("ContactPerson") %>'>
                                                                    <%# (Eval("ContactPerson").ToString().Length>20)?Eval("ContactPerson").ToString().Substring(0,20)+"...":Eval("ContactPerson").ToString() %>
                                                                </span>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
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
                        <div class="col-lg-6" id="TonerListDiv" runat="server">
                            <div class="panel panel-warning">
                                <div class="panel-heading">
                                    <asp:Button ID="btnTonerListClose" runat="server" Text="X" ToolTip="Close" OnClick="btnDivClose_Click" OnClientClick="showSuccessDivClose()" />
                                    &nbsp;&nbsp;&nbsp;Toner Requests 
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div style="min-height: 25vh;">
                                            <div class="table-responsive" id="gvTonerDiv">
                                                <div class="over" style="position: absolute; width: 100%; height: 100%">
                                                    <img src="images/gridLoader.gif" style="position: absolute; height: 50%" />
                                                </div>
                                                <asp:GridView ID="gvTonnerRequestAsync" DataKeyNames="TonnerRequestId" runat="server"
                                                    RowStyle-Font-Size="9px" AutoGenerateColumns="False" Width="100%" CellPadding="4"
                                                    ForeColor="#333333" class="table table-striped" GridLines="None" Style="text-align: left"
                                                    OnRowDataBound="gvTonnerRequestAsync_RowDataBound" OnPageIndexChanging="gvTonnerRequestAsync_PageIndexChanging"
                                                    AllowPaging="true" AllowCustomPaging="true" PageSize="10">
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
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                Name
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <span title='<%# Eval("CustomerName") %>'>
                                                                    <%# (Eval("CustomerName").ToString().Length>30)?Eval("CustomerName").ToString().Substring(0,30)+"...":Eval("CustomerName").ToString() %>
                                                                </span>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField HeaderText="Model" DataField="ProductName" />
                                                        <asp:TemplateField>
                                                            <HeaderTemplate>
                                                                CP
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <span title='<%# Eval("ContactPerson") %>'>
                                                                    <%# (Eval("ContactPerson").ToString().Length>20)?Eval("ContactPerson").ToString().Substring(0,20)+"...":Eval("ContactPerson").ToString() %>
                                                                </span>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
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
                            <div class="col-lg-6" id="ChartDiv" runat="server">
                                <div class="panel panel-green">
                                    <div class="panel-heading">
                                        <asp:Button ID="btnChartClose" runat="server" Text="X" ToolTip="Close" OnClick="btnDivClose_Click" OnClientClick="showSuccessDivClose()" />
                                        &nbsp;&nbsp;&nbsp;Contract Status                      
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
                            <div class="col-lg-3" id="ExpiringListDiv" runat="server">
                                <div class="panel panel-yellow">
                                    <div class="panel-heading">
                                        <asp:Button ID="btnExpiringClose" runat="server" Text="X" ToolTip="Close" OnClick="btnDivClose_Click" OnClientClick="showSuccessDivClose()" />
                                        &nbsp;&nbsp;&nbsp;Contracts Expiring Soon
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
                                                    OnPageIndexChanging="gvExpiringSoonAsync_PageIndexChanging" PageSize="10" AllowPaging="true" AllowCustomPaging="true">
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
                            <div class="col-lg-3" id="ExpiredListDiv" runat="server">
                                <div class="panel panel-red">
                                    <div class="panel-heading">
                                        <asp:Button ID="btnExpiredClose" runat="server" Text="X" ToolTip="Close" OnClick="btnDivClose_Click" OnClientClick="showSuccessDivClose()" />
                                        &nbsp;&nbsp;&nbsp;Contracts Expired
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
                                                    OnPageIndexChanging="gvExpiredListAsync_PageIndexChanging" PageSize="10" AllowPaging="true" AllowCustomPaging="true">
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
                </div>
                <div class="tab-pane  fade" id="tabBody1" role="tabpanel" aria-labelledby="tab1" aria-hidden="true" tabindex="0">
                    
                </div>
            </div>
        </section>
</asp:Content>
