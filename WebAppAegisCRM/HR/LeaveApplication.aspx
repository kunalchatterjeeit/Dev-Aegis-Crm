<%@ Page Title="EMPLOYEE" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="True"
    CodeBehind="Employee.aspx.cs" Inherits="WebAppAegisCRM.Employee.Employee" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
    <br />
    <div class="row">
        <div class="col-lg-12">
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
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                Reason:
                                <asp:TextBox ID="txtReason" CssClass="form-control" runat="server"></asp:TextBox>
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
    </div>
</asp:Content>
