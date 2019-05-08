<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Opportunity.aspx.cs" Inherits="WebAppAegisCRM.Sales.Opportunity" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $(function () {
                prepareDateTimeControls();
            });
        });
        function prepareDateTimeControls() {
            $('#ContentPlaceHolder1_txtExpectedCloseDate').datetimepicker({ format: 'DD MMM YYYY' });
        }
    </script>
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
            <script type="text/javascript">
                Sys.Application.add_load(prepareDateTimeControls);
            </script>
            <div class="row">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Add/Edit Opportunity
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group has-error">
                                        Name
                                <asp:TextBox ID="txtName" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        Description
                                <asp:TextBox ID="txtDescription" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-lg-4">
                                    <div class="form-group">
                                        Expected Close Date
                                        <input type="text" id="txtExpectedCloseDate" runat="server" class="form-control" />
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        Likely Price
                                     <asp:TextBox ID="txtLikelyPrice" CssClass="form-control" runat="server" ></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        Best Price
                                      <asp:TextBox ID="txtBestPrice" CssClass="form-control" runat="server" ></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        Worst Price
                                      <asp:TextBox ID="txtWorstPrice" CssClass="form-control" runat="server" ></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        Commit Stage
                                <asp:DropDownList ID="ddlCommitStage" runat="server" CssClass="form-control">
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
                                    <div class="form-group">
                                        Source Name
                                      <asp:TextBox ID="txtSourceName" CssClass="form-control" runat="server" ></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        Campaign
                                <asp:DropDownList ID="ddlCampaign" runat="server" CssClass="form-control">
                                </asp:DropDownList>
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
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Opportunity List
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <center>
                                    <asp:GridView ID="gvOpportunity" runat="server" Width="100%" AutoGenerateColumns="false" class="table table-striped"
                                        GridLines="None" AllowPaging="false" CellPadding="0" CellSpacing="0" DataKeyNames="Id" ForeColor="#333333" OnRowCommand="gvOpportunity_RowCommand">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL" ItemStyle-Width="15px">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Name" HeaderText="Name" />
                                            <asp:BoundField DataField="BestPrice" HeaderText="Best Price" />
                                            <asp:BoundField DataField="CommitStage" HeaderText="Commit Stage" />
                                            <asp:BoundField DataField="ExpectedCloseDate" HeaderText="Expected Close Date" />
                                            <asp:TemplateField ItemStyle-Width="15px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnEdit" runat="server" class="fa fa-pencil-square-o fa-fw" CommandName="Ed" CausesValidation="false"
                                                        CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="15px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnDelete" runat="server" class="fa fa-trash-o fa-fw" CausesValidation="false"
                                                        CommandName="Del" OnClientClick="return confirm('Are You Sure?');" CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
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
