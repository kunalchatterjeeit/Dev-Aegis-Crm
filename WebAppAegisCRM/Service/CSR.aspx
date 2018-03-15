<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CSR.aspx.cs" Inherits="WebAppAegisCRM.Service.CSR" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>CSR</title>
    <style>
        @page {
            size: A4;
            margin: 0;
        }

        @media print {
            .page {
                margin: 0;
                border: initial;
                border-radius: initial;
                width: initial;
                min-height: initial;
                box-shadow: initial;
                background: initial;
                page-break-after: always;
            }
        }

        body {
            font-family: Cambria, Georgia, serif;
            line-height: 12px;
            font-size: 12px;
        }

        header {
            text-align: center;
        }

        table {
            width: 100%;
            margin: 1px;
            line-height: 18px;
            vertical-align: middle;
        }

            table tbody th {
                text-align: left;
            }

        .leftimg {
            text-align: left !important;
        }

        .rightimg {
            text-align: right !important;
        }
    </style>
    <script type="text/javascript">
        function Print()
        {
            window.print();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server" onclick="Print()">
        <div class="page" title="Click here to print/pdf">
            <header>
                <table cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr>
                            <td style="width: 20%">
                                <img class="leftimg" src="http://aegiscrm.in/images/Aegis_CRM_Logo.png" width="150" />
                            </td>
                            <td>
                                <h3>CUSTOMER SERVICE REPORT</h3>
                                <h2>Aegis Solutions</h2>
                                <h5>232, Jodhpur Gardens, Opp - South City Mall, Kolkata 45<br />
                                    Contact No - 8335810009, Email - info@aegissolutions.in</h5>
                            </td>
                            <td style="width: 20%">
                                <img class="rightimg" src="http://www.aegissolutions.in/Contents/images/logo.png" width="200" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </header>
            <section>
                <table cellspacing="0" cellpadding="0" border="1">
                    <thead>
                        <tr>
                            <th>CUSTOMER ID -
                                    <asp:Label ID="lblCustomerID" runat="server" Text=""></asp:Label>
                            </th>
                            <th>DOCKET NO -
                                    <asp:Label ID="lblDocketNo" runat="server" Text=""></asp:Label>
                            </th>
                            <th>IN DATE
                            </th>
                            <th>IN TIME
                            </th>
                            <th>OUT DATE
                            </th>
                            <th>OUT TIME
                            </th>
                            <th>STATUS
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td id="tdRepeatCallCount" runat="server" colspan="2">CUSTOMER NAME & ADDRESS:
                                <asp:Label ID="lblCustomerNameAddress" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <asp:Repeater ID="rptrRepeatCall" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("InDate") %>
                                    </td>
                                    <td><%# Eval("InTime") %>
                                    </td>
                                    <td><%# Eval("OutDate") %>
                                    </td>
                                    <td><%# Eval("OutTime") %>
                                    </td>
                                    <td><%# Eval("ProblemStatus") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </section>
            <section>
                <table cellspacing="0" cellpadding="0" border="1">
                    <thead>
                        <tr>
                            <th></th>
                            <th>METER READING
                            </th>
                            <th>MACHINE MODEL
                            </th>
                            <th>
                                <asp:Label ID="lblMachineModel" runat="server" Text="1R2320L"></asp:Label>
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <th>A3 BW
                            </th>
                            <td>
                                <asp:Label ID="lblA3BW" runat="server" Text="388459"></asp:Label>
                            </td>
                            <th>MACHINE ID
                            </th>
                            <td>
                                <asp:Label ID="lblMachineId" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th>A4 BW
                            </th>
                            <td>
                                <asp:Label ID="lblA4BW" runat="server" Text="15789"></asp:Label>
                            </td>
                            <th>MACHINE SL NO
                            </th>
                            <td>
                                <asp:Label ID="lblMachineSlNo" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th>A3 CL
                            </th>
                            <td>
                                <asp:Label ID="lblA3CL" runat="server" Text=""></asp:Label>
                            </td>
                            <th>DOCKET DATE & TIME 
                            </th>
                            <td>
                                <asp:Label ID="lblDocketDateTime" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th>A4 CL
                            </th>
                            <td>
                                <asp:Label ID="lblA4CL" runat="server" Text=""></asp:Label>
                            </td>
                            <th>DOCKET TYPE
                            </th>
                            <td>
                                <asp:Label ID="lblDocketType" runat="server" Text="CM"></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </section>
            <section>
                <table cellspacing="0" cellpadding="0" border="1">
                    <thead>
                        <tr>
                            <th>COMPLAINT
                            </th>
                            <th>DIAGONOSIS
                            </th>
                            <th>ACTION
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="lblComplaint" runat="server" Text="1/2 print"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDiagonosis" runat="server" Text="Due to ID card copy mode on"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblAction" runat="server" Text="Rectified the prohlem Machine is  working ok."></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </section>
            <section>
                <table cellspacing="0" cellpadding="0" border="1">
                    <thead>
                        <tr>
                            <th>DESCRIPTION OF PARTS
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptrParts" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td><%# Eval("SpareName") %>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
                <table cellspacing="0" cellpadding="0" border="1">
                    <thead>
                        <tr>
                            <th>CUSTOMER REMARK
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <asp:Label ID="lblCustomerRemarks" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </section>
            <section>
                <table cellspacing="0" cellpadding="0" border="1">
                    <thead>
                        <tr>
                            <th colspan="2">MACHINE IS WORKING SATISFACTORYLY
                            </th>
                        </tr>
                        <tr>
                            <th>CUSTOMER SIGNATURE
                            </th>
                            <th>STAMP
                            </th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr style="text-align:center">
                            <td>
                                <img id="imgCustomerSign" runat="server" alt="Cannot load customer signature" src="#" style="width: 150px; margin:5px;" />
                            </td>
                            <td>
                                <img id="imgCustomerStamp" runat="server" alt="Cannot load customer stamp" src="#" style="width: 150px; margin:5px;" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </section>
        </div>
    </form>
</body>
</html>
