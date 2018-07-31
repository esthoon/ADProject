using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;
using Team3ADProject.Model;

namespace Team3ADProject.Protected
{
    public partial class EmployeeViewHistory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                GenerateGrid();
            }
        }

        protected void GenerateGrid()
        {
            int id = (int)Session["Employee"]; // replace by search query for logged in employee;
            List<requisition_order> list = BusinessLogic.GetAllRequisitionByEmployee(id);
            if (list.Count > 0)
            {
                TextBox1.Visible = true;
                Label2.Text = "Select Date (dd-MM-yyyy)";
                Button1.Visible = true;
                // ImageButton1.Visible = true;
                // Calendar1.Visible = false;
                DropDownList1.Visible = true;
                GridView1.DataSource = list;
                GridView1.DataBind();
                DropdonwAdd();
            }
            else
            {
                // Calendar1.Visible = false;
                // ImageButton1.Visible = false;
                Label2.Text = "There are no Request History";
                TextBox1.Visible = false;
                Button1.Visible = false;
                DropDownList1.Visible = false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            int i = (int)Session["Employee"];
            DateTime d; bool flag;
            if (String.IsNullOrEmpty(TextBox1.Text))
            {
                flag = false;
            }
            else
            {
                flag = true;
            }
            string s = DropDownList1.SelectedValue;
            //Selected all status with date
            if (flag == true && s == "All")
            {
                //
                String ss = TextBox1.Text;
                DateTime dt = DateTime.ParseExact(ss, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                String x = dt.ToString("yyyy-MMMM-dd");
                d = Convert.ToDateTime(x);


                //

                //d = DateTime.Parse(TextBox1.Text);
                GridView1.DataSource = BusinessLogic.GetRequisitionByEmployeeSearchDateAllStatus(i, d);
                GridView1.DataBind();
            }
            //no date but with status search
            else if (flag == false && s != "All")
            {
                GridView1.DataSource = BusinessLogic.GetRequisitionByEmployeeSearchStatus(i, s);
                GridView1.DataBind();
            }
            //no search condition
            else if (flag == false && s == "All")
            {
                GridView1.DataSource = BusinessLogic.GetAllRequisitionByEmployee(i);
                GridView1.DataBind();
            }
            //Both date and status
            else
            {
                String ss = TextBox1.Text;
                DateTime dt = DateTime.ParseExact(ss, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                String x = dt.ToString("yyyy-MMMM-dd");
                d = Convert.ToDateTime(x);
                //d = DateTime.Parse(TextBox1.Text);
                GridView1.DataSource = BusinessLogic.GetRequisitionByEmployeeSearchDate(i, d, s);
                GridView1.DataBind();
            }
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            // Calendar1.Visible = true;
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            TextBox1.Text = Calendar1.SelectedDate.ToString("dd-MM-yyyy");
            // Calendar1.Visible = false;
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date > DateTime.Today)
            {
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.Gray;
            }
        }

        protected void DropdonwAdd()
        {
            List<GetRequisitionStatus_Result> list = BusinessLogic.GetRequisitionStatus();
            DropDownList1.Items.Add("All");
            foreach (GetRequisitionStatus_Result x in list)
            {
                DropDownList1.Items.Add(x.requisition_status);
            }
        }

        protected void View_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            HiddenField hd = (HiddenField)b.FindControl("HiddenField1");
            string s = hd.Value.ToString();
            Response.Redirect("~/Protected/historydetails?Id=" + s);
        }


    }
}