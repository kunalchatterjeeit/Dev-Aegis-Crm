<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Challan.aspx.cs" Inherits="WebAppAegisCRM.Service.Challan" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Challan</title>
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
</head>
<body>
    <form id="form1" runat="server">
        <div class="page">
            <header>
                <table cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr>
                            <td style="width: 30%; text-align: left">
                                <h2>Aegis Solutions</h2>
                                <h5>232, Jodhpur Gardens, Opp - South City Mall, Kolkata 45<br />
                                    Contact No - 8335810009, Email - info@aegissolutions.in
                                </h5>
                            </td>
                            <td>
                                <img class="leftimg" src="http://aegiscrm.in/images/Aegis_CRM_Logo.png" width="150" />
                                <h2><u>CHALLAN</u></h2>
                            </td>
                            <td style="width: 30%; text-align: left">
                                <table>
                                    <tbody>
                                        <tr>
                                            <td>Challan No. ASC</td>
                                            <td><span id="tdChallanNo" runat="server" style="border-bottom: dotted 1px #000"></span></td>
                                            <td>Date</td>
                                            <td><span id="tdChallanDate" runat="server" style="border-bottom: dotted 1px #000"></span></td>
                                        </tr>
                                        <tr>
                                            <td>Invoice No.</td>
                                            <td><span id="tdInvoiceNo" runat="server" style="border-bottom: dotted 1px #000"></span></td>
                                            <td>Date</td>
                                            <td><span id="tdInvoiceDate" runat="server" style="border-bottom: dotted 1px #000"></span></td>
                                        </tr>
                                        <tr>
                                            <td>Order No.</td>
                                            <td><span id="tdOrderNo" runat="server" style="border-bottom: dotted 1px #000"></span></td>
                                            <td>Date</td>
                                            <td><span id="tdOrderDate" runat="server" style="border-bottom: dotted 1px #000"></span></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <table style="border-bottom: solid 1px">
                                    <tbody>
                                        <tr style="text-align: left; font-size: 12pt;">
                                            <td>*VAT No.</td>
                                            <td>19621456433</td>
                                            <td>*CST No.</td>
                                            <td>19621456433</td>
                                            <td>*S.T. No.</td>
                                            <td>AAWFA7948RSD001</td>
                                            <td>*PAN No.</td>
                                            <td>AAWFA7948R</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </header>
            <section>
                <table style="font-size: 10pt; text-align: left">
                    <tr>
                        <td>Consignee,<br />
                            M/s, &nbsp; <span id="tdConsigneeName" runat="server" style="border-bottom: dotted 1px #000"></span>
                            <br />
                            VAT No.
                        <span id="tdVatNo" runat="server" style="border-bottom: dotted 1px #000"></span>
                        </td>
                        <td>Delivery Ref:<br />
                            <span id="tdDeliveryRef" runat="server" style="border-bottom: dotted 1px #000"></span>
                            Remarks:
                        <span id="tdRemarks" runat="server" style="border-bottom: dotted 1px #000"></span>
                        </td>
                    </tr>
                </table>
            </section>
            <section>
                <table cellspacing="0" cellpadding="0" border="1">
                    <thead>
                        <tr>
                            <th>Sl No.</th>
                            <th>Part No</th>
                            <th>Description</th>
                            <th>Qty.</th>
                            <th>Rate</th>
                            <th>Amount</th>
                        </tr>
                    </thead>
                    <tbody style="font-size: 10pt; text-align: center; vertical-align: top">
                        <tr>
                            <td>
                                <div style="min-height: 300px"></div>
                            </td>
                            <td></td>
                            <td>
                                <span id="tdDescription" runat="server"></span>
                            </td>
                            <td>1
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td colspan="4"></td>
                            <td style="text-align: center">TOTAL
                            </td>
                            <td style="text-align: center">
                                <span id="tdTotal" runat="server"></span>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </section>
            <section>
                <table>
                    <tbody>
                        <tr>
                            <td>*This is computer generated no signature required.
                            </td>
                            <td>Print Date Time: <span id="PrintDateTime" runat="server"></span>
                            </td>
                        </tr>
                    </tbody>
                </table>


            </section>
        </div>
    </form>
</body>
</html>
