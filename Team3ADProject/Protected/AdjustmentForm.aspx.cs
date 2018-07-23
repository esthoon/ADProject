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
        static employee user;
        static inventory item;
        static int headid, supid;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //retrieve user
                if (Session["Employee"] != null)
                {
                    int employeeid = (int)Session["Employee"];
                    user = BusinessLogic.GetEmployeeById(employeeid);
                }
                else
                {
                    //hardcoded
                    Session["Employee"] = 10;
                    user = BusinessLogic.GetEmployeeById(10);
                    //redirect to login homepage
                }
                //retrieve headid
                if (Session["Head_id"] != null)
                {
                    headid =(int)Session["Head_id"];
                }
                else
                {
                    headid = 12;
                }
                //retrieve headid
                if (Session["Sup_id"] != null)
                {
                    supid = (int)Session["Head_id"];
                }
                else
                {
                    supid = 13;
                }
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
                item = BusinessLogic.GetInventory(itemcode);
                LabelUnitPrice.Text = BusinessLogic.Adjprice(itemcode).ToString();
                LabelStock.Text = item.current_quantity.ToString();
                LabelItemNum.Text = item.item_number.ToString();
                LabelItem.Text = item.description;

            }

        }

        protected void ButtonCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClerkInventory.aspx");
            //Response.Write("<script language='javascript'> { window.close();}</script>");
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

        protected void ButtonSubmit_Click(object sender, EventArgs e)
        {
            String today = DateTime.Now.ToString("yyyy-MM-dd");
            int qty = Int32.Parse(TextBoxAdjustment.Text);
            string email = RetrieveEmail(TotalPrice());
            if (qty != 0)
            {
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
                            BusinessLogic.sendMail("e0283990@u.nus.edu", "New Adjustment Request awaiting for approval", user.employee_name + " has submitted a new Adjustment Request for approval.");
                        }
                        Response.Redirect("ClerkInventory.aspx");
                        //Response.Write("<script language='javascript'> { window.close();}</script>");
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
                //return BusinessLogic.RetrieveEmailByEmployeeID(headid);
                return "e0283390@u.nus.edu";
            }
            else
            {
                //return BusinessLogic.RetrieveEmailByEmployeeID(supid);
                return "e0283390@u.nus.edu"; 
            }
        }
    }
}