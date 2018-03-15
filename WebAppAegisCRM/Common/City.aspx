<%@ Page Title="CITY" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="City.aspx.cs" Inherits="WebAppAegisCRM.Common.City" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function validate() {
            if (document.getElementById("<%=txtCity.ClientID%>").value.trim() == "") {
                alert("Please Enter City");
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
                    Add/Edit City
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                State
                                <asp:DropDownList ID="ddlState" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="1" Selected="True">West Bengal</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <!-- /.col-lg-6 (nested) -->
                        <div class="col-lg-6">
                            <div class="form-group">
                                District
                                <asp:DropDownList ID="ddlDistrict" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="1" Selected="True">Kolkata</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group has-error">
                                City
                                <asp:TextBox ID="txtCity" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                STD
                                <asp:TextBox ID="txtSTD" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <uc3:Message ID="Message" runat="server" />
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <asp:Button ID="btnSave" runat="server" OnClientClick="return validate();" Text="Save" class="btn btn-outline btn-success"
                                    OnClick="btnSave_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-outline btn-warning"
                                    OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    City List
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <center>
                            <asp:GridView ID="gvCity" DataKeyNames="CityId" runat="server" AutoGenerateColumns="false"
                                Width="50%" CellPadding="4" EnableModelValidation="True" ForeColor="#333333"
                                GridLines="None" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvCity_PageIndexChanging"
                                PageSize="12" class="table table-striped" OnRowCommand="gvCity_RowCommand">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            SN.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="City" DataField="CityName" />
                                    <asp:BoundField HeaderText="District" DataField="DistrictName" />
                                    <asp:BoundField HeaderText="STD" DataField="STD" />
                                    <asp:TemplateField ShowHeader="false" HeaderStyle-Width="25px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgEdit" runat="server" CausesValidation="false" CommandName="Ed"
                                                ImageUrl="~/Images/edit_button.png" ImageAlign="AbsMiddle" ToolTip="EDIT" Width="20px"
                                                Height="20px" CommandArgument='<%# Eval("CityId") %>' />
                                        </ItemTemplate>
                                        <HeaderStyle Width="25px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderStyle-Width="25px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" CommandName="Del" ImageUrl="~/Images/delete_button.png"
                                                Width="20px" Height="20px" OnClientClick="return confirm('Are You Sure?');" CommandArgument='<%# Eval("CityId") %>' />
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
