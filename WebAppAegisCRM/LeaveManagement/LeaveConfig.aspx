<%@ Page Title="Leave configuration Master" Language="C#" AutoEventWireup="true" CodeBehind="LeaveConfig.aspx.cs" 
    Inherits="WebAppAegisCRM.LeaveManagement.LeaveConfig" MasterPageFile="~/Main.Master" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    
    <br />
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Add/Edit Leave Configuration
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group has-error">
                                LeaveFrequency
                                <asp:TextBox ID="txtLeaveFrequency" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group has-error">
                                Leave Accure Date
                                <asp:TextBox ID="txtLeaveAccureDate" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                         <div class="col-lg-6">
                            <div class="form-group has-error">
                                Carry Forward Count
                                <asp:TextBox ID="txtCarryForwardCount" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                         <div class="col-lg-6">
                            <div class="form-group has-error">
                                Encashable
                                <asp:TextBox ID="txtEncashable" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                         <div class="col-lg-6">
                            <div class="form-group has-error">
                                CreatedDate
                                <asp:TextBox ID="txtCreatedDate" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                         <div class="col-lg-6">
                            <div class="form-group has-error">
                                Active
                                <asp:TextBox ID="txtActive" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <br />
                                <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-outline btn-success"
                                    OnClick="btnSave_Click" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-outline btn-warning"
                                    OnClick="btnCancel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-lg-4">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Leave Configuration List
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <center>
                            <asp:GridView ID="dgvLeaveConfiguration" runat="server" Width="100%" AutoGenerateColumns="false" class="table table-striped"
                                GridLines="None" AllowPaging="false" CellPadding="0" CellSpacing="0" DataKeyNames="RoleId" ForeColor="#333333" OnRowCommand="dgvLeaveConfiguration_RowCommand">
                                <Columns>
                                    <asp:TemplateField HeaderText="SL" ItemStyle-Width="15px">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="LeaveConfigId" HeaderText="Leave Config Id" />
                                     <asp:BoundField DataField="LeaveAccureDate" HeaderText="Leave Accure Date" />
                                     <asp:BoundField DataField="CarryForwardCount" HeaderText="Carry Forward Count" />
                                     <asp:BoundField DataField=" Encashable" HeaderText=" Encashable" />
                                     <asp:BoundField DataField="CreatedDate" HeaderText="CreatedDate" />
                               
                                    <asp:TemplateField ItemStyle-Width="15px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/Images/edit_button.png" CommandName="Ed"
                                                Width="15px" Height="15px" CommandArgument='<%# Eval("LeaveConfigId") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-Width="15px">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Images/delete_button.png"
                                                CommandName="Del" Width="15px" Height="15px" OnClientClick="return confirm('Are You Sure?');" CommandArgument='<%# Eval("RoleId") %>' />
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
</asp:Content>

