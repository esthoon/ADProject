<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AcknowledgeDistributionList.aspx.cs" Inherits="Team3ADProject.Protected.AcknowledgeDistributionList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Acknowledge Distribution List</h1>
    <table>
        <tr>
            <th>
                Department Name : 
            </th>
            <td>
                <asp:Label ID="DepartmentNameLabel" Text="Department Name" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                Department Representative :
            </th>
            <td>
                <asp:Label ID="DepartmentRepresentativeLabel" Text="Department Representative" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                Date : 
            </th>
            <td>
                <asp:Label ID="DateLabel" Text="Date " runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                Time :
            </th>
            <td>
                <asp:Label ID="TimeLabel" Text="Time" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <th>
                Location :
            </th>
            <td>
                <asp:Label ID="LocationLabel" Text="Location" runat="server"></asp:Label>
            </td>
        </tr>
         
    </table>
        <br /><br /><br />
    Enter Department Pin :
    <asp:TextBox ID="PinTextBox" runat="server" MaxLength="10"></asp:TextBox>
    <asp:Button ID="VerifyPasswordButton" runat="server" Text="Verify Pin" OnClick="VerifyPasswordButtonClick" CssClass="btn btn-default"/>
    <br /><br /><br />
        
        <asp:GridView ID="gridview1" runat="server"  AutoGenerateColumns="false" CssClass="table table-hover" >
                <Columns>
                    <asp:BoundField DataField="item_number" ReadOnly="true" HeaderText="Item Number" SortExpression="item_number"/>
                    <asp:BoundField DataField="description" ReadOnly="true" HeaderText="Description" SortExpression="description"/>
                    <asp:BoundField DataField="ordered_quantity" ReadOnly="true" HeaderText="Ordered Qty" SortExpression="item_requisition_quantity"/>
                   
                    <asp:TemplateField HeaderText="Collected Qty">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="false" Text='<%#Eval("supply_quantity") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>

            </asp:GridView>   
    <br /><br />
    <asp:Button ID="AcknowledgeButton" runat="server" Text="Acknowledge" Enabled="false" OnClick="AcknowledgeButton_Click" CssClass="btn btn-success"/>



</asp:Content>