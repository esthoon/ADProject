<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Change Collection Point.aspx.cs" Inherits="Team3ADProject.Protected.Change_Collection_Point" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Change Location</h2>
	<asp:Label ID="Label1" runat="server" Text="Select Location"></asp:Label>&emsp;
	<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="false"></asp:DropDownList>
	<br />
	<br />
	<asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" CssClass="btn btn-primary" />
	<br />
	<br />
	<asp:Label ID="Label2" runat="server" Text="Location is Changed" Visible="false"></asp:Label>
	<br />
	<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
		<Columns>
			<asp:BoundField DataField="collection_place" HeaderText="Collection Place" />
			<asp:BoundField DataField="collection_date" HeaderText="Collection Date" />			
			</Columns>
	</asp:GridView>
	<br />
</asp:Content>