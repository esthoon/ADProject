using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;
using Team3ADProject.Model;
using System.Transactions;

namespace Team3ADProject.Protected
{
    public partial class AdjustmentForm1 : System.Web.UI.Page
    {
        static employee user;
        static inventory item;
        static int headid, supid;
        static int minusqty=0;
        protected void Page_Load(object sender, EventArgs e)
        {
            ButtonSubmit.Enabled = true;
            if (!IsPostBack)
            {
                //retrieve user
                if (Session["Employee"] != null)
                {
                    int employeeid = (int)Session["Employee"];
                    user = BusinessLogic.GetEmployeeById(employeeid);
                }
                //retrieve headid
                headid = BusinessLogic.DepartmentHeadID(user);

                //retrieve supid
                if (user.supervisor_id != null)
                {
                    supid = (int)user.supervisor_id;
                }
                else
                {
                    supid = headid;
                }
                LabelDate.Text = DateTime.Now.ToString("dd-MM-yyyy");
                LabelName.Text = user.employee_name;
                UpdatePage();
            }
        }

        // Updates page given a book
        protected void UpdatePage()
        {
            // Grab the attributes from the URL
            string param_itemcode = Request.QueryString["itemcode"];
            if (param_itemcode != null)
            {
                string itemcode = param_itemcode;
                item = BusinessLogic.GetInventory(itemcode);
                List<adjustment> adjlist = BusinessLogic.GetPendingAdjustmentsByItemCode(itemcode);
                LabelUnitPrice.Text = BusinessLogic.Adjprice(itemcode).ToString();
                LabelStock.Text = item.current_quantity.ToString();
                LabelItemNum.Text = item.item_number.ToString();
                LabelItem.Text = item.description;
                minusqty = BusinessLogic.ReturnPendingMinusAdjustmentQty(itemcode);
                int plusqty = BusinessLogic.ReturnPendingPlusAdjustmentQty(itemcode);
                GridViewAdjMinus.DataSource = adjlist.Where(x => x.adjustment_quantity < 0);
                GridViewAdjMinus.DataBind();
                GridViewAdjPlus.DataSource = adjlist.Where(x => x.adjustment_quantity > 0);
                GridViewAdjPlus.DataBind();
                if (adjlist.Count > 0)
                {
                    LabelGrid.Visible = false;
                    if (minusqty != 0)
                    {
                        LabelGridMinus.Text = "Pending adjustment qty to be removed raised: " + minusqty;
                    }
                    else
                    {
                        LabelGridMinus.Text = "No pending adjustment qty to be removed.";
                    }
                    if (plusqty != 0)
                    {
                        LabelGridPlus.Text = "Pending adjustment qty to be added raised: " + plusqty;
                    }
                    else
                    {
                        LabelGridPlus.Text = "No pending adjustment qty to be removed.";
                    }

                }
                else
                {
                    LabelGrid.Text = "No pending adjustments for this item.";
                    LabelGridMinus.Visible = false;
                    LabelGridPlus.Visible = false;
                }
            }

        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            //Response.Redirect(ResolveUrl("~/Protected/ClerkInventory"));
            Response.Write("<script language='javascript'> { window.close();}</script>");
        }

        protected double TotalPrice()
        {
            int qty = 0;
            if (TextBoxAdjustment.Text.Trim() != null && Int32.TryParse(TextBoxAdjustment.Text, out qty))
            {
                string param_itemcode = Request.QueryString["itemcode"];
                double unitprice = BusinessLogic.Adjprice(param_itemcode);
                string symbol = DropDownList1.SelectedItem.Value;
                if (symbol == "-")
                {
                    return (unitprice * (-qty));
                }
                else
                {
                    return (unitprice * qty);
                }
            }
            else
            {
                return 0;
            }
        }

        protected int ReturnQuantity()
        {
            int qty = 0;
            if (TextBoxAdjustment.Text.Trim() != null && Int32.TryParse(TextBoxAdjustment.Text, out qty))
            {
                string symbol = DropDownList1.SelectedItem.Value;
                if (symbol == "-")
                {
                    qty = (-qty);
                }
            }
            return qty;
        }

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            int qty = Int32.Parse(TextBoxAdjustment.Text);
            int submitqty = ReturnQuantity();
            ButtonSubmit.Enabled = false;
            if (qty != 0)
            {
                if (submitqty < 0)
                {
                    if (Math.Abs(submitqty) > (item.current_quantity + minusqty))
                    {
                        LabelError.Text = "Entered quantity is > current quantity";
                    }
                    else
                    {
                        CreateAdjustment();
                    }
                }
                else
                {
                    CreateAdjustment();
                }
            }
        }

        protected void TextBoxAdjustment_TextChanged(object sender, EventArgs e)
        {
            if (TextBoxAdjustment.Text.Trim() != null)
            {
                double price = TotalPrice();
                LabelTotalCost.Text = "$" + price.ToString("0.00");
            }
        }

        protected string RetrieveEmail(double price)
        {
            if (price > 250)
            {
                return BusinessLogic.RetrieveEmailByEmployeeID(headid);
            }
            else
            {
                return BusinessLogic.RetrieveEmailByEmployeeID(supid);
            }
        }

        protected void CreateAdjustment()
        {
            string email = RetrieveEmail(TotalPrice());
            String today = DateTime.Now.ToString("yyyy-MM-dd");
            try
            {
                adjustment a = new adjustment()
                {
                    adjustment_date = DateTime.ParseExact(today, "yyyy-MM-dd", null),
                    employee_id = user.employee_id,
                    item_number = item.item_number,
                    adjustment_quantity = ReturnQuantity(),
                    adjustment_price = TotalPrice(),
                    adjustment_status = "Pending",
                    employee_remark = TextBoxRemarks.Text,
                    manager_remark = null,
                };

                try
                {
                    using (TransactionScope tx = new TransactionScope())
                    {
                        BusinessLogic.CreateAdjustment(a);
                        tx.Complete();
                        Response.Write(BusinessLogic.MsgBox("Success: The adjustment request has been sent for approval"));
                        BusinessLogic.sendMail(email, "New Adjustment Request awaiting for approval", user.employee_name + " has submitted a new Adjustment Request for approval.");
                    }
                    //Response.Redirect("ClerkInventory.aspx");
                    Response.Write("<script language='javascript'> { window.close();}</script>");
                }
                catch (System.Transactions.TransactionException ex)
                {
                    Response.Write(BusinessLogic.MsgBox(ex.Message));
                }
                catch (Exception ex)
                {
                    Response.Write(BusinessLogic.MsgBox(ex.Message));
                }
            }

            catch (Exception ex)
            {
                Response.Write(BusinessLogic.MsgBox(ex.Message));
            }
        }
    }
}