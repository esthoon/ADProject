using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;

//sruthi
namespace Team3ADProject.Protected
{
	public partial class WebForm3 : System.Web.UI.Page
	{
        protected void Page_Load(object sender, EventArgs e)
        {
            string dept = getmethoddepartment();//to get the department
            var q = BusinessLogic.getemployeenames(dept);//to get the employees of the department
            GridView1.DataSource = q;
            GridView1.DataBind();
            var k = BusinessLogic.gettemporaryheadname(dept);//to get the temporary head of the department
            if (k != null)
            {
                Label3.Visible = true;
                Label4.Visible = true;
                TextBox2.Visible = true;
                TextBox2.Text = k.ToString();
                Button2.Enabled = false;
                Button3.Visible = true;

            }
            else
            {
                Label3.Visible = false;
                Label4.Visible = false;
                TextBox2.Visible = false;
                //TextBox2.Text = k.ToString();
                Button3.Visible = false;
            }

        }

        public string getmethoddepartment()//to get the department
        {
            int employeeid = Convert.ToInt32(Session["Employee"]);
            string user = BusinessLogic.GetUserID(employeeid);
            string dept = BusinessLogic.getdepartment(user);//to get the department
            return dept;
        }
		// event handler for select button
        public void button_click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            HiddenField hd = (HiddenField)b.FindControl("HiddenField1");
            TextBox.Text = hd.Value;


        }
		// event handler for delegating
        protected void Button2_Click(object sender, EventArgs e)
        {
            try {
                string name = TextBox.Text;
                string dept = getmethoddepartment();
                int id = BusinessLogic.getemployeeid(name);//to get the employee id based on the name
                BusinessLogic.updatetemporaryhead(id, dept);//to update the temporary head
                Label3.Visible = true;
                Label4.Visible = true;
                TextBox2.Visible = true;
                var k = BusinessLogic.gettemporaryheadname(dept); // to get the temporary head name
                TextBox2.Text = k.ToString();
                Button3.Visible = true;
            }
                



            catch (System.Configuration.Provider.ProviderException ex)
            {
                System.Configuration.Provider.ProviderException ee = ex;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('The selected employee has already been delegated')", true);
                //Response.Write("<br/><br/>Exception:<br/>" + ee);
            }

            catch (System.Data.SqlClient.SqlException ex)
            {
                System.Data.SqlClient.SqlException ee = ex;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please ensure you have selected the correct data.')", true);
                //Response.Write("<br/><br/>Exception:<br/>" + ee);
            }

            catch (System.Data.Entity.Infrastructure.DbUpdateException ex)
            {
                System.Data.Entity.Infrastructure.DbUpdateException ee = ex;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please ensure you have selected an employee to delegate')", true);

            }

            catch (Exception ex)
            {
                Exception ee = ex;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Oops ! Something went wrong. Please try again.')", true);
   

            }

        }
		//  event handler for revoking
        protected void Button3_Click(object sender, EventArgs e)
        {
            string dept = getmethoddepartment();
            BusinessLogic.revoketemporaryhead(dept);//to revoke the temporary head
            Label3.Visible = false;
            Label4.Visible = false;
            TextBox2.Visible = false;
            Button3.Visible = false;
            Button2.Enabled = true;

        }

		//event handler for searching the employee
        protected void Button1_Click(object sender, EventArgs e)
        {
            string dept = getmethoddepartment();
            string name = TextBox1.Text.TrimEnd();
            var q = BusinessLogic.getemployeenamebysearch(dept, name);// to get the employee name
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
                GridView1.Visible = false;
            }


        }
    }
}