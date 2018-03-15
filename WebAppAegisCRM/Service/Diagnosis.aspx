<%@ Page Title="PROBLEM OBSERVED" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Diagnosis.aspx.cs" Inherits="WebAppAegisCRM.Service.Diagnosis" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function validate() {
            if (document.getElementById("<%=txtName.ClientID%>").value.trim() == "") {
                alert("Please enter name");
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
                    Add/Edit Diagnosis
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-4">
                            <div class="form-group has-error">
                                Name
                                <asp:TextBox ID="txtName" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-2">
                            <div class="form-group">
                                <br />
                                <asp:Button ID="btnSave" OnClientClick="return validate();" runat="server" Text="Save" class="btn btn-outline btn-success"
                                    OnClick="btnSave_Click" />
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
                    List
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <center>
                            <asp:GridView ID="gvDiagonosis" DataKeyNames="Id" runat="server" AutoGenerateColumns="false"
                                Width="50%" CellPadding="4" EnableModelValidation="True" ForeColor="#333333"
                                GridLines="None" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvDiagonosis_PageIndexChanging"
                                PageSize="12" class="table table-striped" OnRowCommand="gvDiagonosis_RowCommand">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            SN.</HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Name" DataField="Name" />
                                    <asp:TemplateField ShowHeader="false" HeaderStyle-Width="25px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgEdit" runat="server" CausesValidation="false" CommandName="Ed"
                                                ImageUrl="~/Images/edit_button.png" ImageAlign="AbsMiddle" ToolTip="EDIT" Width="20px"
                                                Height="20px" CommandArgument='<%# Eval("Id") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle Width="25px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="25px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" CommandName="Del" ImageUrl="~/Images/delete_button.png"
                                                Width="20px" Height="20px" OnClientClick="return confirm('Are You Sure?');" CommandArgument='<%# Eval("Id") %>' />
                                        </ItemTemplate>
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
