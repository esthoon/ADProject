<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EmployeeViewHistory.aspx.cs" Inherits="Team3ADProject.Protected.EmployeeViewHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>View Requisition History</h1>
    <div>
        <div>
            <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
            <br />
            <asp:Label ID="Label2" runat="server" Text="Search by Date (dd-MM-yyyy)"></asp:Label>
            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        </div>
    <%--<asp:ImageButton ID="ImageButton1" runat="server" OnClick="ImageButton1_Click" />  --%>
    <asp:RegularExpressionValidator ID="dateValRegex" runat="server" ControlToValidate="TextBox1" ErrorMessage="Please Enter a valid date in the format (dd-MM-yyyy)" ForeColor="Red" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$"></asp:RegularExpressionValidator>

        <asp:Calendar ID="Calendar1" runat="server" OnDayRender="Calendar1_DayRender" OnSelectionChanged="Calendar1_SelectionChanged"
            BackColor="White" BorderColor="#666666" CellPadding="1" DayNameFormat="Shortest" Font-Names="Fantasy" Height="200px" Width="250px">
            <TodayDayStyle BackColor="#99CCCC" ForeColor="White" Font-Names="Fantasy"/>
            <WeekendDayStyle BackColor="#CCCCFF" />
            <DayHeaderStyle BackColor="#eef9ff" ForeColor="#336666" Height="1px" />
            <TitleStyle BackColor="#99ff99" BorderColor="#003300" BorderWidth="1px" Font-Size="10pt" ForeColor="Black" Height="25px" Font-Names="Fantasy"/>
        </asp:Calendar>

    <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click" CssClass="btn btn-info" />
    </div>
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-hover">
            <Columns>
                <asp:BoundField DataField="requisition_id" HeaderText="Requisition Number">
                    <HeaderStyle VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="requisition_status" HeaderText="Requisition Status">
                    <HeaderStyle VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="requisition_date" DataFormatString="{0:dd-MM-yyyy}" HeaderText="Requisition Date">
                    <HeaderStyle VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="View" runat="server" Text="View Details" OnClick="View_Click" CssClass="btn btn-info" />
                        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("requisition_id") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
