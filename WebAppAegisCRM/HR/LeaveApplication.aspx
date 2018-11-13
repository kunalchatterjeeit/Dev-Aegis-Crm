<%@ Page Title="Leave Application" Language="C#" AutoEventWireup="true" CodeBehind="LeaveApplication.aspx.cs"
     MasterPageFile="~/Main.Master" Inherits="WebAppAegisCRM.HR.LeaveApplication" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <br />

    <div class="row">
        <div class="col-lg-12">
            <div class="span6">
                    <div class="widget-box">
                        <div class="widget-title">
                            <span class="icon"><i class="icon-align-justify"></i></span>
                            <h5>Add Leave Type</h5>
                        </div>
                        <div class="widget-content">
                            <div class="form-horizontal">
                                <div class="control-group">
                                    <label class="control-label">Leave Type :</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtLeaveType" runat="server" class="span11" placeholder="Leave Type"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <label class="control-label">Note :</label>
                                    <div class="controls">
                                        <asp:TextBox ID="txtNote" runat="server" class="span11" placeholder="Note" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="control-group">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-success" OnClick="btnSave_Click" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-danger" OnClick="btnCancel_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
             <div class="span6">
                    <div class="widget-box">
                        <div class="widget-title">
                            <span class="icon"><i class="icon-align-justify"></i></span>
                            <h5>Leave type List</h5>
                        </div>
                        <div class="widget-content">
                            <div class="form-horizontal">
                                <asp:GridView ID="gvLeaveType" runat="server" class="table table-bordered table-striped"
                                    AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                                    OnPageIndexChanging="gvLeaveType_PageIndexChanging" OnRowCommand="gvLeaveType_RowCommand">
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                SN.
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex+1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Leave Name">
                                            <ItemTemplate>
                                                <%# Eval("LeaveTypeName") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgEdit" runat="server" CausesValidation="false" CommandName="E"
                                                    CommandArgument='<%# Eval("LeaveTypeId") %>' ImageUrl="~/Images/edit_button.png"
                                                    ImageAlign="AbsMiddle" ToolTip="EDIT" Width="15px" Height="15px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImgDelete" runat="server" CausesValidation="false" CommandName="D"
                                                    CommandArgument='<%# Eval("RoleId") %>' ImageUrl="~/images/delete_button.png"
                                                    ImageAlign="AbsMiddle" ToolTip="DELETE" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#0349AA" Font-Bold="True" ForeColor="White" />
                                    <RowStyle CssClass="RowStyle" BackColor="#F7F6F3" ForeColor="#333333" />
                                    <EditRowStyle BackColor="#999999" />
                                    <EmptyDataRowStyle CssClass="EditRowStyle" />
                                    <AlternatingRowStyle CssClass="AltRowStyle" BackColor="White" ForeColor="#284775" />
                                    <EmptyDataTemplate>
                                        No Record Found...
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            <div class="panel panel-default">
                <div class="panel-heading" style="font-size: large;">
                    Employee Leave Request Form
                </div>
                <div class="panel-body">
                    <div class="row">
                       
                    </div>
                    <div class="row">
                        <div class="col-lg-4">
                            <div class="form-group has-error">
                                Leave Application No
                                <asp:TextBox ID="txtLeaveApplicationNo" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                         <div class="col-lg-4">
                            <div class="form-group has-error">
                                Requestor Id
                                <asp:TextBox ID="txtRequestorId" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                         <div class="col-lg-4">
                            <div class="form-group has-error">
                                Leave Type Id
                                <asp:DropDownList ID="ddLeavetypeid" runat="server" CssClass="form-control" DataTextField="RoleName"
                                    DataValueField="">
                                </asp:DropDownList>
                            </div>
                        </div>
                         <div class="col-lg-4">
                            <div class="form-group">
                                Leave Accumulation Type Id
                                <asp:TextBox ID="txtLeaveAccumulationTypeId" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group has-error">
                                Leave Start Date:
                                <asp:TextBox ID="txtLSD" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd MMM yyyy"
                                    Enabled="True" TargetControlID="txtLSD">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                          <div class="col-lg-4">
                            <div class="form-group has-error">
                                Leave End Date:
                                <asp:TextBox ID="txtLED" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd MMM yyyy"
                                    Enabled="True" TargetControlID="txtLED">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                          <div class="col-lg-4">
                            <div class="form-group has-error">
                                Leave Apply Date:
                                <asp:TextBox ID="txtLAP" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd MMM yyyy"
                                    Enabled="True" TargetControlID="txtLAP">
                                </asp:CalendarExtender>
                            </div>
                               <div class="col-lg-4">
                            <div class="form-group has-error">
                                No of Days:
                                <asp:TextBox ID="txtNoOfDays" CssClass="form-control" runat="server"></asp:TextBox>
                               
                               
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                Reason:
                                <asp:TextBox ID="txtReason" CssClass="form-control" runat="server" OnTextChanged="txtReason_TextChanged"></asp:TextBox>
                            </div>
                        </div>
                         <div class="col-lg-4">
                            <div class="form-group">
                       Attachment
                                <asp:TextBox ID="txtAttachment" CssClass="form-control" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-outline btn-success"
                                OnClick="btnSubmit_Click" OnClientClick="return ValidationForSave();" />
                            <asp:Button ID="btnReset" runat="server" Text="Reset" CssClass="btn btn-outline btn-warning"
                                OnClick="btnReset_Click" />
                        </div>
                </div>
              </div>      
            </div>
        </div>
         <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Leave Request Arrived
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:GridView ID="gvLeaveMaster" DataKeyNames="LeaveMasterId" runat="server"
                            AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                            GridLines="None" Style="text-align: left" OnRowCommand="gvLeaveMaster_RowCommand"
                            HeaderStyle-HorizontalAlign="Center">
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
                                    <asp:BoundField HeaderText="From" DataField="FromDate" />
                                    <asp:BoundField HeaderText="To" DataField="ToDate" />
                                    <asp:BoundField HeaderText="Apply Date" DataField="ApplyDate" />
                                    <asp:BoundField HeaderText="Reason" DataField="Reason" />
                                 <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgApproved" runat="server" CausesValidation="false" CommandName="A"
                                            CommandArgument='<%# Eval("LeaveMasterId") %>' ImageUrl="~/Images/approved.png"
                                            ImageAlign="AbsMiddle" ToolTip="APPROV" Width="20px" Height="20px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgRejected" runat="server" CausesValidation="false" CommandName="R"
                                            CommandArgument='<%# Eval("LeaveMasterId") %>' ImageUrl="~/Images/Rejected.png"
                                            ImageAlign="AbsMiddle" ToolTip="REJECT" Width="20px" Height="20px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgEdit" runat="server" CausesValidation="false" CommandName="E"
                                            CommandArgument='<%# Eval("LeaveMasterId") %>' ImageUrl="~/Images/edit_button.png"
                                            ImageAlign="AbsMiddle" ToolTip="EDIT" Width="20px" Height="20px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ImgDelete" runat="server" CausesValidation="false" CommandName="D"
                                            CommandArgument='<%# Eval("LeaveMasterId") %>' ImageUrl="~/images/delete_button.png"
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
              <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Leave Request Arrived
                </div>
                <div class="panel-body">
                    <div class="table-responsive">
                        <asp:GridView ID="GridView1" DataKeyNames="LeaveMasterId" runat="server"
                            AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                            GridLines="None" Style="text-align: left" OnRowCommand="gvLeaveMaster_RowCommand"
                            HeaderStyle-HorizontalAlign="Center">
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
                                    <asp:BoundField HeaderText="From" DataField="FromDate" />
                                    <asp:BoundField HeaderText="To" DataField="ToDate" />
                                    <asp:BoundField HeaderText="No. of days" DataField="LeaveCount" />
                                    <asp:BoundField HeaderText="Reason" DataField="Reason" />
                                    <asp:BoundField HeaderText="Approved Leave" DataField="LeaveStatusName" />
                                    <asp:BoundField HeaderText="Rejected Leave" DataField="LeaveStatusName" />
                                    <asp:BoundField HeaderText="Edit Leave" DataField="LeaveStatusName" />
                                
                              
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
        </div>
    </div>
        </div>
    
</asp:Content>
