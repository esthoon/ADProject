using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;

namespace Team3ADProject.Protected
{
    public partial class PurchaseOrderApproval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();

            }
        }


        private void BindGrid()
        {
            GridView1.DataSource = BusinessLogic.GetPurchaseOrders();
            GridView1.DataBind();

        }
    }
}