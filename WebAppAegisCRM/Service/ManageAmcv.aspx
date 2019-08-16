<%@ Page Title="MANAGE AMCV" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ManageAmcv.aspx.cs" Inherits="WebAppAegisCRM.Service.ManageAmcv" %>

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
                            AMCV List
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-3">
                                <div class="form-group">
                                    Customer Name :
                                    <asp:TextBox ID="txtCustomerName" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="form-group">
                                    Machine Id :
                                    <asp:TextBox ID="txtMachineId" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <br />
                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-outline btn-success" OnClientClick="return ValidationForSave();" OnClick="btnSubmit_Click" />
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <uc3:Message ID="Message" runat="server"></uc3:Message>
                                </div>
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvAmcv" DataKeyNames="AmcvId,CustomerMasterId,CustomerPurchaseId" runat="server" AutoGenerateColumns="False"
                                            Width="100%" CellPadding="4" ForeColor="#333333" class="table table-striped" GridLines="None"
                                            Style="text-align: left" OnPageIndexChanging="gvAmcv_PageIndexChanging" PageSize="20" AllowPaging="true"
                                            OnRowCommand="gvAmcv_RowCommand">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        SN.
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Run Datetime" DataField="LastRunDate" />
                                                <asp:BoundField HeaderText="Customer" DataField="CustomerName" />
                                                <asp:BoundField HeaderText="Machine Id" DataField="MachineId" />
                                                <asp:BoundField HeaderText="A3BE" DataField="A3BW" />
                                                <asp:BoundField HeaderText="A3CL" DataField="A3CL" />
                                                <asp:BoundField HeaderText="A4BW" DataField="A4BW" />
                                                <asp:BoundField HeaderText="A4CL" DataField="A4CL" />
                                                <asp:BoundField HeaderText="Contact Person" DataField="ContactPerson" />
                                                <asp:TemplateField ItemStyle-Width="15px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" runat="server" class="fa fa-toggle-right fa-fw" CommandName="P" CausesValidation="false"
                                                            CommandArgument='<%# Eval("AmcvId") %>' ToolTip="Create docket" Style="font-size: 16px;"></asp:LinkButton>
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
