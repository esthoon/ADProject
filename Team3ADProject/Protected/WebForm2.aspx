<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="Team3ADProject.Protected.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
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
        <br />
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
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lbad" runat="server" Text="Adjustment Quantity "></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem>+</asp:ListItem>
                            <asp:ListItem>-</asp:ListItem>
                        </asp:DropDownList>
                        <asp:TextBox ID="TextBoxAdjustment" runat="server"></asp:TextBox>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <asp:Label ID="lbtcost" runat="server" Text="Total cost: "></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="LabelTotalCost" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Remarks: "></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="TextBox1" runat="server" Height="184px" Width="745px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:Button ID="ButtonCancel" runat="server" Text="Cancel" style="position:relative; float:right; top: 0px; left: 20px; margin-right:20px;" />
                        &nbsp;<asp:Button ID="ButtonSubmit" runat="server" Text="Submit" style="position:relative; float:right; top: 0px; left: 0px;" />
                    </td>
                </tr>
            </table>
        </div>
            
            
    </form>
</body>
</html>
