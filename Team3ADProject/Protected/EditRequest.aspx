<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EditRequest.aspx.cs" Inherits="Team3ADProject.Protected.EditRequest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div> <asp:Button ID="Button1" runat="server" Text="Back to History" OnClick="Button1_Click" CssClass="btn btn-info"/> </div>
    <div>

<div>
    <asp:Label ID="Label1" runat="server" Text="Requisition Number :" /><asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
    <br/>
    <asp:Label ID="Label2" runat="server" Text="Requisition Date :" /><asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
    <br/>
    <asp:Label ID="Label3" runat="server" Text="Requisition Status :" /><asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
    <br/>
</div>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
        <Columns>
            <asp:BoundField DataField="category" HeaderText="Category" ReadOnly="true" >
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="description" HeaderText="Description" ReadOnly="true" >
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="unit_of_measurement" HeaderText="Unit Of Measurement" ReadOnly="true">
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="-">
                <ItemTemplate>
                    <asp:Button ID="DecQuantity" runat="server" Text="-" OnClick="DecQuantity_Click" UseSubmitBehavior="false" BackColor="#FF9966" />
                    <asp:HiddenField ID="HiddenFieldDecQ" runat="server" Value='<%# Eval("description") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Request Quantity">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("item_requisition_quantity") %>' OnTextChanged="TextBox1_TextChanged" BackColor="#FFFFCC" text-align="right" ForeColor="Black"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="Q" runat="server" ControlToValidate="TextBox2" ErrorMessage="Only numbers allowed"
                            ValidationExpression="(^([0-9]*\d*\d{1}?\d*)$)" Display="Dynamic" ForeColor ="Red"></asp:RegularExpressionValidator>
                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("description") %>'/>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="+">
                <ItemTemplate>
                    <asp:HiddenField ID="HiddenFieldIncQ" runat="server" Value='<%# Eval("description") %>' />
                    <asp:Button ID="IncQuan" runat="server" Text="+" UseSubmitBehavior="false" OnClick="IncQuan_Click" BackColor="#99FF66"/>
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Remove">
                <ItemTemplate> 
                    <asp:Button ID="Remove" runat="server" Text="Remove" UseSubmitBehavior="false" OnClick="Remove_Click" BackColor="#FF3300"/>
                    <asp:HiddenField ID="HiddenFieldRemove" runat="server" Value='<%# Eval("description") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
    </div>
    <div>
        <asp:Button ID="Button2" runat="server" Text="Cancel Request" CssClass="btn btn-danger" OnClick="Button2_Click" />
        <asp:Button ID="Button3" runat="server" Text="Update Request" CssClass="btn btn-success" OnClick="Button3_Click"/>
    </div>
</asp:Content>
