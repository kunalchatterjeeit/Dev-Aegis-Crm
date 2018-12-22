<%@ Page Title="LEAVE APPLICATION LIST" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="LeaveApplicationList.aspx.cs" Inherits="WebAppAegisCRM.LeaveManagement.LeaveApplicationList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Leave Application List
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:GridView ID="gvLeaveApplicationList" runat="server" AllowPaging="True" PageSize="20"
                            AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                            GridLines="None" Style="text-align: left" OnPageIndexChanging="gvLeaveApplicationList_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SN.
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="LeaveApplicationNumber" HeaderText="Application Number" />
                                <asp:BoundField DataField="LeaveTypeName" HeaderText="Leave Type" />
                                <asp:BoundField DataField="FromDate" HeaderText="From" />
                                <asp:BoundField DataField="ToDate" HeaderText="To" />
                                <asp:BoundField DataField="LeaveAccumulationTypeName" HeaderText="Accumulation Type" />
                                <asp:BoundField DataField="LeaveStatusName" HeaderText="Status" />
                            </Columns>
                            <FooterStyle BackColor="#5bb0de" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5bc0de" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle CssClass="RowStyle" BackColor="#F7F6F3" ForeColor="#333333" />
                            <EditRowStyle BackColor="#999999" />
                            <EmptyDataRowStyle CssClass="EditRowStyle" />
                            <AlternatingRowStyle CssClass="AltRowStyle" BackColor="White" ForeColor="#284775" />
                            <PagerStyle CssClass="PagerStyle" BackColor="#379ed6" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <EmptyDataTemplate>
                                No Leave Application found...
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
