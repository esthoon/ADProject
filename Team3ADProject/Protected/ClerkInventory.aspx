<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ClerkInventory.aspx.cs" Inherits="Team3ADProject.Protected.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <br />
        <asp:Label ID="Label3" runat="server" Text="Inventory" Font-Size="XX-Large"></asp:Label>
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
                    <asp:Button ID="Button2" runat="server" Text="View PO Staging" OnClick="Button2_Click1" AutoPostBack="true"/>
                </td>
                <td style="height: 27px">
                </td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Label ID="Label4" runat="server" Font-Italic="True" Font-Size="X-Small" Text="Search by item description"></asp:Label></td>
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
                    <asp:CheckBox ID="CheckBox2" runat="server" Text="Exclude items with adequate pending PO qty" OnCheckedChanged="CheckBox2_CheckedChanged" AutoPostBack="true"/>
                </td>
            </tr>
        </table>
    </div>
    <div>
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        <asp:Label ID="Label2" runat="server"></asp:Label>
    </div>
 
    <div>
        <asp:GridView ID="gvInventoryList" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvInventoryList_RowDataBound" AllowPaging="True" AllowSorting="True" OnPageIndexChanging="gvInventoryList_PageIndexChanging" PageSize="20">
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
                            <asp:Button ID="Button1" runat="server" CommandName="" Text="PO" OnClick="Button1_Click1" />
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
    </div>
    <div>
        <asp:Label ID="LabelLOWINSTOCK" runat="server" Text="(PO will be placed for only items with insufficient pending approval quantity)" style="position:relative; float:right; right:0px;" Font-Size="XX-Small"></asp:Label>
        <br />
        <asp:Button ID="btnAllPO" runat="server" Text="Place PO for all low-in-stock items" style="position:relative;  float:right; right:0px;" OnClick="btnAllPO_Click" />
        <br />
    </div>
   
</asp:Content>



