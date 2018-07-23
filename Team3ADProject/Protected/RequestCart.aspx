<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="RequestCart.aspx.cs" Inherits="Team3ADProject.Protected.RequestCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Request Cart</h1>
    <div>
        <asp:Label ID="Label1" runat="server" Text="Label"/>
        <asp:Button ID="Button1" runat="server" Text="Add more items" OnClick="Button1_Click" CssClass="btn btn-info"/>
    </div>
    <br/>
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-hover">
            <Columns>
                <asp:BoundField DataField="Inventory.category" HeaderText="Category" >
                <HeaderStyle VerticalAlign="Middle" />
                <ItemStyle VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="Inventory.description" HeaderText="Description" >
                
                <HeaderStyle VerticalAlign="Middle" />
                <ItemStyle VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="Inventory.unit_of_measurement" HeaderText="Unit Of Measurement">
                <HeaderStyle VerticalAlign="Middle" />
                <ItemStyle VerticalAlign="Middle" />
                </asp:BoundField>
                
                <asp:TemplateField HeaderText="Decrease">
                    <ItemTemplate>
                        <asp:Button ID="DecreaseQuantity" runat="server" OnClick="DecreaseQuantity_Click" Text="-" CssClass="btn btn-warning" />
                        <asp:HiddenField ID="HiddenField1" Value='<%# Eval("Inventory.item_number") %>' runat="server" />
                    </ItemTemplate>
                    <HeaderStyle VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Quantity") %>' OnTextChanged="TextBox1_TextChanged" ToolTip="Enter quantity to order"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="Q" runat="server" ControlToValidate="TextBox2" ErrorMessage="Only numbers allowed"
                            ValidationExpression="(^([0-9]*\d*\d{1}?\d*)$)" Display="Dynamic" ForeColor ="Red"></asp:RegularExpressionValidator>
                        <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Eval("Inventory.item_number") %>'/>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Increase">
                    <ItemTemplate>
                        <asp:Button ID="IncreaseQuantity" runat="server" Text="+" OnClick="IncreaseQuantity_Click" CssClass="btn btn-success"/>
                        <asp:HiddenField ID="HiddenField4" runat="server" Value='<%# Eval("Inventory.item_number") %>'/>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Remove">
                    <ItemTemplate>
                        <asp:Button ID="RemoveItem" runat="server" OnClick="RemoveItem_Click" Text="Remove" CssClass="btn btn-danger"/>
                        <asp:HiddenField ID="HiddenField3" Value='<%# Eval("Inventory.item_number") %>' runat="server" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                
            </Columns>           
        </asp:GridView>
    </div>
    <asp:Button ID="Button2" runat="server" Text="Confirm Request" OnClick="Button2_Click1" CssClass="btn btn-success" />

</asp:Content>
