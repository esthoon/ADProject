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
    public partial class WebForm2 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //retrieve user
                employee user = (employee)Session["user"];
                if (user == null)
                {
                    user = BusinessLogic.GetEmployeeById(10);
                    Session["user"] = user;
                }
                //
                LabelDate.Text = DateTime.Now.ToString("dd-MMM-yyyy");
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
                inventory item = BusinessLogic.GetInventory(itemcode);
                LabelUnitPrice.Text = BusinessLogic.Adjprice(itemcode).ToString();
                LabelStock.Text = item.current_quantity.ToString();
                LabelItemNum.Text = item.item_number.ToString();
                LabelItem.Text = item.description;
                Session["adjItem"] = item;

            }

        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            //Response.Redirect("ClerkInventory.aspx");
        }

        protected double TotalPrice()
        {
            string param_itemcode = Request.QueryString["itemcode"];
            int qty = Int32.Parse(TextBoxAdjustment.Text);
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

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            employee user = (employee)Session["user"];
            inventory item = (inventory)Session["adjItem"];
            String today = DateTime.Now.ToString("yyyy-MM-dd");
            int qty = Int32.Parse(TextBoxAdjustment.Text);
            try
            {
                adjustment a = new adjustment()
                {
                    adjustment_date = DateTime.ParseExact(today, "yyyy-MM-dd", null),
                    employee_id = user.employee_id,
                    item_number = item.item_number,
                    adjustment_quantity = qty,
                    adjustment_price = TotalPrice(),
                    adjustment_status = "Pending",
                    employee_remark = TextBoxRemarks.Text,
                    manager_remark = "",
                };

                try
                {
                    using (TransactionScope tx = new TransactionScope())
                    {
                        BusinessLogic.CreateAdjustment(a);
                        tx.Complete();
                        Response.Write(BusinessLogic.MsgBox("Success: The adjustment request has been sent for approval"));
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

        protected void TextBoxAdjustment_TextChanged(object sender, EventArgs e)
        {
            double price = TotalPrice();
            LabelTotalCost.Text = "$" + price;
        }
    }
}