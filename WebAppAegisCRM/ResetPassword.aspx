﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="WebAppAegisCRM.ResetPassword" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="/images/favicon.ico" type="image/x-icon" rel="shortcut icon" />
    <link href="/images/favicon.ico" type="image/x-icon" rel="icon" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="description" content="" />
    <meta name="author" content="" />
    <title>Aegis CRM</title>
    <link href="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/css/bootstrap.min.css" rel="stylesheet">
    <script src="//maxcdn.bootstrapcdn.com/bootstrap/4.1.1/js/bootstrap.min.js"></script>
    <script src="//cdnjs.cloudflare.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
    <link href="dist/css/custom-login.css" rel="Stylesheet" />
    <link href="dist/css/custom03052019.css" rel="stylesheet" />
</head>
<body id="radial-position" class="container-login100" style="background-image: url('/images/login_background.jpg');">
    <form id="form2" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="1">
            <ProgressTemplate>
                <div class="divWaiting">
                    <div class="loading">
                        <%--<div class="loading-bar"></div>
                        <div class="loading-bar"></div>
                        <div class="loading-bar"></div>
                        <div class="loading-bar"></div>--%>
                    </div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="bg-image"></div>
                <div class="content">
                    <div class="container login-container">
                        <div class="row">
                            <div class="col-md-6 login-form-2">
                                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnChange">
                                    <h3>Employee Reset Password</h3>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtNewPassword" runat="server" class="form-control" autofocus TextMode="Password" placeholder="New password*"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtReNewPassword" TextMode="Password" runat="server" class="form-control" placeholder="Confirm password*"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Password & Re-enter password should be same" ControlToValidate="txtNewPassword" ValueToCompare="txtNewPassword" ControlToCompare="txtReNewPassword" ForeColor="White"></asp:CompareValidator>
                                        <asp:RegularExpressionValidator ID="Regex5" runat="server" ControlToValidate="txtNewPassword"
                                            ValidationExpression="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]{8,10}"
                                            ErrorMessage="Password must contain: Minimum 8 and Maximum 10 characters atleast 1 UpperCase Alphabet, 1 LowerCase Alphabet, 1 Number and 1 Special Character"
                                            ForeColor="White" />
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="btnChange" runat="server" Text="Change" class="btnSubmit"
                                            OnClick="btnChange_Click" />
                                    </div>
                                </asp:Panel>
                                <span id="lblMessage" runat="server" style="color: white"></span>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <div class="footer content">
        <div class="row">
            <div class="column">
                <img src="images/aegis_crm.png" alt="CRM">
            </div>
            <div class="column">
                <img src="images/aegis_attendance.png" alt="ATTENDANCE">
            </div>
            <div class="column">
                <img src="images/aegis_hrms.png" alt="HRMS">
            </div>
            <div class="column">
                <img src="images/aegis_sales.png" alt="SALES">
            </div>
        </div>
    </div>
    </form>
</body>
</html>
