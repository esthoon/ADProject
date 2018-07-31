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
                Response.Redirect(ResolveUrl("~"));
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
                if ((itemSelected.reorder_level - itemSelected.current_quantity) <= 0)
                {
                    TextBoxOrderQuantity.Text = itemSelected.reorder_quantity.ToString();
                }
                else
                {
                    TextBoxOrderQuantity.Text = (itemSelected.reorder_level - itemSelected.current_quantity).ToString();
                }
                //Getting the user from the session and the current time to be posted on the webpage 
                createByWho.Text = user.employee_name;
                DateTime dateAndTime = DateTime.Now;
                createOnWhen.Text = dateAndTime.ToString("dd-MM-yyyy");
                LabelRequiredDate.Text = dateAndTime.AddDays(28).ToString("dd-MM-yyyy");

                //When dropdownlist change, change the unit price and change the total price based on the quantity
                CalculationForUnitCostAndTotalCost(Convert.ToInt32(TextBoxOrderQuantity.Text));
            }
        }

        protected int validationOnTextBoxOrderQuantity()
        {
            int qty;
            if (Int32.TryParse(TextBoxOrderQuantity.Text, out qty))
            {
                return qty;
            }
            else
            {
                ErrorText.Text = "Please input a Whole Number between 1 and 1,000,000";
                Submit.Enabled = false;
                return 0;

            }



        }

        protected void DropDownListSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            CalculationForUnitCostAndTotalCost(validationOnTextBoxOrderQuantity());
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

        //esther-adding POitem to cart
        protected void Submit_Click(object sender, EventArgs e)
        {
            if (validationOnTextBoxOrderQuantity() > 0)
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
                    String url = "ClerkInventory.aspx";
                    Response.Write(BusinessLogic.MsgBox("Success: Item added to Purchase Order Staging"));
                    Response.Write("<script language=JavaScript>  opener.location.replace('" + url + "'); </script>");
                    Response.Write("<script language='javascript'> { window.close();}</script>");

                }
                catch (Exception ex)
                {
                    Label1.Text = ex.Message;
                }
                //Response.Redirect("POStagingSummary.aspx");
            }

        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClerkInventory.aspx");
        }
    }
}