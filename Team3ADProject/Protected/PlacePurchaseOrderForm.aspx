<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlacePurchaseOrderForm.aspx.cs" Inherits="Team3ADProject.Protected.PlacePurchaseOrderForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
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
	<br />
	Item Description :
	<asp:Label ID="itemDescription" runat="server"></asp:Label>
	<br />
	Required Date :
	<asp:Calendar ID="Calendar1" runat="server"></asp:Calendar>
	<br />
	Stock :
	<asp:Label ID="itemCurrentStock" runat="server"></asp:Label>
	<br />
	Unit Cost :
	$<asp:Label ID="unitCost" runat="server"></asp:Label>
	<br />
	Quantity :
	<asp:TextBox ID="quantity" runat="server" OnTextChanged="DropDownListSupplier_SelectedIndexChanged"></asp:TextBox>
	<br />
	Total Cost :
	<asp:Label ID="totalCost" runat="server"></asp:Label>
	<br />
	Remarks :<asp:TextBox ID="remarks" runat="server" Height="144px" TextMode="MultiLine" Width="910px"></asp:TextBox>
	<br />
	<asp:Button ID="Submit" runat="server" Text="submit" OnClick="Submit_Click" /><asp:Button ID="Cancel" runat="server" Text="cancel" OnClick="Cancel_Click" />
        </div>
    </form>
</body>
</html>
