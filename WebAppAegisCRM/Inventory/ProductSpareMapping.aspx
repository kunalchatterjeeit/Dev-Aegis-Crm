<%@ Page Title="Product Spare Mapping" Language="C#" MasterPageFile="~/Main.Master"
    AutoEventWireup="true" CodeBehind="ProductSpareMapping.aspx.cs" Inherits="WebAppAegisCRM.Inventory.ProductSpareMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function validate() {
            if (document.getElementById("<%=ddlProduct.ClientID%>").selectedIndex == 0) {
                alert("Please Select Model");
                return false;
            }
            else
                return true;
        }
    </script>
    <br />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                            Map Model Spare
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-5">
                                    <div class="form-group has-error">
                                        Model
                                        <asp:DropDownList ID="ddlProduct" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnShow" OnClientClick="return validate();" runat="server" Text="Show" class="btn btn-outline btn-success"
                                            OnClick="btnShow_Click" />&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnMap" runat="server" Text="Do Mapping" class="btn btn-outline btn-success"
                                            OnClick="btnMap_Click" />
                                    </div>
                                </div>
                                <div class="col-lg-5">
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
                            Spare List
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <div style="height: 60vh; overflow: scroll">
                                    <center>
                                        <asp:GridView ID="gvSpare" DataKeyNames="SpareId" runat="server" AutoGenerateColumns="false"
                                            Width="50%" CellPadding="4" EnableModelValidation="True" ForeColor="#333333"
                                            GridLines="None" AllowSorting="True" class="table table-striped">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkMap" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        SN.
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Spare" DataField="SpareName" />
                                                <asp:BoundField HeaderText="Yield" DataField="Yield" />
                                            </Columns>
                                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#0349AA" Font-Bold="True" ForeColor="White" />
                                            <RowStyle CssClass="RowStyle" BackColor="#F7F6F3" ForeColor="#333333" />
                                            <EditRowStyle BackColor="#999999" />
                                            <EmptyDataRowStyle CssClass="EditRowStyle" />
                                            <AlternatingRowStyle CssClass="AltRowStyle" BackColor="White" ForeColor="#284775" />
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
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
