using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;
using System.Security.Principal;
using Team3ADProject.Model;

namespace Team3ADProject.Protected
{
	public partial class NewRequest : System.Web.UI.Page
	{

		protected void Page_Load(object sender, EventArgs e)
		{
           if(!IsPostBack)
           {
                    GenerateAllItemGrid();
           }
        }

        protected void GenerateAllItemGrid()
        {
            GridView1.DataSource = BusinessLogic.GetActiveInventory();
            GridView1.DataBind();
        }

        protected void GenerateSearchItemGrid(string Search)
        {
            GridView1.DataSource = BusinessLogic.GetInventoryById(Search);
            GridView1.DataBind();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GenerateAllItemGrid();
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            HiddenField hd = (HiddenField)b.FindControl("HiddenFieldItemNumber");
            string itemNumber = hd.Value;
            if (Session["RequestCart"] == null)
            { 
                List<cart>cart = new List<cart>();
                cart.Add(new cart(BusinessLogic.GetInventoryById(itemNumber), 1));
                Session["RequestCart"] = cart;
            }
            else
            {
                List<cart> cart = (List<cart>)Session["RequestCart"];
                int index = isExisting(itemNumber);
                if (index == -1)
                {
                    cart.Add(new cart(BusinessLogic.GetInventoryById(itemNumber), 1));
                }
                else
                {
                    cart[index].Quantity++;
                    cart[index].Itemprice = cart[index].Quantity * cart[index].Up;
                    Session["RequestCart"] = cart;
                }
            }
        }

        protected int isExisting(string id)
        {
            List<cart> cart = (List<cart>)Session["RequestCart"];
            for (int i = 0; i < cart.Count; i++)
                if (cart[i].Inventory.item_number == id)
                    return i;
            return -1;
        }

        protected void Button2_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/Protected/RequestCart.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if(TextBox1.Text == null)
            {
                Response.Write("Please enter name to search"); 
            }
            else
            {
                string search = TextBox1.Text.ToString();
                GridView1.DataSource = BusinessLogic.SearchActiveInventory(search);
                GridView1.DataBind();
            }

        }
    }
}