<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ChargeBack.aspx.cs" Inherits="Team3ADProject.Protected.ChargeBack" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="From: "></asp:Label>
                    <asp:TextBox ID="TextBox1" runat="server" TextMode="Date"></asp:TextBox>

                </td>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="To: "></asp:Label>
                    <asp:TextBox ID="TextBox2" runat="server" TextMode="Date"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Search" UseSubmitBehavior="false" OnClick="Button1_Click"/>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
        </table>
    </div>
    <div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="description" HeaderText="Item" />
            <asp:BoundField DataField="received_quantity" HeaderText="Qty Received" />
            <asp:BoundField DataField="price" HeaderText="Cost" DataFormatString="{0:c2}" />
        </Columns>
    </asp:GridView>
    </div>
    <asp:Label ID="Label3" runat="server" Text="Total Price: "></asp:Label>
    <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
    <div>
    <asp:Button ID="Button2" runat="server" Text="Send ChargeBack" OnClick="Button2_Click" />
    </div>


</asp:Content>
