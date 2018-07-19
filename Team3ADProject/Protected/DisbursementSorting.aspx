<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DisbursementSorting.aspx.cs" Inherits="Team3ADProject.Protected.DisbursementSorting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Disbursement Sorting</h1>
    <div>
        <div>
            <table>
                <tr>
                    <td>
                        <asp:RadioButton ID="radBtn_searchByRO" runat="server" GroupName="sortingSearch" Text="Search by Requisition Order ID: " />
                    </td>
                    <td>
                        <asp:TextBox ID="txt_searchByRO" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:RadioButton ID="radBtn_searchByDpt" runat="server" GroupName="sortingSearch" Text="Search by Department: " />
                    </td>
                    <td>
                        <asp:DropDownList ID="dropList_searchByDpt" runat="server" DataValueField="department_name"></asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:Button ID="btn_SortingSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btn_SortingSearch_Click" />
        </div>
    </div>

    <div>
        <br />
        <br />
        <br />
        <asp:GridView ID="gv_SortingTable" runat="server" AutoGenerateColumns="False" CssClass="table table-hover align-center">
            <Columns>
                <asp:BoundField DataField="item_distributed_quantity" HeaderText="ItemDistributedQty" ReadOnly="True" Visible="false" SortExpression="item_distributed_quantity" />
                <asp:BoundField DataField="item_number" HeaderText="Item Number" ReadOnly="True" SortExpression="item_number" />
                <asp:BoundField DataField="description" HeaderText="Description" ReadOnly="True" SortExpression="description" />
                <asp:BoundField DataField="item_requisition_quantity" HeaderText="Required Quantity" ReadOnly="True" SortExpression="item_requisition_quantity" />
               
                <asp:BoundField DataField="item_pending_quantity" HeaderText="Supply Quantity" ReadOnly="True" SortExpression="item_pending_quantity" />
                
                <asp:TemplateField HeaderText="Supply Quantity">
                    <ItemTemplate>
                        <asp:TextBox ID="txt_QtyToSupply" runat="server" Text='<%# (Convert.ToInt32(Eval("quantity_ordered")) <= Convert.ToInt32(Eval("current_quantity")) ? Eval("quantity_ordered") : Eval("current_quantity")) %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Adjustment">
                    <ItemTemplate>
                        <input type="button" name="btn_Adjustment" value="Adjust">
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
