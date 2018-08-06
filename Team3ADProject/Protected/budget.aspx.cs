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

		//loading the grid with the budget of the department
        public void refreshgrid()
        {
            string dept = Session["Department"].ToString();
            GridView1.DataSource = BusinessLogic.getbudget(dept);
            GridView1.DataBind();
        }

		//for disabling the previous months in the dropdown
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


		//for updating the budget on button click
        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                int budget = Convert.ToInt32(TextBox1.Text);
                int employeeid = Convert.ToInt32(Session["Employee"]);
                string user = BusinessLogic.GetUserID(employeeid);
                string dept = Session["Department"].ToString();
                string month = DropDownList1.SelectedValue.ToString();
				//method to update the budget
                BusinessLogic.updatebudget(dept, month, budget);
                refreshgrid();
                DropDownList1.DataSource = months;
                DropDownList1.DataBind();
                disablemonths();
            }

            catch(Exception ee)
            {
                Exception ex = ee;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Oops ! Something Went wrong. Please try again.')", true);
            }
            
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
			//for enabling or disabling the submit button 
            int qty;

            if(Int32.TryParse(TextBox1.Text,out qty))
            {
                Button1.Enabled = true;
            }
            else
            {
                Button1.Enabled = false;
            }
        }
    }
}