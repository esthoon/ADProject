<%@ Page Title="" Language="C#" MasterPageFile="~/Guest.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Team3ADProject.Default" %>

<%-- Written by: Chua Khiong Yang --%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <link rel="stylesheet" href="<%=ResolveUrl("~/Content/Sites/Default.css")%>" />
    <link rel="stylesheet" href="<%=ResolveUrl("~/Content/bootstrap.css") %>" />
    <link rel="stylesheet" href="<%=ResolveUrl("~/Content/bootstrap-theme.css") %>" />

    <%--The default landing page is a login page. --%>
    
    <div class="flex-container">
        <div class="flex-item">
            <h1>Logic University</h1>
        </div>


        <div class="flex-item">
            <asp:Login ID="Login1" runat="server" OnLoggedIn="Login1_LoggedIn">
                <LayoutTemplate>
                    <div class="flex-container-login">
                        <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>

                        <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName" ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">Username is required!</asp:RequiredFieldValidator>

                        <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>

                        <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password" ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">Password is required!</asp:RequiredFieldValidator>

                        <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>

                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="Login1" CssClass="btn btn-primary" />
                    </div>
                </LayoutTemplate>
            </asp:Login>
        </div>
    </div>

</asp:Content>

