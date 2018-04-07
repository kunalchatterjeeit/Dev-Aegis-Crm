<%@ Page Title="PURCHASE" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Purchase.aspx.cs" Inherits="WebAppAegisCRM.Purchase.Purchase" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ValidationSubmit() {
            if (document.getElementById("<%=ddlVendor.ClientID %>").value == '0') {
                alert('Please select vendor');
                document.getElementById("<%=ddlVendor.ClientID %>").focus();
                return false;
            }
        }
        function ValidationItemAdd() {
            if (document.getElementById("<%=ddlItem.ClientID %>").value == '0') {
                alert('Please select item');
                document.getElementById("<%=ddlItem.ClientID %>").focus();
                return false;
            }
            if (document.getElementById("<%=txtQuantity.ClientID %>").value == '') {
                alert('Please enter item quantity');
                document.getElementById("<%=txtQuantity.ClientID %>").focus();
                return false;
            }
        }
    </script>
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
                        <div class="panel-heading" style="font-size: large;">
                            Purchase 
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <uc3:Message ID="Message" runat="server" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="form-group has-error">
                                        Vendor
                                <asp:DropDownList ID="ddlVendor" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                                </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        Purchase Order No.
                                <asp:TextBox ID="txtPurchaseOrderNo" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        Purchase Date
                                <asp:TextBox ID="txtPurchaseDate" CssClass="form-control" runat="server"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                            Format="dd MMM yyyy" TargetControlID="txtPurchaseDate">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        Invoice No.
                                <asp:TextBox ID="txtInvoiceNo" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        Invoice Date
                                        <asp:TextBox ID="txtInvoiceDate" CssClass="form-control" runat="server"></asp:TextBox>
                                        <asp:CalendarExtender ID="txtInvoiceDate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd MMM yyyy" TargetControlID="txtInvoiceDate">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        Billing Amount
                                <asp:TextBox ID="txtBillAmount" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        Paied Amount
                                <asp:TextBox ID="txtPaymentAmount" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading" style="font-size: large;">
                            Details 
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        <asp:Image ID="Image2" runat="server" Width="200px" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Item
                                <asp:DropDownList ID="ddlItem" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                                </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-1">
                                    <div class="form-group has-error">
                                        Quantity
                                <asp:TextBox ID="txtQuantity" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-1">
                                    <div class="form-group">
                                        Rate
                                <asp:TextBox ID="txtRate" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-1">
                                    <div class="form-group">
                                        Discount
                                <asp:TextBox ID="txtDiscount" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-1">
                                    <div class="form-group">
                                        GST
                                <asp:TextBox ID="txtGst" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        HSN Code
                                <asp:TextBox ID="txtHsnCode" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-1">
                                    <div class="form-group">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-outline btn-success" OnClick="btnAdd_Click" OnClientClick="return ValidationItemAdd();" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvItem" DataKeyNames="ItemIdType" runat="server"
                                                AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                                                class="table table-striped" GridLines="None" Style="text-align: left" OnRowCommand="gvItem_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            SN.
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Item Name" DataField="ItemName" />
                                                    <asp:BoundField HeaderText="Item Type" DataField="ItemType" />
                                                    <asp:BoundField HeaderText="Quantity" DataField="Quantity" />
                                                    <asp:BoundField HeaderText="Rate" DataField="Rate" />
                                                    <asp:BoundField HeaderText="Discount" DataField="Discount" />
                                                    <asp:BoundField HeaderText="GST" DataField="GST" />
                                                    <asp:BoundField HeaderText="HSN Code" DataField="HSNCode" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnDelete" runat="server" CommandName="D" ImageUrl="~/Images/delete_button.png"
                                                                CommandArgument='<%#Eval("ItemIdType") %>' Width="20px" Height="20px"
                                                                OnClientClick="return confirm('Are You Sure?');" />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="25px" />
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
                                                    No Item Added...
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-outline btn-success" OnClientClick="return ValidationSubmit();" OnClick="btnSubmit_Click" />
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
