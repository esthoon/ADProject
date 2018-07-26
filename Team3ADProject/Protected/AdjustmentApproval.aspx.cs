using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Ajax.Utilities;
using Team3ADProject.Code;
using Team3ADProject.Model;

namespace Team3ADProject.Protected
{
    public partial class AdjustmentApproval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!this.IsPostBack)
            {


                this.BindGrid();

            }

        }


        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            GridViewRow row = GridView1.Rows[e.RowIndex];
            int adjid = int.Parse(GridView1.Rows[e.RowIndex].Cells[1].Text);
            string adjcomment = (row.FindControl("TextBox1") as TextBox).Text;
            string itemno = GridView1.Rows[e.RowIndex].Cells[4].Text;
            int qty = int.Parse((GridView1.Rows[e.RowIndex].Cells[5].Text));

            BusinessLogic.Updateadj(adjid, adjcomment, itemno, qty);

            GridView1.EditIndex = -1;
            BindGrid();


        }




        private void BindGrid()
        {
            // need to modify base on session in user role
            if ((string)Session["role"] == "storemanager")
            {
                GridView1.DataSource = BusinessLogic.StoreManagerGetAdj();

            }

            else if ((string)Session["role"] == "storesup")
            {
                GridView1.DataSource = BusinessLogic.StoreSupGetAdj();
            }

            
            GridView1.DataBind();

            NoRowDetail();



        }

        //protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        //{


        //    DateTime day = Calendar1.SelectedDate;
        //    TextBox2.Text = day.ToShortDateString();

        //    GridView1.DataSource = BusinessLogic.SearchAdj(day);

        //    GridView1.DataBind();
        //    NoRowDetail();


        //}

        private void NoRowDetail()
        {
            if (GridView1.Rows.Count <= 0)
            {
                LinkButton4.Visible = false;
                LinkButton3.Visible = false;
                TextBox2.Enabled = false;
                Button1.Enabled = false;
                Button2.Enabled = true;

                Label1.Text = "There are no more adjustment forms for approval";
                Label1.Visible = true;
            }
           
            else
            {
                LinkButton4.Visible = true;
                LinkButton3.Visible = true;
                TextBox2.Enabled = true;
                Button1.Enabled = true;
                Button2.Enabled = true;
                Label1.Visible = false;
                if (TextBox2.Text.IsNullOrWhiteSpace())
                {
                    Button2.Enabled = false;
                }
            }
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            int adjid = int.Parse(GridView1.Rows[e.RowIndex].Cells[1].Text);
            string adjcomment = (row.FindControl("TextBox1") as TextBox).Text;

            BusinessLogic.RejectAdj(adjid, adjcomment);
            GridView1.EditIndex = -1;
            BindGrid();
        }

        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                    if (isChecked)
                    {

                        int srow = Convert.ToInt32(row.Cells[1].Text);
                        string scomment = (row.FindControl("TextBox1") as TextBox).Text;

                        string itemno = row.Cells[4].Text;
                        int qty = int.Parse(row.Cells[5].Text);

                        BusinessLogic.Updateadj(srow, scomment, itemno, qty);


                    }
                }
            }

            GridView1.EditIndex = -1;
            BindGrid();
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in GridView1.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    bool isChecked = row.Cells[0].Controls.OfType<CheckBox>().FirstOrDefault().Checked;
                    if (isChecked)
                    {

                        int selectedrow = Convert.ToInt32(row.Cells[1].Text);
                        string selectcomment = (row.FindControl("TextBox1") as TextBox).Text;
                        BusinessLogic.RejectAdj(selectedrow, selectcomment);

                    }

                }
            }
            GridView1.EditIndex = -1;
            BindGrid();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           

            
                String s = TextBox2.Text;
                DateTime dt = DateTime.ParseExact(s, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                String x = dt.ToString("yyyy-MMMM-dd");
                DateTime search = Convert.ToDateTime(x);

                if ((string) Session["role"] == "storemanager")
                {

                    GridView1.DataSource = BusinessLogic.StoreManagerSearchAdj(search);

                }
                else if ((string) Session["role"] == "storesup")
                {

                    GridView1.DataSource = BusinessLogic.StoreSupSearchAdj(search);

                }

                GridView1.DataBind();
                NoRowDetail();
            

        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            TextBox2.Text = string.Empty;
            BindGrid();
        }
//new code
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            List<inventory> ilist = BusinessLogic.GetActiveInventory().ToList();
            int r = GridView1.Rows.Count;

            for (int i = 0; i < r; i++)
            {
                Label l = (Label)GridView1.Rows[i].FindControl("Label2");
                HiddenField hd = (HiddenField)GridView1.Rows[i].FindControl("HiddenFielditem");
                HiddenField hd2 = (HiddenField)GridView1.Rows[i].FindControl("HiddenFieldqty");
                LinkButton btn1 = (LinkButton)GridView1.Rows[i].FindControl("LinkButton1");
                CheckBox chk1 = (CheckBox)GridView1.Rows[i].FindControl("chkSelect");

                int xistingqty = Convert.ToInt32(hd2.Value);

                string item = hd.Value;

                for (int j = 0; j < ilist.Count; j++)
                {
                    if (item.Trim().Equals(ilist[j].item_number.Trim()))
                    {
                        l.Text = ilist[j].current_quantity.ToString().Trim();


                    }

                }


                if (xistingqty < 0)
                {
                    if (Math.Abs(xistingqty) >= Convert.ToInt32(l.Text))
                    {
                        btn1.CssClass = "btn btn-default";

                        btn1.Enabled = false;
                        chk1.Visible = false;
                    }

                }


            }

        }


//end-newcode


        //protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        //{
        //    if (e.Day.Date >= DateTime.Now)
        //    {
        //        e.Day.IsSelectable = false;
        //    }
        //}
    }
}