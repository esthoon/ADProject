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
                DropDownListSupplier.DataSource = Code.BusinessLogic.GetSupplier(itemid);
                DropDownListSupplier.DataTextField = "supplier_name";
                DropDownListSupplier.DataValueField = "unit_price";
                DropDownListSupplier.DataBind();
                unitCost.Text = DropDownListSupplier.SelectedItem.Value.ToString();

                //Getting an object of the item selected and passed it to the web
                inventory itemSelected = Code.BusinessLogic.GetInventory(itemid);
                itemNumber.Text = itemSelected.item_number;
                itemDescription.Text = itemSelected.description;
                itemCurrentStock.Text = itemSelected.current_quantity.ToString();
                TextBoxOrderQuantity.Text = itemSelected.reorder_quantity.ToString();

                //Getting the user from the session and the current time to be posted on the webpage 
                createByWho.Text = user.employee_name;
                DateTime dateAndTime = DateTime.Now;
                createOnWhen.Text = dateAndTime.ToString("dd/MM/yyyy");

                //When dropdownlist change, change the unit price and change the total price based on the quantity
                CalculationForUnitCostAndTotalCost();
            }
        }

        protected void DropDownListSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            //When dropdownlist change, change the unit price and change the total price based on the quantity
            CalculationForUnitCostAndTotalCost();

        }

        public void CalculationForUnitCostAndTotalCost()
        {
            unitCost.Text = DropDownListSupplier.SelectedItem.Value.ToString();
            totalCost.Text = (Convert.ToDouble(TextBoxOrderQuantity.Text) * Convert.ToDouble(unitCost.Text)).ToString("C");
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
            if (Calendar.SelectedDate < DateTime.Now)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select a date')", true);
            }
            else
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
                string requiredDate = Calendar.SelectedDate.ToString("yyyy-MM-dd");
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
            
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClerkInventory.aspx");
        }
    }
}