<%@ Page Title="STOCK SNAP" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="True" CodeBehind="StockSnap.aspx.cs" Inherits="WebAppAegisCRM.Inventory.StockSnap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Lookup Filter
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-3">
                                <div class="form-group">
                                    Product/Spare Name :
                                <asp:TextBox ID="txtName" CssClass="form-control" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="form-group"><br />
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-outline btn-success" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Particulars
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
                            <asp:BoundField HeaderText="Product Name" DataField="ProductName" />
                            <asp:BoundField HeaderText="Spare Name" DataField="SpareName" />
                            <asp:BoundField HeaderText="Location" DataField="Location" />
                            <asp:BoundField HeaderText="Quantity" DataField="Quantity" />
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
</asp:Content>
