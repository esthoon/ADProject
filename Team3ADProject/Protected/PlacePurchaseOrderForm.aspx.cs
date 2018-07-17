using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Model;

namespace Team3ADProject.Protected
{
    public partial class PlacePurchaseOrderForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Binding the supplier to a dropdownlist to the item selected
                DropDownListSupplier.DataSource = Code.BusinessLogic.GetSupplier("F031");
                DropDownListSupplier.DataTextField = "supplier_id";
                DropDownListSupplier.DataValueField = "unit_price";
                DropDownListSupplier.DataBind();
                unitCost.Text = DropDownListSupplier.SelectedItem.Value.ToString();

                //Getting an object of the item selected and passed it to the web
                inventory itemSelected = Code.BusinessLogic.GetInventory("F031");
                itemNumber.Text = itemSelected.item_number;
                itemDescription.Text = itemSelected.description;
                itemCurrentStock.Text = itemSelected.current_quantity.ToString();
                quantity.Text = itemSelected.reorder_quantity.ToString();

                //Getting the user from the session and the current time to be posted on the webpage 
                createByWho.Text = "method from session";
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
            totalCost.Text = (Convert.ToDouble(quantity.Text) * Convert.ToDouble(unitCost.Text)).ToString("C");
        }
    }
}