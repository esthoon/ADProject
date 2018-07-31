using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;
using Team3ADProject.Model;
using System.Security.Principal;

namespace Team3ADProject.Protected
{
    public partial class RequestCart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GenerateGrid();
            }
            if (Session["RequestCart"] == null)
            {
                Label1.Visible = true;
                Label1.Text = "You have not added any items to Request";
                Button2.Visible = false;
            }
            else
            {
                Label1.Visible = false;
                Button2.Visible = true;
            }
        }

        protected void GenerateGrid()
        {
            List<cart> cart = (List<cart>)Session["RequestCart"];
            GridView1.DataSource = cart;
            GridView1.DataBind();
        }

        //To check if cart is empty
        protected void CheckNullCart(object sender, EventArgs e)
        {
            List<cart> cart = (List<cart>)Session["RequestCart"];
            if (cart.Count == 0)
            {
                Label1.Visible = true;
                Label1.Text = "You have not added any items to Request";
                Button2.Visible = false;
            }
            else
            {
                Label1.Visible = false;
                Button2.Visible = true;
            }
        }

        //Redirect to NewRequest page
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/protect/NewRequest.aspx");
        }

        //Check if the item in cart is already existing 
        protected int isExisting(string id)
        {
            List<cart> cart = (List<cart>)Session["RequestCart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Inventory.item_number == id)
                    return i;
            return -1;
        }

        //increase item cart quantity
        protected void ReqItemQuantityIncrease(string itemNumber)
        {
            List<cart> cart = (List<cart>)Session["RequestCart"];
            int index = isExisting(itemNumber);
            cart[index].Quantity++;
            cart[index].Itemprice = cart[index].Quantity * cart[index].Up;
            Session["RequestCart"] = cart;
        }

        //descrease item cart quantity
        protected void ReqItemQuantityDecrease(string itemNumber)
        {
            List<cart> cart = (List<cart>)Session["RequestCart"];
            int index = isExisting(itemNumber);
            if (cart[index].Quantity == 1)
            {
                ReqItemRemove(itemNumber);
            }
            else
            {
                cart[index].Quantity--;
                cart[index].Itemprice = cart[index].Quantity * cart[index].Up;
            }
            Session["RequestCart"] = cart;
        }

        //remove item from cart
        protected void ReqItemRemove(string itemNumber)
        {
            List<cart> cart = (List<cart>)Session["RequestCart"];
            cart.RemoveAt(isExisting(itemNumber));
            Session["RequestCart"] = cart;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Protected/NewRequest.aspx");
        }

        protected void DecreaseQuantity_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            HiddenField hd = (HiddenField)b.FindControl("HiddenField1");
            string itemNumber = hd.Value;
            ReqItemQuantityDecrease(itemNumber);
            GenerateGrid();
            CheckNullCart(this, e);
        }

        protected void IncreaseQuantity_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            HiddenField hd = (HiddenField)b.FindControl("HiddenField4");
            string itemNumber = hd.Value;
            ReqItemQuantityIncrease(itemNumber);
            GenerateGrid();
            CheckNullCart(this, e);
        }

        protected void RemoveItem_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            HiddenField hd = (HiddenField)b.FindControl("HiddenField3");
            string itemNumber = hd.Value;
            ReqItemRemove(itemNumber);
            GenerateGrid();
            CheckNullCart(this, e);
        }

        protected void Button2_Click1(object sender, EventArgs e)
        {
            Button2.CssClass = "btn btn-success disabled";
            Button2.Enabled = false;
            int Empid = (int)Session["Employee"];
            string Depid = (string)Session["Department"]; //19 belongs to ENGL dep
            DateTime d = DateTime.Now.Date;
            //int i = (int)Application["RequestID"] + 1;
            unique_id u = BusinessLogic.getlastrequestid(Depid);
            int i = (int)u.req_id + 1;
            string id = Depid.Trim() + "/" + DateTime.Now.Year.ToString() + "/" + i;
            BusinessLogic.AddNewRequisitionOrder(id, Empid, d);
            List<cart> cart = (List<cart>)Session["RequestCart"];
            BusinessLogic.updatelastrequestid(Depid, i);
            for (int xi = 0; xi < cart.Count; xi++)
            {
                BusinessLogic.AddRequisitionOrderDetail(cart[xi], id);
            }
            //Application["RequestID"] = i;
            Session["RequestCart"] = null;
            string to = BusinessLogic.GetEmployee((int)Session["Head_id"]).email_id.Trim(); //to department head 
            //string to = "tharrani2192@gmail.com";
            string ename = BusinessLogic.GetEmployee(Empid).employee_name;
            string sub = "Stationery System: New request raised for your approval";
            string body = "New Request ID" + i + "has been placed by" + ename + "for your approval";
            BusinessLogic.sendMail(to, sub, body);
            Response.Redirect("~/Protected/RequestConfirm.aspx?id=" + id);

        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox b = (TextBox)sender;
            HiddenField hd = (HiddenField)b.FindControl("HiddenField2");
            string itemNumber = hd.Value;
            List<cart> cart = (List<cart>)Session["RequestCart"];
            int index = isExisting(itemNumber);
            if (cart[index].Quantity == 0)
            {
                ReqItemRemove(itemNumber);
            }
            else
            {
                cart[index].Quantity = Convert.ToInt32(b.Text.ToString());
                cart[index].Itemprice = cart[index].Quantity * cart[index].Up;
            }
            Session["RequestCart"] = cart;
        }
    }
}