<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EmployeeViewHistory.aspx.cs" Inherits="Team3ADProject.Protected.EmployeeViewHistory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
     
        <asp:Label ID="Label2" runat="server" Text="Search by Date (YYYY/MM/DD)"></asp:Label>
       
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:ImageButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" />
        <asp:RegularExpressionValidator ID="dateValRegex" runat="server" ControlToValidate="TextBox1" ErrorMessage="Please Enter a valid date in the format (yyyy/mm/dd)" ForeColor="Red" ValidationExpression="^((19|20)\d\d)[\/](0[1-9]|1[012])[\/](0[1-9]|[12][0-9]|3[01])$"></asp:RegularExpressionValidator>
  
        <asp:Calendar ID="Calendar1" runat="server" OnDayRender="Calendar1_DayRender" OnSelectionChanged="Calendar1_SelectionChanged"
            BackColor="White" BorderColor="#3366CC"  CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399" Height="200px" Width="250px" >
       <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
       <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
       <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
       <WeekendDayStyle BackColor="#CCCCFF" />
       <OtherMonthDayStyle ForeColor="#999999" />
       <NextPrevStyle Font-Size="8pt" ForeColor="#CCCCFF" />
       <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
       <TitleStyle BackColor="#003399" BorderColor="#3366CC" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="#CCCCFF" Height="25px" />
       </asp:Calendar>

        <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" CssClass="btn btn-info"/>
    </div>
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField DataField="requisition_id" HeaderText="Requisition Number">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="requisition_status" HeaderText="Requisition Status">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="requisition_date" DataFormatString="{0:yyyy/MM/dd}" HeaderText="Requisition Date">
                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="View" runat="server" Text="View Details" OnClick="View_Click"/>
                        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("requisition_id") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
