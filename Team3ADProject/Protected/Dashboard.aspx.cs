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
            if (!IsPostBack)
            {
                // Populate the requisition order table
                if (RecentROGridView != null)
                {
                    List<requisition_order> RequisitionOrders = BusinessLogic.GetRecentRequisitionOrders(5);
                    RecentROGridView.DataSource = RequisitionOrders;
                    RecentROGridView.DataBind();
                }


            }
        }
    }
}