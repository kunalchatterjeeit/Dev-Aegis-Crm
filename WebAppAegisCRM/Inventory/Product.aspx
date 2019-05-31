<%@ Page Title="MODEL MASTER" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="Product.aspx.cs" Inherits="WebAppERPNew.Inventory.Product" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function validation() {
            if (document.getElementById("<%=ddlBrand.ClientID %>").selecedIndex == 0) {
                alert('Select Brand');
                document.getElementById("<%=ddlBrand.ClientID %>").focus();
                return false;
            }
            if (document.getElementById("<%=txtProductName.ClientID %>").value == '') {
                alert('Enter Model Name');
                document.getElementById("<%=txtProductName.ClientID %>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlProductCategory.ClientID %>").selecedIndex == 0) {
                alert('Select Product Category');
                document.getElementById("<%=ddlProductCategory.ClientID %>").focus();
                return false;
            }
            if (document.getElementById("<%=txtMachineLife.ClientID %>").value == '') {
                alert('Enter Machine Life');
                document.getElementById("<%=txtMachineLife.ClientID %>").focus();
                return false;
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Add/Edit Model
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group has-error">
                                Brand
                                <asp:DropDownList ID="ddlBrand" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group has-error">
                                Model Category
                                <asp:DropDownList ID="ddlProductCategory" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group has-error">
                                Model Code
                                <asp:TextBox ID="txtProductCode" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <!-- /.col-lg-6 (nested) -->
                        <div class="col-lg-6">
                            <div class="form-group has-error">
                                Model Name
                                <asp:TextBox ID="txtProductName" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group has-error">
                                MCBF
                                <asp:TextBox ID="txtMCBF" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group has-error">
                                MTBF
                                <asp:TextBox ID="txtMTBF" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                Model Specification
                                <asp:TextBox ID="txtProductSpecification" runat="server" CssClass="form-control"
                                    TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group has-error">
                                Machine Life
                                <asp:TextBox ID="txtMachineLife" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-outline btn-success pull-right extra-margin"
                                    OnClick="btnSave_Click" OnClientClick="return validation()" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-outline btn-warning pull-right extra-margin"
                                    OnClick="btnCancel_Click" />
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <uc3:Message ID="Message" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Model List
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <div style="height: 50vh; overflow: scroll">
                            <asp:GridView ID="gvProductMaster" DataKeyNames="ProductMasterId" runat="server"
                                AutoGenerateColumns="false" Width="100%" CellPadding="4" EnableModelValidation="True"
                                ForeColor="#333333" GridLines="None" OnRowCommand="gvProductMaster_RowCommand" class="table table-striped">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            SN.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Brand" DataField="BrandName" />
                                    <asp:BoundField HeaderText="Model Category Name" DataField="ProductCategoryName" />
                                    <asp:BoundField HeaderText="Model Code" DataField="ProductCode" />
                                    <asp:BoundField HeaderText="Model Name" DataField="ProductName" />
                                    <asp:BoundField HeaderText="Machine Life" DataField="MachineLife" />
                                    <asp:BoundField HeaderText="MCBF" DataField="MCBF" />
                                    <asp:BoundField HeaderText="MTBF" DataField="MTBF" />

                                    <asp:TemplateField ItemStyle-Width="15px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" class="fa fa-edit fa-fw" CommandName="Ed" CausesValidation="false"
                                                CommandArgument='<%# Eval("ProductMasterId") %>' Style="font-size: 16px;"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnDelete" runat="server" class="fa fa-trash-o fa-fw" CausesValidation="false"
                                                CommandName="Del" OnClientClick="return confirm('Are You Sure?');" Style="font-size: 16px;"
                                                CommandArgument='<%# Eval("ProductMasterId") %>'></asp:LinkButton>
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
</asp:Content>
