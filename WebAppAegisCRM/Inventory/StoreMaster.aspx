<%@ Page Title="ADD/EDIT STORE MASTER" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="StoreMaster.aspx.cs" Inherits="WebAppAegisCRM.Inventory.StoreMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function validate() {
            if (document.getElementById("<%=txtStoreName.ClientID%>").value.trim() == "") {
                alert("Please Enter Store Name");
                document.getElementById("<%=txtStoreName.ClientID %>").focus();
                return false;
            }
            if (document.getElementById("<%=txtPhone.ClientID%>").value.trim() == "") {
                alert("Please Enter Phone");
                document.getElementById("<%=txtPhone.ClientID %>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlContactPerson.ClientID %>").selecedIndex == 0) {
                alert('Select Contact Person');
                document.getElementById("<%=ddlContactPerson.ClientID %>").focus();
                return false;
            }
            if (document.getElementById("<%=txtLocation.ClientID%>").value.trim() == "") {
                alert("Please Enter Location");
                document.getElementById("<%=txtLocation.ClientID %>").focus();
                return false;
            }

            return true;
        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <br />
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Add/Edit Store Master
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group has-error">
                                Store Name
                                <asp:TextBox ID="txtStoreName" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group has-error">
                                Phone
                                <asp:TextBox ID="txtPhone" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group has-error">
                                Contact Person
                                <asp:DropDownList ID="ddlContactPerson" runat="server" CssClass="form-control searchable"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group has-error">
                                Location
                                <asp:TextBox ID="txtLocation" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <br />
                                <asp:Button ID="btnSave" OnClientClick="return validate();" runat="server" Text="Save" class="btn btn-outline btn-success"
                                    OnClick="btnSave_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-outline btn-warning"
                                    OnClick="btnCancel_Click" />
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
                    Store List
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <center>
                            <asp:GridView ID="gvStoreList" DataKeyNames="StoreId" runat="server" AutoGenerateColumns="false"
                                CellPadding="4" EnableModelValidation="True" ForeColor="#333333" Style="text-align: left"
                                GridLines="None" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvStoreList_PageIndexChanging"
                                PageSize="12" class="table table-striped" OnRowCommand="gvStoreList_RowCommand">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            SN.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Store Name" DataField="StoreName" />
                                    <asp:BoundField HeaderText="Phone" DataField="Phone" />
                                    <asp:BoundField HeaderText="Contact Person" DataField="ContactPerson" />
                                    <asp:TemplateField ItemStyle-Width="15px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEdit" runat="server" class="fa fa-edit fa-fw" CommandName="Ed" CausesValidation="false"
                                                CommandArgument='<%# Eval("StoreId") %>' Style="font-size: 16px;"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnDelete" runat="server" class="fa fa-trash-o fa-fw" CausesValidation="false"
                                                CommandName="Del" OnClientClick="return confirm('Are You Sure?');" Style="font-size: 16px;"
                                                CommandArgument='<%# Eval("StoreId") %>'></asp:LinkButton>
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
</asp:Content>
