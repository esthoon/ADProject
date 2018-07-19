<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Team3ADProject.Protected.Dashboard1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link rel="stylesheet" href="<%=ResolveUrl("~/Content/Sites/Dashboard.css")%>" />

    <!-- If user is a store clerk, display dashboard information -->
    <%
        if (System.Web.Security.Roles.Enabled)
        {
            if (Roles.GetRolesForUser(HttpContext.Current.User.Identity.Name).Contains(Team3ADProject.Code.Constants.ROLES_STORE_CLERK))
            {  %>

    <div class="dashboard-flexbox-container-outer">
        <!--Flex item 1: Table for low stock items -->
        <div class="dashboard-flexbox-item">
            <h3>Low stock items:</h3>
            <asp:GridView ID="LowStockItemGridView" runat="server" CssClass="table table-condensed" AutoGenerateColumns="False">
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
            <div id="pendingPurchaseOrderCountBySupplierChart"></div>
        </div>

        <div class="dashboard-flexbox-item">
            <div id="testChart"></div>
        </div>
    </div>

    <!-- Row 2-->
    <div class="dashboard-flexbox-container">
        <!--Flex item 3: Table for low stock items -->
        <div class="dashboard-flexbox-item">
            <div id="purchaseQuantityByItemQuantityBarChart"></div>
        </div>
    </div>
    <%} %>

    <% else
        {  %>
    <!-- If user is something else, provide shortcut panels -->

    <div class="dashboard-flexbox-container-outer">

        <!--Flex container row 1: Table for low stock items -->
        <div class="dashboard-flexbox-container-item">
            <div class="dashboard-flexbox-item">
                aaa
            </div>

            <div class="dashboard-flexbox-item">
                bbb
            </div>
        </div>

        <!--Flex container row 2 -->
        <div class="dashboard-flexbox-container-item">
            <div class="dashboard-flexbox-item">
                ccc
            </div>

            <div class="dashboard-flexbox-item">
                ddd
            </div>
        </div>
    </div>
    <%}
        }      %>
</asp:Content>
