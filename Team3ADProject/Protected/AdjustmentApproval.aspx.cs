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

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {

        }

        protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
        {

        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            
            GridViewRow row = GridView1.Rows[e.RowIndex];
            int adjid = int.Parse(GridView1.Rows[e.RowIndex].Cells[1].Text);
            string adjcomment= (row.FindControl("TextBox1") as TextBox).Text;

            BusinessLogic.Updateadj(adjid,adjcomment);
          //  BusinessLogic.sendMail();
            GridView1.EditIndex = -1;
            BindGrid();



        }




        private void BindGrid()
        {
            GridView1.DataSource = BusinessLogic.GetAdjustment();
            GridView1.DataBind();

        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            
            DateTime day = Calendar1.SelectedDate;
            GridView1.DataSource = BusinessLogic.Search(day);
            GridView1.DataBind();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.RowIndex];
            int adjid = int.Parse(GridView1.Rows[e.RowIndex].Cells[1].Text);
            string adjcomment = (row.FindControl("TextBox1") as TextBox).Text;

            BusinessLogic.rejectAdj(adjid, adjcomment);
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
                        string scomment = row.Cells[9].Text;

                        BusinessLogic.Updateadj(srow, scomment);
                        GridView1.EditIndex = -1;
                        BindGrid();

                    }
                }
            }
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
                        string selectcomment = row.Cells[9].Text;

                        BusinessLogic.rejectAdj(selectedrow, selectcomment);
                        GridView1.EditIndex = -1;
                        BindGrid();

                    }
                }
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }
    }
}