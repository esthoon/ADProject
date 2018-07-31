<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PlacePurchaseOrderFormPopUp.aspx.cs" Inherits="Team3ADProject.Protected.PlacePurchaseOrderForm" %>

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
			<br /><br /><br />
		<table>
			<tr>
				<td>Created on:</td>
				<td>&nbsp;&nbsp;<asp:Label ID="createOnWhen" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td>Created by:</td>
				<td>&nbsp;&nbsp;<asp:Label ID="createByWho" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td>Item Number:</td>
				<td>&nbsp;&nbsp;<asp:Label ID="itemNumber" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td>Supplier:</td>
				<td>&nbsp;&nbsp;<asp:DropDownList ID="DropDownListSupplier" AutoPostBack="True" runat="server" OnSelectedIndexChanged="DropDownListSupplier_SelectedIndexChanged"></asp:DropDownList>
			<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="DropDownListSupplier" ErrorMessage="Select a supplier" ForeColor="Red"></asp:RequiredFieldValidator></td>
			</tr>
			<tr>
				<td>Item Description:</td>
				<td>&nbsp;&nbsp;<asp:Label ID="itemDescription" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td>Required Date:</td>
				<td>&nbsp;&nbsp;<asp:Label ID="LabelRequiredDate" runat="server" Text="Label"></asp:Label></td>
			</tr>
			<tr>
				<td>Stock:</td>
				<td>&nbsp;&nbsp;<asp:Label ID="itemCurrentStock" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td>Unit Cost:</td>
				<td>&nbsp;&nbsp;$<asp:Label ID="unitCost" runat="server"></asp:Label></td>
			</tr>
			<tr>
				<td>Quantity:</td>
				<td>&nbsp;&nbsp;<asp:TextBox ID="TextBoxOrderQuantity" runat="server" AutoPostBack="true" OnTextChanged="DropDownListSupplier_SelectedIndexChanged"></asp:TextBox>
            <asp:Label ID="ErrorText" runat="server" ForeColor="Red"></asp:Label></td>
			</tr>
			<tr>
				<td>Total Cost:</td>
				<td>&nbsp;&nbsp;<asp:Label ID="totalCost" runat="server"></asp:Label></td>
			</tr>
		</table>    
			<br /><br />
            

			<table>
				<tr>
					<td>Remarks:</td>
					<td><asp:TextBox ID="remarks" runat="server" Height="144px" TextMode="MultiLine" Width="910px" MaxLength="250"></asp:TextBox></td>
				</tr>
				<tr>
					<td></td>
					<td><asp:Button ID="Submit" runat="server" Text="Submit" OnClick="Submit_Click" CssClass="btn btn-primary" CausesValidation ="true" />
						&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:Button ID="Cancel" runat="server" Text="Cancel" OnClientClick="javaScript:window.close(); return false;" CssClass="btn btn-warning"/>
					</td>
				</tr>
			</table>		
			
			<br />		
			
			<asp:Label ID="Label1" runat="server"></asp:Label>
		</div>
	</form>
</body>
</html>
