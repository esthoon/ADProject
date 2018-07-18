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
                List<string> categories = new List<string>();
                categories.Add("All Categories");
                foreach(string a in BusinessLogic.GetCategories())
                {
                    categories.Add(a);
                }
                ddlCategory.DataSource = categories;
                ddlCategory.DataBind();
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

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (CheckBox1.Checked)
            {
                gvInventoryList.DataSource = getCInventoryList(BusinessLogic.GetAllInventories());
                gvInventoryList.DataBind();
            }
            else
            {
                gvInventoryList.DataSource = getCInventoryList(BusinessLogic.GetActiveInventories());
                gvInventoryList.DataBind();
            }
        }

        protected void ddlCategory_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvInventoryList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}