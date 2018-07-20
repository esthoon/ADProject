using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;

namespace Team3ADProject.Protected
{
	public partial class WebForm3 : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			string dept = DepartmentPinChange.getmethoddepartment();
			var q= BusinessLogic.getemployeenames(dept);
			GridView1.DataSource = q;
			GridView1.DataBind();
			var k = BusinessLogic.gettemporaryheadname(dept);
			if (k != null)
			{
				Label4.Visible = true;
				TextBox2.Visible = true;
				TextBox2.Text = k.ToString();
				Button3.Visible = true;

			}
			else
			{
				Label4.Visible = false;
				TextBox2.Visible = false;
				//TextBox2.Text = k.ToString();
				Button3.Visible = false;
			}

		}
		public void button_click(object sender, EventArgs e)
		{
			Button b = (Button)sender;
			HiddenField hd = (HiddenField)b.FindControl("HiddenField1");
			TextBox.Text = hd.Value;
			

		}

		protected void Button2_Click(object sender, EventArgs e)
		{
			string name = TextBox.Text;
			string dept = DepartmentPinChange.getmethoddepartment();
			int id = BusinessLogic.getemployeeid(name);
			BusinessLogic.updatetemporaryhead(id, dept);
			Label4.Visible = true;
			TextBox2.Visible = true;
			var k = BusinessLogic.gettemporaryheadname(dept);
			TextBox2.Text = k.ToString();
			Button3.Visible = true;

		}

		protected void Button3_Click(object sender, EventArgs e)
		{
			string dept = DepartmentPinChange.getmethoddepartment();
			BusinessLogic.revoketemporaryhead(dept);
			Label4.Visible = false;
			TextBox2.Visible = false;
			Button3.Visible = false;

		}

		protected void Button1_Click(object sender, EventArgs e)
		{
			string dept = DepartmentPinChange.getmethoddepartment();
			string name = TextBox1.Text.TrimEnd();
			var q = BusinessLogic.getemployeenamebysearch(dept,name);
			if (q.Count()>0)
			{
				GridView1.Visible = true;
				Label5.Visible = false;
				GridView1.DataSource = q.ToList();
				GridView1.DataBind();
			}
			else
			{
				Label5.Visible = true;
				GridView1.Visible = false;
			}


		}
	}
}