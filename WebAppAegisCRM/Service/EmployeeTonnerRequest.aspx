<%@ Page Title="EMPLOYEE TONER REQUEST" Language="C#" MasterPageFile="~/Main.Master"
    AutoEventWireup="true" CodeBehind="EmployeeTonnerRequest.aspx.cs" Inherits="WebAppAegisCRM.Service.EmployeeTonnerRequest" %>

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
                                    <asp:Button ID="btnCustomerSearch" runat="server" Text="Search Customer" CssClass="btn btn-outline btn-success"
                                        OnClientClick="return ValidationForSave();" OnClick="btnCustomerSearch_Click" />
                                </div>
                            </div>
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvCustomerMaster" DataKeyNames="CustomerMasterId" runat="server"
                                        AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                                        class="table table-striped" GridLines="None" Style="text-align: left" AllowPaging="true" AllowCustomPaging="true" PageSize="10" OnPageIndexChanging="gvCustomerMaster_PageIndexChanging">
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
                                                    <%#  (gvCustomerMaster.PageIndex * gvCustomerMaster.PageSize) + (Container.DataItemIndex + 1) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Customer Id" DataField="CustomerCode" />
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Customer Name
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <span title='<%# Eval("CustomerName") %>'>
                                                        <%# (Eval("CustomerName").ToString().Length>30)?Eval("CustomerName").ToString().Substring(0,30)+"...":Eval("CustomerName").ToString() %>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                                            <asp:GridView ID="gvPurchase" DataKeyNames="CustomerPurchaseId" runat="server" AutoGenerateColumns="False"
                                                Width="100%" CellPadding="4" ForeColor="#333333" class="table table-striped"
                                                GridLines="None" Style="text-align: left" OnRowDataBound="gvPurchase_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chk" runat="server" OnCheckedChanged="chk_CheckedChanged" AutoPostBack="true" ToolTip='<%# (Eval("IsTonerOpen").ToString() == "1")? "Already open request" : "Choose to request a toner" %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Machine Id" DataField="MachineId" />
                                                    <asp:BoundField HeaderText="Contact Person" DataField="ContactPerson" />
                                                    <%--<asp:BoundField HeaderText="Contract End Date" DataField="ContractEndDate" DataFormatString="{0: dd MMM yyyy}" />--%>
                                                    <%--<asp:BoundField HeaderText="Contact Type" DataField="ContractName" />--%>
                                                    <asp:BoundField HeaderText="Avg Response Time" DataField="AVGResponseTime" ItemStyle-HorizontalAlign="Left" />
                                                    <asp:BoundField HeaderText="UpTime " DataField="UpTime" ItemStyle-HorizontalAlign="Left" />
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
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Choose Toner
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group has-error">
                                        <asp:GridView ID="gvTonner" DataKeyNames="SpareId" runat="server"
                                            AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                                            class="table table-striped" GridLines="None" Style="text-align: left">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <%--<asp:CheckBox ID="chk1" runat="server" OnCheckedChanged="chk1_CheckedChanged" AutoPostBack="true" />--%>
                                                        <asp:CheckBox ID="chk1" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Toner" DataField="SpareName" />
                                                <asp:BoundField HeaderText="Yield" DataField="Yield" />
                                                <asp:TemplateField>
                                                    <HeaderTemplate>Quantity</HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtRequisiteQty" runat="server" TextMode="Number" Text="1"></asp:TextBox>
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
                                <div class="col-lg-2">
                                    <div class="form-group has-error">
                                        Last A3 B/W Meter Reading:
                                        <asp:Label ID="lblA3BWLastMeterReading" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group has-error">
                                        Last A4 B/W Reading:                             
                                        <asp:Label ID="lblA4BWLastMeterReading" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group has-error">
                                        Last A3 CL Reading:                              
                                        <asp:Label ID="lblA3ClLastMeterReading" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group has-error">
                                        Last A4 CL Reading:                              
                                        <asp:Label ID="lblA4ClLastMeterReading" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="clearfix"></div>

                                <div class="col-lg-2">
                                    <div class="form-group has-error">
                                        Current A3 B/W Reading:
                                        <asp:TextBox ID="txtA3BWMeterReading" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="txtMeterReading_FilteredTextBoxExtender" ValidChars="0123456789"
                                            runat="server" TargetControlID="txtA3BWMeterReading">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group has-error">
                                        Current A4 B/W Reading:
                                        <asp:TextBox ID="txtA4BWMeterReading" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" ValidChars="0123456789"
                                            runat="server" TargetControlID="txtA4BWMeterReading">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group has-error">
                                        Current A3 CL Reading:
                                        <asp:TextBox ID="txtA3CLMeterReading" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" ValidChars="0123456789"
                                            runat="server" TargetControlID="txtA3CLMeterReading">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group has-error">
                                        Current A4 CL Reading:
                                        <asp:TextBox ID="txtA4CLMeterReading" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" ValidChars="0123456789"
                                            runat="server" TargetControlID="txtA4CLMeterReading">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group has-error">
                                        Request Date:
                                        <asp:TextBox ID="txtRequestDate" runat="server" CssClass="form-control" Style="background: #fff" disabled></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtRequestDate"
                                            Format="dd MMM yyyy" Enabled="True">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        Request:
                                        <asp:TextBox ID="txtRequest" CssClass="form-control" runat="server" TextMode="MultiLine"
                                            Height="100px"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-outline btn-success"
                                        OnClientClick="return ValidationForSave();" OnClick="btnSubmit_Click" />
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
                            Latest Toner Requests
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <asp:GridView ID="gvTonnerRequest" DataKeyNames="TonnerRequestId,CustomerPurchaseId,CallStatusId" runat="server"
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
                                        <asp:BoundField HeaderText="Request No" DataField="RequestNo" />
                                        <asp:BoundField HeaderText="Date" DataField="RequestDate" />
                                        <asp:BoundField HeaderText="Time" DataField="RequestTime" />
                                        <asp:BoundField HeaderText="Model" DataField="ProductName" />
                                        <asp:BoundField HeaderText="A3 B/W Meter" DataField="A3BWMeterReading" />
                                        <asp:BoundField HeaderText="A4 B/W Meter" DataField="A4BWMeterReading" />
                                        <asp:BoundField HeaderText="A3 CL Meter" DataField="A3CLMeterReading" />
                                        <asp:BoundField HeaderText="A4 CL Meter" DataField="A4CLMeterReading" />
                                        <asp:BoundField HeaderText="Status" DataField="CallStatus" />
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
