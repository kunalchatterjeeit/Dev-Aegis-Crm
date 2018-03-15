<%@ Page Title="STOCK SNAP" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="True" CodeBehind="StockSnap.aspx.cs" Inherits="WebAppAegisCRM.Inventory.StockSnap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Entry Particulars
                </div>
                <div class="table-responsive">
                    <asp:GridView ID="gvStockSnap" runat="server" AutoGenerateColumns="False"
                        Width="100%" CellPadding="4" ForeColor="#333333" class="table table-striped" GridLines="None"
                        Style="text-align: left" OnPageIndexChanging="gvStockSnap_PageIndexChanging" PageSize="20" AllowPaging="true">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    SN.
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="ProductName" DataField="ProductName" />
                            <asp:BoundField HeaderText="SpareName" DataField="SpareName" />
                            <asp:BoundField HeaderText="Location" DataField="Location" />
                            <asp:BoundField HeaderText="Quantity" DataField="Quantity" />
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
</asp:Content>
