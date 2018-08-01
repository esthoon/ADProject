<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeBehind="AdjustmentForm.aspx.cs" Inherits="Team3ADProject.Protected.AdjustmentForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div>
                <asp:Label ID="Label2" runat="server" Text="Adjustment Form" Font-Bold="True" Font-Size="XX-Large"></asp:Label>
            </div>
            
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Text="Created On"></asp:Label>
                        </td>
                        <td>&nbsp;&nbsp;
                            <asp:Label ID="LabelDate" runat="server" Text="date"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Text="Created by"></asp:Label>
                        </td>
                        <td>&nbsp;&nbsp;
                            <asp:Label ID="LabelName" runat="server" Text="name"></asp:Label>
                        </td>
                    </tr>
                 <tr>
                        <td>
                            <asp:Label ID="lbinum" runat="server" Text="Item Number"></asp:Label>
                        </td>
                        <td>&nbsp;&nbsp;
                            <asp:Label ID="LabelItemNum" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbdes" runat="server" Text="Description "></asp:Label>
                        </td>
                        <td>&nbsp;&nbsp;
                            <asp:Label ID="LabelItem" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbstk" runat="server" Text="Stock "></asp:Label>
                        </td>
                        <td>&nbsp;&nbsp;
                            <asp:Label ID="LabelStock" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbuprice" runat="server" Text="Unit Price "></asp:Label>
                        </td>
                        <td>&nbsp;&nbsp;$<asp:Label ID="LabelUnitPrice" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            <asp:Label ID="lbad" runat="server" Text="Adjustment Quantity "></asp:Label>
                        </td>
                        <td class="auto-style1">
                            &nbsp;&nbsp;<asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="TextBoxAdjustment_TextChanged">
                                <asp:ListItem>+</asp:ListItem>
                                <asp:ListItem Selected="True">-</asp:ListItem>
                            </asp:DropDownList>
                            <asp:TextBox ID="TextBoxAdjustment" runat="server" CausesValidation="True" AutoPostBack="true" OnTextChanged="TextBoxAdjustment_TextChanged"></asp:TextBox>
							<asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="TextBoxAdjustment" ErrorMessage="Enter value between 1 and 100,000" Type="Integer" MinimumValue ="1" MaximumValue="100000" ForeColor ="Red"></asp:RangeValidator>
                            <%--<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="TextBoxAdjustment" ErrorMessage="Quantity must be more than 0" ForeColor="Red" Operator="GreaterThan" Type="Integer" ValueToCompare="0"></asp:CompareValidator>--%>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxAdjustment" ErrorMessage="No quantity stated" ForeColor="Red"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
					<tr>
						<td></td>
						<td>&nbsp;&nbsp;<asp:Label ID="LabelError" runat="server" ForeColor="Red"></asp:Label></td>
					</tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbtcost" runat="server" Text="Total cost: "></asp:Label>
                        </td>
                        <td>&nbsp;&nbsp;
                            <asp:Label ID="LabelTotalCost" runat="server" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Text="Reason:"></asp:Label>
                        </td>
                        <td>&nbsp;&nbsp;
                            <asp:TextBox ID="TextBoxRemarks" runat="server" Height="184px" Width="745px" TextMode="MultiLine" MaxLength="290"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>&nbsp;&nbsp;
                            <asp:Button ID="ButtonCancel" runat="server" Text="Cancel"  CssClass="btn btn-warning" CausesValidation="false" OnClick="ButtonCancel_Click" />
                            &emsp;&emsp;<asp:Button ID="ButtonSubmit" runat="server" Text="Submit"  CssClass="btn btn-primary" OnClick="ButtonSubmit_Click" CausesValidation="true" OnClientClick="this.disabled=true;" UseSubmitBehavior="false" />
                        </td>
                    </tr>
                </table>
                
                <br /><br />
                <asp:Label ID="LabelGrid" runat="server" Text="Label"></asp:Label>

                <div class="row" style="margin-top: 2.5vh;">
                    <div class="col col-md-6">
                        
                        <asp:Label ID="LabelGridMinus" runat="server" Text="Label"></asp:Label>
                        <asp:GridView ID="GridViewAdjMinus" runat="server" AutoGenerateColumns="False" CssClass="table">
                            <Columns>
                                <asp:BoundField DataField="item_number" HeaderText="Item number" />
                                <asp:BoundField DataField="employee.employee_name" HeaderText="Raised by" />
                                <asp:BoundField DataField="adjustment_date" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date Raised" />
                                <asp:BoundField DataField="employee_remark" HeaderText="Remarks" />
                                <asp:BoundField DataField="adjustment_quantity" HeaderText="Adjusted Quantity" />
                            </Columns>
                        </asp:GridView>
                    </div>

                    <div class="col col-md-6">

                        <asp:Label ID="LabelGridPlus" runat="server" Text="Label"></asp:Label>
                        <asp:GridView ID="GridViewAdjPlus" runat="server" AutoGenerateColumns="False" CssClass="table" Style="margin-top: 1px">
                            <Columns>
                                <asp:BoundField DataField="item_number" HeaderText="Item number" />
                                <asp:BoundField DataField="employee.employee_name" HeaderText="Raised by" />
                                <asp:BoundField DataField="adjustment_date" DataFormatString="{0:dd/MM/yyyy}" HeaderText="Date Raised" />
                                <asp:BoundField DataField="employee_remark" HeaderText="Remarks" />
                                <asp:BoundField DataField="adjustment_quantity" HeaderText="Adjusted Quantity" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
</asp:Content>
