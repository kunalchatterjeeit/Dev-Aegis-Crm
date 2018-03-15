<%@ Page Title="DOCKET" Language="C#" MasterPageFile="~/CustomerMain.Master" AutoEventWireup="true" CodeBehind="CustomerDocket.aspx.cs" Inherits="WebAppAegisCRM.Service.CustomerDocket" %>

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
                                        Docket Registration
                                    </div>
                                    <div class="panel-body">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="form-group has-error">
                                                    Choose Machine
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
                                                <asp:BoundField HeaderText="Avg Response Time" DataField="AVGResponseTime" ItemStyle-HorizontalAlign="Left" />
                                                <asp:TemplateField HeaderText="Up Time" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <%# Eval("UpTime") %>%
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Contact Person" DataField="ContactPerson" />
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
                                            <div class="col-lg-12">
                                                &nbsp;
                                            </div>
                                            <div class="col-lg-12">
                                                &nbsp;
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
                                            <asp:GridView ID="gvDocket" DataKeyNames="DocketId" runat="server"
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
                    <!-- /.col-lg-12 -->
                </div>
                <!-- /.row -->
            </div>
            <!-- /.container-fluid -->
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
