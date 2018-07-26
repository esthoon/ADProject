<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="ChangeRep.aspx.cs" Inherits="Team3ADProject.Protected.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Change Representative</h2>
	
	<asp:Label ID="Label1" runat="server" Text="Employee Name"></asp:Label>&emsp;
	<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox> &emsp;
	<asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" CssClass="btn btn-default" />
	<br />
			<asp:Label ID="Label5" runat="server" Text="Please try again." ForeColor="Red" Visible="false"></asp:Label>
	<br />
	<asp:GridView ID="GridView1" runat="server"  OnSelectedIndexChanged="GridView1_SelectedIndexChanged"  AutoGenerateColumns="false" CssClass="table table-hover" Width="600px">
			<columns>
				<asp:BoundField DataField="employee_name" HeaderText="Name" />
				<asp:TemplateField>
					<ItemTemplate>
						<asp:Button Id="button" Text="Select" runat="server" OnClick="button_click" CssClass="btn btn-info"/>
						<asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("employee_name")%>' />
					</ItemTemplate>
				</asp:TemplateField>
			</columns>
		</asp:GridView>
	<br />
	<asp:Label ID="Label2" runat="server" Text="Employee Selected"></asp:Label>&emsp;
	<asp:TextBox ID="TextBox2" runat="server" ReadOnly="true"></asp:TextBox>&emsp;
	<asp:Button ID="Button2" runat="server" Text="Appoint Rep" OnClick="Button2_Click" CssClass="btn btn-primary" />
	<asp:Label ID="Label4" runat="server" Text="No Rep selected" Visible="false" ForeColor="Red"></asp:Label>&emsp;
	<br />
	<br />
	
	<asp:Label ID="Label3" runat="server" Text="Representative Details" Font-Bold="true" Font-Size="Medium"></asp:Label>
	<asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="false" CssClass="table table-hover">
		<Columns>
			<asp:BoundField DataField="employee_name" HeaderText="Name" />
			<asp:BoundField DataField="representative_status" HeaderText="Status" />
			<asp:BoundField DataField="appointed_date" HeaderText="Appointed Date" DataFormatString="{0:dd-MM-yyyy}" />
		</Columns>
	</asp:GridView>
	<br />
	<br />	

</asp:Content>