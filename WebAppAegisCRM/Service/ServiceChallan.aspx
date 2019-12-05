<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceChallan.aspx.cs" Inherits="WebAppAegisCRM.Service.ServiceChallan" %>

<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SERVICE CHALLAN</title>
    <link href="../dist/css/custom03052019.css" rel="stylesheet" />
    <link href="../dist/css/custom-popup.css" rel="stylesheet" />
    <link rel="stylesheet" href="../dist/css/signature-pad.css" type="text/css" />
    <link href="../js/AutoComplete/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/AutoComplete/jquery.min.js"></script>
    <script type="text/javascript" src="../js/AutoComplete/jquery-ui.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">

        <br />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <%--<asp:HiddenField ID="hdnProductId" runat="server" />--%>
                <div style="overflow: scroll; height: 60vh;">
                    <asp:DropDownList ID="ddlStore" Style="max-width: 100px; margin: 5px 10px; font-size: 11px" 
                        CssClass="form-control myInputDropdown searchable" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlStore_SelectedIndexChanged">
                    </asp:DropDownList>
                    <asp:TextBox ID="txtItem" CssClass="form-control myInput"
                        Style="max-width: 400px; margin: 5px 10px; font-size: 11px" runat="server"
                        onkeydown="javascript:GetAutocompleteInventories()" placeholder="Search"></asp:TextBox>
                    <uc3:Message ID="Message" runat="server" />
                    <table id="myTable">
                        <asp:Repeater ID="RepeaterInventory" runat="server" OnItemCommand="RepeaterInventory_ItemCommand">
                            <HeaderTemplate>
                                <tr class="header">
                                    <th style="width: 50%;">Asset Id</th>
                                    <th style="width: 30%;">Spare Name</th>
                                    <th style="width: 10%;">Yield</th>
                                    <th style="width: 10%;"></th>
                                </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr <%# (Eval("IsSelected").ToString() == "1"?"style='background:#d6d6d6'":"style='background:#fff'")%>>
                                    <td>
                                        <asp:Label ID="lblAssetId" runat="server" Text='<%# Eval("AssetId") %>'></asp:Label>
                                    </td>
                                    <td><%# Eval("SpareName") %></td>
                                    <td><%# Eval("Yield") %></td>
                                    <td>
                                        <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="~/Images/add_to_cart.png" Width="17px" Height="17px" ToolTip="Add" CommandName="Add"
                                            CommandArgument='<%# Eval("AssetId")+"|"+Eval("ItemId")+"|"+Eval("StockLocationId") %>' Enabled='<%# (Eval("IsSelected").ToString() == "1"?false:true)%>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>

                    </table>
                </div>
                <div style="overflow: scroll; height: 27vh;">
                    <asp:GridView ID="gvSelectedAsset" DataKeyNames="AssetId" runat="server"
                        AutoGenerateColumns="False" Width="100%" CellPadding="4"
                        ForeColor="#333333" class="table table-striped" GridLines="None" Style="text-align: left" OnRowCommand="gvSelectedAsset_RowCommand">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    SN.
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Asset Id" DataField="AssetId" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnDelete" runat="server" CommandName="D" ImageUrl="~/Images/delete_button.png"
                                        CommandArgument='<%#Eval("AssetId") %>' Width="15px" Height="15px" ToolTip="Remove" />
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Button ID="btnSign" runat="server" Text="Proceed to Sign" class="btn" OnClientClick="showSignaturePad(); return false;" />
        <asp:Button ID="btnDone" runat="server" Text="Done" class="btn" OnClientClick="window.close();" />

        <div id="signature-pad" class="m-signature-pad" style="display: none">
            <div class="m-signature-pad--body">
                <canvas></canvas>
            </div>
            <div class="m-signature-pad--footer">
                <div class="description">Sign above</div>
                <button type="button" class="button clear" data-action="clear">Clear</button>
                <%--<button type="button" class="button save" data-action="save">Save</button>--%>
                <asp:Button ID="btnSave" runat="server" Text="Done" class="button save" data-action="save" OnClick="btnSave_Click" />
                <input type="hidden" id="signature" runat="server" />
            </div>
        </div>

        <script src="../dist/js/custom.js"></script>
        <script src="../js/signature_pad.js"></script>
        <script src="../js/app.js"></script>

    </form>
</body>
</html>
