<%@ Page Title="SERVICE BOOK" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true"
    CodeBehind="ServiceBook.aspx.cs" Inherits="WebAppAegisCRM.Service.ServiceBook" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function ShowSignaturePad() {
            document.getElementById("signature-pad").style.display = 'block';
            resizeCanvas();
            //scroll to sign pad
            $('html, body').animate({
                scrollTop: $("#signature-pad").offset().top
            }, 2000);
        }

        function OpenWindow(page) {
            window.open(page, 'mypopuptitle', 'width=1000,height=600');
        }

        //scroll to success button
        $(function () {
            $("#btnProceed").click(function () {
                $('html, body').animate({
                    scrollTop: $("#buttonSection").offset().top
                }, 2000);
            });
        });
    </script>
    <script type="text/javascript">
        function Validate() {
            if (document.getElementById("<%=ddlCallType.ClientID%>").selectedIndex == 0) {
                alert("Please select call type");
                return false;
            }
            else
                return true;
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
            <div class="row" id="divCallType" runat="server">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Service Book Entry
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-4">
                                    <div class="form-group has-error">
                                        Call Type :
                                        <asp:DropDownList ID="ddlCallType" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="--SELECT--" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Toner Request" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="Docket" Value="2"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <div class="form-group">
                                        Customer :
                                        <asp:DropDownList ID="ddlCustomer" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-4">
                                    <br />
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-outline btn-success"
                                        OnClientClick="return Validate();" OnClick="btnSearch_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row" id="divDocket" runat="server" visible="false">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Docket List
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        Docket No :
                                        <asp:TextBox ID="txtDocketNo" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <%--<div class="col-lg-2">
                                    <div class="form-group">
                                        Model :
                                        <asp:DropDownList ID="ddlDocketProduct" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>--%>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        From Docket Date :
                                        <asp:TextBox ID="txtFromDocketDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDocketDate"
                                            Format="dd MMM yyyy" Enabled="True">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        To Docket Date :
                                        <asp:TextBox ID="txtToDocketDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDocketDate"
                                            Format="dd MMM yyyy" Enabled="True">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        Status :
                                        <asp:DropDownList ID="ddlDocketCallStatus" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        Type :
                                    <asp:DropDownList ID="ddlDocketType" runat="server" CssClass="form-control">
                                        <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                        <asp:ListItem Text="PM" Value="PM"></asp:ListItem>
                                        <asp:ListItem Text="CM" Value="CM"></asp:ListItem>
                                        <asp:ListItem Text="INSTALLATION" Value="INSTALLATION"></asp:ListItem>
                                        <asp:ListItem Text="OTHERS" Value="OTHERS"></asp:ListItem>
                                    </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <br />
                                    <asp:Button ID="btnDocketSearch" runat="server" Text="Search Docket" CssClass="btn btn-outline btn-success" OnClick="btnDocketSearch_Click" />
                                </div>
                                <div class="clearfix">
                                </div>
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <div style="height: 15vh; overflow: scroll">
                                            <asp:GridView ID="gvDocket" DataKeyNames="DocketId, ProductMasterId, AssignEngineer, CustomerPurchaseId, CallStatusId" runat="server"
                                                AutoGenerateColumns="False" Width="100%" CellPadding="4"
                                                ForeColor="#333333" class="table table-striped" GridLines="None" Style="text-align: left">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkDocket" runat="server" OnCheckedChanged="chkDocket_CheckedChanged"
                                                                AutoPostBack="true" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            SN.
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Docket No" DataField="DocketNo" />
                                                    <asp:BoundField HeaderText="Name" DataField="CustomerName" />
                                                    <asp:BoundField HeaderText="Date" DataField="DocketDate" />
                                                    <asp:BoundField HeaderText="Time" DataField="DocketTime" />
                                                    <asp:BoundField HeaderText="Machine ID" DataField="MachineId" />
                                                    <asp:BoundField HeaderText="Model" DataField="ProductName" />
                                                    <asp:BoundField HeaderText="Problem" DataField="Problem" ItemStyle-Width="20%" />
                                                    <asp:BoundField HeaderText="Status" DataField="CallStatus" />
                                                </Columns>
                                                <FooterStyle BackColor="#5bb0de" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#379ed6" Font-Bold="True" ForeColor="White" />
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
            </div>
            <div class="row" id="divDocketClosing" runat="server" visible="false">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Docket Closing
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="form-group">
                                        Problem Reported :
                                        <asp:Label ID="lblProblem" runat="server" CssClass="form-control" Style="height: 50px"></asp:Label>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="clearfix"></div>
                                <div class="col-lg-3">
                                    <div class="form-group has-error">
                                        In Time :
                                        <div class="form-control">
                                            <asp:TextBox ID="txtInDate" runat="server" Style="border: none; background: #fff; width: 55px" disabled></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtInDate"
                                                Format="dd MMM yyyy" Enabled="True">
                                            </asp:CalendarExtender>
                                            <asp:DropDownList ID="ddlInTimeHH" runat="server" Enabled="false">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlInTimeMM" runat="server" Enabled="false">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlInTimeTT" runat="server" Enabled="false">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group has-error">
                                        Out Time :
                                        <div class="form-control">
                                            <asp:TextBox ID="txtOutDate" runat="server" Style="border: none; background: #fff; width: 55px" disabled></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtOutDate"
                                                Format="dd MMM yyyy" Enabled="True">
                                            </asp:CalendarExtender>
                                            <asp:DropDownList ID="ddlOutTimeHH" runat="server">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlOutTimeMM" runat="server">
                                            </asp:DropDownList>
                                            <asp:DropDownList ID="ddlOutTimeTT" runat="server">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group has-error">
                                        Current Call Status :
                                        <asp:DropDownList ID="ddlCurrentCallStatusDocket" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <br />
                                        <%--<asp:Button ID="Button1" runat="server" Text="Call Transfer" OnClientClick="OpenWindow('CallTransfer.aspx')" CssClass="btn btn-outline btn-info pull-left" Style="width: 100%" />--%>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="form-group has-error">
                                        A3 BW Current Meter Reading:                                        
                                        <asp:TextBox ID="txtA3BWCurrentMeterReading" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="txtA3BWCurrentMeterReading_FilteredTextBoxExtender" ValidChars="0123456789"
                                            runat="server" TargetControlID="txtA3BWCurrentMeterReading">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group has-error">
                                        A4 BW Current Meter Reading:                                        
                                        <asp:TextBox ID="txtA4BWCurrentMeterReading" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" ValidChars="0123456789"
                                            runat="server" TargetControlID="txtA4BWCurrentMeterReading">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group has-error">
                                        A3 CL Current Meter Reading:                                        
                                        <asp:TextBox ID="txtA3CLCurrentMeterReading" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" ValidChars="0123456789"
                                            runat="server" TargetControlID="txtA3CLCurrentMeterReading">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group has-error">
                                        A4 CL Current Meter Reading:                                        
                                        <asp:TextBox ID="txtA4CLCurrentMeterReading" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" ValidChars="0123456789"
                                            runat="server" TargetControlID="txtA4CLCurrentMeterReading">
                                        </asp:FilteredTextBoxExtender>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-3">
                                    <div class="form-group has-error">
                                        Assigned Service Engineer :
                                        <asp:DropDownList ID="ddlServiceEngineer" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-3">
                                    <div class="form-group">
                                        <br />
                                        <asp:Button ID="btnCallTransfer" runat="server" Text="Call Transfer" OnClientClick="OpenWindow('CallTransfer.aspx')" CssClass="btn btn-outline btn-info pull-left" Style="width: 100%" />
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group has-error">
                                        Problem Observed :
                                        <asp:DropDownList ID="ddlProblemObserved" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="form-group has-error">
                                        Diagnosis :
                                        <asp:DropDownList ID="ddlDocketDiagnosis" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group has-error">
                                        Action Taken :
                                        <asp:DropDownList ID="ddlDocketActionTaken" runat="server" CssClass="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        Remarks :
                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        Customer Feedback :
                                        <asp:TextBox ID="txtCustomerFeedback" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        Select Associated Engineers:
                                        <div style="height: 30vh; overflow: scroll">
                                            <asp:GridView ID="gvAssociatedEngineers" DataKeyNames="EmployeeMasterId" runat="server"
                                                AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                                                class="table table-striped" GridLines="None" Style="text-align: left" OnRowDataBound="gvAssociatedEngineers_RowDataBound">
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkEngineer" runat="server"></asp:CheckBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            SN.
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex+1 %>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField HeaderText="Name" DataField="EmployeeName" />
                                                    <asp:BoundField HeaderText="Mobile" DataField="PersonalMobileNo" />
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            In Time
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtAssociatedInDate" runat="server" Style="border: none; background: #fff; width: 60px" disabled></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtAssociatedInDate"
                                                                Format="dd MMM yyyy" Enabled="True">
                                                            </asp:CalendarExtender>
                                                            <asp:DropDownList ID="ddlAssociatedInTimeHH" runat="server">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlAssociatedInTimeMM" runat="server">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlAssociatedInTimeTT" runat="server">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Out Time
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtAssociatedOutDate" runat="server" Style="border: none; background: #fff; width: 60px" disabled></asp:TextBox>
                                                            <asp:CalendarExtender ID="CalendarExtender6" runat="server" TargetControlID="txtAssociatedOutDate"
                                                                Format="dd MMM yyyy" Enabled="True">
                                                            </asp:CalendarExtender>
                                                            <asp:DropDownList ID="ddlAssociatedOutTimeHH" runat="server">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlAssociatedOutTimeMM" runat="server">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="ddlAssociatedOutTimeTT" runat="server">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            Remarks
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtAssociatedRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#5bb0de" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#379ed6" Font-Bold="True" ForeColor="White" />
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
                            <%--<div id="signature-pad" class="m-signature-pad" style="display: none">
                                    <div class="m-signature-pad--body">
                                        <canvas></canvas>
                                    </div>
                                    <div class="m-signature-pad--footer">
                                        <div class="description">Sign above</div>
                                        <button type="button" class="button clear btn btn-outline btn-success extra-margin pull-right" data-action="clear">Erase</button>
                                        <button id="btnProceed" type="button" class="button save btn btn-outline btn-success extra-margin pull-right" data-action="save">Proceed</button>
                                    </div>
                                </div>
                                <input type="hidden" id="signature" runat="server" />--%>
                            <div class="clearfix">
                            </div>
                            <div class="col-lg-12" id="buttonSection">
                                <br />
                                <uc3:Message ID="MessageDocket" runat="server" />
                                <asp:Button ID="btnSpareRequisition" runat="server" Text="Spare Requisition" OnClientClick="OpenWindow('SpareRequisition.aspx')" class="btn btn-outline btn-warning extra-margin pull-right" Style="width: 100%" />
                                <asp:Button ID="btnServiceChallan" runat="server" Text="Proceed to Challan & Signature" OnClientClick="OpenWindow('ServiceChallan.aspx')" class="btn btn-outline btn-success extra-margin pull-right" Style="width: 100%" />
                                <asp:Button ID="btnDocketClose" runat="server" Text="Submit" class="btn btn-outline btn-success extra-margin pull-right" OnClick="btnDocketClose_Click" Style="width: 100%" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            </div>
            <div class="row" id="divDocketClosingHistory" runat="server" visible="false">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Docket Closing History
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvDocketClosingHistory" DataKeyNames="ServiceHistoryId" runat="server"
                                            AutoGenerateColumns="False" Width="100%" CellPadding="4"
                                            ForeColor="#333333" class="table table-striped" GridLines="None" Style="text-align: left">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        SN.
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Service Engineer" DataField="EmployeeName" />
                                                <asp:BoundField HeaderText="In Time" DataField="InTime" DataFormatString="{0: dd MMM yyyy hh:mm tt}" />
                                                <asp:BoundField HeaderText="Out Time" DataField="OutTime" DataFormatString="{0: dd MMM yyyy hh:mm tt}" />
                                                <asp:BoundField HeaderText="Action Taken" DataField="ActionTaken" />
                                                <asp:BoundField HeaderText="Problem Observed" DataField="ProblemObserved" />
                                            </Columns>
                                            <FooterStyle BackColor="#5bb0de" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#379ed6" Font-Bold="True" ForeColor="White" />
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
            <div class="row" id="divTonnerRequest" runat="server" visible="false">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Toner Request List
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        Toner Request No :
                                        <asp:TextBox ID="txtTonnerRequestNo" CssClass="form-control" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        Model :
                                        <asp:DropDownList ID="ddlTonnerRequestProduct" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        From Request Date :
                                        <asp:TextBox ID="txtFromTonnerRequestDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtFromTonnerRequestDate"
                                            Format="dd MMM yyyy" Enabled="True">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        To Request Date :
                                        <asp:TextBox ID="txtToTonnerRequestDate" runat="server" CssClass="form-control"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtToTonnerRequestDate"
                                            Format="dd MMM yyyy" Enabled="True">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        Status :
                                        <asp:DropDownList ID="ddlTonnerRequestCallStatus" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <br />
                                    <asp:Button ID="btnTonnerRequestSearch" runat="server" Text="Search Toner" CssClass="btn btn-outline btn-success" OnClick="btnTonnerRequestSearch_Click" />
                                </div>
                                <div class="clearfix">
                                </div>
                                <div class="col-lg-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvTonnerRequest" DataKeyNames="TonnerRequestId,CustomerPurchaseId,ProductMasterId" runat="server"
                                            AutoGenerateColumns="False" Width="100%" CellPadding="4" AllowPaging="true" AllowCustomPaging="true" PageSize="5"
                                            ForeColor="#333333" class="table table-striped" GridLines="None" Style="text-align: left"
                                            OnPageIndexChanging="gvTonnerRequest_PageIndexChanging">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkTonnerRequest" runat="server" OnCheckedChanged="chkTonnerRequest_CheckedChanged"
                                                            AutoPostBack="true" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        SN.
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# (gvTonnerRequest.PageIndex * gvTonnerRequest.PageSize) + (Container.DataItemIndex + 1) %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Req. No" DataField="RequestNo" />
                                                <asp:BoundField HeaderText="Date" DataField="RequestDate" />
                                                <asp:BoundField HeaderText="Time" DataField="RequestTime" />
                                                <asp:BoundField HeaderText="Machine Id" DataField="MachineId" />
                                                <asp:BoundField HeaderText="Model" DataField="ProductName" />
                                                <asp:BoundField HeaderText="A3 B/W Meter" DataField="A3BWMeterReading" />
                                                <asp:BoundField HeaderText="A4 B/W Meter" DataField="A4BWMeterReading" />
                                                <asp:BoundField HeaderText="A3 CL Meter" DataField="A3CLMeterReading" />
                                                <asp:BoundField HeaderText="A4 CL Meter" DataField="A4CLMeterReading" />
                                                <asp:BoundField HeaderText="Status" DataField="CallStatus" />
                                                <asp:BoundField HeaderText="CE" DataField="IsCustomerEntry" ItemStyle-HorizontalAlign="Center" />
                                            </Columns>
                                            <FooterStyle BackColor="#5bb0de" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#379ed6" Font-Bold="True" ForeColor="White" />
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
            <div class="row" id="divTonnerRequestApproval" runat="server" visible="false">
                <div class="col-lg-12">
                    <div class="panel panel-default">
                        <div class="panel-heading">
                            Toner Request Approval
                        </div>
                        <div class="panel-body">
                            <div class="row">
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        A3 BW Last M. Reading :
                                        <asp:Label ID="lblA3BWLastMeterReading" runat="server" Text="0" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        A4 BW Last M. Reading :
                                        <asp:Label ID="lblA4BWLastMeterReading" runat="server" Text="0" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        A3 CL Last M Reading :
                                        <asp:Label ID="lblA3CLLastMeterReading" runat="server" Text="0" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        A4 CL Last M. Reading :
                                        <asp:Label ID="lblA4CLLastMeterReading" runat="server" Text="0" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group has-error">
                                        Toner delivered by :
                                        <asp:DropDownList ID="ddlTonerServiceEngineer" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="clearfix"></div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        A3 BW Current M. Reading :
                                        <asp:TextBox ID="lblA3BWCurrentMeterReading" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        A4 BW Current M. Reading :
                                        <asp:TextBox ID="lblA4BWCurrentMeterReading" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        A3 CL Current M. Reading :
                                        <asp:TextBox ID="lblA3CLCurrentMeterReading" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group">
                                        A4 CL Current M. Reading :
                                        <asp:TextBox ID="lblA4CLCurrentMeterReading" runat="server" CssClass="form-control" Text="0"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-2">
                                    <div class="form-group has-error">
                                        Current Call Status :
                                        <asp:DropDownList ID="ddlCurrentTonnerRequestCallStatus" CssClass="form-control" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="clearfix">
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        Diagnosis :
                                        <asp:TextBox ID="txtDiagnosis" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        Action Taken :
                                        <asp:TextBox ID="txtActionTaken" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="clearfix">
                                </div>
                                <%--<div class="col-lg-12">
                                    <div class="table-responsive">
                                        Toner Description:
                                        <asp:GridView ID="gvTonnerList" DataKeyNames="SpareId,ApprovalStatus" runat="server"
                                            AutoGenerateColumns="False" Width="100%" CellPadding="4" ForeColor="#333333"
                                            class="table table-striped" GridLines="None" Style="text-align: left" OnRowDataBound="gvTonnerList_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkToner" runat="server" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        SN.
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%# Container.DataItemIndex+1 %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Toner" DataField="SpareName" />
                                                <asp:BoundField HeaderText="Yield" DataField="Yield" />
                                                <asp:TemplateField HeaderText="Approval Status">
                                                    <ItemTemplate>
                                                        <%# (Eval("ApprovalStatus").ToString()=="2")?"Rejected":"Approved" %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Comment" DataField="Comment" />
                                            </Columns>
                                            <FooterStyle BackColor="#5bb0de" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle BackColor="#379ed6" Font-Bold="True" ForeColor="White" />
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
                                </div>--%>
                                <div class="col-lg-12">
                                    <br />
                                    <uc3:Message ID="MessageTonner" runat="server" />
                                    <br />
                                    <asp:Button ID="btnTonerChallan" runat="server" Text="Proceed to Challan" OnClientClick="OpenWindow('ServiceChallan.aspx')" class="btn btn-outline btn-success extra-margin pull-right" Style="width: 100%" />
                                    <asp:Button ID="btnSubmitTonner" runat="server" Text="Submit" CssClass="btn btn-outline btn-success"
                                        OnClick="btnSubmitTonner_Click" Style="width: 100%" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
