using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;

namespace Team3ADProject.Protected
{
    public partial class budget : System.Web.UI.Page
    {
        static string[] months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
        List<string> monthlist = new List<string>(months);
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                refreshgrid();
                DropDownList1.DataSource = months;
                DropDownList1.DataBind();
                disablemonths();

            }


        }

        public void refreshgrid()
        {
            GridView1.DataSource = BusinessLogic.getbudget("COMM");
            GridView1.DataBind();
        }

        public void disablemonths()
        {
            for (int i = 0; i < monthlist.Count; i++)
            {
                string s = DateTime.Now.ToString("MMM");
                if (s == DropDownList1.Items[i].ToString())
                {
                    break;
                }
                else
                {
                    DropDownList1.Items[i].Attributes.Add("disabled", "true");
                }

            }
        }



        protected void Button1_Click(object sender, EventArgs e)
        {
            int budget = Convert.ToInt32(TextBox1.Text);
            int employeeid = Convert.ToInt32(Session["Employee"]);
            string user = BusinessLogic.GetUserID(employeeid);
            string dept = BusinessLogic.getdepartment(user);//to get the department
            string month = DropDownList1.SelectedValue.ToString();
            BusinessLogic.updatebudget("COMM", month, budget);
            refreshgrid();
            DropDownList1.DataSource = months;
            DropDownList1.DataBind();
            disablemonths();
        }
    }
}