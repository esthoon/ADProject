<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="RequestConfirm.aspx.cs" Inherits="Team3ADProject.Protected.RequestConfirm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Request Confirmation</h1>
    <asp:Button ID="Button1" runat="server" Text="Add New request" CssClass="btn btn-info" OnClick="Button1_Click"/>
    <br />
    <div>
         <asp:Label ID="Label1" runat="server" Text="Requisition Number :" /><asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
    <br/>
    <asp:Label ID="Label2" runat="server" Text="Requisition Date :" /><asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
    <br/>
    <asp:Label ID="Label3" runat="server" Text="Requisition Status :" /><asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
    <br/>
    </div>
    <br />
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-hover">
            <Columns>
                <asp:BoundField DataField="category" HeaderText="Category" >
                <HeaderStyle VerticalAlign="Middle" />
                <ItemStyle VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="description" HeaderText="Description" >
                <HeaderStyle VerticalAlign="Middle" />
                <ItemStyle VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="unit_of_measurement" HeaderText="Unit Of Measurement">
                <HeaderStyle VerticalAlign="Middle" />
                <ItemStyle VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="item_requisition_quantity" HeaderText="Quantity" >
                <HeaderStyle VerticalAlign="Middle" />
                <ItemStyle VerticalAlign="Middle" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
