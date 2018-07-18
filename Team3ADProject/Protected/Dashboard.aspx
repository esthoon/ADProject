<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Team3ADProject.Protected.Dashboard1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2>User Dashboard</h2>
    <div class="row">
        <div class="col-md-7">
            <h3>Most Recent Requisition Orders:</h3>
            <asp:GridView ID="RecentROGridView" runat="server" CssClass="table table-condensed">
            </asp:GridView>
            <asp:LinkButton ID="RequisitionOrder_Link" runat="server" CssClass="btn btn-success">Go to Requisition Order Listing</asp:LinkButton>




        </div>
        <div class="col-md-5">
            <div id="requisitionOrderStatusChart"></div>
        </div>
    </div>
</asp:Content>
