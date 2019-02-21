<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SpareRequisition.aspx.cs" Inherits="WebAppAegisCRM.Service.SpareRequisition" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SPARE REQUISITION</title>
    <link href="../dist/css/custom15112018.css" rel="stylesheet" />
    <link href="../dist/css/custom-popup.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div style="overflow: scroll; height: 90vh;">
                    <asp:GridView ID="gvSpareList" DataKeyNames="SpareId" runat="server"
                        AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                        class="table table-striped" GridLines="None" Style="text-align: left">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSpare" runat="server" OnCheckedChanged="chkSpare_CheckedChanged" AutoPostBack="true"></asp:CheckBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    SN.
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Spare" DataField="SpareName" />
                            <asp:BoundField HeaderText="Yield" DataField="Yield" />
                            <asp:TemplateField>
                                <HeaderTemplate>Last A4 BW</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblA4BWLastReading" runat="server" Text="0"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>Quantity</HeaderTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRequisiteQty" runat="server" TextMode="Number" Text="1"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#5bb0de" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#379ed6" Font-Bold="True" ForeColor="White" />
                        <RowStyle CssClass="RowStyle" BackColor="#F7F6F3" ForeColor="#333333" />
                        <EditRowStyle BackColor="#999999" />
                        <EmptyDataRowStyle CssClass="EditRowStyle" />
                        <AlternatingRowStyle CssClass="AltRowStyle" BackColor="White" ForeColor="#284775" />
                        <PagerStyle CssClass="PagerStyle" BackColor="#379ed6" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <EmptyDataTemplate>
                            No Record Found...
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Button ID="btnProceed" runat="server" Text="Proceed" class="btn" OnClick="btnProceed_Click"/>
    </form>
</body>
</html>
