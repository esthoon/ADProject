<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ViewROSpecialRequest.aspx.cs" Inherits="Team3ADProject.Protected.ViewROSpecialRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<h1>Special Requests</h1>
    <br />
    <br />

    <div class="col-md-12">
        <span>Search by Requisition Order ID:</span>
        <asp:TextBox ID="txt_searchByRO" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:Button ID="btn_SortingSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btn_SortingSearch_Click" />
    </div>

    <br />
    <br />
    <div>
        <h4>Requisition Order ID:</h4>
        <asp:Label ID="Label_ViewRO" runat="server" Text=""></asp:Label>
        <asp:GridView ID="gv_ViewRO" runat="server" AutoGenerateColumns="False" CssClass="table table-hover align-center" EmptyDataText="The Requisition Order you entered was not found ">
            <Columns>
                <asp:BoundField DataField="item_number" HeaderText="Item Number" ReadOnly="True" SortExpression="item_number" />
                <asp:BoundField DataField="description" HeaderText="Description" ReadOnly="True" SortExpression="description" />
                <asp:BoundField DataField="unit_of_measurement" HeaderText="UOM" ReadOnly="True" SortExpression="unit_of_measurement" />
                <asp:BoundField DataField="item_pending_quantity" HeaderText="Qty Ordered" ReadOnly="True" SortExpression="item_pending_quantity" />

                <asp:BoundField DataField="current_quantity" HeaderText="Qty Available" ReadOnly="True" SortExpression="current_quantity" />

                <asp:TemplateField HeaderText="Qty Prepared">
                    <ItemTemplate>
                        <asp:TextBox ID="txt_QtyPrepared" runat="server" Text='<%# (Convert.ToInt32(Eval("item_pending_quantity")) <= Convert.ToInt32(Eval("current_quantity")) ? Eval("item_pending_quantity") : Eval("current_quantity")) %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Adjustment">
                    <ItemTemplate>
                        <input type="button" name="btn_Adjustment" value="Adjust">
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
        <asp:TextBox ID="TextBox_Collect_Date" runat="server"></asp:TextBox>
        <asp:Calendar ID="Calendar_Collect_Date" runat="server" OnSelectionChanged="Calendar_Collect_Date_SelectionChanged" OnDayRender="Calendar_Collect_Date_DayRender"></asp:Calendar>

        <asp:Button ID="btn_readyForCollect" runat="server" Text="Ready for Collection" OnClick="btn_readyForCollect_Click" />
    </div>
</asp:Content>
