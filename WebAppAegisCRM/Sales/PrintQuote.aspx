<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintQuote.aspx.cs" Inherits="WebAppAegisCRM.Sales.PrintQuote" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript">
        function Print() {
            window.print();
        }
    </script>
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

        .tblrptr tr:nth-child(even) {
            background-color: grey;
        }
    </style>
    <title>PRINT QUOTE</title>
</head>
<body>
    <form id="form1" runat="server" onclick="Print()">
        <div class="page" title="Click here to print/pdf" style="text-align: center;">

            <table cellspacing="0" cellpadding="0" border="0">
                <tbody>
                    <tr>
                        <td style="width: 40%">
                            <img class="leftimg" src="../images/image001.png" width="200" /><br />
                            <img class="leftimg" src="../images/image003.png" width="100" />
                            <img class="leftimg" src="../images/image004.png" width="100" />
                        </td>
                        <td>
                            <table border="1" cellspacing="0" cellpadding="0" style="width: 90%">
                                <tr>
                                    <td colspan="2" style="width: 30%;">QUOTATION/ PROFORMA INVOICE</td>

                                </tr>
                                <tr style="text-align: left;">
                                    <td style="width: 10%;">&nbsp;No:-</td>
                                    <td style="width: 20%;">
                                        <asp:Label ID="lblQuoteNo" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr style="text-align: left;">
                                    <td style="width: 10%;">&nbsp;Date:-</td>
                                    <td style="width: 20%;">
                                        <asp:Label ID="lblDate" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr style="text-align: left;">
                                    <td style="width: 10%;">&nbsp;Ref:-</td>
                                    <td style="width: 20%;">
                                        <asp:Label ID="lblRefNo" runat="server" Text=""></asp:Label></td>
                                </tr>
                                <tr style="text-align: left;">
                                    <td style="width: 10%;">&nbsp;Kind Attention:-</td>
                                    <td style="width: 20%;">
                                        <asp:Label ID="lblKindAttention" runat="server" Text=""></asp:Label></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table border="1" cellspacing="0" cellpadding="0" style="width: 65%; margin-left: 18%;">
                                <tr>
                                    <td style="width: 20%;">SERVICE PROVIDER</td>
                                </tr>
                                <tr>
                                    <td style="width: 20%; text-align: left;" height="170">&nbsp;
                                            <h2 style="font-weight: bold;">&nbsp;Aegis Services</h2>
                                        &nbsp;58, G.T. Road,<br />
                                        &nbsp;Opp:- Bally Fire  Station 	
                                        <br />
                                        &nbsp;Howrah-711201	
                                        <br />
                                        &nbsp;9831111181	
                                        <br />
                                        &nbsp;sales@aegissolutions.in	
                                        <br />
                                        &nbsp;www.aegissolutions.in	
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <table border="1" cellspacing="0" cellpadding="0" style="width: 90%;">
                                <tr>
                                    <td style="width: 40%;">CUSTOMER</td>
                                </tr>
                                <tr>
                                    <td style="width: 40%; font-weight: bold; text-align: left;" height="170">&nbsp;To,&nbsp;&nbsp;<asp:Label ID="lblCustomer" runat="server" Text=""></asp:Label></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0" id="tblrptr" border="1" style="width: 218%; margin-left: 18%;">
                                <thead>
                                    <tr>
                                        <th style="text-align: center;">Sl. No.</th>
                                        <th style="text-align: center;">DESCRIPTION
                                        </th>
                                        <th style="text-align: center;">Qty
                                        </th>
                                        <th style="text-align: center;">Rate
                                        </th>
                                        <th style="text-align: center;">Discount
                                        </th>
                                        <th style="text-align: center;">Amount</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <asp:Repeater ID="rptrRepeatLineItem" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.ItemIndex + 1 %>' runat="server" /></td>
                                                <td><%# Eval("ItemName") %></td>
                                                <td><%# Eval("Quantity") %></td>
                                                <td><%# Eval("UnitPrice") %></td>
                                                <td><%# Eval("Discount") %></td>
                                                <td><%# Eval("Amount") %></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0" border="1" style="width: 218%; margin-left: 18%;">
                                <thead>
                                    <tr style="background-color: gray;">
                                        <th>DELIVERY PERIOD</th>
                                        <th></th>
                                        <th>SUBTOTAL
                                        </th>
                                        <th>
                                            <asp:Label ID="lblSubtotal" runat="server" Text=""></asp:Label></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr style="text-align: left;">
                                        <td>VALIDITY</td>
                                        <td>15days</td>
                                        <td>GST
                                            <asp:Label ID="lblTaxRate" runat="server" Text=""></asp:Label></td>
                                        <td>
                                            <asp:Label ID="lblGST" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr style="background-color: gray; text-align: left;">
                                        <td>PAYMENT TERMS</td>
                                        <td>100% Alongwith Order</td>
                                        <td>DELIVERY CHARGES</td>
                                        <td>
                                            <asp:Label ID="lblDeliveryCharges" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                    <tr style="text-align: left;">
                                        <td>WARRANTY</td>
                                        <td></td>
                                        <td>TOTAL</td>
                                        <td>
                                            <asp:Label ID="lblTotal" runat="server" Text=""></asp:Label></td>
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 150%;">
                                <tr>
                                    <td>Looking forward towards a long standing professional relationship…</td>
                                </tr>
                            </table>
                            <table border="1" cellspacing="0" cellpadding="0" style="width: 97%; margin-left: 18%;">
                                <tr>
                                    <td style="width: 20%; text-align: left;" height="190">
                                        <p>&nbsp;for</p>
                                        <p style="font-weight: bold;">&nbsp; Aegis Services</p>
                                        <img class="leftimg" src="../images/image009.png" width="100" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <img class="leftimg" src="../images/image007.png" width="100" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                            <br />
                            <br />
                            <table border="1" cellspacing="0" cellpadding="0" style="width: 67%; margin-left: 112px; margin-top: -11px;">
                                <tr>
                                    <td style="width: 20%; background-color: grey;">REMARKS</td>
                                </tr>
                                <tr>
                                    <td style="width: 20%; text-align: left;" height="190">&nbsp;
                                            <h2 style="font-weight: bold; margin-top: -31px;">&nbsp;Bank Details:-</h2>
                                        &nbsp;Name :-Aegis Services<br />
                                        &nbsp;A/c No. 1676102000005258		
                                        <br />
                                        &nbsp;IFSC Code:- IBKL0001676
                                        <br />
                                        &nbsp;Bank:- IDBI Bank Ltd	
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%; font-stretch: expanded" colspan="2">
                            <img class="leftimg" src="../images/image011.png" width="118" />&nbsp;&nbsp;
                            <img class="leftimg" src="../images/image013.png" width="118" />&nbsp;&nbsp;
                            <img class="leftimg" src="../images/image014.png" width="118" />&nbsp;&nbsp;
                            <img class="leftimg" src="../images/image015.png" width="118" />&nbsp;&nbsp;
                            <img class="leftimg" src="../images/image016.png" width="118" />&nbsp;&nbsp;
                            <img class="leftimg" src="../images/image017.png" width="118" />
                        </td>
                    </tr>
                </tbody>
            </table>

        </div>
    </form>
</body>
</html>
