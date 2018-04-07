<%@ Page Title="CONTRACT TYPE" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Contract.aspx.cs" Inherits="WebAppAegisCRM.Customer.Contract" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function validate() {
            if (document.getElementById("<%=txtContractName.ClientID%>").value.trim() == "") {
                alert("Please Enter Contract Type Name");
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
                    Add/Edit Contract Type
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group has-error">
                                Contract Type Name
                                <asp:TextBox ID="txtContractName" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div style="clear: both"></div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                Description
                                <asp:TextBox ID="txtDescription" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                            </div>
                        </div>
                        <br />
                        <div class="col-lg-6">
                            <uc3:Message ID="Message" runat="server" />
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
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Contract Type List
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <center>
                            <asp:GridView ID="gvContract" DataKeyNames="ContractTypeId" runat="server" AutoGenerateColumns="false"
                                Width="60%" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" Style="text-align: left"
                                GridLines="None" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvContract_PageIndexChanging"
                                PageSize="12" class="table table-striped" OnRowCommand="gvContract_RowCommand">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            SN.
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <%# Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Contract Type" DataField="ContractName" />
                                    <asp:BoundField HeaderText="Description" DataField="Description" />
                                    <asp:TemplateField ShowHeader="false" HeaderStyle-Width="25px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImgEdit" runat="server" CausesValidation="false" CommandName="Ed"
                                                ImageUrl="~/Images/edit_button.png" ImageAlign="AbsMiddle" ToolTip="EDIT" Width="20px"
                                                Height="20px" CommandArgument='<%# Eval("ContractTypeId") %>' />
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
                        </center>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
