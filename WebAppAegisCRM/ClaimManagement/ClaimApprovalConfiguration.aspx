<%@ Page Title="CLAIM APPROVAL CONFIGURATION" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ClaimApprovalConfiguration.aspx.cs" Inherits="WebAppAegisCRM.ClaimManagement.ClaimApprovalConfiguration" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
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
                            Add/Edit Leave Approval Configuration
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Requestor Designation
                                        <asp:DropDownList ID="ddlRequestorDesignation" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Approver Designation
                                        <asp:DropDownList ID="ddlApproverDesignation" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Approval Level
                                         <asp:DropDownList ID="ddlApprovalLevel" CssClass="form-control" runat="server">
                                             <asp:ListItem Value="0">--Select--</asp:ListItem>
                                             <asp:ListItem Value="1">1</asp:ListItem>
                                             <asp:ListItem Value="2">2</asp:ListItem>
                                             <asp:ListItem Value="3">3</asp:ListItem>
                                             <asp:ListItem Value="4">4</asp:ListItem>
                                             <asp:ListItem Value="5">5</asp:ListItem>
                                         </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-outline btn-success" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-outline btn-warning" OnClick="btnCancel_Click" />
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <uc3:Message ID="Message" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Leave Approval Configuration List
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <asp:GridView ID="gvClaimApprovalConfiguration" runat="server"
                                    AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                                    GridLines="None" Style="text-align: left" OnRowCommand="gvClaimApprovalConfiguration_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                SN.
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="RequestorDesignation" HeaderText="Requestor Designation" />
                                        <asp:BoundField DataField="ApproverDesignation" HeaderText="Approver Designation" />
                                        <asp:BoundField DataField="ApproverLevel" HeaderText="Approval Level" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgEdit" runat="server" CausesValidation="false" CommandName="E"
                                                    CommandArgument='<%# Eval("LeaveApprovalConfigId") %>' ImageUrl="~/Images/edit_button.png"
                                                    ImageAlign="AbsMiddle" ToolTip="EDIT" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgDelete" runat="server" CausesValidation="false" CommandName="D"
                                                    CommandArgument='<%# Eval("LeaveApprovalConfigId") %>' ImageUrl="~/images/delete_button.png"
                                                    ImageAlign="AbsMiddle" ToolTip="Delete" Width="20px" Height="20px" OnClientClick="return confirm('Are You Sure?');" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#5bb0de" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5bc0de" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
