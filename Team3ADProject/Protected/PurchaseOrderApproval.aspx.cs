using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;

namespace Team3ADProject.Protected
{
    //alan-start
    public partial class PurchaseOrderApproval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindGrid();

            }

            if (GridView1.Rows.Count <= 0)
            {
                Label1.Text = "There is no Pending PO";
                Label1.Visible = true;

            }
        }

        //bind grid
        private void BindGrid()
        {
            GridView1.DataSource = BusinessLogic.GetPurchaseOrders();
            GridView1.DataBind();


        }

        //grid updating
        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            int po = int.Parse(GridView1.Rows[e.RowIndex].Cells[0].Text);
            Response.Redirect("PurchaseOrderApprovalDetails.aspx?po=" + po);


        }
    }
}