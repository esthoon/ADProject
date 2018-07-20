using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;


namespace Team3ADProject.Protected
{
	public partial class WebForm4 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string dept = DepartmentPinChange.getmethoddepartment();
			var q = BusinessLogic.getemployeenames(dept);
			GridView1.DataSource = q;
			GridView1.DataBind();
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

		protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}

		protected void Button2_Click(object sender, EventArgs e)
		{
			string name = TextBox2.Text;
			if (!string.IsNullOrEmpty(name))
			{
				Label4.Visible = false;
				string dept = DepartmentPinChange.getmethoddepartment();
				int id = BusinessLogic.getemployeeid(name);
				BusinessLogic.saverepdetails(dept, id);
			}
			else
			{
				Label4.Visible = true;
			}


		}

		protected void Button1_Click(object sender, EventArgs e)
		{
			string dept = DepartmentPinChange.getmethoddepartment();
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
			

		
	
