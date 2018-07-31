<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="Team3ADProject.Protected.Report1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	 <%@ Import Namespace="Team3ADProject.Code" %>
    <h1>Generate Report</h1>
    <div>
        <div>
            Chart to generate:
			<% if (Roles.IsUserInRole(Constants.ROLES_STORE_MANAGER)){%>
            <asp:DropDownList ID="ChartList" runat="server" OnSelectedIndexChanged="ChartList_SelectedIndexChanged">
                <asp:ListItem Value="placeholder">Select an item..</asp:ListItem>
                <!-- <asp:ListItem Value="requisitionOrderDateChart">Requisition Order By Date</asp:ListItem> -->
                <asp:ListItem Value="requisitionQuantityByDepartmentChart">Requisition Item Quantity by Department</asp:ListItem>
                <asp:ListItem Value="purchaseQuantityByItemCategoryBarChart">Stationeries purchased ordered by Item Category</asp:ListItem>
                <asp:ListItem Value="pendingPurchaseOrderCountBySupplierChart">Pending Purchase Orders By Suppliers</asp:ListItem>
            </asp:DropDownList>
			<%}%>
			<%else if (Roles.IsUserInRole(Constants.ROLES_DEPARTMENT_HEAD)){%>
			<asp:DropDownList ID="ChartList_DeptHead" runat="server" OnSelectedIndexChanged="ChartList_SelectedIndexChanged">
                <asp:ListItem Value="placeholder">Select an item..</asp:ListItem>
                <asp:ListItem Value="requisitionOrderStatusChart">Requisition Order Status Percentage</asp:ListItem>
            </asp:DropDownList>
			<%}%>
        </div>
        <div>Start Date</div>
        <input type="text" id="startDate" runat="server" ClientIDMode="static" class="datePicker" disabled value="-"/><asp:RequiredFieldValidator ID="StartDateRequiredValidator" runat="server" ControlToValidate="startDate" ErrorMessage="This field is required!"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="StartDateValidator" runat="server" ControlToValidate="startDate" ErrorMessage="Date must be in the format dd-mm-yyyy" ValidationExpression="[0123][0-9]-[01][0-9]-[0-9]{4}" Enabled="false"></asp:RegularExpressionValidator>
&nbsp;<div>End Date</div>
        <input type="text" id="endDate" runat="server" ClientIDMode="static" class="datePicker" disabled value="-"/>




        <script>
            $(document).ready(function () {
                $(".datePicker").datepicker({
                    dateFormat: 'dd-mm-yy',
                    maxDate: new Date
                });
                
                var chartStartDate = $("#startDate").val();
                var chartEndDate = $("#endDate").val();
            });
        </script>
        <asp:RequiredFieldValidator ID="EndDateRequiredValidator" runat="server" ControlToValidate="endDate" ErrorMessage="This field is required!"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="EndDateValidator" runat="server" ControlToValidate="endDate" ErrorMessage="Date must be in the format dd-mm-yyyy" ValidationExpression="[0123][0-9]-[01][0-9]-[0-9]{4}" Enabled="false"></asp:RegularExpressionValidator>
        <br />
        <asp:Button ID="submitButton" runat="server" Text="Submit" CssClass="btn btn-primary" />

        <div id="message" runat="server"></div>
        
			<% if (Roles.IsUserInRole(Constants.ROLES_STORE_MANAGER)){%>
        <div id="<%=ChartList.SelectedValue%>" class="chart-container"/>
			<%}%>

            <%else if (Roles.IsUserInRole(Constants.ROLES_DEPARTMENT_HEAD)){%>
        <div id="<%=ChartList_DeptHead.SelectedValue%>" class="chart-container"/>
			<%}%>

        </div>


        
    </div>

</asp:Content>
