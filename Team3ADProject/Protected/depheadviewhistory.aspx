<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="depheadviewhistory.aspx.cs" Inherits="Team3ADProject.Protected.depheadviewhistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<html xmlns="http://www.w3.org/1999/xhtml">

<body>
    
        
			<h2>Requisition Order History</h2>	
	<div>
			<asp:Label ID="Label1" runat="server" Text="Request Status"></asp:Label>
        	<asp:DropDownList ID="dropdown1" runat="server" OnSelectedIndexChanged="Unnamed1_SelectedIndexChanged">
				<asp:ListItem>All</asp:ListItem>
				<asp:ListItem>Approved</asp:ListItem>
				<asp:ListItem>Pending</asp:ListItem>
				<asp:ListItem>Rejected</asp:ListItem>
			</asp:DropDownList>
			<asp:Label ID="Label2" runat="server" Text="Employee Name"></asp:Label>
			<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
			<asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" CssClass="btn btn-primary" />
			<br />
			<asp:Label ID="Label3" runat="server" Text="No Results"></asp:Label>
			</div>
			<div>
			<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table table-hover">
				<Columns>
					<asp:BoundField DataField="id" HeaderText="Request Number" />
					<asp:BoundField DataField="Date" HeaderText="Request Date" DataFormatString="{0:dd-MM-yyyy}"/>
					<asp:BoundField DataField="Name" HeaderText="Name" />
					<asp:BoundField DataField="sum" HeaderText="Request Amount ($)" DataFormatString="{0:N2}"/>
					<asp:BoundField DataField="status" HeaderText="Request Status" />
					<asp:TemplateField>
					<ItemTemplate>
						<asp:Button Id="button" Text="View" runat="server" OnClick="button_click" CssClass="btn btn-info"/>
						<asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("id")%>' />
					</ItemTemplate>
				</asp:TemplateField>
				</Columns>
		    </asp:GridView>
			
			
            </div>	
		
    
</body>
</html>

</asp:Content>