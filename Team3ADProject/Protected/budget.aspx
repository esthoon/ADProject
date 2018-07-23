<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="budget.aspx.cs" Inherits="Team3ADProject.Protected.budget" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<h2> Budget</h2>
	<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
		<Columns>
					<asp:BoundField DataField="month" HeaderText="Month" />
					 <asp:BoundField DataField="budget1" HeaderText="Budget" />			       
					<asp:BoundField DataField="spent" HeaderText="Spent" />			
			        
			</Columns>
	</asp:GridView>
	<br />
	<br />
	<asp:Label ID="Label1" runat="server" Text="Select Month"></asp:Label>&emsp;
	<asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
	<br />
	<br />
	<asp:Label ID="Label2" runat="server" Text="Enter budget here"></asp:Label>&emsp;
	<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
	<br />
	<br />
	<asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" />
	<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Please enter a valid value" 
	ControlToValidate="TextBox1" ValidationExpression="^(?!0+$)[0-9]+$">

	</asp:RegularExpressionValidator>
</asp:Content>
