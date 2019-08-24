<%@ Page Title="Manage Role Access" Language="C#" MasterPageFile="~/Main.Master"
    AutoEventWireup="True" CodeBehind="RoleAccessLevel.aspx.cs" Inherits="WebAppAegisCRM.HR.RoleAccessLevel" %>

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
                                        <asp:DropDownList ID="ddlRole" runat="server" AutoPostBack="true" Width="290px" CssClass="form-control searchable"
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
                            Control Panel
                        </div>
                        <div class="panel-body">
                            <asp:CheckBoxList ID="ChkControlPanel" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CheckListBox_SelectedIndexChanged">
                                <asp:ListItem Value="600" Text="&nbsp;&nbsp;&nbsp;CONTROL PANEL"></asp:ListItem>
                                <asp:ListItem Value="601" Text="&nbsp;&nbsp;&nbsp;SERVICE CALL ATTENDANCE MANAGER"></asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </div>
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
                                <asp:ListItem Value="807" Text="&nbsp;&nbsp;&nbsp;ADD/EDIT SALES DEPARTMENT">
                                </asp:ListItem>
                                 <asp:ListItem Value="808" Text="&nbsp;&nbsp;&nbsp;ADD/EDIT SALES LEAD SOURCE">
                                </asp:ListItem>
                                 <asp:ListItem Value="809" Text="&nbsp;&nbsp;&nbsp;ADD/EDIT SALES MEETING TYPE">
                                </asp:ListItem>
                                 <asp:ListItem Value="810" Text="&nbsp;&nbsp;&nbsp;ADD/EDIT SALES TASK STATUS">
                                </asp:ListItem>
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
                                <asp:ListItem Value="205" Text="&nbsp;&nbsp;&nbsp;EMPLOYEE LOYALTY POINT"></asp:ListItem>
                                <asp:ListItem Value="206" Text="&nbsp;&nbsp;&nbsp;ADD/EDIT HOLIDAY PROFILE"></asp:ListItem>
                                <asp:ListItem Value="207" Text="&nbsp;&nbsp;&nbsp;ADD/EDIT HOLIDAY"></asp:ListItem>
                                <asp:ListItem Value="208" Text="&nbsp;&nbsp;&nbsp;EMPLOYEE HOLIDAY PROFILE MAPPING"></asp:ListItem>
                                <asp:ListItem Value="209" Text="&nbsp;&nbsp;&nbsp;HOLIDAY LIST"></asp:ListItem>
                                <asp:ListItem Value="211" Text="&nbsp;&nbsp;&nbsp;MANAGE ATTENDANCE"></asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-lg-4">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            Leave Management
                        </div>
                        <div class="panel-body">
                            <asp:CheckBoxList ID="chkListLeaveManagement" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CheckListBox_SelectedIndexChanged">
                                <asp:ListItem Value="700" Text="&nbsp;&nbsp;&nbsp;LEAVE MANAGEMENT"></asp:ListItem>
                                <asp:ListItem Value="701" Text="&nbsp;&nbsp;&nbsp;LEAVE CONFIGURATION"></asp:ListItem>
                                <asp:ListItem Value="702" Text="&nbsp;&nbsp;&nbsp;LEAVE DESIGNATION CONFIGURATION"></asp:ListItem>
                                <asp:ListItem Value="703" Text="&nbsp;&nbsp;&nbsp;APPLY LEAVE"></asp:ListItem>
                                <asp:ListItem Value="704" Text="&nbsp;&nbsp;&nbsp;LEAVE APPROVE/REJECT/CANCEL"></asp:ListItem>
                                <asp:ListItem Value="705" Text="&nbsp;&nbsp;&nbsp;MY LEAVE APPLICATION LIST"></asp:ListItem>
                                <asp:ListItem Value="706" Text="&nbsp;&nbsp;&nbsp;GENERATE LEAVE"></asp:ListItem>
                                <asp:ListItem Value="707" Text="&nbsp;&nbsp;&nbsp;LEAVE ADJUSTMENT"></asp:ListItem>
                                <asp:ListItem Value="708" Text="&nbsp;&nbsp;&nbsp;LEAVE REPORT"></asp:ListItem>
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
                                <asp:ListItem Value="307" Text="&nbsp;&nbsp;&nbsp;SALE CHALLAN ENTRY">
                                </asp:ListItem>
                                <asp:ListItem Value="308" Text="&nbsp;&nbsp;&nbsp;SALE CHALLAN LIST">
                                </asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            Sales
                        </div>
                        <div class="panel-body">
                            <asp:CheckBoxList ID="chkListSales" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CheckListBox_SelectedIndexChanged">
                                <asp:ListItem Value="800" Text="&nbsp;&nbsp;&nbsp;SALES">
                                </asp:ListItem>                                 
                                <asp:ListItem Value="805" Text="&nbsp;&nbsp;&nbsp;SALES ACCOUNTS">
                                </asp:ListItem>
                                <asp:ListItem Value="806" Text="&nbsp;&nbsp;&nbsp;SALES LEADS">
                                </asp:ListItem>
                                <asp:ListItem Value="812" Text="&nbsp;&nbsp;&nbsp;SALES OPPORTUNITY">
                                </asp:ListItem>
                                <asp:ListItem Value="814" Text="&nbsp;&nbsp;&nbsp;SALES QUOTE">
                                </asp:ListItem>
                                <asp:ListItem Value="813" Text="&nbsp;&nbsp;&nbsp;SALES CONTACTS">
                                </asp:ListItem>
                                <asp:ListItem Value="801" Text="&nbsp;&nbsp;&nbsp;SALES CALLS">
                                </asp:ListItem>
                                <asp:ListItem Value="802" Text="&nbsp;&nbsp;&nbsp;SALES MEETINGS">
                                </asp:ListItem>
                                <asp:ListItem Value="803" Text="&nbsp;&nbsp;&nbsp;SALES NOTES">
                                </asp:ListItem>
                                <asp:ListItem Value="804" Text="&nbsp;&nbsp;&nbsp;SALES TASKS">
                                </asp:ListItem>
                                <asp:ListItem Value="811" Text="&nbsp;&nbsp;&nbsp;SALES CAMPAIGN">
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
                                <asp:ListItem Value="416" Text="&nbsp;&nbsp;&nbsp;ASSIGN ENGINEER BULK"></asp:ListItem>
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
                                <asp:ListItem Value="504" Text="&nbsp;&nbsp;&nbsp;TONER REQUEST LIST"></asp:ListItem>
                                <asp:ListItem Value="505" Text="&nbsp;&nbsp;&nbsp;DOCKET LIST"></asp:ListItem>
                                <asp:ListItem Value="506" Text="&nbsp;&nbsp;&nbsp;SERVICE BOOK LIST"></asp:ListItem>
                                <asp:ListItem Value="507" Text="&nbsp;&nbsp;&nbsp;SPARE/TONER USAGE LIST"></asp:ListItem>
                                <asp:ListItem Value="503" Text="&nbsp;&nbsp;&nbsp;EMPLOYEE LIST"></asp:ListItem>
                                <asp:ListItem Value="210" Text="&nbsp;&nbsp;&nbsp;ATTENDANCE REPORT"></asp:ListItem>
                                <asp:ListItem Value="212" Text="&nbsp;&nbsp;&nbsp;EMPLOYEE WORK SUMMARY REPORT"></asp:ListItem>
                                <asp:ListItem Value="906" Text="&nbsp;&nbsp;&nbsp;CLAIM REPORT"></asp:ListItem>
                                <asp:ListItem Value="908" Text="&nbsp;&nbsp;&nbsp;VOUCHER REPORT"></asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </div>
                <div class="col-lg-4">
                    <div class="panel panel-primary">
                        <div class="panel-heading">
                            Claim Management
                        </div>
                        <div class="panel-body">
                            <asp:CheckBoxList ID="chkListClaim" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CheckListBox_SelectedIndexChanged">
                                <asp:ListItem Value="900" Text="&nbsp;&nbsp;&nbsp;CLAIM MANAGEMENT"></asp:ListItem>
                                <asp:ListItem Value="901" Text="&nbsp;&nbsp;&nbsp;CLAIM CONFIGURATION"></asp:ListItem>
                                <asp:ListItem Value="902" Text="&nbsp;&nbsp;&nbsp;CLAIM DESIGNATION CONFIGURATION"></asp:ListItem>
                                <asp:ListItem Value="903" Text="&nbsp;&nbsp;&nbsp;APPLY CLAIM"></asp:ListItem>
                                <asp:ListItem Value="904" Text="&nbsp;&nbsp;&nbsp;CLAIM APPROVE/REJECT/CANCEL"></asp:ListItem>
                                <asp:ListItem Value="905" Text="&nbsp;&nbsp;&nbsp;MY CLAIM APPLICATION LIST"></asp:ListItem>
                                <asp:ListItem Value="907" Text="&nbsp;&nbsp;&nbsp;CLAIM DISBURSEMENT"></asp:ListItem>
                            </asp:CheckBoxList>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
