<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PurchaseOrderApproval.aspx.cs" Inherits="Team3ADProject.Protected.PurchaseOrderApproval" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <h1>Purchase Order Approval</h1>
    <h4>Summary</h4>
    <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
   
    
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowUpdating="GridView1_RowUpdating" Height="145px" Width="1000px" CssClass="table table-hover" >
       
        <Columns>
            <asp:BoundField DataField="purchase_order_number" HeaderText="PO No." />
            <asp:BoundField DataField="supplier_name" HeaderText="Supplier Name" />
            <asp:BoundField DataField="purchase_order_date" HeaderText="PO Date" DataFormatString="{0:MM/dd/yyyy}"/>
            <asp:BoundField DataField="employee_name" HeaderText="Employee Name" />
            <asp:BoundField DataField="total_price" HeaderText="Total Price" DataFormatString="{0:c2}" />

            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="View" CssClass="btn btn-info"></asp:LinkButton>
                   
                </ItemTemplate>
                
            </asp:TemplateField>
           
        </Columns>
    </asp:GridView>
</asp:Content>
