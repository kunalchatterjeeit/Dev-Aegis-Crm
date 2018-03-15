<%@ Page Title="MODEL CATEGORY" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ProductCategory.aspx.cs" Inherits="WebAppAegisCRM.Inventory.ProductCategory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function validate() {
            if (document.getElementById("<%=txtProductCategoryName.ClientID%>").value.trim() == "") {
                alert("Please Enter Product Category Name");
                return false;
            }
            else
                return true;
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <br />
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Add/Edit Model Category
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-4">
                            <div class="form-group has-error">
                                Model Category Name
                                <asp:TextBox ID="txtProductCategoryName" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <br />
                                <asp:Button ID="btnSave" OnClientClick="return validate();" runat="server" Text="Save" class="btn btn-outline btn-success"
                                    OnClick="btnSave_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-outline btn-warning"
                                    OnClick="btnCancel_Click" />
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <br />
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
                    Model Category List
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <center>
                            <asp:GridView ID="gvProductCategory" DataKeyNames="ProductCategoryId" runat="server" AutoGenerateColumns="false"
                                Width="30%" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" Style="text-align: left"
                                GridLines="None" class="table table-striped" OnRowCommand="gvProductCategory_RowCommand">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            SN.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Model Category Name" DataField="ProductCategoryName" />
                                    <asp:TemplateField ShowHeader="false" HeaderStyle-Width="25px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgEdit" runat="server" CausesValidation="false" CommandName="Ed"
                                                ImageUrl="~/Images/edit_button.png" ImageAlign="AbsMiddle" ToolTip="EDIT" Width="20px"
                                                Height="20px" CommandArgument='<%# Eval("ProductCategoryId") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle Width="25px"></HeaderStyle>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#0349AA" Font-Bold="True" ForeColor="White" />
                                <RowStyle CssClass="RowStyle" BackColor="#F7F6F3" ForeColor="#333333" />
                                <EditRowStyle BackColor="#999999" />
                                <EmptyDataRowStyle CssClass="EditRowStyle" />
                                <AlternatingRowStyle CssClass="AltRowStyle" BackColor="White" ForeColor="#284775" />
                                <PagerSettings Mode="NumericFirstLast" PageButtonCount="12" FirstPageText="First"
                                    LastPageText="Last" />
                                <PagerStyle CssClass="PagerStyle" BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
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
</asp:Content>
