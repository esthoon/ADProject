<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DepartmentPinChange.aspx.cs" Inherits="Team3ADProject.Protected.DepartmentPinChange" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Change Pin for Department <%=Session["Department"]%></h2>
	<asp:Label ID="Label1" runat="server" Text="Enter Password"></asp:Label>&emsp;
	<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
	<br />
	<br />
	<asp:Label ID="Label2" runat="server" Text="Reenter Password"></asp:Label>&emsp;
	<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
	<br />
	<br />
	<asp:Button ID="Button1" runat="server" Text="Change Password" OnClick="Button1_Click" CssClass="btn btn-success" />
	<br />
	<br />
	<asp:Label ID="Label3" runat="server" Text="Passwords dont match" Visible="false"></asp:Label>
	
<asp:RegularExpressionValidator ControlToValidate="TextBox1" ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter a valid password" ValidationExpression="^(?!0+$)[0-9]+$"></asp:RegularExpressionValidator>
<br />
<br />

</asp:Content>
