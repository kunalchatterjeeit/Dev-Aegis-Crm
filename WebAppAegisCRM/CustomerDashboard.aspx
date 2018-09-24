<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/CustomerMain.Master" AutoEventWireup="true"
    CodeBehind="CustomerDashboard.aspx.cs" Inherits="WebAppAegisCRM.CustomerDashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Morris Charts CSS -->
    <link href="bower_components/morrisjs/morris.css" rel="stylesheet">
    <!-- Timeline CSS -->
    <link href="dist/css/timeline.css" rel="stylesheet">
    <!-- Flot Charts JavaScript -->
    <script src="bower_components/flot/excanvas.min.js"></script>
    <script src="bower_components/flot/jquery.flot.js"></script>
    <script src="bower_components/flot/jquery.flot.pie.js"></script>
    <script src="bower_components/flot/jquery.flot.resize.js"></script>
    <script src="bower_components/flot/jquery.flot.time.js"></script>
    <script src="bower_components/flot.tooltip/js/jquery.flot.tooltip.min.js"></script>

    <script type="text/javascript">
        //Flot Pie Chart
        function PieData(a, b) {
            $(document).ready(function () {

                var data = [{
                    label: "Up Time",
                    data: a
                }, {
                    label: "Down Time",
                    data: b
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
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">
        google.charts.load("current", { packages: ['corechart'] });
        google.charts.setOnLoadCallback(drawChart);
        function drawChart() {
            $.ajax({
                type: 'POST',
                dataType: 'json',
                contentType: 'application/json',
                url: 'WebServices/WebServiceChart.asmx/ColumnChartData',
                data: '{}',
                success: function (response) {
                    debugger;
                    var dataValues = response.d;
                    var data = new google.visualization.DataTable();

                    data.addColumn('string', 'MachineId');
                    data.addColumn('number', 'Avg_Response_Time');

                    for (var i = 0; i < dataValues.length; i++) {
                        data.addRow([dataValues[i].MachineId, parseFloat(dataValues[i].Avg_Response_Time)]);
                    }
                    var view = new google.visualization.DataView(data);
                    view.setColumns([0, 1,
                                     {
                                         calc: "stringify",
                                         sourceColumn: 1,
                                         type: "string",
                                         role: "annotation"
                                     }]);

                    var options = {
                        title: "",
                        width: "100%"
                    };
                    var chart = new google.visualization.ColumnChart(document.getElementById("columnchart_values"));
                    chart.draw(view, options);
                },

                error: function () {
                    alert("Error loading data! Please try again.");
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
        <img src="images/welcome.png" style="margin: 3% 0 0 0" />
    </center>
    <div class="row" style="margin: 4% 0 0 0">
        <div class="container-fluid">
            <div class="row">
                <div class="col-lg-3">
                    <div class="panel-default">
                        <div class="panel-heading">
                            Average Up Time                     
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <div class="flot-chart">
                                <div class="flot-chart-content" id="flot-pie-chart"></div>
                            </div>
                        </div>
                        <!-- /.panel-body -->
                    </div>
                </div>
                <div class="col-lg-9">
                    <div class="panel-default">
                        <div class="panel-heading">
                            Response Time                     
                        </div>
                        <!-- /.panel-heading -->
                        <div class="panel-body">
                            <div class="flot-chart">
                                <div id="columnchart_values" style="height: 100%; width: 100%"></div>
                            </div>
                        </div>
                        <!-- /.panel-body -->
                    </div>
                    <!-- /.panel -->
                </div>
                <div class="row">
                <div class="col-lg-6">
                    <div class="panel-default">
                        <div class="panel-heading">
                            Machine List
                        </div>
                        <asp:GridView ID="gvMachineList" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Width="100%" 
                                 class="table table-striped" Style="text-align: left"
                                 OnPageIndexChanging="gvMachineList_PageIndexChanging" PageSize="5" AllowPaging="true"
                                 OnRowDataBound="gvMachineList_RowDataBound">
                            <AlternatingRowStyle BackColor="White" />
                            <Columns>
                                <asp:BoundField HeaderText="SN." DataField="sn" />
                                <asp:BoundField HeaderText="Machine Id" DataField="machine_id" />
                                <asp:BoundField HeaderText="Serial No." DataField="serial_no" />
                            </Columns>
                            <EditRowStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#EFF3FB" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#F5F7FB" />
                            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                            <SortedDescendingCellStyle BackColor="#E9EBEF" />
                            <SortedDescendingHeaderStyle BackColor="#4870BE" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            </div>
        </div>
    </div>
</asp:Content>
