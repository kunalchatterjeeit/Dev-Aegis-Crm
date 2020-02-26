<%@ Page Title="PRINT QUOTE" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="QuoteBeforePrint.aspx.cs" Inherits="WebAppAegisCRM.Sales.QuoteBeforePrint" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    <%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
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
                            Quote Stages
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <center>
                                    <asp:GridView ID="gvQuotePrint" runat="server" Width="100%" AutoGenerateColumns="false" class="table table-striped"
                                        GridLines="None" AllowPaging="false" CellPadding="0" CellSpacing="0" DataKeyNames="QuoteId" ForeColor="#333333">
                                        <Columns>
                                            <asp:TemplateField HeaderText="SL" ItemStyle-Width="15px">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="StageName" HeaderText="Quote Stage" />
                                            <asp:BoundField DataField="CreatedBy" HeaderText="Created By" />
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    Created Date
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <%# (Eval("CreatedDate")==null)?"":Convert.ToDateTime(Eval("CreatedDate").ToString()).ToString("dd MMM yyyy") %>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                           
                                             <asp:TemplateField ItemStyle-Width="15px">
                                                <ItemTemplate>
                                                    <a href='PrintQuote.aspx?StageId=<%# Eval("Id") %>&showStamp=false' target="_blank" class="fa fa-print" style="font-size: 16px;" title="Print without stamp"></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField ItemStyle-Width="15px">
                                                <ItemTemplate>
                                                    <a href='PrintQuote.aspx?StageId=<%# Eval("Id") %>&showStamp=true' target="_blank" class="fa fa-globe" style="font-size: 16px;" title="Print with stamp"></a>
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
