<%@ Page Title="TAG CUSTOMER MODEL" Language="C#" MasterPageFile="~/Main.Master"
    AutoEventWireup="True" CodeBehind="CustomerPurchase.aspx.cs" Inherits="WebAppAegisCRM.Customer.CustomerPurchase" %>

<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function validation() {
            if (document.getElementById("<%=ddlBrand.ClientID %>").selectedIndex == 0) {
                alert('Select Brand');
                document.getElementById("<%=ddlBrand.ClientID %>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlProduct.ClientID %>").selectedIndex == 0) {
                alert('Select Model');
                document.getElementById("<%=ddlProduct.ClientID %>").focus();
                return false;
            }
            if (document.getElementById("<%=txtProductSlNo.ClientID %>").value == '') {
                alert('Enter Model Serial No.');
                document.getElementById("<%=txtProductSlNo.ClientID %>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlAssignEngineer.ClientID %>").selectedIndex == 0) {
                alert('Select Assign Engineer');
                document.getElementById("<%=ddlAssignEngineer.ClientID %>").focus();
                return false;
            }
        }
    </script>
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
                            Customer List
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="table-responsive">
                                    <div class="col-lg-12">
                                        <asp:GridView ID="gvCustomerMaster" DataKeyNames="CustomerMasterId" runat="server"
                                            AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333" AllowPaging="true" AllowCustomPaging="true" PageSize="20"
                                            Style="text-align: left" GridLines="None" OnRowCommand="gvCustomerMaster_RowCommand"
                                            class="table table-striped" OnPageIndexChanging="gvCustomerMaster_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        SN.
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%#  (gvCustomerMaster.PageIndex * gvCustomerMaster.PageSize) + (Container.DataItemIndex + 1) %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Customer Id" DataField="CustomerCode" />
                                                <asp:BoundField HeaderText="Customer Name" DataField="CustomerName" />
                                                <asp:TemplateField HeaderText="Customer Type">
                                                    <ItemTemplate>
                                                        <%# ((int)Eval("CustomerType") == (int)Business.Common.Constants.CustomerType.APlus)? "A+": (((int)Eval("CustomerType") == (int)Business.Common.Constants.CustomerType.A)? "A" : (((int)Eval("CustomerType") == (int)Business.Common.Constants.CustomerType.B)? "B" : "N/A")) %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Mobile No" DataField="MobileNo" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnCustomerPurchase" runat="server" Text="Tag Details" CommandName="PurchaseDetails" class="btn btn-outline btn-success"
                                                            CommandArgument='<%# Eval("CustomerMasterId") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnContract" runat="server" Text="Contract Details" CommandName="ContractDetails" class="btn btn-outline btn-success"
                                                            CommandArgument='<%# Eval("CustomerMasterId") %>' />
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
            <a id="lnk" runat="server"></a>
            <asp:ModalPopupExtender ID="ModalPopupExtender1" BackgroundCssClass="myModalPopupbackGrnd"
                runat="server" TargetControlID="lnk" PopupControlID="Panel1" CancelControlID="imgbtn">
                <Animations>
                 <OnShown><Fadein Duration="0.50" /></OnShown>
                </Animations>
            </asp:ModalPopupExtender>
            <asp:Panel ID="Panel1" runat="server" CssClass="myModalPopup-10" Style="display: none; z-index: 10000; position: absolute">
                <asp:Panel ID="dragHandler" runat="server" class="popup-working-section" ScrollBars="Auto">
                    <h6 id="popupHeader1" runat="server" class="popup-header-companyname"></h6>
                    <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" CssClass="MyTabStyle"
                        ActiveTabIndex="1">
                        <asp:TabPanel ID="PurchaseDetails" runat="server">
                            <HeaderTemplate>
                                Tag Details
                            </HeaderTemplate>
                            <ContentTemplate>
                                <div class="accountInfo" style="width: 95%; float: left">
                                    <fieldset class="login">
                                        <legend>Enter model details</legend>
                                        <table class="popup-table">
                                            <tr>
                                                <td>Machine Id <span class="mandatory">*</span>
                                                </td>
                                                <td class="has-error">
                                                    <asp:TextBox ID="txtMachineId" runat="server" CssClass="form-control" Enabled="false"
                                                        placeholder="AUTO GENERATED"></asp:TextBox>
                                                </td>
                                                <td>Contact Person
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtContactPerson" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Brand <span class="mandatory">*</span>
                                                </td>
                                                <td class="has-error">
                                                    <asp:DropDownList ID="ddlBrand" runat="server" CssClass="form-control" AutoPostBack="true"
                                                        OnSelectedIndexChanged="ddlBrand_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>Mobile No.
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMobileNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Model <span class="mandatory">*</span>
                                                </td>
                                                <td class="has-error">
                                                    <asp:DropDownList ID="ddlProduct" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>Phone No.
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Model Sl. No. <span class="mandatory">*</span>
                                                </td>
                                                <td class="has-error">
                                                    <asp:TextBox ID="txtProductSlNo" runat="server" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>Address
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Assign Engineer <span class="mandatory">*</span>
                                                </td>
                                                <td class="has-error">
                                                    <asp:DropDownList ID="ddlAssignEngineer" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>Customer Remarks
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCustomerRemarks" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Upload Stamp:
                                                </td>
                                                <td>
                                                    <asp:FileUpload ID="FileUploadStamp" runat="server" />
                                                </td>
                                                <td>Installation Date</td>
                                                <td>
                                                    <asp:TextBox ID="txtInstallationDate" CssClass="form-control" runat="server"></asp:TextBox>
                                                    <asp:CalendarExtender ID="txtInstallationDate_CalendarExtender" runat="server" Enabled="True"
                                                        Format="dd MMM yyyy" TargetControlID="txtInstallationDate">
                                                    </asp:CalendarExtender>
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <asp:Button ID="btnSave" runat="server" Text="Save" ValidationGroup="sub" OnClick="btnSave_Click"
                                            class="btn btn-outline btn-success extra-margin pull-right" OnClientClick="javascript:return validation()"></asp:Button>
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-outline btn-warning extra-margin pull-right"
                                            OnClick="btnCancel_Click"></asp:Button>
                                        <uc3:Message ID="Message" runat="server"></uc3:Message>

                                    </fieldset>
                                </div>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel ID="PurchaseList" runat="server">
                            <HeaderTemplate>
                                Tag List
                            </HeaderTemplate>
                            <ContentTemplate>
                                <asp:GridView ID="gvCustomerPurchaseList" DataKeyNames="CustomerPurchaseId" runat="server"
                                    AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                                    class="table table-striped" GridLines="None" Style="text-align: left" OnRowCommand="gvCustomerPurchaseList_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                SN.
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Machine Id" DataField="MachineId" />
                                        <asp:BoundField HeaderText="Model" DataField="ProductName" />
                                        <asp:BoundField HeaderText="Serial No" DataField="ProductSerialNo" />
                                        <asp:BoundField HeaderText="Contact Person" DataField="ContactPerson" />
                                        <asp:BoundField HeaderText="Mobile No." DataField="MobileNo" />
                                        <asp:TemplateField HeaderText="Stamp">
                                            <ItemTemplate>
                                                <%--<img id="imgCustomerStamp" alt="Cannot load customer stamp" src='~/Customer/StampImage/' style="width: 150px; margin: 5px;" />--%>
                                                <%# (Eval("Stamp") != null && Eval("Stamp").ToString().Length>0)?"Uploaded":"Not Uploaded" %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgEdit" runat="server" CausesValidation="false" CommandName="Ed"
                                                    ImageUrl="~/Images/edit_button.png" ImageAlign="AbsMiddle" ToolTip="EDIT" Width="20px"
                                                    Height="20px" CommandArgument='<%# Eval("CustomerPurchaseId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelete" runat="server" CommandName="Del" ImageUrl="~/Images/delete_button.png"
                                                    Width="20px" Height="20px" OnClientClick="return confirm('Are You Sure?');" CommandArgument='<%# Eval("CustomerPurchaseId") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle Width="25px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField></asp:TemplateField>
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
                            </ContentTemplate>
                        </asp:TabPanel>
                    </asp:TabContainer>
                </asp:Panel>
                <img id="imgbtn" runat="server" src="../images/close-button.png" style="float: right; margin-right: 1px; cursor: pointer"
                    alt="Close" />
            </asp:Panel>

            <a id="lnk2" runat="server"></a>
            <asp:ModalPopupExtender ID="ModalPopupExtender2" BackgroundCssClass="myModalPopupbackGrnd"
                runat="server" TargetControlID="lnk2" PopupControlID="Panel2" CancelControlID="imgbtn2">
                <Animations>
                 <OnShown><Fadein Duration="0.50" /></OnShown>
                </Animations>
            </asp:ModalPopupExtender>
            <asp:Panel ID="Panel2" runat="server" CssClass="myModalPopup-18" Style="display: none;">
                <asp:Panel ID="Panel4" runat="server" class="popup-working-section-extra-height" ScrollBars="Auto">
                    <h6 id="popupHeader2" runat="server" class="popup-header-companyname"></h6>
                    <asp:TabContainer ID="TabContainer2" runat="server" Width="100%" CssClass="MyTabStyle"
                        ActiveTabIndex="1">
                        <asp:TabPanel ID="TabPanel1" runat="server">
                            <HeaderTemplate>
                                Contract Details
                            </HeaderTemplate>
                            <ContentTemplate>
                                <br />
                                <div class="accountInfo" style="width: 98%; float: left">
                                    <fieldset class="login">
                                        <legend>Enter contract details</legend>
                                        <table class="popup-table">
                                            <tr>
                                                <td>Contract Type
                                                    <asp:DropDownList ID="ddlContractType" CssClass="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>Contract Start Date
                                                <asp:TextBox ID="txtContractStartDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtContractStartDate"
                                                        Format="dd MMM yyyy" Enabled="True">
                                                    </asp:CalendarExtender>
                                                </td>
                                                <td>Contract End Date
                                                <asp:TextBox ID="txtContractEndDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtContractEndDate"
                                                        Format="dd MMM yyyy" Enabled="True">
                                                    </asp:CalendarExtender>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <div style="width: 1055px; overflow: scroll">
                                                        <asp:GridView ID="gvCustomerPurchaseListForContract" DataKeyNames="CustomerPurchaseId" runat="server"
                                                            AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333"
                                                            class="table table-striped" GridLines="None" Style="text-align: left">
                                                            <Columns>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkSelect" runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField HeaderText="M Id" DataField="MachineId" />
                                                                <asp:BoundField HeaderText="Model" DataField="ProductName" />
                                                                <asp:BoundField HeaderText="Contact Person" DataField="ContactPerson" />
                                                                <asp:TemplateField HeaderText="A3 B/W SM">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtA3BWStartMeter" CssClass="form-control" runat="server"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="A4 B/W SM">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtA4BWStartMeter" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtA4BWStartMeter" ValidChars="0123456789"></asp:FilteredTextBoxExtender>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="A3 CL SM">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtA3CLStartMeter" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtA3CLStartMeter" ValidChars="0123456789"></asp:FilteredTextBoxExtender>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="A4 CL SM">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtA4CLStartMeter" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtA4CLStartMeter" ValidChars="0123456789"></asp:FilteredTextBoxExtender>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="A3 B/W Page">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtA3BWPage" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" TargetControlID="txtA3BWPage" ValidChars="0123456789"></asp:FilteredTextBoxExtender>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="A4 B/W Page">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtA4BWPage" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" TargetControlID="txtA4BWPage" ValidChars="0123456789"></asp:FilteredTextBoxExtender>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="A3 CL Page">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtA3CLPage" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" TargetControlID="txtA3CLPage" ValidChars="0123456789"></asp:FilteredTextBoxExtender>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="A4 CL Page">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtA4CLPage" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" TargetControlID="txtA4CLPage" ValidChars="0123456789"></asp:FilteredTextBoxExtender>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="A3 B/W Rate">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtA3BWRate" CssClass="form-control " runat="server"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server" TargetControlID="txtA3BWRate" ValidChars="0123456789."></asp:FilteredTextBoxExtender>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="A4 B/W Rate">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtA4BWRate" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" TargetControlID="txtA4BWRate" ValidChars="0123456789."></asp:FilteredTextBoxExtender>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="A3 CL Rate">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtA3CLRate" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" TargetControlID="txtA3CLRate" ValidChars="0123456789."></asp:FilteredTextBoxExtender>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="A4 CL Rate">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtA4CLRate" CssClass="form-control" runat="server"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender11" runat="server" TargetControlID="txtA4CLRate" ValidChars="0123456789."></asp:FilteredTextBoxExtender>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <FooterStyle BackColor="#5bb0de" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#5bc0de" Font-Bold="True" ForeColor="White" Wrap="false" />
                                                            <RowStyle CssClass="RowStyle" BackColor="#F7F6F3" ForeColor="#333333" Wrap="false" />
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
                                                    <br />
                                                    <asp:Button ID="btnContractSave" runat="server" Text="Save" ValidationGroup="sub" OnClick="btnContractSave_Click"
                                                        class="btn btn-outline btn-success"></asp:Button>
                                                    <asp:Button ID="btnContractCancel" runat="server" Text="Cancel" class="btn btn-outline btn-warning"
                                                        OnClick="btnContractCancel_Click"></asp:Button>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3">
                                                    <uc3:Message ID="Message1" runat="server"></uc3:Message>
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel ID="TabPanel2" runat="server">
                            <HeaderTemplate>
                                Contract List
                            </HeaderTemplate>
                            <ContentTemplate>
                                <asp:GridView ID="gvContractList" DataKeyNames="ContractId" runat="server"
                                    AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                                    class="table table-striped" GridLines="None" Style="text-align: left" OnRowCommand="gvContractList_RowCommand"
                                    OnRowDataBound="gvContractList_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                SN.
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Contract Start Date" DataField="ContractStartDate" />
                                        <asp:BoundField HeaderText="Contract End Date" DataField="ContractEndDate" />
                                        <asp:BoundField HeaderText="Contract Name" DataField="ContractName" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgView" runat="server" CausesValidation="false" CommandName="View"
                                                    ImageUrl="~/Images/view_button.png" ImageAlign="AbsMiddle" ToolTip="View" Width="20px"
                                                    Height="20px" CommandArgument='<%# Eval("ContractId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnDelete" runat="server" CommandName="Del" ImageUrl="~/Images/delete_button.png"
                                                    Width="20px" Height="20px" OnClientClick="return confirm('Are You Sure?');" CommandArgument='<%# Eval("ContractId") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle Width="25px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField></asp:TemplateField>
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
                            </ContentTemplate>
                        </asp:TabPanel>
                    </asp:TabContainer>
                </asp:Panel>
                <img id="imgbtn2" runat="server" src="../images/close-button.png" style="float: right; margin-right: 1px; cursor: pointer"
                    alt="Close" />
            </asp:Panel>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="TabContainer1$PurchaseDetails$btnSave" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
