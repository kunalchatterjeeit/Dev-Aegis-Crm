<%@ Page Title="EMPLOYEE" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="True"
    CodeBehind="Employee.aspx.cs" Inherits="WebAppAegisCRM.Employee.Employee" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../js/MyJavaScript.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ValidationForSave() {
            var CustomerName = document.getElementById("<%=txtemployeename.ClientID%>").value.trim();
            var DOB = document.getElementById("<%=txtdateofbirth.ClientID%>").value.trim();
            var EmailId = document.getElementById("<%=txtpersonalEmailId.ClientID%>").value.trim();
            var MobileNo = document.getElementById("<%=txtMobileNo.ClientID%>").value.trim();

            var PAdress = document.getElementById("<%=txtpAddress.ClientID%>").value.trim();
           // var PhoneNumber = document.getElementById("<%=txtofficialPhoneNo.ClientID%>").value.trim();
            var Pin = document.getElementById("<%=txtPin.ClientID%>").value.trim();
            var City = document.getElementById("<%=ddlCity.ClientID%>").selectedIndex;
            var Religion = document.getElementById('<%=ddlReligion.ClientID%>').selectedIndex;
            var designation = document.getElementById('<%=ddldesignation.ClientID%>').selectedIndex;
            var MaratialStatus = document.getElementById('<%=ddlMaritalStatus.ClientID%>').selectedIndex;
            var GenderId = document.getElementById('<%=ddlgenderid.ClientID%>').selectedIndex;
            var img = document.getElementById('<%= this.FileUpload1.ClientID %>').value;
            var extimg = img.substr(img.lastIndexOf('.') + 1).toLowerCase();
            var Password = document.getElementById("<%=txtPassword.ClientID%>").value.trim();

            if (CustomerName == "" || DOB == "" || EmailId == "" || MobileNo == "" || PAdress == ""
            || Pin == "" || City == 0 || Religion == "" || 
             CustomerType == 0 || MaratialStatus == 0 || GenderId == 0) {
                alert("Enter All Madnatery Field");
                return false;
            }
            else {
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

        }

        function Popup(Id) {
            debugger;
            newWindow = window.open('ViewEmoployeeDetails.aspx?ID=' + Id + '', '_blank', 'location=yes,height=600,width=1000,scrollbars=yes,status=yes');
            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <br />
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading" style="font-size: large;">
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
                                <asp:TextBox ID="txtPassword" CssClass="form-control" runat="server" ></asp:TextBox>
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
                                        <img src='EmployeeImage/<%# Eval("Image") %>' alt="Not found" width="100px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Code" DataField="EmployeeCode" />
                                <asp:BoundField HeaderText="Name" DataField="EmployeeName" />
                                <asp:BoundField HeaderText="Mobile" DataField="PersonalMobileNo" />
                                <asp:BoundField HeaderText="Email" DataField="PersonalEmailId" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgEdit" runat="server" CausesValidation="false" CommandName="E"
                                            CommandArgument='<%# Eval("EmployeeMasterId") %>' ImageUrl="~/Images/edit_button.png"
                                            ImageAlign="AbsMiddle" ToolTip="EDIT" Width="20px" Height="20px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgDelete" runat="server" CausesValidation="false" CommandName="D"
                                            CommandArgument='<%# Eval("EmployeeMasterId") %>' ImageUrl="~/images/delete_button.png"
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
</asp:Content>
