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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gvInventoryList.DataSource = getCInventoryList(BusinessLogic.GetActiveInventories());
                gvInventoryList.DataBind();
                ddlSuppliers.DataSource = BusinessLogic.GetCategories();
                ddlSuppliers.DataBind();
            }

        }

        protected List<cInventory> getCInventoryList(List<inventory> list)
        {
            List<cInventory> returnlist = new List<cInventory>();
            foreach(inventory a in list)
            {
                returnlist.Add(new cInventory(a));
            }
            return returnlist;
        }

        protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}