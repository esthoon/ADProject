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
        static List<spReallocateQty_Result> list;
        static List<spGetFullCollectionROIDList_Result> roidList;

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
                roidList = BusinessLogic.GetFullCollectionROIDList();

                gridview_Reallocate.DataSource = list;
                gridview_Reallocate.DataBind();

                Label_itemNum.Text = itemNum;
                Label_Description.Text = description;
                Label_warning.Visible = false;


                int itemCount = 0;
                foreach (var q in list)
                {
                    itemCount += (int)q.item_distributed_quantity;
                }
                Label_collectedAmount.Text = itemCount.ToString();

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
                    ReturnToInventory(returnBalance);
                    Response.Redirect("~/Protected/DisbursementSorting.aspx");
                }
            }

            else
            {
                ReallocateItems();
                Response.Redirect("~/Protected/DisbursementSorting.aspx");
            }
        }

        protected void ReturnToInventory(int returnBalance)
        {
            CollectionListItem item = new CollectionListItem();
            item.itemNum = itemNum;
            item.qtyPrepared = returnBalance;

            BusinessLogic.AddtoInventory(item);
        }

        protected void ReallocateItems()
        {
            //Reset ROD Values
            foreach (spGetFullCollectionROIDList_Result q in roidList)
            {
                foreach (GridViewRow gvr in gridview_Reallocate.Rows)
                {
                    string dpt_id = gvr.Cells[0].Text;
                    if ((dpt_id.ToUpper().Trim() == q.requisition_id.Substring(0, 4).ToUpper().Trim()) && (itemNum.ToUpper().Trim() == q.item_number.ToUpper().Trim()))
                    {
                        q.item_distributed_quantity = 0;
                        q.item_pending_quantity = q.item_requisition_quantity;

                        requisition_order_detail rod = new requisition_order_detail();
                        rod.requisition_id = q.requisition_id;
                        rod.item_number = q.item_number;
                        rod.item_distributed_quantity = q.item_distributed_quantity;
                        rod.item_pending_quantity = q.item_pending_quantity;
                        BusinessLogic.UpdateRODetails(rod);
                    }
                }
            }

            foreach (GridViewRow gvr in gridview_Reallocate.Rows)
            {
                string dpt_id = gvr.Cells[0].Text;
                System.Web.UI.WebControls.TextBox textbox = (System.Web.UI.WebControls.TextBox)gvr.FindControl("txt_distribution_qty");
                int distriQty = Convert.ToInt32(textbox.Text);

                foreach (spGetFullCollectionROIDList_Result q in roidList)
                {
                    if ((dpt_id.ToUpper().Trim() == q.requisition_id.Substring(0, 4).ToUpper().Trim()) && (itemNum.ToUpper().Trim() == q.item_number.ToUpper().Trim()))
                    {
                        if (distriQty >= q.item_pending_quantity)
                        {
                            q.item_distributed_quantity += q.item_pending_quantity;
                            distriQty -= q.item_pending_quantity;
                            q.item_pending_quantity = 0;

                            requisition_order_detail rod = new requisition_order_detail();
                            rod.requisition_id = q.requisition_id;
                            rod.item_number = q.item_number;
                            rod.item_distributed_quantity = q.item_distributed_quantity;
                            rod.item_pending_quantity = q.item_pending_quantity;
                            BusinessLogic.UpdateRODetails(rod);
                        }
                        else
                        {
                            q.item_distributed_quantity += distriQty;
                            q.item_pending_quantity -= distriQty;
                            distriQty = 0;

                            requisition_order_detail rod = new requisition_order_detail();
                            rod.requisition_id = q.requisition_id;
                            rod.item_number = q.item_number;
                            rod.item_distributed_quantity = q.item_distributed_quantity;
                            rod.item_pending_quantity = q.item_pending_quantity;
                            BusinessLogic.UpdateRODetails(rod);
                        }
                    }
                }
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