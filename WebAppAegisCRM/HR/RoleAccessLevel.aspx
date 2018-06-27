<%@ Page Title="Manage Role Access" Language="C#" MasterPageFile="~/Main.Master"
    AutoEventWireup="true" CodeBehind="RoleAccessLevel.aspx.cs" Inherits="WebAppAegisCRM.HR.RoleAccessLevel" %>

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
                            Manage Role Access
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="form-group has-error">
                                        Role Name
                                        <asp:DropDownList ID="ddlRole" runat="server" AutoPostBack="true" Width="290px" CssClass="form-control"
                                            DataValueField="RoleId" DataTextField="RoleName" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-4">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            Settings
                        </div>
                        <div class="panel-body">
                            <asp:CheckBoxList ID="ChkLstSettings" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CheckListBox_SelectedIndexChanged">
                                <asp:ListItem Value="100" Text="&nbsp;&nbsp;&nbsp;SETTINGS"></asp:ListItem>
                                <asp:ListItem Value="101" Text="&nbsp;&nbsp;&nbsp;ADD/EDIT CITY"></asp:ListItem>
                                <asp:ListItem Value="102" Text="&nbsp;&nbsp;&nbsp;ADD/EDIT CONTRACT TYPE"></asp:ListItem>
                                <asp:ListItem Value="103" Text="&nbsp;&nbsp;&nbsp;ADD/EDIT BRAND"></asp:ListItem>
                                <asp:ListItem Value="104" Text="&nbsp;&nbsp;&nbsp;ADD/EDIT MODEL CATEGORY"></asp:ListItem>
                                <asp:ListItem Value="105" Text="&nbsp;&nbsp;&nbsp;ADD/EDIT MODEL"></asp:ListItem>
                                <asp:ListItem Value="106" Text="&nbsp;&nbsp;&nbsp;ADD/EDIT SPARE/CONSUMABLE"></asp:ListItem>
                                <asp:ListItem Value="107" Text="&nbsp;&nbsp;&nbsp;MODEL SPARE MAPPING"></asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            HR
                        </div>
                        <div class="panel-body">
                            <asp:CheckBoxList ID="ChkListHR" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CheckListBox_SelectedIndexChanged">
                                <asp:ListItem Value="200" Text="&nbsp;&nbsp;&nbsp;HR"></asp:ListItem>
                                <asp:ListItem Value="201" Text="&nbsp;&nbsp;&nbsp;ADD/EDIT ROLE"></asp:ListItem>
                                <asp:ListItem Value="202" Text="&nbsp;&nbsp;&nbsp;MANAGE ROLE ACCESS"></asp:ListItem>
                                <asp:ListItem Value="203" Text="&nbsp;&nbsp;&nbsp;ADD/EDIT EMPLOYEE"></asp:ListItem>
                                <asp:ListItem Value="204" Text="&nbsp;&nbsp;&nbsp;ADD/EDIT LOYALTY POINT REASON"></asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            Inventory
                        </div>
                        <div class="panel-body">
                            <asp:CheckBoxList ID="ChkListInventory" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CheckListBox_SelectedIndexChanged">
                                <asp:ListItem Value="300" Text="&nbsp;&nbsp;&nbsp;INVENTORY">
                                </asp:ListItem>
                                <asp:ListItem Value="301" Text="&nbsp;&nbsp;&nbsp;PURCHASE ENTRY">
                                </asp:ListItem>
                                <asp:ListItem Value="304" Text="&nbsp;&nbsp;&nbsp;PURCHASE LIST">
                                </asp:ListItem>
                                <asp:ListItem Value="305" Text="&nbsp;&nbsp;&nbsp;STOCK LOOKUP">
                                </asp:ListItem>
                                <asp:ListItem Value="302" Text="&nbsp;&nbsp;&nbsp;ADD/EDIT VENDOR">
                                </asp:ListItem>
                                <asp:ListItem Value="303" Text="&nbsp;&nbsp;&nbsp;VENDOR LIST">
                                </asp:ListItem>
                                 <asp:ListItem Value="306" Text="&nbsp;&nbsp;&nbsp;PURCHASE REQUISITION ENTRY">
                                </asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-4">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            Service
                        </div>
                        <div class="panel-body">
                            <asp:CheckBoxList ID="ChkListService" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CheckListBox_SelectedIndexChanged">
                                <asp:ListItem Value="400" Text="&nbsp;&nbsp;&nbsp;SERVICE"></asp:ListItem>
                                <asp:ListItem Value="401" Text="&nbsp;&nbsp;&nbsp;ADD/EDIT CUSTOMER"></asp:ListItem>
                                <asp:ListItem Value="412" Text="&nbsp;&nbsp;&nbsp;CUSTOMER LIST SHOW ALL"></asp:ListItem>
                                <asp:ListItem Value="402" Text="&nbsp;&nbsp;&nbsp;TAG CUSTOMER MODEL"></asp:ListItem>
                                <asp:ListItem Value="403" Text="&nbsp;&nbsp;&nbsp;DOCKET ENTRY"></asp:ListItem>
                                <%--<asp:ListItem Value="408" Text="&nbsp;&nbsp;&nbsp;DOCKET LIST SHOW ALL"></asp:ListItem>--%>
                                <asp:ListItem Value="405" Text="&nbsp;&nbsp;&nbsp;TONER REQUEST ENTRY"></asp:ListItem>
                                <asp:ListItem Value="413" Text="&nbsp;&nbsp;&nbsp;TONER REQUEST APPROVAL"></asp:ListItem>
                                <%--<asp:ListItem Value="409" Text="&nbsp;&nbsp;&nbsp;TONER REQUEST LIST SHOW ALL"></asp:ListItem>--%>
                                <asp:ListItem Value="407" Text="&nbsp;&nbsp;&nbsp;SERVICE BOOK"></asp:ListItem>
                                <asp:ListItem Value="415" Text="&nbsp;&nbsp;&nbsp;SERVICE BOOK SPARE APPROVAL"></asp:ListItem>
                                <asp:ListItem Value="411" Text="&nbsp;&nbsp;&nbsp;TONNER QUICK LINK PERMISSION"></asp:ListItem>
                                <asp:ListItem Value="410" Text="&nbsp;&nbsp;&nbsp;DOCKET QUICK LINK PERMISSION"></asp:ListItem>
                                <asp:ListItem Value="414" Text="&nbsp;&nbsp;&nbsp;CALL TRANSFER"></asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            Report
                        </div>
                        <div class="panel-body">
                            <asp:CheckBoxList ID="ChkListReport" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CheckListBox_SelectedIndexChanged">
                                <asp:ListItem Value="500" Text="&nbsp;&nbsp;&nbsp;REPORT"></asp:ListItem>
                                <asp:ListItem Value="501" Text="&nbsp;&nbsp;&nbsp;CUSTOMER LIST"></asp:ListItem>
                                <asp:ListItem Value="502" Text="&nbsp;&nbsp;&nbsp;CONTRACT STATUS REPORT"></asp:ListItem>
                                <asp:ListItem Value="503" Text="&nbsp;&nbsp;&nbsp;EMPLOYEE LIST"></asp:ListItem>
                                <asp:ListItem Value="504" Text="&nbsp;&nbsp;&nbsp;TONER REQUEST LIST"></asp:ListItem>
                                <asp:ListItem Value="505" Text="&nbsp;&nbsp;&nbsp;DOCKET LIST"></asp:ListItem>
                                <asp:ListItem Value="506" Text="&nbsp;&nbsp;&nbsp;SERVICE BOOK LIST"></asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
