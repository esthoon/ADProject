<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="CollectionList.aspx.cs" Inherits="Team3ADProject.Protected.CollectionList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Warehouse Collection List</h1>

    <asp:Label ID="Label2" runat="server" Text="Note: Only ROs raised and approved the previous working day will be reflected here."></asp:Label>

    <asp:GridView ID="gv_CollectionList" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="10" OnPageIndexChanging="gv_CollectionList_PageIndexChanging" CssClass="table table-hover align-center" EmptyDataText="There are no new items to collect">
        <Columns>
            <asp:BoundField DataField="item_number" HeaderText="Item Number" ReadOnly="True" SortExpression="item_number" />
            <asp:BoundField DataField="description" HeaderText="Description" ReadOnly="True" SortExpression="description" />
            <asp:BoundField DataField="unit_of_measurement" HeaderText="UOM" ReadOnly="True" SortExpression="unit_of_measurement" />
            <asp:BoundField DataField="quantity_ordered" HeaderText="Ordered Qty" ReadOnly="True" SortExpression="quantity_ordered" />
            <asp:BoundField DataField="current_quantity" HeaderText="Current Qty" ReadOnly="True" SortExpression="current_quantity" />

            <asp:TemplateField HeaderText="Collect Qty">
                <ItemTemplate>
                    <asp:TextBox ID="txt_QtyPrepared" runat="server" Text='<%# (Convert.ToInt32(Eval("quantity_ordered")) <= Convert.ToInt32(Eval("current_quantity")) ? Eval("quantity_ordered") : Eval("current_quantity")) %>'></asp:TextBox>

                    <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>

                    <%--<asp:CompareValidator ID="CompareValidator_txt_QtyPrepared" runat="server"
                        ControlToValidate="txt_QtyPrepared" ErrorMessage="Can't be -ve number."
                        Operator="GreaterThanEqual" Type="Integer" ForeColor="Red"
                        ValueToCompare="0" ValidationGroup='valGroup1' />--%>
                    <%--					<asp:CompareValidator runat="server" id="CompareValidator_txt_QtyPrepared" controltovalidate="txt_QtyPrepared" valuetocompare='<%# Eval("quantity_ordered") %>' ValidationGroup = 'valGroup1' operator="LessThanEqual" type="Integer" errormessage="Prepared quantity cannot be greater than quantity order" ForeColor="Red"/>--%>

                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_QtyPrepared" ID="RequiredValidator_txt_QtyPrepared" ErrorMessage="Enter a number." ForeColor="Red" ValidationGroup='valGroup1' />
                    </br>
					<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txt_QtyPrepared" ErrorMessage="Positive Quantity should be entered" ForeColor="Red" Type="Integer" MinimumValue="0" MaximumValue="100000" ValidationGroup='valGroup1'></asp:RangeValidator>
                    <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator_txt_QtyPrepared" runat="server" ControlToValidate="txt_QtyPrepared" ErrorMessage="Enter only numbers." ForeColor="Red" ValidationExpression="^\d+$" ValidationGroup='valGroup1'></asp:RegularExpressionValidator>--%>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Adjustment">
                <ItemTemplate>
                    <asp:Button ID="btn_Adjustment" runat="server" Text="ADJ" UseSubmitBehavior="false" OnClientClick='<%# String.Format("javascript:return openAdjForm(\"{0}\")", Eval("item_number").ToString()) %>' CssClass="btn btn-info" />
                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("item_number") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Button ID="btn_submitCollectionList" runat="server" Text="Submit" OnClick="btn_submitCollectionList_Click" CssClass="btn btn-success" ValidationGroup='valGroup1' />

    <script type='text/javascript'> function openAdjForm(url){window.open('AdjustmentForm1.aspx?itemcode='+url);}
    </script>
</asp:Content>

