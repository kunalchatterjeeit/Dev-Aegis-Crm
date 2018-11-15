<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="ViewEmployeeDetails.aspx.cs" Inherits="WebAppAegisCRM.Employee.ViewEmoployeeDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="padding:30px">
    <table style="padding:10px;width:80%" border="1">
                <tr>
            <td style="text-align:center" colspan="4">
                Employee Details
            </td>
        </tr>
         <tr>
            <td>

                Employee Name:</td>
            <td>

                <asp:Label ID="lblEmployeeName" runat="server" Text="Label"></asp:Label>

            </td>
            <td>

                Image:</td>
            <td>

                <asp:Image ID="Image1" runat="server" />

            </td>
        </tr>
                <tr>
            <td>

                Gender:</td>
            <td>

                <asp:Label ID="lblgender" runat="server" Text="Label"></asp:Label>

            </td>
            <td>

                DOB:</td>
            <td>

                <asp:Label ID="lbldob" runat="server" Text="Label"></asp:Label>

            </td>
        </tr>
                <tr>
            <td>

                Marital Status</td>
            <td>

                <asp:Label ID="lblmaratorialStatus" runat="server" Text="Label"></asp:Label>

            </td>
            <td>

                Religion</td>
            <td>

                <asp:Label ID="lblreligion" runat="server" Text="Label"></asp:Label>

            </td>
        </tr>
                <tr>
            <td>

                Blood Group:</td>
            <td>

                <asp:Label ID="lblBloodGroup" runat="server" Text="Label"></asp:Label>

            </td>
            <td>

                Personal MobileNo</td>
            <td>

                <asp:Label ID="lblPersonalMobileNumber" runat="server" Text="Label"></asp:Label>

            </td>
        </tr>
                <tr>
            <td>

                Office MobileNo</td>
            <td>

                <asp:Label ID="lblofficialNumber" runat="server" Text="Label"></asp:Label>

            </td>
            <td>

                Personal EmailId</td>
            <td>

                <asp:Label ID="lblpersonalEmailId" runat="server" Text="Label"></asp:Label>

            </td>
        </tr>
                <tr>
            <td>

                OfficeEmailId</td>
            <td>

                <asp:Label ID="lblOfficeEmailId" runat="server" Text="Label"></asp:Label>

            </td>
            <td>

                ReferenceEmployee</td>
            <td>

                <asp:Label ID="lblReferenceEmployee" runat="server" Text="Label"></asp:Label>

            </td>
        </tr>
                <tr>
            <td>

                pAddress</td>
            <td>

                <asp:Label ID="lblpAddress" runat="server" Text="Label"></asp:Label>

            </td>
            <td>

                CityName</td>
            <td>

                <asp:Label ID="lblCityName" runat="server" Text="Label"></asp:Label>

            </td>
        </tr>
                <tr>
            <td>

                pPIN</td>
            <td>

                <asp:Label ID="lblPpin" runat="server" Text="Label"></asp:Label>

            </td>
            <td>

                tAddress</td>
            <td>

                <asp:Label ID="lbltAddress" runat="server" Text="Label"></asp:Label>

            </td>
        </tr>
                <tr>
            <td>

                tCityId_FK</td>
            <td>

                <asp:Label ID="lbltCityId_FK" runat="server" Text="Label"></asp:Label>

            </td>
            <td>

                tPINMasterId_FK</td>
            <td>

                <asp:Label ID="lbltPINMasterId_FK" runat="server" Text="Label"></asp:Label>

            </td>
        </tr>
                <tr>
            <td>

                DesignationName</td>
            <td>

                <asp:Label ID="lblDesignationName" runat="server" Text="Label"></asp:Label>

            </td>
            <td>

                DOJ</td>
            <td>

                <asp:Label ID="lblDOJ" runat="server" Text="Label"></asp:Label>

            </td>
        </tr>
                <tr>
            <td>

                EmployeeCode</td>
            <td>

                <asp:Label ID="lblEmployeeCode" runat="server" Text="Label"></asp:Label>

            </td>
            <td>

                PANNo</td>
            <td>

                <asp:Label ID="lblPANNo" runat="server" Text="Label"></asp:Label>

            </td>
        </tr>
                <tr>
            <td>

                IsSuspended</td>
            <td>

                <asp:Label ID="lblIsSuspended" runat="server" Text="Label"></asp:Label>

            </td>
            <td>

                IsTerminated</td>
            <td>

                <asp:Label ID="lblIsTerminated" runat="server" Text="Label"></asp:Label>

            </td>
        </tr>
        
    </table>
    </div>
    </form>
</body>
</html>
