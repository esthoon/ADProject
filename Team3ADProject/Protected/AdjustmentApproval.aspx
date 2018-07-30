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
    $(document).ready(function () {
        
            $(".datepicker").datepicker({
                maxDate: 0,
                dateFormat: 'dd-mm-yy'
            });
        })

    </script>


    <h1>Adjustment Form Approval</h1>
    <br/>
    
   <%-- not in used, incase calendar function don't work  <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChanged" OnDayRender="Calendar1_DayRender"></asp:Calendar>
   --%>
    
    Date:<asp:TextBox ID="TextBox2" runat="server" CssClass="datepicker"></asp:TextBox> 
    <asp:RequiredFieldValidator ID="EndDateRequiredValidator" runat="server" ControlToValidate="TextBox2" ErrorMessage="This field is required!" ForeColor ="red"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="StartDateValidator" runat="server" ControlToValidate="TextBox2" ErrorMessage="Please enter a valid date of the form: dd-mm-yyyy" ValidationExpression="[0123][0-9]-[01][0-9]-[0-9]{4}" Enabled="true" ForeColor ="red"></asp:RegularExpressionValidator>

        <div>
        <asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="All" CssClass="btn btn-default" />
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Search" CssClass="btn btn-default" />
    </div>
    <%--validators function for aspnet calendar --%>
    <%-- <asp:RegularExpressionValidator ID="dateValRegex" runat="server" ControlToValidate="TextBox2" ErrorMessage="Please Enter a valid date in the format (mm/dd/yyyy)" ValidationExpression="^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$"></asp:RegularExpressionValidator>
    --%>
        <%-- end of validators --%>

    <h4>Search results: </h4>
    <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Height="99px" Width="1094px" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnPageIndexChanging="GridView1_PageIndexChanging" AllowPaging="True" PageSize="5" OnRowDataBound="GridView1_RowDataBound" >
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
            <asp:BoundField DataField="adjustment_date" HeaderText="Adj date" SortExpression="adjustment_date" DataFormatString="{0:dd-MM-yyyy}" />
            <asp:BoundField DataField="employee_id" HeaderText="Employee Id" SortExpression="employee_id" />
            <asp:BoundField DataField="item_number" HeaderText="Item No." SortExpression="item_number" />
            <asp:BoundField DataField="adjustment_quantity" HeaderText="Adj Qty" SortExpression="adjustment_quantity" />
             <%--newly added--%>
            <asp:TemplateField HeaderText="Currenty Qty">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text="Current qty"></asp:Label>
                    <asp:HiddenField ID="HiddenFielditem" runat="server" Value ='<%# Eval("item_number") %>' />
                    <asp:HiddenField ID="HiddenFieldqty" runat="server" Value ='<%# Eval("adjustment_quantity") %>' />

                </ItemTemplate>            
            </asp:TemplateField>
 <%--newly added end--%>
            <asp:BoundField DataField="adjustment_price" HeaderText="Adj Price" SortExpression="adjustment_price" DataFormatString="{0:c2}" />
            <asp:BoundField DataField="adjustment_status" HeaderText="Adj Status" SortExpression="adjustment_status" />
           <asp:BoundField DataField="employee_remark" HeaderText="Employee Remark" SortExpression="employee_remark" />


            <asp:TemplateField HeaderText="Manager Remark" SortExpression="manager_remark">
                <ItemTemplate>
                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("manager_remark") %>' MaxLength="290"></asp:TextBox>
                </ItemTemplate>


            </asp:TemplateField>

            <asp:TemplateField ShowHeader="True">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Update" Text="Approve" CssClass="btn btn-success" CommandArgument='<%# Eval("adjustment_id") %>'></asp:LinkButton>
                    &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Cancel" Text="Reject" CssClass="btn btn-warning "></asp:LinkButton>
                </ItemTemplate>

            </asp:TemplateField>

        </Columns>

        <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />

    </asp:GridView>

    <br />
  <%--change in code including validation--%>
    <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="False"    OnClick="LinkButton4_Click" CssClass="btn btn-success">Approve Selected</asp:LinkButton>
    <asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False"   OnClick="LinkButton3_Click" CssClass="btn btn-warning">Reject Selected</asp:LinkButton>
<%--change in code including validation--%>

</asp:Content>


