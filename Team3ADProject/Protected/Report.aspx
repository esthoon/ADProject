<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="Team3ADProject.Protected.Report1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Generate Report</h1>
    <div>
        <div>
            Chart to generate:
            <asp:DropDownList ID="ChartList" runat="server" OnSelectedIndexChanged="ChartList_SelectedIndexChanged">
                <asp:ListItem Value="placeholder">Select an item..</asp:ListItem>
                <asp:ListItem Value="requisitionOrderStatusChart">Requisition Order Status Percentage</asp:ListItem>
                <asp:ListItem Value="requisitionOrderDateChart">Requisition Order By Date</asp:ListItem>
                <asp:ListItem Value="requisitionQuantityByDepartmentChart">Requisition Item Quantity by Department</asp:ListItem>
                <asp:ListItem Value="purchaseQuantityByItemCategoryBarChart">Stationaries purchased ordered by Item Category</asp:ListItem>
                <asp:ListItem Value="pendingPurchaseOrderCountBySupplierChart">Pending Purchase Orders By Suppliers</asp:ListItem>

            </asp:DropDownList>
        </div>
        <div>Start Date</div>
        <input type="text" id="startDate" runat="server" ClientIDMode="static" class="datePicker" disabled value="-"/>

        <div>End Date</div>
        <input type="text" id="endDate" runat="server" ClientIDMode="static" class="datePicker" disabled value="-"/>

        <script>
            $(document).ready(function () {
                $(".datePicker").datepicker({
                    dateFormat: 'dd-mm-yy'
                });
                
                var chartStartDate = $("#startDate").val();
                var chartEndDate = $("#endDate").val();
            });
        </script>
        <br />
        <asp:Button ID="submitButton" runat="server" Text="Submit" CssClass="btn btn-primary" />

        <div id="message" runat="server"></div>

        <div id="<%=ChartList.SelectedValue%>" class="chart-container">

        </div>


        
    </div>

</asp:Content>
