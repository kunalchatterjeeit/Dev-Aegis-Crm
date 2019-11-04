<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VoucherPrint.aspx.cs" Inherits="WebAppAegisCRM.ClaimManagement.VoucherPrint" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        function Print() {
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
                                <img class="leftimg" src="http://crm.aegissolutions.in/images/Aegis_CRM_Logo.png" width="150" />
                            </td>
                            <td>
                                <h3>VOUCHER</h3>
                                <h2>Aegis Solutions</h2>
                                <h5>357/2B, Prince Anwar Shah Road, Kolkata-700068<br />
                                    Contact No - 8335810009, Email - info@aegissolutions.in</h5>
                            </td>
                            <td style="width: 20%">
                                <img class="rightimg" src="http://crm.aegissolutions.in/images/Aegis_CRM.png" width="110" />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </header>
            <section>
                <table cellspacing="0" cellpadding="0" style="border: 1px solid">
                    <tbody>
                        <tr>
                            <td>Voucher No.:</td>
                            <td>
                                <asp:Label ID="lblVoucherNo" runat="server" Text=""></asp:Label>
                            </td>
                            <td>Date:</td>
                            <td>
                                <asp:Label ID="lblVoucherDate" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Name</td>
                            <td colspan="3">
                                <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>Method of Payment:</td>
                            <td>
                                <asp:Label ID="lblPayMethod" runat="server" Text=""></asp:Label>
                            </td>
                            <td>Cheque No.:</td>
                            <td>
                                <asp:Label ID="lblCheque" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </tbody>
                </table>

                <table cellspacing="0" cellpadding="0" border="1">
                    <thead style="background-color: lightgrey">
                        <tr>
                            <th>Sl. No.</th>
                            <th>DESCRIPTION</th>
                            <th colspan="2">AMOUNT (&#8377;)</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater ID="rptrDescription" runat="server">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <%# Container.ItemIndex+1 %>
                                    </td>
                                    <td>
                                        <%# Eval("Description") %>
                                    </td>
                                    <td>
                                        <span style="float: right"><%# Eval("Amount").ToString().Split('.')[0] %>/-</span>
                                    </td>
                                    <td>
                                        <span style="float: right"><%# Eval("Amount").ToString().Split('.')[1] %></span>
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                        <tr style="background-color: lightgrey">
                            <td colspan="2">Rupees:
                                <asp:Label ID="lblTotalAmountInRupees" runat="server" Text=""></asp:Label>
                                <span style="float: right; background-color: grey">TOTAL</span>
                            </td>
                            <td>
                                <span style="float: right">
                                    <asp:Label ID="lblTotalAmount" runat="server" Text=""></asp:Label>/-
                                </span>
                            </td>
                            <td>
                                <span style="float: right">
                                    <asp:Label ID="lblTotalPaisa" runat="server" Text=""></asp:Label>
                                </span>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <table cellspacing="0" cellpadding="0" border="1">
                    <thead>
                        <tr>
                            <th>Owner</th>
                            <th>Accountant</th>
                            <th>Received By</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <img src="../images/paid-icon.png" alt="PAID" width="100px" />
                            </td>
                            <td>
                                <br />
                            </td>
                            <td>
                                <br />
                            </td>
                        </tr>
                    </tbody>
                </table>
            </section>
        </div>
    </form>
</body>
</html>
