<%@ Page Title="ADD/EDIT QUOTE" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Quote.aspx.cs" Inherits="WebAppAegisCRM.Sales.Quote" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        
        function ValidationItemAdd() {
            if (document.getElementById("<%=ddlItem.ClientID %>").value == '0') {
                alert('Please select item');
                document.getElementById("<%=ddlItem.ClientID %>").focus();
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
                        <div class="panel-heading">
                            Quote 
                        </div>
                        <div class="panel-body">
                           
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="form-group has-error">
                                        Quote Number
                                <asp:TextBox ID="txtQuoteNumber" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group has-error">
                                        Opportunity
                                <asp:DropDownList ID="ddlOpportunity" CssClass="form-control" runat="server">
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
                                        Payment Term
                                <asp:DropDownList ID="ddlPaymentTerm" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        Original PO Date
                                <asp:TextBox ID="txtOriginalPODate" CssClass="form-control" runat="server"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                            Format="dd MMM yyyy" TargetControlID="txtOriginalPODate">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        Actual Close Date
                                        <asp:TextBox ID="txtActualCloseDate" CssClass="form-control" runat="server"></asp:TextBox>
                                        <asp:CalendarExtender ID="txtInvoiceDate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd MMM yyyy" TargetControlID="txtActualCloseDate">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        Date Shipped
                                        <asp:TextBox ID="txtDateShipped" CssClass="form-control" runat="server"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True"
                                            Format="dd MMM yyyy" TargetControlID="txtDateShipped">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        Shipping Provider
                                <asp:TextBox ID="txtShippingProvider" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>                               
                            </div>
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        Valid Till Date
                                <asp:TextBox ID="txtValidTillDate" CssClass="form-control" runat="server"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender6" runat="server" Enabled="True"
                                            Format="dd MMM yyyy" TargetControlID="txtValidTillDate">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>                                                            
                            </div>
                        </div>
                    </div>
                </div>           
            
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Quote Stage 
                        </div>
                        <div class="panel-body">                            
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        Currency Code
                                <asp:TextBox ID="txtCurrencyCode" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        Currency Name
                               <asp:TextBox ID="txtCurrencyName" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        Tax Rate
                                <asp:TextBox ID="txtTaxRate" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group has-error">
                                        Quote Stage
                                <asp:DropDownList ID="ddlQuoteStage" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                                    </div>
                                </div>
                            </div>                            
                        </div>
                    </div>
                </div>           
            
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Quote Line Item 
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
                                </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        Part Number
                                <asp:TextBox ID="txtPartnumber" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        Unit Price
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
                                        Quantity
                                <asp:TextBox ID="txtQuantity" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <br />
                                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-outline btn-success pull-right" OnClick="btnAdd_Click" OnClientClick="return ValidationItemAdd();" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvItem" DataKeyNames="ItemId" runat="server"
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
                                                    <asp:BoundField HeaderText="Quantity" DataField="Quantity" />
                                                    <asp:BoundField HeaderText="Part Number" DataField="PartNumber" />
                                                    <%--<asp:BoundField HeaderText="Discount" DataField="Discount" />--%>
                                                    <asp:BoundField HeaderText="Unit Price" DataField="UnitPrice" />
                                                    <asp:BoundField HeaderText="Discount" DataField="Discount" />
                                                
                                                    <asp:TemplateField ItemStyle-Width="15px">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnEdit" runat="server" class="fa fa-trash-o fa-fw" CommandName="D" CausesValidation="false"
                                                                CommandArgument='<%#Eval("ItemId") %>' Style="font-size: 16px;"></asp:LinkButton>
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
                                                    No Item Added...
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                               <div class="clearfix"></div>
                                    <div class="col-lg-12">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-outline btn-success" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-outline btn-warning" OnClick="btnCancel_Click" />
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <uc3:Message ID="Message" runat="server" />
                                    </div>
                            </div>
                        </div>

                    </div>
                </div>

                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Quote List
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <center>
                                    <asp:GridView ID="gvQuote" runat="server" Width="100%" AutoGenerateColumns="false" class="table table-striped"
                                        GridLines="None" AllowPaging="false" CellPadding="0" CellSpacing="0" DataKeyNames="Id" ForeColor="#333333" OnRowCommand="gvQuote_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL" ItemStyle-Width="15px">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="QuoteNumber" HeaderText="Quote Number" />
                                            <asp:BoundField DataField="PurchaseOrderNo" HeaderText="Purchase Order No" />
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Original PO Date
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# (Eval("OriginalPODate")==null)?"":Convert.ToDateTime(Eval("OriginalPODate").ToString()).ToString("dd MMM yyyy") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Actual Close Date
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# (Eval("ActualCloseDate")==null)?"":Convert.ToDateTime(Eval("ActualCloseDate").ToString()).ToString("dd MMM yyyy") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CurrencyCode" HeaderText="Currency Code" />
                                            <asp:BoundField DataField="CurrencyName" HeaderText="Currency Name" />
                                            <asp:TemplateField ItemStyle-Width="15px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnView" runat="server" ToolTip="View" class="fa fa-eye fa-fw" CommandName="View" CausesValidation="false"
                                                        CommandArgument='<%# Eval("Id") %>' Style="font-size: 16px;"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="15px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit" class="fa fa-pencil-square-o fa-fw" CommandName="Ed" CausesValidation="false"
                                                        CommandArgument='<%# Eval("Id") %>' Style="font-size: 16px;"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>                                            
                                        </Columns>
                                        <FooterStyle BackColor="#5bb0de" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#379ed6" Font-Bold="True" ForeColor="White" />
                                        <RowStyle CssClass="RowStyle" BackColor="#F7F6F3" ForeColor="#333333" />
                                        <EditRowStyle BackColor="#999999" />
                                        <EmptyDataRowStyle CssClass="EditRowStyle" />
                                        <AlternatingRowStyle CssClass="AltRowStyle" BackColor="White" ForeColor="#284775" />
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="12" FirstPageText="First"
                                            LastPageText="Last" />
                                        <PagerStyle CssClass="PagerStyle" BackColor="#379ed6" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <EmptyDataTemplate>
                                            No Record Found...
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </center>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
