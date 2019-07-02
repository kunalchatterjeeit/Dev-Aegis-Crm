<%@ Page Title="LEAVE APPLY" Language="C#" AutoEventWireup="true" CodeBehind="LeaveApply.aspx.cs" MasterPageFile="~/Main.Master"
    Inherits="WebAppAegisCRM.LeaveManagement.LeaveApply" %>

<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="scriptcontent" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function HalfDayList(obj, date) {
            debugger;
            var halfDayList = $("#ContentPlaceHolder1_hdnHalfDayList").val();

            if ($(obj).find('input').is(':checked')) {
                if (halfDayList == null || halfDayList == '') {
                    halfDayList = date;
                }
                else {
                    halfDayList += ',' + date;
                }
            }
            else {
                if (halfDayList == null || halfDayList == '') {
                    halfDayList = '';
                }
                else {
                    halfDayList = halfDayList.replace(date, '');
                    halfDayList = halfDayList.replace(',,', ',');
                }
            }
            $("#ContentPlaceHolder1_hdnHalfDayList").val(halfDayList);
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                <asp:HiddenField ID="hdnHalfDayList" runat="server" />
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Available Leaves
                        </div>
                        <div class="panel-body">
                            <div class="table-responsive">
                                <center>
                                    <asp:GridView ID="gvLeaveAvailableList" runat="server"
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
                                            <asp:BoundField DataField="LeaveTypeName" HeaderText="Leave Type" />
                                            <asp:BoundField DataField="Amount" HeaderText="Available" />
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
                                </center>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Apply for Leave
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                 <div class="col-lg-12">
                                        <uc3:Message ID="Message" runat="server" />
                                
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group has-error">
                                        Leave Type
                                 <br />
                                        <asp:DropDownList ID="ddlLeaveType" CssClass="form-control" runat="server" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group has-error">
                                        From Date
                                <br />
                                        <asp:Label ID="lbFromDate" CssClass="form-control" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group has-error">
                                        To Date
                                <br />
                                        <asp:Label ID="lbToDate" CssClass="form-control" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group has-error">
                                        Total Count
                                <br />
                                        <asp:Label ID="lbTotalCount" CssClass="form-control" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                               
                                <div class="col-lg-12" id="calender">                                    
                                <span class="red">Note: Select all required dates from calendar then select half day checkbox</span>
                                    <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid"
                                        CellSpacing="1" Font-Names="Verdana"
                                        MultiSelectedDates="true" OnDayRender="Calendar1_DayRender"
                                        Font-Size="9pt" ForeColor="Black" NextPrevFormat="ShortMonth"
                                        OnSelectionChanged="Calendar1_SelectionChanged" SelectionMode="Day" Style="width: 100%; height: 45vh">
                                        <DayHeaderStyle Font-Bold="True" Font-Size="12pt" ForeColor="#333333" BackColor="#83ad29" />
                                        <DayStyle BackColor="#CCCCCC" Font-Size="X-Large" />
                                        <NextPrevStyle Font-Bold="True" Font-Size="12pt" ForeColor="White" Font-Italic="true" />
                                        <OtherMonthDayStyle ForeColor="#999999" />
                                        <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                        <TitleStyle BackColor="#5BC0DE" Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="36" />
                                        <TodayDayStyle BackColor="#999999" ForeColor="White" />
                                        <DayStyle
                                            BackColor="#ffce93"
                                            BorderColor="Orange"
                                            BorderWidth="1"
                                            Font-Bold="true"
                                            Font-Italic="true" Font-Size="XX-Large" />
                                    </asp:Calendar>
                                </div>

                                <div class="col-lg-12">
                                    <div class="form-group has-error">
                                        Reason
                                         <br />
                                        <asp:TextBox ID="txtReason" runat="server" class="form-control" cols="20" Rows="2"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        Attachment
                                         <br />
                                        <asp:FileUpload ID="fileUploadAttachment" runat="server" />
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnSave" runat="server" Text="Save" class="btn btn-outline btn-success" OnClick="btnSave_Click" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" class="btn btn-outline btn-warning" OnClick="btnCancel_Click" />
                                    </div>
                                </div>
                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
