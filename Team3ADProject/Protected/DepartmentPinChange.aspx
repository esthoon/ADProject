<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DepartmentPinChange.aspx.cs" Inherits="Team3ADProject.Protected.DepartmentPinChange" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Change Pin</h2>
	<asp:Label ID="Label1" runat="server" Text="Enter Password"></asp:Label>&emsp;
	<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
	<br />
	<br />
	<asp:Label ID="Label2" runat="server" Text="Reenter Password"></asp:Label>&emsp;
	<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
	<br />
	<br />
	<asp:Button ID="Button1" runat="server" Text="Change Password" OnClick="Button1_Click" />
	<br />
	<br />
	<asp:Label ID="Label3" runat="server" Text="Passwords dont match" Visible="false"></asp:Label>
</asp:Content>
