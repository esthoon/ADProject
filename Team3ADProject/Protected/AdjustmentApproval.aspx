<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AdjustmentApproval.aspx.cs" Inherits="Team3ADProject.Protected.AdjustmentApproval" %>

<%@ Import Namespace="Team3ADProject.Protected" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
   <%--checkbox select all function --%>
     <script>
        function SelectAllCheckboxes(chk) {
            $('#<%=GridView1.ClientID %>').find("input:checkbox").each(function () {
                if (this != chk) {
                    this.checked = chk.checked;
                }
            });
        }
    </script> 
    
    <script>
        $(function() {
            $(".datepicker").datepicker({ maxDate: 0 });
        })
           
    </script>


    <h1>Adjustment Form Approval</h1>
    
    <br/>
    <%--
    <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged" OnDayRender="Calendar1_DayRender"></asp:Calendar> --%>
    <asp:TextBox ID="TextBox2" runat="server" CssClass="datepicker"></asp:TextBox>

    <div>
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="All" CssClass="btn btn-default" />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Search" CssClass="btn btn-default" />
    </div>
    <%--validators function --%>
    <%-- <asp:RegularExpressionValidator ID="dateValRegex" runat="server" ControlToValidate="TextBox2" ErrorMessage="Please Enter a valid date in the format (mm/dd/yyyy)" ValidationExpression="^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$"></asp:RegularExpressionValidator>
    --%>
        <%-- end of validators --%>

    <h4>Search results: </h4>
    <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Height="99px" Width="1094px" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnPageIndexChanging="GridView1_PageIndexChanging" AllowPaging="True" PageSize="5">
        <Columns>
            <asp:TemplateField>
                <HeaderTemplate>
                    <asp:CheckBox ID="chkSelectAll" runat="server" onclick="javascript:SelectAllCheckboxes(this);" />
                </HeaderTemplate>
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelect" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="adjustment_id" HeaderText="Adj ID" InsertVisible="False" ReadOnly="True" SortExpression="adjustment_id" />
            <asp:BoundField DataField="adjustment_date" HeaderText="Adj date" SortExpression="adjustment_date" DataFormatString="{0:MM/dd/yyyy}" />
            <asp:BoundField DataField="employee_id" HeaderText="Employee Id" SortExpression="employee_id" />
            <asp:BoundField DataField="item_number" HeaderText="Item No." SortExpression="item_number" />
            <asp:BoundField DataField="adjustment_quantity" HeaderText="Adj Qty" SortExpression="adjustment_quantity" />
            <asp:BoundField DataField="adjustment_price" HeaderText="Adj Price" SortExpression="adjustment_price" DataFormatString="{0:c2}" />
            <asp:BoundField DataField="adjustment_status" HeaderText="Adj Status" SortExpression="adjustment_status" />

       


            <asp:BoundField DataField="employee_remark" HeaderText="Employee Remark" SortExpression="employee_remark" />
            <asp:TemplateField HeaderText="Manager Remark" SortExpression="manager_remark">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("manager_remark") %>'></asp:TextBox>
                </ItemTemplate>

            </asp:TemplateField>

            <asp:TemplateField ShowHeader="True">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="True" CommandName="Update" Text="Approve" CssClass="btn btn-success" CommandArgument='<%# Eval("adjustment_id") %>'></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Reject" CssClass="btn btn-warning "></asp:LinkButton>
                </ItemTemplate>

            </asp:TemplateField>
          
        </Columns>

        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />

    </asp:GridView>

    <br />
    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="btn btn-success">Approve Selected</asp:LinkButton>
    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click" CssClass="btn btn-warning">Reject Selected</asp:LinkButton>


</asp:Content>


