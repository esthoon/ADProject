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
	public partial class depheadviewhistory : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            string dept = getmethoddepartment();//to get the department
            var q = BusinessLogic.gethistory(dept);
            Label3.Visible = false;
            GridView1.Visible = true;
            GridView1.DataSource = q.ToList();
            GridView1.DataBind();

        }

        public string getmethoddepartment()
        {
            int employeeid = Convert.ToInt32(Session["Employee"]);
            string user = BusinessLogic.GetUserID(employeeid);
            string dept = BusinessLogic.getdepartment(user);//to get the department
            return dept;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string dept = getmethoddepartment();//to get the department
            string status = dropdown1.SelectedValue;
            string name = TextBox2.Text;
            if (name != null && status == "All")
            {
                var q = BusinessLogic.gethistorybyname(name, dept);
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
                var q = BusinessLogic.gethistorybynameandstatus(name, dept, status);
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
                var q = BusinessLogic.gethistory(dept);
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

                var q = BusinessLogic.gethistorybystatus(dept, status);
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