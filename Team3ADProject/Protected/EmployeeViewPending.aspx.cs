using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;
using System.Security.Principal;
using Team3ADProject.Model;
using System.Globalization;

namespace Team3ADProject.Protected
{
    public partial class EmployeeViewPending : System.Web.UI.Page
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
            List<requisition_order> list = BusinessLogic.GetPendingRequisitionByEmployee(id);
            if (list.Count > 0)
            {
                TextBox1.Visible = true;
                Label2.Text = "Search by Date(dd-MM-yyyy)";
                Button1.Visible = true;
                Button2.Visible = false;
                // ImageButton1.Visible = true;
                // Calendar1.Visible = false;
                GridView1.DataSource = list;
                GridView1.DataBind();
            }
            else
            {
                // Calendar1.Visible = false;
                // ImageButton1.Visible = false;
                Label2.Text = "You have no Pending Request";
                Button2.Visible = true;
                TextBox1.Visible = false;
                Button1.Visible = false;
            }
        }

        protected void EditButton_Click(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            HiddenField hd = (HiddenField)b.FindControl("HiddenField1");
            string s = hd.Value.ToString();
            Session["selectedrequestid"] = s;
            Response.Redirect("~/Protected/EditRequest.aspx");
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Protected/NewRequest.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(TextBox1.Text))
            {
                GenerateGrid();
            }
            else
            {
                int i = (int)Session["Employee"];
                //DateTime d = DateTime.ParseExact(TextBox1.Text, "yyyy-MM-dd", CultureInfo.InvariantCulture);
                String ss = TextBox1.Text;
                DateTime dt = DateTime.ParseExact(ss, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                String x = dt.ToString("yyyy-MMMM-dd");
                DateTime d = Convert.ToDateTime(x);

                string s = "Pending";
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

            else
            {
                e.Cell.ForeColor = System.Drawing.Color.GreenYellow;
            }
        }
    }
}