<%@ Page Title="VENDOR" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="VendorMaster.aspx.cs" Inherits="WebAppAegisCRM.Purchase.VendorMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../js/MyJavaScript.js" type="text/javascript"></script>
    <script type="text/javascript">
        function ValidationForSave() {
            if (document.getElementById("<%=txtVendorName.ClientID %>").value == '') {
                alert('Enter Branch Name');
                document.getElementById("<%=txtVendorName.ClientID %>").focus();
                return false;
            }

            if (document.getElementById("<%=ddlState.ClientID %>").value == '') {
                alert('Enter State');
                document.getElementById("<%=ddlState.ClientID %>").focus();
                return false;
            }

            if (document.getElementById("<%=ddlDistrict.ClientID %>").value == '') {
                alert('Enter District');
                document.getElementById("<%=ddlDistrict.ClientID %>").focus();
                return false;
            }
            if (document.getElementById("<%=ddlCity.ClientID %>").value == '') {
                alert('Enter City');
                document.getElementById("<%=ddlCity.ClientID %>").focus();
                return false;
            }
            if (document.getElementById("<%=txtMobileNo.ClientID %>").value == '') {
                alert('Enter Mobile No');
                document.getElementById("<%=txtMobileNo.ClientID %>").focus();
                return false;
            }
            if (document.getElementById("<%=txtMail.ClientID %>").value == '') {
                alert('Enter Mail Id');
                document.getElementById("<%=txtMail.ClientID %>").focus();
                return false;
            }
            else {
                if (confirm('Are you sure?'))
                    return true;
                else
                    return false;

            }
        }
    </script>
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <br />
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Vendor Master
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-4">
                            <div class="form-group has-error">
                                Vendor Name
                                        <asp:TextBox ID="txtVendorName" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <!-- /.col-lg-4 (nested) -->
                        <div class="col-lg-4">
                            <div class="form-group has-error">
                                Vendor Address
                                        <asp:TextBox ID="txtAddress" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group has-error">
                                State
                                        <asp:DropDownList ID="ddlState" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1" Selected="True">West Bengal</asp:ListItem>
                                        </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group has-error">
                                District
                                        <asp:DropDownList ID="ddlDistrict" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1" Selected="True">Kolkata</asp:ListItem>
                                        </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group has-error">
                                City
                                        <asp:DropDownList ID="ddlCity" runat="server" CssClass="form-control" DataTextField="CityName"
                                            DataValueField="CityId">
                                        </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group has-error">
                                Pin
                                        <asp:TextBox ID="txtPin" onkeypress="return isNumber(event)" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                Concerned Person
                                        <asp:TextBox ID="txtConcernedPerson" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        
                        <div class="col-lg-4">
                            <div class="form-group">
                                Tan
                                        <asp:TextBox ID="txtTAN" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                State Code
                                        <asp:TextBox ID="txtStateCode" CssClass="form-control" runat="server"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                Pan
                                        <asp:TextBox ID="txtPAN" CssClass="form-control" runat="server"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                CST
                                        <asp:TextBox ID="txtCST" CssClass="form-control" runat="server"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                GST No
                                        <asp:TextBox ID="txtGSTNo" CssClass="form-control" runat="server"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                Phone No
                                        <asp:TextBox ID="txtPhoneNo" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtPhoneNo" ValidChars="1234567890"></asp:FilteredTextBoxExtender>

                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group has-error">
                                Mobile No
                                        <asp:TextBox ID="txtMobileNo" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" TargetControlID="txtMobileNo" ValidChars="1234567890"></asp:FilteredTextBoxExtender>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                Fax
                                        <asp:TextBox ID="txtFax" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtFax" ValidChars="1234567890"></asp:FilteredTextBoxExtender>

                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group has-error">
                                Email
                                        <asp:TextBox ID="txtMail" CssClass="form-control" runat="server"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                Bank Name
                                        <asp:TextBox ID="txtBankName" CssClass="form-control" runat="server"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                Bank Branch
                                        <asp:TextBox ID="txtBankBranch" CssClass="form-control" runat="server"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                A/c No
                                        <asp:TextBox ID="txtACNo" CssClass="form-control" runat="server"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                IFSC
                                        <asp:TextBox ID="txtIFSC" CssClass="form-control" runat="server"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group has-error">
                                Status
                                        <asp:DropDownList ID="ddlStatus" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="True">Active</asp:ListItem>
                                            <asp:ListItem Value="False">In Active</asp:ListItem>
                                        </asp:DropDownList>

                            </div>
                        </div>                        
                        <div class="col-lg-4">
                            <div class="form-group">
                                Active Date
                                        <asp:TextBox ID="txtActiveDate" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="txtActiveDate_CalendarExtender" runat="server" TargetControlID="txtActiveDate"
                                    Format="dd MMM yyyy">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <uc3:Message ID="Message" runat="server" />
                        </div>
                        <div class="col-lg-4">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-outline btn-success"
                                OnClick="btnSubmit_Click" OnClientClick="return ValidationForSave();" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-outline btn-warning"
                                OnClick="btnReset_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
