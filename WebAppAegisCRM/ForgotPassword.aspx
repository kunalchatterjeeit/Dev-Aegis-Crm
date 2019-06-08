<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="WebAppAegisCRM.ForgotPassword" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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

</head>
<body>
    <form id="form2" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="bg-image"></div>
                <div class="content">
                    <div class="container login-container">
                        <div class="row">
                            <div class="col-md-6 login-form-2">
                                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnValidate">
                                    <h3>Employee Forgot Password</h3>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtUserName" runat="server" class="form-control" placeholder="Your Username *"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtEmailId" runat="server" class="form-control" placeholder="Your Email Id *" TextMode="Email"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:Button ID="btnValidate" runat="server" Text="Validate" OnClick="btnValidate_Click" CssClass="btnSubmit" />
                                    </div>
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
