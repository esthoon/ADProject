<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="DelegateRole.aspx.cs" Inherits="Team3ADProject.Protected.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	
			<h2>Delegate Role</h2>
			<br />
			<asp:Label ID="Label1" runat="server" Text="Name"></asp:Label>
			<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
			<asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" CssClass="btn btn-default" CausesValidation="false" />
			<br />
			<asp:Label ID="Label5" runat="server" Text="No Results available" Visible="false"></asp:Label>
			<br />
			<br />
			<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-hover" Width="600px">
				<Columns>
					<asp:BoundField DataField="employee_name" HeaderText="Name" />
					<asp:TemplateField>
					<ItemTemplate>
						<asp:Button Id="button" Text="Select" runat="server" OnClick="button_click" CssClass="btn btn-info" CausesValidation="false"/>
						<asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("employee_name")%>' />
					</ItemTemplate>
				</asp:TemplateField>
				</Columns>				
			</asp:GridView>
			<br />
			<br />
            
			<asp:Label ID="Label2" runat="server" Text="Employee Selected"></asp:Label>			
			<asp:TextBox runat="server" ID="TextBox" ReadOnly="true"></asp:TextBox>   
   
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Please select an employee to delegate" ControlToValidate="TextBox" ForeColor="Red"></asp:RequiredFieldValidator> 
	<br />
    <asp:Button ID="Button2" runat="server" Text="Delegate Role" OnClick="Button2_Click" CssClass="btn btn-primary" CausesValidation="true" />
                  
	<br />
	<br />
	<br />
	<br />
	<asp:Label ID="Label3" runat="server" Text="Delegated Employee" Font-Bold="true"></asp:Label>&emsp;
	<br />
	<br />
	<asp:Label ID="Label4" runat="server" Text="Employee"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	<asp:TextBox ID="TextBox2" runat="server" ReadOnly="true" ></asp:TextBox>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	<br />
	<br />
&nbsp;
	<asp:Button ID="Button3" runat="server" Text="Revoke" OnClick="Button3_Click" CausesValidation="false" />
	<br />
	<br />
	<br />
	<br />
	<br />
	<br />
	<br />
	<br />
</asp:Content>