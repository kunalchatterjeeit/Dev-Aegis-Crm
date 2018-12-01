<%@ Page Title="USER SETTINGS" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="UserSettings.aspx.cs" Inherits="WebAppAegisCRM.Settings.UserSettings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .header {
            background-color: #356BA0;
            font-family: Calibri;
            font-size: 13px;
            text-transform: uppercase;
            color: #fff;
            font-weight: bold;
            padding: 8px 0 8px 0;
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
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
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Manage Settings
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            Dashboard Settings
                                        </div>
                                        <div class="panel-body">
                                            <asp:CheckBoxList ID="ChkDashboardSettings" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CheckListBox_SelectedIndexChanged">
                                                <asp:ListItem Value="10001" Text="&nbsp;&nbsp;&nbsp;DOCKET LIST"></asp:ListItem>
                                                <asp:ListItem Value="10002" Text="&nbsp;&nbsp;&nbsp;TONER LIST"></asp:ListItem>
                                                <asp:ListItem Value="10003" Text="&nbsp;&nbsp;&nbsp;CONTRACT STATUS CHART"></asp:ListItem>
                                                <asp:ListItem Value="10004" Text="&nbsp;&nbsp;&nbsp;CONTRACT EXPIRING SOON LIST"></asp:ListItem>
                                                <asp:ListItem Value="10005" Text="&nbsp;&nbsp;&nbsp;CONTRACT EXPIRED LIST"></asp:ListItem>
                                            </asp:CheckBoxList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
