﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="POStagingDetails.aspx.cs" Inherits="Team3ADProject.Protected.POStagingDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
        <br />
        <asp:Label ID="Labelheading" runat="server" Text="Purchase Order Staging Details for:" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
        <br />
        <asp:Label ID="LabelSupplier" runat="server" Text="Supplier" Font-Bold="True" Font-Size="X-Large"></asp:Label>
    </p>
    <p>
        <br />
        <asp:Label ID="Label1" runat="server" Text="No items in the list"></asp:Label>
    </p>
        <asp:GridView ID="GridViewPODetails" runat="server" AutoGenerateColumns="False" OnDataBound="GridViewPODetails_DataBound">
            <Columns>
                <asp:BoundField HeaderText="No." />
                <asp:BoundField DataField="DateRequired" HeaderText="Date Required" DataFormatString="{0:yyyy/MM/dd}" />
                <asp:BoundField DataField="Inventory.description" HeaderText="Item" />
                <asp:BoundField DataField="Inventory.item_number" HeaderText="Item Code" />
                <asp:BoundField DataField="UnitPrice" HeaderText="Unit Price" />
                <asp:TemplateField HeaderText="Ordered Qty">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Eval("OrderedQty") %>' CausesValidation="true" AutoPostBack="true" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                        <br />
                        <asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" ControlToValidate="TextBox1" ErrorMessage="Value must be a whole number" ForeColor="Red"/>
                        <asp:HiddenField ID="HiddenField1" runat="server"/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField HeaderText="Total Cost"/>
                <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:Button ID="Button1" runat="server" CommandName="" Text="Update" OnClick="Button1_Click" CausesValidation="true" UseSubmitBehavior="false"/>
                            <asp:HiddenField ID="HiddenField3" runat="server"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:Button ID="Button2" runat="server" CausesValidation="false" CommandName="" Text="Remove" OnClick="Button2_Click"/>
                        <asp:HiddenField ID="HiddenField4" runat="server"/>
                        </ItemTemplate>
                    </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Label ID="Label3" runat="server"></asp:Label>
        <asp:Button ID="Button3" runat="server" Text="Submit for approval" CausesValidation="true" UseSubmitBehavior="false" OnClick="Button3_Click" style="position:relative; float:right; top: 0px; left: 0px;  margin-right:130px;"/>
        <asp:Button ID="Button4" runat="server" Text="&lt;&lt;Staging Summary" style="position:relative; float:right; top: 0px; left: 0px;  margin-right:0x;" UseSubmitBehavior="false" OnClick="Button4_Click"/>
        <br />    
</asp:Content>