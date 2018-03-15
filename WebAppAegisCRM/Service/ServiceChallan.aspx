<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceChallan.aspx.cs" Inherits="WebAppAegisCRM.Service.ServiceChallan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../dist/css/signature-pad.css" type="text/css" />
    <link href="../js/AutoComplete/jquery-ui.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../js/AutoComplete/jquery.min.js"></script>
    <script type="text/javascript" src="../js/AutoComplete/jquery-ui.min.js"></script>
    <style>
        * {
            box-sizing: border-box;
        }

        .myInput {
            background-image: url('/../images/searchicon.png');
            background-position: 10px 10px;
            background-repeat: no-repeat;
            width: 100%;
            font-size: 12px;
            padding: 12px 20px 12px 40px;
            border: 1px solid #ddd;
            margin-bottom: 12px;
        }

        table {
            border-collapse: collapse;
            width: 100%;
            border: 1px solid #ddd;
            font-size: 12px;
        }

        #myTable th, #myTable td {
            text-align: left;
            padding: 2px;
        }

        #myTable tr {
            border-bottom: 1px solid #ddd;
        }

            #myTable tr.header, #myTable tr:hover {
                background-color: #f1f1f1;
            }
    </style>
    <script type="text/javascript">
        function showSignaturePad() {
            document.getElementById("signature-pad").style.display = 'block';
            resizeCanvas();
            //scroll to sign pad
            $('html, body').animate({
                scrollTop: $("#signature-pad").offset().top
            }, 2000);
        }
        function GetAutocompleteInventories() {
            myFunction();
            $("#txtItem").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        url: "../WebServices/InternalServices.asmx/LoadAutoCompleteItems",
                        data: "{'searchContent':'" + document.getElementById('txtItem').value + "'}",
                        dataType: "json",
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (result) {
                            alert(result.responseText);
                        }
                    });
                }
            });
        }
    </script>
    <script>
        function myFunction() {
            var input, filter, table, tr, td, i;
            input = document.getElementById("txtItem");
            filter = input.value.toUpperCase();
            table = document.getElementById("myTable");
            tr = table.getElementsByTagName("tr");
            for (i = 0; i < tr.length; i++) {
                td = tr[i].getElementsByTagName("td")[0];
                td2 = tr[i].getElementsByTagName("td")[1];
                if (td) {
                    if ((td.innerHTML.toUpperCase().indexOf(filter) > -1) || (td2.innerHTML.toUpperCase().indexOf(filter) > -1)) {
                        tr[i].style.display = "";
                    } else {
                        tr[i].style.display = "none";
                    }
                }
            }
        }
    </script>
    <style type="text/css">
        .btn {
            background: #ffffff;
            background-image: -webkit-linear-gradient(top, #ffffff, #ffffff);
            background-image: -moz-linear-gradient(top, #ffffff, #ffffff);
            background-image: -ms-linear-gradient(top, #ffffff, #ffffff);
            background-image: -o-linear-gradient(top, #ffffff, #ffffff);
            background-image: linear-gradient(to bottom, #ffffff, #ffffff);
            -webkit-border-radius: 8;
            -moz-border-radius: 8;
            border-radius: 4px;
            font-family: Arial;
            color: #3cfc63;
            font-size: 13px;
            padding: 10px 20px 10px 20px;
            border: solid #3cfc63 1px;
            text-decoration: none;
            margin-top:5px;
            width:100%;
        }

            .btn:hover {
                text-decoration: none;
                background: #3cfc63;
                color: #fff;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <br />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <%--<asp:HiddenField ID="hdnProductId" runat="server" />--%>
                <div style="overflow: scroll; height: 55vh;">
                    <asp:TextBox ID="txtItem" CssClass="form-control myInput"
                        Style="max-width: 400px; margin: 5px 10px; font-size: 11px" runat="server"
                        onkeydown="javascript:GetAutocompleteInventories()" placeholder="Search"></asp:TextBox>
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
                                            CommandArgument='<%# Eval("AssetId")+"|"+Eval("ItemId") %>' Enabled='<%# (Eval("IsSelected").ToString() == "1"?false:true)%>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>

                    </table>
                </div>
                <div style="overflow: scroll; height: 35vh;">
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
            </ContentTemplate>
        </asp:UpdatePanel>
        <input type="button" class="btn" value="Proceed to Sign" onclick="showSignaturePad()" />

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

        <script src="../js/signature_pad.js"></script>
        <script src="../js/app.js"></script>

    </form>
</body>
</html>
