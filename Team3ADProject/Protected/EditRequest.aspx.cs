using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Principal;
using Team3ADProject.Code;
using Team3ADProject.Model;
using System.Globalization;

namespace Team3ADProject.Protected
{
    public partial class EditRequest : System.Web.UI.Page
    {
        List<getRequisitionOrderDetails_Result> order = new List<getRequisitionOrderDetails_Result>();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string param_id =(string)Session["selectedrequestid"];
            if (param_id != null)
            {
                order = BusinessLogic.GetRequisitionorderDetail(param_id);
                Session["OrderDetail"] = order;
            }
            UpdatePage();
            requisition_order r =BusinessLogic.GetRequisitionOrderById(param_id);
                Label4.Text = r.requisition_id;
                Label5.Text = r.requisition_date.ToString("dd-MM-yyyy");
                Label6.Text = r.requisition_status;
            }
        }

        protected void UpdatePage()
        {
            List<getRequisitionOrderDetails_Result> order = (List<getRequisitionOrderDetails_Result>)Session["OrderDetail"];
            GridView1.DataSource = order;
            GridView1.DataBind();   
        }

        protected void CheckNullCart(object sender, EventArgs e)
        {
            List<getRequisitionOrderDetails_Result> order = (List<getRequisitionOrderDetails_Result>)Session["OrderDetail"];
            if (order.Count == 0)
            {
               
            }
            else
            {

            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Protected/EmployeeViewPending.aspx");
        }

        protected int isExisting(string id)
        {
            List<getRequisitionOrderDetails_Result> order = (List<getRequisitionOrderDetails_Result>)Session["OrderDetail"];
            for (int i = 0; i < order.Count; i++)
                if (order[i].description == id)
                    return i;
            return -1;
        }

        protected void ReqItemQuantityIncrease(string itemNumber)
        {
            List<getRequisitionOrderDetails_Result> order = (List<getRequisitionOrderDetails_Result>)Session["OrderDetail"];
            int index = isExisting(itemNumber);
            order[index].item_requisition_quantity++;
            order[index].item_requisition_price = order[index].item_requisition_quantity * order[index].unit_price;
            Session["OrderDetail"] = order;
        }

        //descrease item cart quantity
        protected void ReqItemQuantityDecrease(string itemNumber)
        {
            List<getRequisitionOrderDetails_Result> order = (List<getRequisitionOrderDetails_Result>)Session["OrderDetail"];
            int index = isExisting(itemNumber);
            if (order[index].item_requisition_quantity == 1)
            {
                ReqItemRemove(itemNumber);
            }
            else
            {
                order[index].item_requisition_quantity--;
                order[index].item_requisition_price = order[index].item_requisition_quantity * order[index].unit_price;
            }
            Session["OrderDetail"] = order;
        }

        //remove item from cart
        protected void ReqItemRemove(string itemNumber)
        {
           
            List<getRequisitionOrderDetails_Result> order = (List<getRequisitionOrderDetails_Result>)Session["OrderDetail"];
            order.RemoveAt(isExisting(itemNumber));
            Session["OrderDetail"] = order;
        }

        protected void DecQuantity_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            HiddenField hd = (HiddenField)b.FindControl("HiddenFieldDecQ");
            string itemNumber = hd.Value;
            ReqItemQuantityDecrease(itemNumber);
            UpdatePage();
            CheckNullCart(this, e);
        }

        protected void IncQuan_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            HiddenField hd = (HiddenField)b.FindControl("HiddenFieldIncQ");
            string itemNumber = hd.Value;
            ReqItemQuantityIncrease(itemNumber);
            UpdatePage();
            CheckNullCart(this, e);
        }

        protected void Remove_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            HiddenField hd = (HiddenField)b.FindControl("HiddenFieldRemove");
            string itemNumber = hd.Value;
            ReqItemRemove(itemNumber);
            UpdatePage();
            CheckNullCart(this, e);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Button3.CssClass = "btn btn-primary disabled";
            Button3.Enabled = false;
            List<getRequisitionOrderDetails_Result> order = (List<getRequisitionOrderDetails_Result>)Session["OrderDetail"];
            string id = (string)Session["selectedrequestid"];
            BusinessLogic.UpdateRequisitionOrderDetail(id, order);
            Response.Redirect("~/Protected/EmployeeViewPending.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Button2.CssClass = "btn btn-danger disabled";
            Button2.Enabled = false;
            string id = (string)Session["selectedrequestid"];
            BusinessLogic.Cancelrequisition(id);
            Response.Redirect("~/Protected/EmployeeViewPending.aspx");
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox b = (TextBox)sender;
            HiddenField hd = (HiddenField)b.FindControl("HiddenField1");
            string itemNumber = hd.Value;
            List<getRequisitionOrderDetails_Result> order = (List<getRequisitionOrderDetails_Result>)Session["OrderDetail"];
            int index = isExisting(itemNumber);
            if (order[index].item_requisition_quantity == 0)
            {
                ReqItemRemove(itemNumber);
            }
            else
            {
                order[index].item_requisition_quantity = Convert.ToInt32(b.Text.ToString());
                order[index].item_requisition_price = order[index].item_requisition_quantity * order[index].unit_price;
            }
            Session["OrderDetail"] = order;

        }
       
    }
}