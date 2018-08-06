using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;
using Team3ADProject.Model;
// Sruthi
namespace Team3ADProject.Protected
{
	public partial class depheadviewhistory : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            string dept = getmethoddepartment();//to get the department
            var q = BusinessLogic.gethistory(dept); // to get the department history of the ros
            Label3.Visible = false;
            GridView1.Visible = true;
            GridView1.DataSource = q.ToList();// loading the grid
            GridView1.DataBind();

        }

        public string getmethoddepartment()
        {
            int employeeid = Convert.ToInt32(Session["Employee"]);
            string user = BusinessLogic.GetUserID(employeeid);
            string dept = BusinessLogic.getdepartment(user);//to get the department
            return dept;
        }
		// Event handler for searching employee by name
        protected void Button1_Click(object sender, EventArgs e)
        {
            string dept = getmethoddepartment();//to get the department
            string status = dropdown1.SelectedValue;
            string name = TextBox2.Text;
            if (name != null && status == "All")
            {
                var q = BusinessLogic.gethistorybyname(name, dept); // to get the history by name
                if (q.Count() > 0)
                {
                    Label3.Visible = false;
                    GridView1.Visible = true;
                    GridView1.DataSource = q.ToList();
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.Visible = false;
                    Label3.Visible = true;
                }
            }
            else if (name != null && status != "All")
            {
                var q = BusinessLogic.gethistorybynameandstatus(name, dept, status); // to get the history by name and status
                if (q.Count() > 0)
                {
                    Label3.Visible = false;
                    GridView1.Visible = true;
                    GridView1.DataSource = q.ToList();
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.Visible = false;
                    Label3.Visible = true;
                }
            }
            else if (status == "All")
            {
                var q = BusinessLogic.gethistory(dept); // to get all ros of the department
                if (q.Count() > 0)
                {
                    Label3.Visible = false;
                    GridView1.Visible = true;
                    GridView1.DataSource = q.ToList();
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.Visible = false;
                    Label3.Visible = true;
                }
            }

            else
            {

                var q = BusinessLogic.gethistorybystatus(dept, status); // to get the history of the ros by status
                if (q.Count() > 0)
                {
                    Label3.Visible = false;
                    GridView1.Visible = true;
                    GridView1.DataSource = q.ToList();
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.Visible = false;
                    Label3.Visible = true;
                }

            }


        }
		//to redirect to details page
        public void button_click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            HiddenField hd = (HiddenField)b.FindControl("HiddenField1");
            string s = hd.Value;
            Response.Redirect("~/protected/historydetails?Id=" + s.TrimEnd());

        }

        protected void Unnamed1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}