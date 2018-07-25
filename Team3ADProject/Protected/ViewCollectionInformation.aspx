<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="ViewCollectionInformation.aspx.cs" Inherits="Team3ADProject.Protected.ViewCollectionInformation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2>View Collection List</h2>

    <asp:GridView ID="gridview1" runat="server" AutoGenerateSelectButton="true" AutoGenerateColumns="false" OnSelectedIndexChanging="gridview1_SelectedIndexChanging" CssClass="table table-hover">

        <Columns>
            <asp:BoundField DataField="employee_name" ReadOnly="true" HeaderText="Employee Name" SortExpression="employee_name"/>
            <asp:BoundField DataField="department_name" ReadOnly="true" HeaderText="Department Name" SortExpression="department_name"/>
            <asp:BoundField DataField="collection_id" ReadOnly="true" HeaderText="Collection" SortExpression="collection_id" />
            <asp:BoundField DataField="collection_place" ReadOnly="true" HeaderText="Collection Place" SortExpression="collection_place"/>
            <asp:BoundField DataField="collection_date" ReadOnly="true" HeaderText="Collection Date" SortExpression="collection_date" DataFormatString="{0:dd-MM-yyyy}"/>            
            <asp:BoundField DataField="collection_time" ReadOnly="true" HeaderText="Collection Time" SortExpression="collection_time"/>
            
        </Columns>

    </asp:GridView>
</asp:Content>