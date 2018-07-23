<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master"  EnableEventValidation="false" AutoEventWireup="true" CodeBehind="depheadpending.aspx.cs" Inherits="Team3ADProject.Protected.depheadpendingm" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">   
        <h2>Pending Orders</h2>
    	<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table table-hover">
			<Columns>
				<asp:BoundField DataField="id" HeaderText="Request Number" />
				<asp:BoundField DataField="Date" HeaderText="Request Date" /> 
				<asp:BoundField DataField="Name" HeaderText="Employee" />
				<asp:BoundField DataField="status" HeaderText="Request Status" />
				<asp:BoundField DataField="sum" HeaderText="Request Price" />
				<asp:TemplateField>
					<ItemTemplate>
						<asp:Button Id="button" Text="View" runat="server" OnClick="button_click" CssClass="btn btn-info"/>
						<asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("id")%>' />
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
		</asp:GridView>
		<br />
		<br />
		<asp:Label ID="Label1" runat="server" Text="Allocated Budget"></asp:Label>&emsp;&emsp;
		<asp:TextBox ID="TextBox1" runat="server" ReadOnly="true"></asp:TextBox>
		<br />
		<br />
		<asp:Label ID="Label2" runat="server" Text="Approved Budget"></asp:Label>&emsp;&emsp;
		<asp:TextBox ID="TextBox2" runat="server" ReadOnly="true"></asp:TextBox>
		<br />
		<br />
		<asp:Label ID="Label4" runat="server" Text="Percentage of Budget Spent" style="width: 191px; height: 147px;"></asp:Label>&emsp;&emsp;
		<asp:Label ID="Label3" runat="server" Text="" style="width: 191px; height: 147px;"></asp:Label>
		<div>
			<asp:Chart ID="Chart1" runat="server">
				<Titles><asp:Title Text="Budget vs Spent for the Month"></asp:Title></Titles>
				<Series>
					<asp:Series Name="Series1" ChartArea="ChartArea1" ChartType="Pie"></asp:Series>
				</Series>
				<ChartAreas>
					<asp:ChartArea Name="ChartArea1"></asp:ChartArea>
				</ChartAreas>
			</asp:Chart>
		</div>
</asp:Content>