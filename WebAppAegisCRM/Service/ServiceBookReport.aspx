<%@ Page Title="SERVICE BOOK REPORT" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="ServiceBookReport.aspx.cs" Inherits="WebAppAegisCRM.Service.ServiceBookReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function Validate() {
            if (document.getElementById("<%=ddlCallType.ClientID%>").selectedIndex == 0) {
                alert("Please select call type");
                return false;
            }
            else
                return true;
        }

    </script>
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
                            Service Book Report
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Call Type :
                                        <asp:DropDownList ID="ddlCallType" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="--SELECT--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Toner Request" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Docket" Value="2"></asp:ListItem>
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
                                        Customer Name :
                                    <asp:TextBox ID="txtCustomerName" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        Model :
                                        <asp:DropDownList ID="ddlDocketProduct" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        Machine Id :
                                        <asp:TextBox ID="txtMachineId" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        Call From Date :
                                        <asp:TextBox ID="txtFromDocketDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDocketDate"
                                            Format="dd MMM yyyy" Enabled="True">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        Call To Date :
                                        <asp:TextBox ID="txtToDocketDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDocketDate"
                                            Format="dd MMM yyyy" Enabled="True">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        Call Status :
                                        <asp:DropDownList ID="ddlDocketCallStatus" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        Docket Type :
                                    <asp:DropDownList ID="ddlDocketType" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="--SELECT ALL--" Value=""></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                        <asp:ListItem Text="CM" Value="CM"></asp:ListItem>
                                        <asp:ListItem Text="INSTALLATION" Value="INSTALLATION"></asp:ListItem>
                                        <asp:ListItem Text="OTHERS" Value="OTHERS"></asp:ListItem>
                                    </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        Service Engineer :
                                    <asp:DropDownList ID="ddlServiceEngineer" CssClass="form-control" runat="server">
                                    </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-9">
                                    &nbsp;
                                </div>
                                <div class="col-lg-3">
                                    <asp:Button ID="btnDocketSearch" runat="server" Text="Search" CssClass="btn btn-outline btn-success pull-right" OnClick="btnDocketSearch_Click" />
                                </div>
                                <div class="clearfix">
                                </div>
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <br />
                                        <asp:Panel ID="pnlServiceDocket" runat="server" Visible="false">
                                            <asp:GridView ID="gvServiceDocket" runat="server" RowStyle-Font-Size="12px" AutoGenerateColumns="False"
                                                Width="100%" CellPadding="4" ForeColor="#333333" class="table table-striped"
                                                GridLines="None" Style="text-align: left" AllowPaging="True" OnPageIndexChanging="gvServiceDocket_PageIndexChanging">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Docket Id" DataField="CallId" />
                                                    <asp:TemplateField HeaderText="Call Date" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%# Convert.ToDateTime(Eval("CallDate")).ToString("dd/MM/yy") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Call Time" DataField="CallTime" ItemStyle-Width="60px" />
                                                    <asp:BoundField HeaderText="Customer" DataField="CustomerName" />
                                                    <asp:BoundField HeaderText="Machine Id" DataField="MachineId" />
                                                    <asp:BoundField HeaderText="Problem Observed" DataField="ProblemObserved" />
                                                    <asp:TemplateField HeaderText="Response Time" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%# (Eval("ResponseTime")!= DBNull.Value)?CalculateTimings(Convert.ToInt32(Eval("ResponseTime"))):"00:00" %>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Service Eng" DataField="ServiceEngineer" />
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>

                                                            <%--<asp:PlaceHolder runat="server" Visible='<%# Eval("CallType").ToString().Equals("1") %>'>
                                                                <a href='Challan.aspx?requestno=<%# Eval("CallId") %>' target="_blank">Challan</a>
                                                            </asp:PlaceHolder>

                                                            <asp:PlaceHolder runat="server" Visible='<%# Eval("CallType").ToString().Equals("2") %>'>
                                                                <a href='CSR.aspx?docketno=<%# Eval("CallId") %>' target="_blank">CSR</a>
                                                            </asp:PlaceHolder>--%>

                                                            <a href='CSR.aspx?docketno=<%# Eval("CallId") %>' target="_blank">CSR</a>

                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
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
                                        </asp:Panel>
                                        <asp:Panel ID="pnlServiceToner" runat="server" Visible="false">
                                            <asp:GridView ID="gvServiceToner" runat="server" RowStyle-Font-Size="12px" AutoGenerateColumns="False"
                                                Width="100%" CellPadding="4" ForeColor="#333333" class="table table-striped"
                                                GridLines="None" Style="text-align: left" AllowPaging="True" OnPageIndexChanging="gvServiceToner_PageIndexChanging">
                                                <Columns>
                                                    <asp:BoundField HeaderText="Toner Id" DataField="CallId" />
                                                    <asp:BoundField HeaderText="Call Date" DataField="CallDate" />
                                                    <asp:BoundField HeaderText="Call Time" DataField="CallTime" />
                                                    <asp:BoundField HeaderText="Customer" DataField="CustomerName" />
                                                    <asp:BoundField HeaderText="Machine Id" DataField="MachineId" />
                                                    <asp:BoundField HeaderText="A4 BW" DataField="A4BWMeterReading" />
                                                    <asp:BoundField HeaderText="In Time" DataField="EngIn" />
                                                    <asp:BoundField HeaderText="Out Time" DataField="EngOut" />
                                                    <asp:TemplateField HeaderText="Response Time" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <%# CalculateTimings(Convert.ToInt32(Eval("ResponseTime")))%>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Service Eng" DataField="ServiceEngineer" />
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>

                                                            <%--<asp:PlaceHolder runat="server" Visible='<%# Eval("CallType").ToString().Equals("1") %>'>
                                                                <a href='Challan.aspx?requestno=<%# Eval("CallId") %>' target="_blank">Challan</a>
                                                            </asp:PlaceHolder>

                                                            <asp:PlaceHolder runat="server" Visible='<%# Eval("CallType").ToString().Equals("2") %>'>
                                                                <a href='CSR.aspx?docketno=<%# Eval("CallId") %>' target="_blank">CSR</a>
                                                            </asp:PlaceHolder>--%>

                                                            <a href='Challan.aspx?requestno=<%# Eval("CallId") %>' target="_blank">Challan</a>

                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
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
                                        </asp:Panel>
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
