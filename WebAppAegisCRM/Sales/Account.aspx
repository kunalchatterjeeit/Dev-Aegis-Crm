<%@ Page Title="ADD/EDIT ACCOUNTS" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Account.aspx.cs" Inherits="WebAppAegisCRM.Sales.Account" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../js/AutoComplete/jquery-ui.css" rel="stylesheet" type="text/css" />
    <%--<script type="text/javascript" src="../js/AutoComplete/jquery.min.js"></script>--%>
    <script type="text/javascript" src="../js/AutoComplete/jquery-ui.min.js"></script>
    <script type="text/javascript" src="../dist/js/custom.js"></script>
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
            <asp:HiddenField ID="hdnOpenForm" runat="server" ClientIDMode="Static" />
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <a id="hrefForm" data-toggle="collapse" data-parent="#accordion" href="#section5" title="Click to expand">
                                <i class="fa fa-chevron-down fa-fw"></i>Add/Edit Accounts
                            </a>
                        </div>
                        <div class="panel-collapse collapse out" id="section5">
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="form-group has-error">
                                            Link Type
                                <asp:DropDownList ID="ddlLinkType" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="form-group">
                                            Link Name
                                <asp:TextBox ID="txtCustomerName" CssClass="form-control"  runat="server" onkeydown="javascript:GetAutoCompleteCustomer(this)"></asp:TextBox>
                                            <asp:Button ID="Button1" runat="server" Text="Find" class="btn btn-outline btn-success" OnClick="btnFind_Click" />
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="form-group has-error">
                                            Lead Name
                                <asp:TextBox ID="txtName" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <div class="form-group">
                                            Lead Description
                                <asp:TextBox ID="txtDescription" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            Website
                                <asp:TextBox ID="txtWebsite" CssClass="form-control" runat="server" TextMode="Url"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            Industry
                                <asp:TextBox ID="txtIndustry" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group has-error">
                                            Office Phone
                                <asp:TextBox ID="txtOfficePhone" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            Customer Type
                                <asp:DropDownList ID="ddlCustomerType" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            Lead Source
                                <asp:DropDownList ID="ddlLeadSource" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group has-error">
                                            Employee Strength
                                        <asp:TextBox ID="txtEmployeeStrength" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            Annual Revenue
                                      <asp:TextBox ID="txtAnnualRevenue" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            Account Score
                                      <asp:TextBox ID="txtAccountScore" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-lg-4">
                                        <div class="form-group">
                                            Source Name
                                      <asp:TextBox ID="txtSourceName" CssClass="form-control" runat="server"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="clearfix"></div>
                                    <div class="col-lg-12">
                                        <div class="form-group">
                                            <br />
                                            <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-outline btn-success" OnClick="btnSave_Click" />
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-outline btn-warning" OnClick="btnCancel_Click" />
                                        </div>
                                    </div>
                                    <div class="col-lg-12">
                                        <uc3:Message ID="Message" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <a id="hrefAssigned" data-toggle="collapse" data-parent="#accordion" href="#section6" title="Click to expand">
                                <i class="fa fa-chevron-down fa-fw"></i>Assigned To
                            </a>
                            <asp:LinkButton ID="btnCreateEmployee" runat="server" CssClass="pull-right" Enabled="false">Create New</asp:LinkButton>
                        </div>
                        <div class="panel-collapse collapse out" id="section6">
                            <div class="panel-body">
                                <div class="table-responsive" style="height: 20vh; overflow: scroll">
                                    <center>
                                        <asp:GridView ID="gvAssignedEmployee" runat="server" Width="100%" AutoGenerateColumns="false" class="table table-striped"
                                            GridLines="None" AllowPaging="false" DataKeyNames="EmployeeMasterId" OnRowDataBound="gvAssignedEmployee_RowDataBound" CellPadding="0" CellSpacing="0" ForeColor="#333333" >
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL" ItemStyle-Width="25px">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="25px">
                                                    <HeaderTemplate>
                                                        Assign
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkAssigned" runat="server" OnCheckedChanged="chkAssigned_CheckedChanged" AutoPostBack="true"/>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="25px">
                                                    <HeaderTemplate>
                                                        Lead
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:RadioButton ID="rbtnIsLead" runat="server" OnCheckedChanged="rbtnIsLead_CheckedChanged" AutoPostBack="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" />
                                                 <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Assign Date
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# (Eval("AssignedDate")==null)?"":Convert.ToDateTime(Eval("AssignedDate").ToString()).ToString("dd MMM yyyy") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Revoke Date
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# (Eval("RevokeDate")==null)?"":Convert.ToDateTime(Eval("RevokeDate").ToString()).ToString("dd MMM yyyy") %>
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
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <a data-toggle="collapse" data-parent="#accordion" href="#section4" title="Click to expand">
                                <i class="fa fa-chevron-down fa-fw"></i>Calls List
                            </a>
                            <asp:LinkButton ID="btnCreateNewCall" runat="server" CssClass="pull-right" Enabled="false">Create New</asp:LinkButton>
                        </div>
                        <div class="panel-collapse collapse out" id="section4">
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
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Start Date
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Convert.ToDateTime(Eval("StartDateTime").ToString()).ToString("dd MMM yyyy HH:mm tt") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        End Date
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Convert.ToDateTime(Eval("EndDateTime").ToString()).ToString("dd MMM yyyy HH:mm tt") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="15px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" runat="server" class="fa fa-pencil-square-o fa-fw" CommandName="Ed" CausesValidation="false"
                                                            CommandArgument='<%# Eval("Id") %>' Style="font-size: 16px;"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="15px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" runat="server" class="fa fa-trash-o fa-fw" CausesValidation="false"
                                                            CommandName="Del" OnClientClick="return confirm('Are You Sure?');" Style="font-size: 16px;" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
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
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <a data-toggle="collapse" data-parent="#accordion" href="#section3" title="Click to expand">
                                <i class="fa fa-chevron-down fa-fw"></i>Meetings List
                            </a>
                            <asp:LinkButton ID="btnCreateNewMeeting" runat="server" CssClass="pull-right" Enabled="false">Create New</asp:LinkButton>
                        </div>
                        <div class="panel-collapse collapse out" id="section3">
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <center>
                                        <asp:GridView ID="gvMeetingss" runat="server" Width="100%" AutoGenerateColumns="false" class="table table-striped"
                                            GridLines="None" AllowPaging="false" CellPadding="0" CellSpacing="0" DataKeyNames="Id" ForeColor="#333333" OnRowCommand="gvMeetingss_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL" ItemStyle-Width="15px">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Name" HeaderText="Name" />
                                                <asp:BoundField DataField="MeetingType" HeaderText="Meeting Type" />
                                                <asp:BoundField DataField="MeetingStatus" HeaderText="Meeting Status" />
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        Start Date
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Convert.ToDateTime(Eval("StartDateTime").ToString()).ToString("dd MMM yyyy HH:mm tt") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        End Date
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Convert.ToDateTime(Eval("EndDateTime").ToString()).ToString("dd MMM yyyy HH:mm tt") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="15px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" runat="server" class="fa fa-pencil-square-o fa-fw" CommandName="Ed" CausesValidation="false"
                                                            CommandArgument='<%# Eval("Id") %>' Style="font-size: 16px;"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="15px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" runat="server" class="fa fa-trash-o fa-fw" CausesValidation="false"
                                                            CommandName="Del" Style="font-size: 16px;" OnClientClick="return confirm('Are You Sure?');" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
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
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            <a data-toggle="collapse" data-parent="#accordion" href="#section1" title="Click to expand">
                                <i class="fa fa-chevron-down fa-fw"></i>Notes List
                            </a>
                            <asp:LinkButton ID="btnCreateNewNote" runat="server" CssClass="pull-right" Enabled="false">Create New</asp:LinkButton>
                        </div>
                        <div class="panel-collapse collapse out" id="section1">
                            <div class="panel-body">
                                <div class="table-responsive">
                                    <center>
                                        <asp:GridView ID="gvNotes" runat="server" Width="100%" AutoGenerateColumns="false" class="table table-striped"
                                            GridLines="None" AllowPaging="false" CellPadding="0" CellSpacing="0" DataKeyNames="Id" ForeColor="#333333" OnRowCommand="gvNotes_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL" ItemStyle-Width="15px">
                                                    <ItemTemplate>
                                                        <%#Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Name" HeaderText="Name" />
                                                <asp:BoundField DataField="ContactName" HeaderText="Contact Name" />
                                                <asp:TemplateField ItemStyle-Width="15px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnEdit" runat="server" class="fa fa-pencil-square-o fa-fw" CommandName="Ed" CausesValidation="false"
                                                            CommandArgument='<%# Eval("Id") %>' Style="font-size: 16px;"></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-Width="15px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="btnDelete" runat="server" class="fa fa-trash-o fa-fw" CausesValidation="false"
                                                            CommandName="Del" Style="font-size: 16px;" OnClientClick="return confirm('Are You Sure?');" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
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
                <div class="col-md-12">
                    <div class="panel-group">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <a data-toggle="collapse" data-parent="#accordion" href="#section2" title="Click to expand">
                                    <i class="fa fa-chevron-down fa-fw"></i>Tasks List
                                </a>
                                <asp:LinkButton ID="btnCreateNewTask" runat="server" CssClass="pull-right" Enabled="false">Create New</asp:LinkButton>
                            </div>
                            <!--  End of heading -->
                            <div class="panel-collapse collapse out" id="section2">
                                <div class="panel-body">
                                    <div class="table-responsive">
                                        <center>
                                            <asp:GridView ID="gvTasks" runat="server" Width="100%" AutoGenerateColumns="false" class="table table-striped"
                                                GridLines="None" AllowPaging="false" CellPadding="0" CellSpacing="0" DataKeyNames="Id" ForeColor="#333333" OnRowCommand="gvTasks_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL" ItemStyle-Width="15px">
                                                        <ItemTemplate>
                                                            <%#Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Subject" HeaderText="Subject" />
                                                    <asp:BoundField DataField="TaskStatus" HeaderText="Task Status" />
                                                    <asp:BoundField DataField="TaskRelatedTo" HeaderText="Task Related To" />
                                                    <asp:BoundField DataField="TaskPriority" HeaderText="Task Priority" />
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Start Date
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Convert.ToDateTime(Eval("StartDateTime").ToString()).ToString("dd MMM yyyy HH:mm tt") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            End Date
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Convert.ToDateTime(Eval("EndDateTime").ToString()).ToString("dd MMM yyyy HH:mm tt") %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Width="15px">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnEdit" runat="server" class="fa fa-pencil-square-o fa-fw" CommandName="Ed" CausesValidation="false"
                                                                CommandArgument='<%# Eval("Id") %>' Style="font-size: 16px;"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Width="15px">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="btnDelete" runat="server" class="fa fa-trash-o fa-fw" CausesValidation="false"
                                                                CommandName="Del" Style="font-size: 16px;" OnClientClick="return confirm('Are You Sure?');" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
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
                </div>
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Accounts List
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <center>
                                    <asp:GridView ID="gvAccounts" runat="server" Width="100%" AutoGenerateColumns="false" class="table table-striped"
                                        GridLines="None" AllowPaging="false" CellPadding="0" CellSpacing="0" DataKeyNames="Id" ForeColor="#333333" OnRowCommand="gvAccounts_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL" ItemStyle-Width="15px">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Name" HeaderText="Account Name" />
                                            <asp:BoundField DataField="CustomerName" HeaderText="Customer Name" />
                                            <asp:BoundField DataField="OfficePhone" HeaderText="Office Phone" />
                                            <asp:BoundField DataField="EmployeeStrength" HeaderText="Employee Strength" />
                                            <asp:BoundField DataField="Industry" HeaderText="Industry" />
                                            <asp:BoundField DataField="CustomerTypeName" HeaderText="Customer Type" />
                                            <asp:TemplateField ItemStyle-Width="15px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnView" runat="server" ToolTip="View" class="fa fa-eye fa-fw" CommandName="View" CausesValidation="false"
                                                        CommandArgument='<%# Eval("Id") %>' Style="font-size: 16px;"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="15px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" ToolTip="Edit" class="fa fa-pencil-square-o fa-fw" CommandName="Ed" CausesValidation="false"
                                                        CommandArgument='<%# Eval("Id") %>' Style="font-size: 16px;"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="15px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnDelete" runat="server" ToolTip="Delete" class="fa fa-trash-o fa-fw" CausesValidation="false"
                                                        CommandName="Del" Style="font-size: 16px;" OnClientClick="return confirm('Are You Sure?');" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
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
            <script type="text/javascript">
                $(function () {
                    loadForm();
                });
                function loadForm() {
                    if ($('#hdnOpenForm').val() == "true") {
                        $('#hrefForm').click();
                        $('#hrefAssigned').click();
                    }
                }
                Sys.Application.add_load(loadForm);
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
