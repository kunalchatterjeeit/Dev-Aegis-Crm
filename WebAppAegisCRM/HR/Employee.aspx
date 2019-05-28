<%@ Page Title="EMPLOYEE" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="True"
    CodeBehind="Employee.aspx.cs" Inherits="WebAppAegisCRM.Employee.Employee" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Import Namespace="Business.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/MyJavaScript.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ValidationForSave() {
            if (img.length > 1) {
                if (!(extimg == "jpeg" || extimg == "jpg")) {
                    alert("Invalid Image file, must select a *.jpeg or *.jpg,  file.");
                    return false;
                }
                else {
                    if (document.getElementById("<%=FileUpload1.ClientID%>").files[0].size > 204800) {
                        alert("Invalid Image size, the size should be less than or equal 200kb");
                        return false;
                    }
                    else
                        return true;
                }
            }
        }
        function Popup(Id) {
            debugger;
            newWindow = window.open('ViewEmoployeeDetails.aspx?ID=' + Id + '', '_blank', 'location=yes,height=600,width=1000,scrollbars=yes,status=yes');
            return true;
        }

        $(window).load(function () {
            $('img').each(function () {
                if (!this.complete || typeof this.naturalWidth == "undefined" || this.naturalWidth == 0) {
                    // image was broken, replace with your new image
                    if (this.getAttribute("sex") == "Male")
                        this.src = '/Images/male-avatar.png';
                    else if (this.getAttribute("sex") == "Female")
                        this.src = '/Images/female-avatar.jpg';
                    else
                        this.src = '/Images/male-avatar.png';
                }
            });
        });
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
                    <uc3:Message ID="MessageBox" runat="server" />
                </div>
            </div>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Add/Edit Employee
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        <asp:Image ID="Image1" runat="server" Width="200px" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Employee Name
                                <asp:TextBox ID="txtemployeename" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <!-- /.col-lg-4 (nested) -->
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Gender
                                <asp:DropDownList ID="ddlgenderid" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                                    <asp:ListItem Value="1">Male</asp:ListItem>
                                    <asp:ListItem Value="2">Female</asp:ListItem>
                                    <asp:ListItem Value="3">Others</asp:ListItem>
                                </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Date of Birth
                                <asp:TextBox ID="txtdateofbirth" CssClass="form-control" runat="server"></asp:TextBox>
                                        <asp:CalendarExtender ID="txtdateofbirth_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd MMM yyyy" TargetControlID="txtdateofbirth">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Religion:
                                <asp:DropDownList ID="ddlReligion" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                                    <asp:ListItem Value="1">Hindu</asp:ListItem>
                                    <asp:ListItem Value="2">Muslim</asp:ListItem>
                                    <asp:ListItem Value="3">Other</asp:ListItem>
                                </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Marital Status
                                <asp:DropDownList ID="ddlMaritalStatus" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                                    <asp:ListItem Value="1">Marraid</asp:ListItem>
                                    <asp:ListItem Value="2">UnMarraid</asp:ListItem>
                                </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        Date of Marraige
                                <asp:TextBox ID="txtdom" CssClass="form-control" runat="server"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd MMM yyyy"
                                            TargetControlID="txtdom">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        Blood Group:
                                <asp:TextBox ID="txtbloodgroup" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Personal Mobile No
                                <asp:TextBox ID="txtMobileNo" CssClass="form-control" runat="server" onkeypress="return isNumber(event)"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        Official Mobile Number No
                                <asp:TextBox ID="txtofficialPhoneNo" CssClass="form-control" runat="server" onkeypress="return isNumber(event)"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Personal Email
                                <asp:TextBox ID="txtpersonalEmailId" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        Official Email
                                <asp:TextBox ID="txtofficialEmailId" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        Referred By
                                <asp:DropDownList ID="ddlRefferencrEmployee" runat="server" CssClass="form-control"
                                    DataTextField="EmployeeName" DataValueField="EmployeeMasterId">
                                </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Permanent Address
                                <asp:TextBox ID="txtpAddress" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Permanent City:
                                <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" DataTextField="CityName"
                                    DataValueField="CityId">
                                </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Permanent PIN
                                <asp:TextBox ID="txtPin" CssClass="form-control" runat="server" onkeypress="return isNumber(event)"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        Present Address:
                                <asp:TextBox ID="txtpresentaddress" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        Present City:
                                <asp:DropDownList ID="ddlPresentCity" CssClass="form-control" runat="server" DataTextField="CityName"
                                    DataValueField="CityId">
                                </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group ">
                                        Present Pin
                                <asp:TextBox ID="txtpresentpin" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Designation:
                                <asp:DropDownList ID="ddldesignation" runat="server" CssClass="form-control" DataTextField="DesignationName"
                                    DataValueField="DesignationMasterId">
                                </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Date Of Joining:
                                <asp:TextBox ID="txtDOJ" CssClass="form-control" runat="server"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd MMM yyyy"
                                            Enabled="True" TargetControlID="txtDOJ">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        PAN No.
                                <asp:TextBox ID="txtPANnumber" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        Password:
                                <asp:TextBox ID="txtPassword" CssClass="form-control" runat="server"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="Regex5" runat="server" ControlToValidate="txtPassword"
                                            ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,10}"
                                            ErrorMessage="Password must contain: Minimum 8 and Maximum 10 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character"
                                            ForeColor="Red" />
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Image:
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Role:
                                <asp:DropDownList ID="ddlRole" runat="server" CssClass="form-control" DataTextField="RoleName"
                                    DataValueField="RoleId">
                                </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Reporting:
                                <asp:DropDownList ID="ddlReporting" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-outline btn-success"
                                        OnClick="btnSubmit_Click" OnClientClick="return ValidationForSave();" />
                                    <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-outline btn-warning"
                                        OnClick="btnReset_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
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
                                    HeaderStyle-HorizontalAlign="Center" OnSelectedIndexChanged="gvEmployeerMaster_SelectedIndexChanged">
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
                                                <img src='<%# Eval("Image") %>' alt="Not found" width="100px" sex='<%# Eval("GenderId") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Code" DataField="EmployeeCode" />
                                        <asp:BoundField HeaderText="Name" DataField="EmployeeName" />
                                        <asp:BoundField HeaderText="Mobile" DataField="PersonalMobileNo" />
                                        <asp:BoundField HeaderText="Email" DataField="PersonalEmailId" />
                                        <asp:TemplateField ItemStyle-Width="15px">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnRemoveMobile" runat="server" class="fa fa-android fa-fw" CommandName="RemoveMobile" CausesValidation="false"
                                                    CommandArgument='<%# Eval("LinkedDeviceId") %>' Visible='<%# (Eval("LinkedDeviceId")==DBNull.Value)? false : true %>' ToolTip="Remove Linked Mobile"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="15px">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnSettings" runat="server" class="fa fa-leaf fa-fw" CommandName="Leave" CausesValidation="false"
                                                    CommandArgument='<%# Eval("EmployeeMasterId") %>' ToolTip="Leave Settings"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="15px">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnEdit" runat="server" class="fa fa-pencil-square-o fa-fw" CommandName="E" CausesValidation="false"
                                                    CommandArgument='<%# Eval("EmployeeMasterId") %>' ToolTip="Edit"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="15px">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btnDelete" runat="server" class="fa fa-trash-o fa-fw" CausesValidation="false" ToolTip="Delete"
                                                    CommandName="D" OnClientClick="return confirm('Are You Sure?');" CommandArgument='<%# Eval("EmployeeMasterId") %>'></asp:LinkButton>
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
                <Animations>
                 <OnShown><Fadein Duration="0.50" /></OnShown>
                </Animations>
            </asp:ModalPopupExtender>
            <asp:Panel ID="Panel1" runat="server" CssClass="myModalPopup-8" Style="display: none; z-index: 10000; position: absolute">
                <asp:Panel ID="dragHandler" runat="server" class="popup-working-section" ScrollBars="Auto">
                    <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" CssClass="MyTabStyle"
                        ActiveTabIndex="1">
                        <asp:TabPanel ID="LeaveGeneral" runat="server">
                            <HeaderTemplate>
                                Leave General Settings
                            </HeaderTemplate>
                            <ContentTemplate>
                                <div class="accountInfo" style="width: 100%; float: left">
                                    <br />
                                    <fieldset class="login">
                                        <legend>Settings</legend>
                                        <table class="popup-table">
                                            <tr>
                                                <td>Leave is 
                                                </td>
                                                <td>
                                                    <asp:RadioButtonList ID="rbtnListLeaveStatus" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtnListLeaveStatus_SelectedIndexChanged" AutoPostBack="true">
                                                        <asp:ListItem Value="true">Activated</asp:ListItem>
                                                        <asp:ListItem Value="false">Blocked</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <uc3:Message ID="MessageGeneralLeave" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel ID="AddApproval" runat="server">
                            <HeaderTemplate>
                                Leave Approval Settings
                            </HeaderTemplate>
                            <ContentTemplate>
                                <div class="accountInfo" style="width: 100%; float: left">
                                    <br />
                                    <fieldset class="login">
                                        <legend>Enter approval details</legend>
                                        <table class="popup-table">
                                            <tr>
                                                <td>Approver<span class="mandatory">*</span>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlApproverEngineer" runat="server" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Approval Level <span class="mandatory">*</span>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlApprovalLevel" CssClass="form-control" runat="server">
                                                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                        <asp:ListItem Value="1">1</asp:ListItem>
                                                        <asp:ListItem Value="2">2</asp:ListItem>
                                                        <asp:ListItem Value="3">3</asp:ListItem>
                                                        <asp:ListItem Value="4">4</asp:ListItem>
                                                        <asp:ListItem Value="5">5</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Button ID="btnTSave" runat="server" Text="Save" OnClick="btnTSave_Click" CssClass="btn btn-outline btn-success pull-right" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <uc3:Message ID="MessageLeave" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </fieldset>
                                </div>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel ID="ApprovalDetails" runat="server">
                            <HeaderTemplate>
                                Approver List
                            </HeaderTemplate>
                            <ContentTemplate>
                                <br />
                                <div class="panel-body">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvApproverDetails" DataKeyNames="LeaveEmployeeWiseApprovalConfigId" runat="server"
                                            OnRowCommand="gvApproverDetails_RowCommand" AutoGenerateColumns="False" Width="100%"
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
                                                <asp:BoundField HeaderText="Approver Name" DataField="ApproverName" />
                                                <asp:BoundField HeaderText="Level" DataField="ApprovalLevel" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImgEdit" runat="server" CausesValidation="false" CommandName="E"
                                                            CommandArgument='<%#Eval("LeaveEmployeeWiseApprovalConfigId") %>' ImageUrl="~/Images/edit_button.png"
                                                            ImageAlign="AbsMiddle" ToolTip="EDIT" Width="20px" Height="20px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="btnDelete" runat="server" CommandName="D" ImageUrl="~/Images/delete_button.png"
                                                            CommandArgument='<%#Eval("LeaveEmployeeWiseApprovalConfigId") %>' Width="20px" Height="20px"
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
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
