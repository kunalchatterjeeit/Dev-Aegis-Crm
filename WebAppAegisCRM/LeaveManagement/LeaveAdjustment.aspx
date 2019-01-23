<%@ Page Title="LEAVE ADJUSTMENT" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="LeaveAdjustment.aspx.cs" Inherits="WebAppAegisCRM.LeaveManagement.LeaveAdjustment" %>

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
                    <uc3:Message ID="Message" runat="server" />
                </div>
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Employee List
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <asp:GridView ID="gvEmployeerMaster" DataKeyNames="EmployeeMasterId" runat="server"
                                    AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                                    GridLines="None" Style="text-align: left" OnRowCommand="gvEmployeerMaster_RowCommand"
                                    HeaderStyle-HorizontalAlign="Center">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                SN.
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <img src='<%# Eval("Image") %>' alt="Not found" width="100px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Code" DataField="EmployeeCode" />
                                        <asp:BoundField HeaderText="Name" DataField="EmployeeName" />
                                        <asp:BoundField HeaderText="Designation" DataField="DesignationName" />
                                        <asp:BoundField HeaderText="Mobile" DataField="PersonalMobileNo" />
                                        <asp:BoundField HeaderText="Email" DataField="PersonalEmailId" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk" runat="server" CommandArgument='<%# Eval("EmployeeMasterId") %>'
                                                    CommandName="Leave">Update</asp:LinkButton>
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
            <a id="lnkLeave" runat="server"></a>
            <asp:ModalPopupExtender ID="ModalPopupExtender1" BackgroundCssClass="myModalPopupbackGrnd"
                runat="server" TargetControlID="lnkLeave" PopupControlID="Panel1" CancelControlID="imgbtn">
            </asp:ModalPopupExtender>
            <asp:Panel ID="Panel1" runat="server" CssClass="myModalPopup-8" Style="display: none; z-index: 10000; position: absolute">
                <asp:Panel ID="dragHandler" runat="server" class="popup-working-section" ScrollBars="Auto">
                    <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" CssClass="MyTabStyle"
                        ActiveTabIndex="1">
                        <asp:TabPanel ID="LeaveUpdate" runat="server">
                            <HeaderTemplate>
                                Leave Balance Adjust
                            </HeaderTemplate>
                            <ContentTemplate>
                                <div class="accountInfo" style="width: 100%; float: left">
                                    <br />
                                    <fieldset class="login">
                                        <legend>Enter leave update details</legend>
                                        <table class="popup-table">
                                            <tr>
                                                <td style="font-weight: bold">Current Leave Balance
                                                </td>
                                                <td colspan="3">
                                                    <div style="height: 20vh; overflow: scroll">
                                                        <div class="panel-body">
                                                            <div class="table-responsive">
                                                                <asp:GridView ID="gvCurrentLeaveBalance" runat="server"
                                                                    AutoGenerateColumns="False" Width="100%"
                                                                    CellPadding="4" ForeColor="#333333" GridLines="None" Style="text-align: left">
                                                                    <Columns>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                SN.
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <%# Container.DataItemIndex+1 %>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="LeaveTypeName" HeaderText="Leave Type" />
                                                                        <asp:BoundField DataField="Amount" HeaderText="Available" />
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
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Leave Type <span class="mandatory">*</span>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlLeaveType" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Operation <span class="mandatory">*</span>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlOperation" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="1" Text="Add"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Substract"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Leave Amount <span class="mandatory">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLeaveAmount" CssClass="form-control" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Comments <span class="mandatory">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtComments" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Button ID="btnLeaveAdjust" runat="server" Text="Save" OnClick="btnLeaveAdjust_Click" CssClass="btn btn-outline btn-success pull-right" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <uc3:Message ID="Message1" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel ID="LeaveDetails" runat="server">
                            <HeaderTemplate>
                                Leave Balance Details
                            </HeaderTemplate>
                            <ContentTemplate>
                                <br />
                                <div class="panel-body">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvLeaveBalanceDetails" runat="server"
                                            AutoGenerateColumns="False" Width="100%"
                                            CellPadding="4" ForeColor="#333333" GridLines="None" Style="text-align: left">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        SN.
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="LeaveTypeName" HeaderText="Leave Type" />
                                                <asp:BoundField DataField="AccumulatedAmount" HeaderText="Available" />
                                                <asp:BoundField DataField="AccumulatedOn" HeaderText="Updated On" />
                                                <asp:BoundField DataField="Reason" HeaderText="Reason" />
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
                            </ContentTemplate>
                        </asp:TabPanel>
                    </asp:TabContainer>
                </asp:Panel>
                <img id="imgbtn" runat="server" src="../images/close-button.png" alt="Close" class="popup-close" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
