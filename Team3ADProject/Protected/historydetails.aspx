<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="historydetails.aspx.cs" Inherits="Team3ADProject.Protected.historydetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <html xmlns="http://www.w3.org/1999/xhtml">

    <body>


         <%@ Import Namespace="Team3ADProject.Code" %>

        <h2>Requisition Order Details</h2>

        <a href="
            <%if (Roles.IsUserInRole(Constants.ROLES_DEPARTMENT_HEAD)){ %>
            <%=ResolveUrl("~/Protected/depheadviewhistory")%>
            <%} %>
            
            <%else{ %>
            <%=ResolveUrl("~/Protected/EmployeeViewHistory")%>
            <%} %>
            " class="btn btn-default">&lt;&lt; Requisition Order History</a>
        <br />
        <br />
        <br />
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Requisition Number: "></asp:Label>
                </td>
                <td>&nbsp;&nbsp;
                    <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Requisition order Date: "></asp:Label>
                </td>
                <td>&nbsp;&nbsp;
                    <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Requisition Status: "></asp:Label>
                </td>
                <td>&nbsp;&nbsp;
                    <asp:Label ID="Label10" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            
            <tr>
                <td>
                    <asp:Label ID="Label7" runat="server" Text="Employee: "></asp:Label>
                </td>
                <td>&nbsp;&nbsp;
                    <asp:Label ID="Label11" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
     
            
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Request Price: "></asp:Label>
                </td>
                <td>&nbsp;&nbsp;
                    <asp:Label ID="Label12" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
        </table>
        
    
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
