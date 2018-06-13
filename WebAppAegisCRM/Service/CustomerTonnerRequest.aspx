<%@ Page Title="TONER REQUEST" Language="C#" MasterPageFile="~/CustomerMain.Master" AutoEventWireup="true" CodeBehind="CustomerTonnerRequest.aspx.cs" Inherits="WebAppAegisCRM.Service.CustomerTonnerRequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
            <div class="container-fluid">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="row">
                            <div class="col-lg-12">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        Request a Toner
                                    </div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="form-group has-error">
                                                    Choose Machine
                                                    <div class="table-responsive">
                                                        <div style="height: 50vh; overflow: scroll">
                                                            <asp:GridView ID="gvPurchase" DataKeyNames="CustomerPurchaseId" runat="server"
                                                                AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                                                                class="table table-striped" GridLines="None" Style="text-align: left" OnRowDataBound="gvPurchase_RowDataBound">
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chk" runat="server" OnCheckedChanged="chk_CheckedChanged" AutoPostBack="true" ToolTip='<%# (Eval("IsTonerOpen").ToString() == "1")? "Already open request" : "Choose to request a toner" %>' />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField HeaderText="Machine Id" DataField="MachineId" />
                                                                    <asp:BoundField HeaderText="Brand" DataField="BrandName" />
                                                                    <asp:BoundField HeaderText="Model Code" DataField="ProductCode" />
                                                                    <asp:BoundField HeaderText="Model Name" DataField="ProductName" />
                                                                    <asp:BoundField HeaderText="Serial No" DataField="ProductSerialNo" />
                                                                    <asp:BoundField HeaderText="Avg Response Time" DataField="AVGResponseTime" ItemStyle-HorizontalAlign="Left" />
                                                                    <asp:TemplateField HeaderText="Up Time" ItemStyle-HorizontalAlign="Left">
                                                                        <ItemTemplate>
                                                                            <%# Eval("UpTime") %>%
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
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
                                            </div>
                                            <div class="col-lg-12">
                                                <div class="form-group has-error">
                                                    Choose Toner
                                        <asp:GridView ID="gvTonner" DataKeyNames="SpareId" runat="server"
                                            AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                                            class="table table-striped" GridLines="None" Style="text-align: left">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chk1" runat="server" OnCheckedChanged="chk1_CheckedChanged" AutoPostBack="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Toner" DataField="SpareName" />
                                                <asp:BoundField HeaderText="A3 B/W Last Meter Reading" DataField="A3BWLastMeterReading" />
                                                <asp:BoundField HeaderText="A4 B/W Last Meter Reading" DataField="A4BWLastMeterReading" />
                                                <asp:BoundField HeaderText="A3 CL Last Meter Reading" DataField="A3CLLastMeterReading" />
                                                <asp:BoundField HeaderText="A4 CL Last Meter Reading" DataField="A4CLLastMeterReading" />
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
                                            <div class="col-lg-3">
                                                <div class="form-group has-error">
                                                    Current A3 B/W Meter Reading:
                                                    <asp:TextBox ID="txtA3BWMeterReading" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="txtMeterReading_FilteredTextBoxExtender" ValidChars="0123456789"
                                                        runat="server" TargetControlID="txtA3BWMeterReading">
                                                    </asp:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                            <div class="col-lg-3">
                                                <div class="form-group has-error">
                                                    Current A4 B/W Meter Reading:
                                                    <asp:TextBox ID="txtA4BWMeterReading" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" ValidChars="0123456789"
                                                        runat="server" TargetControlID="txtA4BWMeterReading">
                                                    </asp:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                            <div class="col-lg-3">
                                                <div class="form-group has-error">
                                                    Current A3 CL Meter Reading:
                                                    <asp:TextBox ID="txtA3CLMeterReading" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" ValidChars="0123456789"
                                                        runat="server" TargetControlID="txtA3CLMeterReading">
                                                    </asp:FilteredTextBoxExtender>
                                                </div>
                                            </div>
                                            <div class="col-lg-3">
                                                <div class="form-group has-error">
                                                    Current A4 CL Meter Reading:
                                                    <asp:TextBox ID="txtA4CLMeterReading" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" ValidChars="0123456789"
                                                        runat="server" TargetControlID="txtA4CLMeterReading">
                                                    </asp:FilteredTextBoxExtender>
                                                </div>
                                            </div>

                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    Request:
                                        <asp:TextBox ID="txtRequest" CssClass="form-control" runat="server" TextMode="MultiLine" Height="100px"></asp:TextBox>
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
                                        Latest Toner Requests
                                    </div>
                                    <div class="panel-body">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvTonnerRequest" DataKeyNames="TonnerRequestId" runat="server"
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
                                                    <asp:BoundField HeaderText="Request Date" DataField="RequestDate" />
                                                    <asp:BoundField HeaderText="Request Time" DataField="RequestTime" />
                                                    <asp:BoundField HeaderText="Model" DataField="ProductName" />
                                                    <asp:BoundField HeaderText="Toner" DataField="SpareName" />
                                                    <asp:BoundField HeaderText="A3 B/W Meter" DataField="A3BWMeterReading" />
                                                    <asp:BoundField HeaderText="A4 B/W Meter" DataField="A4BWMeterReading" />
                                                    <asp:BoundField HeaderText="A3 CL Meter" DataField="A3CLMeterReading" />
                                                    <asp:BoundField HeaderText="A4 CL Meter" DataField="A4CLMeterReading" />
                                                    <asp:BoundField HeaderText="Remarks" DataField="Remarks" />
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
                    </div>
                    <!-- /.col-lg-12 -->
                </div>
                <!-- /.row -->
            </div>
            <!-- /.container-fluid -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
