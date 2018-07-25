<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdjustmentForm.aspx.cs" Inherits="Team3ADProject.Protected.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <link rel="stylesheet" href="<%=ResolveUrl("~/Content/bootstrap.css")%>" />
    <link rel="stylesheet" href="<%=ResolveUrl("~/Content/bootstrap-theme.css")%>" />
    <title>Adjustment Form</title>
    <style type="text/css">
        .auto-style1 {
            height: 42px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div>
                <asp:Label ID="Label2" runat="server" Text="Adjustment Form" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
            </div>
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Created on: "></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelDate" runat="server" Text="date"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Created by"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelName" runat="server" Text="name"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <asp:Label ID="LabelError" runat="server" ForeColor="Red"></asp:Label>
            <br />
            <div>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lbinum" runat="server" Text="Item no. "></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelItemNum" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbdes" runat="server" Text="Description "></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelItem" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbstk" runat="server" Text="Stock "></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelStock" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbuprice" runat="server" Text="Unit Price "></asp:Label>
                        </td>
                        <td>$<asp:Label ID="LabelUnitPrice" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            <asp:Label ID="lbad" runat="server" Text="Adjustment Quantity "></asp:Label>
                        </td>
                        <td class="auto-style1">
                            <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="TextBoxAdjustment_TextChanged">
                                <asp:ListItem>+</asp:ListItem>
                                <asp:ListItem Selected="True">-</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="TextBoxAdjustment" runat="server" CausesValidation="True" AutoPostBack="true" OnTextChanged="TextBoxAdjustment_TextChanged"></asp:TextBox>
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TextBoxAdjustment" ErrorMessage="Quantity must be more than 0" ForeColor="Red" Operator="GreaterThan" Type="Integer" ValueToCompare="0"></asp:CompareValidator>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxAdjustment" ErrorMessage="No quantity stated" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbtcost" runat="server" Text="Total cost: "></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LabelTotalCost" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Remarks: "></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxRemarks" runat="server" Height="184px" Width="745px" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" Style="position: relative; float: right; top: 0px; margin-left: 0.5vw;" CssClass="btn btn-warning" CausesValidation="false" OnClick="ButtonCancel_Click" />
                            <asp:Button ID="ButtonSubmit" runat="server" Text="Submit" Style="position: relative; float: right; top: 0px;" CssClass="btn btn-primary" OnClick="ButtonSubmit_Click" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" />
                        </td>
                    </tr>
                </table>


                <asp:Label ID="LabelGrid" runat="server" Text="Label"></asp:Label>

                <div class="row" style="margin-top: 2.5vh;">
                    <div class="col col-md-6">
                        
                        <asp:Label ID="LabelGridMinus" runat="server" Text="Label"></asp:Label>
                        <asp:GridView ID="GridViewAdjMinus" runat="server" AutoGenerateColumns="False" CssClass="table">
                            <Columns>
                                <asp:BoundField DataField="item_number" HeaderText="Item number" />
                                <asp:BoundField DataField="employee.employee_name" HeaderText="Raised by" />
                                <asp:BoundField DataField="adjustment_date" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date Raised" />
                                <asp:BoundField DataField="employee_remark" HeaderText="Remarks" />
                                <asp:BoundField DataField="adjustment_quantity" HeaderText="Adjusted Quantity" />
                            </Columns>
                        </asp:GridView>
                    </div>

                    <div class="col col-md-6">

                        <asp:Label ID="LabelGridPlus" runat="server" Text="Label"></asp:Label>
                        <asp:GridView ID="GridViewAdjPlus" runat="server" AutoGenerateColumns="False" CssClass="table" Style="margin-top: 1px">
                            <Columns>
                                <asp:BoundField DataField="item_number" HeaderText="Item number" />
                                <asp:BoundField DataField="employee.employee_name" HeaderText="Raised by" />
                                <asp:BoundField DataField="adjustment_date" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date Raised" />
                                <asp:BoundField DataField="employee_remark" HeaderText="Remarks" />
                                <asp:BoundField DataField="adjustment_quantity" HeaderText="Adjusted Quantity" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <!--
    <script>
        $(document).ready(function () {
            $('#DropDownList1, #TextBoxAdjustment').change(function(){
                var a = parseFloat($('#<%=LabelUnitPrice.ClientID%>').text());
                var b = $('#<%=DropDownList1.ClientID%>').val();
                var c = $('#<%=TextBoxAdjustment.ClientID%>').val();
                var d;
                if (c != null && $.isNumeric(c)) {
                    if (b == '-') {
                        d = a * (-c);
                    } else {
                        d = a * c;
                    }
                    $('#<%=LabelTotalCost.ClientID%>').text(d);
                }
            })
        })
    </script>  
-->
</body>
</html>
