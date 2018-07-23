<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="Team3ADProject.Protected.NewRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>New Requisition Order</h1>
        <asp:Label ID="Label1" runat="server" Text="Search"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" CssClass="btn btn-primary" />
    </div>
    <br />
    <div>
        <asp:GridView ID="GridView1" runat="server" GridLines="None" AllowPaging="True" PageSize="15" OnPageIndexChanging="GridView1_PageIndexChanging" AutoGenerateColumns="False" CssClass="table table-hover table-bordered">
            <Columns>
                <asp:BoundField DataField="category" HeaderText="Category">
                    <HeaderStyle VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="description" HeaderText="Description">
                    <HeaderStyle VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="unit_of_measurement" HeaderText="UOM">
                    <HeaderStyle VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:TemplateField HeaderText="Add Button">
                    <ItemTemplate>
                        <asp:Button ID="AddButton" runat="server" Text="Add Item" OnClick="Button2_Click" CssClass="btn btn-success" />
                        <asp:HiddenField ID="HiddenFieldItemNumber" runat="server" Value='<%# Eval("item_number") %>' />
                    </ItemTemplate>
                    <HeaderStyle VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <div>
        <asp:Button ID="Button2" runat="server" Text="View Request Detail" CssClass="btn btn-info" OnClick="Button2_Click1" />
    </div>
</asp:Content>
