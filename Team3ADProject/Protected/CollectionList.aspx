<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CollectionList.aspx.cs" Inherits="Team3ADProject.Protected.CollectionList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Warehouse Collection List</h1>

    <asp:GridView ID="gv_CollectionList" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="10" OnPageIndexChanging="gv_CollectionList_PageIndexChanging" CssClass="table table-hover align-center" EmptyDataText="There are no new items to collect">
        <Columns>
            <asp:BoundField DataField="item_number" HeaderText="Item Number" ReadOnly="True" SortExpression="item_number" />
            <asp:BoundField DataField="description" HeaderText="Description" ReadOnly="True" SortExpression="description" />
            <asp:BoundField DataField="unit_of_measurement" HeaderText="UOM" ReadOnly="True" SortExpression="unit_of_measurement" />
            <asp:BoundField DataField="quantity_ordered" HeaderText="Qty Ordered" ReadOnly="True" SortExpression="quantity_ordered" />
            <asp:BoundField DataField="current_quantity" HeaderText="Qty Available" ReadOnly="True" SortExpression="current_quantity" />

            <asp:TemplateField HeaderText="Qty Prepared">
                <ItemTemplate>
                    <asp:TextBox ID="txt_QtyPrepared" runat="server" Text='<%# (Convert.ToInt32(Eval("quantity_ordered")) <= Convert.ToInt32(Eval("current_quantity")) ? Eval("quantity_ordered") : Eval("current_quantity")) %>'></asp:TextBox>

                    <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>

                    <asp:CompareValidator ID="CompareValidator_txt_QtyPrepared" runat="server"
                        ControlToValidate="txt_QtyPrepared" ErrorMessage="Can't be -ve number."
                        Operator="GreaterThanEqual" Type="Integer" ForeColor="Red"
                        ValueToCompare="0" ValidationGroup='valGroup1' />

                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_QtyPrepared" ID="RequiredValidator_txt_QtyPrepared" ErrorMessage="Enter a number." ForeColor="Red" ValidationGroup='valGroup1' />

                    <asp:RegularExpressionValidator ID="RegularExpressionValidator_txt_QtyPrepared" runat="server" ControlToValidate="txt_QtyPrepared" ErrorMessage="Enter only numbers." ForeColor="Red" ValidationExpression="^\d+$" ValidationGroup='valGroup1'></asp:RegularExpressionValidator>

                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Adjustment">
                <ItemTemplate>
                    <asp:Button ID="btn_Adjustment" runat="server" Text="ADJ" UseSubmitBehavior="false" OnClick="btn_Adjustment_Click" />
                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("item_number") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Button ID="btn_submitCollectionList" runat="server" Text="Submit" OnClick="btn_submitCollectionList_Click" CssClass="btn btn-success" ValidationGroup='valGroup1' />

</asp:Content>

