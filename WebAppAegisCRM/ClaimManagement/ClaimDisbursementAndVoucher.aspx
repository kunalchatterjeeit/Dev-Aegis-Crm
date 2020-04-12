<%@ Page Title="CLAIM DISBURSEMENT" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="ClaimDisbursementAndVoucher.aspx.cs" Inherits="WebAppAegisCRM.ClaimManagement.ClaimDisbursementAndVoucher" %>

<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function checkAll(gvExample, colIndex) {
            var GridView = gvExample.parentNode.parentNode.parentNode;
            for (var i = 1; i < GridView.rows.length; i++) {
                var chb = GridView.rows[i].cells[colIndex].getElementsByTagName("input")[0];
                chb.checked = gvExample.checked;
            }
            showPay(gvExample, colIndex);
        }

        function checkItem_All(objRef, colIndex) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var selectAll = GridView.rows[0].cells[colIndex].getElementsByTagName("input")[0];
            if (!objRef.checked) {
                selectAll.checked = false;
            }
            else {
                var checked = true;
                for (var i = 1; i < GridView.rows.length; i++) {
                    var chb = GridView.rows[i].cells[colIndex].getElementsByTagName("input")[0];
                    if (!chb.checked) {
                        checked = false;
                        break;
                    }
                }
                selectAll.checked = checked;
            }
            showPay(objRef, colIndex);
        }

        function showPay(objRef, colIndex) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            for (var i = 1; i < GridView.rows.length; i++) {
                var chb = GridView.rows[i].cells[colIndex].getElementsByTagName("input")[0];
                if (chb.checked) {
                    $("#ContentPlaceHolder1_btnPay").removeClass("hide");
                    break;
                }
                else {
                    $("#ContentPlaceHolder1_btnPay").addClass("hide");
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <br />
    <uc3:Message ID="MessageSuccess" runat="server" />
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Claim Search Criteria
                </div>
                <div class="panel-body">
                    <div class="col-lg-3">
                        <div class="form-group">
                            Employee :
                            <asp:DropDownList ID="ddlEmployee" CssClass="form-control searchable" runat="server">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-lg-3">
                        <div class="form-group">
                            Claim Application From Date
                            <asp:TextBox ID="txtFromClaimDate" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True"
                                Format="dd MMM yyyy" TargetControlID="txtFromClaimDate">
                            </asp:CalendarExtender>
                        </div>
                    </div>
                    <div class="col-lg-3">
                        <div class="form-group">
                            Claim Application To Date
                            <asp:TextBox ID="txtToClaimDate" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True"
                                Format="dd MMM yyyy" TargetControlID="txtToClaimDate">
                            </asp:CalendarExtender>
                        </div>
                    </div>
                    <div class="col-lg-1">
                        <div class="form-group">
                            <br />
                            <asp:Button ID="btnSearch" runat="server" Text="Search" class="btn btn-outline btn-success" OnClick="btnSearch_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Approved Claim List
                </div>
                <div class="panel-body">
                    <asp:Button ID="btnPay" runat="server" Text="Pay" class="btn btn-success hide" OnClick="btnPay_Click" Style="width: 100%; margin: 5px 0px" />
                    <div class="table-responsive">
                        <asp:GridView ID="gvApprovedClaim" runat="server"
                            AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                            GridLines="None" Style="text-align: left" OnRowCommand="gvClaimApprovalList_RowCommand"
                            DataKeyNames="ClaimId">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this,0);" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkitem" runat="server" onclick="checkItem_All(this,0)" />
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
                                <asp:BoundField DataField="ClaimNo" HeaderText="Application Number" />
                                <asp:BoundField DataField="EmployeeName" HeaderText="Name" />
                                <asp:BoundField DataField="ClaimDateTime" HeaderText="Claim Date" />
                                <asp:BoundField DataField="PeriodFrom" HeaderText="Period From" />
                                <asp:BoundField DataField="PeriodTo" HeaderText="Period To" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Claimed
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        &#8377;<%# Eval("TotalAmount") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Approved
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        &#8377;<%# Eval("ApprovedAmount") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btnView" runat="server" Text="View" CommandName="View" CommandArgument='<%# Eval("ClaimId") %>' CssClass="btn btn-outline btn-info" Style="margin: 2px" />
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
                                No Pending Approval...
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <a id="lnkClaim" runat="server"></a>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" BackgroundCssClass="myModalPopupbackGrnd"
        runat="server" TargetControlID="lnkClaim" PopupControlID="Panel1" CancelControlID="imgbtn">
        <Animations>
                 <OnShown><Fadein Duration="0.50" /></OnShown>
        </Animations>
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" CssClass="myModalPopup-20" Style="display: none; z-index: 10000; position: absolute">
        <asp:Panel ID="dragHandler" runat="server" class="popup-working-section" ScrollBars="Auto">
            <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" CssClass="MyTabStyle"
                ActiveTabIndex="0">
                <asp:TabPanel ID="Approval" runat="server">
                    <HeaderTemplate>
                        Payment Details
                    </HeaderTemplate>
                    <ContentTemplate>
                        <div class="accountInfo" style="width: 100%; float: left">
                            <br />
                            <fieldset class="login">
                                <uc3:Message ID="Message" runat="server" />
                                <table class="popup-table">
                                    <tr>
                                        <td style="font-weight: bold">Total Claim Amount <span class="pull-right">&#8377;</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTotalClaimAmount" runat="server" CssClass="form-control pull-right text-right" Style="font-style: italic; background-color: #b9e8b9; width: 100px"></asp:Label>
                                        </td>
                                        <td style="font-weight: bold">Total Approved Amount <span class="pull-right">&#8377;</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTotalApprovedAmount" runat="server" CssClass="form-control pull-right text-right" Style="font-style: italic; background-color: #b9e8b9; width: 100px"></asp:Label>
                                        </td>
                                        <td style="font-weight: bold">Advance Balance <span class="pull-right">&#8377;</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblAdvanceBalance" runat="server" CssClass="form-control pull-right text-right" Style="font-style: italic; background-color: #b9e8b9; width: 100px"></asp:Label>
                                        </td>
                                        <td style="font-weight: bold">Advance Adjust of <span class="pull-right">&#8377;</span>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblAdvanceAdjustAmount" runat="server" CssClass="form-control pull-right text-right" Style="font-style: italic; background-color: #b9e8b9; width: 100px"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-weight: bold">Mode of payment
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlPaymentModes" runat="server" class="form-control pull-right" Style="width: 100px"></asp:DropDownList>
                                        </td>
                                        <td style="font-weight: bold">Amount <span class="pull-right">&#8377;</span>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAmount" runat="server" CssClass="form-control text-right pull-right" Style="font-style: italic; background-color: #b9e8b9; width: 100px" Text="0.00"></asp:TextBox>
                                        </td>
                                        <td style="font-weight: bold">Payment details
                                        </td>
                                        <td colspan="2">
                                            <asp:TextBox ID="txtPaymentDetails" runat="server" CssClass="form-control pull-right" Style="width: 93%"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="btn btn-outline btn-success pull-right" OnClick="btnAdd_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8">
                                            <div style="height: 37vh; overflow: scroll">
                                                <div class="panel-body">
                                                    <div class="table-responsive">
                                                        <asp:GridView ID="gvPaymentDetails" runat="server" OnRowCommand="gvPaymentDetails_RowCommand"
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
                                                                <asp:BoundField HeaderText="Mode" DataField="PaymentModeName" />
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        Amount
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        &#8377;<%# Eval("Amount") %>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="Payment Details" DataField="PaymentDetails" />
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
                                        <td style="font-weight: bold; font-size: 14px">Total amount paying <span class="pull-right">&#8377;</span>
                                        </td>
                                        <td colspan="7">
                                            <asp:Label ID="lblTotalAmountPaying" runat="server" CssClass="form-control pull-right text-right" Style="font-style: italic; background-color: #b9e8b9; width: 100px; font: bolder; font-size: 14px" Text="0.00"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="8">
                                            <asp:Button ID="btnGenerateVoucher" runat="server" Text="Generate Voucher" CssClass="btn btn-success" OnClick="btnGenerateVoucher_Click" Style="width: 100%" />
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </div>
                    </ContentTemplate>
                </asp:TabPanel>
                <asp:TabPanel ID="ApprovalHistory" runat="server">
                    <HeaderTemplate>
                        Vouchers
                    </HeaderTemplate>
                    <ContentTemplate>
                        <br />
                        <div class="panel-body">
                            <div class="table-responsive">
                                <asp:GridView ID="gvApprovalHistory" runat="server"
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
                                        <asp:BoundField HeaderText="Approver" DataField="ApproverName" />
                                        <asp:BoundField HeaderText="Date" DataField="ActionDate" />
                                        <asp:BoundField HeaderText="Status" DataField="StatusName" />
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
    <a id="lnkClaimDetails" runat="server"></a>
    <asp:ModalPopupExtender ID="ModalPopupExtender2" BackgroundCssClass="myModalPopupbackGrnd"
        runat="server" TargetControlID="lnkClaimDetails" PopupControlID="Panel2" CancelControlID="img1">
        <Animations>
                 <OnShown><Fadein Duration="0.50" /></OnShown>
        </Animations>
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel2" runat="server" CssClass="myModalPopup-20" Style="display: none; z-index: 10000; position: absolute">
        <asp:Panel ID="Panel3" runat="server" class="popup-working-section" ScrollBars="Auto">
            <div class="accountInfo" style="width: 100%; float: left">
                <br />
                <fieldset class="login">
                    <uc3:Message ID="Message1" runat="server" />
                    <table class="popup-table">
                        <tr>
                            <td style="font-weight: bold">Name
                            </td>
                            <td>
                                <asp:Label ID="lblName" runat="server"></asp:Label>
                            </td>
                            <td style="font-weight: bold">Claim No
                            </td>
                            <td>
                                <asp:Label ID="lblClaimApplicationNumber" runat="server"></asp:Label>
                            </td>
                            <td style="font-weight: bold">Period From
                            </td>
                            <td>
                                <asp:Label ID="lblFromDate" runat="server"></asp:Label>
                            </td>
                            <td style="font-weight: bold">Period To
                            </td>
                            <td>
                                <asp:Label ID="lblToDate" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold">Claim Heading
                            </td>
                            <td colspan="7">
                                <asp:Label ID="lblClaimHeader" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8">
                                <div style="height: 35vh; overflow: scroll">
                                    <div class="panel-body">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvClaimDetails" runat="server" OnRowCommand="gvClaimDetails_RowCommand"
                                                AutoGenerateColumns="False" Width="100%" OnRowDataBound="gvClaimDetails_RowDataBound"
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
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Date
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Convert.ToDateTime(Eval("ExpenseDate").ToString()).ToString("dd MMM yyyy") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Category" DataField="CategoryName" />
                                                    <asp:BoundField HeaderText="Description" DataField="Description" />
                                                    <asp:BoundField HeaderText="Amount" DataField="Cost" />
                                                    <asp:BoundField HeaderText="Approved Amount" DataField="ApprovedAmount" />
                                                    <asp:BoundField HeaderText="Approver Remarks" DataField="ApproverRemarks" />
                                                    <asp:BoundField HeaderText="Status" DataField="StatusName" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnkBtnAttachment" runat="server" Style="font-size: 16px;" class="fa fa-paperclip fa-fw" CommandName="A" CommandArgument='<%# Eval("Attachment") %>' />
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
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold">Total Claim Amount
                            </td>
                            <td colspan="7" style="font-weight: bold">
                                <asp:Label ID="lblTotalClaimCount" runat="server" class="pull-right"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-weight: bold">Total Approved Amount
                            </td>
                            <td colspan="7" style="font-weight: bold">
                                <asp:Label ID="Label1" runat="server" class="pull-right" Text="0.00"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </div>
        </asp:Panel>
        <img id="img1" runat="server" src="../images/close-button.png" alt="Close" class="popup-close" />
    </asp:Panel>
</asp:Content>
