<%@ Page Title="SPARE/CONSUMABLES" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Spare.aspx.cs" Inherits="WebAppAegisCRM.Inventory.Spare" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function validate() {
            if (document.getElementById("<%=txtSpareName.ClientID%>").value.trim() == "") {
                alert("Please Enter Spare Name");
                return false;
            }
            else
                if (document.getElementById("<%=txtYield.ClientID%>").value.trim() == "") {
                    alert("Please Enter Yield");
                    return false;
                }
                else
                    return true;
        }
    </script>
    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Add/Edit Spare/Consumables
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group has-error">
                                Spare/Consumable Name
                                <asp:TextBox ID="txtSpareName" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <!-- /.col-lg-6 (nested) -->
                        <div class="col-lg-6">
                            <div class="form-group has-error">
                                Yield
                                <asp:TextBox ID="txtYield" onkeypress="if(event.keyCode<48 || event.keyCode>57)event.returnValue=false;" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                Description
                                <asp:TextBox ID="txtDescription" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <br />
                                <br />
                                Toner
                            <asp:CheckBox ID="chkIsTonner" runat="server" />
                                <asp:Button ID="btnSave" OnClientClick="return validate();" runat="server" Text="Save" class="btn btn-outline btn-success pull-right extra-margin"
                                    OnClick="btnSave_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-outline btn-warning pull-right extra-margin"
                                    OnClick="btnCancel_Click" />
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <br />
                            <uc3:Message ID="Message" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Spare/Consumables List
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <center>
                            <div style="height: 50vh; overflow: scroll">
                                <asp:GridView ID="gvSpare" DataKeyNames="SpareId" runat="server" AutoGenerateColumns="false"
                                    Width="65%" CellPadding="4" EnableModelValidation="True" ForeColor="#333333"
                                    GridLines="None" class="table table-striped" OnRowCommand="gvSpare_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                SN.
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Spare/Consumable" DataField="SpareName" />
                                        <asp:BoundField HeaderText="Yield" DataField="Yield" />
                                        <asp:BoundField HeaderText="Toner" DataField="IsTonner" />
                                        <asp:TemplateField ShowHeader="false" HeaderStyle-Width="25px">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgEdit" runat="server" CausesValidation="false" CommandName="Ed"
                                                    ImageUrl="~/Images/edit_button.png" ImageAlign="AbsMiddle" ToolTip="EDIT" Width="20px"
                                                    Height="20px" CommandArgument='<%# Eval("SpareId") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle Width="25px"></HeaderStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="25px">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelete" runat="server" CommandName="Del" ImageUrl="~/Images/delete_button.png"
                                                    Width="20px" Height="20px" OnClientClick="return confirm('Are You Sure?');" CommandArgument='<%# Eval("SpareId") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle Width="25px"></HeaderStyle>
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
                            </div>
                        </center>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
