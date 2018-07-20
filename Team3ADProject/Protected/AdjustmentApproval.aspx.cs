using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;

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

            BusinessLogic.Updateadj(adjid, adjcomment);

            GridView1.EditIndex = -1;
            BindGrid();


        }




        private void BindGrid()
        {

            if ((string)Session["role"] == "12")
            {
                GridView1.DataSource = BusinessLogic.StoreManagerGetAdj();

            }

            else if ((string)Session["role"] == "11")
            {
                GridView1.DataSource = BusinessLogic.StoreSupGetAdj();
            }
            GridView1.DataBind();
            NoRowDetail();





        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {


            DateTime day = Calendar1.SelectedDate;
            TextBox2.Text = day.ToShortDateString();



            GridView1.DataSource = BusinessLogic.SearchAdj(day);


            GridView1.DataBind();
            NoRowDetail();


        }

        private void NoRowDetail()
        {
            if (GridView1.Rows.Count <= 0)
            {
                LinkButton1.Visible = false;
                LinkButton3.Visible = false;
                TextBox2.Enabled = false;
                Button1.Enabled = false;
                Label1.Text = "There are no more adjustment forms for approval";
                Label1.Visible = true;
            }
            else
            {
                LinkButton1.Visible = true;
                LinkButton3.Visible = true;
                TextBox2.Enabled = true;
                Button1.Enabled = true;
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

        protected void LinkButton1_Click(object sender, EventArgs e)
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

                        BusinessLogic.Updateadj(srow, scomment);


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
            if (TextBox2.Text == null)
            {



                BindGrid();
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            if (e.Day.Date >= DateTime.Now)
            {
                e.Day.IsSelectable = false;
            }
        }
    }
}