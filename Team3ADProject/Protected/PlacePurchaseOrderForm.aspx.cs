using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;
using Team3ADProject.Model;

namespace Team3ADProject.Protected
{
    public partial class PlacePurchaseOrderForm : System.Web.UI.Page
    {
        static string itemid;
        static employee user;

        protected void Page_Load(object sender, EventArgs e)
        {   //Get the item code
            if (Request.QueryString["itemid"] != null)
            {
                itemid = Request.QueryString["itemid"];
            }
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

            if (!IsPostBack)
            {
                
                //Binding the supplier to a dropdownlist to the item selected
                DropDownListSupplier.DataSource = BusinessLogic.GetSupplier(itemid);
                DropDownListSupplier.DataTextField = "supplier_name";
                DropDownListSupplier.DataValueField = "supplier_id";
                DropDownListSupplier.DataBind();
                
                //Getting an object of the item selected and passed it to the web
                inventory itemSelected = BusinessLogic.GetInventory(itemid);
                itemNumber.Text = itemSelected.item_number;
                itemDescription.Text = itemSelected.description;
                itemCurrentStock.Text = itemSelected.current_quantity.ToString();
                TextBoxOrderQuantity.Text = (itemSelected.reorder_level - itemSelected.current_quantity).ToString();

                //Getting the user from the session and the current time to be posted on the webpage 
                createByWho.Text = user.employee_name;
                DateTime dateAndTime = DateTime.Now;
                createOnWhen.Text = dateAndTime.ToString("dd-MM-yyyy");
                LabelRequiredDate.Text = dateAndTime.AddDays(28).ToString("dd-MM-yyyy");

                //When dropdownlist change, change the unit price and change the total price based on the quantity
                CalculationForUnitCostAndTotalCost((itemSelected.reorder_quantity - itemSelected.current_quantity));
            }
        }

        protected void DropDownListSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            int qty;

                if (Int32.TryParse(TextBoxOrderQuantity.Text, out qty))
                {
                    //When dropdownlist change, change the unit price and change the total price based on the quantity
                    CalculationForUnitCostAndTotalCost(qty);
                }
                else
                {
                    ErrorText.Text = "Please input a Whole Number between 1 and 1,000,000";
                    Submit.Enabled = false;
                }
            
        }

        public void CalculationForUnitCostAndTotalCost(int qty)
        {
            if (qty > 0 && qty <=1000000)
            {
                unitCost.Text = BusinessLogic.getUnitPrice(DropDownListSupplier.SelectedValue, itemid).ToString();
                totalCost.Text = (qty * Convert.ToDouble(unitCost.Text)).ToString("C");
                ErrorText.Text = "";
                Submit.Enabled = true;
            }
            else
            {
                ErrorText.Text = "Please input a Whole Number between 1 and 1,000,000";
                Submit.Enabled = false;
            }
                    
           
        }


        protected void CalendarSelected(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date <= DateTime.Now)
            {
                e.Cell.BackColor = ColorTranslator.FromHtml("#a9a9a9");

                e.Day.IsSelectable = false;
            }
        }

        //esther-adding POitem to cart
        protected void Submit_Click(object sender, EventArgs e)
        {
            List<POStaging> alist = new List<POStaging>();
            if (Session["StagingList"] != null)
            {
                alist = (List<POStaging>)Session["StagingList"];
            }
            inventory item = BusinessLogic.GetInventory(itemid);
            string suppliername = DropDownListSupplier.SelectedItem.Text;
            string supplierid = BusinessLogic.GetSupplierID(suppliername);
            int orderqty = Int32.Parse(TextBoxOrderQuantity.Text);
            double unitprice = Double.Parse(unitCost.Text);
            string requiredDate = DateTime.Now.AddDays(28).ToString("yyyy-MM-dd");
            try
            {
                POStaging poItem = new POStaging(item, supplierid, orderqty, unitprice, DateTime.ParseExact(requiredDate, "yyyy-MM-dd", null), user);
                Session["StagingList"] = BusinessLogic.AddToStaging(alist, poItem);

            }
            catch (Exception ex)
            {
                Label1.Text = ex.Message;
            }
            Response.Redirect("POStagingSummary.aspx");

        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClerkInventory.aspx");
        }
    }
}