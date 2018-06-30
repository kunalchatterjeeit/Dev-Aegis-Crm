<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CalendarControl.aspx.cs" Inherits="WebAppAegisCRM.CalendarControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    
    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <div>
        <asp:Calendar ID="Calendar1" runat="server" Height="560px" OnSelectionChanged="Calendar1_SelectionChanged" Width="1153px" ShowGridLines="True"></asp:Calendar>
    </div>
         <a id="lnk" runat="server"></a>
            <asp:ModalPopupExtender ID="ModalPopupExtender1" BackgroundCssClass="myModalPopupbackGrnd"
                runat="server" TargetControlID="lnk" PopupControlID="Panel1" CancelControlID="imgbtn">
            </asp:ModalPopupExtender>
            <asp:Panel ID="Panel1" runat="server" CssClass="myModalPopup-10" Style="display: none; z-index: 10000; position: absolute">
                <asp:Panel ID="dragHandler" runat="server" class="popup-working-section" ScrollBars="Auto">
                    <h6 id="popupHeader1" runat="server" class="popup-header-companyname"></h6>
                    <asp:TabContainer ID="TabContainer1" runat="server" Width="100%" CssClass="MyTabStyle"
                        ActiveTabIndex="1">
                        <asp:TabPanel ID="PurchaseDetails" runat="server">
                            <HeaderTemplate>
                                Tag Details
                            </HeaderTemplate>
                            <ContentTemplate>
                                <div class="accountInfo" style="width: 95%; float: left">
                                    <fieldset class="login">
                                        <legend>Enter model details</legend>
                                        
                                        <br />
                                        

                                    </fieldset>
                                </div>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel ID="PurchaseList" runat="server">
                            <HeaderTemplate>
                                Tag List
                            </HeaderTemplate>
                            <ContentTemplate>
                                
                            </ContentTemplate>
                        </asp:TabPanel>
                    </asp:TabContainer>
                </asp:Panel>
                <img id="imgbtn" runat="server" src="../images/close-button.png" style="float: right; margin-right: 1px; cursor: pointer"
                    alt="Close" />
            </asp:Panel>
    </form>
</body>
</html>
