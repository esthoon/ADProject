using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;
//Sruthi
namespace Team3ADProject.Protected
{
	public partial class DepartmentPinChange : System.Web.UI.Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			int employeeid = (int)Session["Employee"];

		}


		public string getmethoddepartment() //to get the department
		{
			int employeeid = Convert.ToInt32(Session["Employee"]);
			string user = BusinessLogic.GetUserID(employeeid);
			string dept = BusinessLogic.getdepartment(user);//to get the department
			return dept;
		}


		//Event handler for changing the pin
		protected void Button1_Click(object sender, EventArgs e) 
		{
			string dept = getmethoddepartment();
			string password1 = TextBox1.Text;
			string password2 = TextBox2.Text;
			if (password1.Equals(password2))
			{
				try
				{
					int password = Convert.ToInt32(password1);
					BusinessLogic.updatepassword(dept, password); //to update the password
					ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Pin updated successfully')", true);
				}
				catch (Exception x)
				{
					Exception ex = x;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('This pin is already in use by another department. Please try again.')", true);

                }
			}
			else
			{
				ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Entered pins do not match')", true);
			}
		}
	}
}