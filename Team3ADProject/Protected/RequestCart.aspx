<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="RequestCart.aspx.cs" Inherits="Team3ADProject.Protected.RequestCart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Label"/>
        <asp:Button ID="Button1" runat="server" Text="Continue Add Items" OnClick="Button1_Click" CssClass="btn btn-info"/>
    </div>
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="Inventory.category" HeaderText="Category" >
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="Inventory.description" HeaderText="Description" >
                
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="Inventory.unit_of_measurement" HeaderText="Unit Of Measurement">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                
                <asp:TemplateField HeaderText="Decrease">
                    <ItemTemplate>
                        <asp:Button ID="DecreaseQuantity" runat="server" OnClick="DecreaseQuantity_Click" Text="-" BackColor="#FF9966" />
                        <asp:HiddenField ID="HiddenField1" Value='<%# Eval("Inventory.item_number") %>' runat="server" />
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Quantity">
                    <ItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("Quantity") %>' OnTextChanged="TextBox1_TextChanged" BackColor="Gray" ForeColor="Black" ToolTip="enter quantity to order"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="Q" runat="server" ControlToValidate="TextBox2" ErrorMessage="Only numbers allowed"
                            ValidationExpression="(^([0-9]*\d*\d{1}?\d*)$)" Display="Dynamic" ForeColor ="Red"></asp:RegularExpressionValidator>
                        <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Eval("Inventory.item_number") %>'/>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Increase">
                    <ItemTemplate>
                        <asp:Button ID="IncreaseQuantity" runat="server" Text="+" OnClick="IncreaseQuantity_Click" BackColor="#99FF66" />
                        <asp:HiddenField ID="HiddenField4" runat="server" Value='<%# Eval("Inventory.item_number") %>'/>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:TemplateField>
                
                <asp:TemplateField HeaderText="Remove">
                    <ItemTemplate>
                        <asp:Button ID="RemoveItem" runat="server" OnClick="RemoveItem_Click" Text="Remove" BackColor="#FF3300" />
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
