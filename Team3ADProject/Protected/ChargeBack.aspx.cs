using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;
using Team3ADProject.Model;
using System.Globalization;

//Esther, Tharrani
namespace Team3ADProject.Protected
{
    public partial class ChargeBack : System.Web.UI.Page
    {
        private static List<deptusagechargeback_Result> list;
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (!IsPostBack)
            {
                List<department> depts = new List<department>();
                depts = BusinessLogic.ReturnDep();
                DropDownList1.DataSource = depts;
                DropDownList1.DataValueField = "department_id";
                DropDownList1.DataTextField = "department_name";
                DropDownList1.DataBind();
                DropDownList1.SelectedIndex = 0;
                string month = DateTime.Today.Month.ToString();
                if (month.Count() == 1)
                {
                    month = "0" + month;
                }
                string year = DateTime.Today.Year.ToString();
                TextBox1.Text = (year+"-"+month+"-"+"01");
                TextBox2.Text = DateTime.Today.ToString("yyyy-MM-dd");
                list = new List<deptusagechargeback_Result>();
                Label4.Visible = false;
                Label3.Visible = false;
                Button2.Visible = false;
            }
        }

        //bind data to grid
        protected void loadGrid()
        {
           try
            {

                DateTime startdate;
                DateTime enddate;
                string dept = DropDownList1.SelectedItem.Value;
                startdate = DateTime.ParseExact(TextBox1.Text, "yyyy-MM-dd", null);
                enddate = DateTime.ParseExact(TextBox2.Text, "yyyy-MM-dd", null);
                list = BusinessLogic.UsageChargeBack(startdate, enddate, dept.Trim());
                GridView1.DataSource = list;
                GridView1.DataBind();

                if (list.Count > 0)
                {
                    double price = list.Sum(x => x.price).Value;
                    Label4.Text = String.Format("{0:c2}", price);
                    Label3.Text = "Total price";
                    Label4.Visible = true;
                    Label3.Visible = true;
                    Button2.Enabled = true;
                    Button2.Visible = true;
                }
                else
                {
                    Button2.Enabled = false;
                    Label3.Visible = true;
                    Label3.Text = "Selected criteria have no result";
                    Label4.Visible = false;
                    Button2.Visible = false;
                }
            }

            catch(Exception ex)
            {
                Label3.Text = "Please enter a valid date.";
                Label3.Visible = true;
            }
            

        }

        //search function
        protected void Button1_Click(object sender, EventArgs e)
        {
            loadGrid();   
        }

        //send email
        protected void Button2_Click(object sender, EventArgs e)
        {
            DateTime startdate, enddate;
            startdate = DateTime.ParseExact(TextBox1.Text, "yyyy-MM-dd", null);
            enddate = DateTime.ParseExact(TextBox2.Text, "yyyy-MM-dd", null);
            string dept = DropDownList1.SelectedItem.Value.Trim();
            int dephead = BusinessLogic.departmentheadidbydeptid(dept);
            string to = BusinessLogic.RetrieveEmailByEmployeeID(dephead);
            string sub = "Stationery System: Usage Charge Back";
            string body = "Usage charge back for " + DropDownList1.SelectedItem.ToString() + " from "+ startdate.ToString("dd-MM-yyyy")+ " to "+ enddate.ToString("dd-MM-yyyy") + "is $ " + list.Sum(x=>x.price);
            BusinessLogic.sendMail(to, sub, body);
        }
    }
}