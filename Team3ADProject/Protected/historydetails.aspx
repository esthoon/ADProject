<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="historydetails.aspx.cs" Inherits="Team3ADProject.Protected.historydetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">

    <body>

        <h2>Requisition Order Details</h2>

        <a href="<%=ResolveUrl("~/Protected/depheadviewhistory")%>" class="btn btn-default">&lt;&lt; Requisition Order History</a>
        <br />
        <asp:Label ID="Label2" runat="server" Text="Requisition Number: "></asp:Label>
        <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
        <br />
            <asp:Label ID="Label3" runat="server" Text="Date Requisition order was raised: "></asp:Label>
            <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
        <br />
            <asp:Label ID="Label4" runat="server" Text="Requisition Status: "></asp:Label>
            <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label>
        <br />
            <asp:Label ID="Label7" runat="server" Text="Employee: "></asp:Label>
            <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label>
        <br />
            <asp:Label ID="Label6" runat="server" Text="Request Price: "></asp:Label>
            <asp:Label ID="Label12" runat="server" Text="Label"></asp:Label>
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" CssClass="table table-hover">
            <Columns>
                <asp:BoundField DataField="category" HeaderText="Category" />
                <asp:BoundField DataField="description" HeaderText="Description" />
                <asp:BoundField DataField="item_requisition_quantity" HeaderText="Quantity Requested" />
            </Columns>
        </asp:GridView>

    </body>
    </html>

</asp:Content>
