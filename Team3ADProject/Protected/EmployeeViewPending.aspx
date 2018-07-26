<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="EmployeeViewPending.aspx.cs" Inherits="Team3ADProject.Protected.EmployeeViewPending" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>View Pending Requisition Orders</h1>
    <div>
        <asp:Label ID="Label2" runat="server" Text="Search by Date (dd-MM-yyyy)"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:RegularExpressionValidator ID="dateValRegex" runat="server" ControlToValidate="TextBox1" ErrorMessage="Please Enter a valid date in the format (dd-mm-yyyy)" ForeColor="Red" ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[\-](0[1-9]|1[012])[\-]((19|20)\d\d)$"></asp:RegularExpressionValidator>
        <!-- ^(0[1-9]|1[012])[- -.](0[1-9]|[12][0-9]|3[01])[- -.](19|20)\d\d$ (mm/dd/yyyy) -->
        <asp:Calendar ID="Calendar1" runat="server" OnDayRender="Calendar1_DayRender" OnSelectionChanged="Calendar1_SelectionChanged"
            BackColor="White" BorderColor="#666666" CellPadding="1" DayNameFormat="Shortest" Font-Names="Fantasy" Height="200px" Width="250px">
            <TodayDayStyle BackColor="#99CCCC" ForeColor="White" Font-Names="Fantasy"/>
            <WeekendDayStyle BackColor="#CCCCFF" />
            <DayHeaderStyle BackColor="#eef9ff" ForeColor="#336666" Height="1px" />
            <TitleStyle BackColor="#99ff99" BorderColor="#003300" BorderWidth="1px" Font-Size="10pt" ForeColor="Black" Height="25px" Font-Names="Fantasy"/>
        </asp:Calendar>
    </div>

    <div>
        <asp:Button ID="Button1" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="Button1_Click" />
        <br />
        <asp:Button ID="Button2" runat="server" Text="Add New Request" CssClass="btn btn-info" OnClick="Button2_Click" />
    </div>
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-hover">
            <Columns>
                <asp:BoundField DataField="requisition_id" HeaderText="Requsition Number">
                    <HeaderStyle VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="requisition_date" HeaderText="Requisition Date" HtmlEncode="false" DataFormatString="{0:dd-MM-yyyy}">
                    <HeaderStyle VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:BoundField DataField="requisition_status" HeaderText="Requisition Status">
                    <HeaderStyle VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="EditButton" runat="server" OnClick="EditButton_Click" Text="Edit" CssClass="btn btn-info" />
                        <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("requisition_id") %>' />
                    </ItemTemplate>
                    <HeaderStyle VerticalAlign="Middle" />
                    <ItemStyle VerticalAlign="Middle" />
                </asp:TemplateField>
            </Columns>


        </asp:GridView>
    </div>
</asp:Content>
