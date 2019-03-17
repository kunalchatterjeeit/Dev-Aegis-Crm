<%@ Page Title="ADD/EDIT CALLS" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Calls.aspx.cs" Inherits="WebAppAegisCRM.Sales.Calls" %>
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
                            Add/Edit Calls
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                 <div class="col-lg-4">
                                    <div class="form-group has-error">
                                       Call Subject
                                <asp:TextBox ID="txtSubject" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                       Call Description
                                <asp:TextBox ID="txtDescription" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Call Status
                                <asp:DropDownList ID="ddlCallStatus" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                    </div>
                                </div>   
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Call Repeat Type
                                <asp:DropDownList ID="ddlCallRepeatType" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                    </div>
                                </div>   
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Call Direction
                                <asp:DropDownList ID="ddlCallDirection" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                    </div>
                                </div>   
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Call Related To
                                <asp:DropDownList ID="ddlCallRelatedTo" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                    </div>
                                </div>   
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        Call Start Date Time
                                <asp:TextBox ID="txtStartDateTime" CssClass="form-control" runat="server"></asp:TextBox>
                                          <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtStartDateTime"
                                            Format="dd MMM yyyy HH:mm:ss tt" Enabled="True">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Call End Date Time
                                <asp:TextBox ID="txtEndDateTime" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtEndDateTime"
                                            Format="dd MMM yyyy HH:mm:ss tt" Enabled="True">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        <asp:CheckBox ID="chkPopupReminder" runat="server" Text="Pop Up Reminder" Checked="true" />
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        <asp:CheckBox ID="chkEmailReminder" runat="server" Text="Email Reminder" Checked="true" />
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-outline btn-success" OnClick="btnSave_Click"/>
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-outline btn-warning" OnClick="btnCancel_Click"/>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <uc3:Message ID="Message" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Call List
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <center>
                                    <asp:GridView ID="gvCalls" runat="server" Width="100%" AutoGenerateColumns="false" class="table table-striped"
                                        GridLines="None" AllowPaging="false" CellPadding="0" CellSpacing="0" DataKeyNames="Id" ForeColor="#333333" OnRowCommand="gvCalls_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL" ItemStyle-Width="15px">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Subject" HeaderText="Subject" />
                                            <asp:BoundField DataField="CallStatus" HeaderText="Call Status" />
                                            <asp:BoundField DataField="StartDateTime" HeaderText="Start Date" />
                                            <asp:BoundField DataField="EndDateTime" HeaderText="End Date" />
                                            <asp:TemplateField ItemStyle-Width="15px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_button.png" CommandName="Ed"
                                                        Width="15px" Height="15px" CommandArgument='<%# Eval("Id") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="15px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete_button.png"
                                                        CommandName="Del" Width="15px" Height="15px" OnClientClick="return confirm('Are You Sure?');" CommandArgument='<%# Eval("Id") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#5bb0de" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#379ed6" Font-Bold="True" ForeColor="White" />
                                        <RowStyle CssClass="RowStyle" BackColor="#F7F6F3" ForeColor="#333333" />
                                        <EditRowStyle BackColor="#999999" />
                                        <EmptyDataRowStyle CssClass="EditRowStyle" />
                                        <AlternatingRowStyle CssClass="AltRowStyle" BackColor="White" ForeColor="#284775" />
                                        <PagerSettings Mode="NumericFirstLast" PageButtonCount="12" FirstPageText="First"
                                            LastPageText="Last" />
                                        <PagerStyle CssClass="PagerStyle" BackColor="#379ed6" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                        <EmptyDataTemplate>
                                            No Record Found...
                                        </EmptyDataTemplate>
                                    </asp:GridView>
                                </center>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
