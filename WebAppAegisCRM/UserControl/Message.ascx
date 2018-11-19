<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Message.ascx.cs" Inherits="WebAppAegisCRM.UserControl.Message" %>
<span id="spanSuccess" runat="server" style="line-height: 30px; border: 1px solid #72E837; float: left; background: #CEF7B9;
    font-size: 10pt; padding: 5px 2px 5px 30px; color: #58BC21; width: 90%; position: relative;
    margin: 0px 30px 20px 0px">
    <img src="../images/Success.png" alt="SUCCESS" style="margin-bottom: 5px" />
    <asp:Label ID="lblSuccess" runat="server" Style="margin: 0px 15px"></asp:Label></span>
<span id="spanError" runat="server" style="line-height: 30px; border: 1px solid #FF0602; float: left; background: #FFB793;
    font-size: 10pt; padding: 5px 2px 5px 30px; color: #ED1410; width: 90%; position: relative;
    margin: 0px 30px 20px 0px">
    <img src="../images/Error.png" alt="ERROR" style="margin-bottom: 3px" />
    <asp:Label
        l ID="lblError" runat="server" Style="margin: 0px 15px"></asp:Label></span>