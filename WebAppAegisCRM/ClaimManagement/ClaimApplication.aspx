<%@ Page Title="CLAIM APPLICATION" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ClaimApplication.aspx.cs" Inherits="WebAppAegisCRM.ClaimManagement.ClaimApplication" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <br />
    <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1">
        <progresstemplate>
            <div class="divWaiting">
                <div class="loading">
                    <div class="loading-bar"></div>
                    <div class="loading-bar"></div>
                    <div class="loading-bar"></div>
                    <div class="loading-bar"></div>
                </div>
            </div>
        </progresstemplate>
    </asp:UpdateProgress>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <contenttemplate>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Claim General Information
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group has-error">
                                        Claim Header
                                        <asp:TextBox ID="txtClaimHeader" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group has-error">
                                        Period From
                                        <asp:TextBox ID="txtPeriodFrom" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPeriodFrom"
                                            Format="dd MMM yyyy" Enabled="True">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group has-error">
                                        Period To
                                        <asp:TextBox ID="txtPeriodTo" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtPeriodTo"
                                            Format="dd MMM yyyy" Enabled="True">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group has-success">
                                        Advance Balance:
                                    <asp:TextBox ID="txtAdvanceAmount" runat="server" CssClass="form-control pull-right text-right" Style="font-style: italic; background-color: #b9e8b9" Enabled="false"></asp:TextBox>
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
                            Claim Details
                        </div>
                        <div class="panel-body">
                            <div class="col-lg-2">
                                <div class="form-group has-error">
                                    Expense Date
                                        <asp:TextBox ID="txtExpenseDate" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtExpenseDate"
                                        Format="dd MMM yyyy" Enabled="True">
                                    </asp:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group has-error">
                                    Category
                                        <asp:DropDownList ID="ddlCategory" runat="server" CssClass="form-control searchable">
                                        </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-lg-1">
                                <div class="form-group has-error">
                                    Cost
                                        <asp:TextBox ID="txtCost" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-3">
                                <div class="form-group has-error">
                                    Description
                                        <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    Attachment
                                       <asp:FileUpload ID="fuAttachment" runat="server" Style="border: 0px" />
                                </div>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <br />
                                    <asp:Button ID="btnAdd" runat="server" Text="Add" class="btn btn-outline btn-success pull-right"
                                        OnClick="btnAdd_Click" />
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="table-responsive">
                                <center>
                                    <asp:GridView ID="gvClaimDetails" runat="server" Width="100%" AutoGenerateColumns="false" class="table table-striped"
                                        GridLines="None" AllowPaging="false" CellPadding="0" CellSpacing="0" ForeColor="#333333" OnRowCommand="gvClaimDetails_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL" ItemStyle-Width="15px">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ExpenseDate" HeaderText="Expense Date" />
                                            <asp:BoundField DataField="CategoryName" HeaderText="Category" />
                                            <asp:BoundField DataField="Status" HeaderText="Status" />
                                            <asp:BoundField DataField="Cost" HeaderText="Cost" />
                                            <asp:TemplateField ItemStyle-Width="15px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnDelete" runat="server" class="fa fa-trash-o fa-fw" CausesValidation="false"
                                                        CommandName="D" OnClientClick="return confirm('Are You Sure?');" CommandArgument='<%# Eval("AutoId") %>'></asp:LinkButton>
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
                            <div class="col-lg-3 pull-right">
                                <div class="form-group has-success">
                                    Adjust Advance:
                                    <asp:TextBox ID="txtAdjustAdvance" runat="server" CssClass="form-control pull-right text-right" Style="font-style: italic; background-color: #b9e8b9" Text="0.00"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-lg-12">
                                <div class="form-group">
                                    <br />
                                    <asp:Button ID="btnSave" runat="server" Text="Submit Request" class="btn btn-outline btn-success"
                                        OnClick="btnSave_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel Request" class="btn btn-outline btn-warning"
                                        OnClick="btnCancel_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </contenttemplate>
        <triggers>
            <asp:PostBackTrigger ControlID="btnAdd" />
        </triggers>
    </asp:UpdatePanel>
</asp:Content>

