<%@ Page Title="Customer List" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CustomerList.aspx.cs" Inherits="WebAppAegisCRM.Customer.CustomerList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                            Customer List
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <div style="height: 80vh; overflow: scroll">
                                    <asp:GridView ID="gvCustomerMaster" DataKeyNames="CustomerMasterId" runat="server"
                                        AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                                        class="table table-striped" GridLines="None" Style="text-align: left">
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    SN.
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Customer ID" DataField="CustomerCode" />
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Name
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <span title='<%# Eval("CustomerName") %>'>
                                                        <%# (Eval("CustomerName").ToString().Length>30)?Eval("CustomerName").ToString().Substring(0,30)+"...":Eval("CustomerName").ToString() %>
                                                    </span>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Customer Type">
                                                <ItemTemplate>
                                                    <%# ((int)Eval("CustomerType") == (int)Business.Common.Constants.CustomerType.APlus)? "A+": (((int)Eval("CustomerType") == (int)Business.Common.Constants.CustomerType.A)? "A" : (((int)Eval("CustomerType") == (int)Business.Common.Constants.CustomerType.B)? "B" : "N/A")) %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Mobile No." DataField="MobileNo" />
                                            <asp:BoundField HeaderText="Email" DataField="EmailId" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
