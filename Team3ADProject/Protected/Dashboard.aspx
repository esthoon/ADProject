<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Team3ADProject.Protected.Dashboard1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link rel="stylesheet" href="<%=ResolveUrl("~/Content/Sites/Dashboard.css")%>" />


    <h1>Dashboard</h1>

    <!-- If user is a store clerk, display dashboard information -->
    <div class="dashboard-flexbox-container-outer">
        <!--Flex item 1: Table for low stock items -->
        <div class="dashboard-flexbox-item">
            <h3>Low stock items:</h3>
            <asp:GridView ID="LowStockItemGridView" runat="server" CssClass="table table-hover" AutoGenerateColumns="False">
                <Columns>
                    <asp:BoundField DataField="item_number" HeaderText="Item ID" />
                    <asp:BoundField DataField="description" HeaderText="Description" />
                    <asp:BoundField DataField="current_quantity" HeaderText="Current Quantity" />
                    <asp:BoundField DataField="reorder_level" HeaderText="Reorder Level" />
                </Columns>
            </asp:GridView>

            <asp:LinkButton ID="RequisitionOrder_Link" runat="server" CssClass="btn btn-success" OnClick="RequisitionOrder_Link_Click">Go to Inventory Listing</asp:LinkButton>
        </div>

        <!--Flex item 2: Chart -->
        <div class="dashboard-flexbox-item">
            <h3>Pending purchase orders by suppliers:</h3>
            <div id="pendingPurchaseOrderCountBySupplierChart" style="height: 60vh; width: 30vw; margin-bottom: 2.5vh"></div>
            <asp:LinkButton ID="PurchaseOrder_Link" runat="server" CssClass="btn btn-success" OnClick="PurchaseOrder_Link_Click">Go to Purchase Order Listing</asp:LinkButton>

        </div>

    </div>
    <%-- 
    <!-- If user is an employee, show them pointers-->
    <div class="flexbox-column dashboard-flexbox-user">
        <h1>Welcome!</h1>
        <div>Please start through the navigation bar above</div>
    </div>
    --%>
</asp:Content>
