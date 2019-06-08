<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebAppAegisCRM.Login" %>

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
<body>
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
                            <div class="col-md-6 login-form-1">
                                <asp:Panel ID="Panel2" runat="server" DefaultButton="btnCustomerLogin">
                                    <h3>Customer Login</h3>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtCustomerEmail" runat="server" class="form-control" placeholder="Your Email *" autofocus></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtMobileNumber" TextMode="Password" runat="server" class="form-control" placeholder="Your Mobile Number *"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="btnCustomerLogin" runat="server" Text="Login" OnClick="btnCustomerLogin_Click" CssClass="btnSubmit" />
                                    </div>
                                    <span id="lblCustomerMessage" runat="server" style="color: white"></span>
                                </asp:Panel>
                            </div>
                            <div class="col-md-6 login-form-2">
                                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnUserLogin">
                                    <div class="login-logo">
                                        <img src="https://image.ibb.co/n7oTvU/logo_white.png" alt="" />
                                    </div>
                                    <h3>Employee Login</h3>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtUserName" runat="server" class="form-control" placeholder="Your Username *"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" class="form-control" placeholder="Your Password *"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="btnUserLogin" runat="server" Text="Login" OnClick="btnUserLogin_Click" CssClass="btnSubmit" />
                                    </div>
                                    <a href="ForgotPassword.aspx" style="color:white">Forgot password</a>
                                </asp:Panel>
                                <span id="lblUserMessage" runat="server" style="color: white"></span>
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
</body>
</html>
