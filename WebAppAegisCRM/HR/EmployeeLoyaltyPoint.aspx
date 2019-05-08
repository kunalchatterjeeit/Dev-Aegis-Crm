<%@ Page Title="EMPLOYEE LOYALTY POINT" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="EmployeeLoyaltyPoint.aspx.cs" Inherits="WebAppAegisCRM.HR.EmployeeLoyaltyPoint" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
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
                            Search Criteria
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        Month
                                <asp:DropDownList ID="ddlMonth" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0" Selected="True">--Select--</asp:ListItem>
                                    <asp:ListItem Value="Jan">January</asp:ListItem>
                                    <asp:ListItem Value="Feb">February</asp:ListItem>
                                    <asp:ListItem Value="Mar">March</asp:ListItem>
                                    <asp:ListItem Value="Apr">April</asp:ListItem>
                                    <asp:ListItem Value="May">May</asp:ListItem>
                                    <asp:ListItem Value="Jun">June</asp:ListItem>
                                    <asp:ListItem Value="Jul">July</asp:ListItem>
                                    <asp:ListItem Value="Aug">August</asp:ListItem>
                                    <asp:ListItem Value="Sep">September</asp:ListItem>
                                    <asp:ListItem Value="Oct">October</asp:ListItem>
                                    <asp:ListItem Value="Nov">November</asp:ListItem>
                                    <asp:ListItem Value="Dec">December</asp:ListItem>
                                </asp:DropDownList>
                                    </div>
                                </div>
                                <!-- /.col-lg-6 (nested) -->
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        Year
                                <asp:DropDownList ID="ddlYear" CssClass="form-control" runat="server">
                                </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" class="btn btn-outline btn-primary"
                                            OnClick="btnSearch_Click" />
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <uc3:Message ID="Message1" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Employee Loyalty Point Distribution List
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <asp:GridView ID="gvEmployeePoint" DataKeyNames="LoyaltyId" runat="server"
                                    AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333" AllowPaging="True" PageSize="20"
                                    class="table table-striped" GridLines="None" Style="text-align: left" OnRowCommand="gvEmployeePoint_RowCommand"
                                    OnRowDataBound="gvEmployeePoint_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                SN.
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Name" DataField="EmployeeName" />
                                        <asp:BoundField HeaderText="Designation" DataField="DesignationName" />
                                        <asp:TemplateField HeaderText="Call Status">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlLaoyalPointReason" CssClass="form-control" runat="server">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Point">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPoint" runat="server" CssClass="form-control" TextMode="Number" MaxLength="4" Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Note">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtNote" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="1"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btnEdit" runat="server" CommandName="E" ImageUrl="~/images/edit_button.png"
                                                    CommandArgument='<%#Eval("EmployeeMasterId") %>' Width="17px" Height="17px" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="25px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" class="fa fa-trash-o fa-fw" CausesValidation="false"
                                                    CommandName="D" OnClientClick="return confirm('Are You Sure?');" CommandArgument='<%# Eval("EmployeeMasterId") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="25px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#5bb0de" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#379ed6" Font-Bold="True" ForeColor="White" />
                                    <RowStyle CssClass="RowStyle" BackColor="#F7F6F3" ForeColor="#333333" />
                                    <EditRowStyle BackColor="#999999" />
                                    <EmptyDataRowStyle CssClass="EditRowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" BackColor="#F7F6F3" ForeColor="#284775" />
                                    <PagerStyle CssClass="PagerStyle" BackColor="#379ed6" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <EmptyDataTemplate>
                                        No Employee Found...
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
