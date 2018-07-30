<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="budget.aspx.cs" Inherits="Team3ADProject.Protected.budget" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<h2> Budget</h2>
	<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table table-hover" Width="600px">
		<Columns>
					<asp:BoundField DataField="month" HeaderText="Month" />
					 <asp:BoundField DataField="budget1" HeaderText="Budget" />			       
					<asp:BoundField DataField="spent" HeaderText="Spent" />			
			        
			</Columns>
	</asp:GridView>
	<br />
	<br />


    <table>
        <tr>
            <td><asp:Label ID="Label1" runat="server" Text="Select Month"></asp:Label></td>
            <td><asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList></td>
        </tr>
        <tr>
            <td><asp:Label ID="Label2" runat="server" Text="Enter budget here"></asp:Label></td>
            <td><asp:TextBox ID="TextBox1" runat="server" OnTextChanged="TextBox1_TextChanged"></asp:TextBox></td>
        </tr>
    </table>	
    <br />
    <asp:RequiredFieldValidator ID="requiredfieldValidator1" runat="server" ControlToValidate="TextBox1" ErrorMessage="Enter whole number between 0 and 2 Billion only" ForeColor="Red"></asp:RequiredFieldValidator>
    <br />
    <asp:RangeValidator ID="RangeValidator1" runat="server" ErrorMessage="Enter whole number between 0 and 2 Billion only" ControlToValidate="TextBox1" ForeColor="Red" Type="Integer" MinimumValue="0" MaximumValue="2000000000">Enter whole number between 0 and 2 Billion only</asp:RangeValidator>
	<br />
	<asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" CssClass="btn btn-primary"/>
	
</asp:Content>
