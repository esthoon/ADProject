<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Reallocate.aspx.cs" Inherits="Team3ADProject.Protected.Reallocate" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

	<h1>Reallocate Quantity</h1>

    <h3>Item Number:</h3>
    <asp:Label ID="Label_itemNum" runat="server" Text="Label"></asp:Label>
    <h3>Description</h3>
    <asp:Label ID="Label_Description" runat="server" Text="Label"></asp:Label>
    <br />
    <br />

    <asp:GridView ID="gridview_Reallocate" runat="server" AutoGenerateColumns="False" CssClass="table table-hover align-center" OnDataBound="gridview_Reallocate_DataBound">
        <Columns>
            <asp:BoundField DataField="department_id" HeaderText="Department" ReadOnly="True" SortExpression="department_name" />
            <asp:BoundField DataField="item_requisition_quantity" HeaderText="Ordered Quantity" ReadOnly="True" SortExpression="item_requisition_quantity" />
            <asp:BoundField DataField="item_number" HeaderText="Item_Number" ReadOnly="True" Visible="false" SortExpression="department_name" />

            <asp:TemplateField HeaderText="Recommended Distribution Quantity">
                <ItemTemplate>
                    <asp:TextBox ID="txt_distribution_qty" runat="server"></asp:TextBox>
                </ItemTemplate>
            </asp:TemplateField>


        </Columns>
    </asp:GridView>


    <h3>Total Collected from Warehouse:  
        <asp:Label ID="Label_collectedAmount" runat="server" Text="Label" Font-Size="20px" ForeColor="Blue"></asp:Label></h3>

    <asp:Button ID="Button_Reallocate" runat="server" Text="Reallocate" CssClass="btn btn-primary" OnClick="Button_Reallocate_Click" />
    <asp:Label ID="Label_warning" runat="server" Text="You entered more quantity than collected" Visible="false" Font-Size="20px" ForeColor="Red"></asp:Label>


   <%-- <asp:Button ID="Button1" runat="server" Text="Test Modal" data-toggle="modal" data-target="#login_modal" />


    <div class="modal fade" id="login_modal" role="dialog">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h3 class="modal-title">Returning items to warehouse?</h3>
                </div>

                <div class="modal-body">
                    <div class="login-wrapper">
                        <asp:TextBox ID="txt_username" runat="server" CssClass="form-control" placeholder="Email"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator_username" runat="server" CssClass="text-danger"
                            ControlToValidate="txt_username" ErrorMessage="Please enter username"></asp:RequiredFieldValidator>

                        <asp:TextBox ID="txt_password" runat="server" CssClass="form-control" placeholder="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="text-danger"
                            ControlToValidate="txt_password" ErrorMessage="Please enter password"></asp:RequiredFieldValidator>
                    </div>
                </div>

                <div class="modal-footer">
                    <asp:Button ID="btn_login" CssClass="btn btn-primary" runat="server" Text="Login" />
                </div>
            </div>
        </div>
    </div>--%>
</asp:Content>
