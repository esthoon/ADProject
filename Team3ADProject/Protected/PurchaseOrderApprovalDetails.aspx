<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PurchaseOrderApprovalDetails.aspx.cs" Inherits="Team3ADProject.Protected.PurchaseOrderApprovalDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <b>PO Details</b>
    <br />
    <br />
    <table width="500">
        <tr>
            <td>
                <b>Employee Name:</b>
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>

            </td>
            <td>
                <b>PO No.</b>
                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>

            </td>
        </tr>
        <tr>
            <td>
                <b>PO Date</b>
                <asp:Label ID="Label4" runat="server" Text="Label" ></asp:Label>
            </td>
            <td>
                <b>Total PO Price:</b>
                <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
    </table>
    <br />



    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Height="133px" Width="1000px" CssClass="table">
        <Columns>
            <asp:BoundField DataField="description" HeaderText="Item Description" />
            <asp:BoundField DataField="item_required_date" HeaderText="Required Date" DataFormatString="{0:dd-MM-yyyy}" />
            <asp:BoundField DataField="item_purchase_order_quantity" HeaderText="Quantity" />
            <asp:BoundField DataField="current_quantity" HeaderText="Current Quantity" />
            <asp:BoundField DataField="reorder_level" HeaderText="Reorder Level" />
            <asp:BoundField DataField="item_purchase_order_price" HeaderText="Item SubTotal" DataFormatString="{0:c2}" />


        </Columns>
    </asp:GridView>
    <br />
    <b>Manager Comment</b>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Button ID="Button1" runat="server" Text="Approve" OnClick="Button1_Click" CssClass="btn btn-success" />

    <asp:Button ID="Button2" runat="server" Text="Reject" OnClick="Button2_Click" CssClass="btn btn-danger" />


</asp:Content>
