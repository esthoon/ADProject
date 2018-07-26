<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DepartmentPinChange.aspx.cs" Inherits="Team3ADProject.Protected.DepartmentPinChange" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Change Pin for Department <%=Session["Department"]%></h2>
    <br />
    <br />
	
	<table>
		<tr style="height:40px">
			<td>
				 <asp:Label ID="Label1" runat="server" Text="Enter Pin"></asp:Label>
			</td>
			<td>
				<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
			</td>
		</tr>
   
		<tr style="height:40px">
			<td>
				<asp:Label ID="Label2" runat="server" Text="Re-Enter Pin"></asp:Label>&emsp;
			</td>			
			<td>
				<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
			</td>
		</tr>
	</table>
	


	<br />
	<br />
	<asp:Label ID="Label3" runat="server" Text="Pins dont match" Visible="false"></asp:Label>
	<br />
	<asp:Button ID="Button1" runat="server" Text="Change Pin" OnClick="Button1_Click" CssClass="btn btn-success" />
	<br />
	<br />
	
	
<asp:RegularExpressionValidator ControlToValidate="TextBox1" ID="RegularExpressionValidator1" runat="server" ErrorMessage="Only numeric pin allowed" ValidationExpression="^(?!0+$)[0-9]+$"></asp:RegularExpressionValidator>
<br />
<br />

</asp:Content>
