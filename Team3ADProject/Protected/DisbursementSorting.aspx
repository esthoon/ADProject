<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="DisbursementSorting.aspx.cs" Inherits="Team3ADProject.Protected.DisbursementSorting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<h1>Disbursement Sorting</h1>
    <div>
        <div>
            <span>Select department: </span>
            <asp:RadioButtonList ID="RadioButtonList_Dpt" runat="server" CssClass="">
            </asp:RadioButtonList>
        </div>
        <div>
            <asp:Button ID="btn_SortingSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btn_SortingSearch_Click" />
        </div>
    </div>

    <div>
        <br />
        <br />
        <br />
        <asp:GridView ID="gridview_DptSort" runat="server" AutoGenerateColumns="False" CssClass="table table-hover align-center" EmptyDataText="There are no new items to disburse for this department. Pick another department." OnDataBound="gridview_DptSort_DataBound">
            <Columns>
                <asp:BoundField DataField="item_number" HeaderText="Item Number" ReadOnly="True" SortExpression="item_number" />
                <asp:BoundField DataField="description" HeaderText="Description" ReadOnly="True" SortExpression="description" />
                <asp:BoundField DataField="required_qty" HeaderText="Ordered Quantity" ReadOnly="True" SortExpression="required_qty" />

                <asp:TemplateField HeaderText="Recommended Distribution Quantity">
                    <ItemTemplate>
                        <asp:TextBox ID="txt_QtyToSupply" runat="server" Text='<%#Eval("supply_qty") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Collected Quantity Available">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Reallocate">
                    <ItemTemplate>
                        <asp:Button ID="btn_reallocate" runat="server" Text="Reallocate" CssClass="btn btn-primary" OnClick="btn_reallocate_Click" />
                        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("item_number") %>' />
                        <asp:HiddenField ID="HiddenField2" runat="server" Value='<%# Eval("description") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>

        <asp:TextBox ID="TextBox_Collect_Date" runat="server"></asp:TextBox>
        <asp:Calendar ID="Calendar_Collect_Date" runat="server" OnSelectionChanged="Calendar_Collect_Date_SelectionChanged" OnDayRender="Calendar_Collect_Date_DayRender"></asp:Calendar>

        <asp:Button ID="btn_ReadyForCollection" runat="server" Text="Ready for Collection" OnClick="btn_ReadyForCollection_Click" CssClass="btn btn-primary" />

    </div>
</asp:Content>
