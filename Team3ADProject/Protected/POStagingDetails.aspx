<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="POStagingDetails.aspx.cs" Inherits="Team3ADProject.Protected.POStagingDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <br />
        <asp:Label ID="Labelheading" runat="server" Text="Purchase Order Staging Details for:" Font-Size="XX-Large"></asp:Label>
        <br />
        <asp:Label ID="LabelSupplier" runat="server" Text="Supplier" Font-Size="Larger"></asp:Label>
    </p>
    <p>
        <br />
        <asp:Label ID="Label1" runat="server" Text="No items in the list"></asp:Label>
    </p>
        <asp:GridView ID="GridViewPODetails" runat="server" AutoGenerateColumns="False" OnDataBound="GridViewPODetails_DataBound" CssClass="table table-hover">
            <Columns>
                <asp:BoundField HeaderText="Index" />
                <asp:BoundField DataField="DateRequired" HeaderText="Date Required" DataFormatString="{0:dd-MM-yyyy}" />
                <asp:BoundField DataField="Inventory.description" HeaderText="Item" />
                <asp:BoundField DataField="Inventory.item_number" HeaderText="Item Code" />
                <asp:BoundField DataField="UnitPrice" HeaderText="Unit Price" DataFormatString="{0:c2}" />
                <asp:TemplateField HeaderText="Ordered Qty">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("OrderedQty") %>' CausesValidation="true" AutoPostBack="true" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                        <br />
                        <asp:RangeValidator ID="Value1RangeValidator" ControlToValidate="TextBox1" Type="Integer" MinimumValue="1" MaximumValue="1000000" Display="Dynamic" ErrorMessage="Please enter an integer between than 1 and 1,000,000." runat="server"/>
						<asp:RequiredFieldValidator ID="Value2RangeValidator" ControlToValidate="TextBox1" Display = "Static" Width = "100%" ErrorMessage="Please enter an integer between than 1 and 1,000,000." runat="server"/>
						<asp:HiddenField ID="HiddenField1" runat="server"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Total Cost"/>
                <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:Button ID="Button1" runat="server" CommandName="" Text="Update" OnClick="Button1_Click" CausesValidation="true" UseSubmitBehavior="false" CssClass="btn btn-success"/>
                            <asp:HiddenField ID="HiddenField3" Value='<%# Eval("UnitPrice") %>' runat="server"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:Button ID="Button2" runat="server" CausesValidation="false" CommandName="" Text="Remove" OnClick="Button2_Click" CssClass="btn btn-warning"/>
                        <asp:HiddenField ID="HiddenField4" runat="server"/>
                        </ItemTemplate>
                    </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Label ID="Label3" runat="server"></asp:Label>
        <asp:Button ID="Button4" runat="server" Text="&lt;&lt;Staging Summary" style="position:relative; float:left; top: 0px; left: 0px;  margin-right:0x;" UseSubmitBehavior="false" OnClick="Button4_Click" CssClass="btn btn-default"/>
        <asp:Button ID="Button3" runat="server" Text="Submit for approval" CausesValidation="true" UseSubmitBehavior="false" OnClick="Button3_Click" style="position:relative; float:left; top: 0px; left: 0px;" CssClass="btn btn-primary" OnClientClick="this.disabled=true;"/>
        <br />    
</asp:Content>