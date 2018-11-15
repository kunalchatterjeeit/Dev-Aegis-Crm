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
                               Leave Type Id
                                <asp:DropDownList ID="ddlLeaveTypeId" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0">--SELECT--</asp:ListItem>
                                    <asp:ListItem Value="1">CL</asp:ListItem>
                                    <asp:ListItem Value="2">PL</asp:ListItem>
                                    <asp:ListItem Value="3">ML</asp:ListItem>
                                    
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group has-error">
                                Leave Frequency
                                <asp:TextBox ID="txtLeaveFrequency" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <br/>
                        <div class="col-lg-6">
                            <div class="form-group has-error">
                                Leave Accure Date
                                <asp:TextBox ID="txtLeaveAccureDate" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <br/>
                         <div class="col-lg-6">
                            <div class="form-group has-error">
                                Carry Forward Count
                                <asp:TextBox ID="txtCarryForwardCount" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <br/>
                         <div class="col-lg-6">
                            <div class="form-group has-error">
                                Encashable
                                <asp:CheckBox ID="ckEncashable" runat="server" />
                            </div>
                        </div>
                        
                        

                         <div class="col-lg-6">
                            <div class="form-group">
                                <br />
                                <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-outline btn-success"  OnClick="btnSave_Click"/>
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-outline btn-warning" OnClick="btnCancel_Click"/>
                            </div>
                        </div>
                       
                    </div>
                </div>
            </div>
        </div>
       
            
 <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Leave Configuration List
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                       <asp:GridView ID="dgvLeaveConfiguration"  runat="server"
                            AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                            GridLines="None" Style="text-align: left">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SN.
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                     <asp:BoundField DataField="LeaveConfigId" HeaderText="Leave Config Id" />
                                     <asp:BoundField DataField="LeaveTypeId" HeaderText="Leave Type" />  
                                     <asp:BoundField DataField="LeaveAccureDate" HeaderText="Leave Accure Date" />
                                     <asp:BoundField DataField="CarryForwardCount" HeaderText="Carry Forward Count" />
                                     <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgEdit" runat="server" CausesValidation="false" CommandName="E"
                                            CommandArgument='<%# Eval("LeaveConfigId") %>' ImageUrl="~/Images/edit_button.png"
                                            ImageAlign="AbsMiddle" ToolTip="EDIT" Width="20px" Height="20px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgDelete" runat="server" CausesValidation="false" CommandName="D"
                                            CommandArgument='<%# Eval("LeaveConfigId") %>' ImageUrl="~/images/delete_button.png"
                                            ImageAlign="AbsMiddle" ToolTip="Delete" Width="20px" Height="20px" OnClientClick="return confirm('Are You Sure?');" />
                                    </ItemTemplate>
                                </asp:TemplateField>
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
                                No Record Found...
                            </EmptyDataTemplate>
                        </asp:GridView>
                    </div>
                </div>
             </div>
           </div> 
                
        </div></asp:Content>

