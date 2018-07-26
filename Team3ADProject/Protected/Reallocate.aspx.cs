using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Team3ADProject.Code;
using Team3ADProject.Model;
using System.Windows.Forms;

namespace Team3ADProject.Protected
{
    public partial class Reallocate : System.Web.UI.Page
    {
        string itemNum;
        string description;
        int collectedItemCount;
        static List<spReallocateQty_Result> list;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["itemNum"] != null)
            {
                itemNum = Request.QueryString["itemNum"];
            }

            if (Request.QueryString["description"] != null)
            {
                description = Request.QueryString["description"];
            }

            if (!IsPostBack)
            {
                list = BusinessLogic.GetReallocateList(itemNum);

                gridview_Reallocate.DataSource = list;
                gridview_Reallocate.DataBind();

                Label_itemNum.Text = itemNum;
                Label_Description.Text = description;
                Label_warning.Visible = false;

                collectedItemCount = BusinessLogic.GetTotalCollectedVolumeForChosenItem(itemNum);
                Label_collectedAmount.Text = collectedItemCount.ToString();
            }
        }

        protected void gridview_Reallocate_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gvr in gridview_Reallocate.Rows)
            {
                string dpt_id = gvr.Cells[0].Text;
                System.Web.UI.WebControls.TextBox text = (System.Web.UI.WebControls.TextBox)gvr.FindControl("txt_distribution_qty");
                foreach (var q in list)
                {
                    if (dpt_id == q.department_id)
                    {
                        int qty = (int)q.item_distributed_quantity;
                        text.Text = qty.ToString();
                    }
                }
            }
        }

        protected void Button_Reallocate_Click(object sender, EventArgs e)
        {
            Label_warning.Visible = false;


            if (ValidatePreparedQty() < 0)
            {
                return;
            }

            // getting total input
            int totalEnterQty = 0;
            foreach (GridViewRow gvr in gridview_Reallocate.Rows)
            {
                System.Web.UI.WebControls.TextBox textbox = (System.Web.UI.WebControls.TextBox)gvr.FindControl("txt_distribution_qty");
                int distriQty = Convert.ToInt32(textbox.Text);
                totalEnterQty += distriQty;
            }

            int collectedQty = Convert.ToInt32(Label_collectedAmount.Text);

            // if what is insert > total collected, error msg
            if (totalEnterQty > collectedQty)
            {
                Label_warning.Visible = true;
                Label_warning.Text = "You entered more quantity than collected";
                return;
            }

            // if what is inserted < total, show message that the extra will be returned to inventory
            if (totalEnterQty < collectedQty)
            {
                System.Windows.Forms.DialogResult result = System.Windows.Forms.MessageBox.Show("You entered less items than collected. Do you want to return items to warehouse / inventory?", "Return to warehouse?", MessageBoxButtons.YesNo);
                //if no, return.
                if (result == DialogResult.No)
                {
                    return;
                }

                if (result == DialogResult.Yes)
                {
                    ReallocateItems();
                    int returnBalance = collectedQty - totalEnterQty;
                    BusinessLogic.ReturnToInventory(returnBalance, itemNum);
                    Response.Redirect("~/Protected/DisbursementSorting.aspx");
                }
            }

            else
            {
                ReallocateItems();
                Response.Redirect("~/Protected/DisbursementSorting.aspx");
            }
        }

        protected void ReallocateItems()
        {
            //Reset ROD Values

            foreach (GridViewRow gvr in gridview_Reallocate.Rows)
            {
                string dpt_id = gvr.Cells[0].Text;

                BusinessLogic.ResetRODTable(dpt_id, itemNum);
            }

            foreach (GridViewRow gvr in gridview_Reallocate.Rows)
            {
                string dpt_id = gvr.Cells[0].Text;
                System.Web.UI.WebControls.TextBox textbox = (System.Web.UI.WebControls.TextBox)gvr.FindControl("txt_distribution_qty");
                int distriQty = Convert.ToInt32(textbox.Text);

                BusinessLogic.UpdateRODTableOnReallocate(dpt_id, itemNum, distriQty);
            }
        }

        protected int ValidatePreparedQty()
        {
            bool flag = false;
            foreach (GridViewRow gvr in gridview_Reallocate.Rows)
            {
                int qtyOrder = Convert.ToInt32(gvr.Cells[1].Text);

                System.Web.UI.WebControls.TextBox tb = (System.Web.UI.WebControls.TextBox)gvr.FindControl("txt_distribution_qty");
                int qtyToPrep = Convert.ToInt32(tb.Text);

                System.Web.UI.WebControls.Label validator = (System.Web.UI.WebControls.Label)gvr.FindControl("Label1");
                validator.Visible = false;

                if (qtyToPrep > qtyOrder)
                {
                    validator.Visible = true;
                    validator.Text = "Amount is more than Ordered Qty";
                    flag = true;
                    break;
                }
            }
            if (flag == true)
                return -1;

            else
                return 1;
        }
    }
}