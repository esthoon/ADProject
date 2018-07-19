using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;

namespace Team3ADProject.Protected
{
    public partial class ViewCollectionInformation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Load_Collections();
        }

        protected void Load_Collections()
        {
            gridview1.DataSource = BusinessLogic.ViewCollectionList();
            gridview1.DataBind();
        }

        protected void gridview1_SelectedIndexChanging(object sender,GridViewSelectEventArgs e)
        {
            Session["EmployeeName"] = gridview1.Rows[e.NewSelectedIndex].Cells[1].Text;
            Session["DepartmentName"] = gridview1.Rows[e.NewSelectedIndex].Cells[2].Text;
            Session["CollectionDate"] = gridview1.Rows[e.NewSelectedIndex].Cells[5].Text;
            Session["CollectionLocation"] = gridview1.Rows[e.NewSelectedIndex].Cells[4].Text;
            Session["CollectionTime"] = gridview1.Rows[e.NewSelectedIndex].Cells[6].Text;
            Session["disbursement_list_id"] = gridview1.Rows[e.NewSelectedIndex].Cells[3].Text;
            Response.Redirect("~/Protected/AcknowledgeDistributionList.aspx");
        }
    }
}