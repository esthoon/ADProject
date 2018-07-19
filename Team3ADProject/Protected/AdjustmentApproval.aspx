<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AdjustmentApproval.aspx.cs" Inherits="Team3ADProject.Protected.AdjustmentApproval" %>
<%@ Import Namespace="Team3ADProject.Protected" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">   
    
   

    <h1>Adjustment Form Approval</h1>
    
    
    
    
    <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged"></asp:Calendar> 
    
    
    <br/>
    <br/>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Height="209px" Width="1014px" OnRowEditing="GridView1_RowEditing"  OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnPageIndexChanging="GridView1_PageIndexChanging" AllowPaging="True" PageSize="5" >
        <Columns>
            
            <asp:TemplateField>
                 
                <HeaderTemplate>
                    <asp:CheckBox ID="chkSelectAll" runat="server" CssClass="chkHeader" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelect"  runat="server" CssClass="chkItem" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="adjustment_id" HeaderText="Adj ID" InsertVisible="False" ReadOnly="True" SortExpression="adjustment_id" />
            <asp:BoundField DataField="adjustment_date" HeaderText="Adj date" SortExpression="adjustment_date" DataFormatString="{0:MM/dd/yyyy}" />
            <asp:BoundField DataField="employee_id" HeaderText="Employee Id" SortExpression="employee_id" />
            <asp:BoundField DataField="item_number" HeaderText="Item No." SortExpression="item_number" />
            <asp:BoundField DataField="adjustment_quantity" HeaderText="Adj Qty" SortExpression="adjustment_quantity" />
            <asp:BoundField DataField="adjustment_price" HeaderText="Adj Price" SortExpression="adjustment_price" />
            <asp:BoundField DataField="adjustment_status" HeaderText="Adj Status" SortExpression="adjustment_status" />
            <asp:BoundField DataField="employee_remark" HeaderText="Employee Remark" SortExpression="employee_remark" />
            <asp:TemplateField HeaderText="Manager Remark" SortExpression="manager_remark">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("manager_remark") %>'></asp:TextBox>
                </ItemTemplate>
                <%--<ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("manager_remark") %>'></asp:Label>
                </ItemTemplate>--%>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="True">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Approve" CommandArgument='<%# Eval("adjustment_id") %>'></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Reject"></asp:LinkButton>
                </ItemTemplate>
                <%--<ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                </ItemTemplate>--%>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    <br/>
    <asp:LinkButton ID="LinkButton1"  runat="server" OnClick="LinkButton1_Click">Approve</asp:LinkButton>
   <br/>
    <asp:LinkButton ID="LinkButton3"  runat="server" OnClick="LinkButton3_Click">Reject</asp:LinkButton>

</asp:Content>


