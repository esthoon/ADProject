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
    public partial class DisbursementSorting : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //unpack Session
                List<CollectionListItem> cli = new List<CollectionListItem>();
                cli = (List<CollectionListItem>)Session["CollectionList"];

                dropList_searchByDpt.DataSource = BusinessLogic.GetDepartmentList();
                dropList_searchByDpt.DataBind();
            }
        }

        protected void btn_SortingSearch_Click(object sender, EventArgs e)
        {
            if (radBtn_searchByRO.Checked)
            {
                gv_SortingTable.DataSource = BusinessLogic.GetRODetailsByROId(txt_searchByRO.Text);
                gv_SortingTable.DataBind();
            }
        }
    }
}