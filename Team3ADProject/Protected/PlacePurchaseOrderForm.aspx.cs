using System;
using System.Collections.Generic;
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
        string itemid;
        List<StagingItem> stagingitem;
        protected void Page_Load(object sender, EventArgs e)
        {   //Get the item code
            if (Request.QueryString["itemid"] != null)
            {
                itemid = Request.QueryString["itemid"];
            }
            else
            {
                itemid = "E032";
            }

            if (!IsPostBack)
            {
                
                //Binding the supplier to a dropdownlist to the item selected
                DropDownListSupplier.DataSource = Code.BusinessLogic.GetSupplier(itemid);
                DropDownListSupplier.DataTextField = "supplier_id";
                DropDownListSupplier.DataValueField = "unit_price";
                DropDownListSupplier.DataBind();
                unitCost.Text = DropDownListSupplier.SelectedItem.Value.ToString();

                //Getting an object of the item selected and passed it to the web
                inventory itemSelected = Code.BusinessLogic.GetInventory(itemid);
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

        protected void Submit_Click(object sender, EventArgs e)
        {
            if(Session["staging"] != null)
            {
                stagingitem = (List<StagingItem>)Session["staging"];
            }
            else
            {
                stagingitem = new List<StagingItem>();
            }

            StagingItem newItem = new StagingItem();

            newItem.item_id = itemDescription.Text;
            newItem.date_required = Calendar1.SelectedDate.ToString("dd/MM/yyyy");
            newItem.quantity = quantity.Text;
            newItem.buyer = createByWho.Text;
            newItem.unit_price = Convert.ToDouble(unitCost.Text);
            newItem.supplier = DropDownListSupplier.SelectedItem.Text;

            stagingitem.Add(newItem);

            Session["staging"] = stagingitem;

            Response.Redirect("ClerkInventory.aspx");
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClerkInventory.aspx");
        }
    }
}