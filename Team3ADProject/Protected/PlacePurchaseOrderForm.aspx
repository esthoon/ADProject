<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlacePurchaseOrderForm.aspx.cs" Inherits="Team3ADProject.Protected.PlacePurchaseOrderForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title></title>
	<script src="<%=ResolveUrl("~/Scripts/jquery.canvasjs.min.js")%>"></script>
	<link rel="stylesheet" href="<%=ResolveUrl("~/Content/bootstrap.css")%>" />
	<link rel="stylesheet" href="<%=ResolveUrl("~/Content/bootstrap-theme.css")%>" />
</head>
<body>
	<form id="form1" runat="server">
		<div class="container" style="margin-top: 5vh;">
			Created on:
	<asp:Label ID="createOnWhen" runat="server"></asp:Label>
			<br />
			Created by:
	<asp:Label ID="createByWho" runat="server"></asp:Label>
			<br />
			Item no. :
	<asp:Label ID="itemNumber" runat="server"></asp:Label>
			<br />
			Supplier :
	<asp:DropDownList ID="DropDownListSupplier" AutoPostBack="True" runat="server" OnSelectedIndexChanged="DropDownListSupplier_SelectedIndexChanged"></asp:DropDownList>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DropDownListSupplier" ErrorMessage="Select a supplier" ForeColor="Red"></asp:RequiredFieldValidator>
			<br />
			Item Description :
	<asp:Label ID="itemDescription" runat="server"></asp:Label>
			<br />
			Required Date :
			<asp:Label ID="LabelRequiredDate" runat="server" Text="Label"></asp:Label>
			<br />
			Stock :
	<asp:Label ID="itemCurrentStock" runat="server"></asp:Label>
			<br />
			Unit Cost :
	$<asp:Label ID="unitCost" runat="server"></asp:Label>
			<br />
			Quantity :
	<asp:TextBox ID="TextBoxOrderQuantity" runat="server" AutoPostBack="True" OnTextChanged="DropDownListSupplier_SelectedIndexChanged"></asp:TextBox>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter a qty" ForeColor="#FF3300" ControlToValidate="TextBoxOrderQuantity"></asp:RequiredFieldValidator>
			<asp:CompareValidator runat="server" Operator="GreaterThan" Type="Integer" ControlToValidate="TextBoxOrderQuantity" ErrorMessage="Value must be a whole number" ForeColor="Red" ValueToCompare="0" />
			<br />
			Total Cost :
	<asp:Label ID="totalCost" runat="server"></asp:Label>
			<br />
			Remarks :<asp:TextBox ID="remarks" runat="server" Height="144px" TextMode="MultiLine" Width="910px"></asp:TextBox>
			<br />
			<asp:Button ID="Submit" runat="server" Text="Submit" OnClick="Submit_Click" CssClass="btn btn-primary" />
			<asp:Button ID="Cancel" runat="server" Text="Cancel" OnClick="Cancel_Click" CssClass="btn btn-warning"/>
			<asp:Label ID="Label1" runat="server"></asp:Label>
		</div>
	</form>
</body>
</html>
