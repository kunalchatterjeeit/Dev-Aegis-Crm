<%@ Page Title="VENDOR LIST" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="VendorMasterView.aspx.cs" Inherits="WebAppAegisCRM.Purchase.VendorMasterView" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <br />
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Vendor List
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:GridView ID="gvVendorMaster" DataKeyNames="VendorMasterId" runat="server" PageSize="13"
                            OnRowDeleting="gvVendorMaster_RowDeleting" OnRowEditing="gvVendorMaster_RowEditing"
                            AutoGenerateColumns="false" Width="100%" CellPadding="4" EnableModelValidation="True"
                            class="table table-striped" GridLines="None" Style="text-align: left" OnRowCommand="gvVendorMaster_RowCommand"
                            AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvVendorMaster_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>SN.</HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Vendor Name" DataField="VendorMasterName" />
                                <asp:BoundField HeaderText="Mobile" DataField="Mobile" />
                                <asp:BoundField HeaderText="Email" DataField="MailID" />
                                <asp:BoundField HeaderText="City" DataField="CityName" />
                                <asp:BoundField HeaderText="Status" DataField="Status" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgEdit" runat="server" CausesValidation="false" CommandName="Edit"
                                            ImageUrl="~/Images/edit_button.png" ImageAlign="AbsMiddle" ToolTip="EDIT" Width="20px"
                                            Height="20px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="25px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" ImageUrl="~/Images/delete_button.png"
                                            Width="20px" Height="20px" OnClientClick="return confirm('Are You Sure?');" />
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
</asp:Content>
