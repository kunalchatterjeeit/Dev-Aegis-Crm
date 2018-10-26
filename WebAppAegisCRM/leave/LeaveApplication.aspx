
<%@ Page Title="LEAVE APPLICATION" Language="C#" AutoEventWireup="true" MasterPageFile="~/Main.Master" 
    CodeBehind="LeaveApplication.aspx.cs" Inherits="WebAppAegisCRM.leave.LeaveApplication" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>

           <div class="panel panel-default">
               <div class="row">
                    <ul><h3>Request For Leave</h3></ul>
               </div>
                <div class="col-lg-6">
                    <div class="form-group">
                         Name:
                            <asp:TextBox ID="txtName" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                 </div>
                <div class="col-lg-6">
                     <div class="form-group">
                         Employe No.:
                         <asp:TextBox ID="TxtEmployeNo" CssClass="form-control" runat="server"></asp:TextBox>
                     </div>
                </div>
                <div class="col-lg-6">
                     <div class="form-group">
                         Phone:
                         <asp:TextBox ID="TxtPhone" CssClass="form-control" runat="server"></asp:TextBox>
                      </div>
                 </div>
                <div class="col-lg-6">
                     <div class="form-group">
                         email:
                         <asp:TextBox ID="TextBox2" CssClass="form-control" runat="server"></asp:TextBox>
                      </div>
                 </div>
            
        
           <div class="panel panel-default">
                <div class="row">
                    <ul><h4>Details of Leave</h4></ul>
                </div>
                 <div class="col-lg-6">
                     <div class="form-group">
                         Leave Start:
                         <asp:TextBox ID="TextBox1" CssClass="form-control" runat="server"></asp:TextBox>
                     </div>
                 </div>
                 <div class="col-lg-6">
                     <div class="form-group">
                         Leave End:
                         <asp:TextBox ID="TextBox3" CssClass="form-control" runat="server"></asp:TextBox>
                      </div>
                 </div>
                      <div class="col-lg-6">
                        <div class="form-group">
                            <b>Leave Type:</b><br />
                            <asp:RadioButton ID="RadioButtonCl" runat="server" Text="CL" /><br />
                            <asp:RadioButton ID="RadioButtonPl" runat="server" Text="PL" /><br />
                            <asp:RadioButton ID="RadioButtonLwp" runat="server" Text="LWP" />
                        </div>
                     </div>
                 <div class="col-lg-6">
                    <div class="form-group">
                         Comments:
                            <asp:TextBox ID="TxtComments" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>
                 </div>
                    </div>
                
        </div>
</asp:Content>