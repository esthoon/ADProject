using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;

namespace Team3ADProject.Protected
{
	public partial class DepartmentPinChange : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            int employeeid = (int)Session["Employee"];

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
            string dept = getmethoddepartment();
            string password1 = TextBox1.Text;
            string password2 = TextBox2.Text;
            if (password1.Equals(password2))
            {
                try
                {
                    int password = Convert.ToInt32(password1);
                    BusinessLogic.updatepassword(dept, password);
                    Label3.Text = "password changed";
                }
                catch (Exception x)
                {
                    Response.Write("This password is already used. Please select others");
                }
            }
            else
            {
                Label3.Visible = true;
            }
        }
    }
}