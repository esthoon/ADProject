<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="StagingOfPurchaseOrder.aspx.cs" Inherits="Team3ADProject.Protected.StagingOfPurchaseOrder" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<h1>Purchase Order Staging</h1>

	<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
		<Columns>
                <asp:BoundField DataField="item_number" HeaderText="Item name" SortExpression="Username" />
        </Columns>

	</asp:GridView>
</asp:Content>
