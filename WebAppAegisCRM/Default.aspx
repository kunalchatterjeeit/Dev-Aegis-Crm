<%@ Page Title="Loading..." Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAppAegisCRM.Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript">
        window.history.forward(1); 
    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:Timer ID="Timer1" runat="server" Interval="3000" OnTick="Timer1_Tick">
    </asp:Timer>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <center>
        <div style="font-family: Cambria, Georgia, serif; color:#565656">
            <img src="images/Aegis_CRM_Logo.png" />
            <br />
            <h2>
                Welcome to Aegis Customer Relationship Management Portal</h2>
            <br />
            <h5>
                Please wait while we are redirecting you to the log in.</h5>
            <img src="images/loading.gif" />
        </div>
    </center>
    </form>
</body>
</html>
