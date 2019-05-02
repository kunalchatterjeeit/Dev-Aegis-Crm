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
    <style type="text/css">
        .bg-image {
            /* The image used */
            position: fixed;
            left: 0;
            right: 0;
            z-index: 1;
            display: block;
            background-image: url("/images/login_background.jpg");
            height: 100%;
            -webkit-filter: blur(8px);
            -moz-filter: blur(8px);
            -o-filter: blur(8px);
            -ms-filter: blur(8px);
            filter: blur(8px);
        }

        .content {
            position: fixed;
            left: 0;
            right: 0;
            z-index: 9999;
        }

        .footer {
            position: fixed;
            left: 0;
            bottom: 0;
            width: 100%;
            background-color: white;
            color: white;
            text-align: center;
        }

            .footer img {
                height: 60px;
            }

        .column {
            float: left;
            width: 25%;
            padding: 5px;
        }

        /* Clearfix (clear floats) */
        .row::after {
            content: "";
            clear: both;
            display: table;
        }
    </style>
    <style type="text/css">
        .login-container {
            margin-top: 5%;
            margin-bottom: 5%;
        }

        .login-logo {
            position: relative;
            margin-left: -41.5%;
        }

            .login-logo img {
                position: absolute;
                width: 20%;
                margin-top: 19%;
                background: #282726;
                border-radius: 4.5rem;
                padding: 5%;
            }

        .login-form-1 {
            filter: blur(0px);
            -webkit-filter: blur(0px);
            padding: 9%;
            background: #282726;
            box-shadow: 0 5px 8px 0 rgba(0, 0, 0, 0.2), 0 9px 26px 0 rgba(0, 0, 0, 0.19);
        }

            .login-form-1 h3 {
                text-align: center;
                margin-bottom: 12%;
                color: #fff;
            }

        .login-form-2 {
            padding: 9%;
            background: #f05837;
            box-shadow: 0 5px 8px 0 rgba(0, 0, 0, 0.2), 0 9px 26px 0 rgba(0, 0, 0, 0.19);
        }

            .login-form-2 h3 {
                text-align: center;
                margin-bottom: 12%;
                color: #fff;
            }

        .btnSubmit {
            font-weight: 600;
            width: 50%;
            color: #282726;
            background-color: #fff;
            border: none;
            border-radius: 1.5rem;
            padding: 2%;
        }

        .btnForgetPwd {
            color: #fff;
            font-weight: 600;
            text-decoration: none;
        }

            .btnForgetPwd:hover {
                text-decoration: none;
                color: #fff;
            }
    </style>
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
                            <div class="col-md-6 login-form-1">
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
                            </div>
                            <div class="col-md-6 login-form-2">
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
