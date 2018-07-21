<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ClerkInventory.aspx.cs" Inherits="Team3ADProject.Protected.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <br />
    </div>
    <div>
        <table>
            <tr>
                <td style="height: 27px">
                    <asp:DropDownList ID="ddlCategory" runat="server" Width="208px" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged" AutoPostBack="true">
                    </asp:DropDownList>
                    &emsp;
                </td>
                <td style="height: 27px">
                    <asp:TextBox ID="TextBox1" runat="server" Width="352px" OnTextChanged="Button1_Click" AutoPostBack="true"></asp:TextBox>
                    &emsp;
                </td>
                <td style="height: 27px">
                    <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" />
                    &emsp;
                </td>
                <td style="height: 27px">
                    <asp:Button ID="Button2" runat="server" Text="View PO Staging" OnClick="Button2_Click1" AutoPostBack="true"/>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <br />
                    <asp:CheckBox ID="CheckBox1" runat="server" Text="Include obsolete items" OnCheckedChanged="CheckBox1_CheckedChanged" autopostback="true"/>
                </td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Selected="True" Value="1">All</asp:ListItem>
                        <asp:ListItem Value="2">Low in stock</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        <br />
    </div>
    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>

    <div>
        <asp:GridView ID="gvInventoryList" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvInventoryList_RowDataBound">
            <Columns>
                <asp:BoundField DataField="Inventory.item_number" HeaderText="Item no." />
                <asp:BoundField DataField="Inventory.description" HeaderText="Item Description" />
                <asp:BoundField DataField="Inventory.category" HeaderText="Category" />
                <asp:BoundField DataField="Inventory.reorder_level" HeaderText="Reorder Level" />
                <asp:BoundField DataField="Inventory.current_quantity" HeaderText="Current Qty" />
                <asp:BoundField DataField="reorder_quantity" HeaderText="Reorder Qty" />
                <asp:BoundField HeaderText="Ordered Qty" DataField="OrderedQty" />
                <asp:BoundField HeaderText="Pending Approval Qty" DataField="PendingApprovalQty" />
                <asp:BoundField DataField="PendingAdjustmentQty" HeaderText="Pending Adjustment Qty" />
                <asp:BoundField DataField="Inventory.unit_of_measurement" HeaderText="Unit of Measure" />
                <asp:BoundField DataField="Inventory.item_status" HeaderText="Status" />
                <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:Button ID="Button1" runat="server" CommandName="" Text="PO" />
                            <asp:HiddenField ID="HiddenFieldID" runat="server" Value='<%# Eval("Inventory.item_number") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:Button ID="Button2" runat="server" CausesValidation="false" CommandName="" Text="ADJ" OnClick="Button2_Click"/>
                        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("Inventory.item_number") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <br />
        <asp:Button ID="btnAllPO" runat="server" Text="Place PO for all low-in-stock items" style="position:relative; float:right; top: 0px; left: 0px;" OnClick="btnAllPO_Click" />
        <br />
    </div>
    
</asp:Content>



