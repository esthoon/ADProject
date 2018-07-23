<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditRequest.aspx.cs" Inherits="Team3ADProject.Protected.EditRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Edit Requisition Order</h1>
    <div> <asp:Button ID="Button1" runat="server" Text="<< Back to History" OnClick="Button1_Click" CssClass="btn btn-default"/> </div>
    <div>

<div>
    <asp:Label ID="Label1" runat="server" Text="Requisition Number :" /><asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
    <br/>
    <asp:Label ID="Label2" runat="server" Text="Requisition Date :" /><asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
    <br/>
    <asp:Label ID="Label3" runat="server" Text="Requisition Status :" /><asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
    <br/>
</div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-hover">
        <Columns>
            <asp:BoundField DataField="category" HeaderText="Category" ReadOnly="true" >
            <HeaderStyle VerticalAlign="Middle" />
            <ItemStyle VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="description" HeaderText="Description" ReadOnly="true" >
            <HeaderStyle VerticalAlign="Middle" />
            <ItemStyle VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="unit_of_measurement" HeaderText="Unit Of Measurement" ReadOnly="true">
            <HeaderStyle VerticalAlign="Middle" />
            <ItemStyle VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="-">
                <ItemTemplate>
                    <asp:Button ID="DecQuantity" runat="server" Text="-" OnClick="DecQuantity_Click" UseSubmitBehavior="false" CssClass="btn btn-warning" />
                    <asp:HiddenField ID="HiddenFieldDecQ" runat="server" Value='<%# Eval("description") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Request Quantity">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("item_requisition_quantity") %>' OnTextChanged="TextBox1_TextChanged" text-align="right" ForeColor="Black"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="Q" runat="server" ControlToValidate="TextBox2" ErrorMessage="Only numbers allowed"
                            ValidationExpression="(^([0-9]*\d*\d{1}?\d*)$)" Display="Dynamic" ForeColor ="Red"></asp:RegularExpressionValidator>
                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("description") %>'/>
                </ItemTemplate>
                <HeaderStyle VerticalAlign="Middle" />
                <ItemStyle VerticalAlign="Middle" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="+">
                <ItemTemplate>
                    <asp:HiddenField ID="HiddenFieldIncQ" runat="server" Value='<%# Eval("description") %>' />
                    <asp:Button ID="IncQuan" runat="server" Text="+" UseSubmitBehavior="false" OnClick="IncQuan_Click" CssClass="btn btn-success"/>
                </ItemTemplate>
                <HeaderStyle VerticalAlign="Middle" />
                <ItemStyle VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Remove">
                <ItemTemplate> 
                    <asp:Button ID="Remove" runat="server" Text="Remove" UseSubmitBehavior="false" OnClick="Remove_Click" CssClass="btn btn-danger"/>
                    <asp:HiddenField ID="HiddenFieldRemove" runat="server" Value='<%# Eval("description") %>' />
                </ItemTemplate>
                <HeaderStyle VerticalAlign="Middle" />
                <ItemStyle VerticalAlign="Middle" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    <div>
        <asp:Button ID="Button2" runat="server" Text="Cancel Request" CssClass="btn btn-danger" OnClick="Button2_Click" />
        <asp:Button ID="Button3" runat="server" Text="Update Request" CssClass="btn btn-primary" OnClick="Button3_Click"/>
    </div>
</asp:Content>
