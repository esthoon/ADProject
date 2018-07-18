<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="StagingOfPurchaseOrder.aspx.cs" Inherits="Team3ADProject.Protected.StagingOfPurchaseOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<h1>Purchase Order Staging</h1>

	<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
		<Columns>
                <asp:BoundField DataField="item_id" HeaderText="Item name" SortExpression="Username" />
                <asp:BoundField DataField="date_required" HeaderText="Date needed" SortExpression="FoodID" />
                <asp:BoundField DataField="quantity" HeaderText="Quantity" SortExpression="FoodName" />
                <asp:BoundField DataField="buyer" HeaderText="Buyer" SortExpression="Size" />
                <asp:BoundField DataField="unit_price" HeaderText="Total price" SortExpression="Chilli" />
                <asp:BoundField HeaderText="Control" SortExpression="MoreSalt" />
        </Columns>

	</asp:GridView>
</asp:Content>
