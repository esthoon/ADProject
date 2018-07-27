<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="OtherPODetails.aspx.cs" Inherits="Team3ADProject.Protected.OtherPODetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div>
		<asp:Label ID="Label1" runat="server" Text="Purchase Order Details" Font-Bold="True" Font-Size="XX-Large" />
	</div>
	<br />
	<br />
	 <div>
		<asp:Label ID="Label2" runat="server" Text="Purchase Order Number :" Font-Bold="True" />
		<asp:Label ID="Label5" runat="server" Text="Label" Font-Italic="True" />
	</div>
	 <div>
		<asp:Label ID="Label8" runat="server" Text="Purchase Order Date (dd-MM-yyyy) :" Font-Bold="True"/>
		<asp:Label ID="Label9" runat="server" Text="Label" Font-Italic="True" />
	</div>
	<div>
		<asp:Label ID="Label12" runat="server" Text="Purchase Order Placed By :" Font-Bold="True"/>
		<asp:Label ID="Label13" runat="server" Text="Label" Font-Italic="True" />
	</div>
	 <div>
		<asp:Label ID="Label10" runat="server" Text="Purchase Order Status :" Font-Bold="True" />
		<asp:Label ID="Label11" runat="server" Text="Label" Font-Italic="True" />
	</div>
	<br />
	 <div>
		<asp:Label ID="Label3" runat="server" Text="Supplier Name :" Font-Bold="True"/>
		<asp:Label ID="Label4" runat="server" Text="Label" Font-Italic="True" />
	 </div>
	<div>
		<asp:Label ID="Label6" runat="server" Text="Supplier ID :" Font-Bold="True"/>
		<asp:Label ID="Label7" runat="server" Text="Label" Font-Italic="True" />
	 </div>
<br />
	 <div> 
		 <a href="<%=ResolveUrl("~/Protected/ViewPOHistory")%>" class="btn btn-default"><< Purchase Order History</a>
	 </div>
	<div>
		<asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-hover">
			<Columns>
				<asp:BoundField DataField="item_number" HeaderText="Item Code">
				<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
				<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
				</asp:BoundField>
				<asp:BoundField DataField="item_purchase_order_quantity" HeaderText="PO Quantity">
				<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
				<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
				</asp:BoundField>
				<asp:BoundField DataField="item_purchase_order_price" HeaderText="PO Price">
				<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
				<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
				</asp:BoundField>
				<asp:BoundField DataField="item_required_date" HeaderText="Item Required Date" DataFormatString="{0:dd-MM-yyyy}">
				<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
				<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
				</asp:BoundField>
				<asp:BoundField DataField="item_purchase_order_status" HeaderText="Item Receipt Status">
				<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
				<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
				</asp:BoundField>
				<asp:BoundField DataField="item_accept_quantity" HeaderText="PO Received Quantity">
				<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
				<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
				</asp:BoundField>
				<asp:BoundField DataField="item_accept_date" HeaderText="Item Receipt Date" DataFormatString="{0:dd-MM-yyyy}">
				<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
				<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
				</asp:BoundField>
				<asp:BoundField DataField="purchase_order_item_remark" HeaderText="Receipt Remarks">
				<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
				<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
				</asp:BoundField>
			</Columns>
		</asp:GridView>
		

	</div>
</asp:Content>
