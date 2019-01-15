<%@ Page Title="LEAVE CALENDAR" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="LeaveCanlendar.aspx.cs" Inherits="WebAppAegisCRM.LeaveManagement.LeaveCanlendar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid"
        CellSpacing="1" Font-Names="Verdana"
        MultiSelectedDates="true" OnDayRender="Calendar1_DayRender"
        Font-Size="9pt" ForeColor="Black" NextPrevFormat="ShortMonth"
        SelectionMode="Day" Style="width: 100%; height: 45vh">
        <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" />
        <DayStyle BackColor="#CCCCCC" />
        <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
        <OtherMonthDayStyle ForeColor="#999999" />
        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
        <TitleStyle BackColor="#333399" BorderStyle="Solid" Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="12pt" />
        <TodayDayStyle BackColor="#999999" ForeColor="White" />
    </asp:Calendar>
</asp:Content>
