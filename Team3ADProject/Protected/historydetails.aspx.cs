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
	public partial class historydetails : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            String id = Request.QueryString["Id"];
            getpendingrequestdetails_Result request = BusinessLogic.getdetails(id);
            Label8.Text = request.id.ToString();
            Label9.Text = request.Date.ToString();
            Label10.Text = request.status.ToString();
            Label11.Text = request.Name.ToString();
            Label12.Text = request.Sum.ToString();
            var q = BusinessLogic.pendinggetitemdetails(id);
            GridView1.DataSource = q;
            GridView1.DataBind();

        }
    }
}