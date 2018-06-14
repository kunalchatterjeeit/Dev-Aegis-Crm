<%@ Page Title="EMPLOYEE DOCKET" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="EmployeeDocket.aspx.cs" Inherits="WebAppAegisCRM.Service.EmployeeDocket" %>

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
                            Customer List
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        Customer Id:
                                        <asp:TextBox ID="txtCustomerCode" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        Customer Name:
                                        <asp:TextBox ID="txtName" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        Mobile No:
                                        <asp:TextBox ID="txtContactNo" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        Email:
                                        <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <br />
                                    <asp:Button ID="btnCustomerSearch" runat="server" Text="Search Customer" CssClass="btn btn-outline btn-success" OnClientClick="return ValidationForSave();" OnClick="btnCustomerSearch_Click" />
                                </div>
                            </div>
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <div style="height: 50vh; overflow: scroll">
                                        <asp:GridView ID="gvCustomerMaster" DataKeyNames="CustomerMasterId" runat="server"
                                            AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                                            class="table table-striped" GridLines="None" Style="text-align: left">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkCustomer" AutoPostBack="true" OnCheckedChanged="chkCustomer_checkchanged"
                                                            CommandArgument='<%# Eval("CustomerMasterId") %>' runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        SN.
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Customer Id" DataField="CustomerCode" />
                                                <asp:BoundField HeaderText="Customer Name" DataField="CustomerName" />
                                                <asp:TemplateField HeaderText="Customer Type">
                                                    <ItemTemplate>
                                                        <%# ((int)Eval("CustomerType") == (int)Business.Common.Constants.CustomerType.APlus)? "A+": (((int)Eval("CustomerType") == (int)Business.Common.Constants.CustomerType.A)? "A" : (((int)Eval("CustomerType") == (int)Business.Common.Constants.CustomerType.B)? "B" : "N/A")) %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Mobile" DataField="MobileNo" />
                                                <asp:BoundField HeaderText="Email" DataField="EmailId" />
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
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Choose Machine
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group has-error">
                                        <div style="height: 30vh; overflow: scroll">
                                            <asp:GridView ID="gvPurchase" DataKeyNames="CustomerPurchaseId" runat="server"
                                                AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                                                class="table table-striped" GridLines="None" Style="text-align: left" OnRowDataBound="gvPurchase_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk" runat="server" OnCheckedChanged="chk_CheckedChanged" AutoPostBack="true" ToolTip='<%# (Eval("IsDocketOpen").ToString() == "1")? "Already open docket" : "Choose to docket" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Machine Id" DataField="MachineId" />
                                                    <asp:BoundField HeaderText="Brand" DataField="BrandName" />
                                                    <asp:BoundField HeaderText="Model Code" DataField="ProductCode" />
                                                    <asp:BoundField HeaderText="Model Name" DataField="ProductName" />
                                                    <asp:BoundField HeaderText="Serial No" DataField="ProductSerialNo" />
                                                    <asp:BoundField HeaderText="Contact Person" DataField="ContactPerson" />
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
                                <div class="col-lg-3">
                                    <div class="form-group has-error">
                                        Docket Type:                                        
                                        <asp:DropDownList ID="ddlDocketType" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                            <asp:ListItem Text="CM" Value="CM"></asp:ListItem>
                                            <asp:ListItem Text="INSTALLATION" Value="INSTALLATION"></asp:ListItem>
                                            <asp:ListItem Text="OTHERS" Value="OTHERS"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <%--<div class="col-lg-3">
                                    <div class="form-group has-error">
                                        Current Meter Reading:                                        
                                        <asp:TextBox ID="txtCurrentMeterReading" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>--%>
                                <div class="col-lg-3">
                                    <div class="form-group has-error">
                                        Docket Date:
                                        <asp:TextBox ID="txtDocketDate" runat="server" CssClass="form-control" Style="background: #fff" disabled></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDocketDate"
                                            Format="dd MMM yyyy" Enabled="True">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-lg-5">
                                    <div class="form-group has-error">
                                        Time :
                                        <div class="form-control">
                                            <asp:DropDownList ID="ddlTimeHH" runat="server">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlTimeMM" runat="server">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlTimeTT" runat="server">
                                                <asp:ListItem Value="AM" Text="AM"></asp:ListItem>
                                                <asp:ListItem Value="PM" Text="PM"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div class="form-group has-error">
                                        Problem:
                                        <asp:TextBox ID="txtProblem" CssClass="form-control" runat="server" TextMode="MultiLine" Height="100px"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-outline btn-success" OnClientClick="return ValidationForSave();" OnClick="btnSubmit_Click" />

                                </div>
                                <div class="col-lg-10">
                                    <uc3:Message ID="Message" runat="server"></uc3:Message>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Latest Dockets
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <asp:GridView ID="gvDocket" DataKeyNames="DocketId,CustomerPurchaseId,CallStatusId" runat="server"
                                    AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                                    class="table table-striped" GridLines="None" Style="text-align: left">
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
                                        <asp:BoundField HeaderText="Docket Date" DataField="DocketDate" />
                                        <asp:BoundField HeaderText="Docket Time" DataField="DocketTime" />
                                        <asp:BoundField HeaderText="Model" DataField="ProductName" />
                                        <asp:BoundField HeaderText="Problem" DataField="Problem" ItemStyle-Width="35%" />
                                        <asp:BoundField HeaderText="Call Status" DataField="CallStatus" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
