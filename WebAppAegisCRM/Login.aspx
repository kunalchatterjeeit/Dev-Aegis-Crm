<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="WebAppAegisCRM.Login" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="/images/favicon.ico" type="image/x-icon" rel="shortcut icon" />
    <link href="/images/favicon.ico" type="image/x-icon" rel="icon" />
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
    <meta name="viewport" content="width=device-width, initial-scale=1"/>
    <meta name="description" content=""/>
    <meta name="author" content=""/>
    <title>Aegis CRM</title>
    <!-- Bootstrap Core CSS -->
    <link href="../bower_components/bootstrap/dist/css/bootstrap.min.css" rel="stylesheet"/>
    <!-- MetisMenu CSS -->
    <link href="../bower_components/metisMenu/dist/metisMenu.min.css" rel="stylesheet"/>
    <!-- Custom CSS -->
    <link href="../dist/css/sb-admin-2.css" rel="stylesheet"/>
    <!-- Custom Fonts -->
    <link href="../bower_components/font-awesome/css/font-awesome.min.css" rel="stylesheet"
        type="text/css"/>
    <link href="/dist/css/custom15112018.css" rel="stylesheet" />
    <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
    <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
    <!--[if lt IE 9]>
        <script src="https://oss.maxcdn.com/libs/html5shiv/3.7.0/html5shiv.js"></script>
        <script src="https://oss.maxcdn.com/libs/respond.js/1.4.2/respond.min.js"></script>
    <![endif]-->
    <style type="text/css">
        #radial-position {
            background: #CC95C0; /* fallback for old browsers */
            background: -webkit-linear-gradient(to right, #7AA1D2, #DBD4B4, #CC95C0); /* Chrome 10-25, Safari 5.1-6 */
            background: linear-gradient(to right, #7AA1D2, #DBD4B4, #CC95C0); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
        }
    </style>
</head>
<body id="radial-position" class="container-login100" style="background-image: url('/images/login_background.jpg');">
    <form id="form2" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
        <div class="container">
            <div class="row">
                <div class="col-md-4 col-md-offset-4">
                    <div class="login-panel panel panel-default">
                        <div class="panel-heading">
                            <h3 class="panel-title">Please Sign In</h3>
                        </div>
                        <br />
                        <center>
                            <img src="../images/HRMS1.png" height="90px" alt="AEGIS HR" />
                            <img src="../images/Aegis_CRM_Logo2.png" height="90px" alt="WELCOME TO AEGIS CRM" />
                        </center>
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="panel-body">
                                    <fieldset>
                                        <div class="form-group">
                                            Login Type :
                                        <asp:DropDownList ID="ddlLoginType" CssClass="form-control" runat="server" AutoPostBack="true"
                                            OnSelectedIndexChanged="ddlLoginType_SelectedIndexChanged">
                                            <asp:ListItem Text="Employee &nbsp;" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Customer" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblUserName" runat="server"></asp:Label>
                                            <asp:TextBox ID="txtUsername" runat="server" class="form-control" autofocus></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <asp:Label ID="lblPassword" runat="server"></asp:Label>
                                            <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" class="form-control"></asp:TextBox>
                                        </div>
                                        <div class="checkbox">
                                            <span id="lblMessage" runat="server" style="color: #FC2D45;"></span>
                                        </div>
                                        <!-- Change this to a button or input when using this as a form -->
                                        <asp:Button ID="btnLogin" runat="server" Text="Login" class="btn btn-lg btn-success btn-block"
                                            OnClick="btnLogin_Click" />
                                    </fieldset>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <!-- jQuery -->
        <script src="../bower_components/jquery/dist/jquery.min.js"></script>
        <!-- Bootstrap Core JavaScript -->
        <script src="../bower_components/bootstrap/dist/js/bootstrap.min.js"></script>
        <!-- Metis Menu Plugin JavaScript -->
        <script src="../bower_components/metisMenu/dist/metisMenu.min.js"></script>
        <!-- Custom Theme JavaScript -->
        <script src="../dist/js/sb-admin-2.js"></script>
    </form>
</body>
</html>
