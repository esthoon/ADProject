using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Model;
using Team3ADProject.Code;

namespace Team3ADProject.Protected
{
    public partial class PurchaseOrderApprovalDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                int po = Convert.ToInt32(Request.QueryString["po"]);


                GridView1.DataSource = BusinessLogic.GetPODetails(po);
                GridView1.DataBind();

                List<sp_geteachPendingPOList_Result> x = BusinessLogic.GetEachPODetail(po);
                Label1.Text = x[0].employee_name;
                Label2.Text = (x[0].purchase_order_number).ToString();
                Label3.Text = String.Format("{0:c2}", x[0].total_price);
                Label4.Text = x[0].purchase_order_date.ToShortDateString();



            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

            String s = TextBox1.Text;
            int po = Convert.ToInt32(Request.QueryString["po"]);
            List<sp_getPendingPODetails_Result> y = BusinessLogic.GetPODetails(po);
            string email = y[0].supplier_email;


            BusinessLogic.UpdateUponPOApproval(po, s, email);
            Response.Redirect("~/Protected/PurchaseOrderApproval.aspx");

        }

        protected void Button2_Click(object sender, EventArgs e)
        {

            String remark = TextBox1.Text;
            int po = Convert.ToInt32(Request.QueryString["po"]);
            BusinessLogic.UpdateUponPOReject(po, remark);
            Response.Redirect("~/Protected/PurchaseOrderApproval.aspx");


        }
    }
}