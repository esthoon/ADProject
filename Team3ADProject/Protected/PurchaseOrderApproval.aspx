<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PurchaseOrderApproval.aspx.cs" Inherits="Team3ADProject.Protected.PurchaseOrderApproval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <br/>
    <br/>
    
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
        
        <Columns>
            <asp:BoundField  DataField="purchase_order_number" HeaderText="PO number" />
            <asp:BoundField  DataField="purchase_order_required_date" HeaderText="Order Required Date" DataFormatString="{0:MM/dd/yyyy}"/>
            <asp:BoundField DataField="purchase_order_date" HeaderText="PO Date" DataFormatString="{0:MM/dd/yyyy}"/>
            <asp:BoundField DataField="suppler_id" HeaderText="Supplier ID"/>
            <asp:BoundField DataField="employee_id" HeaderText="Employee ID"/>
            <asp:BoundField DataField="purchase_order_status" HeaderText="PO status" />
        </Columns>
    </asp:GridView>
</asp:Content>
