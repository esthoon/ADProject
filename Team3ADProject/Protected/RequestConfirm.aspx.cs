using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;
using Team3ADProject.Model;

namespace Team3ADProject.Protected
{
    public partial class RequestConfirm : System.Web.UI.Page
    {
        List<getRequisitionOrderDetails_Result> order = new List<getRequisitionOrderDetails_Result>();
        protected void Page_Load(object sender, EventArgs e)
        {
            string param_id = (string)Request.QueryString["id"];
            if (param_id != null)
            {
                order = BusinessLogic.GetRequisitionorderDetail(param_id);
                GridView1.DataSource = order;
                GridView1.DataBind();
                requisition_order r = BusinessLogic.GetRequisitionOrderById(param_id);
                Label4.Text = r.requisition_id;
                String DateTemp = r.requisition_date.ToShortDateString().ToString();
                DateTime dt = DateTime.ParseExact(DateTemp, "d/M/yyyy", CultureInfo.InvariantCulture);
                Label5.Text = dt.ToString("dd-MM-yyyy");
                Label6.Text = r.requisition_status;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Protected/NewRequest.aspx");
        }
    }
}