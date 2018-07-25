<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="pendingdetails.aspx.cs" Inherits="Team3ADProject.Protected.pendingdetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

		<h2>Requisition Details</h2>

		<asp:Label ID="Label2" runat="server" Text="Requisition Number"></asp:Label>
		<asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
		<p>
			<asp:Label ID="Label3" runat="server" Text="Date of Requisition Raised"></asp:Label>
			<asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
		</p>
		<p>
			<asp:Label ID="Label4" runat="server" Text="Requisition Status"></asp:Label>
			<asp:Label ID="Label10" runat="server" Text="Label"></asp:Label>
		</p>
		<p>
			<asp:Label ID="Label7" runat="server" Text="Employee"></asp:Label>
			<asp:Label ID="Label11" runat="server" Text="Label"></asp:Label>
		</p>
		<p>
			<asp:Label ID="Label6" runat="server" Text="Request Price"></asp:Label>
			<asp:Label ID="Label12" runat="server" Text="Label"></asp:Label>
		</p>
		<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" Width="600px" CssClass="table table-hover">
			<Columns>
				<asp:BoundField DataField="category" HeaderText="Category" />
				<asp:BoundField DataField="description" HeaderText="Description" />
				<asp:BoundField DataField="item_requisition_quantity" HeaderText="Quantity Requested" />
			</Columns>
		</asp:GridView>
		<p></p>
		<p>
			<asp:Label ID="Label13" runat="server" Text="Comments"></asp:Label>
			<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
		</p>
		<br />
		<br />
		<asp:Label ID="Label1" runat="server" Text="Allocated Budget"></asp:Label>
		<asp:Label ID="Label5" runat="server" Text=""></asp:Label>
		<br />
		<br />
		<asp:Label ID="Label14" runat="server" Text="Approved Budget"></asp:Label>
		<asp:Label ID="Label15" runat="server" Text=""></asp:Label>
		<br />
		<br />
		<p>
			<asp:Button ID="Button2" runat="server" Text="Approve" OnClick="Button2_Click" CssClass="btn btn-success"/>
			<asp:Button ID="Button1" runat="server" Text=" Reject" OnClick="Button1_Click" CssClass="btn btn-danger" />
		</p>
		<p>
		</p>
</asp:Content>
