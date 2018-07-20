<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master"  EnableEventValidation="false" AutoEventWireup="true" CodeBehind="depheadpending.aspx.cs" Inherits="Team3ADProject.Protected.depheadpendingm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<html xmlns="http://www.w3.org/1999/xhtml">
	<body>
   
        <h2>Pending Orders</h2>
    	<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false">
			<Columns>
				<asp:BoundField DataField="id" HeaderText="Request Number" />
				<asp:BoundField DataField="Date" HeaderText="Request Date" /> 
				<asp:BoundField DataField="Name" HeaderText="Employee" />
				<asp:BoundField DataField="status" HeaderText="Request Status" />
				<asp:BoundField DataField="sum" HeaderText="Request Price" />
				<asp:TemplateField>
					<ItemTemplate>
						<asp:Button Id="button" Text="View" BackColor="LightGreen" runat="server" OnClick="button_click"/>
						<asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("id")%>' />
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
		</asp:GridView>
    	
   
</body>
</html>
</asp:Content>
