using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;
using System.Web.Security;
using Team3ADProject.Model;

namespace Team3ADProject.Protected
{
	public partial class WebForm4 : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string dept = getmethoddepartment();//to get the department
                var q = BusinessLogic.getemployeenames(dept);
                GridView1.DataSource = q;
                GridView1.DataBind();
                updategrid();
            }
        }

        public string getmethoddepartment()
        {
            int employeeid = Convert.ToInt32(Session["Employee"]);
            string user = BusinessLogic.GetUserID(employeeid);
            string dept = BusinessLogic.getdepartment(user);//to get the department
            return dept;
        }

        public void updategrid()
        {
            string dept = getmethoddepartment();
            var k = BusinessLogic.getpreviousrepdetails(dept);
            GridView2.DataSource = k;
            GridView2.DataBind();
        }

        protected void button_click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            HiddenField hd = (HiddenField)b.FindControl("HiddenField1");
            TextBox2.Text = hd.Value;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            string name = TextBox2.Text;
            if (!string.IsNullOrEmpty(name))
            {
                Label4.Visible = false;
                int id = BusinessLogic.getemployeeid(name);
                string dept = getmethoddepartment();
                BusinessLogic.saverepdetails(dept, id);
                //sending the email to the department employees,store clerk on the Representative change.
                string messagebody = "The following person has been appointed as the representative for the collection of items \n \n" + name;
                BusinessLogic.sendMail("pssruthi123@gmail.com", "Department Representative Change", messagebody);
                string messagebody1 = "Congratulations,\n You have been appointed as the department representative for the collection of items ";
                BusinessLogic.sendMail("pssruthi123@gmail.com", "Department Representative Change", messagebody1);
                updategrid();

                //adding the person as rep
                employee getName = BusinessLogic.GetEmployee(id);
                Roles.AddUserToRole(getName.user_id, "deprep");
                Roles.RemoveUserFromRole(getName.user_id, "employee");
            }
            else
            {
                Label4.Visible = true;
            }


        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string dept = getmethoddepartment();
            string name = TextBox1.Text.TrimEnd();
            var q = BusinessLogic.getemployeenamebysearch(dept, name);
            if (q.Count() > 0)
            {
                GridView1.Visible = true;
                Label5.Visible = false;
                GridView1.DataSource = q.ToList();
                GridView1.DataBind();
            }
            else
            {
                Label5.Visible = true;
                GridView1.Visible = true;
            }
        }




    }
}