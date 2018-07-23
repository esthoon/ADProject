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
    public partial class Dashboard1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            var context = new LogicUniversityEntities();
            var recentRequisitionOrders = context.getLowStockItemsByCategory();

            LowStockItemGridView.DataSource = recentRequisitionOrders.ToList();
            LowStockItemGridView.DataBind();
           
            

        }

        protected void RequisitionOrder_Link_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl("~/Protected/ClerkInventory"));
        }

        protected void PurchaseOrder_Link_Click(object sender, EventArgs e)
        {
            Response.Redirect(ResolveUrl("~/Protected/ViewPOHistory"));
        }
    }
}