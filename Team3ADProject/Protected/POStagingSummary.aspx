<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="POStagingSummary.aspx.cs" Inherits="Team3ADProject.Protected.POStagingSummaryaspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<p>
        &nbsp;</p>
    <p>
        <asp:Label ID="Label2" runat="server" Text="Purchase Order Staging Summary" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
    </p>
    <p>
        <asp:Label ID="LabelNoResult" runat="server" Text="No items"></asp:Label>
        <asp:Label ID="Label1" runat="server"></asp:Label>
        <br />
        <asp:GridView ID="GridViewPOStagingSummary" runat="server" AutoGenerateColumns="False" OnDataBound="GridViewPOStagingSummary_DataBound" CssClass="table table-hover" >
            <Columns>
                <asp:BoundField HeaderText="Index" />
                <asp:BoundField DataField="SupplierID" HeaderText="Supplier" />
                <asp:BoundField DataField="ItemCount" HeaderText="Count" />
                <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:Button ID="Button2" runat="server" CausesValidation="false" CommandName="" Text="View Details" OnClick="Button2_Click" CssClass="btn btn-info"/>
                        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("SupplierID") %>'/>
                        </ItemTemplate>
                    </asp:TemplateField>
                <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:Button ID="Button3" runat="server" CausesValidation="false" CommandName="" Text="Remove" OnClick="Button3_Click" UseSubmitBehavior="false" CssClass="btn btn-warning" />
                        </ItemTemplate>
                    </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <asp:Button ID="Button4" runat="server" Text="&lt;&lt;Inventory" style="position:relative; float:left; top: 1px; left: 0px;" UseSubmitBehavior="false" OnClick="Button4_Click" CssClass="btn btn-default"/>
        <asp:Button ID="ButtonPOApproval" runat="server" Text="Submit all for approval" style="position:relative; float:left; top: 1px; left: 0px;" UseSubmitBehavior="false" OnClick="ButtonPOApproval_Click" CssClass="btn btn-primary" OnClientClick="this.disabled=true;"/>
        <asp:Button ID="ButtonClear" runat="server" Text="Remove all" style="position:relative; float:left; top: 1px; left: 1px;" UseSubmitBehavior="false" OnClick="ButtonClear_Click" CssClass="btn btn-danger"/>
    </p>
</asp:Content>