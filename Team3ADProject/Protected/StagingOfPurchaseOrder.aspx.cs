using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;

namespace Team3ADProject.Protected
{
    public partial class StagingOfPurchaseOrder : System.Web.UI.Page
    {
        List<StagingItem> stagingitem;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["staging"] != null)
            {
                stagingitem = (List<StagingItem>)Session["staging"];
            }
            else
            {
                
            }

            GridView1.DataSource = stagingitem;
            GridView1.DataBind();

        }
    }
}