using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Model;
using Team3ADProject.Code;
using System.Globalization;

namespace Team3ADProject.Protected
{
	public partial class pendingdetails : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {

            string dept = getmethoddepartment();//to get the department
            String id = Request.QueryString["Id"];
            getpendingrequestdetails_Result request = BusinessLogic.getdetails(id);
            Label8.Text = request.id.ToString();
            Label9.Text = request.Date.ToString("dd-MM-yyyy");
            Label10.Text = request.status.ToString();
            Label11.Text = request.Name.ToString();
            Label12.Text = String.Format($"{request.Sum:c2}");
            ViewState["Sum"] = request.Sum;
            var q = BusinessLogic.pendinggetitemdetails(id);
            GridView1.DataSource = q;
            GridView1.DataBind();
            Label5.Text = String.Format($"{BusinessLogic.getbudgetbydept(dept):c2}");
            Label15.Text = String.Format($"{BusinessLogic.getspentbudgetbydept(dept):c2}");

        }
        public string getmethoddepartment()
        {
            int employeeid = Convert.ToInt32(Session["Employee"]);
            string user = BusinessLogic.GetUserID(employeeid);
            string dept = BusinessLogic.getdepartment(user);//to get the department
            return dept;
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            string dept = getmethoddepartment();//to get the department
            String id = Request.QueryString["Id"];
            BusinessLogic.approvestatus(id, TextBox1.Text, dept, Convert.ToInt32(ViewState["Sum"]));
            Response.Redirect("~/protected/depheadpending");

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            String id = Request.QueryString["Id"];
            BusinessLogic.rejectstatus(id, TextBox1.Text);
            Response.Redirect("~/protected/depheadpending");

        }
    }
}