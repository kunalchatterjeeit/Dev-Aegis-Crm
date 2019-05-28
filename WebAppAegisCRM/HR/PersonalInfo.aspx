<%@ Page Title="PERSONAL INFO" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="PersonalInfo.aspx.cs" Inherits="WebAppAegisCRM.HR.PersonalInfo" %>

<%@ Register Src="../UserControl/Message.ascx" TagName="Message" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(window).load(function () {
            $('#ContentPlaceHolder1_Image1').each(function () {
                if (!this.complete || typeof this.naturalWidth == "undefined" || this.naturalWidth == 0) {
                    // image was broken, replace with your new image
                    if (this.getAttribute("sex") == "Male")
                        this.src = '/Images/male-avatar.png';
                    else if (this.getAttribute("sex") == "Female")
                        this.src = '/Images/female-avatar.jpg';
                    else
                        this.src = '/Images/male-avatar.png';
                }
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <div class="row">
        <div class="col-lg-12">
            <uc3:Message ID="MessageBox" runat="server" />
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12">
            <div class="panel panel-default">
                <div class="panel-heading">
                    Personal details
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-lg-4">
                            <div class="form-group">
                                <asp:Image ID="Image1" runat="server" Width="200px" />
                                <div class="clearfix"></div>
                                <span class="red">Max Size 250KB. JPG,PNG formats only</span>
                                <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control"/>
                                <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" CssClass="btn btn-outline btn-success"/>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                Name:
                                        <asp:Label ID="lblName" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                Date of birth:
                                        <asp:Label ID="lblDateOfBirth" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                Mobile:
                                        <asp:Label ID="lblMobile" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                Personal Email:
                                        <asp:Label ID="lblPersonalEmail" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                Official Email:
                                        <asp:Label ID="lblOfficialEmail" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                Address:
                                        <asp:Label ID="lblAddress" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                PAN:
                                        <asp:Label ID="lblPan" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                Date of joining:
                                        <asp:Label ID="lblDateOfJoining" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                Designation:
                                        <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div class="col-lg-4">
                            <div class="form-group">
                                Reporting:
                                        <asp:Label ID="lblReporting" runat="server"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
