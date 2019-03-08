<%@ Page Title="CUSTOMER PURCHASE ASSIGN ENGINEER" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="CustomerPurchaseAssignEngineer.aspx.cs" Inherits="WebAppAegisCRM.Customer.CustomerPurchaseAssignEngineer" %>

<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
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
                <br />
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Search Criteria
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="form-group has-error">
                                        Customer
                                        <asp:DropDownList ID="ddlCustomer" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group has-error">
                                        Assigned Engineer
                                        <asp:DropDownList ID="ddlAssignedEngineer" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group has-error">
                                        Serial No
                                        <asp:TextBox ID="txtSerialNo" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <br />
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-outline btn-success pull-left extra-margin "
                                        OnClick="btnSearch_Click" />
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
                            Customer Purchase List
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-8">
                                    <span class="red">Please select checkbox in list to change Assign Engineer and click on Save Assignment to commit changes.</span>
                                </div>
                                <div class="col-lg-4">
                                    <asp:Button ID="btnSaveAssignment" runat="server" Text="Save Assignment" CssClass="btn btn-outline btn-danger pull-right extra-margin "
                                        OnClick="btnSaveAssignment_Click" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="table-responsive">
                                    <div class="col-lg-12">
                                        <asp:GridView ID="gvCustomerPurchase" DataKeyNames="CustomerPurchaseId" runat="server"
                                            AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333" AllowPaging="true" AllowCustomPaging="true" PageSize="20"
                                            Style="text-align: left" GridLines="None" class="table table-striped" OnRowDataBound="gvCustomerPurchase_RowDataBound"
                                            OnPageIndexChanging="gvCustomerPurchase_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        SN.
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#  (gvCustomerPurchase.PageIndex * gvCustomerPurchase.PageSize) + (Container.DataItemIndex + 1) %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Customer Name
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <span title='<%# Eval("CustomerName") %>'>
                                                            <%# (Eval("CustomerName").ToString().Length>30)?Eval("CustomerName").ToString().Substring(0,30)+"...":Eval("CustomerName").ToString() %>
                                                        </span>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Brand" DataField="BrandName" />
                                                <asp:BoundField HeaderText="Product Name" DataField="ProductName" />
                                                <asp:BoundField HeaderText="Serial No" DataField="ProductSerialNo" />
                                                <asp:BoundField HeaderText="Machine Id" DataField="MachineId" />
                                                <asp:BoundField HeaderText="Contact Person" DataField="ContactPerson" />
                                                <asp:BoundField HeaderText="Assigned Engineer" DataField="AssignedEngineerName" />
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Select Engineer To Assign
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlAssignedEngineer" runat="server" Enabled="false" CssClass="form-control"></asp:DropDownList>
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
