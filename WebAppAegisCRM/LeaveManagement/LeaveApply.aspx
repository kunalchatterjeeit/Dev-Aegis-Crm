<%@ Page Title="Leave Apply" Language="C#" AutoEventWireup="true" CodeBehind="LeaveApply.aspx.cs" MasterPageFile="~/Main.Master"
    Inherits="WebAppAegisCRM.LeaveManagement.LeaveApply" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Apply for Leave
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-3">
                            <div class="form-group has-error">
                                Leave Type
                                 <br />
                                <asp:DropDownList ID="ddlLeaveType" CssClass="form-control" runat="server"></asp:DropDownList>
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


                        <div class="col-lg-12" id="calender">
                            <asp:Calendar ID="Calendar1" runat="server" BackColor="White" BorderColor="Black" BorderStyle="Solid"
                                CellSpacing="1" Font-Names="Verdana"
                                MultiSelectedDates="true" OnDayRender="Calendar1_DayRender"
                                Font-Size="9pt" ForeColor="Black" NextPrevFormat="ShortMonth"
                                OnSelectionChanged="Calendar1_SelectionChanged" SelectionMode="Day" Style="width: 100%; height: 45vh">
                                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" />
                                <DayStyle BackColor="#CCCCCC" />
                                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                                <OtherMonthDayStyle ForeColor="#999999" />
                                <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                                <TitleStyle BackColor="#333399" BorderStyle="Solid" Font-Bold="True" Font-Size="12pt" ForeColor="White" Height="12pt" />
                                <TodayDayStyle BackColor="#999999" ForeColor="White" />
                            </asp:Calendar>
                        </div>

                        <div class="col-lg-12">
                            <div class="form-group has-error">
                                Reason
                                <br />
                                <textarea id="txtaareaReason" class="form-control" cols="20" rows="2"></textarea>
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

</asp:Content>
