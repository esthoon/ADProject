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
    public partial class OtherPODetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                int id = Convert.ToInt32(Request.QueryString["id"]);
                GridView1.DataSource = BusinessLogic.getpurchaseorderdetail(id);
                GridView1.DataBind();
                purchase_order p = BusinessLogic.getpurchaseorder(id);
                supplier s = BusinessLogic.getSupplierNameforPurchaseorder(id);
                employee d = BusinessLogic.GetEmployee(p.employee_id);
                Label5.Text = id.ToString();
                Label4.Text = s.supplier_name;
                Label7.Text = s.supplier_id;
                Label9.Text = p.purchase_order_date.ToString("dd-MM-yyyy");
                Label11.Text = p.purchase_order_status;
                Label13.Text = d.employee_name;    
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Protected/ViewPOHistory.aspx");
        }
    }
}