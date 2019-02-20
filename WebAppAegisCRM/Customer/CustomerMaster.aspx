<%@ Page Title="Customer" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="CustomerMaster.aspx.cs" Inherits="WebAppAegisCRM.Customer.CustomerMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/MyJavaScript.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ValidationForSave() {
            var CustomerName = document.getElementById("<%=txtCustomerName.ClientID%>").value.trim();

            var EmailId = document.getElementById("<%=txtEmailId.ClientID%>").value.trim();
            var MobileNo = document.getElementById("<%=txtMobileNo.ClientID%>").value.trim();

            var PAdress = document.getElementById("<%=txtpAddress.ClientID%>").value.trim();
            var PhoneNumber = document.getElementById("<%=txtPhoneNo.ClientID%>").value.trim();
            var Pin = document.getElementById("<%=txtPin.ClientID%>").value.trim();
            var Pstreet = document.getElementById("<%=txtpStreet.ClientID%>").value.trim();
            var Refference = document.getElementById("<%=txtrefferenceName.ClientID%>").value.trim();

            var CustomerType = document.getElementById('<%=ddlCustomerType.ClientID%>').selectedIndex;

            if (CustomerName == "" || DOB == "" || EmailId == "" || MobileNo == "" || PAdress == "" || PhoneNumber == "" || Pin == "" || Pstreet == "" || ReferenceError == "" || UserId == "" ||
                CompanyId == 0 || CustomerType == 0) {
                alert("Enter All Madnatery Field");
                return false;
            }
            else
                return true;
        }
    </script>
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
                            Customer Basic Details
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Customer Name
                                        <asp:TextBox ID="txtCustomerName" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <!-- /.col-lg-4 (nested) -->
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Customer Type
                                        <asp:DropDownList ID="ddlCustomerType" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">A+</asp:ListItem>
                                            <asp:ListItem Value="2">A</asp:ListItem>
                                            <asp:ListItem Value="3">B</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        Referred by
                                        <asp:TextBox ID="txtrefferenceName" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Permanent Mobile No.
                                        <asp:TextBox ID="txtMobileNo" CssClass="form-control" runat="server" onkeypress="return isNumber(event)"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Permanent Phone No.
                                        <asp:TextBox ID="txtPhoneNo" CssClass="form-control" runat="server" onkeypress="return isNumber(event)"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Permanent Email
                                        <asp:TextBox ID="txtEmailId" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Street
                                        <asp:TextBox ID="txtpStreet" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Pin
                                        <asp:TextBox ID="txtPin" CssClass="form-control" runat="server" onkeypress="return isNumber(event)"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Permanent Address
                                        <asp:TextBox ID="txtpAddress" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-lg-4">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-outline btn-success pull-right extra-margin "
                                        OnClick="btnSubmit_Click" OnClientClick="return ValidationForSave();" />
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-outline btn-warning pull-right extra-margin "
                                        OnClick="btnReset_Click" />
                                </div>
                                <div class="col-lg-12">
                                    <uc3:Message ID="Message" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Customer List
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <div style="height: 40vh; overflow: scroll">
                                    <asp:GridView ID="gvCustomerMaster" DataKeyNames="CustomerMasterId" runat="server"
                                        AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333" OnPageIndexChanging="gvCustomerMaster_PageIndexChanging"
                                        class="table table-striped" GridLines="None" Style="text-align: left" OnRowCommand="gvCustomerMaster_RowCommand">
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
                                            <asp:BoundField HeaderText="Customer Name" DataField="CustomerName" />
                                            <asp:TemplateField HeaderText="Customer Type">
                                                        <ItemTemplate>
                                                            <%# ((int)Eval("CustomerType") == (int)Business.Common.Constants.CustomerType.APlus)? "A+": (((int)Eval("CustomerType") == (int)Business.Common.Constants.CustomerType.A)? "A" : (((int)Eval("CustomerType") == (int)Business.Common.Constants.CustomerType.B)? "B" : "N/A")) %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                            <asp:BoundField HeaderText="Mobile No." DataField="MobileNo" />
                                            <asp:BoundField HeaderText="Email" DataField="EmailId" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnk" runat="server" CommandArgument='<%# Eval("CustomerMasterId") %>'
                                                        CommandName="Contact">Add Contact</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkContact" runat="server" CommandArgument='<%# Eval("CustomerMasterId") %>'
                                                        CommandName="Address">Add Address</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImgEdit" runat="server" CausesValidation="false" CommandName="E"
                                                        CommandArgument='<%# Eval("CustomerMasterId") %>' ImageUrl="~/Images/edit_button.png"
                                                        ImageAlign="AbsMiddle" ToolTip="Edit" Width="20px" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImgDelete" runat="server" CausesValidation="false" CommandName="D"
                                                        CommandArgument='<%# Eval("CustomerMasterId") %>' ImageUrl="~/images/delete_button.png"
                                                        ImageAlign="AbsMiddle" ToolTip="Delete" Width="20px" Height="20px" OnClientClick="return confirm('Are you sure?');" />
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
                <a id="lnkContact" runat="server"></a>
                <asp:ModalPopupExtender ID="ModalPopupExtender1" BackgroundCssClass="myModalPopupbackGrnd"
                    runat="server" TargetControlID="lnkContact" PopupControlID="Panel1" CancelControlID="imgbtn">
                </asp:ModalPopupExtender>
                <asp:Panel ID="Panel1" runat="server" CssClass="myModalPopup-8" Style="display: none; z-index: 10000; position: absolute">
                    <asp:Panel ID="dragHandler" runat="server" class="popup-working-section" ScrollBars="Auto">
                        <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" CssClass="MyTabStyle"
                            ActiveTabIndex="1">
                            <asp:TabPanel ID="AddContact" runat="server">
                                <HeaderTemplate>
                                    Add/Edit Contact
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <div class="accountInfo" style="width: 100%; float: left">
                                        <br />
                                        <fieldset class="login">
                                            <legend>Enter contact person details</legend>
                                            <table class="popup-table">
                                                <tr>
                                                    <td>Name<span class="mandatory">*</span>
                                                    </td>
                                                    <td class="has-error">
                                                        <asp:TextBox ID="txtcontactPerson" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Phone No. <span class="mandatory">*</span>
                                                    </td>
                                                    <td class="has-error">
                                                        <asp:TextBox ID="txtCphoneNumber" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Designation
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtcontactDesignation" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Button ID="btnTSave" runat="server" Text="Save" OnClick="btnTSave_Click" CssClass="btn btn-outline btn-success pull-right" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </div>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel ID="ContactDetails" runat="server">
                                <HeaderTemplate>
                                    Contact List
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <br />
                                    <div class="panel-body">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvContactDetails" DataKeyNames="CustomerContactDetailsId" runat="server"
                                                OnRowCommand="gvContactDetails_RowCommand" AutoGenerateColumns="False" Width="100%"
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
                                                    <asp:BoundField HeaderText="Customer" DataField="CustomerName" />
                                                    <asp:BoundField HeaderText="Contact Person Name" DataField="ContactPerson" />
                                                    <asp:BoundField HeaderText="Phone No." DataField="CPPhoneNo" />
                                                    <asp:BoundField HeaderText="Designation" DataField="CPDesignation" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImgEdit" runat="server" CausesValidation="false" CommandName="E"
                                                                CommandArgument='<%#Eval("CustomerContactDetailsId") %>' ImageUrl="~/Images/edit_button.png"
                                                                ImageAlign="AbsMiddle" ToolTip="EDIT" Width="20px" Height="20px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnDelete" runat="server" CommandName="D" ImageUrl="~/Images/delete_button.png"
                                                                CommandArgument='<%#Eval("CustomerContactDetailsId") %>' Width="20px" Height="20px"
                                                                OnClientClick="return confirm('Are You Sure?');" />
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
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:TabPanel>
                        </asp:TabContainer>
                    </asp:Panel>
                    <img id="imgbtn" runat="server" src="../images/close-button.png" alt="Close" class="popup-close" />
                </asp:Panel>
                <a href="#" id="lnkAddress" runat="server"></a>
                <asp:ModalPopupExtender ID="ModalPopupExtender2" BackgroundCssClass="myModalPopupbackGrnd"
                    runat="server" TargetControlID="lnkAddress" PopupControlID="Panel2" CancelControlID="img1">
                </asp:ModalPopupExtender>
                <asp:Panel ID="Panel2" runat="server" CssClass="myModalPopup-8" Style="display: none; z-index: 10000; position: absolute">
                    <asp:Panel ID="Panel3" runat="server" class="popup-working-section" ScrollBars="Auto">
                        <asp:TabContainer ID="TabContainer2" runat="server" Width="100%" CssClass="MyTabStyle"
                            ActiveTabIndex="1">
                            <asp:TabPanel ID="AddAddress" runat="server">
                                <HeaderTemplate>
                                    Add/Edit Address
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <div class="accountInfo" style="width: 100%; float: left">
                                        <br />
                                        <fieldset class="login">
                                            <legend>Enter Address Details</legend>
                                            <table style="padding: 0 0 0 50px" width="70%">
                                                <tr>
                                                    <td style="padding: 10px; text-align: justify; vertical-align: top">Street <span class="mandatory">*</span>
                                                    </td>
                                                    <td style="padding: 10px; text-align: justify; vertical-align: top" class="has-error">
                                                        <asp:TextBox ID="txtStreet" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 10px; text-align: justify; vertical-align: top">Address <span class="mandatory">*</span>
                                                    </td>
                                                    <td class="has-error">
                                                        <asp:TextBox ID="txttAddress" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Button ID="btnSaveAddresstDetails" runat="server" Text="Save" CssClass="btn btn-outline btn-success pull-right" OnClick="btnSaveAddressDetails_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </fieldset>
                                    </div>
                                </ContentTemplate>
                            </asp:TabPanel>
                            <asp:TabPanel ID="AddressList" runat="server">
                                <HeaderTemplate>
                                    Address List
                                </HeaderTemplate>
                                <ContentTemplate>
                                    <br />
                                    <div class="panel-body">
                                        <div class="table-responsive">
                                            <asp:GridView ID="gvCustomerAddressList" DataKeyNames="CustomerAddressId" runat="server"
                                                OnRowCommand="gvCustomerAddressList_RowCommand" AutoGenerateColumns="False" Width="100%"
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
                                                    <asp:BoundField HeaderText="Customer" DataField="CustomerName" />
                                                    <asp:BoundField HeaderText="Street" DataField="tStreet" />
                                                    <asp:BoundField HeaderText="Address" DataField="tAddress" />
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ImgEdit" runat="server" CausesValidation="false" CommandName="E"
                                                                CommandArgument='<%#Eval("CustomerAddressId") %>' ImageUrl="~/Images/edit_button.png"
                                                                ImageAlign="AbsMiddle" ToolTip="EDIT" Width="20px" Height="20px" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnDelete" runat="server" CommandName="D" ImageUrl="~/Images/delete_button.png"
                                                                CommandArgument='<%#Eval("CustomerAddressId") %>' Width="20px" Height="20px"
                                                                OnClientClick="return confirm('Are You Sure?');" />
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
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:TabPanel>
                        </asp:TabContainer>
                    </asp:Panel>
                    <img id="img1" runat="server" src="../images/close-button.png" style="float: right; margin-right: 1px; cursor: pointer"
                        alt="Close" />
                </asp:Panel>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID = "btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
