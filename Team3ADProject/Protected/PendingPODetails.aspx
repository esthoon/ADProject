<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="PendingPODetails.aspx.cs" Inherits="Team3ADProject.Protected.PendingPODetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Text="Purchase Order Details" Font-Bold="True" Font-Size="XX-Large" />
        <br />
        <div>
            <asp:Button ID="Button1" runat="server" Text="<< View Purchase Order History" OnClick="Button1_Click" CssClass="btn btn-default" />
        </div>
    </div>
    <br />
    <br />
    <div>
        <asp:Label ID="Label2" runat="server" Text="Purchase Order Number :" Font-Bold="True" />
        <asp:Label ID="Label5" runat="server" Text="Label" Font-Italic="True" />
    </div>
    <div>
        <asp:Label ID="Label8" runat="server" Text="Purchase Order Date (dd-MM-yyyy) :" Font-Bold="True" />
        <asp:Label ID="Label9" runat="server" Text="Label" Font-Italic="True" />
    </div>
    <div>
        <asp:Label ID="Label12" runat="server" Text="Purchase Order Placed By :" Font-Bold="True" />
        <asp:Label ID="Label13" runat="server" Text="Label" Font-Italic="True" />
    </div>
    <div>
        <asp:Label ID="Label10" runat="server" Text="Purchase Order Status :" Font-Bold="True" />
        <asp:Label ID="Label11" runat="server" Text="Label" Font-Italic="True" />
    </div>
    <br />
    <div>
        <asp:Label ID="Label3" runat="server" Text="Supplier Name :" Font-Bold="True" />
        <asp:Label ID="Label4" runat="server" Text="Label" Font-Italic="True" />
    </div>
    <div>
        <asp:Label ID="Label6" runat="server" Text="Supplier ID :" Font-Bold="True" />
        <asp:Label ID="Label7" runat="server" Text="Label" Font-Italic="True" />
    </div>
    <br />
    <asp:Label ID="Label14" runat="server" Text="Label" ForeColor="Red"></asp:Label>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound" CssClass="table table-hover">
        <Columns>
            <asp:BoundField DataField="item_number" HeaderText="Item Code">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="item_purchase_order_quantity" HeaderText="PO Quantity">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="item_purchase_order_price" HeaderText="PO Price">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="item_required_date" HeaderText="Item Required Date" DataFormatString="{0:dd-MM-yyyy}">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:BoundField DataField="item_purchase_order_status" HeaderText="Item Receipt Status">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="PO Received Quantity">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" BackColor="White" Text='<%# Bind("item_accept_quantity") %>'></asp:TextBox>
                    <%--                    <asp:RegularExpressionValidator ID="Q" runat="server" ControlToValidate="TextBox1" ErrorMessage="Please enter quantity"
                        ValidationExpression="(^([1-9]*\d*\d{1}?\d*)$)" Display="Dynamic" ForeColor="Red"></asp:RegularExpressionValidator>--%>
                   <asp:CompareValidator runat="server" id="cmpNumbers" controltovalidate="TextBox1" valuetocompare='<%# Eval("item_purchase_order_quantity") %>' ValidationGroup = '<%# "Group_" + Container.DataItemIndex %>' operator="LessThanEqual" type="Integer" errormessage="Received quantity cannot be greater than PO quantity" ForeColor="Red" />
                  <%--  <asp:CompareValidator ID="CompareValidator1" runat="server" controltovalidate="TextBox1" valuetocompare="0" operator="GreaterThan" type="Integer" errormessage="Received quantity should be greater than 0" ForeColor="Red"></asp:CompareValidator>--%>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="TextBox1" runat="server"  ValidationGroup = '<%# "Group_" + Container.DataItemIndex %>' ErrorMessage="Quantity should be entered" ForeColor="Red"></asp:RequiredFieldValidator>
                    <asp:HiddenField ID="HiddenField3" runat="server" Value='<%# Eval("item_purchase_order_quantity") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="item_accept_date" HeaderText="Item Receipt Date" DataFormatString="{0:dd-MM-yyyy}">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:BoundField>
            <asp:TemplateField HeaderText="Receipt Remarks">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("purchase_order_item_remark") %>'></asp:TextBox>
                    <asp:HiddenField ID="HiddenField4" runat="server" Value='<%# Eval("item_number") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="AcceptItem" runat="server" Text="Accept Item" Enabled='<%# ((string)Eval("item_purchase_order_status") != "Accepted") ? true : false  %>' OnClick="AcceptItem_Click" />
                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("item_number") %>' />
                    <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Eval("item_purchase_order_status") %>' />
                </ItemTemplate>
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
