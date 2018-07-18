<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Report.aspx.cs" Inherits="Team3ADProject.Protected.Report1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Generate Report</h1>
    <div>
        <div>
        Chart to generate: <asp:DropDownList ID="DropDownList1" runat="server">
            <asp:ListItem Value="placeholder">Select an item..</asp:ListItem>
            <asp:ListItem Value="requisitionOrderStatusChart">Requisition Order Status Percentage</asp:ListItem>
            <asp:ListItem Value="testChart">Test Chart</asp:ListItem>
            <asp:ListItem Value="requisitionOrderDateChart">Requisition Order By Date</asp:ListItem>
            <asp:ListItem Value="purchaseQuantityByItemQuantityBarChart">Stationaries purchased ordered by Item Quantity</asp:ListItem>
            <asp:ListItem Value="requisitionQuantityByDepartmentChart">Requisition Item Quantity by Department</asp:ListItem>

            
        </asp:DropDownList>
        </div>

        <div>
        <%if (DropDownList1.SelectedValue == "purchaseQuantityByItemQuantityBarChart") { %>
            Months ago: <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>       
        <% }%>
        </div>
        
        <asp:Button ID="submitButton" runat="server" Text="Submit"/>

    </div>

    <div class="container">
      <div id="<%=DropDownList1.SelectedValue%>" 

        <%if (DropDownList1.SelectedValue == "purchaseQuantityByItemQuantityBarChart") {%> 
            monthsParam="<%=TextBox1.Text%>"
        <% }%>>

      </div>
      <div id="chartMessage"></div>
    </div>
    
</asp:Content>
